# Get leaderboard by year

## Tác dụng
Lấy bảng xếp hạng theo năm, tổng hợp điểm leaderboard của các event trong năm đó.

## URL
`GET /api/v1/leaderboards/year`

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `year` | `int` | Có | Năm cần xem leaderboard. Lọc theo năm của `Event.StartTime`. |
| `pageIndex` | `int` | Không | Trang hiện tại, mặc định `1`. |
| `pageSize` | `int` | Không | Số item mỗi trang, mặc định `10`. |

## Ví dụ request
```http
GET /api/v1/leaderboards/year?year=2026&pageIndex=1&pageSize=10
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
    "items": [
      {
        "rank": 1,
        "teamId": "guid",
        "teamName": "string",
        "totalScore": 0
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
- Chỉ tính team có đơn đăng ký hợp lệ, team và event chưa bị soft-disable.
- Kết quả sắp xếp theo `TotalScore` giảm dần, sau đó theo `TeamName` tăng dần.
- Chỉ tính team có `totalScore > 0`.
- Rank được đánh số thứ tự tuần tự.
- `pageIndex` phải lớn hơn hoặc bằng `1`; `pageSize` phải lớn hơn hoặc bằng `1`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | YEAR_REQUIRED |
| 400 | BAD_REQUEST | Query parameter không hợp lệ (pageIndex/pageSize). |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
