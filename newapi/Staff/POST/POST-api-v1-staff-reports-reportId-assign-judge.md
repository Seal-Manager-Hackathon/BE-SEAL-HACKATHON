# BTC phân công Judge chấm phúc khảo (Staff Assign Regrade Judge)

## Tác dụng
BTC phân công giám khảo khác (hoặc giám khảo được chọn) chấm lại bài thi phúc khảo. Report phúc khảo có `submissionId`, Staff/Admin dựa vào `submissionId` đó để gán đúng bài nộp cho judge chấm lại.

## URL
`POST /api/v1/staff/reports/{reportId}/assign-judge`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `reportId` (Guid, Bắt buộc): ID của khiếu nại phúc khảo.

## Request Body
```json
{
  "judgeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "JUDGE_ASSIGNED_FOR_REGRADE",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Báo cáo phải tồn tại và đang mở cho phép phúc khảo.
- Báo cáo phải có `SubmissionId` hợp lệ để xác định bài nộp cần chấm lại.
- Giám khảo `judgeId` được chọn phải là Lecturer đang hoạt động và được phân công làm Judge trong event/track phù hợp.
- Tạo bản ghi phân công chấm điểm mới hoặc gán liên kết bài thi với giám khảo đó trong DB để thực hiện chấm lại.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy giảng viên chấm thi.",
  "MessageCode": "JUDGE_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Không được phân công phụ trách event. |
| 404 | REPORT_NOT_FOUND | Báo cáo khiếu nại không tồn tại. |
| 404 | JUDGE_NOT_FOUND | Giảng viên chỉ định không tồn tại hoặc chưa được gán vai trò Judge của event. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
