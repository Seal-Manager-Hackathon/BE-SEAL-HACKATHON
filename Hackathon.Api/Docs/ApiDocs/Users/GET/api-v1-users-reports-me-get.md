# Danh sách khiếu nại của tôi (Get My Reports)

## Tác dụng
Cho phép người dùng đã đăng nhập xem danh sách tất cả các báo cáo/khiếu nại/yêu cầu hỗ trợ hệ thống mà bản thân đã gửi lên.

## URL
`GET /api/v1/users/reports/me`

## Quyền
Authenticated User (Yêu cầu đăng nhập, chỉ xem các báo cáo do mình gửi)

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Query Parameters:**
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số lượng bản ghi mỗi trang.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": {
    "items": [
      {
        "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "title": "Báo cáo vấn đề chấm điểm lệch",
        "typeReport": "Phúc khảo",
        "status": 0,
        "createdAt": "2026-06-22T08:00:00Z"
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 1,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

## Business rules
- Chỉ hiển thị các báo cáo có `UserId` trùng khớp với Token của người dùng đang đăng nhập và chưa bị soft-delete.
- Sắp xếp mặc định theo `CreatedAt` giảm dần (báo cáo mới nhất xếp lên đầu).

### Bảng trạng thái ReportStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Open | Đang mở / Chờ xử lý khiếu nại |
| `1` | Closed | Đã đóng / Đã giải quyết xong |

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
