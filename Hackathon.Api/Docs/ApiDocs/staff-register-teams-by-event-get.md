# Staff/Admin get register teams by event

## Tác dụng
Staff hoặc Admin lấy danh sách đơn đăng ký tham gia event của các team, có hỗ trợ lọc theo trạng thái (`Pending`, `Approved`, `Rejected`), tìm kiếm, lọc soft-disable và phân trang theo `BasePaginationResponse`.

## URL
`GET /api/v1/staff/events/{eventId}/register-teams`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event cần lấy danh sách đơn đăng ký. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `keyword` | `string` | Không | Từ khóa tìm kiếm theo tên team. |
| `status` | `int` | Không | Lọc theo trạng thái đơn đăng ký. Giá trị: `Pending`, `Approved`, `Rejected`. | // 0: Pending, 1: Approved, 2: Rejected
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable của đơn đăng ký. Nếu không truyền, mặc định lấy `false`. |
| `pageIndex` | `int` | Không | Trang hiện tại (thuộc `PaginationRequest`), mặc định `1`. |
| `pageSize` | `int` | Không | Số item mỗi trang (thuộc `PaginationRequest`), mặc định `10`. |

## Ví dụ request
```http
GET /api/v1/staff/events/00000000-0000-0000-0000-000000000000/register-teams?keyword=abc&status=Approved&isDisable=false&pageIndex=1&pageSize=10
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
        "teamId": "guid",
        "teamName": "string",
        "eventId": "guid",
        "trackId": "guid|null",
        "trackTitle": "string|null",
        "topicId": "guid|null",
        "topicTitle": "string|null",
        "description": "string|null",
        "rejectionReason": "string|null",
        "status": "string",
        "isBanned": false,
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
- Staff hoặc Admin phải đăng nhập bằng access token hợp lệ.
- Endpoint này yêu cầu role `Staff` hoặc `Admin` qua `[Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]`.
- `eventId` là bắt buộc trên path.
- Event phải tồn tại và chưa bị soft-disable, nếu không trả `EVENT_NOT_FOUND`.
- Nếu người gọi là Staff: phải được phân công vào event đó (`AssignEvents`) thì mới được xem danh sách, nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- Nếu người gọi là Admin: không cần kiểm tra phân công, có thể xem tất cả.
- Nếu truyền `status`, lọc theo `RegisterTeamStatusEnum` tương ứng (`Pending`, `Approved`, `Rejected`). // 0: Pending, 1: Approved, 2: Rejected
- Nếu không truyền `status`, trả về tất cả trạng thái.
- Query luôn lọc `IsDisable == (isDisable ?? false)`.
- Nếu truyền `keyword`, tìm kiếm không phân biệt hoa thường theo tên team.
- Kết quả bao gồm thông tin track/topic đã được gán (nếu có).
- `pageIndex` và `pageSize` được lấy từ `PaginationRequest`.
- `pageIndex = pageIndex <= 0 ? 1 : pageIndex`.
- `pageSize = pageSize <= 0 ? 10 : Math.Min(pageSize, 100)`.
- `totalCount` được tính trước khi `Skip/Take`.
- Kết quả sắp xếp theo tên team tăng dần, sau đó `CreatedAt` tăng dần.
- `hasNextPage = pageIndex * pageSize < totalCount`.
- `hasPreviousPage = pageIndex > 1`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | User không có role `Staff` hoặc `Admin`. |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 400 | BAD_REQUEST | Query parameter không hợp lệ. |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |

## Trạng thái implement
- Chưa implement endpoint.
- Cần tạo service method mới trong `TracksService` để query `RegisterTeams` theo `EventId`, hỗ trợ lọc `Status` (3 trạng thái), `IsDisable`, `keyword` theo tên team, phân trang.
- Cần include `Team`, `Track`, `Topic` để lấy tên.
- Cần thêm endpoint mới trong `StaffTracksController` với `StaffOrAdminPolicy`.
