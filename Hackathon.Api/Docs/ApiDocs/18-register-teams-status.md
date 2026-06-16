# Get my registration status

## Tác dụng
Member xem trạng thái đơn đăng ký team và lý do bị từ chối nếu có.

## URL
`GET /api/register-teams/{registerTeamId}/status`

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
    "eventId": "guid",
    "eventName": "string|null",
    "description": "string|null",
    "status": "Pending | Approved | Rejected",
    "rejectionReason": "string|null",
    "isBanned": false,
    "createdAt": "datetimeoffset",
    "updatedAt": "datetimeoffset",
    "message": "REGISTER_TEAM_STATUS_RETRIEVED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ member thuộc team của đơn đăng ký mới được xem trạng thái.
- Trả trạng thái hiện tại của đơn: `Pending`, `Approved` hoặc `Rejected`.
- Nếu đơn bị từ chối, response có kèm `rejectionReason`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 403 | FORBIDDEN | REGISTER_TEAM_NOT_VISIBLE_TO_USER |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
