# Danh sách đề bài quản lý (Admin Get Track Topics)

## Tác dụng
Cho phép Admin/Staff xem danh sách đầy đủ tất cả các đề bài (Topic) của một bảng đấu kể cả đề thi đang bị ẩn hoặc disable.

## URL
`GET /api/v1/admin/tracks/{trackId}/topics`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `trackId` (Guid, Bắt buộc): ID của Track cần quản lý đề.
*   **Query Parameters:**
    *   `keyword` (string, Không bắt buộc): Tìm kiếm đề thi theo từ khóa.
    *   `isDisable` (bool, Không bắt buộc): Lọc theo trạng thái disable.
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên trang.

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
        "Title": "Hệ thống số hóa y tế",
        "description": "Xây dựng ứng dụng quản lý.",
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
- Track được tra cứu phải tồn tại.
- BTC kiểm tra quyền của Staff đối với sự kiện tương ứng (phải được phân công quản lý).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Bảng đấu không tồn tại.",
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
| 403 | FORBIDDEN | Không được gán quyền quản lý sự kiện chứa bảng này. |
| 404 | TRACK_NOT_FOUND | Track không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
