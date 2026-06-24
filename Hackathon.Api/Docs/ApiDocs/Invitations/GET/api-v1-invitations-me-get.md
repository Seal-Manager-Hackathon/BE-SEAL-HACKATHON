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
*Cấu trúc trả về dạng `BasePaginationResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Message": "SUCCESS",
  "Data": {
    "Items": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "teamId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
        "teamName": "Chiến binh công nghệ",
        "status": 0, /* 0: Pending, 1: Accepted, 2: Rejected, 3: Expired */
        "description": "Chào bạn, hãy tham gia team của mình nhé!",
        "limitTime": "2026-06-30T23:59:59Z",
        "createdAt": "2026-06-22T08:00:00Z",
        "leaderName": "Nguyễn Văn A"
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
- API lấy danh sách các lời mời của người dùng đang đăng nhập (`UserId` lấy từ Access Token).
- Các lời mời và team tương ứng phải chưa bị disable (`!x.IsDisable` và `!x.Team.IsDisable`).
- **Sắp xếp:**
  - Lời mời ở trạng thái chưa chấp nhận (`Pending`) được xếp lên đầu tiên.
  - Sau đó sắp xếp theo thời gian gửi lời mời giảm dần (lời mời mới nhất lên trên - `CreatedAt` giảm dần).

### Bảng trạng thái lời mời InvitationStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Lời mời đang chờ phản hồi |
| `1` | Accepted | Lời mời đã được chấp nhận |
| `2` | Rejected | Lời mời bị từ chối |
| `3` | Expired | Lời mời đã hết hạn phản hồi |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
