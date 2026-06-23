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
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": {
    "items": [
      {
        "rank": 1,
        "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "teamName": "Chiến binh công nghệ",
        "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
        "averageScore": 88.5
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 1,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

## Business rules
- Round đấu phải tồn tại trong DB và không bị soft-disable.
- Hệ thống lấy tất cả bài nộp mới nhất (`Submissions`) của các team trong round đấu (`RoundDetails`).
- Điểm trung bình của team trong round được tính bằng trung bình cộng điểm số (`Scores.TotalScore`) của các giám khảo chấm thi (BR-SCO-04).
- Kết quả được sắp xếp theo `averageScore` giảm dần để ra thứ hạng.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | ROUND_NOT_FOUND | Vòng thi không tồn tại hoặc bị disable. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
