# Staff/Admin get danh sách giảng viên khả dụng để phân công vào Event

## Tác dụng
Staff/Admin lấy danh sách `Lecturer` có thể được chọn để phân công vào một sự kiện cụ thể. API này dùng cho màn hình chọn giảng viên trước khi gọi `POST /api/v1/staff/events/{eventId}/assign-lecturers`.

## URL
`GET /api/v1/staff/events/{eventId}/lecturers/available`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id của sự kiện cần phân công giảng viên. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventRoleId` | `guid` | Có | Id role trong event muốn phân công (`Mentor` hoặc `Judge`). Dùng để loại các giảng viên bị conflict role. |
| `keyword` | `string` | Không | Tìm kiếm theo tên hoặc email giảng viên. |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định `1`). |
| `pageSize` | `int` | Không | Số item mỗi trang (mặc định `10`). |

## Ví dụ request
```http
GET /api/v1/staff/events/00000000-0000-0000-0000-000000000000/lecturers/available?eventRoleId=11111111-1111-1111-1111-111111111111&keyword=Nguyen&pageIndex=1&pageSize=10
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount)`.
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "items": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "firstName": "Nguyễn",
        "lastName": "Văn A",
        "fullName": "Nguyễn Văn A",
        "email": "nguyenvana@school.edu.vn",
        "phoneNumber": "0901234567",
        "avatarUrl": "https://example.com/avatar.png",
        "role": 3,
        "isAlreadyAssignedToEvent": false,
        "assignedEventRole": null
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 1,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

### Cấu trúc item
| Tên | Kiểu dữ liệu | Mô tả |
|---|---|---|
| `id` | `guid` | Id của user giảng viên. |
| `firstName` | `string` | Tên. |
| `lastName` | `string` | Họ/tên đệm. |
| `fullName` | `string` | Tên đầy đủ, ghép từ `FirstName` + `LastName`. |
| `email` | `string` | Email giảng viên. |
| `phoneNumber` | `string` | Số điện thoại. |
| `avatarUrl` | `string` | Ảnh đại diện. |
| `role` | `int` | Global role của user. Với lecturer là `3`. |
| `isAlreadyAssignedToEvent` | `bool` | Cho biết giảng viên đã được phân công trong event này chưa. |
| `assignedEventRole` | `int | null` | Role hiện tại trong event nếu đã được phân công. Xem bảng EventRoleEnum bên dưới. |

### Bảng vai trò EventRoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |
| `2` | Staff | Nhân viên vận hành sự kiện |

### Bảng vai trò hệ thống RoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả |
| :--- | :--- | :--- |
| `0` | Admin | Quản trị viên hệ thống |
| `1` | Staff | Nhân viên quản lý |
| `2` | Student | Sinh viên/thí sinh |
| `3` | Lecturer | Giảng viên |

## Business rules
- Người gọi phải là `Staff` hoặc `Admin`.
- Nếu người gọi là `Staff`, staff đó phải được phân công quản lý sự kiện này trong `AssignEvents`; nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- Nếu người gọi là `Admin`, không cần kiểm tra phân công quản lý sự kiện.
- `eventId` phải tồn tại và chưa bị disable.
- `eventRoleId` phải tồn tại trong bảng `EventRoles` và chỉ nên là role `Mentor` hoặc `Judge`.
- Chỉ trả về user có global role `Lecturer`.
- Không trả lecturer bị disable hoặc bị ban.
- Nếu `eventRoleId` là `Mentor`, loại các lecturer đã là `Judge` trong cùng event.
- Nếu `eventRoleId` là `Judge`, loại các lecturer đã là `Mentor` trong cùng event.
- Loại các lecturer đã được phân công đúng cùng `eventRoleId` trong event để tránh assign trùng.
- `keyword` tìm kiếm theo `FirstName`, `LastName`, `FullName` hoặc `Email`, không phân biệt hoa thường.
- Danh sách trả về phân trang theo `pageIndex`, `pageSize`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | EVENT_ROLE_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
