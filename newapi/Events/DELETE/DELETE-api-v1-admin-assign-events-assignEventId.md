# Gỡ phân công Giảng viên khỏi Event (Remove Lecturer From Event)

## Tác dụng
Cho phép Admin thu hồi toàn bộ phân công của giảng viên khỏi một sự kiện cụ thể.

## URL
`DELETE /api/v1/admin/assign-events/{assignEventId}`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `assignEventId` (Guid, Bắt buộc): ID của bản ghi phân công.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "LECTURER_REMOVED_FROM_EVENT",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Bản ghi `AssignEvents` phải tồn tại.
- Xóa mềm bản ghi bằng cách đặt `IsDisable = true` (hoặc xóa vật lý nếu dữ liệu chưa phát sinh ràng buộc điểm số).
- Toàn bộ các phân công bảng đấu liên quan (`AssignTracks`) của giảng viên này trong event cũng được tự động disable theo để tránh phân quyền chấm điểm sai lệch.
- Việc vô hiệu hóa các bảng liên quan bắt buộc thực hiện trong một **Database Transaction**.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy thông tin phân công.",
  "MessageCode": "ASSIGNMENT_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Không phải vai trò Admin. |
| 404 | ASSIGNMENT_NOT_FOUND | Bản ghi phân công không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
