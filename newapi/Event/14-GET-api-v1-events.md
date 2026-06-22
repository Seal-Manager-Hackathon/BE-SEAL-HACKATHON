# API 14: Lấy danh sách sự kiện (Student Event List)

## Tác dụng
Lấy danh sách phân trang các event đang hoạt động (IsDisable = false) hiển thị cho học sinh tìm kiếm và đăng ký.

## URL
`GET /api/v1/events`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Query Parameters:**
    *   `keyword` (string, Không bắt buộc): Tìm kiếm không phân biệt hoa thường theo tên, mô tả hoặc season của event.
    *   `year` (int, Không bắt buộc): Lọc event theo năm diễn ra.
    *   `status` (string, Không bắt buộc): Lọc theo trạng thái event (`Draft`, `Published`, `Closed`, `Cancelled`).
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Số trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên một trang.

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
        "id": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
        "name": "SEAL Hackathon 2026",
        "startTime": "2026-07-01T08:00:00Z",
        "endTime": "2026-07-03T18:00:00Z",
        "Status": 1, /* Published */
        "season": "Summer 2026",
        "createdAt": "2026-06-20T08:00:00Z"
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
- Ẩn các event bị soft-disable (`IsDisable = true`).
- Sắp xếp mặc định theo `StartTime` tăng dần, sau đó theo `CreatedAt` tăng dần.
- Lọc theo keyword (tìm kiếm không phân biệt hoa thường trên `Name`, `Description`, `Season`).

### Bảng trạng thái EventStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Draft | Nháp (Thí sinh không nhìn thấy) |
| `1` | Published | Đang diễn ra / Mở đăng ký |
| `2` | Closed | Đã đóng / Kết thúc giải đấu |
| `3` | Cancelled | Đã hủy |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Bad Request",
  "Status": 400,
  "Detail": "Trạng thái event gửi lên không hợp lệ.",
  "MessageCode": "BAD_REQUEST",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Trạng thái event (`status`) không hợp lệ. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống phát sinh. |
