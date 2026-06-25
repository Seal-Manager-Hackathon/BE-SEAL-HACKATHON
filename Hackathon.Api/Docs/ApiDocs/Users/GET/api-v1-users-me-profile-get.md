# Lấy thông tin cá nhân (Profile)

## Tác dụng
Cho phép người dùng đã đăng nhập lấy thông tin cá nhân của họ.

## URL
`GET /api/v1/users/profile`

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

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-18T23:00:00Z",
  "message": "SUCCESS",
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
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
    "status": 0, /* 0: Active, 1: Inactive, 2: Banned */
    "banReason": null,
    "role": 2 /* 0: Admin, 1: Staff, 2: Student, 3: Lecturer */
  }
}
```

## Business rules
- User phải tồn tại trong hệ thống.
- User status sẽ trả về dạng enum.

### Bảng vai trò RoleEnum (Integer)
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Admin | Quản trị viên hệ thống |
| `1` | Staff | Nhân viên vận hành sự kiện |
| `2` | Student | Sinh viên / Thí sinh tham gia thi đấu |
| `3` | Lecturer | Giảng viên hỗ trợ chuyên môn hoặc chấm thi |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
