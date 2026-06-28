# Staff/Admin approve regrade

## Tác dụng
BTC phê duyệt cho phép chấm lại bài thi phúc khảo. Hành động này đánh dấu submission cần regrade; các judge đã chấm điểm gốc của submission sẽ tự thấy bài trong danh sách regrade của mình.

## URL
`POST /api/v1/staff/reports/{reportId}/regrade`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `reportId` | `guid` | Có | ID của báo cáo/khiếu nại cần phê duyệt chấm lại. |

## Request body
Không có.

## Response body (200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-24T09:00:00Z",
  "message": "REGRADE_APPROVED_SUCCESSFULLY",
  "data": {
    "reportId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "status": 2,
    "statusName": "Approved",
    "isRegrade": true
  }
}
```

## Business rules
- Report phải tồn tại, không bị soft-delete và đang ở trạng thái `Open` (0).
- Chỉ áp dụng cho report có `TypeReport = "Phúc khảo"`.
- Submission liên kết phải tồn tại, không bị soft-delete.
- Submission phải đã có ít nhất một score gốc active (`Scores.IsRetake = false`, `Scores.IsMock = false`).
- Cập nhật `Reports.Status = Approved` (2).
- Cập nhật `Submissions.IsRegrade = true` cho submission liên kết với report.
- Không phân công judge riêng cho regrade. Judge được phép chấm lại là judge đã sở hữu score gốc của submission đó.
- Nếu submission đã `IsRegrade = true`, không duyệt lại lần nữa.

## Errors
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | REPORT_MUST_BE_OPEN |
| 400 | BAD_REQUEST | REPORT_ALREADY_CLOSED |
| 400 | BAD_REQUEST | NOT_APPEAL_TYPE_REPORT |
| 400 | BAD_REQUEST | SUBMISSION_NOT_GRADED |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | REPORT_NOT_FOUND |
| 404 | NOT_FOUND | SUBMISSION_NOT_FOUND |
| 409 | CONFLICT | SUBMISSION_ALREADY_IN_REGRADE |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
