# Mentor xem danh sách sự kiện phụ trách

## Tác dụng
Giúp giảng viên được phân công vai trò Mentor xem danh sách các event mà mình tham gia hỗ trợ chuyên môn trong mùa giải.

## URL
`GET /api/v1/mentor/events`

## Quyền
Lecturer với vai trò Mentor (Yêu cầu đăng nhập tài khoản Giảng viên)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": [
    {
      "assignEventId": "b1a7d6c2-4821-4f9b-bd5e-3c2fa56789e0",
      "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
      "eventName": "SEAL Hackathon 2026",
      "role": "Mentor"
    }
  ],
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Người gọi phải là giảng viên (`Role = Lecturer` trong `Users`).
- Trích xuất thông tin phân công trong bảng nối `AssignEvents` liên kết với `EventRoles` có vai trò là `Mentor` (giá trị enum `0`) trong event (BR-ASG-02).
- Chỉ lấy các sự kiện chưa bị disable.

### Bảng vai trò EventRoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Bạn không phải Mentor hoặc không được phân công hỗ trợ sự kiện nào.",
  "MessageCode": "FORBIDDEN",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Giảng viên chưa được phân công làm Mentor trong event nào. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
