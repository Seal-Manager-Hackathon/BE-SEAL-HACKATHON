# Team member xem danh sách đơn đăng ký vào event của team

## Tác dụng
API dùng cho thành viên trong team (cả Leader và Member) xem toàn bộ đơn đăng ký tham gia event của team mình, bao gồm cả 3 trạng thái: `Pending`, `Approved`, `Rejected`. API tự động xác định team dựa trên user hiện tại.

## URL
`GET /api/v1/teams/me/register-teams`

## Authorization
Yêu cầu access token hợp lệ với role `Student` và user hiện tại phải là thành viên đang active của một team (có `Status = Active` trong `TeamDetails`).

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `status` | `string` | Không | Lọc theo trạng thái (`Pending`, `Approved`, `Rejected`). Bỏ trống sẽ lấy toàn bộ. |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định 1) |
| `pageSize` | `int` | Không | Số lượng item trên mỗi trang (mặc định 10, tối đa 100) |

## Ví dụ request
```http
GET /api/v1/teams/me/register-teams?pageIndex=1&pageSize=10
Authorization: Bearer {accessToken}
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
  "status": 200,
  "traceId": "00-84a1e9df64619d8...",
  "timestampUtc": "2026-06-19T10:00:00.0000000Z",
  "data": {
    "items": [
      {
        "registerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "teamName": "Tên team",
        "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
        "eventName": "Hackathon ABC",
        "status": 0,
        "statusName": "Pending",
        "description": "Mô tả đơn đăng ký",
        "rejectionReason": null,
        "createdAt": "2026-06-19T10:00:00.0000000Z"
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

## Business rules
- User phải đăng nhập và có role `Student`.
- User phải là thành viên đang active (`Status = Active`) của một team (`IsDisable = false`) trong `TeamDetails`.
- Nếu user thuộc nhiều team, ưu tiên team mà user là leader; nếu không phải leader của team nào, lấy team đầu tiên user tham gia.
- Trả về toàn bộ các đơn đăng ký (`RegisterTeams`) của team đó bất kể trạng thái duyệt (`Pending`/`Approved`/`Rejected`).
- Nếu truyền `status`, chỉ lọc các đơn có trạng thái tương ứng.
- Danh sách được sắp xếp mới nhất lên trước (`CreatedAt` giảm dần).
- Nếu user không phải thành viên của team nào, trả về lỗi `FORBIDDEN`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT |
| 403 | FORBIDDEN | NOT_TEAM_MEMBER |
| 400 | BAD_REQUEST | INVALID_STATUS |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
