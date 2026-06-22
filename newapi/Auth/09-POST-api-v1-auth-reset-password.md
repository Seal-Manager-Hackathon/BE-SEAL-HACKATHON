# API 9: Đặt lại mật khẩu (Reset Password)

## Tác dụng
Reset mật khẩu mới bằng token đặt lại mật khẩu nhận qua link email. User bấm link reset password trong email, FE chuyển sang trang đặt lại mật khẩu, đọc token trên URL, cho user nhập mật khẩu mới và nhập lại mật khẩu rồi gọi API này.

## URL
`POST /api/v1/auth/reset-password`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Body
```json
{
  "token": "reset_password_token_string",
  "newPassword": "NewSecurePassword123",
  "confirmPassword": "NewSecurePassword123"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "message": "PASSWORD_RESET_SUCCESSFUL"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- `token` phải tồn tại trong DB, chưa từng được sử dụng (`IsUsed = false`) và còn hạn (`ExpiresAt > DateTimeOffset.UtcNow`).
- `newPassword` và `confirmPassword` phải trùng nhau hoàn toàn.
- Khi reset mật khẩu thành công, token đó phải được đánh dấu đã sử dụng (`IsUsed = true`) để ngăn chặn việc tái sử dụng token cũ.
- Cập nhật trường `HashPassword` của user tương ứng trong bảng `Users`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Bad Request",
  "Status": 400,
  "Detail": "Token đặt lại mật khẩu không hợp lệ, đã dùng hoặc hết hạn.",
  "MessageCode": "INVALID_OR_EXPIRED_RESET_TOKEN",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | INVALID_OR_EXPIRED_RESET_TOKEN | Token không tồn tại, đã dùng hoặc hết hạn. |
| 400 | PASSWORD_CONFIRMATION_NOT_MATCH | Mật khẩu mới và mật khẩu xác nhận không khớp. |
| 404 | USER_NOT_FOUND | Không tìm thấy user liên kết với token này. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống phát sinh. |
