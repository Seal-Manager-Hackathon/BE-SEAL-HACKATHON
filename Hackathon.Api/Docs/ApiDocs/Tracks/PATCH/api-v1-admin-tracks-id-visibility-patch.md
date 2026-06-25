# Ẩn/Hiện bảng đấu (Admin Toggle Track Visibility)

## Tác dụng
Cho phép Staff/Admin ẩn hoặc hiện bảng đấu (Track) ra giao diện public của thí sinh.

## URL
`PATCH /api/v1/admin/tracks/{trackId}/visibility`

## Authorization
Yêu cầu Access Token của tài khoản Staff hoặc Admin (BTC).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `trackId` | `guid` | Có | ID của bảng đấu. |

## Request body
```json
{
  "isVisible": true
}
```

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
    "isDisable": false,
    "createdAt": "2026-06-21T08:00:00Z",
    "updatedAt": "2026-06-22T08:00:00Z"
  },
  "message": "TRACK_VISIBILITY_UPDATED"
}
```

## Business rules
- Track phải tồn tại trong DB.
- BTC kiểm tra quyền của Staff đối với sự kiện chứa track này.
- *Lưu ý*: Vì DB hiện chưa có trường `IsVisible` riêng nên cờ ẩn hiện tạm thời được cập nhật thông qua việc chuyển đổi trạng thái `IsDisable` hoặc thông qua một cấu hình metadata của Event. Nếu DB sau này được bổ sung trường `IsVisible`, API này sẽ ánh xạ trực tiếp vào trường đó.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | TRACK_ID_REQUIRED |
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
