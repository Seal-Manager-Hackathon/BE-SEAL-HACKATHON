# Staff/Lecturer/Admin xem chi tiết đơn đăng ký

## Tác dụng
Staff, Lecturer hoặc Admin (đã được phân công trong event) xem chi tiết đơn đăng ký tham gia event của một team, bao gồm thông tin team, danh sách thành viên, track/topic đã gán, trạng thái đơn, trạng thái thi đấu (đã bị loại hay chưa) và vòng thi hiện tại.

## URL
`GET /api/v1/register-teams/staff/{registerTeamId}`

## Authorization
Yêu cầu access token hợp lệ với role `Staff`, `Lecturer` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `registerTeamId` | `guid` | Có | Id của đơn đăng ký cần xem chi tiết. |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/register-teams/staff/00000000-0000-0000-0000-000000000000
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
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
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
    "status": 0, /* RegisterTeamStatusEnum: 0=Pending, 1=Approved, 2=Rejected */
    "isBanned": false,
    "isDisable": false,
    "isEliminated": false,
    "currentRoundId": "guid|null",
    "currentRoundName": "string|null",
    "currentRoundNo": 1,
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
  },
  "message": "SUCCESS"
}
```

## Business rules
- Phải đăng nhập bằng access token hợp lệ.
- Endpoint này dùng policy `StaffLecturerOrAdminPolicy` (Staff, Lecturer, Admin).
- Admin có thể xem tất cả đơn đăng ký mà không cần phân công.
- Staff hoặc Lecturer phải được phân công vào event của đơn đăng ký đó (`AssignEvents`) thì mới được xem chi tiết, nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- `registerTeamId` là bắt buộc trên path.
- Đơn đăng ký phải tồn tại và chưa bị soft-disable, nếu không trả `REGISTER_TEAM_NOT_FOUND`.
- Kết quả bao gồm danh sách thành viên của team (`TeamDetails`) đang active, cùng với thông tin event, track/topic đã được gán (nếu có).
- **Cách tính `IsEliminated` (động):**
  - Nếu event chưa có round nào active → `isEliminated = false` (chưa bắt đầu).
  - Nếu event có round active: đội bị loại (`isEliminated = true`) khi **không có** `RoundDetails` active trong bất kỳ round active nào.
- **Cách tính `CurrentRound` (động):**
  - Vòng thi hiện tại = round active có `RoundNo` lớn nhất mà đội có `RoundDetails`.
  - Nếu đội đã bị loại hoặc event chưa có round → `currentRoundId` / `currentRoundName` / `currentRoundNo` = `null`.

### Bảng trạng thái RegisterTeamStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ duyệt |
| `1` | Approved | Đã được duyệt |
| `2` | Rejected | Bị từ chối |

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.RegisterTeamController`.
- Method: `GetRegisterTeamDetail(Guid registerTeamId)`.
- Endpoint dùng route `GET /api/v1/register-teams/staff/{registerTeamId:guid}` và `StaffLecturerOrAdminPolicy`.
