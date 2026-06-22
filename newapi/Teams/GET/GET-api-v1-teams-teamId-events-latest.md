# API 33: Sự kiện mới nhất của Team (Latest Registered Event)

## Tác dụng
Lấy ra duy nhất **một** event tham gia gần đây nhất của một team (đã được chấp nhận - Approved).

## URL
`GET /api/v1/teams/{teamId}/events/latest`

## Quyền
Authenticated User (Yêu cầu đăng nhập)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần tra cứu.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`. Trả về `value: null` nếu team chưa có sự kiện nào được duyệt.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "registerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "eventName": "SEAL Hackathon 2026",
    "Status": "Approved",
    "createdAt": "2026-06-22T08:00:00Z"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Team phải tồn tại và không bị vô hiệu hóa (`IsDisable = false`).
- Chỉ lấy đơn đăng ký có `Status = Approved`.
- Sắp xếp đơn đăng ký theo thời gian `CreatedAt` giảm dần và lấy cái đầu tiên (mới nhất).

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
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy team.",
  "MessageCode": "TEAM_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
