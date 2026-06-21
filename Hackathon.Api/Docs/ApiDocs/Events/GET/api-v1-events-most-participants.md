# Get events with most participants

## Tác dụng
Lấy danh sách event có nhiều người tham gia nhất, không quan tâm thời gian diễn ra.

## URL
`GET /api/v1/events/most-participants`

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `limit` | `int` | Không | Số lượng event cần lấy, mặc định `10`. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable của event. Nếu không truyền, mặc định chỉ trả event chưa bị disable (`IsDisable = false`). |

## Ví dụ request
```http
GET /api/v1/events/most-participants?limit=10&isDisable=false
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
      "status": 0, /* Draft */
      "numberRound": 0,
      "season": "string|null",
      "isDisable": false,
      "createdAt": "datetimeoffset",
      "teamCount": 0,
      "participantCount": 0
    }
  ]
}
```

## Business rules
- API không yêu cầu đăng nhập.
- Không lọc theo thời gian diễn ra event.
- Số người tham gia được tính từ member của các team đã đăng ký event.
- Chỉ tính `RegisterTeams` chưa bị soft-disable và có trạng thái `Approved`.
- `participantCount` tính số member active, chưa disable trong `TeamDetails`.
- `teamCount` tính số team hợp lệ tham gia event.
- Kết quả sắp xếp theo `participantCount` giảm dần, sau đó `teamCount` giảm dần.
- Nếu không truyền `isDisable`, mặc định chỉ trả event chưa bị soft-disable.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | QUERY_PARAMETER_INVALID |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
