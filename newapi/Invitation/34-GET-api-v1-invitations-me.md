# API 34: Xem lời mời của tôi (Get My Invitations)

## Tác dụng
Lấy danh sách phân trang các lời mời gia nhập nhóm gửi tới sinh viên đang đăng nhập.

## URL
`GET /api/v1/invitations/me`

## Quyền
Student (Yêu cầu đăng nhập tài khoản Student)

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
        "id": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
        "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "teamName": "Chiến binh công nghệ",
        "Status": 0, /* Pending */
        "description": "Chào bạn, hãy tham gia nhóm của mình nhé!",
        "limitTime": "2026-06-24T08:00:00Z",
        "createdAt": "2026-06-22T08:00:00Z",
        "leaderName": "Hoàng Phạm"
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
- Tài khoản phải đăng nhập với Access Token hợp lệ.
- Lời mời và team gửi lời mời phải đang không bị soft-disable (`!x.IsDisable` và `!x.Team.IsDisable`).
- Sắp xếp kết quả:
  - Lời mời chưa phản hồi (`Status = Pending`) xếp lên đầu.
  - Sau đó sắp xếp theo `CreatedAt` giảm dần (lời mời mới nhất lên trên).

### Bảng trạng thái InvitationStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ xử lý / Chờ phản hồi |
| `1` | Accepted | Đã chấp nhận gia nhập nhóm |
| `2` | Rejected | Đã từ chối lời mời |
| `3` | Expired | Lời mời đã hết hạn |

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
