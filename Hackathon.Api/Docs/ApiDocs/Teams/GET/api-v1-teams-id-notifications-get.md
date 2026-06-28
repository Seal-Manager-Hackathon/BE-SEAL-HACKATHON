# Get team notifications

## Tác dụng
Cho phép các thành viên trong team xem danh sách các thông báo gửi riêng cho team của mình từ Ban tổ chức.

## URL
`GET /api/v1/teams/{teamId}/notifications`

## Authorization
Yêu cầu access token hợp lệ và người dùng phải là thành viên đang hoạt động của team (`Status = Active` trong `TeamDetails`).

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần xem thông báo.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa danh sách thông báo của team.*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "SUCCESS",
  "data": [
    {
      "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
      "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
      "title": "Thông báo duyệt nhóm",
      "description": "Nhóm của bạn đã được duyệt tham gia giải đấu SEAL Hackathon 2026.",
      "createdAt": "2026-06-22T08:00:00Z"
    }
  ]
}
```

## Business rules
- Team phải tồn tại trong DB, không bị soft-disable.
- Người gọi phải đang là thành viên hoạt động trong team (`Status = Active` trong `TeamDetails`).
- Trích xuất toàn bộ các thông báo trong bảng `Notifications` liên kết với `teamId`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Forbidden",
  "status": 403,
  "message": "NOT_A_TEAM_MEMBER",
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
| 403 | FORBIDDEN | NOT_A_TEAM_MEMBER |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
