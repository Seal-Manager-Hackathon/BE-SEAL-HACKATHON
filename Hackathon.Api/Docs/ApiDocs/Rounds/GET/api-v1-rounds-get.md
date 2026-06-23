# Get rounds

## Tác dụng
Lấy danh sách round, lọc theo event.

## URL
`GET /api/v1/rounds`

## Authorization
Không yêu cầu Access Token (Public API).

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `eventId` | `guid` | Có | Lọc round thuộc một event cụ thể. |

## Ví dụ request
```http
GET /api/v1/rounds?eventId=00000000-0000-0000-0000-000000000000
```

## Request body
Không có.

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string",
  "timestampUtc": "datetime",
  "data": [
    {
      "id": "guid",
      "eventId": "guid",
      "name": "string",
      "description": "string|null",
      "roundNo": 0,
      "startTime": "datetimeoffset|null",
      "endTime": "datetimeoffset|null",
      "startSubmission": "datetimeoffset|null",
      "endSubmission": "datetimeoffset|null",
      "limitTeam": 0,
      "isDisable": false,
      "createdAt": "datetimeoffset"
    }
  ],
  "message": "SUCCESS"
}
```

## Business rules
- `eventId` là bắt buộc, event phải tồn tại và không bị disable.
- Chỉ trả về các round thuộc event đó, chưa bị disable.
- Kết quả sắp xếp theo `RoundNo` tăng dần, sau đó `CreatedAt` tăng dần.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
