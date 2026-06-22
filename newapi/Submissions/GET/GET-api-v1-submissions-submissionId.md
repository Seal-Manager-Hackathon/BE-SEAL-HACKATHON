# Xem chi tiết bài nộp (Get Submission Detail)

## Tác dụng
Xem chi tiết một bài nộp, bao gồm URL, mô tả, trạng thái chấm điểm và điểm/kết quả nếu đã được chấm/công bố. FE dùng API này khi user bấm vào chi tiết bài nộp trong lịch sử bài nộp của round.

## URL
`GET /api/v1/submissions/{submissionId}`

## Quyền
Authenticated User (Team owner/Staff/Admin/Judge assigned)

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `submissionId` (Guid, Bắt buộc): ID của bài nộp cần xem.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "roundDetailId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "roundName": "Vòng loại",
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "teamName": "Chiến binh công nghệ",
    "url": "https://github.com/seal-hackathon/team-project-web",
    "description": "Bài thi hoàn chỉnh.",
    "Status": 0,
    "submittedAt": "2026-06-22T08:00:00Z",
    "gradingStatus": "Graded",
    "message": null,
    "score": {
      "averageTotalScore": 88.5,
      "isAppealable": true,
      "criteriaScores": [
        {
          "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
          "criteriaItemName": "Tính thực tiễn",
          "averageCriteriaScore": 27.5,
          "maxScore": 30.0
        }
      ]
    }
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Trường hợp chưa có kết quả chấm
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "url": "https://github.com/seal-hackathon/team-project-web",
    "description": "Bài thi hoàn chỉnh.",
    "submittedAt": "2026-06-22T08:00:00Z",
    "gradingStatus": "NotGraded",
    "message": "Bài chưa được chấm",
    "score": null
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Submission phải tồn tại và chưa bị disable.
- Team member chỉ được xem submission của team mình; Judge chỉ được xem submission thuộc track được phân công; Staff/Admin xem theo quyền event.
- Nếu chưa có score hoặc điểm chưa được công bố, trả `gradingStatus = "NotGraded"`, `score = null`, `message = "Bài chưa được chấm"`.
- Nếu đã có kết quả, trả điểm tổng trung bình và điểm theo tiêu chí. Khi `isAppealable = true`, FE hiển thị nút khiếu nại/phúc khảo.
- Nút khiếu nại dùng [`POST /api/v1/teams/{teamId}/submissions/{submissionId}/appeal`](../Report/POST-api-v1-teams-teamId-submissions-submissionId-appeal.md) để tạo report gắn trực tiếp với `submissionId`.

### Bảng trạng thái SubmissionStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Submitted | Đã nộp bài thi thành công |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy bài nộp.",
  "MessageCode": "SUBMISSION_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | User không có quyền xem bài nộp này. |
| 404 | SUBMISSION_NOT_FOUND | Bài nộp không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống phát sinh. |
