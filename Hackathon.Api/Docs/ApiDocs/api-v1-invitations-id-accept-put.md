# Accept Invitation

## Tác dụng
Chấp nhận lời mời tham gia team. Khi chấp nhận, học sinh sẽ chính thức được thêm vào bảng `TeamDetails` của team đó và thay đổi trạng thái lời mời thành `Accepted`.

## URL
`POST /api/v1/invitations/{invitationId:guid}/accept`

## Request Parameters
*   **Route Parameters:**
    *   `invitationId` (Guid, Bắt buộc): ID của lời mời cần chấp nhận.

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
    "status": 1, /* Accepted */
    "description": "string|null",
    "limitTime": "datetime|null",
    "createdAt": "datetime"
  }
}
```

## Business rules
- Yêu cầu xác thực tài khoản qua Access Token ở Header.
- Học sinh đang đăng nhập phải chính là người nhận lời mời (`UserId` khớp với lời mời).
- Profile của người được mời lúc này phải **đã hoàn thiện** (không null đối với các trường bắt buộc như email, password, firstName, lastName, phoneNumber, address, dateOfBirth, studentId, college). Nếu chưa hoàn thiện, trả lỗi `USER_PROFILE_NOT_COMPLETED`.
- Lời mời phải đang ở trạng thái chờ xử lý (`Status = Pending`).
- Lời mời phải chưa hết hạn (`LimitTime` phải lớn hơn thời gian hiện tại). Nếu hết hạn, trạng thái lời mời sẽ chuyển thành `Expired`, cập nhật `UpdatedAt` và trả lỗi `400 BadRequest` (`INVITATION_EXPIRED`).
- Team mời phải đang tồn tại, chưa bị vô hiệu hóa (`IsDisable = false`), và còn cho phép sửa đổi thành viên (`CanEdit = true`).
- Người dùng không được là thành viên hiện tại của team hoặc team đã đạt giới hạn tối đa 50 thành viên.
- Khi chấp nhận thành công:
  - Một bản ghi thành viên mới được thêm vào bảng `TeamDetails` với vai trò thành viên (`IsLeader = false`, `Status = Active`).
  - Trạng thái lời mời trong bảng `Invitations` chuyển sang `Accepted` và cập nhật trường `UpdatedAt` thành thời gian hiện tại.
  - Một bản ghi thông báo mới được thêm vào bảng `Notifications` gửi đến Leader của team để thông báo.
  - Quá trình này được bọc trong cùng một Database Transaction. Các thao tác đọc/kiểm tra trước đó không chạy trong transaction.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | INVITATION_EXPIRED (lời mời đã hết hạn) |
| 400 | BAD_REQUEST | USER_PROFILE_NOT_COMPLETED (profile người dùng chưa hoàn thành) |
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. (khi không truyền token) |
| 401 | INVALID_ACCESS_TOKEN | Invalid access token. (khi token sai định dạng) |
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT (người gọi không phải học sinh) |
| 403 | FORBIDDEN | INVITATION_NOT_FOR_CURRENT_USER (lời mời không thuộc về học sinh này) |
| 403 | FORBIDDEN | TEAM_MEMBER_LOCKED (team đã bị khóa thành viên) |
| 404 | NOT_FOUND | USER_NOT_FOUND (tài khoản không tồn tại hoặc bị khóa) |
| 404 | NOT_FOUND | INVITATION_NOT_FOUND (không tìm thấy lời mời) |
| 404 | NOT_FOUND | TEAM_NOT_FOUND (team không tồn tại) |
| 404 | NOT_FOUND | TEAM_LEADER_NOT_FOUND (không tìm thấy leader của team) |
| 409 | CONFLICT | INVITATION_ALREADY_RESPONDED (lời mời đã được chấp nhận/từ chối trước đó) |
| 409 | CONFLICT | USER_ALREADY_IN_TEAM (đã là thành viên của team) |
| 409 | CONFLICT | TEAM_MEMBER_LIMIT_EXCEEDED (team đã đạt giới hạn 50 thành viên) |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |


## Ghi chú Enum
Tham chiếu file [00-enum-values.md](00-enum-values.md) để biết chi tiết các giá trị số (int) trả về cho các trường Trạng thái (Status).
