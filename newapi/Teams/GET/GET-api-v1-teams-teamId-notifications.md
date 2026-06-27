# Xem thông báo riêng của Team (Get Team Notifications)

## Tác dụng
Cho phép các thành viên trong team xem danh sách các thông báo gửi riêng cho team của mình từ Ban tổ chức.

## URL
`GET /api/v1/teams/{teamId}/notifications`

## Quyền
Authenticated User (Yêu cầu đăng nhập, là thành viên trong team)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần xem thông báo.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa danh sách thông báo của team.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": [
    {
      "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
      "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
      "Title": "Thông báo duyệt nhóm",
      "description": "Nhóm của bạn đã được duyệt tham gia giải đấu SEAL Hackathon 2026.",
      "createdAt": "2026-06-22T08:00:00Z"
    }
  ],
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Team phải tồn tại trong DB, không bị soft-disable.
- Người gọi phải đang là thành viên hoạt động trong team (`Status = Active` trong `TeamDetails`).
- Trích xuất toàn bộ các thông báo trong bảng `Notifications` liên kết với `teamId`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Bạn không phải thành viên hoạt động của đội thi này.",
  "MessageCode": "NOT_A_TEAM_MEMBER",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | NOT_A_TEAM_MEMBER | Sinh viên không thuộc team hoặc trạng thái Inactive. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
