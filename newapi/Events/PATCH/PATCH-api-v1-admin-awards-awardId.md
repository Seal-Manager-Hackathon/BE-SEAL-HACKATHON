# Admin cập nhật giải thưởng (Admin Update Award)

## Tác dụng
Cho phép Admin cập nhật thông tin một hạng mục giải thưởng.

## URL
`PATCH /api/v1/admin/awards/{awardId}`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `awardId` (Guid, Bắt buộc): ID của giải thưởng cần cập nhật.

## Request Body
*API hỗ trợ partial update (chỉ cập nhật các trường được truyền khác null).*
```json
{
  "name": "Giải Nhất Cuộc Thi",
  "description": "Đội thi xuất sắc nhất giải đấu.",
  "levelAward": 1,
  "numberOfAward": 1,
  "prize": 15000000
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "AWARD_UPDATED_SUCCESSFULLY",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Bản ghi giải thưởng phải tồn tại và chưa bị soft-disable.
- Nếu `name` được truyền thì không được để trống.
- Cập nhật `UpdatedAt` của bản ghi.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy giải thưởng cần cập nhật.",
  "MessageCode": "AWARD_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | AWARD_NAME_REQUIRED | Tên giải thưởng không được để trống. |
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không có quyền Admin. |
| 404 | AWARD_NOT_FOUND | Giải thưởng không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống phát sinh. |
