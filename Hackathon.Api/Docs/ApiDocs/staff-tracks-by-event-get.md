# Staff get tracks by event

## Tác dụng
Staff lấy danh sách track thuộc một event, có hỗ trợ tìm kiếm, lọc trạng thái soft-disable và phân trang theo `BasePaginationResponse`.

## URL
`GET /api/v1/staff/events/{eventId}/tracks`

## Authorization
Yêu cầu access token hợp lệ với role `Staff`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event cần lấy danh sách track. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `keyword` | `string` | Không | Từ khóa tìm kiếm theo `Title` hoặc `Description`. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable. Nếu không truyền, mặc định lấy `false`. |
| `pageIndex` | `int` | Không | Trang hiện tại, mặc định `1`. Nếu truyền `<= 0`, service tự đổi về `1`. |
| `pageSize` | `int` | Không | Số item mỗi trang, mặc định `10`. Nếu truyền `<= 0`, service tự đổi về `10`; tối đa `100`. |

## Ví dụ request
```http
GET /api/v1/staff/events/00000000-0000-0000-0000-000000000000/tracks?keyword=ai&isDisable=false&pageIndex=1&pageSize=10
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount)`.

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
- Staff phải đăng nhập bằng access token hợp lệ.
- Endpoint này bắt buộc Staff-only qua `[Authorize(Policy = JwtExtensions.StaffPolicy)]`.
- `eventId` là bắt buộc trên path.
- Nếu truyền `eventId`, event phải tồn tại và chưa bị soft-disable, nếu không trả `EVENT_NOT_FOUND`.
- Query luôn lọc `IsDisable == (isDisable ?? false)`.
- Nếu truyền `keyword`, service trim và lower-case keyword, sau đó tìm kiếm không phân biệt hoa thường theo `Title` hoặc `Description`.
- `pageIndex = pageIndex <= 0 ? 1 : pageIndex`.
- `pageSize = pageSize <= 0 ? 10 : Math.Min(pageSize, 100)`.
- `totalCount` được tính trước khi `Skip/Take`.
- Kết quả sắp xếp theo `Title` tăng dần, sau đó `CreatedAt` tăng dần.
- `hasNextPage = pageIndex * pageSize < totalCount`.
- `hasPreviousPage = pageIndex > 1`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | User không có role `Staff`. |
| 400 | BAD_REQUEST | Query parameter không hợp lệ. |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |

## Trạng thái implement
- Đã có logic lấy track theo `eventId`, `keyword`, `isDisable`, `pageIndex`, `pageSize` trong `TracksService.GetTracks`.
- Đã trả response theo `ApiResponseFactory.BasePagination`.
- Đã có endpoint Staff-only trong `StaffTracksController`.
