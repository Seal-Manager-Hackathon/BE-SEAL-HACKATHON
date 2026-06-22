# API 28: Cập nhật thông tin nhóm (Student Update Team)

## Tác dụng
Leader của team có thể cập nhật tên của team, khi team đó chưa bị khóa (`CanEdit` đang là `true`).

## URL
`PUT /api/v1/teams/{teamId}`

## Quyền
Student Leader (Yêu cầu đăng nhập tài khoản Trưởng nhóm)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần cập nhật.

## Request Body
```json
{
  "teamName": "Đội thi cập nhật"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "message": "TEAM_UPDATED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Người gọi API phải là leader của team (`IsLeader = true`).
- `CanEdit` của team phải là `true`.
- Tên team không được trùng lặp với team khác.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Đội thi đã được duyệt tham gia giải, không thể chỉnh sửa tên.",
  "MessageCode": "TEAM_CANNOT_BE_EDITED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | TEAM_NAME_REQUIRED | Tên team không được rỗng. |
| 403 | ONLY_TEAM_LEADER_CAN_UPDATE_TEAM | Chỉ trưởng nhóm mới có quyền sửa tên team. |
| 403 | TEAM_CANNOT_BE_EDITED | Team đã bị khóa chức năng chỉnh sửa. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 409 | TEAM_NAME_ALREADY_EXISTS | Trùng tên team khác đang hoạt động. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
