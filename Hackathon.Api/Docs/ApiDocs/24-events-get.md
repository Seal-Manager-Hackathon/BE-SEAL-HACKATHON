# Get events

## Tác dụng
Lấy danh sách event, có thể lọc theo năm và trạng thái soft-disable theo query truyền lên.

## URL
`GET /api/events`

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `year` | `int` | Không | Lọc event theo năm. API lọc theo năm của `StartTime`. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable. Nếu không truyền, mặc định chỉ trả event chưa bị disable (`IsDisable = false`). |

## Ví dụ request
```http
GET /api/events?year=2026&isDisable=false
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
  "value": [
    {
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
      "createdAt": "datetimeoffset",
      "updatedAt": "datetimeoffset"
    }
  ]
}
```

## Business rules
- API trả về danh sách event theo điều kiện query.
- Nếu `year` được truyền, chỉ trả các event có `StartTime` thuộc năm đó.
- Nếu `isDisable` được truyền:
  - `isDisable=false`: chỉ trả event chưa bị soft-disable.
  - `isDisable=true`: chỉ trả event đã bị soft-disable.
- Nếu không truyền `isDisable`, mặc định chỉ trả event chưa bị soft-disable.
- Kết quả nên sắp xếp theo `StartTime` tăng dần, sau đó `CreatedAt` tăng dần.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Query parameter không hợp lệ. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
