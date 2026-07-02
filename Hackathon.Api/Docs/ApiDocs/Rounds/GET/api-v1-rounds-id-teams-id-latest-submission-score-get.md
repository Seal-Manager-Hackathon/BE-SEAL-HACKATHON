# Check latest submission score of a team in a round

## Tác dụng
Kiểm tra submission mới nhất của một team trong một round đã được chấm hay chưa, và nếu đã chấm thì trả về điểm tổng cùng breakdown theo criteria.

## URL
`GET /api/v1/rounds/{roundId}/teams/{teamId}/latest-submission-score`

## Quyền
Public API

## Request Parameters
*   **Path Parameters:**
    *   `roundId` (Guid, bắt buộc): ID của round cần kiểm tra.
    *   `teamId` (Guid, bắt buộc): ID của team cần kiểm tra.

## Response body (Success - 200 OK - đã có điểm)
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-07-02T19:06:11.9578018Z",
  "Message": "Graded",
  "Data": {
    "roundId": "21000000-0000-0000-0000-000000000019",
    "roundName": "Robotics Control Pitch 2026",
    "teamId": "6e913cda-fa56-4a35-bbc8-9eeb7d13d505",
    "teamName": "za",
    "submissionId": "8c2ad9d2-aa7c-4957-bfd3-47288124c9da",
    "submittedAt": "2026-06-30T22:43:43.428116+00:00",
    "hasSubmission": true,
    "isGraded": true,
    "gradingStatus": "Graded",
    "message": null,
    "averageTotalScore": 1.5,
    "criteriaScores": [
      {
        "criteriaItemId": "f8f81513-fd18-49b3-a323-672bce63881d",
        "criteriaItemName": "1",
        "averageCriteriaScore": 1,
        "maxScore": 1
      },
      {
        "criteriaItemId": "65d79a02-578e-4128-ae19-816542bf2273",
        "criteriaItemName": "1",
        "averageCriteriaScore": 0.5,
        "maxScore": 1
      }
    ]
  }
}
```

## Response body (Success - 200 OK - chưa có submission)
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-07-02T19:06:11.9578018Z",
  "Message": "NO_SUBMISSION",
  "Data": {
    "roundId": "21000000-0000-0000-0000-000000000019",
    "roundName": "Robotics Control Pitch 2026",
    "teamId": "6e913cda-fa56-4a35-bbc8-9eeb7d13d505",
    "teamName": "za",
    "submissionId": null,
    "submittedAt": null,
    "hasSubmission": false,
    "isGraded": false,
    "gradingStatus": "NoSubmission",
    "message": "NO_SUBMISSION",
    "averageTotalScore": null,
    "criteriaScores": []
  }
}
```

## Response body (Success - 200 OK - đã nộp nhưng chưa được chấm)
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-07-02T19:06:11.9578018Z",
  "Message": "NOT_GRADED",
  "Data": {
    "roundId": "21000000-0000-0000-0000-000000000019",
    "roundName": "Robotics Control Pitch 2026",
    "teamId": "6e913cda-fa56-4a35-bbc8-9eeb7d13d505",
    "teamName": "za",
    "submissionId": "8c2ad9d2-aa7c-4957-bfd3-47288124c9da",
    "submittedAt": "2026-06-30T22:43:43.428116+00:00",
    "hasSubmission": true,
    "isGraded": false,
    "gradingStatus": "NotGraded",
    "message": "NOT_GRADED",
    "averageTotalScore": null,
    "criteriaScores": []
  }
}
```

## Business rules
- Hệ thống lấy submission mới nhất của team trong round.
- Chỉ tính các score hiện có, không suy diễn judge vắng mặt.
- Với mỗi judge, chỉ lấy score mới nhất theo `AssignTrackId`.
- Điểm từng criteria = trung bình các score hợp lệ của criteria đó trên các judge đã chấm.
- `averageTotalScore` = tổng các `averageCriteriaScore`.
- Nếu chưa có submission thì trả `hasSubmission = false`.
- Nếu có submission nhưng chưa có score hợp lệ thì trả `isGraded = false`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | ROUND_NOT_FOUND | Round không tồn tại hoặc bị disable. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại hoặc bị disable. |
| 404 | ROUND_DETAIL_NOT_FOUND | Team không tham gia round này. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
