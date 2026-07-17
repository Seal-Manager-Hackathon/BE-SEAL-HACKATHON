# Xem thông báo cá nhân (Get My Notifications)

## Tác dụng
Lấy danh sách phân trang các thông báo dành cho tài khoản người dùng đang đăng nhập, bao gồm thông báo cá nhân, thông báo team và thông báo toàn hệ thống.

## URL
`GET /api/v1/notifications/me`

## Quyền
Authenticated User (Yêu cầu đăng nhập)

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Query Parameters:**
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số lượng bản ghi mỗi trang.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": {
    "items": [
      {
        "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "title": "Lời mời vào nhóm",
        "description": "Bạn có lời mời mới từ team Chiến binh công nghệ.",
        "status": 1,
        "targetType": 0,
        "createdAt": "2026-06-22T08:00:00Z"
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
- Trả về danh sách thông báo chưa bị disable (`IsDisable = false`) được gán cho `UserId` của người đăng nhập.
- Thông báo toàn hệ thống cũng được lưu thành bản ghi riêng theo từng user với `targetType = System`, nên vẫn được lấy qua `UserId` hiện tại.
- Sắp xếp kết quả: Thông báo chưa đọc xếp trước, sau đó sắp xếp theo `CreatedAt` giảm dần (mới nhất lên đầu).

### Bảng trạng thái NotificationStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ gửi |
| `1` | Unread | Chưa đọc |
| `2` | Read | Đã đọc |

### Bảng phân loại NotificationTargetTypeEnum
| Giá trị (Value) | Loại (TargetType) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Personal | Gửi riêng cho một người dùng |
| `1` | Team | Gửi cho một team |
| `2` | System | Gửi toàn hệ thống (tất cả user đều nhận được) |

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
