# Get Joined Events

## Tác dụng
Lấy danh sách phân trang các event mà học sinh (Student) đang đăng nhập đã tham gia (thông qua team đã đăng ký event đó).

## URL
`GET /api/v1/events/events/joined`

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
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "items": [
      {
        "id": "guid",
        "name": "string",
        "startTime": "datetime|null",
        "endTime": "datetime|null",
        "status": "Draft"
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
- Yêu cầu xác thực tài khoản qua Access Token ở Header.
- Chỉ hiển thị các event mà học sinh đang đăng nhập đã tham gia thông qua việc team của học sinh đó đã đăng ký tham gia event (`RegisterTeams` có trạng thái không bị disable, team và thành viên không bị disable).
- Ẩn các event bị soft-disable (`IsDisable = true`).
- Sắp xếp danh sách theo thời gian bắt đầu của event giảm dần (lấy event mới nhất theo năm/thời gian trước - `StartTime` giảm dần).
- Hỗ trợ lọc theo `keyword`, `year` (năm bắt đầu), và `status` (trạng thái event).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | INVALID_EVENT_STATUS |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | INVALID_ACCESS_TOKEN | INVALID_ACCESS_TOKEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
