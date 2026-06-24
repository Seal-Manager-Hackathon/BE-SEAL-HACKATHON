# Khôi phục sự kiện (Admin Restore Event)

## Tác dụng
Khôi phục sự kiện bị soft-delete (`IsDisable = true`) quay lại trạng thái hoạt động bình thường (`IsDisable = false`).

## URL
`PATCH /api/v1/admin/events/{eventId}/restore`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | ID của event cần khôi phục. |

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
  "message": "EVENT_RESTORED_SUCCESSFULLY"
}
```

## Business rules
- Event phải tồn tại trong DB, nếu không báo lỗi `EVENT_NOT_FOUND`.
- Chuyển cờ `IsDisable = false` và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.
- Giúp sự kiện hiển thị lại trên giao diện quản trị của Admin/Staff và giao diện chính của Thí sinh nếu đã được publish.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ⏳ **Đề xuất**: Chưa implement trong code hiện tại. Entity: `Events.IsDisable`.
