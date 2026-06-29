# Legacy round appeal

## Tác dụng
API cũ mô tả gửi phúc khảo theo `roundId`. Luồng hiện tại nên dùng API phúc khảo theo `submissionId`: `POST /api/v1/teams/{teamId}/submissions/{submissionId}/appeal`, vì Staff/Admin cần biết chính xác bài nộp nào để xem và duyệt regrade.

## URL
`POST /api/v1/teams/{teamId}/rounds/{roundId}/appeal`

## Authorization
Yêu cầu access token hợp lệ với role `Student` và phải là Leader của team.

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team.
    *   `roundId` (Guid, Bắt buộc): ID của vòng đấu muốn khiếu nại.

## Request Body
```json
{
  "title": "Yêu cầu phúc khảo điểm Vòng loại",
  "description": "Chúng em tin rằng điểm chấm tiêu chí Kỹ thuật bị nhầm lẫn.",
  "imgUrl": "https://example.com/evidence.jpg",
  "fileUrl": "https://example.com/evidence.pdf"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa ID của report.*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "APPEAL_SUBMITTED_SUCCESSFULLY",
  "data": {
    "reportId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0"
  }
}
```

## Business rules
- Người gọi phải là Leader của team.
- Team phải có bài nộp thi đấu trong round đó (`Submissions` tồn tại liên kết với `RoundDetails`).
- Giới hạn mỗi team chỉ được gửi phúc khảo tối đa 1 lần duy nhất cho mỗi round đấu (nếu đã có báo cáo phúc khảo cùng `RoundDetailId` trong DB thì từ chối gửi thêm và báo lỗi `APPEAL_ALREADY_SUBMITTED_FOR_ROUND`).
- Tạo bản ghi mới trong bảng `Reports` với `TypeReport = "Phúc khảo"` và trạng thái mặc định là `0` (Open) của `ReportStatusEnum`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Conflict",
  "status": 409,
  "message": "APPEAL_ALREADY_SUBMITTED_FOR_ROUND",
  "messageCode": "CONFLICT",
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
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT |
| 403 | FORBIDDEN | ONLY_TEAM_LEADER_CAN_APPEAL |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 404 | NOT_FOUND | SUBMISSION_NOT_FOUND |
| 409 | CONFLICT | APPEAL_ALREADY_SUBMITTED_FOR_ROUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
