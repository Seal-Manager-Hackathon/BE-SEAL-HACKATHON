# Submit submission appeal

## Tác dụng
Cho phép Team Leader gửi khiếu nại/phúc khảo cho một bài nộp cụ thể sau khi bài đã có kết quả chấm. Đây vẫn là luồng report bình thường, nhưng request gắn trực tiếp `submissionId` để Staff/Admin xem đúng bài nộp và duyệt chấm lại nếu cần.

## URL
`POST /api/v1/teams/{teamId}/submissions/{submissionId}/appeal`

## Authorization
Yêu cầu access token hợp lệ với role `Student` và phải là Leader của team.

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team gửi khiếu nại.
    *   `submissionId` (Guid, Bắt buộc): ID bài nộp cần khiếu nại/phúc khảo.

## Request Body
```json
{
  "title": "Yêu cầu phúc khảo bài nộp Vòng loại",
  "description": "Team muốn BTC xem lại điểm tiêu chí kỹ thuật của bài nộp này.",
  "imgUrl": "https://example.com/evidence.jpg",
  "fileUrl": "https://example.com/evidence.pdf"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
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
    "reportId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff"
  }
}
```

## Business rules
- Người gọi phải là Leader của team.
- Submission phải thuộc team này thông qua `Submission -> RoundDetail -> RegisterTeam -> TeamId`.
- Chỉ cho gửi khiếu nại khi submission đã có kết quả chấm/công bố; nếu chưa có kết quả thì FE không hiển thị nút khiếu nại và API trả lỗi.
- Mỗi submission chỉ có một report/phúc khảo đang mở; nếu đã gửi rồi thì báo conflict.
- Tạo bản ghi `Reports` với `SubmissionId = submissionId`, `TypeReport = "Phúc khảo"`, và `Status = 0` (Open) của `ReportStatusEnum`.
- Staff/Admin xem report này ở `GET /api/v1/staff/reports/{reportId}`; nếu duyệt phúc khảo thì gọi `POST /api/v1/staff/reports/{reportId}/regrade`. Judge được chấm lại là judge đã chấm score gốc của submission.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Conflict",
  "status": 409,
  "message": "APPEAL_ALREADY_SUBMITTED_FOR_SUBMISSION",
  "messageCode": "CONFLICT",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | SUBMISSION_NOT_GRADED |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT |
| 403 | FORBIDDEN | ONLY_TEAM_LEADER_CAN_APPEAL |
| 403 | FORBIDDEN | SUBMISSION_NOT_BELONG_TO_TEAM |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 404 | NOT_FOUND | SUBMISSION_NOT_FOUND |
| 409 | CONFLICT | APPEAL_ALREADY_SUBMITTED_FOR_SUBMISSION |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
