# Get My Registrations By Event

## Tác dụng

Lấy danh sách phân trang các đơn đăng ký sự kiện của các đội thi mà sinh viên hiện tại tham gia theo một `eventId` cụ thể.

## URL

`GET /api/v1/teams/my-registrations`

## Request Parameters

- **Query Parameters:**
  - `eventId` (Guid, Bắt buộc): ID của sự kiện cần lấy danh sách đơn đăng ký.
  - `pageIndex` (int, Không bắt buộc, mặc định: 1): Số trang hiện tại.
  - `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên một trang.

## Authorization

Yêu cầu access token hợp lệ với role `Student`.

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
        "registerTeamId": "guid",
        "teamId": "guid",
        "teamName": "string",
        "status": "string" /* Pending, Approved, Rejected, etc. */,
        "rejectionReason": "string|null",
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

- Yêu cầu đăng nhập với role `Student` (student policy).
- Tham số `eventId` phải thuộc về một sự kiện tồn tại và đang hoạt động (`IsDisable = false`).
- Chỉ hiển thị các đơn đăng ký (`RegisterTeams`) của sự kiện đó nếu người dùng hiện tại là thành viên đang hoạt động (`Status = Active` trong `TeamDetails`, thành viên không bị disable) của đội thi đó.
- Lọc các bản ghi chưa bị disable (`IsDisable = false`).
- Sắp xếp giảm dần theo thời gian đăng ký (`RegisterTeams.CreatedAt` giảm dần).

## Lỗi có thể xảy ra

_Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:_

| HTTP | messageCode           | message/detail                                      |
| ---: | --------------------- | --------------------------------------------------- |
|  400 | VALIDATION_FAILED     | Dữ liệu đầu vào không hợp lệ (ví dụ: thiếu eventId) |
|  401 | MISSING_ACCESS_TOKEN  | ACCESS_TOKEN_IS_MISSING                             |
|  401 | INVALID_ACCESS_TOKEN  | INVALID_ACCESS_TOKEN                                |
|  404 | EVENT_NOT_FOUND       | Event không tồn tại hoặc bị disable                 |
|  500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED                        |
