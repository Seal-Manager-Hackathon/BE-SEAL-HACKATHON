# Search events

## Tác dụng
Tìm kiếm danh sách event theo từ khóa và các điều kiện lọc cơ bản.

**Lưu ý:** Chức năng search là một phần của `GET /api/v1/events`. Không có endpoint `/events/search` riêng. Vui lòng xem [24-events-get.md](24-events-get.md) để biết chi tiết query parameters và response.

## URL
`GET /api/v1/events`

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `keyword` | `string` | Không | Từ khóa tìm kiếm theo `Name`, `Description` hoặc `Season`. |
| `year` | `int` | Không | Lọc event theo năm của `StartTime`. |
| `status` | `string` | Không | Lọc theo trạng thái event. Giá trị theo `EventStatusEnum`. |
| `pageIndex` | `int` | Không | Trang hiện tại, mặc định `1`. |
| `pageSize` | `int` | Không | Số item mỗi trang, mặc định `10`. |

## Ví dụ request
```http
GET /api/v1/events?keyword=hackathon&year=2026&pageIndex=1&pageSize=10
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
        "id": "guid",
        "name": "string",
        "description": "string|null",
        "startTime": "datetimeoffset|null",
        "endTime": "datetimeoffset|null",
        "registerLimitTime": "datetimeoffset|null",
        "limitTeam": 0,
        "minMember": 0,
        "maxMember": 0,
        "status": "string|null",
        "numberRound": 0,
        "season": "string|null",
        "isDisable": false,
        "createdAt": "datetimeoffset",
        "updatedAt": "datetimeoffset"
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
- Nếu không truyền `isDisable`, mặc định chỉ trả event chưa bị soft-disable.
- Nếu truyền `keyword`, tìm kiếm không phân biệt hoa thường theo `Name`, `Description`, `Season`.
- Nếu truyền `year`, lọc theo năm của `StartTime`.
- Nếu truyền `status`, lọc theo trạng thái event hợp lệ.
- Kết quả nên sắp xếp theo `StartTime` tăng dần, sau đó `CreatedAt` tăng dần.
- `pageIndex` phải lớn hơn hoặc bằng `1`; `pageSize` phải lớn hơn hoặc bằng `1`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Query parameter không hợp lệ. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
