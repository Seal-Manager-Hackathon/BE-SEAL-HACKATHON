# API 30: Chuyển quyền Trưởng nhóm (Transfer Leader)

## Tác dụng
Leader hiện tại của team có thể chuyển quyền leader cho một thành viên khác đang ở trong team. Sau đó, bản thân sẽ trở thành member bình thường.

## URL
`PUT /api/v1/teams/{teamId}/leader`

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
  "newLeaderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "message": "LEADER_TRANSFERRED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Người gọi API phải là leader của team (`IsLeader = true`).
- `newLeaderId` phải là một thành viên đang hoạt động trong team.
- Không thể tự truyền bản thân làm `newLeaderId` được nữa.
- `CanEdit` của team phải là `true`.
- Có sử dụng **Database Transaction**.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Bad Request",
  "Status": 400,
  "Detail": "Bạn đang là trưởng nhóm rồi.",
  "MessageCode": "ALREADY_THE_LEADER",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | NEW_LEADER_ID_REQUIRED | Thiếu ID của trưởng nhóm mới. |
| 400 | ALREADY_THE_LEADER | Tự chuyển quyền cho chính mình. |
| 403 | ONLY_TEAM_LEADER_CAN_TRANSFER_ROLE | Chỉ trưởng nhóm hiện tại mới được nhường quyền. |
| 403 | TEAM_MEMBER_LOCKED | Team đã bị khóa chức năng chỉnh sửa. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 404 | NEW_LEADER_NOT_IN_TEAM | Không tìm thấy thành viên chỉ định hoạt động trong nhóm này. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
