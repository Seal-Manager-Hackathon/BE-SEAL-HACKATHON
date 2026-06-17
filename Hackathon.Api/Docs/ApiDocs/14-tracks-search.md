# Search tracks

## Tác dụng
Lấy danh sách track, có thể tìm kiếm và lọc theo event.

## URL
`GET /api/v1/tracks`

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Không | Lọc track thuộc một event cụ thể. |
| `keyword` | `string` | Không | Từ khóa tìm kiếm theo tên (`Title`) hoặc mô tả track. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable. Nếu không truyền, mặc định chỉ trả track chưa bị disable (`IsDisable = false`). |
| `pageIndex` | `int` | Không | Trang hiện tại, mặc định `1`. |
| `pageSize` | `int` | Không | Số item mỗi trang, mặc định `10`. |

## Ví dụ request
```http
GET /api/v1/tracks?eventId=00000000-0000-0000-0000-000000000000&keyword=ai&isDisable=false&pageIndex=1&pageSize=10
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
        "eventId": "guid",
        "title": "string",
        "description": "string|null",
        "maxTeam": 0,
        "isDisable": false,
        "createdAt": "datetimeoffset"
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
- Nếu truyền `eventId`, chỉ trả track thuộc event đó.
- Nếu không truyền `isDisable`, mặc định chỉ trả track chưa bị soft-disable.
- Nếu truyền `keyword`, tìm kiếm không phân biệt hoa thường theo `Title` hoặc mô tả track.
- Track thuộc event bị disable không nên được trả về, trừ khi nghiệp vụ cho phép lấy dữ liệu disable bằng query riêng.
- Kết quả nên sắp xếp theo `Title` tăng dần.
- `pageIndex` phải lớn hơn hoặc bằng `1`; `pageSize` phải lớn hơn hoặc bằng `1`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Query parameter không hợp lệ. |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
