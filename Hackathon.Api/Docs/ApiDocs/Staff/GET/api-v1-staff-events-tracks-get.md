# Staff get tracks by event

## Tác dụng
Staff lấy danh sách track của một event cụ thể (bao gồm cả track đã bị disable), phục vụ quản lý.

## URL
`GET /api/v1/staff/events/{eventId}/tracks`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event cần lấy danh sách track. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `keyword` | `string` | Không | Từ khóa tìm kiếm theo tên track. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable. |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định `1`). |
| `pageSize` | `int` | Không | Số item mỗi trang (mặc định `10`). |

## Ví dụ request
```http
GET /api/v1/staff/events/00000000-0000-0000-0000-000000000000/tracks?keyword=ai&pageIndex=1&pageSize=10
Authorization: Bearer {accessToken}
```

## Response body
Response dùng `ApiResponseFactory.BasePagination(...)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": null,
  "timestampUtc": "datetime",
  "value": {
    "items": [
      {
        "id": "guid",
        "eventId": "guid",
        "title": "string",
        "description": "string|null",
        "maxTeam": 0,
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
- Chỉ Staff/Admin mới có quyền truy cập.
- Event phải tồn tại, nếu không trả `EVENT_NOT_FOUND`.
- Staff phải được phân công vào event (`AssignEvents`) mới xem được, Admin thì không cần.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
