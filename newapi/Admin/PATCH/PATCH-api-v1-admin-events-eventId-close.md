# Kết thúc sự kiện (Admin Close Event)

## Tác dụng
Chuyển trạng thái sự kiện sang `Closed` (Đã đóng) sau khi kết thúc toàn bộ vòng thi và công bố bảng vàng.

## URL
`PATCH /api/v1/admin/events/{eventId}/close`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản có quyền Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của event cần đóng.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "EVENT_CLOSED_SUCCESSFULLY",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Event phải tồn tại trong DB, nếu không báo lỗi `EVENT_NOT_FOUND`.
- Chuyển trạng thái `Status = Closed` (giá trị enum `2`) và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.
- Kể từ thời điểm đóng sự kiện, toàn bộ dữ liệu điểm số, bảng xếp hạng và bài nộp thi của event chuyển sang trạng thái chỉ đọc (Read-only), không cho phép Judge cập nhật điểm hay thí sinh sửa bài nộp (BR-SCO-07, BR-LB-06).

### Bảng trạng thái EventStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Draft | Nháp (Thí sinh không nhìn thấy) |
| `1` | Published | Đang diễn ra / Mở đăng ký |
| `2` | Closed | Đã đóng / Kết thúc giải đấu |
| `3` | Cancelled | Đã hủy |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy sự kiện cần đóng.",
  "MessageCode": "EVENT_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Quyền truy cập bị từ chối do thiếu role Admin. |
| 404 | EVENT_NOT_FOUND | Event không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
