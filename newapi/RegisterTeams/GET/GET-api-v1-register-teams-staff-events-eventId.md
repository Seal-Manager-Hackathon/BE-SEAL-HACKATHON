# API 40: BTC xem danh sách đăng ký Event (Staff Get Register Teams)

## Tác dụng
Cho phép Staff/Admin xem danh sách các đơn đăng ký thi của các team tham gia vào một event để thực hiện xét duyệt đơn.

## URL
`GET /api/v1/register-teams/staff/events/{eventId}`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của event.
*   **Query Parameters:**
    *   `keyword` (string, Không bắt buộc): Tìm kiếm theo tên nhóm.
    *   `status` (string, Không bắt buộc): Lọc theo trạng thái duyệt đơn (`Pending`, `Approved`, `Rejected`).
    *   `isDisable` (bool, Không bắt buộc): Lọc theo trạng thái soft-disable.
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên mỗi trang.

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
        "registerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "teamName": "Chiến binh công nghệ",
        "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
        "eventName": "SEAL Hackathon 2026",
        "Status": 0, /* Pending */
        "description": "Lời nhắn từ team",
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
- Đối với vai trò Staff: Phải được BTC phân công quản lý sự kiện này trong bảng `AssignEvents` mới được phép lấy danh sách (BR-ASG-01, nếu sai trả lỗi `STAFF_NOT_ASSIGNED_TO_EVENT`).
- Admin có đặc quyền xem toàn bộ mà không cần kiểm tra phân công.
- Lọc theo các tiêu chí keyword (tìm tên team), status, isDisable.
- Sắp xếp mới nhất lên trước (`RegisterTeams.CreatedAt` giảm dần).

### Bảng trạng thái RegisterTeamStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ duyệt đăng ký |
| `1` | Approved | Đã duyệt tham gia sự kiện |
| `2` | Rejected | Bị từ chối tham gia sự kiện |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Bạn không được phân công quản trị sự kiện này.",
  "MessageCode": "STAFF_NOT_ASSIGNED_TO_EVENT",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | STAFF_NOT_ASSIGNED_TO_EVENT | Staff chưa được BTC phân công phụ trách quản lý sự kiện này. |
| 404 | EVENT_NOT_FOUND | Sự kiện không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
