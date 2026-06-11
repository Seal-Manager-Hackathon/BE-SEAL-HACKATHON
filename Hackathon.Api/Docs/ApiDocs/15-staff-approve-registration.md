# Approve registration

## Tác dụng
Staff duyệt đơn đăng ký, khóa chỉnh sửa team và gửi thông báo cho leader.

## URL
`PATCH /api/staff/register-teams/{registerTeamId}/approve`

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
    "status": "Approved",
    "rejectionReason": null,
    "isBanned": false,
    "createdAt": "datetimeoffset",
    "updatedAt": "datetimeoffset",
    "message": "REGISTER_TEAM_APPROVED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Register team phải tồn tại và chưa bị disable.
- Staff chỉ được duyệt đơn thuộc event mà mình được phân công.
- Chỉ đơn đang `Pending` mới được duyệt.
- Khi duyệt thành công, trạng thái đơn chuyển sang `Approved`, xóa lý do từ chối và team tiếp tục bị khóa chỉnh sửa member.
- Hệ thống gửi thông báo cho team leader sau khi duyệt.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 409 | CONFLICT | REGISTER_TEAM_NOT_PENDING |
| 404 | NOT_FOUND | TEAM_LEADER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
