# Đánh dấu đọc toàn bộ thông báo (Mark All As Read)

## Tác dụng
Đổi toàn bộ thông báo chưa đọc của user hiện tại sang trạng thái đã đọc (`Read`).

## URL
`PATCH /api/v1/notifications/read-all`

## Quyền
Authenticated User (Yêu cầu đăng nhập)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

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
  "message": "ALL_NOTIFICATIONS_MARKED_AS_READ"
}
```

## Business rules
- Tìm kiếm toàn bộ các thông báo của user đang đăng nhập có trạng thái `Unread` hoặc `Pending`, bao gồm system notification được lưu riêng cho user đó.
- Thực hiện cập nhật hàng loạt trường `Status = Read` và cập nhật `UpdatedAt = DateTimeOffset.UtcNow` trong cùng một transaction.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
