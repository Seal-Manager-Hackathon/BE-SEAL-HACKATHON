# Get leaderboard by year

## Tác dụng
Lấy bảng xếp hạng theo năm, tổng hợp điểm leaderboard của các event trong năm đó.

## URL
`GET /api/leaderboards/year`

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `year` | `int` | Có | Năm cần xem leaderboard. Lọc theo năm của `Event.StartTime`. |
| `pageIndex` | `int` | Không | Trang hiện tại, mặc định `1`. |
| `pageSize` | `int` | Không | Số item mỗi trang, mặc định `10`. |

## Ví dụ request
```http
GET /api/leaderboards/year?year=2026&pageIndex=1&pageSize=10
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
  "value": {
    "year": 2026,
    "items": [
      {
        "rank": 1,
        "teamId": "guid",
        "teamName": "string",
        "totalScore": 0,
        "events": [
          {
            "eventId": "guid",
            "eventName": "string",
            "score": 0
          }
        ]
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 0,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

## Business rules
- `year` là bắt buộc.
- Year leaderboard = tổng điểm event leaderboard trong các event thuộc năm đó.
- Chỉ tính event và leaderboard chưa bị soft-disable.
- Kết quả sắp xếp theo `totalScore` giảm dần.
- `pageIndex` phải lớn hơn hoặc bằng `1`; `pageSize` phải lớn hơn hoặc bằng `1`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | YEAR_REQUIRED | Year is required. |
| 400 | BAD_REQUEST | Query parameter không hợp lệ. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
