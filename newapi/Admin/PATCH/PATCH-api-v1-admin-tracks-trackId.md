# Cập nhật bảng đấu (Admin Update Track)

## Tác dụng
Cho phép Admin cập nhật thông tin chi tiết của một bảng đấu (Track) đã thiết lập.

## URL
`PATCH /api/v1/admin/tracks/{trackId}`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `trackId` (Guid, Bắt buộc): ID của bảng đấu cần sửa.

## Request Body
```json
{
  "Title": "Bảng A - Web Application - Updated",
  "description": "Mô tả mới.",
  "maxTeam": 60
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "TRACK_UPDATED_SUCCESSFULLY",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Track phải tồn tại trong DB, nếu không báo lỗi `TRACK_NOT_FOUND`.
- `title` nếu truyền không được rỗng và không trùng với track khác trong cùng event.
- Thực hiện partial update: chỉ cập nhật các trường được truyền.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy bảng đấu cần sửa.",
  "MessageCode": "TRACK_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | TRACK_TITLE_REQUIRED | Không được gán title trống. |
| 404 | TRACK_NOT_FOUND | Track không tồn tại trong hệ thống. |
| 409 | TRACK_TITLE_ALREADY_EXISTS | Trùng tên track khác cùng event. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
