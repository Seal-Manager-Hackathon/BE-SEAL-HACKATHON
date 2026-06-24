# Mentor xem danh sách các team trong bảng đấu

## Tác dụng
Giúp Mentor xem danh sách tất cả các đội thi (team) thuộc bảng đấu (Track) mình được phân công phụ trách. Team nào chọn/được gán vào track này thì thuộc phạm vi mentor đảm nhiệm.

## URL
`GET /api/v1/mentor/tracks/{trackId}/teams`

## Quyền
Mentor phụ trách track (Yêu cầu đăng nhập tài khoản Giảng viên được phân công)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `trackId` (Guid, Bắt buộc): ID của Track cần lấy danh sách team.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa danh sách team thi đấu.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": [
    {
      "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
      "teamName": "Chiến binh công nghệ",
      "topicId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
      "topicTitle": "Hệ thống quản lý y tế thông minh",
      "leaderName": "Hoàng Phạm",
      "memberCount": 5
    }
  ],
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Mentor gọi API phải được phân công phụ trách track thi đấu này (đối chiếu qua bảng `AssignTracks`), nếu không từ chối và báo lỗi `FORBIDDEN` (BR-MEN-01).
- Trả về danh sách các team đã chọn/được gán track thi đấu này trong bảng `RegisterTeams` và đơn đăng ký được duyệt (`Status = Approved`).
- Mentor chỉ quản lý/xem các team thuộc track mình được phân công; không xem hoặc quản lý team ở track khác.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Bạn không được phân công hướng dẫn bảng đấu này.",
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
| 403 | FORBIDDEN | Không được phân công phụ trách track này (check BR-MEN-01). |
| 404 | TRACK_NOT_FOUND | Track không tồn tại trong DB. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
