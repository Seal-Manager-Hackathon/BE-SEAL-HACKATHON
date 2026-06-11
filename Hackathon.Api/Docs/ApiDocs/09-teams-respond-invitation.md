# Respond invitation

## Tác dụng
User được mời chấp nhận hoặc từ chối lời mời vào team.

## URL
`POST /api/teams/invitations/{invitationId}/response`

## Request body
```json
{
  "isAccepted": true
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
    "userId": "guid",
    "status": "Accepted | Rejected",
    "description": "string|null",
    "limitTime": "datetimeoffset|null",
    "message": "TEAM_INVITATION_ACCEPTED_SUCCESSFULLY | TEAM_INVITATION_REJECTED_SUCCESSFULLY"
  }
}
```

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | INVITATION_NOT_FOUND |
| 403 | FORBIDDEN | INVITATION_NOT_FOR_CURRENT_USER |
| 409 | CONFLICT | INVITATION_ALREADY_RESPONDED |
| 400 | BAD_REQUEST | INVITATION_EXPIRED |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 403 | FORBIDDEN | TEAM_MEMBER_LOCKED |
| 404 | NOT_FOUND | TEAM_LEADER_NOT_FOUND |
| 409 | CONFLICT | TEAM_MEMBER_LIMIT_EXCEEDED |
| 409 | CONFLICT | USER_ALREADY_IN_TEAM |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
