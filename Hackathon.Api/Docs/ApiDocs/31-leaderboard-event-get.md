# Get event leaderboard

## Tác dụng
Lấy bảng xếp hạng của một event theo `eventId`.

## URL
`GET /api/events/{eventId}/leaderboard`

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
  "value": {
    "eventId": "guid",
    "eventName": "string",
    "items": [
      {
        "rank": 1,
        "teamId": "guid",
        "teamName": "string",
        "totalScore": 0,
        "roundScores": [
          {
            "roundId": "guid",
            "roundName": "string",
            "score": 0
          }
        ]
      }
    ]
  }
}
```

## Business rules
- Event leaderboard tính theo tổng điểm các round của event.
- Round score là điểm trung bình từ các judge scores trong round.
- Chỉ tính team có đơn đăng ký event hợp lệ theo rule hệ thống.
- Kết quả sắp xếp theo `totalScore` giảm dần.
- Nếu bằng điểm, có thể giữ cùng rank hoặc sắp xếp phụ theo rule FE/BE thống nhất.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | EVENT_NOT_FOUND | Event not found. |
| 404 | LEADERBOARD_NOT_FOUND | Leaderboard not found. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
