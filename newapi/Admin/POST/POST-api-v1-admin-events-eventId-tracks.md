# Tạo bảng đấu mới (Admin Create Track)

## Tác dụng
Cho phép Admin khởi tạo một bảng đấu (Track) mới thuộc về một sự kiện.

## URL
`POST /api/v1/admin/events/{eventId}/tracks`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của sự kiện muốn tạo bảng đấu.

## Request Body
```json
{
  "Title": "Bảng A - Web Application",
  "description": "Phát triển Web.",
  "maxTeam": 50
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa ID của track mới.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "id": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "message": "TRACK_CREATED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Event phải tồn tại trong DB, nếu không báo lỗi `EVENT_NOT_FOUND`.
- `title` là bắt buộc, không được để trống và không được trùng với tên track khác trong cùng một event.
- Thiết lập cờ `IsDisable = false` khi tạo mới.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Tên bảng đấu này đã được sử dụng cho sự kiện này.",
  "MessageCode": "TRACK_TITLE_ALREADY_EXISTS",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | TRACK_TITLE_REQUIRED | Trường title không được rỗng. |
| 404 | EVENT_NOT_FOUND | Event liên kết không tồn tại. |
| 409 | TRACK_TITLE_ALREADY_EXISTS | Trùng tên track trong cùng sự kiện. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
