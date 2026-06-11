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
      "topicId": "guid",
      "topicTitle": "string",
      "memberCount": 0,
      "status": "Pending",
      "createdAt": "datetimeoffset"
    }
  ]
}
```

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
