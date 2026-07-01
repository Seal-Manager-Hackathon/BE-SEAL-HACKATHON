# Admin đổi vai trò người dùng

## Tác dụng
Cho phép Admin thay đổi vai trò (Role) của bất kỳ người dùng nào. Admin có toàn quyền — có thể set bất kỳ role nào (Admin, Staff, Lecturer, Student).

## URL
`PATCH /api/v1/admin/users/{userId}/role`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `userId` (Guid, Bắt buộc): ID của người dùng cần đổi vai trò.

## Request Body
```json
{
  "role": 0
}
```

### Bảng giá trị RoleEnum (Integer)
| Giá trị (Value) | Vai trò (Role) |
| :--- | :--- |
| `0` | Admin |
| `1` | Staff |
| `2` | Student |
| `3` | Lecturer |

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "USER_ROLE_UPDATED_SUCCESSFULLY",
  "data": null
}
```

## Business rules
- User phải tồn tại và không bị disable, nếu không báo lỗi `USER_NOT_FOUND`.
- Nếu role mới trùng với role hiện tại → báo lỗi `ROLE_ALREADY_SET`.
- Admin có toàn quyền set bất kỳ role nào.
- Chỉ thay đổi trường `Role` — các dữ liệu khác của user vẫn giữ nguyên.

## Lỗi có thể xảy ra
```json
{
  "title": "Not Found",
  "status": 404,
  "message": "USER_NOT_FOUND",
  "messageCode": "NOT_FOUND",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | ROLE_ALREADY_SET |
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | ADMIN_ROLE_REQUIRED |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.AdminController`.
- Route: `PATCH /api/v1/admin/users/{userId}/role`.
- Sử dụng policy `AdminPolicy`.
