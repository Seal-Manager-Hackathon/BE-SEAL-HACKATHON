# Trưởng nhóm nộp đơn phúc khảo theo Round (Legacy Round Appeal)

## Tác dụng
API cũ mô tả gửi phúc khảo theo `roundId`. Luồng hiện tại nên dùng API phúc khảo theo `submissionId`: [`POST /api/v1/teams/{teamId}/submissions/{submissionId}/appeal`](./POST-api-v1-teams-teamId-submissions-submissionId-appeal.md), vì Staff/Admin cần biết chính xác bài nộp nào để xem và phân công judge khác chấm lại.

## URL
`POST /api/v1/teams/{teamId}/rounds/{roundId}/appeal`

## Quyền
Student Leader (Yêu cầu đăng nhập tài khoản Trưởng nhóm)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team.
    *   `roundId` (Guid, Bắt buộc): ID của vòng đấu muốn khiếu nại.

## Request Body
```json
{
  "Title": "Yêu cầu phúc khảo điểm Vòng loại",
  "description": "Chúng em tin rằng điểm chấm tiêu chí Kỹ thuật bị nhầm lẫn.",
  "imgUrl": "https://example.com/evidence.jpg",
  "fileUrl": "https://example.com/evidence.pdf"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa ID của report.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "reportId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "message": "APPEAL_SUBMITTED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Người gọi phải là Leader của team.
- Team phải có bài nộp thi đấu trong round đó (`Submissions` tồn tại liên kết với `RoundDetails`).
- Giới hạn mỗi team chỉ được gửi phúc khảo tối đa 1 lần duy nhất cho mỗi round đấu (check BR-REP-04, nếu đã có báo cáo phúc khảo cùng `RoundDetailId` trong DB thì từ chối gửi thêm và báo lỗi `APPEAL_ALREADY_SUBMITTED_FOR_ROUND`).
- Tạo bản ghi mới trong bảng `Reports` với `TypeReport = "Phúc khảo"` và trạng thái mặc định là `Open`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Đội thi đã nộp đơn phúc khảo cho vòng đấu này trước đó.",
  "MessageCode": "APPEAL_ALREADY_SUBMITTED_FOR_ROUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | ONLY_TEAM_LEADER_CAN_APPEAL | Chỉ trưởng nhóm mới được gửi đơn khiếu nại điểm. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 404 | SUBMISSION_NOT_FOUND | Đội thi chưa nộp bài thi nào ở vòng đấu này. |
| 409 | APPEAL_ALREADY_SUBMITTED_FOR_ROUND | Đội đã phúc khảo vòng này rồi, giới hạn 1 lần. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
