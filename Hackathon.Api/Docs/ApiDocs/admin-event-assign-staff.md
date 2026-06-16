# Admin assign event staff

## Tác dụng
Admin phân công nhân sự phụ trách cuộc thi hoặc bảng đấu, bao gồm Staff, Judge hoặc Mentor. Dữ liệu phân công chính lưu ở `AssignEvents`; nếu phân công theo bảng đấu thì dùng thêm `AssignTracks`.

## URL
`POST /api/admin/events/{eventId}/assignments`

## Authorization
Yêu cầu access token hợp lệ và role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id event cần phân công nhân sự. |

## Request body
```json
{
  "userId": "guid",
  "eventRoleId": "guid|null",
  "trackIds": ["guid"]
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
    "assignEventId": "guid",
    "eventId": "guid",
    "userId": "guid",
    "eventRoleId": "guid|null",
    "trackIds": ["guid"],
    "message": "EVENT_ASSIGNMENT_CREATED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ Admin được phân công nhân sự.
- Event phải tồn tại và chưa bị soft-disable.
- User được phân công phải tồn tại và chưa bị soft-disable.
- Staff phụ trách event có thể chỉ cần record `AssignEvents`.
- Mentor/Judge phải có `EventRoles` tương ứng và có thể được assign vào một hoặc nhiều track qua `AssignTracks`.
- Track nếu truyền phải thuộc cùng event.
- Không tạo trùng assignment active cho cùng user/event/role/track.
- Global role của user phải phù hợp với loại phân công nếu service áp dụng rule: Staff dùng `RoleEnum.Staff`, Judge/Mentor dùng `RoleEnum.Lecturer`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | ADMIN_REQUIRED |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 404 | NOT_FOUND | EVENT_ROLE_NOT_FOUND |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 400 | BAD_REQUEST | INVALID_ASSIGNMENT_ROLE |
| 400 | BAD_REQUEST | TRACK_NOT_IN_EVENT |
| 409 | CONFLICT | ASSIGNMENT_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
