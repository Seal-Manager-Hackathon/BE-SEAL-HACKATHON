# Get my registrations by event

## Tác dụng
Lấy danh sách phân trang các đơn đăng ký sự kiện của các đội thi mà sinh viên hiện tại tham gia theo một `eventId` cụ thể.

## URL
`GET /api/v1/teams/my-registrations`

## Authorization
Yêu cầu access token hợp lệ với role `Student`.

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Query Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của sự kiện cần lấy danh sách đơn đăng ký.
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Số trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên một trang.

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
        "status": 1, /* 0: Pending, 1: Approved, 2: Rejected */
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
- Yêu cầu đăng nhập với role `Student`.
- Tham số `eventId` phải thuộc về một sự kiện tồn tại và đang hoạt động (`IsDisable = false`).
- Chỉ hiển thị các đơn đăng ký (`RegisterTeams`) của sự kiện đó nếu người dùng hiện tại là thành viên đang hoạt động (`Status = Active` trong `TeamDetails`, thành viên không bị disable) của đội thi đó.
- Lọc các bản ghi chưa bị disable (`IsDisable = false`).
- Sắp xếp giảm dần theo thời gian đăng ký (`RegisterTeams.CreatedAt` giảm dần).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | VALIDATION_FAILED |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
