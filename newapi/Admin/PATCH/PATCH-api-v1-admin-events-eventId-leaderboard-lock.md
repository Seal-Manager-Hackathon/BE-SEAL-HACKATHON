# Khóa bảng xếp hạng chung cuộc (Lock Leaderboard)

## Tác dụng
Cho phép BTC khóa bảng xếp hạng chung cuộc (chuyển sang chế độ Read-only vĩnh viễn sau khi kết thúc giải đấu).

## URL
`PATCH /api/v1/admin/events/{eventId}/leaderboard/lock`

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
  "Value": "LEADERBOARD_LOCKED",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Event phải tồn tại trong DB.
- BTC kiểm tra quyền của Staff.
- Đánh dấu trạng thái khóa bảng xếp hạng. Kể từ thời điểm này, cấm toàn bộ các thao tác chỉnh sửa giải thưởng, sửa đổi điểm số chung cuộc hay chạy tính toán lại (BR-SCO-07, BR-LB-06). *DB hiện chưa có trường lock cho Leaderboards.*

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy sự kiện.",
  "MessageCode": "EVENT_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Không có quyền quản trị sự kiện này. |
| 404 | EVENT_NOT_FOUND | Event không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
