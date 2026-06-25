# Xem chi tiết bảng đấu (Track Detail)

## Tác dụng
Xem thông tin cấu hình chi tiết của một bảng đấu (Track).

## URL
`GET /api/v1/tracks/{trackId}`

## Authorization
Không yêu cầu Access Token (Public API).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `trackId` | `guid` | Có | ID của bảng đấu cần xem. |

## Ví dụ request
```http
GET /api/v1/tracks/c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d
```

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
    "isDisable": false,
    "createdAt": "2026-06-21T08:00:00Z"
  },
  "message": "SUCCESS"
}
```

## Business rules
- Track phải tồn tại trong hệ thống, nếu không báo lỗi `TRACK_NOT_FOUND`.
- Trả ra đầy đủ các trường thông tin cấu hình của Track.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
