# Create submission

## Tác dụng
Đội thi tiến hành nộp link sản phẩm hoặc mã nguồn bài làm cho Vòng thi hiện tại.

## URL
`POST /api/submissions`

## Authorization
Yêu cầu access token hợp lệ.

## Request body
```json
{
  "roundDetailId": "guid",
  "title": "string",
  "description": "string|null",
  "sourceUrl": "string|null",
  "demoUrl": "string|null",
  "fileUrl": "string|null"
}
```

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "id": "guid",
    "roundDetailId": "guid",
    "teamId": "guid",
    "title": "string",
    "description": "string|null",
    "sourceUrl": "string|null",
    "demoUrl": "string|null",
    "fileUrl": "string|null",
    "status": "Submitted",
    "submittedAt": "datetimeoffset",
    "message": "SUBMISSION_CREATED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- User hiện tại phải là leader của team thuộc `RoundDetails`.
- Team phải có đơn đăng ký đã được duyệt trong event.
- Round hiện tại phải đang trong thời gian cho phép nộp bài.
- Bài nộp phải có ít nhất một link sản phẩm, source code hoặc file.
- Có thể lưu nhiều submission; khi chấm nên dùng submission hợp lệ/latest theo rule của round.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | ONLY_TEAM_LEADER_CAN_SUBMIT |
| 404 | NOT_FOUND | ROUND_DETAIL_NOT_FOUND |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 400 | BAD_REQUEST | SUBMISSION_CONTENT_REQUIRED |
| 400 | BAD_REQUEST | SUBMISSION_WINDOW_NOT_OPEN |
| 400 | BAD_REQUEST | SUBMISSION_WINDOW_CLOSED |
| 409 | CONFLICT | TEAM_NOT_APPROVED_FOR_EVENT |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
