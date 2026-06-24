# Đếm lời mời chờ phản hồi

## Tác dụng
Đếm số lượng lời mời vào team của người dùng hiện tại đang ở trạng thái chờ (`Pending`), chưa được chấp nhận hoặc từ chối.

## URL
`GET /api/v1/invitations/pending/count`

## Request Headers
```
Authorization: Bearer <token>
```

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "message": "SUCCESS",
  "data": {
    "count": 3
  }
}
```

## Business rules
- Yêu cầu xác thực tài khoản qua Access Token ở Header.
- Đếm các lời mời của người dùng đang đăng nhập (`UserId` lấy từ Access Token) có:
  - Chưa bị disable (`!x.IsDisable`).
  - Trạng thái là `Pending`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | INVALID_ACCESS_TOKEN | INVALID_ACCESS_TOKEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
