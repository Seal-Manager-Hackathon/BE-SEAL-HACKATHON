# Student cập nhật team

## Tác dụng
Leader của team có thể cập nhật tên của team, khi team đó chưa bị khóa (`CanEdit` đang là `true`).

## URL
`PUT /api/v1/teams/{teamId}`

## Authorization
Yêu cầu access token hợp lệ với role `Student`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `teamId` | `guid` | Có | Id của team cần cập nhật. |

## Query parameters
Không có.

## Ví dụ request
```http
PUT /api/v1/teams/00000000-0000-0000-0000-000000000000
Authorization: Bearer {accessToken}
Content-Type: application/json

{
    "teamName": "Tên team mới"
}
```

## Request body
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `teamName` | `string` | Có | Tên mới của team. |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "message": "TEAM_UPDATED_SUCCESSFULLY"
  }
}
```

## Business rules
- Người gọi API phải là leader của team (`IsLeader = true`).
- `CanEdit` của team phải là `true`.
- Tên team không được trùng lặp với team khác.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | TEAM_NAME_REQUIRED |
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 403 | FORBIDDEN | ONLY_TEAM_LEADER_CAN_UPDATE_TEAM, TEAM_CANNOT_BE_EDITED |
| 404 | NOT_FOUND | TEAM_NOT_FOUND, USER_NOT_FOUND |
| 409 | CONFLICT | TEAM_NAME_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
