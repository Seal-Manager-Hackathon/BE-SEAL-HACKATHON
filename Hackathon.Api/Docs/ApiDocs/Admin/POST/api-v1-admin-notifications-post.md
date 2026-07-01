# Admin gửi thông báo toàn hệ thống (Admin Send System Notification)

## Tác dụng
Admin gửi thông báo đến toàn bộ người dùng trong hệ thống (tất cả user).

## URL
`POST /api/v1/admin/notifications`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.
Policy: `AdminPolicy`.

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request body
```json
{
  "title": "Thông báo hệ thống",
  "description": "Hệ thống sẽ bảo trì từ 00:00 đến 02:00 ngày 15/07/2026."
}
```

| Field | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `title` | `string` | Có | Tiêu đề thông báo. Không được rỗng hoặc chỉ chứa khoảng trắng. |
| `description` | `string` | Có | Nội dung chi tiết thông báo. Không được rỗng hoặc chỉ chứa khoảng trắng. |

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-07-01T08:00:00Z",
  "data": {
    "notificationIds": [
      "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "4fa85f64-5717-4562-b3fc-2c963f66afa7"
    ],
    "totalSent": 2
  },
  "message": "SYSTEM_NOTIFICATION_SENT"
}
```

## Business rules
- Người gọi phải có role `Admin`.
- `title` là bắt buộc, sau khi trim không được rỗng.
- `description` là bắt buộc, sau khi trim không được rỗng.
- Hệ thống tạo một bản ghi trong bảng `Notifications` cho mỗi user đang hoạt động (`IsDisable = false`).
- Mỗi bản ghi có `UserId = userId`, `TeamId = null`, `TargetType = System` (enum value `2`).
- Thông báo được tạo với `Status = Unread`, `CreatedAt = DateTimeOffset.UtcNow`.
- Mỗi user thấy bản ghi system notification của chính mình qua `GET /api/v1/notifications/me`, nên trạng thái đọc/chưa đọc độc lập theo từng user.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | TITLE_REQUIRED |
| 400 | BAD_REQUEST | DESCRIPTION_REQUIRED |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- **Đã implement** trong `Hackathon.Api.Controllers.AdminController`.
- Route hiện có: `POST /api/v1/admin/notifications`.
- Policy: `AdminPolicy`.
- Service: `Hackathon.Service.Admin.Service.SendSystemNotification()`.
- DTO request: `SendSystemNotificationRequest` — fields: `title`, `description`.
- DTO response: `SendSystemNotificationResponse` — fields: `notificationIds`, `totalSent`.
- Entity: `Notifications` — tạo N bản ghi theo số user active với `TargetType = System`, `Status = Unread`.
