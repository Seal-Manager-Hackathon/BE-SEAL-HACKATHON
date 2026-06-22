# Thu hồi toàn bộ phiên đăng nhập (Revoke All Tokens)

## Tác dụng
Thu hồi toàn bộ refresh token hiện có của user đang đăng nhập (logout tất cả các thiết bị khác, ngoại trừ phiên hiện tại nếu muốn, hoặc logout sạch sẽ).

## URL
`POST /api/v1/auth/tokens/revoke-all`

## Quyền
Authenticated User (Yêu cầu đăng nhập)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "message": "ALL_TOKENS_REVOKED"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Yêu cầu token truy cập hợp lệ.
- Hệ thống tìm tất cả refresh token của user hiện tại đang active (`RevokedAt == null` và `ExpiredAt > DateTimeOffset.UtcNow`).
- Cập nhật trường `RevokedAt = DateTimeOffset.UtcNow` cho toàn bộ các token đó để vô hiệu hóa chúng vĩnh viễn.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Unauthorized",
  "Status": 401,
  "Detail": "Vui lòng đăng nhập để thực hiện hành động này.",
  "MessageCode": "UNAUTHORIZED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc hết hạn. |
| 404 | USER_NOT_FOUND | Không tìm thấy thông tin tài khoản tương ứng. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi phát sinh tại server khi cập nhật DB. |
