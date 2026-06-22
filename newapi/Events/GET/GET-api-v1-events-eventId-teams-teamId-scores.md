# Xem chi tiết điểm của Team theo từng Round & Tiêu chí (Get Team Score Details)

## Tác dụng
Cho phép xem chi tiết bảng điểm của một team cụ thể qua từng vòng thi (Round) trong sự kiện, bao gồm điểm số chi tiết cho từng tiêu chí chấm điểm (Criteria Items).

## URL
`GET /api/v1/events/{eventId}/teams/{teamId}/scores`

## Quyền
Public API (Hoặc Authenticated tùy theo thời điểm BTC reveal điểm số)

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của sự kiện.
    *   `teamId` (Guid, Bắt buộc): ID của đội thi cần xem điểm chi tiết.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa mảng kết quả điểm của từng round.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": [
    {
      "RoundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
      "RoundName": "Vòng loại",
      "RoundNo": 1,
      "AverageTotalScore": 88.5,
      "CriteriaScores": [
        {
          "CriteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
          "CriteriaItemName": "Tính thực tiễn",
          "AverageCriteriaScore": 27.5,
          "MaxScore": 30.0
        },
        {
          "CriteriaItemId": "f1f2a3b4-c5d6-e7f8-a9b0-c5d6e7f8a9b1",
          "CriteriaItemName": "Độ hoàn thiện kỹ thuật",
          "AverageCriteriaScore": 61.0,
          "MaxScore": 70.0
        }
      ]
    }
  ],
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Event và Team phải tồn tại trong DB và chưa bị soft-disable.
- Chỉ hiển thị điểm số chi tiết khi sự kiện đã được công bố điểm (hoặc kết quả round đấu tương ứng đã được publish).
- Điểm trung bình của từng tiêu chí (`AverageCriteriaScore`) được tính bằng trung bình cộng điểm số của các giám khảo chấm trên tiêu chí đó (`ScoreItems.Score`).
- Điểm trung bình tổng của round (`AverageTotalScore`) bằng trung bình cộng tổng điểm chấm của các giám khảo (`Scores.TotalScore`).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Điểm số của vòng thi đấu này chưa được ban tổ chức công bố.",
  "MessageCode": "SCORES_NOT_REVEALED_YET",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ (nếu yêu cầu đăng nhập). |
| 403 | SCORES_NOT_REVEALED_YET | Điểm thi của team chưa được BTC mở hiển thị (reveal). |
| 404 | EVENT_NOT_FOUND | Sự kiện không tồn tại. |
| 404 | TEAM_NOT_FOUND | Đội thi không tồn tại hoặc đã bị disable. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
