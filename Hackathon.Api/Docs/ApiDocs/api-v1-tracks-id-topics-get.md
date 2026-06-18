# Lấy danh sách topics của track

## Tác dụng
Lấy danh sách các topic thuộc về một track cụ thể, có hỗ trợ tìm kiếm, lọc trạng thái và phân trang.

## URL
`GET /api/v1/tracks/{trackId}/topics`

## Authorization
Không yêu cầu (Public). Bất kỳ ai cũng có thể xem danh sách topic của một track.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `trackId` | `guid` | Có | Id của track cần lấy danh sách topic. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `keyword` | `string` | Không | Từ khóa tìm kiếm theo `Title` hoặc `Description`. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable. Nếu không truyền, mặc định lấy `false`. |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định `1`). |
| `pageSize` | `int` | Không | Số item mỗi trang (mặc định `10`). |

## Ví dụ request
```http
GET /api/v1/tracks/00000000-0000-0000-0000-000000000000/topics?keyword=ai&isDisable=false&pageIndex=1&pageSize=10
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
        "trackId": "guid",
        "title": "string",
        "description": "string|null",
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
- `trackId` là bắt buộc trên path. Nếu truyền `trackId`, track phải tồn tại và chưa bị soft-disable, nếu không trả `TRACK_NOT_FOUND`.
- Query luôn lọc `IsDisable == (isDisable ?? false)`.
- Nếu truyền `keyword`, tìm kiếm không phân biệt hoa thường theo `Title` hoặc `Description`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Query parameter không hợp lệ. |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
