# Staff remove lecturer assignment

## Tác dụng
Staff gỡ bỏ phân công của một Giảng viên (`Lecturer`) khỏi sự kiện (thực hiện soft-disable record trong bảng `AssignEvents`). 

## URL
`DELETE /api/v1/staff/assign-events/{id}`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `id` | `guid` | Có | Id của record phân công (`AssignEvents.Id`). |

## Request body
Không có.

## Ví dụ request
```http
DELETE /api/v1/staff/assign-events/00000000-0000-0000-0000-000000000000
Authorization: Bearer {accessToken}
```

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
    "id": "guid"
  },
  "message": "LECTURER_ASSIGNMENT_REMOVED_SUCCESSFULLY"
}
```

## Business rules
- Người gọi phải là `Staff` hoặc `Admin`.
- `id` (AssignEventId) phải tồn tại và chưa bị disable.
- Nếu là `Staff`, phải được phân công quản lý sự kiện tương ứng với `AssignEvent` đó.
- Set `IsDisable = true` cho record trong bảng `AssignEvents`.
- **Quan trọng:** Nếu Lecturer này đang là `Judge` và đã được phân công chấm thi cho các `Track` (bảng `AssignTracks`), thì hệ thống cũng phải tự động tìm các `AssignTracks` liên quan đến `AssignEventId` này và set `IsDisable = true` (gỡ luôn quyền chấm thi ở các track).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | ASSIGN_EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |