# Xem thông tin phiên bản (Get System Version)

## Tác dụng
Lấy thông tin phiên bản build hiện tại của backend phục vụ bảo trì.

## URL
`GET /api/v1/version`

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
  "message": "GET_SYSTEM_VERSION_SUCCESSFUL",
  "data": {
    "version": "1.0.4-build.20260622",
    "environment": "Production",
    "dotnetVersion": ".NET 8.0"
  }
}
```

## Business rules
- Trả ra phiên bản assembly của project được BTC cấu hình.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi:*

```json
{
  "title": "Internal Server Error",
  "status": 500,
  "message": "AN_UNEXPECTED_ERROR_OCCURRED",
  "messageCode": "INTERNAL_SERVER_ERROR",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
