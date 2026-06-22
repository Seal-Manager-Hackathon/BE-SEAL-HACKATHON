# BTC phát thông báo hệ thống (BTC Send Notification)

## Tác dụng
Cho phép Staff/Admin gửi thông báo hệ thống tới một user cụ thể, một team cụ thể hoặc toàn bộ các đội thi trong sự kiện.

## URL
`POST /api/v1/staff/notifications`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Body
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "teamId": null,
  "Title": "Thông báo thay đổi thời gian thi",
  "description": "Thời gian Vòng 2 dời lại sang 14h00 cùng ngày."
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa ID và kết quả gửi.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "notificationId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "message": "NOTIFICATION_SENT"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Người gọi phải là nhân viên/admin thuộc ban tổ chức.
- `userId` và `teamId` không được đồng thời có giá trị hoặc đồng thời rỗng (nếu gửi riêng lẻ). Để gửi cho team, `teamId` phải tồn tại trong DB.
- *Lưu ý về DB*: Bảng `Notifications` hiện tại bắt buộc phải có cả `UserId` và `TeamId`. Cần cấu hình nullable cho hai trường này trong database để hỗ trợ gửi thông báo riêng biệt cho chỉ User hoặc chỉ Team (phản ánh tại mục D của `doc.md`).
- Khi tạo thành công, bản ghi lưu với trạng thái mặc định là `Unread` (hoặc `Pending` trước khi gửi đi).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Bad Request",
  "Status": 400,
  "Detail": "Phải chọn người nhận là cá nhân hoặc nhóm thi.",
  "MessageCode": "RECIPIENT_REQUIRED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | RECIPIENT_REQUIRED | Đồng thời để trống cả `userId` và `teamId` trong body. |
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Tài khoản không có vai trò Staff/Admin. |
| 404 | RECIPIENT_NOT_FOUND | Không tìm thấy User hoặc Team chỉ định trong DB. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
