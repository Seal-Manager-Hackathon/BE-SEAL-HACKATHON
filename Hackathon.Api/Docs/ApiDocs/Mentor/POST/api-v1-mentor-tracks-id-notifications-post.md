# Mentor phát thông báo một chiều (Mentor Send Notification)

## Tác dụng
Cho phép Mentor gửi thông báo hướng dẫn kỹ thuật một chiều tới toàn bộ các team nằm trong bảng đấu (Track) mình hướng dẫn.

## URL
`POST /api/v1/mentor/tracks/{trackId}/notifications`

## Quyền
Mentor phụ trách track (Yêu cầu đăng nhập tài khoản Giảng viên được phân công)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `trackId` (Guid, Bắt buộc): ID của Track (bảng đấu) muốn phát thông báo.

## Request Body
```json
{
  "title": "Nhắc nhở kỹ thuật Vòng 1",
  "description": "Các nhóm chú ý validate kỹ các API đầu vào và đính kèm file database migration vào Project."
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "Value": {
    "mentorNotificationId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "message": "MENTOR_NOTIFICATION_SENT"
  },
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Track được phát thông báo phải tồn tại và đang hoạt động.
- Mentor gọi API phải được phân công phụ trách hướng dẫn track này (`AssignTracks` liên kết `TrackId` và `AssignEventId` của Mentor, check BR-MEN-02).
- Thông báo chỉ gửi tới các team đã chọn/được gán vào đúng track mentor phụ trách.
- Tạo bản ghi mới trong bảng `MentorNotifications` liên kết với `AssignTrackId` và ghi nhận thời gian gửi `CreatedAt = DateTimeOffset.UtcNow`.
- Thông báo này là một chiều: thí sinh/team chỉ đọc, không có API phản hồi/chat lại mentor (BR-MEN-02).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "title": "Forbidden",
  "status": 403,
  "Detail": "Bạn không được phân công hướng dẫn bảng đấu này để phát thông báo.",
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
| 403 | FORBIDDEN | Giảng viên chưa được phân công làm Mentor cho track này. |
| 404 | TRACK_NOT_FOUND | Track không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
