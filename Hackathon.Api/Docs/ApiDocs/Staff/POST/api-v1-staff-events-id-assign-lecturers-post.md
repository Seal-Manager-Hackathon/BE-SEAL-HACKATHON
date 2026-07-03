# Staff assign lecturer to event

## Tác dụng
Staff phân công một `Lecturer` vào sự kiện với vai trò cụ thể (`Mentor` hoặc `Judge`). Dữ liệu được lưu vào bảng `AssignEvents`.

## URL
`POST /api/v1/staff/events/{eventId}/assign-lecturers`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id của sự kiện. |

## Request body
```json
{
  "lecturerId": "guid",
  "eventRoleId": "guid"
}
```
*Ghi chú*: `eventRoleId` là ID của record trong bảng `EventRoles` tương ứng với `Mentor (0)` hoặc `Judge (1)`.

## Response body
Response dùng `ApiResponseFactory.Base(...)`.
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
    "userId": "guid",
    "eventRoleId": "guid",
    "eventId": "guid"
  },
  "message": "LECTURER_ASSIGNED_TO_EVENT_SUCCESSFULLY"
}
```

## Business rules
- Người gọi phải là `Staff` hoặc `Admin`.
- Nếu là `Staff`, phải được phân công quản lý sự kiện này trước.
- `eventId` phải tồn tại và không bị disable.
- `lecturerId` phải tồn tại, có global role là `Lecturer` và không bị ban/disable.
- `eventRoleId` phải tồn tại trong bảng `EventRoles`.
- Một `Lecturer` **không được** vừa làm Mentor vừa làm Judge trong cùng một sự kiện (theo RULE: "A lecture should not be mentor and judge in the same event"). Nếu vi phạm trả lỗi `LECTURER_CANNOT_BE_BOTH_MENTOR_AND_JUDGE`.
- Nếu đã được phân công vai trò này rồi thì không tạo mới (trả lỗi conflict hoặc return thành công).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | FORBIDDEN / STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | LECTURER_NOT_FOUND |
| 404 | NOT_FOUND | EVENT_ROLE_NOT_FOUND |
| 409 | CONFLICT | LECTURER_ALREADY_ASSIGNED_THIS_ROLE |
| 409 | CONFLICT | LECTURER_CANNOT_BE_BOTH_MENTOR_AND_JUDGE |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |