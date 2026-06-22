# Xóa bảng đấu (Admin Delete Track)

## Tác dụng
Cho phép Admin xóa mềm (disable) bảng đấu thi đấu khỏi hệ thống.

## URL
`DELETE /api/v1/admin/tracks/{trackId}`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `trackId` (Guid, Bắt buộc): ID của bảng đấu cần xóa.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "TRACK_DELETED_SUCCESSFULLY",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Track phải tồn tại trong DB, nếu không báo lỗi `TRACK_NOT_FOUND`.
- Thay đổi cờ `IsDisable = true` của Track.
- Các liên kết đề thi (Topics) của bảng đấu này nên được tự động disable theo để tránh mâu thuẫn dữ liệu.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy bảng đấu cần xóa.",
  "MessageCode": "TRACK_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Không phải quản trị viên. |
| 404 | TRACK_NOT_FOUND | Track không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
