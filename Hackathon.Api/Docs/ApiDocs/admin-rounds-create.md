# Admin create rounds

## Tác dụng
Admin thiết lập các Vòng thi (Round) cho cuộc thi Hackathon.

## URL
`POST /api/admin/events/{eventId}/rounds`

## Authorization
Yêu cầu access token hợp lệ và role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id event cần tạo round. |

## Request body
```json
{
  "name": "string",
  "description": "string|null",
  "startTime": "datetimeoffset|null",
  "endTime": "datetimeoffset|null",
  "submissionStartTime": "datetimeoffset|null",
  "submissionEndTime": "datetimeoffset|null",
  "roundNumber": 1
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
    "eventId": "guid",
    "name": "string",
    "description": "string|null",
    "startTime": "datetimeoffset|null",
    "endTime": "datetimeoffset|null",
    "submissionStartTime": "datetimeoffset|null",
    "submissionEndTime": "datetimeoffset|null",
    "roundNumber": 1,
    "message": "ROUND_CREATED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ Admin được tạo round.
- Event phải tồn tại và chưa bị soft-disable.
- Tên round là bắt buộc.
- `roundNumber` phải là số dương và không được trùng trong cùng event.
- `startTime` phải trước `endTime` nếu cả hai được truyền.
- `submissionStartTime` phải trước `submissionEndTime` nếu cả hai được truyền.
- Thời gian nộp bài nên nằm trong thời gian diễn ra round nếu service áp dụng rule này.
- Số lượng round không nên vượt quá `Events.NumberRound` nếu event đã cấu hình.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | ADMIN_REQUIRED |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 400 | BAD_REQUEST | ROUND_NAME_REQUIRED |
| 400 | BAD_REQUEST | INVALID_ROUND_NUMBER |
| 400 | BAD_REQUEST | INVALID_ROUND_TIME_RANGE |
| 400 | BAD_REQUEST | INVALID_SUBMISSION_TIME_RANGE |
| 409 | CONFLICT | ROUND_NUMBER_ALREADY_EXISTS |
| 409 | CONFLICT | ROUND_LIMIT_EXCEEDED |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
