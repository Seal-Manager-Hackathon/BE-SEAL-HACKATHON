# Get current user

## Tác dụng
Lấy thông tin profile ngắn gọn của user đang đăng nhập.

## URL
`GET /api/users/me`

## Request body
Không có.

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "firstName": "string",
    "lastName": "string",
    "email": "string",
    "avatar": "string|null"
  }
}
```

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | Token không hợp lệ hoặc bị chặn bởi [Authorize] |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
