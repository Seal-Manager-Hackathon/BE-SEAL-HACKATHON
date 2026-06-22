# Ẩn/Hiện đề thi (Admin Toggle Topic Visibility)

## Tác dụng
Cho phép Staff/Admin ẩn hoặc công bố đề thi ra giao diện public của thí sinh.

## URL
`PATCH /api/v1/admin/topics/{topicId}/visibility`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `topicId` (Guid, Bắt buộc): ID của đề thi.

## Request Body
```json
{
  "isVisible": true
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "TOPIC_VISIBILITY_UPDATED",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Đề thi phải tồn tại trong DB.
- BTC kiểm tra quyền của Staff gán sự kiện chứa đề thi này.
- *Lưu ý*: Vì DB hiện chưa thiết lập trường `IsVisible` riêng nên cờ ẩn hiện tạm thời được cập nhật thông qua trạng thái `IsDisable` hoặc các cấu hình Metadata của Event. Nếu DB sau này được bổ sung trường `IsVisible` riêng cho Topic, API này sẽ ánh xạ trực tiếp vào trường đó.

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
| 403 | FORBIDDEN | Staff chưa được phân công quản lý sự kiện này. |
| 404 | TOPIC_NOT_FOUND | Đề thi không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
