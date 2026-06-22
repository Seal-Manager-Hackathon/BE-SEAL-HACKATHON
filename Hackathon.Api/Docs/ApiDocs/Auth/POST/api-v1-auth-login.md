# Login

## Tác dụng
Đăng nhập bằng email/password và cấp access/refresh token.

## URL
`POST /api/v1/auth/login`

## Request body
```json
{
  "email": "string",
  "password": "string"
}
```

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string",
  "timestampUtc": "datetime",
  "data": {
    "accessToken": "string|null",
    "refreshToken": "string|null"
  },
  "message": "LOGIN_SUCCESSFUL"
}
```

## Business rules
- Email và password là bắt buộc.
- Email/password phải khớp với tài khoản đang tồn tại.
- Tài khoản bị disable (`IsDisable = true`) không thể đăng nhập.
- Tài khoản bị cấm (`Status = Banned`) không thể đăng nhập.
- Tài khoản chưa xác thực email (`IsVerified = false`) nhưng nhập đúng password, hệ thống sẽ tự động gửi lại OTP qua email và block luồng đăng nhập (báo lỗi `EMAIL_UNVERIFIED_OTP_SENT`).
- Đăng nhập thành công sẽ cấp access token và refresh token mới.
- Access token và refresh token được trả về trong response body và set vào cookie.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | NOT_FOUND | EMAIL_NOT_FOUND |
| 403 | FORBIDDEN | USER_IS_BANNED |
| 401 | UNAUTHORIZED | EMAIL_UNVERIFIED_OTP_SENT |
| 401 | UNAUTHORIZED | INVALID_EMAIL_OR_PASSWORD |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
