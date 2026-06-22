# BTC xem chi tiết khiếu nại (Staff Get Report Detail)

## Tác dụng
Cho phép Staff/Admin xem thông tin chi tiết của một khiếu nại, bao gồm ảnh/file minh chứng và bài thi liên kết qua `submissionId`. Với khiếu nại phúc khảo, Staff/Admin dùng `submissionId` để xem đúng bài nộp cần xử lý và có thể phân công judge khác chấm lại.

## URL
`GET /api/v1/staff/reports/{reportId}`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `reportId` (Guid, Bắt buộc): ID của báo cáo/khiếu nại.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Value": {
    "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "userFullName": "Hoàng Phạm",
    "assignEventId": "b1a7d6c2-4821-4f9b-bd5e-3c2fa56789e0",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "Title": "Khiếu nại điểm thi vòng 1",
    "description": "Giám khảo chấm bảng A cho điểm không khớp tiêu chí.",
    "imgUrl": "https://example.com/evidence.jpg",
    "fileUrl": "https://example.com/evidence.pdf",
    "typeReport": "Phúc khảo",
    "Status": 0, /* Open */
    "reason": null,
    "createdAt": "2026-06-22T08:00:00Z",
    "updatedAt": "2026-06-22T08:00:00Z"
  }
}
```

## Business rules
- Báo cáo phải tồn tại trong DB, không bị soft-delete.
- Staff phải được phân công quản lý event liên quan mới được xem chi tiết.

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
  "Detail": "Không tìm thấy báo cáo khiếu nại.",
  "MessageCode": "REPORT_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Staff chưa được BTC phân công phụ trách quản lý sự kiện liên quan. |
| 404 | REPORT_NOT_FOUND | Báo cáo không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
