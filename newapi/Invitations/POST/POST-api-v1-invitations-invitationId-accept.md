# API 35: Chấp nhận lời mời (Accept Invitation)

## Tác dụng
Đồng ý gia nhập đội thi. Người dùng sẽ chính thức được thêm vào danh sách thành viên của team.

## URL
`POST /api/v1/invitations/{invitationId}/accept`

## Quyền
Student (Yêu cầu đăng nhập, là người nhận lời mời)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `invitationId` (Guid, Bắt buộc): ID của lời mời gia nhập nhóm.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Value": {
    "id": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "teamName": "Chiến binh công nghệ",
    "Status": 1, /* Accepted */
    "description": "Chào bạn, hãy tham gia nhóm của mình nhé!",
    "limitTime": "2026-06-24T08:00:00Z",
    "createdAt": "2026-06-22T08:00:00Z"
  }
}
```

## Business rules
- Sinh viên gọi API phải chính là người được mời (`UserId` khớp với bản ghi trong `Invitations`).
- Hồ sơ (Profile) của sinh viên được mời tại thời điểm đồng ý phải **đầy đủ thông tin bắt buộc** (check BR-ACC-03, nếu không báo lỗi `USER_PROFILE_NOT_COMPLETED`).
- Lời mời phải đang ở trạng thái `Pending` và chưa bị quá hạn (`LimitTime > DateTimeOffset.UtcNow`). Nếu đã quá hạn, trạng thái lời mời tự động cập nhật sang `Expired` và trả lỗi `INVITATION_EXPIRED`.
- Team gửi lời mời phải tồn tại, đang hoạt động (`IsDisable = false`), và còn cho phép chỉnh sửa thành viên (`CanEdit = true`).
- Tổng thành viên nhóm thi đấu không được vượt quá 50 người.
- Khi chấp nhận thành công:
  - Thêm thành viên vào bảng `TeamDetails` với vai trò Member (`IsLeader = false`, `Status = Active`).
  - Cập nhật trạng thái `Status = Accepted` cho lời mời.
  - Tạo `Notifications` gửi tới Trưởng nhóm (Leader) để thông báo.
  - Toàn bộ quá trình phải thực thi trong cùng một **Database Transaction**.

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
  "Title": "Bad Request",
  "Status": 400,
  "Detail": "Lời mời này đã quá hạn xác nhận.",
  "MessageCode": "INVITATION_EXPIRED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | INVITATION_EXPIRED | Lời mời đã hết hạn xác thực (chuyển sang Expired). |
| 400 | USER_PROFILE_NOT_COMPLETED | Profile sinh viên chưa điền đủ các trường bắt buộc. |
| 403 | INVITATION_NOT_FOR_CURRENT_USER | Lời mời gửi tới tài khoản khác, không phải bạn. |
| 403 | TEAM_MEMBER_LOCKED | BTC đã duyệt team tham gia event, danh sách thành viên bị khóa. |
| 404 | INVITATION_NOT_FOUND | Lời mời không tồn tại hoặc đã bị disable. |
| 409 | INVITATION_ALREADY_RESPONDED | Lời mời đã được chấp nhận hoặc từ chối trước đó. |
| 409 | TEAM_MEMBER_LIMIT_EXCEEDED | Đội thi đã đủ 50 thành viên. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
