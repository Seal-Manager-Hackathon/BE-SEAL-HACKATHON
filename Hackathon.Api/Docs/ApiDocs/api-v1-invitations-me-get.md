# Get My Invitations

## Tác dụng
Lấy danh sách phân trang các lời mời vào team của học sinh (Student) đang đăng nhập.

## URL
`GET /api/v1/invitations/me`

## Request Parameters
*   **Query Parameters:**
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
        "id": "guid",
        "teamId": "guid",
        "teamName": "string",
        "status": 0, /* Pending */
        "description": "Chào bạn, hãy tham gia team của mình nhé!",
        "limitTime": "datetime",
        "createdAt": "datetime",
        "leaderName": "string|null"
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
- API lấy danh sách các lời mời của người dùng đang đăng nhập (`UserId` lấy từ Access Token).
- Các lời mời và team tương ứng phải chưa bị disable (`!x.IsDisable` và `!x.Team.IsDisable`).
- **Sắp xếp:**
  - Lời mời ở trạng thái chưa chấp nhận (`Pending`) được xếp lên đầu tiên.
  - Sau đó sắp xếp theo thời gian gửi lời mời giảm dần (lời mời mới nhất lên trên - `CreatedAt` giảm dần).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. (khi không truyền token) |
| 401 | INVALID_ACCESS_TOKEN | Invalid access token. (khi token sai định dạng) |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
