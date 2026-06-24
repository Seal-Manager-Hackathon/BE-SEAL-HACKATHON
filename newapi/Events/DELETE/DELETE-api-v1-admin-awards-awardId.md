# Admin xóa giải thưởng (Admin Delete Award)

## Tác dụng
Cho phép Admin soft-disable một hạng mục giải thưởng khỏi event.

## URL
`DELETE /api/v1/admin/awards/{awardId}`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `awardId` (Guid, Bắt buộc): ID của giải thưởng cần xóa.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "AWARD_DELETED_SUCCESSFULLY",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Bản ghi giải thưởng phải tồn tại trong DB.
- Thực hiện xóa mềm: gán `IsDisable = true` và cập nhật `UpdatedAt`.
- Không xóa cứng khỏi database.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy giải thưởng cần xóa.",
  "MessageCode": "AWARD_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không có quyền Admin. |
| 404 | AWARD_NOT_FOUND | Giải thưởng không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống phát sinh. |
