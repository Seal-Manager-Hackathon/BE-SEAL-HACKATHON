# Công bố kết quả bốc thăm (Publish Draw Results)

## Tác dụng
Cho phép BTC chính thức công bố kết quả bốc thăm đề bài và phân chia bảng đấu lên giao diện public của thí sinh.

## URL
`PATCH /api/v1/staff/events/{eventId}/draw-results/publish`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của sự kiện.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "DRAW_RESULTS_PUBLISHED",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Event phải tồn tại trong DB và chưa bị disable.
- BTC kiểm tra xem đã gán đầy đủ Track và Topic cho toàn bộ các team được duyệt (`Approved`) của event chưa. Nếu phát hiện có team chưa gán, từ chối công bố và báo lỗi `UNASSIGNED_TEAMS_FOUND`.
- Đánh dấu trạng thái công bố kết quả bốc thăm của Event trong DB. *DB hiện chưa thiết lập trường công bố bốc thăm.*

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Vẫn còn một số đội chưa được phân bảng đấu hoặc đề bài.",
  "MessageCode": "UNASSIGNED_TEAMS_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không có quyền quản lý sự kiện. |
| 404 | EVENT_NOT_FOUND | Event không tồn tại. |
| 409 | UNASSIGNED_TEAMS_FOUND | Phát hiện đội thi đã duyệt nhưng chưa phân đề/phân bảng đấu. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
