# API 27: Mời thành viên vào nhóm (Invite Member)

## Tác dụng
Gửi lời mời tham gia team cho một học sinh khác bằng địa chỉ email của học sinh đó.

## URL
`POST /api/v1/teams/{teamId}/invitations`

## Quyền
Student Leader (Yêu cầu đăng nhập tài khoản Trưởng nhóm)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team gửi lời mời.

## Request Body
```json
{
  "email": "member@college.edu.vn",
  "description": "Tham gia đội mình nhé!"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "message": "INVITATION_SENT_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Người thực hiện gửi lời mời (gọi API) phải có vai trò `Student` và phải là Leader đang hoạt động của team (`IsLeader = true` và `Status = Active` trong `TeamDetails`).
- Team mời phải đang cho phép chỉnh sửa thành viên (`CanEdit = true`, check BR-TEAM-03).
- Email được nhập phải thuộc về tài khoản học sinh (`Role = Student`), chưa bị vô hiệu hóa (`IsDisable = false`), đã xác thực email (`IsVerified = true`).
- Người gửi lời mời không được tự mời chính mình.
- Tổng số thành viên trong team không được vượt quá 50 người (giới hạn hệ thống).
- Người được mời không phải thành viên hiện tại của team hoặc đã có lời mời ở trạng thái `Pending` đối với team này.
- Khi gửi lời mời thành công:
  - Một bản ghi lời mời mới được thêm vào bảng `Invitations` ở trạng thái `Pending`.
  - Một bản ghi thông báo mới được thêm vào bảng `Notifications` gửi đến học sinh được mời.

### Bảng trạng thái InvitationStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ xử lý / Chờ phản hồi |
| `1` | Accepted | Đã chấp nhận gia nhập nhóm |
| `2` | Rejected | Đã từ chối lời mời |
| `3` | Expired | Lời mời đã hết hạn |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Sinh viên này đã là thành viên của nhóm.",
  "MessageCode": "USER_ALREADY_IN_TEAM",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | CANNOT_INVITE_YOURSELF | Không được phép tự gửi lời mời cho chính mình. |
| 403 | ONLY_TEAM_LEADER_CAN_INVITE_MEMBER | Chỉ trưởng nhóm mới có quyền mời thành viên. |
| 403 | TEAM_MEMBER_LOCKED | Team đã được duyệt vào event, danh sách thành viên bị khóa. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 404 | INVITED_USER_NOT_FOUND | Không tìm thấy người dùng sở hữu email này. |
| 409 | INVITATION_ALREADY_PENDING | Đã có một lời mời đang chờ xử lý với user này. |
| 409 | USER_ALREADY_IN_TEAM | Người được mời đã là thành viên nhóm. |
| 409 | TEAM_MEMBER_LIMIT_EXCEEDED | Số lượng thành viên vượt quá 50 người. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
