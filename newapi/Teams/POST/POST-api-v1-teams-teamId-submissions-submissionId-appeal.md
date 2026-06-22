# Team gửi khiếu nại/phúc khảo theo bài nộp (Submit Submission Appeal)

## Tác dụng
Cho phép Team Leader gửi khiếu nại/phúc khảo cho một bài nộp cụ thể sau khi bài đã có kết quả chấm. Đây vẫn là luồng report bình thường, nhưng request gắn trực tiếp `submissionId` để Staff/Admin xem đúng bài nộp và phân công judge khác chấm lại nếu cần.

## URL
`POST /api/v1/teams/{teamId}/submissions/{submissionId}/appeal`

## Quyền
Authenticated Team Leader

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team gửi khiếu nại.
    *   `submissionId` (Guid, Bắt buộc): ID bài nộp cần khiếu nại/phúc khảo.

## Request Body
```json
{
  "Title": "Yêu cầu phúc khảo bài nộp Vòng loại",
  "description": "Team muốn BTC xem lại điểm tiêu chí kỹ thuật của bài nộp này.",
  "imgUrl": "https://example.com/evidence.jpg",
  "fileUrl": "https://example.com/evidence.pdf"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "reportId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "message": "APPEAL_SUBMITTED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Người gọi phải là Leader của team.
- Submission phải thuộc team này thông qua `Submission -> RoundDetail -> RegisterTeam -> TeamId`.
- Chỉ cho gửi khiếu nại khi submission đã có kết quả chấm/công bố; nếu chưa có kết quả thì FE không hiển thị nút khiếu nại và API trả lỗi.
- Mỗi submission chỉ có một report/phúc khảo đang mở; nếu đã gửi rồi thì báo conflict.
- Tạo bản ghi `Reports` với `SubmissionId = submissionId`, `TypeReport = "Phúc khảo"`, `Status = Open`.
- Staff/Admin xem report này ở [`GET /api/v1/staff/reports/{reportId}`](./GET-api-v1-staff-reports-reportId.md), sau đó có thể phân công judge khác bằng [`POST /api/v1/staff/reports/{reportId}/assign-judge`](./POST-api-v1-staff-reports-reportId-assign-judge.md).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

```json
{
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Bài nộp này đã có khiếu nại phúc khảo đang xử lý.",
  "MessageCode": "APPEAL_ALREADY_SUBMITTED_FOR_SUBMISSION",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | SUBMISSION_NOT_GRADED | Bài nộp chưa có kết quả chấm nên chưa thể khiếu nại. |
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | ONLY_TEAM_LEADER_CAN_APPEAL | Chỉ trưởng nhóm mới được gửi khiếu nại. |
| 403 | FORBIDDEN | Submission không thuộc team này. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 404 | SUBMISSION_NOT_FOUND | Submission không tồn tại. |
| 409 | APPEAL_ALREADY_SUBMITTED_FOR_SUBMISSION | Submission đã có khiếu nại đang xử lý. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống phát sinh. |
