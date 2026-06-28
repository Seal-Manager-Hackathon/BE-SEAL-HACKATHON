# Lấy danh sách Role hệ thống

## Tác dụng
Lấy danh sách tất cả Role hệ thống (`RoleEnum`). Không yêu cầu phân quyền.

## URL
`GET /api/v1/roles`

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
      "id": 0,
      "name": "Admin",
      "displayName": "Admin"
    },
    {
      "id": 1,
      "name": "Staff",
      "displayName": "Staff"
    },
    {
      "id": 2,
      "name": "Student",
      "displayName": "Student"
    },
    {
      "id": 3,
      "name": "Lecturer",
      "displayName": "Lecturer"
    }
  ],
  "message": "SUCCESS"
}
```

## Business rules
- Không cần xác thực.
- Trả về danh sách cố định từ enum `RoleEnum`.

## Lỗi có thể xảy ra
Không có — API luôn trả về 200 OK.

## Trạng thái implement
- Đã implement trong `Hackathon.Api.Controllers.RolesController`.
- Route: `GET /api/v1/roles`.
- Không yêu cầu authorization.
