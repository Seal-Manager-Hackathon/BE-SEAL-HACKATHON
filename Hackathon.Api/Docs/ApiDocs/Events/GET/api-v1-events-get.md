# Get Events

## Tác dụng
Lấy danh sách phân trang các event (mặc định chỉ lấy các event chưa bị disable) dành cho học sinh.

## URL
`GET /api/v1/events`

## Request Parameters
*   **Query Parameters:**
    *   `keyword` (string, Không bắt buộc): Tìm kiếm theo tên, mô tả hoặc season của event.
    *   `year` (int, Không bắt buộc): Lọc các event bắt đầu trong năm chỉ định.
    *   `status` (string, Không bắt buộc): Lọc theo trạng thái của event (ví dụ: `Draft`, `Upcoming`, `Ongoing`, `Ended`).
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Số trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên một trang.

## Request Headers
Không yêu cầu Access Token (Public API).

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
- Không yêu cầu đăng nhập.
- Luôn ẩn các event bị soft-disable (`IsDisable = true`).
- Sắp xếp danh sách mặc định theo thời gian bắt đầu của event tăng dần (`StartTime` tăng dần), sau đó theo thời gian tạo (`CreatedAt` tăng dần).
- Lọc theo keyword (tìm kiếm không phân biệt chữ hoa thường trên `Name`, `Description`, `Season`).
- Lọc theo năm bắt đầu của event (`StartTime.Value.Year`).
- Lọc theo trạng thái của event (`Status`). Nếu trạng thái không hợp lệ, trả lỗi `400 BadRequest` (`INVALID_EVENT_STATUS`).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | INVALID_EVENT_STATUS |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
