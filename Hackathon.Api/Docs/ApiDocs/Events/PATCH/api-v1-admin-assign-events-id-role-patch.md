# Thay đổi vai trò Giảng viên (Update Lecturer Event Role)

## Tác dụng
Cho phép Admin thay đổi vai trò (từ Mentor sang Judge hoặc ngược lại) của một giảng viên trong sự kiện.

## URL
`PATCH /api/v1/admin/assign-events/{id}/role`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `id` | `guid` | Có | ID của bản ghi phân công event. |

## Request body
```json
{
  "eventRole": "Mentor"
}
```

| Field | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `eventRole` | `string` | Có | Vai trò mới: `Mentor` hoặc `Judge`. |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": null,
  "message": "LECTURER_ROLE_UPDATED"
}
```

## Business rules
- Bản ghi `AssignEvents` phải tồn tại.
- Cập nhật trường `EventRoleId` của bản ghi chỉ định sang vai trò mới.
- Hệ thống tự động kiểm tra và gỡ bỏ toàn bộ phân công track cũ (`AssignTracks`) của giảng viên này để tránh sai lệch dữ liệu phân bảng đấu (vì đổi vai trò từ Judge sang Mentor hoặc ngược lại sẽ thay đổi hoàn toàn nghiệp vụ trên bảng đấu).

### Bảng vai trò EventRoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |
| `2` | Staff | Nhân viên vận hành sự kiện |

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 400 | BAD_REQUEST | INVALID_EVENT_ROLE |
| 404 | NOT_FOUND | ASSIGN_EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.EventsController`.
- Đã thêm method `UpdateLecturerRole(Guid id, UpdateLecturerRoleRequest request)` trong `Hackathon.Service.Events.IService`.
- Request dùng `eventRole` (string: `Mentor`/`Judge`), tự động lookup `EventRoles` để gán `EventRoleId`.
- Khi đổi role, tự động soft-delete toàn bộ `AssignTracks` cũ của assignment đó.
- Endpoint dùng route `PATCH /api/v1/admin/assign-events/{id}/role` và `AdminPolicy`.
