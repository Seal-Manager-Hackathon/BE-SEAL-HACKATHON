# Cập nhật đề thi (Admin Update Topic)

## Tác dụng
Cho phép Staff/Admin cập nhật lại nội dung đề thi (sửa tiêu đề, cập nhật mô tả, link đề bài).

## URL
`PATCH /api/v1/admin/topics/{topicId}`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `topicId` (Guid, Bắt buộc): ID của đề thi (Topic).

## Request Body
```json
{
  "Title": "Hệ thống số hóa y tế - Cập nhật",
  "description": "Mô tả đề thi cập nhật mới."
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "TOPIC_UPDATED_SUCCESSFULLY",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Đề thi phải tồn tại trong DB, nếu không báo lỗi `TOPIC_NOT_FOUND`.
- `title` nếu truyền không được rỗng và không trùng tên đề thi khác trong cùng một track.
- Việc sửa đổi nội dung đề thi cần được hệ thống logging/audit kỹ càng để tránh rủi ro gian lận.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy đề bài thi đấu để chỉnh sửa.",
  "MessageCode": "TOPIC_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | TOPIC_TITLE_REQUIRED | Tên đề thi không được rỗng. |
| 403 | FORBIDDEN | Không có quyền quản lý sự kiện chứa đề thi này. |
| 404 | TOPIC_NOT_FOUND | Đề thi không tồn tại. |
| 409 | TOPIC_TITLE_ALREADY_EXISTS | Tên đề thi cập nhật bị trùng lặp trong cùng bảng đấu. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
