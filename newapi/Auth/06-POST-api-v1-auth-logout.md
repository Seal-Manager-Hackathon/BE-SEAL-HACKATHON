# API 6: Đăng xuất

## Tác dụng
Thu hồi refresh token hiện tại và xóa cookies xác thực của client.

## URL
`POST /api/v1/auth/logout`

## Quyền
Authenticated User (Đồng thời đọc refresh token trong cookie)

## Request Headers & Cookies
- \`Authorization: Bearer <AccessToken>\`
- Cookie: \`Refresh-Token=<token>\`

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "accessToken": null,
    "refreshToken": null,
    "message": "LOGOUT_SUCCESSFUL"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Request phải gửi kèm refresh token hiện tại trong cookie.
- Refresh token phải hợp lệ và chưa bị thu hồi trước đó.
- Đăng xuất thành công sẽ đánh dấu thu hồi refresh token hiện tại trong Database (ghi nhận `RevokedAt`) và xóa các cookies lưu trữ token.
- User đã logout trước đó không thể logout lại bằng cùng một refresh token cũ.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Unauthorized",
  "Status": 401,
  "Detail": "Phiên làm việc không tồn tại hoặc đã đăng xuất.",
  "MessageCode": "USER_ALREADY_LOGGED_OUT",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Token truy cập bị thiếu. |
| 401 | INVALID_REFRESH_TOKEN | Refresh token gửi lên không hợp lệ. |
| 401 | USER_ALREADY_LOGGED_OUT | Token đã bị thu hồi/đăng xuất trước đó. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi phát sinh trong quá trình dọn dẹp cookie hoặc DB. |
