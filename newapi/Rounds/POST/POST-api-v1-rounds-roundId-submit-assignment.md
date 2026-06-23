# API 55: Nộp bài thi (Student Submit Assignment)

## Tác dụng
Cho phép Trưởng nhóm (Leader) của team nộp bài thi (URL bài làm, mô tả chi tiết sản phẩm) cho vòng thi hiện tại.

## URL
`POST /api/v1/rounds/{roundId}/submit-assignment`

## Quyền
Student Leader (Yêu cầu đăng nhập tài khoản Trưởng nhóm)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `roundId` (Guid, Bắt buộc): ID của vòng thi cần nộp bài.

## Request Body
```json
{
  "url": "https://github.com/seal-hackathon/team-project-web",
  "description": "Sản phẩm Web App quản lý y tế hoàn chỉnh, có video demo đính kèm."
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa thông tin bài nộp.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Value": {
    "id": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "roundDetailId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "url": "https://github.com/seal-hackathon/team-project-web",
    "description": "Sản phẩm Web App quản lý y tế hoàn chỉnh, có video demo đính kèm.",
    "Status": 0, /* Submitted */
    "submittedAt": "2026-06-22T08:00:00Z"
  }
}
```

## Business rules
- Người nộp bài bắt buộc phải là Leader của team (`IsLeader = true` và `Status = Active` trong `TeamDetails`).
- Team phải đang được phép thi đấu trong vòng thi này (phải tồn tại bản ghi trong bảng `RoundDetails` liên kết `RoundId` và `RegisterTeamId` của team).
- Thời điểm nộp bài phải nằm trong khoảng thời gian nộp bài cho phép của vòng (`StartSubmission` <= Hiện tại <= `EndSubmission`, check BR-SUB-03). Nếu quá hạn, cổng nộp bài tự động khóa trừ khi được BTC mở khóa thủ công.
- API hỗ trợ nộp bài nhiều lần trước deadline (BR-SUB-04): mỗi lần nộp sẽ tạo một bản ghi mới trong bảng `Submissions` liên kết với `RoundDetailId`. Hệ thống ghi nhận lịch sử và sẽ sử dụng bản nộp mới nhất để chấm điểm (BR-SUB-05).

### Bảng trạng thái SubmissionStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Submitted | Đã nộp bài thi thành công |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Bad Request",
  "Status": 400,
  "Detail": "Thời gian nộp bài của vòng thi này đã kết thúc.",
  "MessageCode": "EVENT_REGISTRATION_CLOSED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | EVENT_REGISTRATION_CLOSED | Hạn nộp bài thi của vòng này đã đóng (qua deadline). |
| 400 | URL_REQUIRED | Link bài làm thi đấu bắt buộc phải có. |
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | ONLY_TEAM_LEADER_CAN_SUBMIT | Chỉ nhóm trưởng mới có quyền nộp bài thi. |
| 404 | ROUND_NOT_FOUND | Vòng thi không tồn tại. |
| 404 | ROUND_DETAIL_NOT_FOUND | Team không nằm trong danh sách thi đấu của vòng này. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
