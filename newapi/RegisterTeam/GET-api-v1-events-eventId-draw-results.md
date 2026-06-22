# Xem kết quả bốc thăm (Get Event Draw Results)

## Tác dụng
Xem kết quả bốc thăm chia bảng đấu và đề bài thi đấu đã công bố của sự kiện.

## URL
`GET /api/v1/events/{eventId}/draw-results`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của sự kiện.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa danh sách kết quả bốc thăm.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": [
    {
      "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
      "teamName": "Chiến binh công nghệ",
      "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
      "trackTitle": "Bảng A - Web Application",
      "topicId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
      "topicTitle": "Hệ thống quản lý y tế thông minh"
    }
  ],
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Event phải tồn tại trong DB và chưa bị disable.
- Chỉ hiển thị kết quả khi sự kiện đã được công bố kết quả bốc thăm (nếu BTC áp dụng cờ hiển thị), nếu chưa công bố trả về danh sách rỗng hoặc lỗi `DRAW_RESULTS_NOT_PUBLISHED_YET`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Kết quả bốc thăm của sự kiện chưa được công bố.",
  "MessageCode": "DRAW_RESULTS_NOT_PUBLISHED_YET",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 403 | DRAW_RESULTS_NOT_PUBLISHED_YET | BTC chưa mở công bố thông tin bốc thăm. |
| 404 | EVENT_NOT_FOUND | Sự kiện không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
