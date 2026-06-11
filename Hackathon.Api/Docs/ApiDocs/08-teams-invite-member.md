# Invite team member

## Tác dụng
Leader gửi lời mời user khác vào team.

## URL
`POST /api/teams/{teamId}/invitations`

## Request body
```json
{
  "userId": "guid",
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
    "userId": "guid",
    "status": "Pending",
    "description": "string|null",
    "limitTime": "datetimeoffset|null",
    "message": "TEAM_INVITATION_SENT_SUCCESSFULLY"
  }
}
```

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 400 | BAD_REQUEST | INVITED_USER_ID_REQUIRED |
| 400 | BAD_REQUEST | CANNOT_INVITE_YOURSELF |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 403 | FORBIDDEN | TEAM_MEMBER_LOCKED |
| 403 | FORBIDDEN | ONLY_TEAM_LEADER_CAN_INVITE_MEMBER |
| 404 | NOT_FOUND | INVITED_USER_NOT_FOUND |
| 403 | FORBIDDEN | INVITED_USER_NOT_VERIFIED |
| 400 | BAD_REQUEST | INVITED_USER_PROFILE_NOT_COMPLETED |
| 409 | CONFLICT | TEAM_MEMBER_LIMIT_EXCEEDED |
| 409 | CONFLICT | USER_ALREADY_IN_TEAM |
| 409 | CONFLICT | INVITATION_ALREADY_PENDING |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
