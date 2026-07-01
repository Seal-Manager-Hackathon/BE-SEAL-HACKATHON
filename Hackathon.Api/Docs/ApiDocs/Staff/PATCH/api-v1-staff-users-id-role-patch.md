# Staff đổi vai trò người dùng

## Tác dụng
Cho phép Staff thay đổi vai trò (Role) của người dùng. Staff **chỉ có thể set role Student hoặc Lecturer**, không thể set Admin hay Staff cho người khác.

## URL
`PATCH /api/v1/staff/users/{userId}/role`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` (StaffOrAdmin policy).

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `userId` (Guid, Bắt buộc): ID của người dùng cần đổi vai trò.

## Request Body
```json
{
  "role": 2
}
```

### Bảng giá trị RoleEnum (Integer)
| Giá trị (Value) | Vai trò (Role) |
| :--- | :--- |
| `2` | Student |
| `3` | Lecturer |

> **Lưu ý:** Staff không thể set role `0` (Admin) hoặc `1` (Staff). Nếu cố tình sẽ báo lỗi `FORBIDDEN`.

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
- Staff **chỉ được phép** set role `Student` (2) hoặc `Lecturer` (3). Cố gắng set `Admin` hoặc `Staff` → `FORBIDDEN`.
- Nếu role mới trùng với role hiện tại → báo lỗi `ROLE_ALREADY_SET`.
- Chỉ thay đổi trường `Role` — các dữ liệu khác của user vẫn giữ nguyên.

## Lỗi có thể xảy ra
```json
{
  "title": "Forbidden",
  "status": 403,
  "message": "FORBIDDEN",
  "messageCode": "FORBIDDEN",
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
| 403 | FORBIDDEN | Staff không thể set Admin/Staff role |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.Staff`.
- Route: `PATCH /api/v1/staff/users/{userId}/role`.
- Sử dụng policy `StaffOrAdminPolicy`.
