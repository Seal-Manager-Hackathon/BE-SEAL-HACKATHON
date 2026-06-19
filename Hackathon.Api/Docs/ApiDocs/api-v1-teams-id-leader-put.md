# Student chuyển quyền leader

## Tác dụng
Leader hiện tại của team có thể chuyển quyền leader cho một thành viên khác đang ở trong team. Sau đó, bản thân sẽ trở thành member bình thường.

## URL
`PUT /api/v1/teams/{teamId}/leader`

## Authorization
Yêu cầu access token hợp lệ với role `Student`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `teamId` | `guid` | Có | Id của team. |

## Query parameters
Không có.

## Ví dụ request
```http
PUT /api/v1/teams/00000000-0000-0000-0000-000000000000/leader
Authorization: Bearer {accessToken}
Content-Type: application/json

{
    "newLeaderId": "guid"
}
```

## Request body
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `newLeaderId` | `guid` | Có | Id của thành viên sẽ được lên làm leader mới. |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "message": "LEADER_TRANSFERRED_SUCCESSFULLY"
  }
}
```

## Business rules
- Người gọi API phải là leader của team (`IsLeader = true`).
- `newLeaderId` phải là một thành viên đang hoạt động trong team.
- Không thể tự truyền bản thân làm `newLeaderId` được nữa.
- `CanEdit` của team phải là `true`.
- Có sử dụng **Database Transaction**.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | NEW_LEADER_ID_REQUIRED, ALREADY_THE_LEADER |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | ONLY_TEAM_LEADER_CAN_TRANSFER_ROLE, TEAM_MEMBER_LOCKED |
| 404 | NOT_FOUND | TEAM_NOT_FOUND, NEW_LEADER_NOT_IN_TEAM |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
