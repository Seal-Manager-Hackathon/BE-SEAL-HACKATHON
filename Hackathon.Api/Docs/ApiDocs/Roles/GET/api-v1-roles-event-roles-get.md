# Lấy danh sách Event Role

## Tác dụng
Lấy danh sách tất cả Event Role (`EventRoleEnum`) từ database. Không yêu cầu phân quyền.

## URL
`GET /api/v1/roles/event-roles`

## Authorization
Không yêu cầu — ai cũng có thể gọi.

## Path parameters
Không có.

## Query parameters
Không có.

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
  "data": [
    {
      "roleId": "55555555-5555-5555-5555-555555555555",
      "id": 0,
      "name": "Mentor",
      "displayName": "Mentor"
    },
    {
      "roleId": "66666666-6666-6666-6666-666666666666",
      "id": 1,
      "name": "Judge",
      "displayName": "Judge"
    },
    {
      "roleId": "77777777-7777-7777-7777-777777777777",
      "id": 2,
      "name": "Staff",
      "displayName": "Staff"
    }
  ],
  "message": "SUCCESS"
}
```

## Business rules
- Không cần xác thực.
- Dữ liệu lấy từ bảng `EventRoles` trong database.

## Lỗi có thể xảy ra
Không có — API luôn trả về 200 OK.

## Trạng thái implement
- Đã implement trong `Hackathon.Api.Controllers.RolesController`.
- Route: `GET /api/v1/roles/event-roles`.
- Không yêu cầu authorization.
