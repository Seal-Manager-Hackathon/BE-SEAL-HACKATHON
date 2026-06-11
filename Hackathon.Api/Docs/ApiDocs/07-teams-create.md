# Create team

## Tác dụng
Tạo team mới và tự thêm user hiện tại làm leader.

## URL
`POST /api/teams`

## Request body
```json
{
  "name": "string"
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
    "members": [
      {
        "userId": "guid",
        "isLeader": true,
        "status": "Active"
      }
    ],
    "createdAt": "datetimeoffset",
    "message": "TEAM_CREATED_SUCCESSFULLY"
  }
}
```

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 400 | BAD_REQUEST | TEAM_NAME_REQUIRED |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 403 | FORBIDDEN | USER_NOT_VERIFIED |
| 400 | BAD_REQUEST | USER_PROFILE_NOT_COMPLETED |
| 409 | CONFLICT | TEAM_NAME_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
