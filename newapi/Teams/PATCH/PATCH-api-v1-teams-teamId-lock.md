# Khóa chỉnh sửa Team (BTC Lock Team)

## Tác dụng
Cho phép Staff/Admin khóa cứng thông tin của một team cụ thể (không cho phép đổi tên nhóm, thêm/mời thành viên mới, xóa thành viên cũ, hoặc tự rời nhóm).

## URL
`PATCH /api/v1/teams/{teamId}/lock`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần khóa.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "message": "TEAM_LOCKED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Team phải tồn tại trong DB, nếu không báo lỗi `TEAM_NOT_FOUND`.
- Đặt trường `CanEdit = false` trong bảng `Teams` và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.
- Hệ thống tự động kích hoạt API này khi một trong số các đơn đăng ký thi của team được chuyển sang trạng thái `Approved` (duyệt tham gia event, BR-TEAM-07).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy thông tin nhóm cần khóa.",
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
| 403 | FORBIDDEN | Quyền truy cập bị từ chối (không phải Admin/Staff). |
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
