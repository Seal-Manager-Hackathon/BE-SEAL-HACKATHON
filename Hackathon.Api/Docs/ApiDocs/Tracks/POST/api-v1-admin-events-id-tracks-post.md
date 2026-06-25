# Tạo bảng đấu mới (Admin Create Track)

## Tác dụng
Cho phép Admin khởi tạo một bảng đấu (Track) mới thuộc về một sự kiện.

## URL
`POST /api/v1/admin/events/{eventId}/tracks`

## Authorization
Yêu cầu Access Token của tài khoản Admin.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `eventId` | `guid` | Có | ID của sự kiện muốn tạo bảng đấu. |

## Request body
```json
{
  "title": "Bảng A - Web Application",
  "description": "Phát triển Web.",
  "maxTeam": 50
}
```

## Response body
Response dùng `ApiResponseFactory.Base(result)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 201,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": {
    "id": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d"
  },
  "message": "TRACK_CREATED_SUCCESSFULLY"
}
```

## Business rules
- Event phải tồn tại trong DB, nếu không báo lỗi `EVENT_NOT_FOUND`.
- `title` là bắt buộc, không được để trống và không được trùng với tên track khác trong cùng một event.
- Thiết lập cờ `IsDisable = false` khi tạo mới.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | TRACK_TITLE_REQUIRED |
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 409 | CONFLICT | TRACK_TITLE_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
