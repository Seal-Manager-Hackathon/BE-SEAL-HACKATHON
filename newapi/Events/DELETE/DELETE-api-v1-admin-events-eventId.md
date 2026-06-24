# API 21: Xóa mềm sự kiện (Admin Delete Event)

## Tác dụng
Cho phép Admin xóa mềm (soft delete) một sự kiện bằng cách đặt cờ `IsDisable = true` để ẩn sự kiện khỏi giao diện thí sinh.

## URL
`DELETE /api/v1/admin/events/{eventId}`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản có quyền Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của event cần xóa.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "EVENT_DELETED_SUCCESSFULLY",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Event phải tồn tại trong DB, nếu không báo lỗi `EVENT_NOT_FOUND`.
- Thao tác này chỉ đổi cờ `IsDisable = true` và cập nhật thời gian sửa đổi `UpdatedAt`.
- Các dữ liệu thi đấu, phân bảng đấu của thí sinh đã ghi nhận được bảo toàn trong database, không bị cascade delete.
- API hỗ trợ tính lũy thoái (Idempotent): nếu gọi nhiều lần trên event đã disable, vẫn trả về kết quả thành công `EVENT_DELETED_SUCCESSFULLY`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy event cần xóa.",
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
| 403 | FORBIDDEN | Tài khoản không có vai trò quản trị viên. |
| 404 | EVENT_NOT_FOUND | Event không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh khi cập nhật cờ. |
