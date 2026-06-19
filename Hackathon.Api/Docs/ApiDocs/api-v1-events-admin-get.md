# Get Events For Admin

## Tác dụng
Lấy danh sách phân trang tất cả các event dành cho Admin/Staff quản lý, hỗ trợ lọc theo trạng thái disable.

## URL
`GET /api/v1/admin/events`

## Request Parameters
*   **Query Parameters:**
    *   `keyword` (string, Không bắt buộc): Tìm kiếm theo tên, mô tả hoặc season của event.
    *   `year` (int, Không bắt buộc): Lọc các event bắt đầu trong năm chỉ định.
    *   `status` (string, Không bắt buộc): Lọc theo trạng thái của event.
    *   `isDisable` (bool, Không bắt buộc): Lọc theo trạng thái soft-disable (`true` để lấy các event đã ẩn, `false` để lấy các event đang hoạt động).
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Số trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên một trang.

## Request Headers
```
Authorization: Bearer <token>
```

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "value": {
    "items": [
      {
        "id": "guid",
        "name": "string",
        "startTime": "datetime|null",
        "endTime": "datetime|null",
        "status": 0, /* Draft */
        "season": "string|null",
        "isDisable": false,
        "createdAt": "datetime"
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
- Yêu cầu xác thực qua Access Token với vai trò Admin hoặc Staff (Admin/Staff Policy).
- Hỗ trợ xem cả các event đã bị soft-disable (`IsDisable = true`) thông qua query parameter `isDisable`.
- Sắp xếp danh sách mặc định theo thời gian bắt đầu của event tăng dần (`StartTime` tăng dần), sau đó theo thời gian tạo (`CreatedAt` tăng dần).
- Lọc theo keyword (tìm kiếm không phân biệt chữ hoa thường trên `Name`, `Description`, `Season`).
- Lọc theo năm bắt đầu của event (`StartTime.Value.Year`).
- Lọc theo trạng thái của event (`Status`). Nếu trạng thái không hợp lệ, trả lỗi `400 BadRequest` (`BAD_REQUEST`).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Trạng thái event (`status`) không hợp lệ. | // 0: Draft, 1: Published, 2: Closed, 3: Cancelled
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. (khi không truyền token) |
| 401 | INVALID_ACCESS_TOKEN | Invalid access token. (khi token sai định dạng) |
| 403 | FORBIDDEN | Quyền truy cập bị từ chối (khi người gọi không phải Admin/Staff) |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |


## Ghi chú Enum
Tham chiếu file [00-enum-values.md](00-enum-values.md) để biết chi tiết các giá trị số (int) trả về cho các trường Trạng thái (Status).
