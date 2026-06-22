# Ẩn/Hiện bảng đấu (Admin Toggle Track Visibility)

## Tác dụng
Cho phép Staff/Admin ẩn hoặc hiện bảng đấu (Track) ra giao diện public của thí sinh.

## URL
`PATCH /api/v1/admin/tracks/{trackId}/visibility`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `trackId` (Guid, Bắt buộc): ID của bảng đấu.

## Request Body
```json
{
  "isVisible": true
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "TRACK_VISIBILITY_UPDATED",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Track phải tồn tại trong DB.
- BTC kiểm tra quyền của Staff đối với sự kiện chứa track này.
- *Lưu ý*: Vì DB hiện chưa có trường `IsVisible` riêng nên cờ ẩn hiện tạm thời được cập nhật thông qua việc chuyển đổi trạng thái `IsDisable` hoặc thông qua một cấu hình metadata của Event. Nếu DB sau này được bổ sung trường `IsVisible`, API này sẽ ánh xạ trực tiếp vào trường đó.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy bảng đấu.",
  "MessageCode": "TRACK_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Staff chưa được BTC phân công phụ trách quản lý event này. |
| 404 | TRACK_NOT_FOUND | Track không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
