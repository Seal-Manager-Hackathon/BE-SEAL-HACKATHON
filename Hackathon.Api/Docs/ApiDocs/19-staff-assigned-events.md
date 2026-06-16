# Get assigned events

## Tác dụng
Staff xem danh sách event được phân công.

## URL
`GET /api/staff/register-teams/events`

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
      "eventRole": "string",
      "registerLimitTime": "datetimeoffset|null"
    }
  ]
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ trả các event mà user hiện tại được phân công trong `AssignEvents`.
- Event bị disable không được trả về danh sách.
- Response kèm role của staff trong từng event và thời hạn đăng ký nếu có.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
