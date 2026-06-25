# API 11: Lấy thông tin cá nhân (Profile)

## Tác dụng
Cho phép người dùng đã đăng nhập lấy toàn bộ thông tin cá nhân chi tiết trong hồ sơ của họ.

## URL
`GET /api/v1/users/profile`

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
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Value": {
    "email": "student@college.edu.vn",
    "firstName": "Hoàng",
    "lastName": "Phạm",
    "phoneNumber": "0987654321",
    "avatarUrl": "https://example.com/avatar.jpg",
    "bio": "Đam mê lập trình C#.",
    "address": "123 Đường 3/2, Quận 10, TP.HCM",
    "dateOfBirth": "2004-06-20T00:00:00Z",
    "studentId": "STU123456",
    "college": "Đại Học Bách Khoa",
    "imgUrl": "https://example.com/img.jpg",
    "linkUrl": "https://github.com/johndoe",
    "Status": 0, /* 0: Active, 1: Inactive, 2: Banned */
    "banReason": null
  }
}
```

## Business rules
- Người dùng đang gọi API phải tồn tại trong hệ thống.

### Bảng trạng thái UserStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Active | Tài khoản đang hoạt động bình thường |
| `1` | Inactive | Tài khoản chưa kích hoạt hoặc tạm dừng |
| `2` | Banned | Tài khoản bị cấm |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy thông tin tài khoản người dùng.",
  "MessageCode": "USER_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Token bị thiếu hoặc không hợp lệ. |
| 404 | USER_NOT_FOUND | Người dùng không tồn tại trong DB. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ khi truy vấn cơ sở dữ liệu. |
