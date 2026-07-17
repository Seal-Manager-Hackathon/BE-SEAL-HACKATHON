# Health check hệ thống (Get System Health)

## Tác dụng
Kiểm tra trạng thái hoạt động của dịch vụ API và tính kết nối ổn định tới Database.

## URL
`GET /api/v1/health`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "GET_SYSTEM_HEALTH_SUCCESSFUL",
  "data": {
    "status": 1 /* 0: Unhealthy, 1: Healthy */,
    "database": 1 /* 0: Disconnected, 1: Connected */,
    "uptimeSeconds": 86400
  }
}
```

## Business rules
- Thực hiện ping kiểm tra kết nối DB. Nếu DB không phản hồi, status trả về `0` (hoặc trả lỗi `503 Service Unavailable`).

## Lỗi có thể xảy ra
*Khi gặp lỗi kết nối, API trả về cấu trúc lỗi:*

```json
{
  "title": "Service Unavailable",
  "status": 503,
  "message": "DATABASE_CONNECTION_LOST",
  "messageCode": "DATABASE_CONNECTION_LOST",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 503 | DATABASE_CONNECTION_LOST | DATABASE_CONNECTION_LOST |
