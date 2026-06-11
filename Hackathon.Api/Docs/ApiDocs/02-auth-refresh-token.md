# Refresh token

## Tác dụng
Rotate refresh token và cấp access token mới.

## URL
`POST /api/auth/tokens/refresh`

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
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "accessToken": "string|null",
    "refreshToken": "string|null",
    "message": "string|null"
  }
}
```

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | ACCESS_TOKEN_STILL_VALID |
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | EXPIRED_REFRESH_TOKEN | Refresh token has expired. Please login again. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
