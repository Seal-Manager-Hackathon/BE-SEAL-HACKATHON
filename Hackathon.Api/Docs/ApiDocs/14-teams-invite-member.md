# Invite team member

## Tác dụng
Leader gửi lời mời student khác vào team.

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

## Business rules
- Request phải có access token hợp lệ.
- Chỉ leader đang active của team mới được gửi lời mời.
- Team phải tồn tại, chưa bị disable và còn được chỉnh sửa member (`canEdit = true`).
- Chỉ được mời user có role `Student`.
- Không được tự mời chính mình.
- User được mời phải tồn tại, chưa bị disable, đã verify email và profile đã đủ thông tin bắt buộc.
- Team chỉ được có tối đa 50 member trước khi đăng ký event.
- Không được mời user đã là member của team.
- Không được tạo thêm lời mời nếu user đó đã có invitation `Pending` trong cùng team.
- Tạo invitation thành công với trạng thái `Pending` và gửi thông báo cho user được mời.

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
| 403 | FORBIDDEN | INVITED_USER_MUST_BE_STUDENT |
| 403 | FORBIDDEN | INVITED_USER_NOT_VERIFIED |
| 400 | BAD_REQUEST | INVITED_USER_PROFILE_NOT_COMPLETED |
| 409 | CONFLICT | TEAM_MEMBER_LIMIT_EXCEEDED |
| 409 | CONFLICT | USER_ALREADY_IN_TEAM |
| 409 | CONFLICT | INVITATION_ALREADY_PENDING |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
