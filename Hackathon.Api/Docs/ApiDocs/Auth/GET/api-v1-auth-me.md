# Get current user

## Tác dụng
Lấy thông tin profile ngắn gọn của user đang đăng nhập.

## URL
`GET /api/v1/auth/me`

## Authorization
Yêu cầu access token hợp lệ.

## Request body
Không có.

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
    "id": "guid",
    "role": "string",
    "firstName": "string",
    "lastName": "string",
    "email": "string",
    "avatar": "string|null"
  },
  "message": "SUCCESS"
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ trả thông tin của user đang đăng nhập, không truyền userId từ client.
- User phải còn tồn tại và chưa bị disable.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
