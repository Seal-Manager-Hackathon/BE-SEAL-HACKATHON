# Forgot password

## Tác dụng
Gửi yêu cầu quên mật khẩu để user nhận email/link hoặc mã xác thực đặt lại mật khẩu.

## URL
`POST /api/auth/forgot-password`

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
    "message": "RESET_PASSWORD_EMAIL_SENT"
  }
}
```

## Business rules
- `email` là bắt buộc và phải đúng định dạng email.
- Nếu email tồn tại, hệ thống tạo reset token/OTP và gửi mail cho user.
- Không trả thông tin nhạy cảm như token thô nếu token chỉ dùng qua email.
- Có thể trả response thành công ngay cả khi email không tồn tại để tránh lộ thông tin tài khoản.
- Reset token/OTP phải có thời hạn sử dụng.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | EMAIL_REQUIRED | Email is required. |
| 400 | INVALID_EMAIL_FORMAT | Email format is invalid. |
| 429 | TOO_MANY_REQUESTS | Too many forgot password requests. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
