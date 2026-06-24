# Xóa đề thi (Admin Delete Topic)

## Tác dụng
Cho phép Staff/Admin xóa mềm (disable) đề thi khỏi bảng đấu.

## URL
`DELETE /api/v1/admin/topics/{topicId}`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `topicId` (Guid, Bắt buộc): ID của đề thi cần xóa.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "TOPIC_DELETED_SUCCESSFULLY",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Đề thi phải tồn tại trong DB, nếu không báo lỗi `TOPIC_NOT_FOUND`.
- Staff phải có quyền quản lý event chứa đề này.
- Cập nhật cờ `IsDisable = true` của Topic để ẩn đề thi khỏi hệ thống (Trường `IsDisable` được kế thừa từ `BaseEntity`).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy đề thi.",
  "MessageCode": "TOPIC_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không có quyền quản lý sự kiện chứa đề thi. |
| 404 | TOPIC_NOT_FOUND | Đề thi không tồn tại trong hệ thống. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
