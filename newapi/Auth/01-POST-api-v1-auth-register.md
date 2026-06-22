# API 1: Đăng ký tài khoản mới

## Tác dụng
Đăng ký tài khoản student mới và gửi email chứa mã xác thực tài khoản.

## URL
`POST /api/v1/auth/register`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Body
```json
{
  "firstName": "Hoàng",
  "lastName": "Phạm",
  "email": "student@college.edu.vn",
  "password": "SecurePassword123",
  "confirmPassword": "SecurePassword123"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": "REGISTRATION_SUCCESSFUL",
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Email phải chưa tồn tại ở trạng thái đã xác thực.
- Nếu email đã tồn tại nhưng **chưa xác thực** (`IsVerified = false`), hệ thống sẽ tự động gửi lại email xác thực mới và trả về lỗi `UNVERIFIED_ACCOUNT_PLEASE_LOGIN_TO_VERIFY` để yêu cầu người dùng sang trang Login lấy lại mã.
- Nếu email đã tồn tại và **đã xác thực** (`IsVerified = true`), hệ thống báo lỗi `EMAIL_ALREADY_EXISTS`.
- `password` và `confirmPassword` phải trùng nhau (validate phía client).
- Sau khi đăng ký thành công, tài khoản cần verify email trước khi dùng các luồng yêu cầu xác thực.
- Hệ thống gửi email xác thực cho user mới.
- User mới được gán role `Student` mặc định.

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
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Email đã được sử dụng.",
  "MessageCode": "EMAIL_ALREADY_EXISTS",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | VALIDATION_FAILED | Các trường thông tin không hợp lệ hoặc confirmPassword không khớp. |
| 409 | EMAIL_ALREADY_EXISTS | Email đã được đăng ký và xác thực trước đó. |
| 409 | UNVERIFIED_ACCOUNT_PLEASE_LOGIN_TO_VERIFY | Tài khoản đã được tạo nhưng chưa xác thực email, vui lòng login để lấy lại OTP. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống khi gửi email hoặc thao tác database. |
