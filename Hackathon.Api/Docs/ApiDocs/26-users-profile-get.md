# Get profile

## Tác dụng
Lấy thông tin profile của user hiện tại.

## URL
`GET /api/users/profile`

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
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "id": "guid",
    "email": "string",
    "firstName": "string|null",
    "lastName": "string|null",
    "phoneNumber": "string|null",
    "studentId": "string|null",
    "college": "string|null",
    "avatarUrl": "string|null",
    "bio": "string|null",
    "status": "string|null",
    "isDisable": false,
    "createdAt": "datetimeoffset",
    "updatedAt": "datetimeoffset"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Profile fields nằm trực tiếp trong `Users`, không dùng bảng `Profile` riêng.
- Không trả thông tin nhạy cảm như `HashPassword`, refresh token hoặc reset token.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | USER_NOT_FOUND | User not found. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
