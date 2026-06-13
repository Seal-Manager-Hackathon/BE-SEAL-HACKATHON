# Get events with most participants

## Tác dụng
Lấy danh sách event có nhiều người tham gia nhất, không quan tâm thời gian diễn ra.

## URL
`GET /api/events/most-participants`

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `limit` | `int` | Không | Số lượng event cần lấy, mặc định `10`. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable của event. Nếu không truyền, mặc định chỉ trả event chưa bị disable (`IsDisable = false`). |

## Ví dụ request
```http
GET /api/events/most-participants?limit=10&isDisable=false
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
      "eventId": "guid",
      "eventName": "string",
      "description": "string|null",
      "status": "string|null",
      "season": "string|null",
      "startTime": "datetimeoffset|null",
      "endTime": "datetimeoffset|null",
      "participantCount": 0,
      "teamCount": 0,
      "isDisable": false
    }
  ]
}
```

## Business rules
- Không lọc theo thời gian diễn ra event.
- Số người tham gia được tính từ member của các team đã đăng ký event.
- Chỉ tính `RegisterTeams` chưa bị soft-disable.
- Nên chỉ tính đơn đăng ký có trạng thái `Approved`, trừ khi nghiệp vụ yêu cầu tính cả `Pending`.
- `participantCount` tính số member active, chưa disable trong `TeamDetails`.
- `teamCount` tính số team hợp lệ tham gia event.
- Kết quả sắp xếp theo `participantCount` giảm dần, sau đó `teamCount` giảm dần.
- Nếu không truyền `isDisable`, mặc định chỉ trả event chưa bị soft-disable.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Query parameter không hợp lệ. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
