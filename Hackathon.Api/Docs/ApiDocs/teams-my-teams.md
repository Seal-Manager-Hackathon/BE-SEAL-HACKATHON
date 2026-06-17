# Get My Teams

## Tác dụng
Lấy danh sách phân trang các team mà người dùng hiện tại đang tham gia (trong bảng `TeamDetails`).

## URL
`GET /api/v1/teams/me`

## Request Parameters
*   **Query Parameters:**
    *   `status` (string, Không bắt buộc): Trạng thái của thành viên trong team (`Active` hoặc `Inactive`).
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
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "value": {
    "items": [
      {
        "teamId": "guid",
        "teamName": "string",
        "canEdit": true,
        "isLeader": true,
        "memberStatus": "Active",
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
- Yêu cầu đăng nhập (`[Authorize]`) bằng Access Token qua Header.
- Trả về danh sách team mà người dùng hiện tại tham gia, sắp xếp theo:
  - Trạng thái thành viên trong team: `Active` trước, `Inactive` sau.
  - Thời gian tạo team: Team nào được tạo mới hơn sẽ lên trước (`Team.CreatedAt` giảm dần).
- Hỗ trợ lọc theo `status` của thành viên trong team nếu client cung cấp.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. (khi không truyền token) |
| 401 | INVALID_ACCESS_TOKEN | Invalid access token. (khi token sai định dạng) |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
