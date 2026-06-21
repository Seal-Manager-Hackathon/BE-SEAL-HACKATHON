# Student đuổi thành viên khỏi team

## Tác dụng
Leader của team có thể loại bỏ nhiều thành viên ra khỏi team. Trạng thái của các thành viên này trong `TeamDetails` sẽ chuyển thành disabled.

## URL
`DELETE /api/v1/teams/{teamId}/members`

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
DELETE /api/v1/teams/00000000-0000-0000-0000-000000000000/members
Authorization: Bearer {accessToken}
Content-Type: application/json

{
    "userIds": ["guid1", "guid2"]
}
```

## Request body
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `userIds` | `array of guid` | Có | Danh sách id của các thành viên cần loại bỏ. |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "message": "MEMBERS_REMOVED_SUCCESSFULLY"
  }
}
```

## Business rules
- Người gọi API phải là leader của team (`IsLeader = true`).
- Không được tự truyền `leaderId` của chính mình vào mảng xóa.
- `CanEdit` của team phải là `true`.
- Có sử dụng **Database Transaction**.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | USER_IDS_REQUIRED, CANNOT_REMOVE_YOURSELF |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT |
| 403 | FORBIDDEN | ONLY_TEAM_LEADER_CAN_REMOVE_MEMBER, TEAM_MEMBER_LOCKED, TEAM_LOCKED_DUE_TO_REGISTRATION_STATUS |
| 404 | NOT_FOUND | USER_NOT_FOUND, TEAM_NOT_FOUND, NO_MATCHING_MEMBERS_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
