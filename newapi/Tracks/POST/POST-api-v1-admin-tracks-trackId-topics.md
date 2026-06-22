# Tạo đề thi mới (Admin Create Topic)

## Tác dụng
Cho phép Staff/Admin khởi tạo một đề thi/chủ đề thi (Topic) mới thuộc về một bảng đấu.

## URL
`POST /api/v1/admin/tracks/{trackId}/topics`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `trackId` (Guid, Bắt buộc): ID của Track (bảng đấu) muốn thêm đề.

## Request Body
```json
{
  "Title": "Hệ thống số hóa y tế",
  "description": "Xây dựng ứng dụng quản lý quy trình khám chữa bệnh."
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa ID của topic.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "message": "TOPIC_CREATED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Track liên kết phải tồn tại trong DB, nếu không báo lỗi `TRACK_NOT_FOUND`.
- Staff phải có quyền quản lý sự kiện chứa track này.
- `title` là bắt buộc, không được để trống và không được trùng với đề thi khác trong cùng bảng đấu.
- Thiết lập cờ `IsDisable = false` khi tạo mới.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Tên đề thi này đã được sử dụng trong bảng đấu.",
  "MessageCode": "TOPIC_TITLE_ALREADY_EXISTS",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | TOPIC_TITLE_REQUIRED | Không được bỏ trống tên đề thi. |
| 403 | FORBIDDEN | Không được phân công phụ trách quản lý bảng này. |
| 404 | TRACK_NOT_FOUND | Bảng đấu không tồn tại. |
| 409 | TOPIC_TITLE_ALREADY_EXISTS | Trùng tên đề trong bảng đấu. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
