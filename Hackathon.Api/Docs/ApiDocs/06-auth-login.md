# Login

## Tác dụng
Đăng nhập bằng email/password và cấp access/refresh token.

## URL
`POST /api/auth/login`

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
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "accessToken": "string|null",
    "refreshToken": "string|null",
    "message": "LOGIN_SUCCESSFUL"
  }
}
```

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | Validation | email/password thiếu hoặc sai format |
| 401 | UNAUTHORIZED | INVALID_EMAIL_OR_PASSWORD |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
