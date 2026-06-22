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
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "ALL_NOTIFICATIONS_MARKED_AS_READ",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Tìm kiếm toàn bộ các thông báo của user đang đăng nhập có trạng thái `Unread` hoặc `Pending`.
- Thực hiện cập nhật hàng loạt trường `Status = Read` và cập nhật `UpdatedAt = DateTimeOffset.UtcNow` trong cùng một transaction.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Unauthorized",
  "Status": 401,
  "Detail": "Vui lòng xác thực tài khoản.",
  "MessageCode": "UNAUTHORIZED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh khi cập nhật. |
