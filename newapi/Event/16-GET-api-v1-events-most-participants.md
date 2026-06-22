# API 16: Lấy danh sách sự kiện nổi bật (Most Participants)

## Tác dụng
Lấy danh sách các event có số lượng thí sinh tham gia đông nhất, không quan tâm đến thời gian diễn ra của event.

## URL
`GET /api/v1/events/most-participants`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Query Parameters:**
    *   `limit` (int, Không bắt buộc, mặc định: 10): Số lượng event cần lấy.
    *   `isDisable` (bool, Không bắt buộc, mặc định: false): `true` để lấy cả event đã disable, `false` để lọc chỉ lấy event chưa disable.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa danh sách sự kiện kèm theo số liệu thống kê.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Value": [
    {
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
      "createdAt": "2026-06-20T08:00:00Z",
      "teamCount": 15,
      "participantCount": 75
    }
  ]
}
```

## Business rules
- Số lượng người tham gia được tính từ tổng số thành viên của các team đã đăng ký sự kiện và đơn đăng ký được duyệt (`RegisterTeams` có `Status = Approved`).
- Chỉ tính các thành viên ở trạng thái `Active` trong bảng `TeamDetails`.
- Sắp xếp danh sách trả về theo số lượng thí sinh (`participantCount`) giảm dần, sau đó theo số lượng team (`teamCount`) giảm dần.

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
  "Title": "Bad Request",
  "Status": 400,
  "Detail": "Tham số limit không hợp lệ.",
  "MessageCode": "BAD_REQUEST",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Tham số limit không đúng định dạng số nguyên dương. |
| 500 | INTERNAL_SERVER_ERROR | Gặp sự cố không mong muốn tại server. |
