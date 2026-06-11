# Verify email

## Tác dụng
Xác thực email bằng token; nếu xác thực thành công lần đầu thì trả token đăng nhập.

## URL
`POST /api/auth/email-verifications`

## Request body
```json
{
  "token": "string"
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
    "accessToken": "string|null",
    "refreshToken": "string|null",
    "message": "EMAIL_VERIFICATION_SUCCESSFUL | USER_ALREADY_VERIFIED"
  }
}
```

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | INVALID_OR_EXPIRED_EMAIL_VERIFICATION_TOKEN |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
