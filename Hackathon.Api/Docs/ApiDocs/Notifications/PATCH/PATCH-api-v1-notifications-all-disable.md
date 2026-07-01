# Disable toàn bộ thông báo (Disable All Notifications)

## Tác dụng
Soft-delete (disable) toàn bộ thông báo đang hiển thị của user hiện tại.

## URL
`PATCH /api/v1/notifications/all/disable`

## Quyền
Authenticated User (Yêu cầu đăng nhập)

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": null,
  "message": "ALL_NOTIFICATIONS_DISABLED"
}
```

## Business rules
- Tìm tất cả notifications của user đang đăng nhập có `IsDisable = false`.
- Set `IsDisable = true` và `UpdatedAt = now`.
- Không giới hạn trạng thái (disable cả đã đọc, chưa đọc).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
