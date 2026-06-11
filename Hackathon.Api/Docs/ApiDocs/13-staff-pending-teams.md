# Get pending teams by event

## Tác dụng
Staff xem danh sách team đang chờ duyệt trong event được phân công.

## URL
`GET /api/staff/register-teams/events/{eventId}/pending`

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
      "registerTeamId": "guid",
      "teamId": "guid",
      "teamName": "string",
      "memberCount": 0,
      "status": "Pending",
      "createdAt": "datetimeoffset"
    }
  ]
}
```

## Business rules
- Request phải có access token hợp lệ.
- Staff chỉ xem được pending teams của event mà mình được phân công.
- Chỉ trả các đơn đăng ký có trạng thái `Pending`.
- Mỗi item gồm thông tin team, trạng thái đơn và số lượng member active.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
