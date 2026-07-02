# Admin gắn/kích hoạt tiêu chí chấm điểm cho vòng thi (Admin Activate Criteria)

## Tác dụng
Admin gắn/kích hoạt 1 bộ tiêu chí (CriteriaTemplate) vào vòng thi. Khi gắn, hệ thống tự động hủy kích hoạt tất cả template khác của round đó và chỉ giữ template được chọn ở trạng thái active (`IsDisable = false`).

## URL
`PATCH /api/v1/admin/events/{eventId}/rounds/{roundId}/criteria/{templateId}/activate`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `eventId` | `guid` | Có | ID của sự kiện. |
| `roundId` | `guid` | Có | ID của vòng thi. |
| `templateId` | `guid` | Có | ID của bộ tiêu chí muốn gắn. |

## Request body
Không có.

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": null,
  "message": "CRITERIA_ACTIVATED_SUCCESSFULLY"
}
```

## Business rules
- Event phải tồn tại, không bị soft-disable.
- Round phải thuộc event, không bị soft-disable.
- Template phải tồn tại, thuộc round.
- Logic xử lý:
  1. Tìm tất cả template trong round có `IsDisable = false` (đang được active).
  2. Set các template đó về `IsDisable = true` (deactivate) — **đồng thời các CriteriaItems của template đó cũng set `IsDisable = true`**.
  3. Set template được chọn thành `IsDisable = false` (activate) — **đồng thời các CriteriaItems của template đó cũng set `IsDisable = false`**.
- Chỉ đảm bảo 1 template duy nhất được active trong 1 round tại 1 thời điểm.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND / ROUND_NOT_FOUND / CRITERIA_TEMPLATE_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement trong `Hackathon.Api.Controllers.CriticalController`.
- Route: `PATCH /api/v1/admin/events/{eventId}/rounds/{roundId}/criteria/{templateId}/activate`.
- Sử dụng policy `AdminPolicy`.
- Message: `CRITERIA_ACTIVATED_SUCCESSFULLY`.
