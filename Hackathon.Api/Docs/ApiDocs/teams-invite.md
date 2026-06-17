# Invite Member

## Tác dụng
Gửi lời mời tham gia team cho một học sinh khác bằng email của học sinh đó.

## URL
`POST /api/v1/teams/{teamId:guid}/invitations`

## Request body
```json
{
  "email": "string",
  "description": "string|null"
}
```

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "message": "INVITATION_SENT_SUCCESSFULLY"
  }
}
```

## Business rules
- Yêu cầu xác thực tài khoản qua Access Token ở Header.
- Người thực hiện gửi lời mời (gọi API) phải có vai trò `Student`.
- Người thực hiện gửi lời mời phải là Leader của team (`IsLeader = true` và `Status = Active` trong `TeamDetails`).
- Team mời phải đang cho phép chỉnh sửa thành viên (`CanEdit = true`).
- Email được nhập phải thuộc về tài khoản học sinh (`Role = Student`), chưa bị vô hiệu hóa (`IsDisable = false`), đã xác thực email (`IsVerified = true`), và đã hoàn thiện hồ sơ.
- Người được mời không thể tự mời chính mình.
- Team được mời không được vượt quá giới hạn 50 thành viên hiện tại.
- Học sinh được mời không được là thành viên hiện tại của team hoặc đã có lời mời ở trạng thái `Pending` đối với team này.
- Khi gửi lời mời thành công:
  - Một bản ghi lời mời mới được thêm vào bảng `Invitations` ở trạng thái `Pending`.
  - Một bản ghi thông báo mới được thêm vào bảng `Notifications` gửi đến học sinh được mời.

## Lỗi có thể xảy ra
*Khi gặp lỗi Validation hoặc nghiệp vụ, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

```json
{
  "title": "string",
  "status": "integer",
  "detail": "string",
  "messageCode": "string",
  "errors": "object|null",
  "traceId": "string|null",
  "timestampUtc": "datetime"
}
```

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | VALIDATION_FAILED | EMAIL_REQUIRED (khi `email` trống hoặc null) |
| 400 | VALIDATION_FAILED | INVALID_EMAIL_FORMAT (khi `email` sai định dạng) |
| 400 | BAD_REQUEST | CANNOT_INVITE_YOURSELF (khi tự mời chính mình) |
| 400 | BAD_REQUEST | INVITED_USER_PROFILE_NOT_COMPLETED (người được mời chưa đầy đủ hồ sơ) |
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. (khi không truyền token) |
| 401 | INVALID_ACCESS_TOKEN | Invalid access token. (khi token sai định dạng) |
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT (người gọi không phải học sinh) |
| 403 | FORBIDDEN | TEAM_MEMBER_LOCKED (team đã khóa chỉnh sửa) |
| 403 | FORBIDDEN | ONLY_TEAM_LEADER_CAN_INVITE_MEMBER (người gọi không phải leader) |
| 403 | FORBIDDEN | INVITED_USER_MUST_BE_STUDENT (người được mời không phải học sinh) |
| 403 | FORBIDDEN | INVITED_USER_NOT_VERIFIED (người được mời chưa xác thực email) |
| 404 | NOT_FOUND | USER_NOT_FOUND (tài khoản người gọi không tồn tại hoặc bị khóa) |
| 404 | NOT_FOUND | TEAM_NOT_FOUND (team không tồn tại) |
| 404 | NOT_FOUND | INVITED_USER_NOT_FOUND (không tìm thấy người được mời) |
| 409 | CONFLICT | TEAM_MEMBER_LIMIT_EXCEEDED (team đã đủ 50 thành viên) |
| 409 | CONFLICT | USER_ALREADY_IN_TEAM (người được mời đã trong team) |
| 409 | CONFLICT | INVITATION_ALREADY_PENDING (đã có lời mời đang chờ xử lý) |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
