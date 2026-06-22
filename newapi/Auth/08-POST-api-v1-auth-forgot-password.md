# API 8: Yêu cầu quên mật khẩu

## Tác dụng
Gửi yêu cầu quên mật khẩu. Hệ thống sẽ tạo token đặt lại mật khẩu và gửi link reset password tuyệt đối về hòm thư của user. Khi user bấm link, FE mở trang đặt lại mật khẩu, đọc token trên URL và hiển thị form nhập mật khẩu mới / nhập lại mật khẩu.

## URL
`POST /api/v1/auth/forgot-password`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Body
```json
{
  "email": "student@college.edu.vn"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "message": "FORGOT_PASSWORD_REQUEST_ACCEPTED"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- `email` là bắt buộc và phải đúng định dạng email.
- Nếu email tồn tại, hệ thống tạo bản ghi trong bảng `ResetPasswords` chứa token/OTP và gửi mail cho user.
- Email reset password phải chứa link tuyệt đối tới trang FE đặt lại mật khẩu, ví dụ: `https://<frontend-domain>/reset-password?token=<reset_token>`.
- Sau khi user bấm link, FE đọc token trên URL và gọi [`POST /api/v1/auth/reset-password`](./09-POST-api-v1-auth-reset-password.md) với `token`, `newPassword`, `confirmPassword`.
- Để bảo mật và tránh rò rỉ thông tin tài khoản (username harvesting), hệ thống trả về mã thành công `200 OK` ngay cả khi email không tồn tại trong DB (không trả về lỗi 404).
- Token đặt lại mật khẩu có thời hạn sử dụng giới hạn (ví dụ: 2 phút).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Internal Server Error",
  "Status": 500,
  "Detail": "Không thể gửi email khôi phục mật khẩu.",
  "MessageCode": "EMAIL_SENDING_FAILED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | VALIDATION_FAILED | Trường email bị thiếu hoặc định dạng sai. |
| 500 | EMAIL_SENDING_FAILED | Gặp sự cố kết nối với mail server (SMTP). |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ không mong muốn. |
