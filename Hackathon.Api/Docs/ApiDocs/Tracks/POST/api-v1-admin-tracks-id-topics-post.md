# Tạo đề thi mới (Admin Create Topic)

## Tác dụng
Cho phép Staff/Admin khởi tạo một đề thi/chủ đề thi (Topic) mới thuộc về một bảng đấu.

## URL
`POST /api/v1/admin/tracks/{trackId}/topics`

## Authorization
Yêu cầu Access Token của tài khoản Staff hoặc Admin (BTC).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `trackId` | `guid` | Có | ID của Track (bảng đấu) muốn thêm đề. |

## Request body
```json
{
  "title": "Hệ thống số hóa y tế",
  "description": "Xây dựng ứng dụng quản lý quy trình khám chữa bệnh."
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
    "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0"
  },
  "message": "TOPIC_CREATED_SUCCESSFULLY"
}
```

## Business rules
- Track liên kết phải tồn tại trong DB, nếu không báo lỗi `TRACK_NOT_FOUND`.
- Staff phải có quyền quản lý sự kiện chứa track này.
- `title` là bắt buộc, không được để trống và không được trùng với đề thi khác trong cùng bảng đấu.
- Thiết lập cờ `IsDisable = false` khi tạo mới.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | TOPIC_TITLE_REQUIRED |
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 409 | CONFLICT | TOPIC_TITLE_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
