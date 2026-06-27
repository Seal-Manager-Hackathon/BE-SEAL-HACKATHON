# Get My Teams

## Tác dụng
Lấy danh sách phân trang các team mà người dùng hiện tại đang tham gia (trong bảng `TeamDetails`).

## URL
`GET /api/v1/teams/me`

## Request Parameters
*   **Query Parameters:**
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Số trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên một trang.

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
        "teamId": "guid",
        "teamName": "string",
        "canEdit": true,
        "isLeader": true,
        "memberStatus": 0, /* Active */
        "joinedAt": "datetime"
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
- Chỉ hiển thị các team mà người dùng hiện tại đang tham gia và đang còn hoạt động (`Status = Active` trong bảng `TeamDetails`, team và thành viên không bị disable).
- Sắp xếp danh sách theo thời gian tạo team: Team nào được tạo mới hơn sẽ lên trước (`Team.CreatedAt` giảm dần).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | INVALID_ACCESS_TOKEN | INVALID_ACCESS_TOKEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
