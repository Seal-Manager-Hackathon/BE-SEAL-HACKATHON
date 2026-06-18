# Lấy thông tin cá nhân (Profile)

## Tác dụng
Cho phép người dùng đã đăng nhập lấy thông tin cá nhân của họ.

## URL
`GET /api/users/profile`

## Authorization
Yêu cầu access token hợp lệ.

## Path parameters
Không có.

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/users/profile
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "2026-06-18T23:00:00Z",
  "value": {
    "email": "user@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "phoneNumber": "0123456789",
    "avatarUrl": "https://example.com/avatar.jpg",
    "bio": "Software developer",
    "address": "123 Main St",
    "dateOfBirth": "2000-01-01T00:00:00Z",
    "studentId": "STU123456",
    "college": "FPT University",
    "imgUrl": "https://example.com/img.jpg",
    "linkUrl": "https://github.com/johndoe",
    "status": "0",
    "banReason": null
  }
}
```

## Business rules
- User phải tồn tại trong hệ thống.
- User status sẽ trả về dạng enum.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing or invalid. |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
