# API 4: Xác thực Email

## Tác dụng
Xác thực email của tài khoản bằng token; nếu xác thực thành công lần đầu thì trả về token đăng nhập.

## URL
`POST /api/v1/auth/email-verifications`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Body
```json
{
  "token": "verification_token_string"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`. Trả về tokens nếu tài khoản verify lần đầu.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "accessToken": "ey...",
    "refreshToken": "rf...",
    "message": "EMAIL_VERIFICATION_SUCCESSFUL"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Token xác thực email phải tồn tại trong DB và còn hạn.
- User gắn với token phải còn tồn tại trong hệ thống.
- Nếu user đã verify trước đó, API trả thông báo `USER_ALREADY_VERIFIED`.
- Nếu verify lần đầu thành công, hệ thống đánh dấu email đã xác thực (`IsVerified = true`, cập nhật `VerifyEmailAt`) và trả token đăng nhập.

### Bảng trạng thái EmailVerificationStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ xác thực |
| `1` | Verified | Đã xác thực thành công |
| `2` | Expired | Mã xác thực đã hết hạn |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Bad Request",
  "Status": 400,
  "Detail": "Mã xác thực không hợp lệ hoặc đã hết hạn.",
  "MessageCode": "INVALID_OR_EXPIRED_EMAIL_VERIFICATION_TOKEN",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | INVALID_OR_EXPIRED_EMAIL_VERIFICATION_TOKEN | Token không chính xác, đã sử dụng hoặc quá hạn. |
| 404 | USER_NOT_FOUND | Không tìm thấy user liên kết với token này. |
| 404 | EMAILVALID_NOT_FOUND | Không tìm thấy bản ghi token trong hệ thống. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống phát sinh. |
