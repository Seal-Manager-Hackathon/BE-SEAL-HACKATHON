# API 2: Đăng nhập hệ thống

## Tác dụng
Đăng nhập bằng email/password, nhận access token và refresh token.

## URL
`POST /api/v1/auth/login`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Body
```json
{
  "email": "student@college.edu.vn",
  "password": "SecurePassword123"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`. Access token và refresh token cũng đồng thời được set vào HTTP-only cookie.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "accessToken": "ey...",
    "refreshToken": "rf...",
    "message": "LOGIN_SUCCESSFUL"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Email và password là bắt buộc.
- Email/password phải khớp với tài khoản đang tồn tại.
- Tài khoản bị disable (`IsDisable = true`) không thể đăng nhập.
- Tài khoản bị cấm (`Status = Banned`) không thể đăng nhập.
- Tài khoản chưa xác thực email (`IsVerified = false` hoặc `VerifyEmailAt == null`) nhưng nhập đúng password, hệ thống sẽ tự động gửi lại OTP qua email và block luồng đăng nhập (báo lỗi `EMAIL_UNVERIFIED_OTP_SENT`).
- Đăng nhập thành công sẽ cấp access token và refresh token mới.
- Access token và refresh token được trả về trong response body và set vào HTTP-only cookie.

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
  "Title": "Unauthorized",
  "Status": 401,
  "Detail": "Email hoặc mật khẩu không chính xác.",
  "MessageCode": "INVALID_EMAIL_OR_PASSWORD",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | VALIDATION_FAILED | Định dạng email không hợp lệ hoặc thiếu trường bắt buộc. |
| 404 | EMAIL_NOT_FOUND | Email không tồn tại trên hệ thống. |
| 403 | USER_IS_BANNED | Tài khoản đang bị cấm tham gia hệ thống. |
| 401 | EMAIL_UNVERIFIED_OTP_SENT | Tài khoản chưa verify email. Hệ thống đã tự động kích hoạt gửi lại mã OTP về hòm thư. |
| 401 | INVALID_EMAIL_OR_PASSWORD | Sai mật khẩu hoặc email. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống phát sinh. |
