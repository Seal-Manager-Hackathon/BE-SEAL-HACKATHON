# Khôi phục sự kiện (Admin Restore Event)

## Tác dụng
Khôi phục sự kiện bị soft-delete (IsDisable = true) quay lại trạng thái hoạt động bình thường (IsDisable = false).

## URL
`PATCH /api/v1/admin/events/{eventId}/restore`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản có quyền Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của event cần khôi phục.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "EVENT_RESTORED_SUCCESSFULLY",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Event phải tồn tại trong DB, nếu không báo lỗi `EVENT_NOT_FOUND`.
- Chuyển cờ `IsDisable = false` và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.
- Giúp sự kiện hiển thị lại trên giao diện quản trị của Admin/Staff và giao diện chính của Thí sinh nếu đã được publish.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy sự kiện cần khôi phục.",
  "MessageCode": "EVENT_NOT_FOUND",
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
| 404 | EVENT_NOT_FOUND | Event không tồn tại trong hệ thống. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
