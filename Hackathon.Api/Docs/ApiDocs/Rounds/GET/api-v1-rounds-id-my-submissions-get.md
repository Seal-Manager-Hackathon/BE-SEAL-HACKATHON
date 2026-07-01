# Team xem bài nộp mới nhất của team trong Round

## Tác dụng
Cho phép team xem bài nộp MỚI NHẤT của chính team trong một round.

**Chỉ trả về 1 bài nộp duy nhất — bài cuối cùng của team.**  
Các lần nộp cũ trước đó không được hiển thị (chỉ dùng để ghi log).

## URL
`GET /api/v1/rounds/{roundId}/my-submissions`

## Quyền
Authenticated Team Member

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `roundId` (Guid, Bắt buộc): ID của round cần xem.
*   **Query Parameters:**
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1)
    *   `pageSize` (int, Không bắt buộc, mặc định: 10)

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Data": {
    "Items": [
      {
        "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
        "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
        "roundName": "Vòng loại",
        "roundDetailId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "url": "https://github.com/seal-hackathon/team-project-web",
        "description": "Bài thi hoàn chỉnh.",
        "status": 0,
        "submittedAt": "2026-06-22T08:00:00Z",
        "isLatest": true,
        "gradingStatus": "NotGraded"
      }
    ],
    "PageIndex": 1,
    "PageSize": 10,
    "TotalCount": 1,
    "HasNextPage": false,
    "HasPreviousPage": false
  }
}
```

## Business rules
- User phải là thành viên của team có `RoundDetails` trong round này.
- **Chỉ trả về 1 bài nộp duy nhất** — bài mới nhất (`.FirstOrDefaultAsync()` sau sort `SubmittedAt` desc).
- `isLatest` luôn là `true` vì chỉ trả về bài cuối.
- Nếu chưa có điểm chính thức → `gradingStatus = "NotGraded"`.
- Nếu đã có điểm → `gradingStatus = "Graded"` và FE có thể mở chi tiết bằng `GET /api/v1/submissions/{submissionId}`.

### Bảng trạng thái SubmissionStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Submitted | Đã nộp bài thi thành công |
| `1` | Unsubmitted | Chưa nộp bài |
| `2` | Failed | Nộp bài thất bại |

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 404 | NOT_FOUND | ROUND_DETAIL_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
