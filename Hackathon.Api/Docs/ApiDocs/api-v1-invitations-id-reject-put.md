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
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "value": {
    "id": "guid",
    "teamId": "guid",
    "teamName": "string",
    "status": 2, /* Rejected */
    "description": "string|null",
    "limitTime": "datetime|null",
    "createdAt": "datetime"
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

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | INVITATION_EXPIRED (lời mời đã hết hạn) |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | INVALID_ACCESS_TOKEN | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT (người gọi không phải học sinh) |
| 403 | FORBIDDEN | INVITATION_NOT_FOR_CURRENT_USER (lời mời không thuộc về học sinh này) |
| 404 | NOT_FOUND | USER_NOT_FOUND (tài khoản không tồn tại hoặc bị khóa) |
| 404 | NOT_FOUND | INVITATION_NOT_FOUND |
| 404 | NOT_FOUND | TEAM_LEADER_NOT_FOUND |
| 409 | CONFLICT | INVITATION_ALREADY_RESPONDED (lời mời đã được chấp nhận/từ chối trước đó) |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
