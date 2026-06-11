# Respond invitation

## Tác dụng
Student được mời chấp nhận hoặc từ chối lời mời vào team.

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

## Business rules
- Request phải có access token hợp lệ.
- Chỉ user là `Student` mới được phản hồi lời mời team.
- Chỉ user được ghi trong invitation mới được phản hồi invitation đó.
- Invitation phải tồn tại, chưa bị disable, còn trạng thái `Pending` và chưa hết hạn.
- Team của invitation phải tồn tại, chưa bị disable và còn được chỉnh sửa member (`canEdit = true`).
- Nếu từ chối, invitation chuyển sang `Rejected`, không thêm user vào team.
- Nếu chấp nhận, hệ thống kiểm tra lại giới hạn member và việc user đã thuộc team hay chưa.
- Nếu chấp nhận thành công, invitation chuyển sang `Accepted` và user được thêm vào team với trạng thái `Active`, không phải leader.
- Sau khi phản hồi, hệ thống gửi thông báo cho team leader.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT |
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
