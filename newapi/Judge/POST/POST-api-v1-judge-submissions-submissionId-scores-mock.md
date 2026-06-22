# Judge chấm điểm thử (Judge Submit Mock Score)

## Tác dụng
Cho phép Judge hoặc Admin nhập điểm chấm thử/chấm nháp của bài thi (không tính vào điểm số thăng vòng chính thức).

## URL
`POST /api/v1/judge/submissions/{submissionId}/scores/mock`

## Quyền
Judge phụ trách hoặc Admin (Yêu cầu đăng nhập)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `submissionId` (Guid, Bắt buộc): ID của bài nộp.

## Request Body
```json
{
  "totalScore": 75.0,
  "scores": [
    {
      "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
      "score": 20.0,
      "comment": "Chấm nháp thử nghiệm hệ thống."
    }
  ]
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "scoreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "message": "MOCK_SCORE_SUBMITTED"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Ghi nhận bản ghi điểm trong DB với cờ `IsMock = true` để phân biệt hoàn toàn với điểm thi đấu chính thức.
- Điểm mock này sẽ bị bỏ qua khi BTC chạy API kết thúc round và tính toán điểm trung bình thăng vòng cho các team.

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
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Thiếu quyền Judge phụ trách hoặc Admin. |
| 404 | SUBMISSION_NOT_FOUND | Bài thi không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
