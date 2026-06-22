# Nhập kết quả bốc thăm hàng loạt (Bulk Draw Assign)

## Tác dụng
Cho phép Staff/Admin nhập nhanh kết quả bốc thăm offline (gán Track + Topic) cho nhiều team cùng lúc.

## URL
`POST /api/v1/staff/events/{eventId}/draw-results/bulk`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của sự kiện.

## Request Body
```json
{
  "assignments": [
    {
      "registerTeamId": "d1e2f3a4-b5c6-d7e8-f9a0-b1c2d3e4f5a6",
      "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
      "topicId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0"
    }
  ]
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "totalProcessed": 1,
    "message": "BULK_ASSIGNMENT_COMPLETED"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- BTC chọn event trước, sau đó nhập danh sách kết quả bốc thăm offline gồm team, track và topic tương ứng.
- Kiểm tra tính tồn tại của các ID gửi lên và đảm bảo tất cả `registerTeamId` thuộc đúng `eventId` trên path.
- Chỉ xử lý các đơn đăng ký đã được duyệt (`RegisterTeams.Status = Approved`) và chưa bị soft-disable.
- Đảm bảo các đề bài (`TopicId`) thuộc đúng bảng đấu (`TrackId`) gán tương ứng của team.
- Cập nhật trường `TrackId` và `TopicId` cho bản ghi `RegisterTeams` tương ứng, đồng thời khởi tạo `RoundDetails` cho round đầu tiên của event (`RoundNo = 1`) cho toàn bộ danh sách.
- Toàn bộ quá trình chạy hàng loạt bắt buộc bọc trong một **Database Transaction** để rollback nếu có bất kỳ dòng nào bị lỗi dữ liệu.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Bad Request",
  "Status": 400,
  "Detail": "Đề thi gán lên không thuộc bảng đấu đã chỉ định.",
  "MessageCode": "TOPIC_MUST_BELONG_TO_ASSIGNED_TRACK",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | TOPIC_MUST_BELONG_TO_ASSIGNED_TRACK | Có bản ghi gán đề bài lệch với bảng đấu gán kèm. |
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Staff chưa được phân công quản lý sự kiện. |
| 404 | REGISTER_TEAM_NOT_FOUND | Có đơn đăng ký gửi lên không tồn tại trong DB. |
| 500 | INTERNAL_SERVER_ERROR | Gặp sự cố không mong muốn tại server. |
