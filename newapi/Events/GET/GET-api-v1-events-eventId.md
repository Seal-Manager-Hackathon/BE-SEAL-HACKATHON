# API 15: Xem chi tiết sự kiện (Event Detail)

## Tác dụng
Lấy thông tin cấu hình chi tiết đầy đủ của một event cụ thể dựa trên `eventId`.

## URL
`GET /api/v1/events/{eventId}`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của event cần xem chi tiết.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Value": {
    "id": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
    "name": "SEAL Hackathon 2026",
    "description": "Giải đấu lập trình thường niên cho sinh viên.",
    "startTime": "2026-07-01T08:00:00Z",
    "endTime": "2026-07-03T18:00:00Z",
    "registerLimitTime": "2026-06-28T23:59:59Z",
    "limitTeam": 50,
    "minMember": 3,
    "maxMember": 5,
    "Status": 1, /* Published */
    "numberRound": 3,
    "season": "Summer 2026",
    "isDisable": false,
    "createdAt": "2026-06-20T08:00:00Z"
  }
}
```

## Business rules
- Tìm kiếm chính xác event theo ID.
- Trả về đầy đủ tất cả các trường thông tin của event (bao gồm các trường cấu hình như mô tả, thời gian đăng ký tối hạn, giới hạn số lượng team, số thành viên tối đa/tối thiểu của team, số vòng đấu, v.v.).
- Nếu không tìm thấy, trả lỗi `404 Not Found` (`EVENT_NOT_FOUND`).

### Bảng trạng thái EventStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Draft | Nháp (Thí sinh không nhìn thấy) |
| `1` | Published | Đang diễn ra / Mở đăng ký |
| `2` | Closed | Đã đóng / Kết thúc giải đấu |
| `3` | Cancelled | Đã hủy |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy event có ID tương ứng.",
  "MessageCode": "EVENT_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Định dạng ID (`eventId`) không đúng chuẩn GUID. |
| 404 | EVENT_NOT_FOUND | Event không tồn tại trong hệ thống. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống khi kết nối DB. |
