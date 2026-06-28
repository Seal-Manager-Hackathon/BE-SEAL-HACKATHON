# Mentor gửi thông báo riêng cho từng team (Mentor Send Team Notification)

## Tác dụng
Cho phép Mentor gửi thông báo hướng dẫn kỹ thuật một chiều tới một team cụ thể nằm trong bảng đấu (Track) mình hướng dẫn.

## URL
`POST /api/v1/mentor/teams/{teamId}/notifications`

## Quyền
Mentor phụ trách track của team (Yêu cầu đăng nhập tài khoản Giảng viên được phân công)

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team muốn gửi thông báo.

## Request Body
```json
{
  "title": "Nhắc nhở kỹ thuật Vòng 1",
  "description": "Nhóm bạn chú ý validate kỹ các API đầu vào."
}
```

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
  "data": {
    "mentorNotificationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "message": "MENTOR_NOTIFICATION_SENT"
  }
}
```

## Business rules
- Team phải tồn tại, đã được duyệt (`Approved`) và không bị ban.
- Team phải thuộc track mà mentor được phân công phụ trách.
- Mentor gọi API phải được phân công phụ trách track của team này.
- Tạo bản ghi mới trong bảng `MentorNotifications` liên kết với `AssignTrackId` của mentor.
- Thông báo này là một chiều: thí sinh/team chỉ đọc, không có API phản hồi/chat lại mentor.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

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
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không được phân công hướng dẫn track chứa team này. |
| 404 | REGISTER_TEAM_NOT_FOUND | Team không tồn tại hoặc chưa được duyệt. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
