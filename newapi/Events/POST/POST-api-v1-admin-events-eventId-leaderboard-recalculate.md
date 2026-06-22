# Tính toán lại bảng xếp hạng (Recalculate Leaderboard)

## Tác dụng
Cho phép BTC kích hoạt chạy tính toán/đồng bộ lại bảng xếp hạng event dựa trên điểm số thực tế của các round thi đấu.

## URL
`POST /api/v1/admin/events/{eventId}/leaderboard/recalculate`

## Quyền
Admin hoặc Staff (Yêu cầu đăng nhập tài khoản BTC)

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
  "Value": "LEADERBOARD_RECALCULATED",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Event phải tồn tại trong DB, không bị soft-disable.
- BTC kiểm tra quyền gán của Staff.
- Điểm event = tổng điểm trung bình của các round đấu mà team đã thi đấu (BR-LB-03).
- Cập nhật trường `Score` trong bảng `LeaderBoardDetails` của từng team tương ứng.
- Hành động cập nhật bảng xếp hạng hàng loạt bắt buộc bọc trong một **Database Transaction**.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Bạn không được phân công quản lý sự kiện này.",
  "MessageCode": "FORBIDDEN",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không được gán quyền quản lý sự kiện. |
| 404 | EVENT_NOT_FOUND | Event không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
