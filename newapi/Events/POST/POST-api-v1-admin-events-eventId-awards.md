# Admin tạo giải thưởng (Admin Create Award)

## Tác dụng
Cho phép Admin tạo một hạng mục giải thưởng mới trong event.

## URL
`POST /api/v1/admin/events/{eventId}/awards`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của sự kiện.

## Request Body
```json
{
  "name": "Giải Nhất",
  "description": "Đội thi xuất sắc nhất toàn giải.",
  "levelAward": 1,
  "numberOfAward": 1,
  "prize": 10000000
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa ID và message thành công.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "id": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
    "message": "AWARD_CREATED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Event phải tồn tại trong DB, không bị soft-disable.
- `name` là bắt buộc, không được để trống.
- `levelAward` xác định thứ hạng giải thưởng (1: Nhất, 2: Nhì, 3: Ba, 4: Khuyến khích, v.v.).
- `numberOfAward` xác định số lượng giải cho hạng mục này (mặc định 1).
- `prize` là giá trị giải thưởng (số dương, đơn vị VND).
- Khi tạo mới, bản ghi mặc định có `IsDisable = false`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy sự kiện.",
  "MessageCode": "EVENT_NOT_FOUND",
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
| 404 | EVENT_NOT_FOUND | Event không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống phát sinh. |
