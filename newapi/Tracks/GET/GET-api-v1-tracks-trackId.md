# Xem chi tiết bảng đấu (Track Detail)

## Tác dụng
Xem thông tin cấu hình chi tiết của một bảng đấu (Track).

## URL
`GET /api/v1/tracks/{trackId}`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Path Parameters:**
    *   `trackId` (Guid, Bắt buộc): ID của bảng đấu cần xem.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "id": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
    "Title": "Bảng A - Web Application",
    "description": "Phát triển Web.",
    "maxTeam": 50,
    "isDisable": false,
    "createdAt": "2026-06-21T08:00:00Z"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Track phải tồn tại trong hệ thống, nếu không báo lỗi `TRACK_NOT_FOUND`.
- Trả ra đầy đủ các trường thông tin cấu hình của Track.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy bảng đấu.",
  "MessageCode": "TRACK_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | TRACK_NOT_FOUND | Track không tồn tại trong DB. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
