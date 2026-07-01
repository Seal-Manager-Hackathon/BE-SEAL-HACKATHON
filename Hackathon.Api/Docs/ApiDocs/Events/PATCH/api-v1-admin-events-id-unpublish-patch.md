# Hủy công bố sự kiện (Admin Unpublish Event)

## Tác dụng
Chuyển trạng thái sự kiện từ `Published` (Đã công bố) quay ngược lại trạng thái `Draft` (Nháp) để tạm ẩn giải đấu khỏi giao diện thí sinh khi cần điều chỉnh khẩn cấp.

## URL
`PATCH /api/v1/admin/events/{eventId}/unpublish`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | ID của event cần hủy công bố. |

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
  "message": "EVENT_UNPUBLISHED_SUCCESSFULLY"
}
```

## Business rules
- Event phải tồn tại trong DB, nếu không báo lỗi `EVENT_NOT_FOUND`.
- Chỉ chấp nhận hủy công bố khi event đang ở trạng thái `Published`. Nếu event đang ở trạng thái `Draft` hoặc `Closed` thì từ chối hành động và báo lỗi `EVENT_NOT_IN_PUBLISHED_STATUS`.
- Khi unpublish: gán `Status = Draft` (giá trị enum `0`) và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.

### Bảng trạng thái EventStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Draft | Nháp (Thí sinh không nhìn thấy) |
| `1` | Published | Đang diễn ra / Mở đăng ký |
| `2` | Closed | Đã đóng / Kết thúc giải đấu |

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 409 | CONFLICT | EVENT_NOT_IN_PUBLISHED_STATUS |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.EventsController`.
- Đã thêm method `UnpublishEvent(Guid eventId)` trong `Hackathon.Service.Events.IService`.
- Đã implement logic trong `Hackathon.Service.Events.Service`.
- Endpoint dùng route `PATCH /api/v1/admin/events/{eventId}/unpublish` và `AdminPolicy`.
