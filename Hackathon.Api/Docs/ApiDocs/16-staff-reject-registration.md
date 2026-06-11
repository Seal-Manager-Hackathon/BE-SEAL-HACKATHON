# Reject registration

## Tác dụng
Staff từ chối đơn đăng ký, lưu lý do, mở khóa team và gửi thông báo cho leader.

## URL
`PATCH /api/staff/register-teams/{registerTeamId}/reject`

## Request body
```json
{
  "reason": "string"
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
    "status": "Rejected",
    "rejectionReason": "string",
    "isBanned": false,
    "createdAt": "datetimeoffset",
    "updatedAt": "datetimeoffset",
    "message": "REGISTER_TEAM_REJECTED_SUCCESSFULLY"
  }
}
```

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 400 | BAD_REQUEST | REJECTION_REASON_REQUIRED |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 409 | CONFLICT | REGISTER_TEAM_NOT_PENDING |
| 404 | NOT_FOUND | TEAM_LEADER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
