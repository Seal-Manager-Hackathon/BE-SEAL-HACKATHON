# API 48: BTC gán đề bài cho Team (Staff Assign Topic)

## Tác dụng
Cho phép Staff gán đề bài thi (Topic) cho team sau khi BTC chọn event, chọn team và đã có kết quả bốc thăm offline.

## URL
`PATCH /api/v1/staff/teams/{teamId}/topic`

## Quyền
Staff (Yêu cầu đăng nhập tài khoản Staff; route hiện tại dùng `StaffPolicy` trong controller)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team.

## Request Body
```json
{
  "topicId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa kết quả.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "topicId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "message": "TOPIC_ASSIGNED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- BTC chọn event trước, sau đó chọn team đã `Approved` trong event để nhập kết quả bốc thăm offline.
- Team thi đấu và Topic gán phải tồn tại trong DB, không bị soft-disable và thuộc đúng event.
- Team phải được gán bảng đấu (`TrackId`) tương ứng trước khi gán đề bài (`TopicId`), và Topic này phải thuộc về Track đó.
- Cập nhật trường `TopicId` của bản ghi trong bảng `RegisterTeams`.
- Khi gán đề bài thành công, hệ thống tự động đưa team này vào vòng thi đầu tiên của sự kiện bằng cách tạo bản ghi trong bảng `RoundDetails` liên kết `RegisterTeamId` với `RoundId` của `RoundNo = 1` (BR-TRACK-05).
- Hành động cập nhật Topic và tạo RoundDetail Round 1 phải bọc chung trong một **Database Transaction**.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Đội thi chưa được phân vào bảng đấu (track) trước đó.",
  "MessageCode": "TRACK_MUST_BE_ASSIGNED_FIRST",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Không được gán quyền quản lý sự kiện. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 404 | TOPIC_NOT_FOUND | Đề thi không tồn tại trong hệ thống. |
| 409 | TRACK_MUST_BE_ASSIGNED_FIRST | Phải gán track trước khi gán topic. |
| 409 | TOPIC_MUST_BELONG_TO_ASSIGNED_TRACK | Đề thi gán lên không thuộc bảng đấu đã chia của team. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
