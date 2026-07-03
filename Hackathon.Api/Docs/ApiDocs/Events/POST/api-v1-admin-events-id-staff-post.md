# Admin phân công staff vào event

## Tác dụng
Admin phân công một `Staff` (nhân viên vận hành) vào một event cụ thể. Staff được phân công sẽ có quyền truy cập các API vận hành của event đó (duyệt đơn đăng ký, quản lý bốc thăm, v.v.). Bản ghi được tạo trong bảng `AssignEvents` với `EventRoleId = null` (vì staff không có event role như Mentor/Judge).

## URL
`POST /api/v1/admin/events/{eventId}/staff`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `eventId` | `guid` | Có | Id của event cần phân công staff. |

## Request body
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

| Field | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `userId` | `guid` | Có | Id của staff cần phân công vào event. |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 201,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "id": "guid"
  },
  "message": "STAFF_ASSIGNED_TO_EVENT_SUCCESSFULLY"
}
```

## Business rules
- Người dùng phải có role `Admin` (xác thực qua `[Authorize(Policy = JwtExtensions.AdminPolicy)]`).
- `eventId` phải tồn tại trong hệ thống và không bị disable, nếu không trả `EVENT_NOT_FOUND`.
- `userId` phải tồn tại trong hệ thống, nếu không trả `USER_NOT_FOUND`.
- User được phân công phải có `Role = Staff` trong bảng `Users`, nếu không trả `USER_MUST_BE_STAFF`.
- User không được phép đã bị `IsDisable = true`.
- Không được phân công staff trùng (cùng `UserId` + `EventId` + `IsDisable = false`), nếu không trả `STAFF_ALREADY_ASSIGNED_TO_EVENT`.
- Bản ghi được tạo với `EventRoleId = null` để phân biệt với phân công Lecturer (Mentor/Judge).
- `CreatedAt` và `UpdatedAt` được set theo thời gian hiện tại (`DateTimeOffset.UtcNow`).
- Staff được phân công sẽ được kiểm tra quyền khi truy cập API vận hành event qua `EnsureStaffAssignedToEvent`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 400 | BAD_REQUEST | USER_MUST_BE_STAFF |
| 409 | CONFLICT | STAFF_ALREADY_ASSIGNED_TO_EVENT |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
