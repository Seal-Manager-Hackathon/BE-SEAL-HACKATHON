# Get current user

## Tác dụng
Lấy thông tin profile ngắn gọn của user đang đăng nhập.

## URL
`GET /api/v1/auth/me`

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

## Business rules
- Request phải có access token hợp lệ.
- Chỉ trả thông tin của user đang đăng nhập, không truyền userId từ client.
- User phải còn tồn tại và chưa bị disable.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
