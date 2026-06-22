# Vô hiệu hóa nhóm toàn cục (Admin Disable Team)

## Tác dụng
Cho phép Admin vô hiệu hóa (disable) một team cụ thể trên toàn cục (đặt cờ IsDisable = true để xóa mềm nhóm khỏi hệ thống).

## URL
`PATCH /api/v1/admin/teams/{teamId}/disable`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản có quyền Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần vô hiệu hóa.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "message": "TEAM_DISABLED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Team phải tồn tại trong DB, nếu không báo lỗi `TEAM_NOT_FOUND`.
- Cập nhật trường `IsDisable = true` trong bảng `Teams` và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.
- Toàn bộ thành viên thuộc nhóm đó bị ngắt quyền truy cập thông tin nhóm, đơn đăng ký giải đấu liên đới sẽ bị ẩn ở giao diện public.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy nhóm cần vô hiệu hóa.",
  "MessageCode": "TEAM_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Quyền truy cập bị từ chối do thiếu vai trò Admin. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
