# Đánh dấu đã đọc thông báo (Mark Notification As Read)

## Tác dụng
Đổi trạng thái thông báo thành đã đọc (`Read`).

## URL
`PATCH /api/v1/notifications/{notificationId}/read`

## Quyền
Authenticated User (Yêu cầu đăng nhập, là người sở hữu thông báo)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `notificationId` (Guid, Bắt buộc): ID của thông báo cần đánh dấu.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": null,
  "message": "NOTIFICATION_MARKED_AS_READ"
}
```

## Business rules
- Bản ghi thông báo phải tồn tại và chưa bị disable.
- Người gọi phải chính là người nhận thông báo đó (`UserId` khớp với token) hoặc thông báo là toàn hệ thống (`TargetType = System`).
- Cập nhật trường `Status = Read` (giá trị enum `2`) và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.

### Bảng trạng thái NotificationStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ gửi |
| `1` | Unread | Chưa đọc |
| `2` | Read | Đã đọc |

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | NOTIFICATION_NOT_FOR_CURRENT_USER |
| 404 | NOT_FOUND | NOTIFICATION_NOT_FOUND |
