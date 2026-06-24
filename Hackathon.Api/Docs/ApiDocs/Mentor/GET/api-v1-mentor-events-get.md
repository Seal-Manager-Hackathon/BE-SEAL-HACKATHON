# Giảng viên xem danh sách sự kiện được phân công

## Tác dụng
Giúp giảng viên (Lecturer) xem danh sách các event mà mình được phân công tham gia hỗ trợ hoặc chấm điểm (với các vai trò như Mentor, Judge,...) trong mùa giải.

## URL
`GET /api/v1/mentor/events`

## Quyền
Lecturer đã được phân công trong sự kiện (Yêu cầu đăng nhập tài khoản Giảng viên)

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": {
    "Items": [
      {
        "assignEventId": "b1a7d6c2-4821-4f9b-bd5e-3c2fa56789e0",
        "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
        "eventName": "SEAL Hackathon 2026",
        "role": "Mentor"
      },
      {
        "assignEventId": "c2b8e7d3-5932-5a0c-ce6f-4d3fb6789af1",
        "eventId": "f8e7d6c5-b4a3-2109-8c7d-6e5f4a3b2c1d",
        "eventName": "AI Hackathon 2026",
        "role": "Judge"
      }
    ],
    "PageIndex": 1,
    "PageSize": 10,
    "TotalCount": 2,
    "HasNextPage": false,
    "HasPreviousPage": false
  }
}
```

## Business rules
- Người gọi phải là giảng viên (`Role = Lecturer` trong `Users`).
- Trích xuất thông tin phân công trong bảng nối `AssignEvents` liên kết với `EventRoles` của giảng viên hiện tại (không giới hạn riêng vai trò Mentor).
- Chỉ lấy các sự kiện chưa bị disable.

### Bảng vai trò EventRoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Forbidden",
  "status": 403,
  "message": "FORBIDDEN",
  "messageCode": "FORBIDDEN",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
