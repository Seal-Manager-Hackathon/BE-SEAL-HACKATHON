# Admin create event

## Tác dụng
Admin khởi tạo một sự kiện Hackathon mới trên hệ thống.

## URL
`POST /api/admin/events`

## Authorization
Yêu cầu access token hợp lệ và role `Admin`.

## Request body
```json
{
  "name": "string",
  "description": "string|null",
  "startTime": "datetimeoffset|null",
  "endTime": "datetimeoffset|null",
  "registerLimitTime": "datetimeoffset|null",
  "limitTeam": 100,
  "minMember": 3,
  "maxMember": 5,
  "numberRound": 3,
  "season": "string|null",
  "status": "Draft"
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
    "description": "string|null",
    "startTime": "datetimeoffset|null",
    "endTime": "datetimeoffset|null",
    "registerLimitTime": "datetimeoffset|null",
    "limitTeam": 100,
    "minMember": 3,
    "maxMember": 5,
    "numberRound": 3,
    "season": "string|null",
    "status": "Draft",
    "message": "EVENT_CREATED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ Admin được tạo event.
- Tên event là bắt buộc và không được trùng với event chưa bị soft-disable.
- `startTime` phải trước `endTime` nếu cả hai được truyền.
- `registerLimitTime` phải trước hoặc bằng `startTime` nếu cả hai được truyền.
- `minMember` không được lớn hơn `maxMember`.
- `limitTeam`, `minMember`, `maxMember`, `numberRound` phải là số dương nếu được truyền.
- Event mới nên bắt đầu với trạng thái `Draft` nếu request không truyền status.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | ADMIN_REQUIRED |
| 400 | BAD_REQUEST | EVENT_NAME_REQUIRED |
| 400 | BAD_REQUEST | INVALID_EVENT_TIME_RANGE |
| 400 | BAD_REQUEST | INVALID_MEMBER_LIMIT |
| 400 | BAD_REQUEST | INVALID_TEAM_LIMIT |
| 409 | CONFLICT | EVENT_NAME_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
