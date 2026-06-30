# Admin xóa vòng thi (Admin Delete Round)

## Tác dụng
Admin xóa mềm (soft delete) một vòng thi (Round) — đặt `IsDisable = true` để ẩn round khỏi hệ thống.

## URL
`DELETE /api/v1/admin/rounds/{roundId}`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.
Policy: `AdminPolicy`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `roundId` | `guid` | Có | ID của vòng thi cần xóa. |

## Request body
Không có.

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
  "message": "ROUND_DELETED_SUCCESSFULLY"
}
```

## Business rules
- Người gọi phải có role `Admin`.
- `roundId` phải là GUID hợp lệ trên path.
- Round phải tồn tại. Nếu không, trả `404 Not Found` (`ROUND_NOT_FOUND`).
- Xóa mềm: chỉ đặt `IsDisable = true`, round không bị xóa vĩnh viễn khỏi database.
- Các bản ghi liên quan (`RoundDetails`, `Submissions`, `Scores`, `CriteriaTemplates`, `CriteriaItems`) giữ nguyên, không bị xóa cascade.
- `UpdatedAt` được cập nhật theo thời gian hiện tại (`DateTimeOffset.UtcNow`).
- Nếu round đã bị disable (`IsDisable == true`), vẫn trả `ROUND_DELETED_SUCCESSFULLY` (idempotent) và không thay đổi dữ liệu khác.
- Sau khi round bị disable, public API `GET /api/v1/rounds?eventId={eventId}` và `GET /api/v1/rounds/{roundId}` không nên trả round này.
- Admin vẫn xem được round đã disable qua `GET /api/v1/admin/events/{eventId}/rounds?isDisable=true`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- **Đã implement** trong `Hackathon.Api.Controllers.AdminController`.
- Route hiện có: `DELETE /api/v1/admin/rounds/{roundId}`.
- Sử dụng policy `AdminPolicy` (attribute trên controller class).
- Service: `Hackathon.Service.Admin.Service.DeleteRound()`.
- Entity: `Rounds` — tìm bằng `FirstOrDefaultAsync(x => x.Id == roundId)` (không check IsDisable), set `IsDisable = true`, `UpdatedAt = UtcNow`.
- Idempotent: nếu round đã disable vẫn trả success, không throw lỗi.
