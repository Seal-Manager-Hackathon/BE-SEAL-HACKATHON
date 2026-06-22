# Xem bảng xếp hạng chung cuộc Event (Get Event Leaderboard)

## Tác dụng
Xem bảng xếp hạng chung cuộc của một event thi đấu (điểm chung cuộc = tổng điểm các round thi đấu của team). FE dùng danh sách này để hiển thị mỗi team đang đứng hạng thứ mấy trong event đó.

## URL
`GET /api/v1/events/{eventId}/leaderboard`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của sự kiện.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa danh sách chi tiết xếp hạng.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": [
    {
      "rank": 1,
      "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
      "teamName": "Chiến binh công nghệ",
      "totalScore": 270.5,
      "levelAward": "Giải Nhất"
    }
  ],
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Event phải tồn tại trong DB và không bị soft-disable.
- Chỉ hiển thị khi leaderboard đã được BTC công bố công khai (published).
- Xếp hạng được sắp xếp theo tổng điểm (`totalScore`) giảm dần.
- Trường `rank` cho biết team đang đứng hạng thứ mấy trong event.
- `levelAward` hiển thị danh hiệu đạt được (Nhất, Nhì, Ba, Khuyến khích) nếu BTC đã gán.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Bảng xếp hạng của sự kiện chưa được công bố.",
  "MessageCode": "LEADERBOARD_NOT_PUBLISHED_YET",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 403 | LEADERBOARD_NOT_PUBLISHED_YET | BTC chưa mở công bố bảng xếp hạng chung cuộc. |
| 404 | EVENT_NOT_FOUND | Sự kiện không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
