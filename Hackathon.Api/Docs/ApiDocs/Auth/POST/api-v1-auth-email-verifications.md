# Verify email

## Tác dụng
Xác thực email bằng token; nếu xác thực thành công lần đầu thì trả token đăng nhập.

## URL
`POST /api/v1/auth/email-verifications`

## Request body
```json
{
  "token": "string"
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
  "data": {
    "accessToken": "string|null",
    "refreshToken": "string|null"
  },
  "message": "EMAIL_VERIFICATION_SUCCESSFUL | USER_ALREADY_VERIFIED"
}
```

## Business rules
- Token xác thực email phải tồn tại và còn hạn.
- User gắn với token phải còn tồn tại trong hệ thống.
- Nếu user đã verify trước đó, API trả thông báo `USER_ALREADY_VERIFIED`.
- Nếu verify lần đầu thành công, hệ thống đánh dấu email đã xác thực và trả token đăng nhập.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | INVALID_OR_EXPIRED_EMAIL_VERIFICATION_TOKEN |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 404 | NOT_FOUND | EMAILVALID_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
