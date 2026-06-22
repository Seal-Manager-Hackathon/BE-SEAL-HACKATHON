# Tạo mới đề thi (Admin Create Topic)

## Tác dụng
Cho phép Staff/Admin tạo mới một đề thi (Topic) và gán trực tiếp vào một phân ban (Track) cụ thể.

## URL
`POST /api/v1/admin/tracks/{trackId}/topics`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC, có quyền quản lý event chứa track này)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `trackId` (Guid, Bắt buộc): ID của phân ban (Track) mà đề thi sẽ thuộc về.

## Request Body
```json
{
  "title": "Hệ thống số hóa y tế",
  "description": "Xây dựng ứng dụng quản lý quy trình khám chữa bệnh từ xa."
}
```

## Response body (Success - 201 Created)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "title": "Hệ thống số hóa y tế",
    "description": "Xây dựng ứng dụng quản lý quy trình khám chữa bệnh từ xa.",
    "isDisable": false,
    "createdAt": "2026-06-23T08:00:00Z",
    "updatedAt": "2026-06-23T08:00:00Z"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-23T08:00:00Z"
}
```

## Business rules
- `trackId` phải tồn tại trong DB, nếu không báo lỗi `TRACK_NOT_FOUND`.
- `title` là trường bắt buộc (required), không được để trống.
- Tên đề thi (`title`) không được trùng lặp với các đề thi khác đã tồn tại trong cùng một Track. Nếu trùng báo lỗi `TOPIC_TITLE_ALREADY_EXISTS`.
- Hệ thống tự động khởi tạo các trường `createdAt`, `updatedAt`, và đặt `isDisable = false` mặc định.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Bad Request",
  "Status": 400,
  "Detail": "Tên đề thi không được để trống.",
  "MessageCode": "TOPIC_TITLE_REQUIRED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-23T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | TOPIC_TITLE_REQUIRED | Tên đề thi không được rỗng. |
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không có quyền thực hiện hành động này. |
| 404 | TRACK_NOT_FOUND | Phân ban (Track) không tồn tại. |
| 409 | TOPIC_TITLE_ALREADY_EXISTS | Tên đề thi đã tồn tại trong phân ban này. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
