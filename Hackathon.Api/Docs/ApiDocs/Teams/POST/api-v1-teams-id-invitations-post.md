# Invite member

## Tác dụng
Gửi lời mời tham gia team cho một học sinh khác bằng email của học sinh đó.

## URL
`POST /api/v1/teams/{teamId}/invitations`

## Authorization
Yêu cầu access token hợp lệ với role `Student`.

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team gửi lời mời.

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
  "status": 200,
  "traceId": "string",
  "timestampUtc": "datetime",
  "data": null,
  "message": "INVITATION_SENT_SUCCESSFULLY"
}
```

## Business rules
- Yêu cầu xác thực tài khoản qua Access Token ở Header.
- Người thực hiện gửi lời mời (gọi API) phải có vai trò `Student`.
- Người thực hiện gửi lời mời phải là Leader của team (`IsLeader = true` và `Status = Active` trong `TeamDetails`).
- Team mời phải đang cho phép chỉnh sửa thành viên (`CanEdit = true`).
- Email được nhập phải thuộc về tài khoản học sinh (`Role = Student`), chưa bị vô hiệu hóa (`IsDisable = false`), đã xác thực email (`IsVerified = true`). KHÔNG cần thiết người được mời phải có profile hoàn chỉnh ngay lúc này.
- Người được mời không thể tự mời chính mình.
- Team được mời không được vượt quá giới hạn 50 thành viên hiện tại.
- Học sinh được mời không được là thành viên hiện tại của team hoặc đã có lời mời ở trạng thái `Pending` đối với team này.
- Khi gửi lời mời thành công:
  - Một bản ghi lời mời mới được thêm vào bảng `Invitations` ở trạng thái `Pending`.
  - Một bản ghi thông báo mới được thêm vào bảng `Notifications` gửi đến học sinh được mời.

## Lỗi có thể xảy ra
*Khi gặp lỗi Validation hoặc nghiệp vụ, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Bad Request",
  "status": 400,
  "message": "EMAIL_REQUIRED",
  "messageCode": "BAD_REQUEST",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | EMAIL_REQUIRED |
| 400 | BAD_REQUEST | CANNOT_INVITE_YOURSELF |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT |
| 403 | FORBIDDEN | TEAM_MEMBER_LOCKED |
| 403 | FORBIDDEN | TEAM_LOCKED_DUE_TO_REGISTRATION_STATUS |
| 403 | FORBIDDEN | ONLY_TEAM_LEADER_CAN_INVITE_MEMBER |
| 403 | FORBIDDEN | INVITED_USER_MUST_BE_STUDENT |
| 403 | FORBIDDEN | INVITED_USER_NOT_VERIFIED |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 404 | NOT_FOUND | INVITED_USER_NOT_FOUND |
| 409 | CONFLICT | TEAM_MEMBER_LIMIT_EXCEEDED |
| 409 | CONFLICT | USER_ALREADY_IN_TEAM |
| 409 | CONFLICT | INVITATION_ALREADY_PENDING |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
