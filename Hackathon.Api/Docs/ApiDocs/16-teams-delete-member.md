# Delete member in team

## Tác dụng
Xóa một member khỏi team bằng cách soft-disable hoặc cập nhật trạng thái member trong `TeamDetails`.

## URL
`DELETE /api/teams/{teamId}/members/{userId}`

## Authorization
Yêu cầu access token hợp lệ.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `teamId` | `guid` | Có | Id của team. |
| `userId` | `guid` | Có | Id của member cần xóa khỏi team. |

## Request body
Không có.

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "teamId": "guid",
    "userId": "guid",
    "message": "TEAM_MEMBER_DELETED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ team leader được xóa member khỏi team.
- Team phải chưa bị soft-disable.
- Team chỉ được chỉnh sửa khi `CanEdit = true`.
- Không cho xóa team leader bằng API này.
- Member cần xóa phải thuộc team và chưa bị soft-disable.
- Khi xóa member, cập nhật `TeamDetails.IsDisable = true` hoặc status tương ứng theo rule hệ thống.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | ONLY_TEAM_LEADER_CAN_DELETE_MEMBER | Only team leader can delete member. |
| 403 | TEAM_MEMBER_LOCKED | Team cannot be edited. |
| 400 | CANNOT_DELETE_TEAM_LEADER | Team leader cannot be deleted by this API. |
| 404 | TEAM_NOT_FOUND | Team not found. |
| 404 | TEAM_MEMBER_NOT_FOUND | Team member not found. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
