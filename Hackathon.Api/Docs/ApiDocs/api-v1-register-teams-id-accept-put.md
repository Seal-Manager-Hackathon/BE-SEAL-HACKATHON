# Staff accept register team

## Tác dụng
Staff duyệt đơn đăng ký tham gia event của một team. Khi đơn được duyệt, trạng thái đăng ký chuyển sang `Approved` và team bị khóa chỉnh sửa thành viên để đảm bảo danh sách thành viên tham gia event không thay đổi sau khi được chấp nhận.

## URL
`PATCH /api/v1/staff/register-teams/{registerTeamId}/accept`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `registerTeamId` | `guid` | Có | Id của đơn đăng ký cần duyệt. |

## Query parameters
Không có.

## Ví dụ request
```http
PATCH /api/v1/staff/register-teams/00000000-0000-0000-0000-000000000000/accept
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
    "status": 1, /* Approved */
    "message": "REGISTER_TEAM_ACCEPTED_SUCCESSFULLY"
  }
}
```

## Business rules
- Staff hoặc Admin phải đăng nhập bằng access token hợp lệ.
- Endpoint này cho phép Staff hoặc Admin qua `[Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]`.
- `registerTeamId` là bắt buộc trên path.
- Đơn đăng ký phải tồn tại và chưa bị soft-disable, nếu không trả `REGISTER_TEAM_NOT_FOUND`.
- Event của đơn đăng ký phải tồn tại và chưa bị soft-disable, nếu không trả `EVENT_NOT_FOUND`.
- Team của đơn đăng ký phải tồn tại và chưa bị soft-disable, nếu không trả `TEAM_NOT_FOUND`.
- Staff phải được phân công vào event của đơn đăng ký đó (`AssignEvents`) thì mới được duyệt, nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- Admin được duyệt trực tiếp, không cần kiểm tra phân công vào event.
- Chỉ được duyệt đơn đang ở trạng thái `Pending`; nếu đơn đã `Approved` hoặc `Rejected` thì trả conflict.
- Nếu team đang bị banned khỏi event (`IsBanned == true`) thì không được duyệt.
- Khi duyệt thành công:
  - `RegisterTeams.Status` được cập nhật thành `Approved`.
  - `RegisterTeams.RejectionReason` được reset về `null`.
  - `RegisterTeams.UpdatedAt` được cập nhật theo thời gian hiện tại.
  - `Teams.CanEdit` được cập nhật thành `false` để khóa chỉnh sửa thành viên.
  - `Teams.UpdatedAt` được cập nhật theo thời gian hiện tại.
- Nên thực hiện cập nhật trong transaction để tránh trạng thái đơn đã duyệt nhưng team chưa bị khóa.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | User không có role `Staff`. |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 409 | CONFLICT | REGISTER_TEAM_ALREADY_APPROVED |
| 409 | CONFLICT | REGISTER_TEAM_ALREADY_REJECTED |
| 409 | CONFLICT | TEAM_IS_BANNED_FROM_EVENT |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.RegisterTeam`.
- Đã thêm method `AcceptRegisterTeam(Guid registerTeamId)` trong `Hackathon.Service.RegisterTeam.IService`.
- Đã implement logic trong `Hackathon.Service.RegisterTeam.Service`.
- Đã thêm response model `RegisterTeamActionResponse` trong `Hackathon.Service.RegisterTeam.Response`.
- Endpoint dùng route `PATCH /api/v1/staff/register-teams/{registerTeamId}/accept` và `StaffOrAdminPolicy`.
