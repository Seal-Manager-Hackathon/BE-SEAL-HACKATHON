# Logout

## Tác dụng
Thu hồi refresh token hiện tại và xóa auth cookies.

## URL
`POST /api/auth/logout`

## Request body
Không có. Cần cookie:
```json
{
  "Refresh-Token": "string"
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
    "accessToken": "string|null",
    "refreshToken": "string|null",
    "message": "LOGOUT_SUCCESSFUL"
  }
}
```

## Business rules
- Request phải gửi refresh token hiện tại trong cookie.
- Refresh token phải hợp lệ và chưa bị thu hồi.
- Logout thành công sẽ revoke refresh token hiện tại và xóa auth cookies.
- User đã logout trước đó không thể logout lại bằng cùng refresh token.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_REFRESH_TOKEN |
| 401 | UNAUTHORIZED | USER_ALREADY_LOGGED_OUT |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
