# Công bố bảng xếp hạng chung cuộc (Publish Leaderboard)

## Tác dụng
Cho phép BTC chính thức công bố bảng xếp hạng chung cuộc lên giao diện public để thí sinh xem kết quả.

## URL
`PATCH /api/v1/admin/events/{eventId}/leaderboard/publish`

## Authorization
Yêu cầu access token hợp lệ với role `Admin` hoặc `Staff`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | ID của sự kiện. |

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
  "message": "LEADERBOARD_PUBLISHED"
}
```

## Business rules
- Event phải tồn tại trong DB.
- BTC kiểm tra quyền của Staff.
- Đánh dấu cờ đã công bố bảng xếp hạng chung cuộc (`IsPublished = true`).
- Nếu chưa có leaderboard cho event, trả lỗi `LEADERBOARD_NOT_FOUND`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | LEADERBOARD_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.EventsController`.
- Đã thêm method `PublishLeaderboard(Guid eventId)` trong `Hackathon.Service.Events.IService`.
- Đã implement logic trong `Hackathon.Service.Events.Service`.
- Endpoint dùng route `PATCH /api/v1/admin/events/{eventId}/leaderboard/publish` và `StaffOrAdminPolicy`.
- Đã thêm cột `IsPublished` trong entity `LeaderBoards` qua migration `add_leaderboard_flags`.
