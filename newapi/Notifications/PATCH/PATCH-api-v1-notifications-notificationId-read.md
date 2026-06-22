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
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "NOTIFICATION_MARKED_AS_READ",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Bản ghi thông báo phải tồn tại và chưa bị disable.
- Người gọi phải chính là người nhận thông báo đó (`UserId` khớp với token).
- Cập nhật trường `Status = Read` (giá trị enum `2`) và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.

### Bảng trạng thái NotificationStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ gửi |
| `1` | Unread | Chưa đọc |
| `2` | Read | Đã đọc |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy thông báo.",
  "MessageCode": "NOTIFICATION_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Bạn không sở hữu thông báo này. |
| 404 | NOT_FOUND | Thông báo không tồn tại trong hệ thống. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
