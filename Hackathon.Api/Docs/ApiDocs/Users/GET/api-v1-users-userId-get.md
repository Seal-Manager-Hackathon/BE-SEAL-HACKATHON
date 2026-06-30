# Lấy thông tin người dùng theo ID (Get User Detail)

## Tác dụng
Cho phép người dùng đã đăng nhập xem thông tin chi tiết của một người dùng khác qua ID.

## URL
`GET /api/v1/users/{userId}`

## Authorization
Yêu cầu access token hợp lệ.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `userId` | `guid` | Có | ID của người dùng cần xem thông tin. |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/users/3fa85f64-5717-4562-b3fc-2c963f66afa6
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
  "timestampUtc": "2026-06-30T10:00:00Z",
  "message": "SUCCESS",
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "email": "student@example.com",
    "firstName": "Nguyễn",
    "lastName": "Văn A",
    "phoneNumber": "0123456789",
    "avatarUrl": "https://example.com/avatar.jpg",
    "bio": "Sinh viên năm 3 chuyên ngành CNTT",
    "address": "Hà Nội",
    "dateOfBirth": "2000-01-01T00:00:00Z",
    "studentId": "STU123456",
    "college": "FPT University",
    "imgUrl": "https://example.com/img.jpg",
    "linkUrl": "https://github.com/student",
    "role": 2, /* 0: Admin, 1: Staff, 2: Student, 3: Lecturer */
    "status": 1, /* 0: Inactive, 1: Active, 2: Banned */
    "isVerified": true
  }
}
```

## Business rules
- Người gọi API cần có access token hợp lệ.
- User phải tồn tại trong hệ thống và chưa bị disable (`IsDisable == false`). Nếu không, trả `404 Not Found` (`USER_NOT_FOUND`).
- API trả về thông tin cơ bản của user (không bao gồm mật khẩu, refresh token, hay các thông tin nhạy cảm khác).
- API không kiểm tra role của user được truy vấn — có thể xem bất kỳ user nào (Admin, Staff, Student, Lecturer).

### Bảng vai trò RoleEnum (Integer)
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Admin | Quản trị viên hệ thống |
| `1` | Staff | Nhân viên vận hành sự kiện |
| `2` | Student | Sinh viên / Thí sinh tham gia thi đấu |
| `3` | Lecturer | Giảng viên hỗ trợ chuyên môn hoặc chấm thi |

### Bảng trạng thái UserStatusEnum (Integer)
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Inactive | Không hoạt động |
| `1` | Active | Đang hoạt động |
| `2` | Banned | Đã bị cấm |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- **Đã implement** trong `Hackathon.Api.Controllers.UserController`.
- Route hiện có: `GET /api/v1/users/{userId}`.
- Sử dụng policy `[Authorize]` (attribute trên method).
- Service: `Hackathon.Service.Users.Service.GetUserById()`.
- DTO response: `UserDetailResponse` (`Users Service.Response`) — gồm id, email, firstName, lastName, phoneNumber, avatarUrl, bio, address, dateOfBirth, studentId, college, imgUrl, linkUrl, role, status, isVerified.
- Entity: `Users` — query `AsNoTracking`, filter `!IsDisable`, throw `NotFoundException("USER_NOT_FOUND")` nếu không tìm thấy.
