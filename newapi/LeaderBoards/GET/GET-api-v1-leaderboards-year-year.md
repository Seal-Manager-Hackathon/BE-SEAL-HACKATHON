# Xem bảng xếp hạng mùa giải năm (Get Year Leaderboard)

## Tác dụng
Xem bảng xếp hạng tích lũy điểm số của toàn bộ các event được tổ chức trong năm (mùa giải).

## URL
`GET /api/v1/leaderboards/year/{year}`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Path Parameters:**
    *   `year` (int, Bắt buộc): Năm của mùa giải cần xem xếp hạng (ví dụ: 2026).

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa danh sách chi tiết xếp hạng tích lũy.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": [
    {
      "rank": 1,
      "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
      "teamName": "Chiến binh công nghệ",
      "totalYearScore": 810,
      "eventsParticipated": 3
    }
  ],
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Điểm tích lũy năm của team bằng tổng điểm của toàn bộ các event team đã tham gia trong năm (BR-LB-04).
- Nếu team không tham gia đủ số event trong năm, hệ thống vẫn cộng điểm các event đã tham gia thi đấu (không loại bỏ khỏi leaderboard, BR-LB-05).
- Sắp xếp kết quả xếp hạng theo `totalYearScore` giảm dần.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Bad Request",
  "Status": 400,
  "Detail": "Năm chỉ định không hợp lệ.",
  "MessageCode": "INVALID_YEAR",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | INVALID_YEAR | Tham số năm truyền lên sai định dạng hoặc không có giải đấu nào được tổ chức trong năm đó. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
