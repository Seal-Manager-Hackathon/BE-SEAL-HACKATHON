# API 19: Tạo sự kiện mới (Admin Create Event)

## Tác dụng
Cho phép Admin tạo một giải đấu Hackathon mới trong hệ thống.

## URL
`POST /api/v1/admin/events`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản có quyền Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Body
```json
{
  "name": "SEAL Hackathon 2026",
  "description": "Giải đấu lập trình thường niên cho sinh viên.",
  "startTime": "2026-07-01T08:00:00Z",
  "endTime": "2026-07-03T18:00:00Z",
  "registerLimitTime": "2026-06-28T23:59:59Z",
  "limitTeam": 50,
  "minMember": 3,
  "maxMember": 5,
  "numberRound": 3,
  "season": "Summer 2026"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa ID và message thành công.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "id": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
    "message": "EVENT_CREATED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- `name` là bắt buộc, không được để trống hoặc chỉ chứa khoảng trắng, nếu trùng tên đã có (không phân biệt hoa thường) báo lỗi `EVENT_NAME_ALREADY_EXISTS`.
- Khi tạo mới, event mặc định có trạng thái `Status = Draft` và `IsDisable = false`.
- `startTime` phải diễn ra trước `endTime` (nếu truyền), nếu sai trả lỗi `START_TIME_MUST_BE_BEFORE_END_TIME`.
- `registerLimitTime` phải diễn ra trước `startTime` (nếu truyền), nếu sai trả lỗi `REGISTER_LIMIT_TIME_MUST_BE_BEFORE_START_TIME`.
- `CreatedAt` và `UpdatedAt` được tự động ghi nhận theo mốc thời gian UTC hiện tại.

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
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Tên sự kiện này đã được sử dụng.",
  "MessageCode": "EVENT_NAME_ALREADY_EXISTS",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | EVENT_NAME_REQUIRED | Tên event không được bỏ trống. |
| 400 | START_TIME_MUST_BE_BEFORE_END_TIME | Thời gian bắt đầu diễn ra sau kết thúc. |
| 400 | REGISTER_LIMIT_TIME_MUST_BE_BEFORE_START_TIME | Hạn chót đăng ký diễn ra sau khi sự kiện bắt đầu. |
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Tài khoản của bạn không có vai trò Admin. |
| 409 | EVENT_NAME_ALREADY_EXISTS | Tên event đã tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
