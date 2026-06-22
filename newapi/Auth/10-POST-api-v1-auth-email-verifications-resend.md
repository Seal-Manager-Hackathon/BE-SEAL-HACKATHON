# API 10: Gửi lại email xác thực tài khoản

## Tác dụng
Gửi lại email chứa mã xác thực tài khoản (OTP/Token) trong trường hợp người dùng chưa nhận được hoặc mã cũ hết hạn.

## URL
`POST /api/v1/auth/email-verifications/resend`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Body
```json
{
  "email": "student@college.edu.vn"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "message": "VERIFICATION_EMAIL_RESENT"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- `email` là bắt buộc và phải đúng định dạng.
- User tương ứng với email phải tồn tại trong hệ thống.
- Nếu tài khoản đã xác thực email từ trước (`IsVerified = true`), API từ chối gửi lại và báo lỗi `USER_ALREADY_VERIFIED`.
- Hệ thống vô hiệu hóa các token xác thực cũ của user này, tạo token mới có thời hạn và gửi email chứa token mới đi.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Tài khoản email này đã được xác minh trước đó.",
  "MessageCode": "USER_ALREADY_VERIFIED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | VALIDATION_FAILED | Trường email bị thiếu hoặc sai định dạng. |
| 404 | USER_NOT_FOUND | Không tìm thấy tài khoản tương ứng với email này. |
| 409 | USER_ALREADY_VERIFIED | Tài khoản đã được xác minh trước đó, không cần resend. |
| 500 | EMAIL_SENDING_FAILED | Gặp sự cố kết nối với mail server (SMTP). |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ không mong muốn. |
