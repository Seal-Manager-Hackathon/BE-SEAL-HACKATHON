# Staff get assigned lecturers by event

## Tác dụng
Staff lấy danh sách các giảng viên (`Lecturer`) đã được phân công vào sự kiện với vai trò `Mentor` hoặc `Judge`. Hỗ trợ lọc theo vai trò, tìm kiếm tên và phân trang.

## URL
`GET /api/v1/staff/events/{eventId}/lecturers`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id của sự kiện. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventRoleId` | `guid` | Không | Lọc theo ID của Role (Mentor hoặc Judge). Nếu không truyền thì lấy tất cả. |
| `keyword` | `string` | Không | Từ khóa tìm kiếm theo tên hoặc email của giảng viên. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable của assignment. Mặc định `false`. |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định `1`). |
| `pageSize` | `int` | Không | Số item mỗi trang (mặc định `10`). |

## Ví dụ request
```http
GET /api/v1/staff/events/00000000-0000-0000-0000-000000000000/lecturers?keyword=nguyen&pageIndex=1&pageSize=10
Authorization: Bearer {accessToken}
```

## Response body
Response dùng `ApiResponseFactory.BasePagination(...)`.
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "items": [
      {
        "id": "guid", /* ID của AssignEvent */
        "userId": "guid", /* ID của Lecturer */
        "fullName": "string",
        "email": "string",
        "eventRoleId": "guid",
        "eventRoleName": "string", /* Mentor hoặc Judge */
        "isDisable": false,
        "createdAt": "datetimeoffset"
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 0,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

## Business rules
- Người gọi phải là `Staff` hoặc `Admin`.
- Nếu là `Staff`, phải được phân công quản lý sự kiện này trước (`AssignEvents`).
- `eventId` phải tồn tại và không bị disable.
- Nếu truyền `eventRoleId`, lọc các assignment có role tương ứng.
- Kết quả join với bảng `Users` để lấy thông tin của Lecturer và bảng `EventRoles` để lấy tên Role.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |