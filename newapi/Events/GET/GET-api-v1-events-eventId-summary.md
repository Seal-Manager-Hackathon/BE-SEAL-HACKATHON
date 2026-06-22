# Lấy tóm tắt thống kê sự kiện (Event Summary Statistics)

## Tác dụng
Lấy nhanh tóm tắt thống kê của sự kiện (tổng số lượng đội thi được duyệt, số bảng đấu, số vòng thi) để vẽ giao diện dashboard của sự kiện.

## URL
`GET /api/v1/events/{eventId}/summary`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của event cần lấy tóm tắt.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "totalApprovedTeams": 24,
    "totalTracks": 3,
    "totalRounds": 3,
    "totalAwards": 5
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Thống kê dữ liệu trực tiếp trong DB:
  - `totalApprovedTeams`: Đếm số team có `RegisterTeams.Status = Approved` và `IsBanned = false` trong event này.
  - `totalTracks`: Đếm số `Tracks` hoạt động của event.
  - `totalRounds`: Đếm số `Rounds` hoạt động của event.
  - `totalAwards`: Đếm số hạng mục giải thưởng của event.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy event chỉ định.",
  "MessageCode": "EVENT_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | Định dạng eventId không hợp lệ. |
| 404 | EVENT_NOT_FOUND | Event không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
