# Xóa bảng đấu (Admin Delete Track)

## Tác dụng
Cho phép Admin xóa mềm (disable) bảng đấu thi đấu khỏi hệ thống.

## URL
`DELETE /api/v1/admin/tracks/{trackId}`

## Authorization
Yêu cầu Access Token của tài khoản Admin.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `trackId` | `guid` | Có | ID của bảng đấu cần xóa. |

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.Base(result)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": {
    "id": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
    "title": "Bảng A - Web Application",
    "description": "Phát triển Web.",
    "maxTeam": 50,
    "isDisable": true,
    "createdAt": "2026-06-21T08:00:00Z",
    "updatedAt": "2026-06-22T08:00:00Z"
  },
  "message": "TRACK_DELETED_SUCCESSFULLY"
}
```

## Business rules
- Track phải tồn tại trong DB, nếu không báo lỗi `TRACK_NOT_FOUND`.
- Thay đổi cờ `IsDisable = true` của Track.
- Các liên kết đề thi (Topics) của bảng đấu này nên được tự động disable theo để tránh mâu thuẫn dữ liệu.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | TRACK_ID_REQUIRED |
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
