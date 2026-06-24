# Admin xóa giải thưởng (Admin Delete Award)

## Tác dụng
Cho phép Admin soft-disable một hạng mục giải thưởng khỏi event.

## URL
`DELETE /api/v1/admin/awards/{id}`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `id` | `guid` | Có | ID của giải thưởng cần xóa. |

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
  "message": "AWARD_DELETED_SUCCESSFULLY"
}
```

## Business rules
- Bản ghi giải thưởng phải tồn tại trong DB.
- Thực hiện xóa mềm: gán `IsDisable = true` và cập nhật `UpdatedAt`.
- Không xóa cứng khỏi database.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | AWARD_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.EventsController` (`EventsController.cs:62`).
- Route: `DELETE /api/v1/admin/awards/{id:guid}`.
- Sử dụng policy `AdminPolicy`.
- Soft delete: đặt `IsDisable = true` trên bảng `Awards`.
- Message: `AWARD_DELETED_SUCCESSFULLY`.
