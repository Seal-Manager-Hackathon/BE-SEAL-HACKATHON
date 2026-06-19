# Lấy danh sách đăng ký event của một Team

## Tác dụng
API dùng để lấy danh sách toàn bộ các event mà một Team cụ thể (dựa vào `teamId`) đã nộp đơn đăng ký tham gia (bao gồm các trạng thái: Pending, Approved, Rejected).

## URL
`GET /api/v1/teams/{teamId}/events`

## Authorization
Yêu cầu Access Token (Cần đăng nhập).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `teamId` | `guid` | Có | ID của Team cần tra cứu. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `status` | `string` | Không | Lọc trạng thái (Pending, Approved, Rejected). Bỏ trống sẽ lấy toàn bộ. |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định 1) |
| `pageSize` | `int` | Không | Số lượng item trên mỗi trang (mặc định 10) |

## Ví dụ request
```http
GET /api/v1/teams/c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d/events?status=Pending&pageIndex=1&pageSize=10
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
  "traceId": "00-84a1e9df64619d8...",
  "timestampUtc": "2026-06-19T10:00:00.0000000Z",
  "value": {
    "items": [
      {
        "registerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "teamName": "Tên team",
        "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
        "eventName": "Hackathon ABC",
        "status": 1 /* Approved */,
        "description": "Mô tả nếu có",
        "createdAt": "2026-06-19T10:00:00.0000000Z"
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 1,
    "totalPages": 1,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

## Business rules
- Không yêu cầu role đặc biệt, user nào đăng nhập cũng có thể xem được lịch sử đăng ký event của team.
- Team phải đang không bị soft-disable (`IsDisable = false`).
- Trả về toàn bộ các đơn đăng ký (`RegisterTeams`) của `teamId` đó bất kể tình trạng duyệt (Pending/Approved/Rejected).
- Danh sách được sắp xếp mới nhất lên trước (`CreatedAt` giảm dần).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 400 | BAD_REQUEST | INVALID_STATUS (Nếu status truyền vào sai tên enum) |