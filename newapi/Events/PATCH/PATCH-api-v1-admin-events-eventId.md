# API 20: Cập nhật sự kiện (Admin Update Event)

## Tác dụng
Cho phép Admin cập nhật một phần hoặc toàn bộ thông tin của một sự kiện đã cấu hình trong hệ thống.

## URL
`PATCH /api/v1/admin/events/{eventId}`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản có quyền Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của event cần cập nhật.

## Request Body
*API hỗ trợ partial update (chỉ cập nhật các trường được truyền khác null).*
```json
{
  "name": "SEAL Hackathon 2026 - Updated",
  "description": "Giải đấu lập trình mới.",
  "Status": 1 /* Published */
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "EVENT_UPDATED_SUCCESSFULLY",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Event phải tồn tại trong DB, nếu không có báo lỗi `EVENT_NOT_FOUND`.
- `name` nếu truyền không được rỗng, không được trùng với event khác (báo lỗi `EVENT_NAME_ALREADY_EXISTS`).
- `status` được map vào `EventStatusEnum`.
- Thực hiện kiểm tra lại logic ràng buộc mốc thời gian (`StartTime` trước `EndTime`, `RegisterLimitTime` trước `StartTime`) dựa trên giá trị cập nhật hoặc giá trị cũ hiện có.
- Cập nhật thời gian thay đổi `UpdatedAt`.

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
  "Detail": "Tên sự kiện cập nhật trùng lặp với sự kiện khác.",
  "MessageCode": "EVENT_NAME_ALREADY_EXISTS",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | EVENT_NAME_REQUIRED | Tên event không được để trống khi gửi lên. |
| 400 | START_TIME_MUST_BE_BEFORE_END_TIME | Cấu hình mốc thời gian bắt đầu/kết thúc mâu thuẫn. |
| 404 | EVENT_NOT_FOUND | Không tìm thấy event tương ứng với ID. |
| 409 | EVENT_NAME_ALREADY_EXISTS | Trùng tên event đã có. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
