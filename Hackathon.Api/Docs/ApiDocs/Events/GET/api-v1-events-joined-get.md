# Get Joined Events

## Tác dụng
Lấy danh sách phân trang các event mà học sinh (Student) đang đăng nhập đã tham gia (thông qua team đã đăng ký event đó).

## URL
`GET /api/v1/events/joined`

## Request Parameters
*   **Query Parameters:**
    *   `keyword` (string, Không bắt buộc): Tìm kiếm theo tên, mô tả hoặc season của event.
    *   `year` (int, Không bắt buộc): Lọc các event bắt đầu trong năm chỉ định.
    *   `status` (string, Không bắt buộc): Lọc theo trạng thái của event.
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Số trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên một trang.

## Request Headers
```
Authorization: Bearer <token>
```

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
- Yêu cầu xác thực tài khoản qua Access Token ở Header.
- Chỉ hiển thị các event mà học sinh đang đăng nhập đã tham gia thông qua việc team của học sinh đó đã đăng ký tham gia event (`RegisterTeams` có trạng thái không bị disable, team và thành viên không bị disable).
- Ẩn các event bị soft-disable (`IsDisable = true`).
- **Chỉ trả về event có trạng thái `Published` hoặc `Closed`** — `Draft` và `Cancelled` bị loại bỏ.
- Sắp xếp danh sách theo `StartTime` giảm dần, sau đó theo `CreatedAt` giảm dần.
- Hỗ trợ lọc theo `keyword`, `year` (năm bắt đầu), và `status` (trạng thái event).

### Bảng trạng thái EventStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Draft | Sự kiện đang nháp, chưa công bố |
| `1` | Published | Sự kiện đã công bố và hoạt động |
| `2` | Closed | Sự kiện đã kết thúc và đóng lại |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | INVALID_EVENT_STATUS |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
