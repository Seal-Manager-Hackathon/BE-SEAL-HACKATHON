# Mentor create notification

## Tác dụng
Mentor viết và phát một thông báo hoặc hướng dẫn mới xuống toàn bộ các Đội thi thuộc Bảng đấu (Track) mình được phân công phụ trách.

## URL
`POST /api/mentor-notifications`

## Authorization
Yêu cầu access token hợp lệ và role `Lecturer` với event role `Mentor`.

## Request body
```json
{
  "trackId": "guid",
  "title": "string",
  "description": "string"
}
```

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "id": "guid",
    "trackId": "guid",
    "title": "string",
    "description": "string",
    "message": "MENTOR_NOTIFICATION_CREATED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- User hiện tại phải là Mentor được assign vào track qua `AssignEvents` và `AssignTracks`.
- Track phải tồn tại và chưa bị soft-disable.
- Mentor chỉ được gửi thông báo cho track mình phụ trách.
- `title` và `description` là bắt buộc.
- Thông báo mentor được lưu bằng `MentorNotifications`.
- Thông báo hướng tới các team thuộc track được mentor phụ trách; mentor không có quyền chấm điểm.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | MENTOR_REQUIRED |
| 403 | FORBIDDEN | MENTOR_NOT_ASSIGNED_TO_TRACK |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 400 | BAD_REQUEST | NOTIFICATION_TITLE_REQUIRED |
| 400 | BAD_REQUEST | NOTIFICATION_DESCRIPTION_REQUIRED |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
