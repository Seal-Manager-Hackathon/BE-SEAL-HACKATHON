# Admin delete event

## Tác dụng
Admin xóa mềm (soft delete) một event — đặt `IsDisable = true` để ẩn event khỏi hệ thống.

## URL
`DELETE /api/v1/admin/events/{eventId}`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event cần xóa. |

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
  "message": "EVENT_DELETED_SUCCESSFULLY"
}
```

## Business rules
- Admin phải đăng nhập bằng access token hợp lệ.
- Endpoint này bắt buộc Admin-only qua `[Authorize(Policy = JwtExtensions.AdminPolicy)]`.
- `eventId` là bắt buộc trên path.
- Event phải tồn tại, nếu không trả `EVENT_NOT_FOUND`.
- Xóa mềm: chỉ đặt `IsDisable = true`, event sẽ không bị xóa vĩnh viễn khỏi database.
- Các bản ghi liên quan (RegisterTeams, AssignEvents, ...) giữ nguyên, không bị ảnh hưởng.
- `UpdatedAt` được cập nhật theo thời gian hiện tại (`DateTimeOffset.UtcNow`).
- Nếu event đã bị disable (`IsDisable == true`), vẫn trả `EVENT_DELETED_SUCCESSFULLY` (idempotent).
- Khi xóa thành công chỉ trả message `EVENT_DELETED_SUCCESSFULLY`, không trả data event.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.EventsController`.
- Đã thêm method `DeleteEvent(Guid eventId)` trong `Hackathon.Service.Events.IService`.
- Đã implement logic soft delete trong `Hackathon.Service.Events.Service`.
- Endpoint dùng route `DELETE /api/v1/admin/events/{eventId:guid}` và `AdminPolicy`.
- Response thành công chỉ trả message `EVENT_DELETED_SUCCESSFULLY`.
