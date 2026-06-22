# API 18: Danh sách sự kiện đã tham gia (Joined Events)

## Tác dụng
Lấy danh sách phân trang các event mà tài khoản Student hiện tại đã tham gia (thông qua việc team mà student này làm thành viên đã đăng ký thi đấu và được BTC duyệt).

## URL
`GET /api/v1/events/events/joined`
*(Khuyến nghị chuẩn hóa route thành `/api/v1/events/joined`)*

## Quyền
Authenticated User (Yêu cầu đăng nhập)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Query Parameters:**
    *   `keyword` (string, Không bắt buộc): Tìm kiếm theo tên, mô tả hoặc season của event.
    *   `year` (int, Không bắt buộc): Lọc theo năm của event.
    *   `status` (string, Không bắt buộc): Lọc theo trạng thái event.
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số lượng bản ghi trên một trang.

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
- Chỉ hiển thị các event mà học sinh đang đăng nhập đã tham gia (Team của học sinh đó đăng ký tham gia event và được BTC duyệt - `RegisterTeams` có `Status = Approved`).
- Ẩn các event đã bị BTC soft-disable (`IsDisable = true`).
- Sắp xếp mặc định theo `StartTime` giảm dần để hiển thị sự kiện mới nhất.
- Hỗ trợ các bộ lọc `keyword`, `year`, `status`.

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
  "Title": "Unauthorized",
  "Status": 401,
  "Detail": "Vui lòng đăng nhập để xem danh sách.",
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
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống phát sinh. |
