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
      "id": 0,
      "name": "Mentor",
      "displayName": "Mentor"
    },
    {
      "id": 1,
      "name": "Judge",
      "displayName": "Judge"
    },
    {
      "id": 2,
      "name": "Staff",
      "displayName": "Staff"
    }
  ],
  "message": "SUCCESS"
}
```

### Bảng vai trò EventRoleEnum (Integer)
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |
| `2` | Staff | Nhân viên vận hành sự kiện |

## Business rules
- Không cần xác thực.
- Dữ liệu lấy từ bảng `EventRoles` trong database.
- Nếu bảng rỗng → trả về mảng `data: []` rỗng (HTTP 200).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 500 | INTERNAL_SERVER_ERROR | Lỗi database hoặc lỗi hệ thống khác |

## Trạng thái implement
- Đã implement trong `Hackathon.Api.Controllers.RolesController`.
- Route: `GET /api/v1/roles/event-roles`.
- Không yêu cầu authorization.
