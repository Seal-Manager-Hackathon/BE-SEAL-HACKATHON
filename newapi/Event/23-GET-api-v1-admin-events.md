# API 23: Lấy danh sách sự kiện quản lý (Get Events For Admin)

## Tác dụng
Lấy danh sách phân trang tất cả các event dành cho quản trị viên (Admin/Staff) quản lý, hỗ trợ lọc sâu theo trạng thái soft-disable.

## URL
`GET /api/v1/admin/events`

## Quyền
Admin hoặc Staff (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Query Parameters:**
    *   `keyword` (string, Không bắt buộc): Tìm kiếm theo tên, mô tả hoặc season của event.
    *   `year` (int, Không bắt buộc): Lọc các event theo năm bắt đầu.
    *   `status` (string, Không bắt buộc): Lọc theo trạng thái của event (`Draft`, `Published`, `Closed`, `Cancelled`).
    *   `isDisable` (bool, Không bắt buộc): `true` để lấy các event đã ẩn (disable), `false` để lấy các event đang hoạt động.
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
        "Status": 0, /* Draft */
        "season": "Summer 2026",
        "isDisable": false,
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
- Hỗ trợ lọc theo trạng thái soft-disable `IsDisable` thông qua tham số `isDisable` (giúp Admin tìm lại event đã ẩn).
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
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Tài khoản của bạn không được phân quyền truy cập danh sách quản trị.",
  "MessageCode": "FORBIDDEN",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Trạng thái event gửi lên không hợp lệ. |
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Người gọi không phải Admin hoặc Staff. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ không mong muốn. |
