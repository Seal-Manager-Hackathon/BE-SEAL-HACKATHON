# Tính toán lại bảng xếp hạng (Recalculate Leaderboard)

## Tác dụng
Cho phép BTC kích hoạt chạy tính toán/đồng bộ lại bảng xếp hạng event dựa trên điểm số thực tế của các round thi đấu.

## URL
`POST /api/v1/admin/events/{eventId}/leaderboard/recalculate`

## Authorization
Yêu cầu access token hợp lệ với role `Admin` hoặc `Staff`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
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
  "message": "LEADERBOARD_RECALCULATED"
}
```

## Business rules
- Event phải tồn tại trong DB, không bị soft-disable.
- BTC kiểm tra quyền gán của Staff.
- Điểm event = tổng điểm trung bình của các round đấu mà team đã thi đấu (BR-LB-03).
- Cập nhật trường `Score` trong bảng `LeaderBoardDetails` của từng team tương ứng.
- Hành động cập nhật bảng xếp hạng hàng loạt bắt buộc bọc trong một **Database Transaction**.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.EventsController`.
- Route: `POST /api/v1/admin/events/{eventId}/leaderboard/recalculate`.
- Sử dụng policy `StaffOrAdminPolicy`.
- Message: `LEADERBOARD_RECALCULATED`.
- Entity: `LeaderBoards`, `LeaderBoardDetails`.
