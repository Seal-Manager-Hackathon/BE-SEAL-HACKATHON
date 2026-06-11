# Register team for event

## Tác dụng
Leader gửi đơn đăng ký team tham gia event thông qua topic.

## URL
`POST /api/register-teams`

## Request body
```json
{
  "teamId": "guid",
  "topicId": "guid",
  "description": "string|null"
}
```

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
    "teamId": "guid",
    "teamName": "string|null",
    "topicId": "guid",
    "topicTitle": "string|null",
    "eventId": "guid",
    "eventName": "string|null",
    "description": "string|null",
    "status": "Pending",
    "rejectionReason": "string|null",
    "isBanned": false,
    "createdAt": "datetimeoffset",
    "updatedAt": "datetimeoffset",
    "message": "REGISTER_TEAM_SUBMITTED_SUCCESSFULLY"
  }
}
```

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 400 | BAD_REQUEST | TEAM_ID_REQUIRED |
| 400 | BAD_REQUEST | TOPIC_ID_REQUIRED |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 403 | FORBIDDEN | TEAM_MEMBER_LOCKED |
| 403 | FORBIDDEN | ONLY_TEAM_LEADER_CAN_REGISTER_TEAM |
| 404 | NOT_FOUND | TOPIC_NOT_FOUND |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 400 | BAD_REQUEST | EVENT_REGISTRATION_CLOSED |
| 400 | BAD_REQUEST | TEAM_MEMBER_COUNT_NOT_VALID |
| 400 | BAD_REQUEST | TEAM_MEMBER_PROFILE_NOT_COMPLETED |
| 409 | CONFLICT | MEMBER_ALREADY_REGISTERED_IN_EVENT |
| 409 | CONFLICT | TEAM_ALREADY_REGISTERED_IN_EVENT |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
