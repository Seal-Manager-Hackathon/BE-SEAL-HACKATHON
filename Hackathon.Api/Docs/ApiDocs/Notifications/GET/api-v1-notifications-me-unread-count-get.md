# Đếm thông báo chưa đọc (Get Unread Count)

## Tác dụng
Trả về số lượng thông báo chưa đọc của user hiện tại.

## URL
`GET /api/v1/notifications/me/unread-count`

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
  "data": {
    "unreadCount": 5
  },
  "message": "SUCCESS"
}
```

## Business rules
- Đếm notifications của user đang đăng nhập, `IsDisable = false` và `Status = Unread`.
- Không phân trang.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
