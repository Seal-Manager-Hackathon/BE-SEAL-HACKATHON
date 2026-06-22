# API 5: Lấy thông tin user đăng nhập

## Tác dụng
Lấy thông tin profile ngắn gọn và quyền hạn của user đang đăng nhập.

## URL
`GET /api/v1/auth/me`

## Quyền
Authenticated User (Yêu cầu đăng nhập)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "email": "student@college.edu.vn",
    "firstName": "Hoàng",
    "lastName": "Phạm",
    "role": "Student",
    "isVerified": true
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Yêu cầu access token hợp lệ trong header.
- Token chứa claim định danh của user (`nameid` / `sub`).
- User phải ở trạng thái hoạt động bình thường, chưa bị ban hay disable.

### Bảng vai trò RoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Admin | Quản trị viên hệ thống |
| `1` | Staff | Nhân viên vận hành cuộc thi |
| `2` | Student | Sinh viên / Thí sinh |
| `3` | Lecturer | Giảng viên (Mentor/Judge) |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Unauthorized",
  "Status": 401,
  "Detail": "Không thể xác định danh tính. Token không hợp lệ.",
  "MessageCode": "UNAUTHORIZED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Token bị thiếu, sai hoặc hết hạn. |
| 404 | USER_NOT_FOUND | Không tìm thấy tài khoản tương ứng với token trong DB. |
| 403 | USER_IS_BANNED | Tài khoản của bạn đã bị ban và không được truy cập. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ không mong muốn. |
