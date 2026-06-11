# Change password

## Tác dụng
User đã đăng nhập đổi mật khẩu hiện tại sang mật khẩu mới.

## URL
`PATCH /api/auth/change-password`

## Authorization
Yêu cầu access token hợp lệ.

## Request body
```json
{
  "currentPassword": "string",
  "newPassword": "string",
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
  "value": {
    "message": "PASSWORD_CHANGED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- `currentPassword` phải khớp với mật khẩu hiện tại.
- `newPassword` và `confirmPassword` phải giống nhau.
- Mật khẩu mới phải thỏa rule bảo mật của hệ thống.
- Sau khi đổi mật khẩu, có thể thu hồi refresh token cũ nếu nghiệp vụ yêu cầu.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | PASSWORD_CONFIRMATION_NOT_MATCH | New password and confirm password do not match. |
| 400 | INVALID_PASSWORD_FORMAT | Password format is invalid. |
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | CURRENT_PASSWORD_INVALID | Current password is invalid. |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
