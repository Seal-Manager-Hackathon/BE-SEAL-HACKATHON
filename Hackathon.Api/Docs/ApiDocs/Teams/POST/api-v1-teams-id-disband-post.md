# Student disband team (Disband Team)

## Tác dụng
Cho phép Leader giải tán team. Tất cả thành viên trong team sẽ bị set `IsDisable = true`, `Status = Inactive`. Team sẽ bị set `IsDisable = true`, `CanEdit = false`.

## URL
`POST /api/v1/teams/{teamId}/disband`

## Authorization
Yêu cầu access token hợp lệ với role `Student` (Leader của team).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `teamId` | `guid` | Có | ID của team cần giải tán. |

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string",
  "timestampUtc": "datetime",
  "data": null,
  "message": "TEAM_DISBANDED_SUCCESSFULLY"
}
```

## Business rules
- Chỉ Leader mới có quyền disband team.
- Sau khi disband, tất cả thành viên mất quyền truy cập team.
- Tất cả thành viên được set `IsDisable = true`, `Status = Inactive`.
- Team được set `IsDisable = true`, `CanEdit = false`.
- Các lời mời (Invitations) của team sẽ không còn hiệu lực.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | ONLY_LEADER_CAN_DISBAND |
| 400 | BAD_REQUEST | TEAM_ALREADY_DISBANDED |
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
