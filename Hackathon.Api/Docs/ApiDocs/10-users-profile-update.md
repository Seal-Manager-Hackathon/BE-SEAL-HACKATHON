# Update profile

## Tác dụng
Cập nhật thông tin profile của user hiện tại.

## URL
`PATCH /api/v1/users/profile`

## Authorization
Yêu cầu access token hợp lệ.

## Request body
```json
{
  "firstName": "string|null",
  "lastName": "string|null",
  "phoneNumber": "string|null",
  "studentId": "string|null",
  "college": "string|null",
  "avatarUrl": "string|null",
  "bio": "string|null"
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
    "isVerified": false,
    "createdAt": "datetimeoffset",
    "updatedAt": "datetimeoffset"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ user hiện tại được cập nhật profile của chính mình.
- Không cho cập nhật các field nhạy cảm như `Email`, `HashPassword`, `Role` qua API này.
- Cập nhật `UpdatedAt` sau khi lưu thành công.
- Student cần hoàn thành các field profile bắt buộc trước khi tạo/join team.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | INVALID_PROFILE_DATA |
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
