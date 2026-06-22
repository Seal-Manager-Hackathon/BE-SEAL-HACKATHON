# Judge xem danh sách bảng đấu được phân công

## Tác dụng
Giúp giảng viên được phân công vai trò Judge xem danh sách các bảng đấu (track) mà mình chịu trách nhiệm chấm điểm trong sự kiện.

## URL
`GET /api/v1/judge/tracks`

## Quyền
Lecturer với vai trò Judge trong event (Yêu cầu đăng nhập tài khoản Giảng viên)

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
      "assignTrackId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
      "trackTitle": "Bảng A - Web Application",
      "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
      "eventName": "SEAL Hackathon 2026",
      "role": "Judge"
    }
  ],
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Người gọi phải là giảng viên (`Role = Lecturer` trong `Users`).
- Trả về danh sách các track mà giám khảo này được phân công thông qua bảng nối `AssignTracks` và `AssignEvents` có cờ role là `Judge` trong event (BR-ASG-03).
- Chỉ lấy các track và event đang hoạt động (`IsDisable = false`).

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
  "Detail": "Bạn không có quyền Judge hoặc không được phân công chấm thi bảng nào.",
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
| 403 | FORBIDDEN | Giảng viên chưa được phân công làm Judge trong bảng đấu nào. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
