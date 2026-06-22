# API 36: Từ chối lời mời (Reject Invitation)

## Tác dụng
Từ chối lời mời tham gia vào đội thi. Trạng thái lời mời sẽ cập nhật sang `Rejected`.

## URL
`POST /api/v1/invitations/{invitationId}/reject`

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
    "Status": 2, /* Rejected */
    "description": "Chào bạn, hãy tham gia nhóm của mình nhé!",
    "limitTime": "2026-06-24T08:00:00Z",
    "createdAt": "2026-06-22T08:00:00Z"
  }
}
```

## Business rules
- Sinh viên gọi API phải chính là người được mời (`UserId` khớp với bản ghi trong `Invitations`).
- Lời mời phải đang ở trạng thái `Pending` và chưa bị quá hạn.
- Khi từ chối thành công:
  - Cập nhật trạng thái `Status = Rejected` cho lời mời.
  - Tạo `Notifications` gửi tới Trưởng nhóm (Leader) để thông báo việc bị từ chối.
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
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Lời mời này đã được phản hồi từ trước.",
  "MessageCode": "INVITATION_ALREADY_RESPONDED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | INVITATION_EXPIRED | Lời mời đã hết hạn xác thực. |
| 403 | INVITATION_NOT_FOR_CURRENT_USER | Lời mời không gửi cho bạn. |
| 404 | INVITATION_NOT_FOUND | Lời mời không tồn tại. |
| 409 | INVITATION_ALREADY_RESPONDED | Lời mời đã được giải quyết trước đó. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
