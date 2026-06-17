# Get event details

## Tác dụng
Lấy thông tin chi tiết của một event theo `eventId`.

## URL
`GET /api/v1/events/{eventId}`

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event cần xem chi tiết. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `isDisable` | `bool` | Không | Cho phép lấy event theo trạng thái soft-disable. Nếu không truyền, mặc định chỉ lấy event chưa bị disable (`IsDisable = false`). |

## Ví dụ request
```http
GET /api/v1/events/00000000-0000-0000-0000-000000000000?isDisable=false
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
  "timestampUtc": "datetime",
  "value": {
    "id": "guid",
    "name": "string",
    "description": "string|null",
    "startTime": "datetimeoffset|null",
    "endTime": "datetimeoffset|null",
    "registerLimitTime": "datetimeoffset|null",
    "limitTeam": 0,
    "minMember": 0,
    "maxMember": 0,
    "status": "string|null",
    "numberRound": 0,
    "season": "string|null",
    "isDisable": false,
    "createdAt": "datetimeoffset"
  }
}
```

## Business rules
- API trả về chi tiết event theo `eventId`.
- Nếu không truyền `isDisable`, chỉ tìm event chưa bị soft-disable.
- Nếu truyền `isDisable=false`, chỉ tìm event chưa bị soft-disable.
- Nếu truyền `isDisable=true`, chỉ tìm event đã bị soft-disable.
- Nếu không tìm thấy event phù hợp điều kiện, trả lỗi `EVENT_NOT_FOUND`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | `eventId` hoặc query parameter không hợp lệ. |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
