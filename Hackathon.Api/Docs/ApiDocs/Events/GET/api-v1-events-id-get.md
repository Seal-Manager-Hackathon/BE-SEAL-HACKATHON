# Get Event Detail

## Tác dụng
Lấy thông tin chi tiết đầy đủ của một event theo `eventId` (phục vụ khi học sinh hoặc admin bấm vào xem chi tiết event).

## URL
`GET /api/v1/events/{eventId:guid}`

## Request Parameters
*   **Route Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của event cần xem chi tiết.

## Request Headers
Không yêu cầu Access Token (Public API).

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "value": {
    "id": "guid",
    "name": "string",
    "description": "string|null",
    "startTime": "datetime|null",
    "endTime": "datetime|null",
    "registerLimitTime": "datetime|null",
    "limitTeam": 0,
    "minMember": 0,
    "maxMember": 0,
    "status": 0, /* Draft */
    "numberRound": 0,
    "season": "string|null",
    "isDisable": false,
    "createdAt": "datetime"
  }
}
```

## Business rules
- Không yêu cầu đăng nhập.
- Tìm kiếm event theo `eventId` chính xác.
- Trả về đầy đủ tất cả các trường thông tin của event (bao gồm các trường cấu hình như mô tả, thời gian đăng ký tối hạn, giới hạn số lượng team, số thành viên tối đa/tối thiểu của team, số vòng đấu, v.v.).
- Nếu không tìm thấy event có `eventId` tương ứng, trả về lỗi `404 Not Found` (`EVENT_NOT_FOUND`).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
