# Judge chấm điểm phúc khảo (Judge Submit Regrade Score)

## Tác dụng
Giúp Judge được BTC phân công thực hiện chấm lại điểm bài thi phúc khảo (ghi nhận điểm phúc khảo riêng biệt).

## URL
`POST /api/v1/judge/scores/{scoreId}/retake`

## Quyền
Judge được BTC phân công chấm lại (Yêu cầu đăng nhập tài khoản Giảng viên)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `scoreId` (Guid, Bắt buộc): ID của bảng điểm cần chấm lại.

## Request Body
```json
{
  "totalScore": 88.0,
  "scores": [
    {
      "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
      "score": 28.0,
      "comment": "Chấm lại: Đã xem xét kỹ khiếu nại của thí sinh."
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
    "message": "REGRADE_SCORE_SUBMITTED"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Bảng điểm `scoreId` phải tồn tại trong DB.
- Trận đấu/Bài thi liên quan phải được BTC phê duyệt cho phép phúc khảo (thể hiện qua trạng thái của bản ghi khiếu nại trong bảng `Reports` liên kết).
- Giám khảo thực hiện chấm lại phải được BTC chỉ định phân công.
- Cập nhật trường `IsRetake = true` cho bản ghi điểm mới chấm lại trong DB để biểu thị đây là điểm phúc khảo (BR-REP-06, BR-SCO-04).
- Bản ghi cũ được giữ nguyên hoặc cập nhật tùy cấu hình, điểm phúc khảo này sẽ là kết quả chấm cuối cùng của bài thi (BR-REP-06).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Bài thi này chưa được duyệt cho phép phúc khảo.",
  "MessageCode": "REGRADE_NOT_APPROVED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Bạn không phải là giám khảo được BTC chỉ định chấm lại bài thi này. |
| 404 | SCORE_NOT_FOUND | Không tìm thấy bảng điểm cũ. |
| 409 | REGRADE_NOT_APPROVED | BTC chưa duyệt mở cổng chấm lại cho bài thi này. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
