# Reset password

## Tác dụng
Cho phép user đặt lại mật khẩu mới bằng token đã nhận được qua email (từ forgot-password).

## URL
`POST /api/v1/auth/reset-password`

## Request body
```json
{
  "token": "string",
  "newPassword": "string"
}
```

| Field | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `token` | `string` | Có | Token đặt lại mật khẩu được gửi qua email. |
| `newPassword` | `string` | Có | Mật khẩu mới, phải đáp ứng yêu cầu độ mạnh. |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string",
  "timestampUtc": "datetime",
  "data": null,
  "message": "PASSWORD_RESET_SUCCESSFULLY"
}
```

## Business rules
- `token` và `newPassword` là bắt buộc.
- Token phải hợp lệ và chưa hết hạn.
- Token chỉ sử dụng được một lần; sau khi reset thành công token sẽ bị vô hiệu hóa.
- Mật khẩu mới phải đáp ứng chính sách bảo mật (độ dài, ký tự đặc biệt, v.v.).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | INVALID_TOKEN |
| 400 | BAD_REQUEST | TOKEN_EXPIRED |
| 400 | BAD_REQUEST | PASSWORD_POLICY_NOT_MET |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
