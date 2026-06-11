# Register

## Tác dụng
Đăng ký user mới và gửi email xác thực tài khoản.

## URL
`POST /api/auth/register`

## Request body
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "password": "string",
  "confirmPassword": "string"
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
  "value": "Đăng ký thành công. Vui lòng kiểm tra email để xác thực tài khoản."
}
```

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | PASSWORD_CONFIRMATION_NOT_MATCH |
| 409 | CONFLICT | EMAIL_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
