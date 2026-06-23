# Staff xem bảng xếp hạng theo Vòng đấu (Get Staff Round Ranking)

## Tác dụng
Cho phép Staff/Admin xem bảng xếp hạng điểm số của toàn bộ đội thi trong một round để xét duyệt đi tiếp trước khi công bố kết quả cho thí sinh.

## URL
`GET /api/v1/staff/rounds/{roundId}/ranking`

## Quyền
Staff/Admin API (yêu cầu đăng nhập và có quyền vận hành/chấm vòng thi tương ứng)

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
        "RegisterTeamId": "9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d",
        "SubmissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
        "AverageScore": 88.5,
        "IsAdvanced": false
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
- Staff/Admin phải có quyền vận hành event/round tương ứng.
- Điểm xếp hạng lấy theo điểm trung bình `Scores.TotalScore` của submission trong round đó.
- API staff có thể xem trước khi public ranking cho thí sinh, dùng để chọn đội vào vòng tiếp theo.
- Kết quả được sắp xếp theo `AverageScore` giảm dần để ra thứ hạng.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Bạn không có quyền xem bảng xếp hạng vòng thi này.",
  "MessageCode": "FORBIDDEN",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | User không có quyền Staff/Admin hoặc không được phân công event/round này. |
| 404 | ROUND_NOT_FOUND | Vòng thi không tồn tại hoặc bị disable. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
