# Staff reject register team

## Tác dụng
Staff từ chối đơn đăng ký tham gia event của một team, chuyển trạng thái đơn từ `Pending` sang `Rejected` kèm lý do từ chối.

## URL
`PATCH /api/v1/staff/register-teams/{registerTeamId}/reject`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `registerTeamId` | `guid` | Có | Id của đơn đăng ký cần từ chối. |

## Query parameters
Không có.

## Ví dụ request
```http
PATCH /api/v1/staff/register-teams/00000000-0000-0000-0000-000000000000/reject
Authorization: Bearer {accessToken}
Content-Type: application/json
```

## Request body
```json
{
  "reason": "string"
}
```

| Field | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `reason` | `string` | Có | Lý do từ chối đơn đăng ký. |

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
    "status": 2, /* Rejected */
    "rejectionReason": "string",
    "message": "REGISTER_TEAM_REJECTED_SUCCESSFULLY"
  }
}
```

## Business rules
- Staff hoặc Admin phải đăng nhập bằng access token hợp lệ.
- Endpoint này cho phép Staff hoặc Admin qua `[Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]`.
- `registerTeamId` là bắt buộc trên path.
- `reason` là bắt buộc trong request body, sau khi trim không được để trống, nếu không trả `REASON_REQUIRED`.
- Đơn đăng ký phải tồn tại và chưa bị soft-disable, nếu không trả `REGISTER_TEAM_NOT_FOUND`.
- Event của đơn đăng ký phải tồn tại và chưa bị soft-disable, nếu không trả `EVENT_NOT_FOUND`.
- Team của đơn đăng ký phải tồn tại và chưa bị soft-disable, nếu không trả `TEAM_NOT_FOUND`.
- Staff phải được phân công vào event của đơn đăng ký đó (`AssignEvents`) thì mới được từ chối, nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- Admin được từ chối trực tiếp, không cần kiểm tra phân công vào event.
- Chỉ được từ chối đơn đang ở trạng thái `Pending`; nếu đơn đã `Approved` hoặc `Rejected` thì trả conflict.
- Khi từ chối thành công:
  - `RegisterTeams.Status` được cập nhật thành `Rejected`.
  - `RegisterTeams.RejectionReason` được lưu bằng `reason` đã trim.
  - `RegisterTeams.UpdatedAt` được cập nhật theo thời gian hiện tại.
  - `Teams.CanEdit` được cập nhật thành `true` để team có thể chỉnh sửa/gửi lại sau khi bị từ chối.
  - `Teams.UpdatedAt` được cập nhật theo thời gian hiện tại.
- Nên thực hiện cập nhật trong transaction để tránh trạng thái đơn đã bị từ chối nhưng team chưa được mở khóa.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | User không có role `Staff`. |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 400 | BAD_REQUEST | REASON_REQUIRED |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 409 | CONFLICT | REGISTER_TEAM_ALREADY_APPROVED |
| 409 | CONFLICT | REGISTER_TEAM_ALREADY_REJECTED |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.RegisterTeam`.
- Đã thêm method `RejectRegisterTeam(Guid registerTeamId, RejectRegisterTeamRequest request)` trong `Hackathon.Service.RegisterTeam.IService`.
- Đã thêm request model `RejectRegisterTeamRequest` trong `Hackathon.Service.RegisterTeam.Request` với field `reason`.
- Đã implement logic trong `Hackathon.Service.RegisterTeam.Service`.
- Đã dùng chung response model `RegisterTeamActionResponse` với accept trong `Hackathon.Service.RegisterTeam.Response`.
- Endpoint dùng route `PATCH /api/v1/staff/register-teams/{registerTeamId}/reject` và `StaffOrAdminPolicy`.
