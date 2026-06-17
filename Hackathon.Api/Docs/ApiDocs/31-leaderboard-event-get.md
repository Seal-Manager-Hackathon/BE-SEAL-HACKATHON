# Get event leaderboard

## Tác dụng
Lấy bảng xếp hạng của một event theo `eventId`.

## URL
`GET /api/v1/events/{eventId}/leaderboard`

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event cần xem leaderboard. |

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
      "rank": 1,
      "teamId": "guid",
      "teamName": "string",
      "totalScore": 0
    }
  ]
}
```

## Business rules
- Event leaderboard tính theo tổng điểm các round của event.
- Round score là điểm trung bình từ các judge scores trong round, chỉ tính submission chưa bị disable.
- Chỉ tính team có đơn đăng ký event hợp lệ (chưa bị disable) và team chưa bị disable.
- Chỉ tính team có `totalScore > 0`.
- Kết quả sắp xếp theo `TotalScore` giảm dần, sau đó theo `TeamName` tăng dần.
- Rank được đánh số thứ tự tuần tự (1, 2, 3,...), không có rank trùng.
- Event phải tồn tại và chưa bị soft-disable.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | LEADERBOARD_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
