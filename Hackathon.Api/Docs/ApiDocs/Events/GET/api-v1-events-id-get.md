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
*Cấu trúc trả về dạng `BaseResponse`:*
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
    "id": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
    "name": "SEAL Hackathon 2026",
    "description": "Cuộc thi lập trình SEAL Hackathon mùa hè 2026.",
    "startTime": "2026-07-01T08:00:00Z",
    "endTime": "2026-07-10T17:00:00Z",
    "registerLimitTime": "2026-06-30T23:59:59Z",
    "limitTeam": 50,
    "minMember": 3,
    "maxMember": 5,
    "status": 0, /* 0: Draft, 1: Published, 2: Closed, 3: Cancelled */
    "numberRound": 3,
    "season": "Mùa hè 2026",
    "isDisable": false,
    "createdAt": "2026-06-22T08:00:00Z"
  }
}
```

## Business rules
- Không yêu cầu đăng nhập.
- Tìm kiếm event theo `eventId` chính xác.
- Trả về đầy đủ tất cả các trường thông tin của event (bao gồm các trường cấu hình như mô tả, thời gian đăng ký tối hạn, giới hạn số lượng team, số thành viên tối đa/tối thiểu của team, số vòng đấu, v.v.).
- Nếu không tìm thấy event có `eventId` tương ứng, trả về lỗi `404 Not Found` (`EVENT_NOT_FOUND`).

### Bảng trạng thái EventStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Draft | Sự kiện đang nháp, chưa công bố |
| `1` | Published | Sự kiện đã công bố và hoạt động |
| `2` | Closed | Sự kiện đã kết thúc và đóng lại |
| `3` | Cancelled | Sự kiện đã bị hủy bỏ |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
