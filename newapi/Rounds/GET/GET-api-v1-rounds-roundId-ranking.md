# Xem bảng xếp hạng theo Vòng đấu (Get Round Ranking)

## Tác dụng
Xem bảng xếp hạng điểm số của toàn bộ các đội thi trong một vòng đấu (Round) cụ thể (lấy theo điểm trung bình của bài nộp Submission).

## URL
`GET /api/v1/rounds/{roundId}/ranking`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Path Parameters:**
    *   `roundId` (Guid, Bắt buộc): ID của vòng đấu cần xem xếp hạng.
*   **Query Parameters:**
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên một trang.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse` chứa danh sách thứ hạng.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "Items": [
      {
        "Rank": 1,
        "TeamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "TeamName": "Chiến binh công nghệ",
        "SubmissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
        "AverageScore": 88.5
      }
    ],
    "PageIndex": 1,
    "PageSize": 10,
    "TotalCount": 1,
    "HasNextPage": false,
    "HasPreviousPage": false
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Round đấu phải tồn tại trong DB và không bị soft-disable.
- Hệ thống lấy tất cả bài nộp mới nhất (`Submissions`) của các team trong round đấu (`RoundDetails`).
- Điểm trung bình của team trong round được tính bằng trung bình cộng điểm số (`Scores.TotalScore`) của các giám khảo chấm thi (BR-SCO-04).
- Kết quả được sắp xếp theo `AverageScore` giảm dần để ra thứ hạng.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy vòng thi đấu.",
  "MessageCode": "ROUND_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | ROUND_NOT_FOUND | Vòng thi không tồn tại hoặc bị disable. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
