# Team xem điểm của mình trong Round (Get My Round Score)

## Tác dụng
Cho phép team xem kết quả/điểm của chính team trong một round. Nếu bài nộp chưa được chấm hoặc điểm chưa được công bố, FE hiển thị trạng thái "Bài chưa được chấm".

## URL
`GET /api/v1/rounds/{roundId}/scores/me`

## Quyền
Authenticated Team Member

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `roundId` (Guid, Bắt buộc): ID của round cần xem điểm.

## Response body (Success - 200 OK - đã có điểm)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "roundName": "Vòng loại",
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "teamName": "Chiến binh công nghệ",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "gradingStatus": "Graded",
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
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Response body (Success - 200 OK - chưa được chấm)
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "gradingStatus": "NotGraded",
    "message": "Bài chưa được chấm",
    "averageTotalScore": null,
    "criteriaScores": []
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- User phải thuộc team đang xem điểm.
- Team phải có `RoundDetails` trong round này.
- Hệ thống lấy submission mới nhất của team trong round để xem kết quả.
- Nếu submission chưa có điểm hoặc điểm chưa được công bố, trả trạng thái `NotGraded` và message "Bài chưa được chấm".
- Khi đã có điểm và còn trong thời gian/điều kiện phúc khảo, `isAppealable = true` để FE hiển thị nút khiếu nại.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy bài nộp của đội trong vòng này.",
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
| 403 | FORBIDDEN | User không thuộc team này. |
| 404 | ROUND_NOT_FOUND | Round không tồn tại. |
| 404 | ROUND_DETAIL_NOT_FOUND | Team không tham gia round này. |
| 404 | SUBMISSION_NOT_FOUND | Team chưa nộp bài trong round này. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống phát sinh. |
