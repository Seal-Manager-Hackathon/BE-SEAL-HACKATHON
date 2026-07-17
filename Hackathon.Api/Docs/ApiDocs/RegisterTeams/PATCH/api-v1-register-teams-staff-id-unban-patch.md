# Staff unban team

## Tác dụng
Staff thực hiện gỡ cấm (Unban) cho một đội thi đã bị cấm khỏi sự kiện trước đó. Sau khi gỡ cấm, đội thi vẫn ở trạng thái `Rejected`, nhưng Staff có thể tiến hành duyệt lại (Approve) đơn đăng ký của đội đó nếu muốn.

## URL
`PATCH /api/v1/register-teams/staff/{registerId}/unban`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `registerId` | `guid` | Có | Id của đơn đăng ký (`RegisterTeams.Id`) của team cần gỡ cấm. |

## Request body
Không có.

## Ví dụ request
```http
PATCH /api/v1/register-teams/staff/00000000-0000-0000-0000-000000000000/unban
Authorization: Bearer {accessToken}
```

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
    "rejectionReason": "string|null",
    "isBanned": false
  },
  "message": "TEAM_UNBANNED_SUCCESSFULLY"
}
```

## Business rules
- Người gọi phải là `Staff` hoặc `Admin`.
- Nếu là `Staff`, phải được phân công quản lý sự kiện của đơn đăng ký này (`AssignEvents`).
- `registerId` phải tồn tại và không bị disable.
- Team phải đang trong trạng thái bị cấm (`IsBanned == true`), nếu không trả lỗi.
- Cập nhật các trường:
  - `IsBanned = false`
  - `UpdatedAt = DateTimeOffset.UtcNow`
- **Lưu ý:** Giữ nguyên `Status` hiện tại (thường là `Rejected`), giữ nguyên `RejectionReason`, và giữ nguyên `Team.CanEdit = true`. Đội thi phải được duyệt tay lại (Approve) nếu đủ điều kiện tham gia trở lại.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | FORBIDDEN / STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 409 | CONFLICT | TEAM_IS_NOT_BANNED |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
