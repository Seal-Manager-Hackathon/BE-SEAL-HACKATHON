# Staff lấy danh sách đơn đăng ký theo event

## Tác dụng
Staff lấy danh sách đơn đăng ký tham gia event của các team, có hỗ trợ tìm kiếm theo tên team và lọc theo trạng thái.

**Ưu tiên sắp xếp:** đơn chưa duyệt (`Pending`) lên đầu, đã duyệt (`Approved`) ở giữa, bị từ chối (`Rejected`) ở cuối.

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
| `status` | `int` | Không | Lọc theo trạng thái đơn. `0`: Pending, `1`: Approved, `2`: Rejected. Nếu không truyền thì lấy tất cả. |
| `pageIndex` | `int` | Không | Trang hiện tại, mặc định `1`. |
| `pageSize` | `int` | Không | Số item mỗi trang, mặc định `10`, tối đa `100`. |

## Response body (Success - 200 OK)
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
        "status": 0,
        "isBanned": false,
        "isDisable": false,
        "createdAt": "datetimeoffset"
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 1,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

### Bảng trạng thái RegisterTeamStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ duyệt |
| `1` | Approved | Đã được duyệt |
| `2` | Rejected | Bị từ chối |

## Business rules
- Yêu cầu access token hợp lệ.
- `eventId` là bắt buộc trên path.
- Event phải tồn tại và chưa bị disable, nếu không trả `EVENT_NOT_FOUND`.
- Nếu người gọi là Staff: phải được phân công vào event đó (`AssignEvents`) thì mới được xem, nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- Nếu người gọi là Admin: không cần kiểm tra phân công.
- Nếu truyền `status`, lọc theo `RegisterTeamStatusEnum` tương ứng.
- Nếu không truyền `status`, trả về tất cả trạng thái.
- Query luôn lọc `IsDisable == false`.
- Nếu truyền `keyword`, tìm kiếm không phân biệt hoa thường theo tên team.
- **Sắp xếp mặc định khi không lọc theo status:**
  - `Pending` (0) lên đầu → `Approved` (1) ở giữa → `Rejected` (2) ở cuối.
  - Sau đó theo tên team tăng dần, rồi `CreatedAt` tăng dần.
- Kết quả bao gồm thông tin track/topic đã được gán (nếu có).
- Phân trang theo `BasePaginationResponse`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
