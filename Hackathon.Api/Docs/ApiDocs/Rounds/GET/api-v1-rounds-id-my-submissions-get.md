# Team xem lịch sử bài nộp trong Round (Get My Round Submissions)

## Tác dụng
Cho phép team xem lịch sử các lần nộp bài của chính team trong một round. FE dùng API này ở màn hình chi tiết round, trong thẻ/nút "Bài nộp" để hiển thị danh sách submission đã nộp; khi hết thời gian nộp bài, hệ thống lấy submission mới nhất làm bài chính thức để chấm.

## URL
`GET /api/v1/rounds/{roundId}/my-submissions`

## Quyền
Authenticated Team Member

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `roundId` (Guid, Bắt buộc): ID của round cần xem lịch sử bài nộp.
*   **Query Parameters:**
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số item mỗi trang.

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
        "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
        "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
        "roundName": "Vòng loại",
        "roundDetailId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "url": "https://github.com/seal-hackathon/team-project-web",
        "description": "Bài thi hoàn chỉnh.",
        "status": 0, /* Submitted */
        "submittedAt": "2026-06-22T08:00:00Z",
        "isLatest": true,
        "gradingStatus": "NotGraded"
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
- User phải là thành viên của team có `RoundDetails` trong round này.
- Trả về các submission của team trong round, sắp xếp `submittedAt` giảm dần.
- Submission đầu tiên sau khi sort giảm dần được đánh dấu `isLatest = true` và là bài cuối cùng hệ thống dùng để chấm khi đã hết hạn nộp bài.
- Nếu submission chưa có score chính thức thì `gradingStatus = "NotGraded"` để FE hiển thị "Bài chưa được chấm".
- Nếu đã có score/điểm được công bố thì `gradingStatus = "Graded"` và FE có thể mở chi tiết bằng [`GET /api/v1/submissions/{submissionId}`](Submissions/GET/GET-api-v1-submissions-submissionId.md).

### Bảng trạng thái SubmissionStatusEnum
| Giá trị | Trạng thái | Mô tả |
|---|---|---:|---|
| `0` | Submitted | Đã nộp bài thi thành công |
| `1` | Unsubmitted | Chưa nộp bài |
| `2` | Failed | Nộp bài thất bại |

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | User không thuộc team trong round này. |
| 404 | ROUND_NOT_FOUND | Round không tồn tại. |
| 404 | ROUND_DETAIL_NOT_FOUND | Team không tham gia round này. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống phát sinh. |
