# Admin kết thúc round ngay lập tức (Admin End Round Final)

## Tác dụng
Set `EndTime` và `EndSubmission` của round thành thời điểm hiện tại. Dùng để test — kết thúc round sớm để có thể xem submissions qua API Lecturer.

## URL
`POST /api/v1/rounds/{roundId}/endFinal`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `roundId` | `guid` | Có | ID của round cần kết thúc. |

## Request body
Không có.

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": null,
  "message": "ROUND_ENDED_IMMEDIATELY"
}
```

## Business rules
- Round phải tồn tại.
- Set `EndTime = now`, nếu `EndSubmission > now` thì set `EndSubmission = now`.
- Không cần check event đã bắt đầu hay chưa — force kết thúc ngay.
- Không ảnh hưởng đến vòng sau (không advance team).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
