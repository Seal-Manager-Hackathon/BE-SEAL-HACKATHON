# Mở khóa chỉnh sửa Team (BTC Unlock Team)

## Tác dụng
Cho phép Staff/Admin mở khóa cho một team cụ thể, cho phép trưởng nhóm cập nhật nhân sự/thông tin nhóm trong trường hợp đặc biệt.

## URL
`PATCH /api/v1/teams/{teamId}/unlock`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần mở khóa.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "message": "TEAM_UNLOCKED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Team phải tồn tại trong DB, nếu không báo lỗi `TEAM_NOT_FOUND`.
- Đặt trường `CanEdit = true` trong bảng `Teams` và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.
- Giúp mở khóa để leader team có quyền sửa đổi thành viên khi đơn đăng ký bị `Rejected` và cần gửi lại.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy thông tin nhóm cần mở khóa.",
  "MessageCode": "TEAM_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Quyền truy cập bị từ chối. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
