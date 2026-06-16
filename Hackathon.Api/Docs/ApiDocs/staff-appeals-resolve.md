# Staff resolve appeal

## Tác dụng
Staff hoặc Hội đồng giám khảo phê duyệt đơn phúc khảo và cập nhật điểm số sửa đổi cuối cùng, tức chốt điểm lần 2.

## URL
`PATCH /api/staff/appeals/{appealId}/resolve`

## Authorization
Yêu cầu access token hợp lệ và role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `appealId` | `guid` | Có | Id đơn phúc khảo cần xử lý. |

## Request body
```json
{
  "isApproved": true,
  "finalScore": 8.75,
  "resolutionNote": "string|null"
}
```

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "appealId": "guid",
    "submissionId": "guid",
    "status": "Closed",
    "isApproved": true,
    "finalScore": 8.75,
    "resolutionNote": "string|null",
    "message": "APPEAL_RESOLVED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ Staff/Admin hoặc hội đồng có quyền liên quan được xử lý phúc khảo.
- Đơn phúc khảo dùng dữ liệu `Reports`, không dùng bảng `Appeals` riêng.
- Appeal phải tồn tại, chưa bị soft-disable và đang ở trạng thái `Open`.
- Submission liên quan phải tồn tại.
- Nếu phúc khảo được duyệt, điểm cuối cùng phải được cập nhật theo rule chấm điểm/phúc khảo.
- Sau khi xử lý, appeal chuyển sang trạng thái `Closed`.
- Không cho resolve lại appeal đã đóng.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | STAFF_OR_ADMIN_REQUIRED |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | APPEAL_NOT_FOUND |
| 404 | NOT_FOUND | SUBMISSION_NOT_FOUND |
| 400 | BAD_REQUEST | APPEAL_ALREADY_CLOSED |
| 400 | BAD_REQUEST | FINAL_SCORE_REQUIRED |
| 400 | BAD_REQUEST | FINAL_SCORE_OUT_OF_RANGE |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
