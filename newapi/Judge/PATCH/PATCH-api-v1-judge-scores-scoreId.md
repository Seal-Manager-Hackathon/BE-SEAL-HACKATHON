# Judge sửa điểm (Judge Update Score)

## Tác dụng
Giúp Judge sửa lại điểm số tổng hoặc điểm số chi tiết từng tiêu chí đã chấm cho bài thi (chỉ được thực hiện khi điểm chưa được khóa/finalized).

## URL
`PATCH /api/v1/judge/scores/{scoreId}`

## Quyền
Judge sở hữu bảng điểm (Yêu cầu đăng nhập tài khoản Giảng viên chấm thi)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `scoreId` (Guid, Bắt buộc): ID của bảng điểm cần sửa.

## Request Body
```json
{
  "totalScore": 90.0,
  "scores": [
    {
      "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
      "score": 30.0,
      "comment": "Chỉnh sửa: Ý tưởng xuất sắc hơn mong đợi."
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
  "Value": "SCORE_UPDATED_SUCCESSFULLY",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Bảng điểm `scoreId` phải tồn tại trong DB.
- Người gọi phải chính là Judge đã tạo ra bảng điểm này.
- Bảng điểm phải đang ở trạng thái chưa khóa (chưa finalized). Nếu đã finalized, từ chối cập nhật trực tiếp và báo lỗi `SCORE_ALREADY_FINALIZED` (muốn sửa phải báo BTC/Staff để mở khóa - BR-SCO-07).
- Thực hiện kiểm tra lại giới hạn `maxScore` của các tiêu chí cập nhật.
- Cập nhật ghi đè các bản ghi cũ trong bảng `Scores` và `ScoreItems` trong cùng một transaction (BR-SCO-06).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Bảng điểm đã được khóa chung cuộc, không thể chỉnh sửa trực tiếp.",
  "MessageCode": "SCORE_ALREADY_FINALIZED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | SCORE_LIMIT_EXCEEDED | Điểm cập nhật vượt quá điểm tối đa của rubric. |
| 403 | SCORE_ALREADY_FINALIZED | Điểm đã khóa chung cuộc, cấm Judge cập nhật trực tiếp. |
| 403 | SCORE_NOT_OWNED_BY_JUGDE | Bảng điểm này do giám khảo khác chấm, bạn không được sửa. |
| 404 | SCORE_NOT_FOUND | Bảng điểm không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
