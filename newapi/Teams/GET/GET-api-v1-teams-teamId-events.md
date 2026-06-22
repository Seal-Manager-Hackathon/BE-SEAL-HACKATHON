# API 31: Danh sách sự kiện đăng ký của Team

## Tác dụng
API dùng để lấy danh sách toàn bộ các event mà một Team cụ thể (dựa vào `teamId`) đã nộp đơn đăng ký/tham gia (bao gồm các trạng thái: Pending, Approved, Rejected). Khi user bấm vào một event trong danh sách, FE dùng `eventId` để gọi [`GET /api/v1/events/{eventId}`](../Event/15-GET-api-v1-events-eventId.md) xem chi tiết event.

## URL
`GET /api/v1/teams/{teamId}/events`

## Quyền
Authenticated User (Yêu cầu đăng nhập, cho phép thành viên trong team xem lịch sử)

## Request Headers
- \`Authorization: Bearer <"AccessToken">\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của Team cần tra cứu.
*   **Query Parameters:**
    *   `status` (string, Không bắt buộc): Lọc trạng thái (Pending, Approved, Rejected). Bỏ trống sẽ lấy toàn bộ.
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Trang hiện tại (mặc định 1)
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số lượng item trên mỗi trang (mặc định 10)

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
        "teamName": "Tên team",
        "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
        "eventName": "Hackathon ABC",
        "Status": 1 /* Approved */,
        "description": "Mô tả nếu có",
        "createdAt": "2026-06-19T10:00:00Z"
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
- Không yêu cầu role đặc biệt, user nào đăng nhập cũng có thể xem được lịch sử đăng ký event của team.
- Team phải đang không bị soft-disable (`IsDisable = false`).
- Trả về toàn bộ các đơn đăng ký (`RegisterTeams`) của `teamId` đó bất kể tình trạng duyệt (Pending/Approved/Rejected).
- Danh sách được sắp xếp mới nhất lên trước (`CreatedAt` giảm dần).

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
  "Detail": "Không tìm thấy thông tin nhóm thi đấu.",
  "MessageCode": "TEAM_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | INVALID_STATUS | Lọc trạng thái `status` không khớp với enum giá trị. |
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại hoặc đã bị disable. |
| 500 | INTERNAL_SERVER_ERROR | Gặp sự cố tại server. |
