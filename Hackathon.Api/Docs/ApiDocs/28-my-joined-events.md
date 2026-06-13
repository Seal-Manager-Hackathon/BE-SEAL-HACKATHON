# Get my joined events

## Tác dụng
Lấy danh sách sự kiện mà user hiện tại đã tham gia thông qua team đã đăng ký event.

## URL
`GET /api/me/events/joined`

## Authorization
Yêu cầu access token hợp lệ.

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `year` | `int` | Không | Lọc event theo năm của `StartTime`. |
| `status` | `string` | Không | Lọc theo trạng thái đơn đăng ký team trong event. Giá trị theo `RegisterTeamStatusEnum`, ví dụ `Pending`, `Approved`, `Rejected`. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable của event. Nếu không truyền, mặc định chỉ trả event chưa bị disable (`IsDisable = false`). |

## Ví dụ request
```http
GET /api/me/events/joined?year=2026&status=Approved&isDisable=false
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
      "eventId": "guid",
      "eventName": "string",
      "eventStatus": "string|null",
      "registrationId": "guid",
      "registrationStatus": "string|null",
      "teamId": "guid",
      "teamName": "string",
      "startTime": "datetimeoffset|null",
      "endTime": "datetimeoffset|null",
      "registerLimitTime": "datetimeoffset|null",
      "season": "string|null",
      "isDisable": false,
      "registeredAt": "datetimeoffset"
    }
  ]
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ trả các event mà user hiện tại là member của team có đơn đăng ký trong `RegisterTeams`.
- Team member phải đang active và không bị soft-disable trong `TeamDetails`.
- Đơn đăng ký bị soft-disable không được trả về.
- Nếu không truyền `isDisable`, mặc định chỉ trả event chưa bị soft-disable.
- Nếu truyền `year`, lọc theo năm của `Event.StartTime`.
- Nếu truyền `status`, lọc theo trạng thái đơn đăng ký team trong event.
- Kết quả nên sắp xếp theo `StartTime` giảm dần hoặc `registeredAt` giảm dần tùy nhu cầu hiển thị FE.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 400 | BAD_REQUEST | Query parameter không hợp lệ. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
