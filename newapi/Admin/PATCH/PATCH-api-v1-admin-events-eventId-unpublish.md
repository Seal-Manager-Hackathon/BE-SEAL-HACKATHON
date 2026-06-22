# Hủy công bố sự kiện (Admin Unpublish Event)

## Tác dụng
Chuyển trạng thái sự kiện từ `Published` (Đã công bố) quay ngược lại trạng thái `Draft` (Nháp) để tạm ẩn giải đấu khỏi giao diện thí sinh khi cần điều chỉnh khẩn cấp.

## URL
`PATCH /api/v1/admin/events/{eventId}/unpublish`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản có quyền Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của event cần hủy công bố.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "EVENT_UNPUBLISHED_SUCCESSFULLY",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Event phải tồn tại trong DB, nếu không báo lỗi `EVENT_NOT_FOUND`.
- Chỉ chấp nhận hủy công bố khi event đang ở trạng thái `Published`. Nếu event đang ở trạng thái `Draft`, `Closed`, hoặc `Cancelled` thì từ chối hành động và báo lỗi `EVENT_NOT_IN_PUBLISHED_STATUS`.
- Khi unpublish: gán `Status = Draft` (giá trị enum `0`) và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.

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
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Sự kiện hiện tại không ở trạng thái Published.",
  "MessageCode": "EVENT_NOT_IN_PUBLISHED_STATUS",
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
| 409 | EVENT_NOT_IN_PUBLISHED_STATUS | Event chưa được công bố hoặc đã kết thúc. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
