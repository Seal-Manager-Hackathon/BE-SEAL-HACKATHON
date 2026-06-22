# API 56: Lấy danh sách bài nộp (Get Round Submissions)

## Tác dụng
Lấy danh sách phân trang các bài nộp thi của toàn bộ các team tham gia vòng đấu (phục vụ cho Staff/Admin/Judge xem bài thi).

## URL
`GET /api/v1/rounds/{roundId}/submissions`

## Quyền
Authenticated User (Yêu cầu đăng nhập, BTC/Judge được quyền xem toàn bộ)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `roundId` (Guid, Bắt buộc): ID của vòng thi.
*   **Query Parameters:**
    *   `pageIndex` (int, Không bắt buộc, mặc định: 1): Trang hiện tại.
    *   `pageSize` (int, Không bắt buộc, mặc định: 10): Số phần tử trên một trang.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BasePaginationResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Value": {
    "Items": [
      {
        "id": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
        "roundDetailId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "teamName": "Chiến binh công nghệ",
        "url": "https://github.com/seal-hackathon/team-project-web",
        "description": "Bài thi Web App.",
        "Status": 0, /* Submitted */
        "submittedAt": "2026-06-22T08:00:00Z"
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
- Vòng thi phải tồn tại trong DB và không bị soft-disable.
- Sắp xếp mặc định theo thời gian nộp bài giảm dần (`SubmittedAt` giảm dần) để lấy bài thi mới nhất.
- Với mỗi team có nhiều lần nộp trong round, bài nộp cuối cùng trước khi kết thúc thời gian nộp bài là bài chính thức được dùng để chấm.
- API này phục vụ Staff/Admin/Judge xem danh sách bài nộp của round; team xem lịch sử bài nộp của chính mình qua [`GET /api/v1/rounds/{roundId}/my-submissions`](./GET-api-v1-rounds-roundId-my-submissions.md).

### Bảng trạng thái SubmissionStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Submitted | Đã nộp bài thi thành công |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Unauthorized",
  "Status": 401,
  "Detail": "Vui lòng xác thực tài khoản.",
  "MessageCode": "UNAUTHORIZED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 404 | ROUND_NOT_FOUND | Vòng thi không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
