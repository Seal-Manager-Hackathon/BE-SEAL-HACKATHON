# Staff ban team

## Tác dụng
Staff thực hiện cấm (Ban) một đội thi khỏi sự kiện. Đội thi bị cấm sẽ lập tức chuyển trạng thái đăng ký thành `Rejected`, cờ `IsBanned` được bật thành `true`, và không thể được duyệt lại (Approved) trong sự kiện này trừ khi được gỡ cấm (Unban).

## URL
`PATCH /api/v1/register-teams/staff/{registerId}/ban`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `registerId` | `guid` | Có | Id của đơn đăng ký (`RegisterTeams.Id`) của team cần cấm. |

## Request body
```json
{
  "reason": "string"
}
```
*Ghi chú*: `reason` là lý do cấm (VD: Vi phạm quy chế thi, gian lận, không đủ điều kiện...).

## Response body
Response dùng `ApiResponseFactory.Base(...)`.
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
    "status": 2 /* Rejected */,
    "rejectionReason": "string",
    "isBanned": true
  },
  "message": "TEAM_BANNED_SUCCESSFULLY"
}
```

## Business rules
- Người gọi phải là `Staff` hoặc `Admin`.
- Nếu là `Staff`, phải được phân công quản lý sự kiện của đơn đăng ký này (`AssignEvents`).
- `registerId` phải tồn tại và không bị disable.
- Không thể cấm một đơn đăng ký đã bị disable.
- Cập nhật các trường:
  - `IsBanned = true`
  - `Status = RegisterTeamStatusEnum.Rejected`
  - `RejectionReason = reason` (Ghi đè lý do từ chối bằng lý do cấm).
  - `UpdatedAt = DateTimeOffset.UtcNow`
  - Đồng thời mở khóa team: `Team.CanEdit = true` (vì team đã bị loại bỏ khỏi event, họ được quyền chỉnh sửa lại thành viên cho các sự kiện khác).
- Phải thực hiện cập nhật cả `RegisterTeams` và `Teams` trong cùng một context.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | FORBIDDEN / STAFF_NOT_ASSIGNED_TO_EVENT |
| 400 | BAD_REQUEST | REASON_IS_REQUIRED |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 409 | CONFLICT | TEAM_IS_ALREADY_BANNED |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
