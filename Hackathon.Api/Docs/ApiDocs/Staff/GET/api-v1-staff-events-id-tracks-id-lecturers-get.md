# Staff lấy danh sách giảng viên đã phân công vào Track

## Tác dụng
Lấy danh sách các giảng viên (`Lecturer`) đã được phân công vào một `Track` cụ thể trong `Event`. Chỉ trả về các user có vai trò `Mentor` hoặc `Judge` được gán vào track đó.

## URL
`GET /api/v1/staff/events/{eventId}/tracks/{trackId}/lecturers`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `eventId` | `guid` | Có | Id của sự kiện. |
| `trackId` | `guid` | Có | Id của track. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable của AssignTrack. Nếu để trống (null), lấy tất cả. Nếu truyền true/false, lọc theo giá trị đó. |

## Request Headers
```
Authorization: Bearer <token>
```

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "message": "SUCCESS",
  "data": [
    {
      "id": "guid", /* ID của AssignTrack */
      "assignEventId": "guid", /* ID của AssignEvent */
      "userId": "guid", /* ID của Lecturer */
      "firstName": "string",
      "lastName": "string",
      "email": "string",
      "eventRole": 1, /* EventRoleEnum */
      "role": 3, /* RoleEnum */
      "createdAt": "datetimeoffset"
    }
  ]
}
```

### Bảng vai trò EventRoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |
| `2` | Staff | Nhân viên vận hành sự kiện (không được gán vào track) |

### Bảng vai trò hệ thống RoleEnum (SystemRole)
| Giá trị (Value) | Vai trò (Role) | Mô tả |
| :--- | :--- | :--- |
| `0` | Admin | Quản trị viên hệ thống |
| `1` | Staff | Nhân viên quản lý |
| `2` | Student | Sinh viên/Thí sinh |
| `3` | Lecturer | Giảng viên |

## Business rules
- Yêu cầu access token hợp lệ.
- Nếu là Staff: phải được phân công quản lý sự kiện này trước (`AssignEvents`), nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- Nếu là Admin: không cần kiểm tra phân công quản lý sự kiện.
- `eventId` và `trackId` phải tồn tại và không bị disable.
- Chỉ hiển thị các user được gán vào track có vai trò `Mentor` hoặc `Judge` (Staff không được phân vào track).
- Trả về cả `role` (role ngoài cùng của User) và `eventRole` (role trong event) riêng biệt.
- Lọc theo `isDisable` nếu được truyền vào, mặc định nếu không truyền thì lấy tất cả.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 404 | NOT_FOUND | NO_ONE_ASSIGNED_TO_TRACK |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
