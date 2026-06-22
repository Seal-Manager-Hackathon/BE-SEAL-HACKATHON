# Lấy danh sách phiên hoạt động (Get Active Sessions)

## Tác dụng
Xem danh sách các phiên đăng nhập (refresh token) đang active của user, bao gồm thông tin IP, trình duyệt và nhãn thiết bị để người dùng quản lý bảo mật.

## URL
`GET /api/v1/auth/sessions`

## Quyền
Authenticated User (Yêu cầu đăng nhập)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa danh sách các phiên đăng nhập.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "ipAddress": "192.168.1.1",
      "userAgent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64)...",
      "deviceLabel": "Windows Laptop",
      "createdAt": "2026-06-22T04:00:00Z",
      "expiredAt": "2026-06-29T04:00:00Z",
      "isCurrentSession": true
    }
  ],
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Trả về danh sách refresh token của user đang hoạt động (`RevokedAt == null` và `ExpiredAt > DateTimeOffset.UtcNow`).
- Đánh dấu phiên hiện tại `isCurrentSession: true` bằng cách đối chiếu JTI claim trong access token hoặc IP/UserAgent của request hiện tại.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Unauthorized",
  "Status": 401,
  "Detail": "Xác thực phiên thất bại.",
  "MessageCode": "UNAUTHORIZED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc hết hạn. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi phát sinh tại server. |
