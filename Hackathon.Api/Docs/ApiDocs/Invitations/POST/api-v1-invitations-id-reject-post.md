# Reject Invitation

## Tác dụng
Từ chối lời mời tham gia team. Khi từ chối, trạng thái lời mời sẽ được thay đổi sang `Rejected`.

## URL
`POST /api/v1/invitations/{invitationId:guid}/reject`

## Request Parameters
*   **Route Parameters:**
    *   `invitationId` (Guid, Bắt buộc): ID của lời mời cần từ chối.

## Request Headers
```
Authorization: Bearer <token>
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Message": "INVITATION_REJECTED",
  "Data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "teamId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
    "teamName": "Chiến binh công nghệ",
    "status": 2, /* 0: Pending, 1: Accepted, 2: Rejected, 3: Expired */
    "description": "Chào bạn, hãy tham gia team của mình nhé!",
    "limitTime": "2026-06-30T23:59:59Z",
    "createdAt": "2026-06-22T08:00:00Z"
  }
}
```

## Business rules
- Yêu cầu xác thực tài khoản qua Access Token ở Header.
- Học sinh đang đăng nhập phải chính là người nhận lời mời (`UserId` khớp với lời mời).
- Lời mời phải đang ở trạng thái chờ xử lý (`Status = Pending`).
- Lời mời phải chưa hết hạn (`LimitTime` phải lớn hơn thời gian hiện tại). Nếu hết hạn, trạng thái lời mời sẽ chuyển thành `Expired`, cập nhật `UpdatedAt` và trả lỗi `400 BadRequest` (`INVITATION_EXPIRED`).
- Khi từ chối thành công:
  - Trạng thái lời mời trong bảng `Invitations` chuyển sang `Rejected` và cập nhật trường `UpdatedAt` thành thời gian hiện tại.
  - Một bản ghi thông báo mới được thêm vào bảng `Notifications` gửi đến Leader của team để thông báo việc bị từ chối.
  - Quá trình này được bọc trong cùng một Database Transaction. Các thao tác đọc/kiểm tra trước đó không chạy trong transaction.

### Bảng trạng thái lời mời InvitationStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Lời mời đang chờ phản hồi |
| `1` | Accepted | Lời mời đã được chấp nhận |
| `2` | Rejected | Lời mời bị từ chối |
| `3` | Expired | Lời mời đã hết hạn phản hồi |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | INVITATION_EXPIRED |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT |
| 403 | FORBIDDEN | INVITATION_NOT_FOR_CURRENT_USER |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 404 | NOT_FOUND | INVITATION_NOT_FOUND |
| 404 | NOT_FOUND | TEAM_LEADER_NOT_FOUND |
| 409 | CONFLICT | INVITATION_ALREADY_RESPONDED |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
