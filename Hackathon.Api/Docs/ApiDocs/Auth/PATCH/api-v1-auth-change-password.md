# Change password

## Tác dụng
User đã đăng nhập đổi mật khẩu hiện tại sang mật khẩu mới.

## URL
`PATCH /api/v1/auth/change-password`

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
  "status": 200,
  "traceId": "string",
  "timestampUtc": "datetime",
  "data": null,
  "message": "PASSWORD_CHANGED_SUCCESSFULLY"
}
```

## Business rules
- Request phải có access token hợp lệ.
- `currentPassword` phải khớp với mật khẩu hiện tại.
- `newPassword` và `confirmPassword` phải giống nhau (validate phía client).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | CURRENT_PASSWORD_INVALID |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
