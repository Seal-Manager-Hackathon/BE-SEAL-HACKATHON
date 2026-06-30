# Tìm kiếm sinh viên (Search Students)

## Tác dụng
Cho phép người dùng đã đăng nhập (leader) tìm kiếm sinh viên theo tên hoặc email để gửi lời mời vào team.

## URL
`GET /api/v1/users/students`

## Authorization
Yêu cầu access token hợp lệ.

## Path parameters
Không có.

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `search` | `string` | Không | Từ khóa tìm kiếm — tìm theo tên (FirstName, LastName) hoặc email. Nếu không truyền, trả tất cả sinh viên. |
| `pageIndex` | `int` | Không | Trang hiện tại. Mặc định `1`. |
| `pageSize` | `int` | Không | Số item mỗi trang. Mặc định `10`, tối đa `100`. |

## Ví dụ request
```http
GET /api/v1/users/students?search=nguyen&pageIndex=1&pageSize=20
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-30T10:00:00Z",
  "message": "SUCCESS",
  "data": {
    "items": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "firstName": "Nguyễn",
        "lastName": "Văn A",
        "email": "nguyenvana@example.com",
        "phoneNumber": "0123456789",
        "avatarUrl": "https://example.com/avatar.jpg",
        "studentId": "STU123456",
        "college": "FPT University",
        "status": 1 /* 0: Inactive, 1: Active, 2: Banned */
      }
    ],
    "pageIndex": 1,
    "pageSize": 20,
    "totalCount": 1,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

## Business rules
- Người gọi API cần có access token hợp lệ.
- API chỉ trả về user có role `Student` (`RoleEnum = 2`).
- Nếu có `search`, tìm kiếm không phân biệt hoa thường (`ToLower`) trên các field:
  - `FirstName` + khoảng trắng + `LastName` (tên đầy đủ).
  - `Email`.
- Nếu không truyền `search`, trả tất cả sinh viên (không filter theo keyword).
- Mặc định chỉ trả sinh viên **chưa bị disable** (`IsDisable == false`).
- Nếu không tìm thấy sinh viên phù hợp, trả `items: []`, `totalCount: 0`.
- Kết quả sắp xếp theo `CreatedAt` giảm dần (mới nhất lên trước).
- Phân trang: `pageIndex < 1` normalize về `1`; `pageSize` clamp trong `[1, 100]`.
- Mỗi item trả về các thông tin cơ bản: id, tên, email, phoneNumber, avatar, studentId, college, status — đủ để leader nhận diện và gửi lời mời.

### Bảng vai trò RoleEnum (Integer)
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `2` | Student | Sinh viên / Thí sinh tham gia thi đấu |

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
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- **Đã implement** trong `Hackathon.Api.Controllers.UserController`.
- Route hiện có: `GET /api/v1/users/students`.
- Sử dụng policy `[Authorize]` (attribute trên method).
- Service: `Hackathon.Service.Users.Service.SearchStudents()`.
- DTO request: `SearchStudentsRequest` (`Users Service.Request`) — gồm search, pageIndex, pageSize.
- DTO response: `BasePaginationResponse` với item `StudentSearchResponse` (`Users Service.Response`) — gồm id, firstName, lastName, email, phoneNumber, avatarUrl, studentId, college, status.
- Entity: `Users` — query với `Role == RoleEnum.Student && !IsDisable`, search filter `(FirstName + " " + LastName)` hoặc `Email` (lower contains), sort `CreatedAt` desc, pagination clamp.
