# Khóa bảng xếp hạng chung cuộc (Lock Leaderboard)

## Tác dụng
Cho phép BTC khóa bảng xếp hạng chung cuộc (chuyển sang chế độ Read-only vĩnh viễn sau khi kết thúc giải đấu).

## URL
`PATCH /api/v1/admin/events/{eventId}/leaderboard/lock`

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
  "message": "LEADERBOARD_LOCKED"
}
```

## Business rules
- Event phải tồn tại trong DB.
- BTC kiểm tra quyền của Staff.
- Đánh dấu trạng thái khóa bảng xếp hạng (`IsLocked = true`). Kể từ thời điểm này, cấm toàn bộ các thao tác chỉnh sửa giải thưởng, sửa đổi điểm số chung cuộc hay chạy tính toán lại (BR-SCO-07, BR-LB-06).
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
- Đã thêm method `LockLeaderboard(Guid eventId)` trong `Hackathon.Service.Events.IService`.
- Đã implement logic trong `Hackathon.Service.Events.Service`.
- Endpoint dùng route `PATCH /api/v1/admin/events/{eventId}/leaderboard/lock` và `StaffOrAdminPolicy`.
- Đã thêm cột `IsLocked` trong entity `LeaderBoards` qua migration `add_leaderboard_flags`.
