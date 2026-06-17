# Get my joined events

## Tác dụng
Lấy danh sách sự kiện mà user hiện tại đã tham gia thông qua team đã đăng ký event (dựa trên `RegisterTeams`).

## URL
`GET /api/me/events/joined`

## Authorization
Yêu cầu access token hợp lệ.

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `year` | `int` | Không | Lọc event theo năm của `StartTime`. |
| `status` | `string` | Không | Lọc theo trạng thái event (EventStatusEnum): `Draft`, `Published`, `Closed`, `Cancelled`. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable của event. Nếu không truyền, mặc định chỉ trả event chưa bị disable (`IsDisable = false`). |

## Ví dụ request
```http
GET /api/me/events/joined?year=2026&status=Published&isDisable=false
Authorization: Bearer {accessToken}
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
      "createdAt": "datetimeoffset"
    }
  ]
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ trả các event mà user hiện tại có team đã đăng ký (`RegisterTeams`) và là member active (`TeamDetails` không disable).
- Đơn đăng ký bị soft-disable không được tính.
- Nếu không truyền `isDisable`, mặc định chỉ trả event chưa bị soft-disable.
- Nếu truyền `year`, lọc theo năm của `Event.StartTime`.
- Nếu truyền `status`, lọc theo trạng thái event (EventStatusEnum).
- Kết quả nên sắp xếp theo `StartTime` tăng dần, sau đó `CreatedAt` tăng dần.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 400 | BAD_REQUEST | BAD_REQUEST (status không hợp lệ) |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
