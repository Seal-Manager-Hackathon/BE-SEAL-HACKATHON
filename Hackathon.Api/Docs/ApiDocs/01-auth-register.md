# Register

## Tác dụng
Đăng ký user mới và gửi email xác thực tài khoản.

## URL
`POST /api/v1/auth/register`

## Request body
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "password": "string",
  "confirmPassword": "string"
}
```

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": "REGISTRATION_SUCCESSFUL"
}
```
*(Nếu email đã tồn tại nhưng chưa verify, response sẽ trả về "UNVERIFIED_ACCOUNT_VERIFICATION_RESENT")*

## Business rules
- Email phải chưa tồn tại ở trạng thái đã xác thực.
- Nếu email đã tồn tại nhưng **chưa xác thực** (`IsVerified = false`), hệ thống sẽ gửi lại email xác thực mới thay vì báo lỗi.
- Nếu email đã tồn tại và **đã xác thực** (`IsVerified = true`), hệ thống báo lỗi `EMAIL_ALREADY_EXISTS`.
- `password` và `confirmPassword` phải trùng nhau (validate phía client).
- Sau khi đăng ký thành công, tài khoản cần verify email trước khi dùng các luồng yêu cầu xác thực.
- Hệ thống gửi email xác thực cho user mới.
- User mới được gán role `Student` mặc định.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 409 | CONFLICT | EMAIL_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
