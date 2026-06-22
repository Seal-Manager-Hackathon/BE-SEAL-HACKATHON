# BTC cập nhật trạng thái khiếu nại (Staff Resolve Report)

## Tác dụng
Staff/Admin cập nhật trạng thái giải quyết khiếu nại (chuyển sang Closed) và ghi chú nội dung phản hồi xử lý.

## URL
`PATCH /api/v1/staff/reports/{reportId}/status`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `reportId` (Guid, Bắt buộc): ID của báo cáo/khiếu nại.

## Request Body
```json
{
  "Status": 1, /* Closed */
  "reason": "Yêu cầu phúc khảo được chấp nhận. Hệ thống đã mở chấm lại."
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "REPORT_RESOLVED",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Báo cáo phải tồn tại trong DB, không bị soft-delete.
- Staff phải được phân công quản lý event liên quan.
- Cập nhật trường `Status = Closed` (giá trị enum `1`) và ghi nhận nội dung giải quyết vào `Reason` của bảng `Reports`.

### Bảng trạng thái ReportStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Open | Đang mở / Chờ xử lý khiếu nại |
| `1` | Closed | Đã đóng / Đã giải quyết xong |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy báo cáo cần xử lý.",
  "MessageCode": "REPORT_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Thiếu quyền quản lý hoặc vai trò BTC. |
| 404 | REPORT_NOT_FOUND | Báo cáo không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
