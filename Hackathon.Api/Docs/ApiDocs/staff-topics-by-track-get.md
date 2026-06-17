# Staff get topics by track

## Tác dụng
Staff lấy danh sách topic thuộc một track, có hỗ trợ tìm kiếm, lọc trạng thái soft-disable và phân trang theo `BasePaginationResponse`.

## URL
`GET /api/v1/staff/tracks/{trackId}/topics`

## Authorization
Yêu cầu access token hợp lệ với role `Staff`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `trackId` | `guid` | Có | Id của track cần lấy danh sách topic. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `keyword` | `string` | Không | Từ khóa tìm kiếm theo `Title` hoặc `Description`. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable. Nếu không truyền, mặc định lấy `false`. |
| `pageIndex` | `int` | Không | Trang hiện tại (thuộc `PaginationRequest`), mặc định `1`. |
| `pageSize` | `int` | Không | Số item mỗi trang (thuộc `PaginationRequest`), mặc định `10`. |

## Ví dụ request
```http
GET /api/v1/staff/tracks/00000000-0000-0000-0000-000000000000/topics?keyword=backend&isDisable=false&pageIndex=1&pageSize=10
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body
Response nên dùng `ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount)`.

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
- Staff phải đăng nhập bằng access token hợp lệ.
- Track phải tồn tại và chưa bị soft-disable, nếu không trả `TRACK_NOT_FOUND`.
- Chỉ trả topic thuộc `trackId` trên path.
- Query nên lọc `IsDisable == (isDisable ?? false)`.
- Nếu truyền `keyword`, service nên trim và lower-case keyword, sau đó tìm kiếm không phân biệt hoa thường theo `Title` hoặc `Description`.
- `pageIndex` và `pageSize` được lấy từ `PaginationRequest`.
- `pageIndex = pageIndex <= 0 ? 1 : pageIndex`.
- `pageSize = pageSize <= 0 ? 10 : Math.Min(pageSize, 100)`.
- `totalCount` được tính trước khi `Skip/Take`.
- Kết quả nên sắp xếp theo `Title` tăng dần, sau đó `CreatedAt` tăng dần.
- `hasNextPage = pageIndex * pageSize < totalCount`.
- `hasPreviousPage = pageIndex > 1`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | User không có role `Staff`. |
| 400 | BAD_REQUEST | Query parameter không hợp lệ. |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |

## Trạng thái implement
- Đã implement trong `StaffTracksController` và `TracksService.GetTopicsByTrack`.
- Entity `Topics` có các field phù hợp: `Id`, `TrackId`, `Title`, `Description`, `IsDisable`, `CreatedAt`, `UpdatedAt`.
- Logic dùng cùng pattern phân trang như mẫu: chuẩn hóa `pageIndex/pageSize`, build query, tính `totalCount`, `OrderBy`, `Skip/Take`, map response, rồi trả `ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount)`.
