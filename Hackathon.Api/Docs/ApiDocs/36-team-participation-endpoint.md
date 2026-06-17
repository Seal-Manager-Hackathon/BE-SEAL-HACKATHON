# Get my participated teams

## Tác dụng
Lấy danh sách team mà user hiện tại đã từng tham gia hoặc đang tham gia.

## URL
`GET /api/v1/teams/me`

## Authorization
Yêu cầu access token hợp lệ.

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `status` | `TeamDetailStatusEnum` | Không | Lọc theo trạng thái membership của user trong team. Giá trị: `Active`, `Inactive`. |
| `year` | `int` | Không | Lọc theo năm của event mà team đã đăng ký/tham gia. |

## Ví dụ request
```http
GET /api/v1/teams/me?status=Active&year=2026
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
      "teamId": "guid",
      "teamName": "string",
      "canEdit": false,
      "isLeader": true,
      "memberStatus": "Active",
      "joinedAt": "datetimeoffset",
      "events": [
        {
          "eventId": "guid",
          "eventName": "string",
          "season": "string|null",
          "year": 2026,
          "registrationStatus": "Approved",
          "isBanned": false
        }
      ]
    }
  ]
}
```

## Business rules
- Request phải có access token hợp lệ.
- User hiện tại được xác định bằng claim `UserId` trong access token.
- Chỉ trả các team mà user hiện tại có record trong `TeamDetails`.
- Team và membership bị soft-disable không được trả về.
- Nếu truyền `status`, lọc theo `TeamDetails.Status` của user hiện tại trong team.
- Nếu truyền `year`, chỉ trả team có ít nhất một đơn đăng ký event thuộc năm đó.
- Năm ưu tiên lấy theo `Event.StartTime`; nếu event không có `StartTime`, dùng `Event.CreatedAt`.
- Nếu truyền `year`, danh sách `events` chỉ chứa event thuộc năm được filter.
- Nếu team chưa đăng ký event nào, team vẫn được trả về khi không filter `year`; khi có `year` thì không trả về team đó.
- `year` phải lớn hơn `0`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 400 | BAD_REQUEST | INVALID_YEAR |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
