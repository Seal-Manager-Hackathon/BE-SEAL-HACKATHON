# Get Events

## Tác dụng
Lấy danh sách phân trang các event đã Published hoặc Closed (ẩn Draft, Cancelled) dành cho học sinh.

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
*Cấu trúc trả về dạng `BasePaginationResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Data": {
    "Items": [
      {
        "id": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
        "name": "SEAL Hackathon 2026",
        "startTime": "2026-07-01T08:00:00Z",
        "endTime": "2026-07-10T17:00:00Z",
        "status": 0, /* 0: Draft, 1: Published, 2: Closed, 3: Cancelled */
        "season": "Mùa hè 2026",
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
- Không yêu cầu đăng nhập.
- Luôn ẩn các event bị soft-disable (`IsDisable = true`).
- **Chỉ trả về event có trạng thái `Published` hoặc `Closed`** — hoàn toàn không trả `Draft` hoặc `Cancelled`.
- Sắp xếp danh sách theo `StartTime` giảm dần (event sắp/mới diễn ra lên đầu), sau đó theo `CreatedAt` giảm dần.
- Lọc theo keyword (tìm kiếm không phân biệt chữ hoa thường trên `Name`, `Description`, `Season`).
- Lọc theo năm bắt đầu của event (`StartTime.Value.Year`).
- Lọc theo trạng thái của event (`Status`). Nếu trạng thái không hợp lệ, trả lỗi `400 BadRequest` (`INVALID_EVENT_STATUS`).

### Bảng trạng thái EventStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Draft | Sự kiện đang nháp, chưa công bố |
| `1` | Published | Sự kiện đã công bố và hoạt động |
| `2` | Closed | Sự kiện đã kết thúc và đóng lại |
| `3` | Cancelled | Sự kiện đã bị hủy bỏ |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | INVALID_EVENT_STATUS |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
