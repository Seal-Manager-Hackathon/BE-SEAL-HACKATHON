# Xem chi tiết vòng thi (Get Round Detail)

## Tác dụng
Xem thông tin chi tiết của một vòng thi (Round), bao gồm timeline, thời gian nộp bài và thông tin event liên quan. FE dùng API này khi thí sinh bấm vào chi tiết một round trong trang event.

## URL
`GET /api/v1/rounds/{roundId}`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Path Parameters:**
    *   `roundId` (Guid, Bắt buộc): ID của vòng thi cần xem chi tiết.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "id": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
    "eventName": "SEAL Hackathon 2026",
    "name": "Vòng loại",
    "description": "Nộp sản phẩm đề tài tự chọn.",
    "roundNo": 1,
    "startTime": "2026-07-01T08:00:00Z",
    "endTime": "2026-07-01T18:00:00Z",
    "startSubmission": "2026-07-01T08:00:00Z",
    "endSubmission": "2026-07-01T17:30:00Z",
    "limitTeam": 50,
    "isDisable": false
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Round phải tồn tại trong DB và chưa bị soft-disable.
- Round thuộc một event cụ thể (`Rounds.EventId`). FE có thể dùng `eventId` trong response để lấy tiếp leaderboard của event hoặc các dữ liệu event-level khác.
- Nếu cần hiển thị tiêu chí chấm điểm của round, gọi tiếp [`GET /api/v1/rounds/{roundId}/criteria`](../Criteria/58-GET-api-v1-rounds-roundId-criteria.md).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy vòng thi đấu.",
  "MessageCode": "ROUND_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | `roundId` sai định dạng GUID. |
| 404 | ROUND_NOT_FOUND | Vòng thi không tồn tại hoặc đã bị disable. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
