# Create submission appeal

## Tác dụng
Đội thi nộp đơn khiếu nại phúc khảo điểm số sau khi xem kết quả công bố lần 1.

## URL
`POST /api/submissions/{submissionId}/appeals`

## Authorization
Yêu cầu access token hợp lệ.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `submissionId` | `guid` | Có | Id bài nộp cần khiếu nại/phúc khảo. |

## Request body
```json
{
  "title": "string",
  "description": "string"
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
    "reportId": "guid",
    "submissionId": "guid",
    "userId": "guid",
    "title": "string",
    "description": "string",
    "status": "Open",
    "message": "APPEAL_CREATED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- User hiện tại phải là leader của team sở hữu submission.
- Submission phải tồn tại và chưa bị soft-disable.
- Phúc khảo dùng bảng `Reports`; không có bảng `Appeals` riêng.
- Chỉ cho nộp khiếu nại sau khi điểm/kết quả đã được công bố theo rule của round/event.
- Không cho tạo nhiều appeal đang `Open` cho cùng một submission nếu service áp dụng rule chống trùng.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | ONLY_TEAM_LEADER_CAN_APPEAL |
| 404 | NOT_FOUND | SUBMISSION_NOT_FOUND |
| 400 | BAD_REQUEST | APPEAL_TITLE_REQUIRED |
| 400 | BAD_REQUEST | APPEAL_DESCRIPTION_REQUIRED |
| 400 | BAD_REQUEST | APPEAL_WINDOW_NOT_OPEN |
| 409 | CONFLICT | APPEAL_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
