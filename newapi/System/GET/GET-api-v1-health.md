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
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "Status": "Healthy",
    "database": "Connected",
    "uptimeSeconds": 86400
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Thực hiện ping kiểm tra kết nối DB. Nếu DB không phản hồi, status trả về `Unhealthy` (hoặc trả lỗi `503 Service Unavailable`).

## Lỗi có thể xảy ra
*Khi gặp lỗi kết nối, API trả về cấu trúc lỗi:*

```json
{
  "Title": "Service Unavailable",
  "Status": 503,
  "Detail": "Mất kết nối tới Database.",
  "MessageCode": "DATABASE_CONNECTION_LOST",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 503 | DATABASE_CONNECTION_LOST | Mất kết nối tới máy chủ DB SQL Server. |
