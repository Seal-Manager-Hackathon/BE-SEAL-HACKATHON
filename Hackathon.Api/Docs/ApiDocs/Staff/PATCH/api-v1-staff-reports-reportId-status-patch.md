# Staff/Admin update report status

## Tác dụng
Staff/Admin đóng report/khiếu nại kèm lý do phản hồi. Với report phúc khảo đã duyệt (`Approved`), chỉ được đóng sau khi toàn bộ score gốc đã có score phúc khảo.

## URL
`PATCH /api/v1/staff/reports/{reportId}/status`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `reportId` | `guid` | Có | ID của báo cáo/khiếu nại. |

## Request body
```json
{
  "status": 1,
  "reason": "BTC đã xem xét và hoàn tất xử lý phúc khảo."
}
```

| Field | Kiểu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `status` | `int` | Có | Trạng thái mới. Hiện chỉ hỗ trợ `1` = Closed. |
| `reason` | `string` | Có | Lý do phản hồi từ BTC khi đóng report. |

## Response body (200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-26T14:00:00Z",
  "message": "REPORT_STATUS_UPDATED_SUCCESSFULLY",
  "data": null
}
```

## Business rules
- Report phải tồn tại và không bị soft-delete.
- Không thể chuyển report đã `Closed` về `Open`.
- Không hỗ trợ set status trực tiếp sang `Approved`; duyệt phúc khảo phải dùng `POST /api/v1/staff/reports/{reportId}/regrade`.
- `Open -> Closed`: dùng để từ chối/đóng report thường hoặc từ chối phúc khảo, bắt buộc có `reason`.
- `Approved -> Closed`: chỉ cho phép khi regrade đã hoàn tất.
- Regrade hoàn tất khi tất cả score gốc active (`IsRetake = false`, `IsMock = false`) của submission đều có một score phúc khảo active trỏ về qua `RetakeFromScoreId`.
- Khi đóng report, không set `Submissions.IsRegrade = false`; giữ `true` để lưu lịch sử submission từng được phúc khảo.

## Errors
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | REASON_REQUIRED_WHEN_CLOSING |
| 400 | BAD_REQUEST | CANNOT_REOPEN_CLOSED_REPORT |
| 400 | BAD_REQUEST | CANNOT_SET_APPROVED_DIRECTLY |
| 400 | BAD_REQUEST | REGRADE_NOT_COMPLETED |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | REPORT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
