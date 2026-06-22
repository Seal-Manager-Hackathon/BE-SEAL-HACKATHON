# API 7: Đổi mật khẩu

## Tác dụng
Đổi mật khẩu của tài khoản người dùng đang đăng nhập sang mật khẩu mới.

## URL
`PATCH /api/v1/auth/change-password`

## Quyền
Authenticated User (Yêu cầu đăng nhập)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Body
```json
{
  "currentPassword": "CurrentSecurePassword123",
  "newPassword": "NewSecurePassword456",
  "confirmPassword": "NewSecurePassword456"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "message": "PASSWORD_CHANGED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Yêu cầu access token hợp lệ của user đang hoạt động.
- `currentPassword` phải khớp với mật khẩu hiện tại trong DB.
- `newPassword` và `confirmPassword` phải giống nhau hoàn toàn.
- Khuyến nghị kiểm tra độ mạnh mật khẩu mới (ít nhất 8 ký tự, có chữ và số).
- Sau khi đổi mật khẩu thành công, toàn bộ refresh token cũ của user nên bị thu hồi để yêu cầu đăng nhập lại ở các thiết bị khác (nếu dùng API revoke all).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Bad Request",
  "Status": 400,
  "Detail": "Mật khẩu hiện tại không chính xác.",
  "MessageCode": "CURRENT_PASSWORD_INVALID",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | CURRENT_PASSWORD_INVALID | Mật khẩu hiện tại nhập sai. |
| 400 | PASSWORD_CONFIRMATION_NOT_MATCH | Mật khẩu mới và mật khẩu xác nhận không khớp. |
| 401 | MISSING_ACCESS_TOKEN | Access token bị thiếu hoặc không hợp lệ. |
| 404 | USER_NOT_FOUND | Không tìm thấy thông tin người dùng trong cơ sở dữ liệu. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ khi mã hóa mật khẩu hoặc lưu database. |
