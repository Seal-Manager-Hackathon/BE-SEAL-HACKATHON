# Get events with most participants

## Tác dụng
Lấy danh sách event có nhiều người tham gia nhất, không quan tâm thời gian diễn ra.

## URL
`GET /api/v1/events/most-participants`

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `limit` | `int` | Không | Số lượng event cần lấy, mặc định `10`. |
| `isDisable` | `bool` | Không | Lọc theo trạng thái soft-disable của event. Nếu không truyền, mặc định chỉ trả event chưa bị disable (`IsDisable = false`). |

## Ví dụ request
```http
GET /api/v1/events/most-participants?limit=10&isDisable=false
```

## Request body
Không có.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Message": "SUCCESS",
  "Data": [
    {
      "id": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
      "name": "SEAL Hackathon 2026",
      "description": "Cuộc thi lập trình SEAL Hackathon mùa hè 2026.",
      "startTime": "2026-07-01T08:00:00Z",
      "endTime": "2026-07-10T17:00:00Z",
      "registerLimitTime": "2026-06-30T23:59:59Z",
      "limitTeam": 50,
      "minMember": 3,
      "maxMember": 5,
      "status": 0, /* 0: Draft, 1: Published, 2: Closed, 3: Cancelled */
      "numberRound": 3,
      "season": "Mùa hè 2026",
      "isDisable": false,
      "createdAt": "2026-06-22T08:00:00Z",
      "teamCount": 10,
      "participantCount": 45
    }
  ]
}
```

## Business rules
- API không yêu cầu đăng nhập.
- Không lọc theo thời gian diễn ra event.
- Số người tham gia được tính từ member của các team đã đăng ký event.
- Chỉ tính `RegisterTeams` chưa bị soft-disable và có trạng thái `Approved`.
- `participantCount` tính số member active, chưa disable trong `TeamDetails`.
- `teamCount` tính số team hợp lệ tham gia event.
- Kết quả sắp xếp theo `participantCount` giảm dần, sau đó `teamCount` giảm dần.
- Nếu không truyền `isDisable`, mặc định chỉ trả event chưa bị soft-disable.

### Bảng trạng thái EventStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Draft | Sự kiện đang nháp, chưa công bố |
| `1` | Published | Sự kiện đã công bố và hoạt động |
| `2` | Closed | Sự kiện đã kết thúc và đóng lại |
| `3` | Cancelled | Sự kiện đã bị hủy bỏ |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | INVALID_LIMIT_PARAMETER |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
