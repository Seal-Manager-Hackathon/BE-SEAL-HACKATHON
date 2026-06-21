# Lấy danh sách track của event

## Tác dụng
Lấy danh sách các track thuộc về một sự kiện (event) cụ thể, có hỗ trợ tìm kiếm, lọc trạng thái và phân trang.

## URL
`GET /api/v1/events/{eventId}/tracks`

## Authorization
Không yêu cầu (Public). Bất kỳ ai cũng có thể xem danh sách track của một event.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event cần lấy danh sách track. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `keyword` | `string` | Không | Từ khóa tìm kiếm theo `Title` hoặc `Description`. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable. Nếu không truyền, mặc định lấy `false`. |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định `1`). |
| `pageSize` | `int` | Không | Số item mỗi trang (mặc định `10`). |

## Ví dụ request
```http
GET /api/v1/events/00000000-0000-0000-0000-000000000000/tracks?keyword=ai&isDisable=false&pageIndex=1&pageSize=10
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.BasePagination(...)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": null,
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
- Không yêu cầu auth, endpoint public.
- `eventId` là bắt buộc trên path. Nếu truyền `eventId`, event phải tồn tại và chưa bị soft-disable, nếu không trả `EVENT_NOT_FOUND`.
- Query luôn lọc `IsDisable == (isDisable ?? false)`.
- Nếu truyền `keyword`, service trim và lower-case keyword, sau đó tìm kiếm không phân biệt hoa thường theo `Title` hoặc `Description`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | QUERY_PARAMETER_INVALID |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
