# Register team for event

## Tác dụng
Cho phép team đăng ký tham gia một event. Sau khi đăng ký, đơn sẽ ở trạng thái `Pending` chờ Staff/Admin duyệt.

## URL
`POST /api/v1/register-teams`

## Authorization
Yêu cầu access token hợp lệ (Student đã đăng nhập).

## Request body
```json
{
  "eventId": "guid",
  "teamId": "guid"
}
```

| Field | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event muốn đăng ký. |
| `teamId` | `guid` | Có | Id của team sẽ tham gia event. |

## Response body
Response dùng `ApiResponseFactory.Base(data)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "id": "guid",
    "teamId": "guid",
    "eventId": "guid",
    "status": 0
  },
  "message": "REGISTERED_SUCCESSFULLY"
}
```

## Business rules
- Student phải đăng nhập và là leader của team.
- Event phải tồn tại, không bị disable và đang ở trạng thái `Published`.
- Team không được đăng ký event đã tham gia trước đó.
- Mỗi team chỉ được đăng ký một event một lần.
- Nếu team đã bị khóa (`CanEdit = false`) tức đang tham gia một event khác, không thể đăng ký thêm.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | ACCESS_TOKEN_IS_MISSING |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 400 | BAD_REQUEST | TEAM_ALREADY_REGISTERED |
| 400 | BAD_REQUEST | EVENT_NOT_PUBLISHED |
| 400 | BAD_REQUEST | TEAM_CANNOT_REGISTER |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
