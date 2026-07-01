# Admin xóa vòng thi (Admin Delete Round)

## Tác dụng
Admin xóa mềm (soft delete) một vòng thi — set `IsDisable = true`. Đồng thời chuẩn hoá RoundNo và cập nhật NumberRound của event.

## URL
`DELETE /api/v1/admin/rounds/{roundId}`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `roundId` | `guid` | Có | ID vòng thi cần xóa. |

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
  "message": "ROUND_DELETED_SUCCESSFULLY"
}
```

## Business rules
- Round phải tồn tại → 404 `ROUND_NOT_FOUND`.
- **Event chưa bắt đầu** (`StartTime > now`) mới được xóa round. Nếu event đã bắt đầu → 400 `EVENT_ALREADY_STARTED`.
- Soft-delete: set `IsDisable = true`, `UpdatedAt = now`.
- **Chuẩn hoá RoundNo:** tất cả round có `RoundNo > RoundNo của round bị xóa` được giảm 1 (để RoundNo luôn liên tục 1, 2, 3...).
- **Giảm NumberRound** của event đi 1.
- Các bản ghi liên quan (`RoundDetails`, `Submissions`, `Scores`, `CriteriaTemplates`) giữ nguyên.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | EVENT_ALREADY_STARTED |
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
