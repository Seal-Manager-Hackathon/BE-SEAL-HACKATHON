# API 13: Tạo báo cáo hệ thống (System Report)

## Tác dụng
Cho phép người dùng đã đăng nhập tạo báo cáo gửi lên hệ thống, liên quan đến một Assignment hoặc một Submission.

## URL
`POST /api/v1/users/system-report`

## Quyền
Authenticated User (Yêu cầu đăng nhập)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Body
```json
{
  "assignEventId": "b1a7d6c2-4821-4f9b-bd5e-3c2fa56789e0",
  "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
  "Title": "Báo cáo vấn đề chấm điểm lệch",
  "description": "Giám khảo chấm bảng A cho điểm không khớp tiêu chí.",
  "imgUrl": "https://example.com/evidence.jpg",
  "fileUrl": "https://example.com/evidence.pdf",
  "typeReport": "Phúc khảo"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "REPORT_CREATED_SUCCESSFULLY",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- ID người báo cáo (`UserId`) được hệ thống lấy tự động từ Access Token.
- Báo cáo tạo ra được gán trạng thái mặc định là `Open` (Mở/Đang xử lý).
- Ghi nhận thời gian tạo `CreatedAt` và `UpdatedAt` hiện tại.

### Bảng trạng thái ReportStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Open | Đang mở / Chờ xử lý khiếu nại |
| `1` | Closed | Đã đóng / Đã giải quyết xong |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Bad Request",
  "Status": 400,
  "Detail": "Dữ liệu Guid gửi lên không hợp lệ.",
  "MessageCode": "BAD_REQUEST",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Lỗi validation từ định dạng Guid hoặc thiếu tiêu đề/mô tả. |
| 401 | MISSING_ACCESS_TOKEN | Access token bị thiếu hoặc không hợp lệ. |
| 404 | ASSIGN_EVENT_NOT_FOUND | Không tìm thấy thông tin phân công sự kiện. |
| 500 | INTERNAL_SERVER_ERROR | Gặp sự cố không mong muốn tại server. |
