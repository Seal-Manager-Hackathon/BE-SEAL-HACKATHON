# API 12: Cập nhật thông tin cá nhân (Profile)

## Tác dụng
Cho phép người dùng đã đăng nhập cập nhật thông tin cá nhân trong hồ sơ của họ. Các trường gửi lên sẽ ghi đè dữ liệu cũ.

## URL
`PATCH /api/v1/users/profile`

## Quyền
Authenticated User (Yêu cầu đăng nhập)

## Request Headers
- \`Authorization: Bearer <"AccessToken">\`

## Request Body
```json
{
  "firstName": "Hoàng",
  "lastName": "Phạm",
  "phoneNumber": "0987654321",
  "avatarUrl": "https://example.com/new-avatar.jpg",
  "bio": "Updated bio",
  "address": "456 Updated St",
  "dateOfBirth": "2004-06-20T00:00:00Z",
  "studentId": "STU123456",
  "college": "Đại Học Bách Khoa"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "PROFILE_UPDATED_SUCCESSFULLY",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Chỉ các trường được gửi lên (khác null) mới thực hiện cập nhật đè thông tin cũ.
- Người dùng đang gọi API phải tồn tại trong hệ thống.
- Cập nhật trường tương ứng trong bảng `Users`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy tài khoản người dùng cần cập nhật.",
  "MessageCode": "USER_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | VALIDATION_FAILED | Số điện thoại không đúng định dạng hoặc dữ liệu nhập sai. |
| 401 | MISSING_ACCESS_TOKEN | Access token bị thiếu hoặc không hợp lệ. |
| 404 | USER_NOT_FOUND | Người dùng không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi phát sinh trong server khi lưu database. |
