# Chi tiết khiếu nại của tôi (Get My Report Detail)

## Tác dụng
Cho phép người dùng đã đăng nhập xem chi tiết tiến độ giải quyết một khiếu nại cụ thể của họ.

## URL
`GET /api/v1/users/reports/{reportId}`

## Quyền
Authenticated User (Yêu cầu đăng nhập, là người gửi báo cáo)

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `reportId` (Guid, Bắt buộc): ID của báo cáo khiếu nại.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa chi tiết bản ghi báo cáo.*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": {
    "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "assignEventId": "b1a7d6c2-4821-4f9b-bd5e-3c2fa56789e0",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "title": "Báo cáo vấn đề chấm điểm lệch",
    "description": "Giám khảo chấm bảng A cho điểm không khớp tiêu chí.",
    "imgUrl": "https://example.com/evidence.jpg",
    "fileUrl": "https://example.com/evidence.pdf",
    "typeReport": "Phúc khảo",
    "status": 0,
    "reason": null,
    "createdAt": "2026-06-22T08:00:00Z",
    "updatedAt": "2026-06-22T08:00:00Z"
  }
}
```

## Business rules
- Báo cáo phải tồn tại trong DB, không bị soft-delete.
- Người gọi phải chính là chủ sở hữu của báo cáo đó (`UserId` khớp với Token, nếu sai trả lỗi `FORBIDDEN`).
- Trả ra đầy đủ nội dung báo cáo và phản hồi giải quyết từ BTC (`reason`).

### Bảng trạng thái ReportStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Open | Đang mở / Chờ xử lý khiếu nại |
| `1` | Closed | Đã đóng / Đã giải quyết xong |
| `2` | Approved | Đã duyệt phúc khảo / Đang chờ judge cũ chấm lại |

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Người gọi không phải là người tạo báo cáo này. |
| 404 | REPORT_NOT_FOUND | Báo cáo không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
