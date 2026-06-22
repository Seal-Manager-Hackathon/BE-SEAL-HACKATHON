# Resend email verification

## Tác dụng
Gửi lại email xác thực tài khoản cho user khi user chưa xác thực email hoặc OTP trước đó đã hết hạn.

## URL
`POST /api/v1/auth/email-verifications/resend`

## Request body
```json
{
  "email": "string"
}
```

| Field | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `email` | `string` | Có | Email của tài khoản cần gửi lại mã xác thực. |

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
  "message": "VERIFICATION_EMAIL_RESENT"
}
```

## Business rules
- `email` là bắt buộc.
- Email phải tồn tại trong hệ thống.
- Nếu tài khoản đã xác thực email rồi, không gửi lại (báo lỗi).
- OTP cũ sẽ bị vô hiệu hóa và OTP mới được tạo.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | NOT_FOUND | EMAIL_NOT_FOUND |
| 400 | BAD_REQUEST | EMAIL_ALREADY_VERIFIED |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
