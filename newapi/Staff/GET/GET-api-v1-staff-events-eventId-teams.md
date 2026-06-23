# API 46: BTC xem danh sách team chuẩn bị bốc thăm

## Tác dụng
Cho phép Staff/Admin xem danh sách các team tham gia event (mặc định lấy tất cả 3 trạng thái Pending, Approved, Rejected nếu không truyền status; lọc theo status truyền vào nếu có) để chuẩn bị bốc thăm offline.

## URL
`GET /api/v1/staff/events/{eventId}/teams`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của event thi đấu.
*   **Query Parameters:**
    *   `keyword` (string, Không bắt buộc): Tìm kiếm team theo tên.
    *   `status` (int, Không bắt buộc): Lọc theo trạng thái đăng ký của team. Giá trị: `0`: Pending, `1`: Approved, `2`: Rejected.
    *   `isDisable` (bool, Không bắt buộc): Lọc theo disable status.
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên mỗi trang.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "Items": [
      {
        "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "teamName": "Chiến binh công nghệ",
        "trackId": null,
        "topicId": null,
        "Status": "Approved"
      }
    ],
    "PageIndex": 1,
    "PageSize": 10,
    "TotalCount": 1,
    "HasNextPage": false,
    "HasPreviousPage": false
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- BTC chọn event trước, sau đó API trả về danh sách team tham gia event.
- Mặc định trả về các team ở bất kì trạng thái nào (Pending, Approved, Rejected) và chưa bị soft-disable. Nếu có truyền `status` thì lọc theo trạng thái đó.
- Response có `trackId` và `topicId` để FE biết team nào đã được gán kết quả bốc thăm, team nào còn `null` cần gán tiếp.
- Giúp BTC rà soát danh sách trước khi chạy luồng bốc thăm chia đề/chia bảng thi đấu (BR-TRACK-03).
- Sắp xếp mặc định theo `RegisterTeams.UpdatedAt` tăng dần.

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
  "Detail": "Bạn không được phân công quản lý sự kiện này.",
  "MessageCode": "FORBIDDEN",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Không có quyền quản lý sự kiện. |
| 404 | EVENT_NOT_FOUND | Event không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
