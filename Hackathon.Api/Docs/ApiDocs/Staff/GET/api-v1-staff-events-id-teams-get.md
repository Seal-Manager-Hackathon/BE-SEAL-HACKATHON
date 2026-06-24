# Staff/Admin get teams by event

## Tác dụng
Staff hoặc Admin lấy danh sách team tham gia một event (lọc theo trạng thái đăng ký nếu truyền status, nếu không thì lấy tất cả các trạng thái), có hỗ trợ tìm kiếm, lọc trạng thái soft-disable và phân trang theo `BasePaginationResponse`.

## URL
`GET /api/v1/staff/events/{eventId}/teams`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event cần lấy danh sách team đã duyệt. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `keyword` | `string` | Không | Từ khóa tìm kiếm theo tên team. |
| `status` | `int` | Không | Lọc theo trạng thái đăng ký của team. Giá trị: `0`: Pending, `1`: Approved, `2`: Rejected. Nếu không truyền, lấy tất cả trạng thái. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable của đăng ký. Nếu không truyền, mặc định lấy `false`. |
| `pageIndex` | `int` | Không | Trang hiện tại (thuộc `PaginationRequest`), mặc định `1`. |
| `pageSize` | `int` | Không | Số item mỗi trang (thuộc `PaginationRequest`), mặc định `10`. |

## Ví dụ request
```http
GET /api/v1/staff/events/00000000-0000-0000-0000-000000000000/teams?keyword=abc&isDisable=false&pageIndex=1&pageSize=10
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
  "status": 200,
  "traceId": null,
  "timestampUtc": "datetime",
  "data": {
    "items": [
      {
        "teamId": "guid",
        "teamName": "string",
        "trackId": "guid|null",
        "trackTitle": "string|null",
        "topicId": "guid|null",
        "topicTitle": "string|null",
        "members": [
          {
            "userId": "guid",
            "fullName": "string",
            "email": "string",
            "studentId": "string",
            "isLeader": false
          }
        ],
        "isBanned": false,
        "status": 1,
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
- Nếu người gọi là Staff: phải được phân công vào event đó (`AssignEvents`) thì mới được xem danh sách team, nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- Nếu người gọi là Admin: không cần kiểm tra phân công, có thể xem tất cả.
- Lọc theo `status` nếu có truyền. Nếu không truyền `status`, lấy tất cả `RegisterTeams` ở bất kì trạng thái nào (`Pending`, `Approved`, `Rejected`). Các bản ghi phải không bị soft-disable, và team không bị soft-disable.
- Nếu truyền `keyword`, tìm kiếm không phân biệt hoa thường theo tên team.
- Kết quả bao gồm danh sách thành viên của team (`TeamDetails`) đang active, cùng với thông tin track/topic đã được gán (nếu có).
- `pageIndex` và `pageSize` được lấy từ `PaginationRequest`.
- `pageIndex = pageIndex <= 0 ? 1 : pageIndex`.
- `pageSize = pageSize <= 0 ? 10 : Math.Min(pageSize, 100)`.
- `totalCount` được tính trước khi `Skip/Take`.
- `hasNextPage = pageIndex * pageSize < totalCount`.
- `hasPreviousPage = pageIndex > 1`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 400 | BAD_REQUEST | QUERY_PARAMETER_INVALID |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Staff` controller.
- Đã implement service method `TracksService.GetApprovedTeamsByEvent` (hỗ trợ lọc status).
- Service query `RegisterTeams` với điều kiện `IsDisable == (isDisable ?? false)`, `!Team.IsDisable`, lọc theo `status` nếu có truyền.
- Nếu người gọi là Staff thì kiểm tra `AssignEvents`; nếu là Admin thì bỏ qua kiểm tra phân công.
- Response trả danh sách team, thành viên active, track/topic đã gán, trạng thái đăng ký `status` và phân trang theo `BasePaginationResponse`.
