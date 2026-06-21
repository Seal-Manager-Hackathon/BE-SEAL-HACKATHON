# Admin publish event

## Tác dụng
Admin publish một event — chuyển trạng thái event từ `Draft` sang `Published` để event có thể được hiển thị và cho phép đăng ký.

## URL
`PATCH /api/v1/admin/events/{eventId}/publish`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event cần publish. |

## Request body
Không cần request body.

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
  "message": "EVENT_PUBLISHED_SUCCESSFULLY"
}
```

## Business rules
- Admin phải đăng nhập bằng access token hợp lệ.
- Endpoint này bắt buộc Admin-only qua `[Authorize(Policy = JwtExtensions.AdminPolicy)]`.
- `eventId` là bắt buộc trên path.
- Event phải tồn tại, nếu không trả `EVENT_NOT_FOUND`.
- Event phải đang ở trạng thái `Draft`, nếu không trả `EVENT_NOT_IN_DRAFT_STATUS`.
- Khi publish: `Status = Published`, `UpdatedAt` được cập nhật theo thời gian hiện tại (`DateTimeOffset.UtcNow`).
- Khi publish thành công chỉ trả message `EVENT_PUBLISHED_SUCCESSFULLY`, không trả data event.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 409 | CONFLICT | EVENT_NOT_IN_DRAFT_STATUS |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.EventsController`.
- Đã thêm method `PublishEvent(Guid eventId)` trong `Hackathon.Service.Events.IService`.
- Đã implement logic publish trong `Hackathon.Service.Events.Service`.
- Endpoint dùng route `PATCH /api/v1/admin/events/{eventId:guid}/publish` và `AdminPolicy`.
- Response thành công chỉ trả message `EVENT_PUBLISHED_SUCCESSFULLY`.
