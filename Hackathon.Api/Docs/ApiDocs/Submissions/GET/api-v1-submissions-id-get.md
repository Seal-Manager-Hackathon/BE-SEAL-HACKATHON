# Get submission detail

## Tác dụng
Xem chi tiết một bài nộp, bao gồm URL, mô tả, trạng thái chấm điểm và điểm/kết quả nếu đã được chấm/công bố. FE dùng API này khi user bấm vào chi tiết bài nộp trong lịch sử bài nộp của round.

## URL
`GET /api/v1/submissions/{submissionId}`

## Authorization
Yêu cầu access token hợp lệ của Team member/Staff/Admin/Judge assigned.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `submissionId` | `guid` | Có | ID của bài nộp cần xem. |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/submissions/f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*

### Trường hợp đã có kết quả chấm
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Message": "SUCCESS",
  "Data": {
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "roundDetailId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "roundName": "Vòng loại",
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "teamName": "Chiến binh công nghệ",
    "url": "https://github.com/seal-hackathon/team-project-web",
    "description": "Bài thi hoàn chỉnh.",
    "status": "Submitted",
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
  }
}
```

### Trường hợp chưa có kết quả chấm
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Message": "NOT_GRADED",
  "Data": {
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "roundDetailId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "roundName": "Vòng loại",
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "teamName": "Chiến binh công nghệ",
    "url": "https://github.com/seal-hackathon/team-project-web",
    "description": "Bài thi hoàn chỉnh.",
    "status": "Submitted",
    "submittedAt": "2026-06-22T08:00:00Z",
    "gradingStatus": "NotGraded",
    "message": "NOT_GRADED",
    "score": null
  }
}
```

## Business rules
- Submission phải tồn tại và chưa bị disable.
- Team member chỉ được xem submission của team mình; Judge chỉ được xem submission thuộc track được phân công; Staff/Admin xem theo quyền event.
- Nếu chưa có score hoặc điểm chưa được công bố, trả `gradingStatus = "NotGraded"`, `score = null`, `message = "NOT_GRADED"`.
- Nếu đã có kết quả, trả điểm tổng trung bình và điểm theo tiêu chí. Khi `isAppealable = true`, FE hiển thị nút khiếu nại/phúc khảo.
- Nút khiếu nại dùng [`POST /api/v1/teams/{teamId}/submissions/{submissionId}/appeal`](../Report/POST-api-v1-teams-teamId-submissions-submissionId-appeal.md) để tạo report gắn trực tiếp với `submissionId`.

### Bảng trạng thái SubmissionStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Submitted | Đã nộp bài thi thành công |
| `1` | Unsubmitted | Chưa nộp bài (hoặc đã hủy nộp) |
| `2` | Failed | Nộp bài thất bại (lỗi hệ thống/vi phạm) |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | SUBMISSION_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
