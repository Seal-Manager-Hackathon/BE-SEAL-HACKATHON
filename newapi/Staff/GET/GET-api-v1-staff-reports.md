# BTC xem danh sách khiếu nại (Staff Get Reports)

## Tác dụng
Cho phép Staff/Admin xem danh sách các báo cáo/khiếu nại/yêu cầu hỗ trợ gửi lên hệ thống.

## URL
`GET /api/v1/staff/reports`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Query Parameters:**
    *   `status` (string, Không bắt buộc): Lọc theo trạng thái report (`Open`, `Closed`).
    *   `typeReport` (string, Không bắt buộc): Lọc theo phân loại khiếu nại (ví dụ: `Phúc khảo`, `Lỗi hệ thống`).
    *   `eventId` (Guid, Không bắt buộc): Lọc khiếu nại thuộc sự kiện cụ thể.
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số lượng item trên trang.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Value": {
    "Items": [
      {
        "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "userFullName": "Hoàng Phạm",
        "Title": "Khiếu nại điểm thi vòng 1",
        "typeReport": "Phúc khảo",
        "Status": 0, /* Open */
        "createdAt": "2026-06-22T08:00:00Z"
      }
    ],
    "PageIndex": 1,
    "PageSize": 10,
    "TotalCount": 1,
    "HasNextPage": false,
    "HasPreviousPage": false
  }
}
```

## Business rules
- Chỉ hiển thị các báo cáo chưa bị disable.
- Staff được phân công ở sự kiện nào chỉ được thấy báo cáo thuộc sự kiện đó. Admin được quyền xem toàn bộ (BR-ASG-01).

### Bảng trạng thái ReportStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Open | Đang mở / Chờ xử lý khiếu nại |
| `1` | Closed | Đã đóng / Đã giải quyết xong |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Unauthorized",
  "Status": 401,
  "Detail": "Vui lòng đăng nhập tài khoản BTC.",
  "MessageCode": "UNAUTHORIZED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không có vai trò Staff/Admin. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
