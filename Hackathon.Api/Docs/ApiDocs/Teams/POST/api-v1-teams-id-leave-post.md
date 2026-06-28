# Student leave team

## Tác dụng
Cho phép thành viên hiện tại của team tự rời khỏi nhóm thi đấu.

## URL
`POST /api/v1/teams/{teamId}/leave`

## Authorization
Yêu cầu access token hợp lệ với role `Student`.

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần rời.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "TEAM_LEFT_SUCCESSFULLY",
  "data": null
}
```

## Business rules
- Người gọi API phải đang là thành viên hoạt động trong team (`Status = Active` trong `TeamDetails`).
- Trưởng nhóm (`IsLeader = true`) KHÔNG được phép tự rời nhóm bằng API này. Nếu muốn rời nhóm, Leader phải nhường/chuyển quyền leader cho thành viên khác trước (API 30).
- Team phải đang mở cho phép sửa đổi thành viên (`CanEdit = true`).
- Cập nhật `IsDisable = true` cho bản ghi thành viên trong `TeamDetails` (xóa mềm), giống pattern `RemoveMembers`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Forbidden",
  "status": 403,
  "message": "LEADER_CANNOT_LEAVE_TEAM",
  "messageCode": "FORBIDDEN",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | LEADER_CANNOT_LEAVE_TEAM |
| 403 | FORBIDDEN | TEAM_MEMBER_LOCKED |
| 403 | FORBIDDEN | TEAM_LOCKED_DUE_TO_REGISTRATION_STATUS |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 404 | NOT_FOUND | NOT_A_TEAM_MEMBER |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
