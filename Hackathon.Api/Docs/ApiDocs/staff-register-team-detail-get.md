# Staff get register team detail

## Tác dụng
Staff xem chi tiết đơn đăng ký tham gia event của một team, bao gồm thông tin team, danh sách thành viên, track/topic đã gán và trạng thái đơn.

## URL
`GET /api/v1/staff/register-teams/{registerTeamId}`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `registerTeamId` | `guid` | Có | Id của đơn đăng ký cần xem chi tiết. |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/staff/register-teams/00000000-0000-0000-0000-000000000000
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.Base(data)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "value": {
    "id": "guid",
    "teamId": "guid",
    "teamName": "string",
    "eventId": "guid",
    "eventName": "string",
    "trackId": "guid|null",
    "trackTitle": "string|null",
    "topicId": "guid|null",
    "topicTitle": "string|null",
    "description": "string|null",
    "rejectionReason": "string|null",
    "status": 0, /* Pending */
    "isBanned": false,
    "isDisable": false,
    "members": [
      {
        "userId": "guid",
        "fullName": "string",
        "email": "string",
        "studentId": "string",
        "isLeader": false
      }
    ],
    "createdAt": "datetimeoffset",
    "updatedAt": "datetimeoffset"
  }
}
```

## Business rules
- Staff phải đăng nhập bằng access token hợp lệ.
- Endpoint này dùng policy `StaffOrAdminPolicy`.
- Admin có thể xem tất cả đơn đăng ký mà không cần phân công.
- Staff phải được phân công vào event của đơn đăng ký đó (`AssignEvents`) thì mới được xem chi tiết, nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- `registerTeamId` là bắt buộc trên path.
- Đơn đăng ký phải tồn tại và chưa bị soft-disable, nếu không trả `REGISTER_TEAM_NOT_FOUND`.
- Kết quả bao gồm danh sách thành viên của team (`TeamDetails`) đang active, cùng với thông tin event, track/topic đã được gán (nếu có).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | User không có role `Staff`. |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |

## Trạng thái implement
- Chưa implement endpoint.
- Cần thêm method `GetRegisterTeamDetail` trong `RegisterTeamService`.
- Dùng policy `StaffOrAdminPolicy` (Admin không cần kiểm tra phân công, Staff phải được phân công vào event).
- Cần thêm response model `RegisterTeamDetailResponse` và `RegisterTeamMemberResponse` (đã có sẵn trong `Response.cs`).
- Endpoint mới trong `RegisterTeam` controller.
