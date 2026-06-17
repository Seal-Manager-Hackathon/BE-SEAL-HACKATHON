# Update team

## Tác dụng
Cập nhật thông tin team.

## URL
`PATCH /api/v1/teams/{teamId}`

## Authorization
Yêu cầu access token hợp lệ.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `teamId` | `guid` | Có | Id của team cần cập nhật. |

## Request body
```json
{
  "name": "string",
  "description": "string|null"
}
```

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "id": "guid",
    "name": "string",
    "canEdit": true,
    "createdAt": "datetimeoffset",
    "members": [
      {
        "userId": "guid",
        "isLeader": true,
        "status": "string|null"
      }
    ]
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ team leader được cập nhật team.
- Team phải chưa bị soft-disable.
- Team chỉ được cập nhật khi `CanEdit = true`.
- `name` không được rỗng và không được trùng theo rule hiện tại của hệ thống nếu có.
- Cập nhật `UpdatedAt` sau khi lưu thành công.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | TEAM_NAME_REQUIRED | Team name is required. |
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | ONLY_TEAM_LEADER_CAN_UPDATE_TEAM | Only team leader can update team. |
| 403 | TEAM_MEMBER_LOCKED | Team cannot be edited. |
| 404 | TEAM_NOT_FOUND | Team not found. |
| 409 | TEAM_NAME_ALREADY_EXISTS | Team name already exists. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
