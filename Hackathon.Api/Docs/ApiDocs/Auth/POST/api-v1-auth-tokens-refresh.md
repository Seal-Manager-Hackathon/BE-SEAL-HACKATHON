# Refresh token

## Tác dụng
Rotate refresh token và cấp access token mới.

## URL
`POST /api/v1/auth/tokens/refresh`

## Request body
Không có. Cần cookie:
```json
{
  "Refresh-Token": "string"
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
  "message": "SUCCESS"
}
```

## Business rules
- Request phải gửi refresh token hợp lệ trong cookie.
- Refresh token còn hạn mới được dùng để cấp token mới.
- Nếu access token hiện tại vẫn còn hiệu lực thì không rotate refresh token.
- Khi refresh thành công, refresh token cũ bị thay thế bằng refresh token mới.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | ACCESS_TOKEN_STILL_VALID |
| 400 | BAD_REQUEST | REFRESH_TOKEN_MISSING |
| 401 | EXPIRED_REFRESH_TOKEN | REFRESH_TOKEN_HAS_EXPIRED |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
