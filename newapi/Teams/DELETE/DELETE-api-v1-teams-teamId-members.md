# API 29: Trục xuất thành viên (Student Remove Members)

## Tác dụng
Leader của team có thể loại bỏ nhiều thành viên ra khỏi team. Trạng thái của các thành viên này trong `TeamDetails` sẽ chuyển thành disabled.

## URL
`DELETE /api/v1/teams/{teamId}/members`

## Quyền
Student Leader (Yêu cầu đăng nhập tài khoản Trưởng nhóm)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team.

## Request Body
```json
{
  "userIds": [
    "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  ]
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "message": "MEMBERS_REMOVED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Người gọi API phải là leader của team (`IsLeader = true`).
- Không được tự truyền `leaderId` của chính mình vào mảng xóa.
- `CanEdit` của team phải là `true`.
- Có sử dụng **Database Transaction**.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Bad Request",
  "Status": 400,
  "Detail": "Trưởng nhóm không được tự loại bản thân ra khỏi nhóm.",
  "MessageCode": "CANNOT_REMOVE_YOURSELF",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | USER_IDS_REQUIRED | Danh sách thành viên cần xóa bị bỏ trống. |
| 400 | CANNOT_REMOVE_YOURSELF | Leader tự xóa chính mình khỏi team. |
| 403 | ONLY_TEAM_LEADER_CAN_REMOVE_MEMBER | Chỉ trưởng nhóm mới có quyền xóa thành viên. |
| 403 | TEAM_MEMBER_LOCKED | Danh sách thành viên đã bị khóa do đã được BTC duyệt vào event. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 404 | NO_MATCHING_MEMBERS_FOUND | Không tìm thấy thành viên nào khớp trong nhóm để xóa. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
