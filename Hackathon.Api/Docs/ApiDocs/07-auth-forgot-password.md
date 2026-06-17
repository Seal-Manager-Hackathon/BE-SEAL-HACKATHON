# Forgot password

## Tác dụng
Gửi yêu cầu quên mật khẩu để user nhận email/link chứa token đặt lại mật khẩu.

## URL
`POST /api/v1/auth/forgot-password`

## Request body
```json
{
  "email": "user@example.com"
}
```

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "message": "FORGOT_PASSWORD_REQUEST_ACCEPTED"
  }
}
```

## Business rules
- `email` là bắt buộc và phải đúng định dạng email.
- Nếu email tồn tại, hệ thống tạo reset token/OTP và gửi mail cho user.
- Trả response thành công ngay cả khi email không tồn tại để tránh lộ thông tin tài khoản (ko trả lỗi `USER_NOT_FOUND`).
- Reset token có thời hạn sử dụng (2 phút).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
