# API 45: Nhân sự xem danh sách đề bài (Staff Get Topics By Track)

## Tác dụng
Cho phép Staff/Admin xem danh sách các đề bài (Topic) thuộc một bảng đấu để phục vụ việc bốc thăm đề thi.

## URL
`GET /api/v1/staff/tracks/{trackId}/topics`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `trackId` (Guid, Bắt buộc): ID của Track cần xem đề thi.
*   **Query Parameters:**
    *   `keyword` (string, Không bắt buộc): Tìm kiếm đề thi theo tên hoặc mô tả.
    *   `isDisable` (bool, Không bắt buộc): Lọc theo trạng thái ẩn/disable.
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
        "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "Title": "Hệ thống quản lý y tế thông minh",
        "description": "Đề thi số hóa khám chữa bệnh.",
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
- Track được truy vấn phải tồn tại, nếu không báo lỗi `TRACK_NOT_FOUND`.
- Staff phải có quyền quản lý event chứa track này (đối chiếu thông tin qua bảng `AssignEvents`).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy bảng đấu (track) tương ứng.",
  "MessageCode": "TRACK_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | BTC từ chối quyền truy cập do chưa phân công quản lý. |
| 404 | TRACK_NOT_FOUND | Track không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
