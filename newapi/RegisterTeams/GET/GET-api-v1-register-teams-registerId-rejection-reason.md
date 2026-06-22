# API 39: Xem lý do từ chối (Get Rejection Reason)

## Tác dụng
Lấy lý do từ chối phê duyệt đơn đăng ký của nhóm thi đấu từ Ban tổ chức.

## URL
`GET /api/v1/register-teams/{registerId}/rejection-reason`

## Quyền
Student (Yêu cầu đăng nhập, là thành viên của team đăng ký)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `registerId` (Guid, Bắt buộc): ID của đơn đăng ký cần xem lý do.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "registerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "rejectionReason": "Danh sách thành viên thiếu thông tin MSSV bắt buộc."
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Bản ghi đơn đăng ký phải tồn tại trong DB, ở trạng thái `Rejected` (đã bị từ chối) và chưa bị soft-delete.
- Người gọi phải là thành viên hoạt động của team đăng ký đó.
- Trả ra trường `RejectionReason` ghi nhận từ BTC.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy đơn đăng ký thi đấu.",
  "MessageCode": "REGISTER_TEAM_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Bạn không phải thành viên của nhóm đăng ký này. |
| 404 | REGISTER_TEAM_NOT_FOUND | Đơn đăng ký không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
