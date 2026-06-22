# Thay đổi vai trò Giảng viên (Update Lecturer Event Role)

## Tác dụng
Cho phép Admin thay đổi vai trò (từ Mentor sang Judge hoặc ngược lại) của một giảng viên trong sự kiện.

## URL
`PATCH /api/v1/admin/assign-events/{assignEventId}/role`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `assignEventId` (Guid, Bắt buộc): ID của bản ghi phân công event.

## Request Body
```json
{
  "eventRole": "Mentor"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "LECTURER_ROLE_UPDATED",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Bản ghi `AssignEvents` phải tồn tại.
- Cập nhật trường `EventRoleId` của bản ghi chỉ định sang vai trò mới.
- Hệ thống tự động kiểm tra và gỡ bỏ toàn bộ phân công track cũ (`AssignTracks`) của giảng viên này để tránh sai lệch dữ liệu phân bảng đấu (vì đổi vai trò từ Judge sang Mentor hoặc ngược lại sẽ thay đổi hoàn toàn nghiệp vụ trên bảng đấu).

### Bảng vai trò EventRoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy thông tin phân công sự kiện.",
  "MessageCode": "ASSIGNMENT_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | INVALID_EVENT_ROLE | Vai trò sự kiện gửi lên sai enum. |
| 404 | ASSIGNMENT_NOT_FOUND | Bản ghi phân công không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
