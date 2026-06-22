# Xem thông báo cá nhân (Get My Notifications)

## Tác dụng
Lấy danh sách phân trang các thông báo gửi riêng cho tài khoản người dùng đang đăng nhập.

## URL
`GET /api/v1/notifications/me`

## Quyền
Authenticated User (Yêu cầu đăng nhập)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Query Parameters:**
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số lượng bản ghi mỗi trang.

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
        "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "Title": "Lời mời vào nhóm",
        "description": "Bạn có lời mời mới từ team Chiến binh công nghệ.",
        "Status": 1, /* Unread */
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
- Trả về danh sách thông báo chưa bị disable (`IsDisable = false`) được gán cho `UserId` của người đăng nhập.
- Sắp xếp kết quả: Thông báo chưa đọc xếp trước, sau đó sắp xếp theo `CreatedAt` giảm dần (mới nhất lên đầu).

### Bảng trạng thái NotificationStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ gửi |
| `1` | Unread | Chưa đọc |
| `2` | Read | Đã đọc |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Unauthorized",
  "Status": 401,
  "Detail": "Vui lòng xác thực tài khoản.",
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
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
