# BTC phê duyệt chấm lại (Staff Approve Regrade)

## Tác dụng
BTC phê duyệt khiếu nại và đồng ý mở cổng chấm lại điểm bài thi phúc khảo.

## URL
`POST /api/v1/staff/reports/{reportId}/regrade`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `reportId` (Guid, Bắt buộc): ID của báo cáo khiếu nại.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "REGRADE_APPROVED",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Báo cáo phải tồn tại, thuộc loại `Phúc khảo` (`typeReport = "Phúc khảo"`) và đang ở trạng thái `Open`.
- Staff phải được phân công quản lý event liên quan.
- Khi phê duyệt thành công: hệ thống chuyển trạng thái chấm của bài thi liên quan (`Scores`) cho phép Judge chấm lại, ghi nhận cờ chấm lại (`IsRetake = true` ở API chấm lại tiếp theo).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Báo cáo này không phải khiếu nại phúc khảo.",
  "MessageCode": "NOT_AN_APPEAL_REPORT",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không được phân công phụ trách. |
| 404 | REPORT_NOT_FOUND | Báo cáo không tồn tại. |
| 409 | NOT_AN_APPEAL_REPORT | Báo cáo không thuộc loại phúc khảo. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
