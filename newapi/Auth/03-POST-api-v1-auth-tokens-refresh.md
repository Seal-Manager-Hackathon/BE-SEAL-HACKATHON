# API 3: Làm mới Access Token

## Tác dụng
Rotate refresh token hiện tại và cấp access token mới cho client.

## URL
`POST /api/v1/auth/tokens/refresh`

## Quyền
Public API (Nhận diện phiên đăng nhập qua cookie)

## Request Headers & Cookies
*Yêu cầu cookie được set từ trước:*
- Cookie name: `Refresh-Token` (HTTP-only)

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "accessToken": "new_access_token_ey...",
    "refreshToken": "new_refresh_token_rf...",
    "message": "TOKEN_REFRESH_SUCCESSFUL"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Request phải gửi refresh token hợp lệ trong cookie.
- Refresh token còn hạn mới được dùng để cấp token mới.
- Nếu access token hiện tại vẫn còn hiệu lực thì không rotate refresh token.
- Khi refresh thành công, refresh token cũ bị thay thế bằng refresh token mới và ghi nhận thời gian sử dụng vào database.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Unauthorized",
  "Status": 401,
  "Detail": "Refresh token đã hết hạn hoặc không hợp lệ. Vui lòng đăng nhập lại.",
  "MessageCode": "EXPIRED_REFRESH_TOKEN",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | ACCESS_TOKEN_STILL_VALID | Access token vẫn còn thời gian hoạt động, không cần refresh. |
| 401 | MISSING_ACCESS_TOKEN | Access token bị thiếu trong header. |
| 401 | EXPIRED_REFRESH_TOKEN | Refresh token đã hết hạn hoặc bị thu hồi. Yêu cầu đăng nhập lại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ không mong muốn. |
