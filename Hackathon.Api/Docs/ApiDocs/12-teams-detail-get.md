# Get team details

## Tác dụng
Lấy thông tin chi tiết team và danh sách member trong team.

## URL
`GET /api/v1/teams/{teamId}`

## Authorization
Yêu cầu access token hợp lệ.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `teamId` | `guid` | Có | Id của team cần xem chi tiết. |

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
- Chỉ member của team hoặc staff có quyền liên quan mới được xem chi tiết team.
- Team và member bị soft-disable không được trả về mặc định.
- Danh sách member chỉ lấy `TeamDetails` đang active và chưa disable.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | TEAM_NOT_VISIBLE_TO_USER | User cannot view this team. |
| 404 | TEAM_NOT_FOUND | Team not found. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
