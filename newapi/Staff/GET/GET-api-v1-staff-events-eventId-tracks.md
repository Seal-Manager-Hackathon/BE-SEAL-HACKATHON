# API 44: Nhân sự xem danh sách Track (Staff Get Event Tracks)

## Tác dụng
Cho phép nhân viên (Staff/Admin) được phân công xem danh sách các track của event nhằm phục vụ quản lý và bốc thăm.

## URL
`GET /api/v1/staff/events/{eventId}/tracks`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của event.
*   **Query Parameters:**
    *   `keyword` (string, Không bắt buộc): Tìm kiếm theo tên hoặc mô tả track.
    *   `isDisable` (bool, Không bắt buộc): Lọc theo trạng thái disable.
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử mỗi trang.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "Items": [
      {
        "id": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
        "Title": "Bảng A - Web Application",
        "description": "Mô tả bảng đấu Web.",
        "maxTeam": 50,
        "isDisable": false,
        "createdAt": "2026-06-21T08:00:00Z"
      }
    ],
    "PageIndex": 1,
    "PageSize": 10,
    "TotalCount": 1,
    "HasNextPage": false,
    "HasPreviousPage": false
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Yêu cầu người gọi có role Staff hoặc Admin.
- BTC kiểm tra quyền gán của Staff đối với event (`AssignEvents` chứa `UserId` và `EventId`). Nếu không phải là nhân viên vận hành giải đấu đó, từ chối và báo lỗi `FORBIDDEN`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Bạn không được phân công quản lý sự kiện này.",
  "MessageCode": "FORBIDDEN",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không được phân công phụ trách event thi đấu này (check BR-ASG-01). |
| 404 | EVENT_NOT_FOUND | Event không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
