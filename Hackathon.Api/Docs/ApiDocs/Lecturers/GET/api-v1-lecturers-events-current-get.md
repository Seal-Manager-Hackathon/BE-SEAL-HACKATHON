# Giảng viên xem sự kiện đang diễn ra

## Tác dụng
Kiểm tra và hiển thị các sự kiện mà giảng viên được phân công và đang diễn ra ở thời điểm hiện tại (thời gian hiện tại nằm trong khoảng `StartTime` đến `EndTime` của sự kiện).

## URL
`GET /api/v1/lecturers/events/current`

## Quyền
Lecturer đã được phân công trong sự kiện (Yêu cầu đăng nhập tài khoản Giảng viên)

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
Không có.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` với định dạng camelCase:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "SUCCESS",
  "data": [
    {
      "assignEventId": "b1a7d6c2-4821-4f9b-bd5e-3c2fa56789e0",
      "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
      "eventName": "SEAL Hackathon 2026",
      "season": "Mùa hè 2026",
      "startTime": "2026-07-01T08:00:00Z",
      "endTime": "2026-07-10T17:00:00Z",
      "role": 0, /* 0: Mentor, 1: Judge */
      "eventStatus": 1 /* 0: Draft, 1: Published, 2: Closed, 3: Cancelled */
    }
  ]
}
```

## Business rules
- Người gọi phải là giảng viên (`role = 3` tương ứng `RoleEnum.Lecturer` trong `Users`).
- Chỉ lấy các sự kiện mà giảng viên được phân công trong bảng `AssignEvents` và sự kiện chưa bị disable.
- Thời gian hiện tại phải nằm trong khoảng `StartTime` đến `EndTime` của sự kiện (`StartTime ≤ now ≤ EndTime`).
- Nếu không có sự kiện nào đang diễn ra, trả về lỗi 404 `NOT_FOUND` với message `NOT_ASSIGNED_TO_ANY_EVENT`.
- Kết quả được sắp xếp theo thời gian tạo phân công giảm dần.

### Bảng vai trò EventRoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |
| `2` | Staff | Nhân viên vận hành sự kiện |

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
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | NOT_ASSIGNED_TO_ANY_EVENT |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
