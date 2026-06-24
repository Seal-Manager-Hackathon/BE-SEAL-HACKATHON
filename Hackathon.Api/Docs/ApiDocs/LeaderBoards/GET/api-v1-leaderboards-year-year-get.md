# Xem bảng xếp hạng mùa giải năm

## Tác dụng
Xem bảng xếp hạng tích lũy điểm số của toàn bộ các event được tổ chức trong năm (mùa giải).

## URL
`GET /api/v1/leaderboards/year/{year}`

## Authorization
Không yêu cầu access token (Public API).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `year` | `integer` | Có | Năm của mùa giải cần xem xếp hạng (ví dụ: 2026). |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/leaderboards/year/2026
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
      "rank": 1,
      "teamId": "guid",
      "teamName": "string",
      "totalYearScore": "decimal",
      "eventsParticipated": "integer"
    }
  ],
  "message": "SUCCESS"
}
```

## Business rules
- Điểm tích lũy năm của team bằng tổng điểm của toàn bộ các event team đã tham gia trong năm (BR-LB-04).
- Nếu team không tham gia đủ số event trong năm, hệ thống vẫn cộng điểm các event đã tham gia thi đấu (không loại bỏ khỏi leaderboard, BR-LB-05).
- Sắp xếp kết quả xếp hạng theo `totalYearScore` giảm dần.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | INVALID_YEAR | Năm chỉ định không hợp lệ hoặc không có giải đấu nào được tổ chức trong năm đó. |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
