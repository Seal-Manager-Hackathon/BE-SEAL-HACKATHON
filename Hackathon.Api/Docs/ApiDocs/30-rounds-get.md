# Get rounds

## Tác dụng
Lấy danh sách round, có thể lọc theo event.

## URL
`GET /api/v1/rounds`

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Không | Lọc round thuộc một event cụ thể. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable. Nếu không truyền, mặc định chỉ trả round chưa bị disable (`IsDisable = false`). |

## Ví dụ request
```http
GET /api/v1/rounds?eventId=00000000-0000-0000-0000-000000000000&isDisable=false
```

## Request body
Không có.

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": [
    {
      "id": "guid",
      "eventId": "guid",
      "name": "string",
      "description": "string|null",
      "startTime": "datetimeoffset|null",
      "endTime": "datetimeoffset|null",
      "startSubmission": "datetimeoffset|null",
      "endSubmission": "datetimeoffset|null",
      "limitTeam": 0,
      "isDisable": false,
      "createdAt": "datetimeoffset"
    }
  ]
}
```

## Business rules
- Nếu truyền `eventId`, chỉ trả round thuộc event đó.
- Nếu không truyền `isDisable`, mặc định chỉ trả round chưa bị soft-disable.
- Round thuộc event bị disable không nên được trả về, trừ khi nghiệp vụ cho phép lấy dữ liệu disable bằng query riêng.
- Kết quả sắp xếp theo `StartTime` tăng dần, sau đó `CreatedAt` tăng dần.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Query parameter không hợp lệ. |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
