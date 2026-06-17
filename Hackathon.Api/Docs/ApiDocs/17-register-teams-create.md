# Register team for event

## Tác dụng
Leader gửi đơn đăng ký team tham gia event.

## URL
`POST /api/v1/register-teams`

## Request body
```json
{
  "teamId": "guid",
  "eventId": "guid",
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
    "teamId": "guid",
    "teamName": "string|null",
    "eventId": "guid",
    "eventName": "string|null",
    "description": "string|null",
    "status": "Pending",
    "rejectionReason": "string|null",
    "isBanned": false,
    "createdAt": "datetimeoffset",
    "updatedAt": "datetimeoffset",
    "message": "REGISTER_TEAM_SUBMITTED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- API này chỉ đăng ký team vào `Event`, không đăng ký thông qua `Topic`.
- `teamId` và `eventId` là bắt buộc.
- Team phải tồn tại, chưa bị disable và còn được chỉnh sửa member (`canEdit = true`).
- Chỉ leader đang active của team mới được gửi đơn đăng ký.
- Event phải tồn tại, chưa bị disable và còn trong thời hạn đăng ký.
- Số lượng member active của team phải nằm trong khoảng `MinMember` và `MaxMember` của event.
- Tất cả member active trong team phải hoàn tất profile bắt buộc.
- Một member không được nằm trong đơn `Pending` hoặc `Approved` của team khác trong cùng event.
- Team không được có đơn `Pending` hoặc `Approved` khác trong cùng event.
- Nếu team từng bị `Rejected` trong event, lần đăng ký mới sẽ gửi lại đơn đó về trạng thái `Pending` và xóa lý do từ chối cũ.
- Sau khi gửi đơn thành công, team bị khóa chỉnh sửa member (`canEdit = false`) cho đến khi đơn bị từ chối hoặc có luồng mở khóa khác.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 400 | BAD_REQUEST | TEAM_ID_REQUIRED |
| 400 | BAD_REQUEST | EVENT_ID_REQUIRED |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 403 | FORBIDDEN | TEAM_MEMBER_LOCKED |
| 403 | FORBIDDEN | ONLY_TEAM_LEADER_CAN_REGISTER_TEAM |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 400 | BAD_REQUEST | EVENT_REGISTRATION_CLOSED |
| 400 | BAD_REQUEST | TEAM_MEMBER_COUNT_NOT_VALID |
| 400 | BAD_REQUEST | TEAM_MEMBER_PROFILE_NOT_COMPLETED |
| 409 | CONFLICT | MEMBER_ALREADY_REGISTERED_IN_EVENT |
| 409 | CONFLICT | TEAM_ALREADY_REGISTERED_IN_EVENT |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
