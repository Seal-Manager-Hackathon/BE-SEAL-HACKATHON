# API 49: Đếm số Team đăng ký vào Track

## Tác dụng
Lấy số lượng Team hiện đang được assign (gán) vào một Track nhất định trong hệ thống, bao gồm số team giới hạn tối đa (`MaxTeam`) của phân ban.

## URL
`GET /api/v1/tracks/{trackId}/teams/count`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Path Parameters:**
    *   `trackId` (Guid, Bắt buộc): ID của phân ban (Track) cần đếm số team.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "eventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "Title": "Web Application",
    "maxTeam": 50,
    "currentTeamCount": 12
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Không yêu cầu đăng nhập.
- `trackId` là bắt buộc, Track không bị soft-disable.
- Event chứa Track đó cũng phải chưa bị soft-disable. Nếu không thoả, trả về `TRACK_NOT_FOUND` hoặc `EVENT_NOT_FOUND`.
- `currentTeamCount` chỉ đếm các Team thuộc về Track đó đã được phê duyệt đơn đăng ký (`Status = Approved`), và cả đơn `RegisterTeams` cũng như `Teams` đều không bị soft-disable.
- Trả về `maxTeam` (nếu có cấu hình) để frontend dễ dàng xác định xem Track đó đã đầy hay chưa.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy bảng đấu tương ứng.",
  "MessageCode": "TRACK_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | TRACK_NOT_FOUND | Bảng đấu không tồn tại hoặc đã bị disable. |
| 404 | EVENT_NOT_FOUND | Sự kiện không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
