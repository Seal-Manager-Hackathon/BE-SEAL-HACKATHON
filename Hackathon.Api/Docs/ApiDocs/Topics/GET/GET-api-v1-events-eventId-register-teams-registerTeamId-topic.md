# API 51: Xem đề bài của nhóm (Get Assigned Topic)

## Tác dụng
Lấy thông tin chủ đề (Topic) và phân ban (Track) của một Team (đơn đăng ký) trong phạm vi sự kiện.

## URL
`GET /api/v1/events/{eventId}/register-teams/{registerTeamId}/topic`

## Quyền
Authenticated User (Yêu cầu đăng nhập, dành cho thành viên của team)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của sự kiện.
    *   `registerTeamId` (Guid, Bắt buộc): ID đơn đăng ký của team vào sự kiện đó.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "registerTeamId": "d1e2f3a4-b5c6-d7e8-f9a0-b1c2d3e4f5a6",
    "eventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "trackTitle": "Web Application",
    "trackDescription": "Phát triển các ứng dụng nền tảng Web",
    "topicId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "topicTitle": "Quản lý Bệnh viện",
    "topicDescription": "Xây dựng hệ thống số hóa quy trình khám chữa bệnh"
  },
  "Error": null,
  "TraceId": "00-84a1e9df64619d8...",
  "TimestampUtc": "2026-06-19T10:00:00.0000000Z"
}
```

## Business rules
- Trả về Track và Topic của team đã được assign trong bảng `RegisterTeams`.
- `registerTeamId` and `eventId` phải khớp với bản ghi thực tế, nếu không trả `REGISTER_TEAM_NOT_FOUND`.
- Nếu cả Track và Topic của đơn đăng ký đều chưa được assign (bằng `null`) -> trả lỗi `400 BadRequest` với mã lỗi `TRACK_NOT_ASSIGNED`.
- Nếu một trong hai trường Track hoặc Topic bị `null` (gặp lỗi phân công chưa hoàn thiện) -> trả lỗi `400 BadRequest` với mã lỗi `TRACK_OR_TOPIC_ASSIGNMENT_INCOMPLETE_CONTACT_ADMIN` để báo người dùng liên hệ ban tổ chức/admin hỗ trợ.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy thông tin đơn đăng ký của đội.",
  "MessageCode": "REGISTER_TEAM_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | TRACK_NOT_ASSIGNED | Đội thi chưa được phân công ban thi đấu (Track). |
| 400 | TRACK_OR_TOPIC_ASSIGNMENT_INCOMPLETE_CONTACT_ADMIN | Quá trình phân ban hoặc đề tài chưa hoàn tất. Vui lòng liên hệ Admin/Staff. |
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 404 | REGISTER_TEAM_NOT_FOUND | Đơn đăng ký không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
