# Get registration detail for review

## Tác dụng
Staff xem chi tiết đơn đăng ký và thông tin member để duyệt/từ chối.

## URL
`GET /api/staff/register-teams/{registerTeamId}`

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
    "teamId": "guid",
    "teamName": "string|null",
    "topicId": "guid",
    "topicTitle": "string|null",
    "trackId": "guid",
    "trackTitle": "string|null",
    "eventId": "guid",
    "eventName": "string|null",
    "description": "string|null",
    "status": "Pending | Approved | Rejected",
    "rejectionReason": "string|null",
    "isBanned": false,
    "createdAt": "datetimeoffset",
    "updatedAt": "datetimeoffset",
    "message": "REGISTER_TEAM_DETAIL_RETRIEVED_SUCCESSFULLY",
    "members": [
      {
        "userId": "guid",
        "email": "string",
        "firstName": "string|null",
        "lastName": "string|null",
        "phoneNumber": "string|null",
        "studentId": "string|null",
        "college": "string|null",
        "avatarUrl": "string|null",
        "bio": "string|null",
        "isLeader": true,
        "status": "Active"
      }
    ]
  }
}
```

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
