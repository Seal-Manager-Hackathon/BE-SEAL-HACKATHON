# Admin khôi phục vòng thi (Admin Restore Round)

## Tác dụng
Khôi phục một round đã bị soft-delete (IsDisable = true) trở lại hoạt động.

## URL
`PATCH /api/v1/admin/rounds/{roundId}/restore`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `roundId` | `guid` | Có | ID vòng thi cần khôi phục. |

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
  "message": "ROUND_RESTORED_SUCCESSFULLY"
}
```

## Business rules
- Round phải tồn tại và đang bị disable (`IsDisable = true`). Nếu ko tìm thấy → 404. Nếu chưa disable → 409.
- Event phải tồn tại và chưa disable.
- Round được gán lại `RoundNo = max RoundNo hiện tại + 1` (đặt xuống cuối danh sách).
- `NumberRound` của event được +1.
- **Criteria templates/items vẫn giữ nguyên trạng thái disable** — admin phải tự active lại nếu cần.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 409 | CONFLICT | ROUND_NOT_DISABLED |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
