# Cập nhật ảnh đại diện (Avatar Upload/Update)

## Tác dụng
Cho phép người dùng đã đăng nhập cập nhật trực tiếp đường dẫn ảnh đại diện mới.

## URL
`PATCH /api/v1/users/me/avatar`

## Quyền
Authenticated User (Yêu cầu đăng nhập)

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Body
```json
{
  "avatarUrl": "https://cdn.seal-hackathon.vn/uploads/avatars/user-123.jpg"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": {
    "avatarUrl": "https://cdn.seal-hackathon.vn/uploads/avatars/user-123.jpg"
  },
  "message": "AVATAR_UPDATED_SUCCESSFULLY"
}
```

## Business rules
- Trường `avatarUrl` là bắt buộc và phải đúng định dạng URL.
- Cập nhật đồng thời trường `AvatarUrl` và `ImgUrl` (nếu dự án dùng song song cả 2 trường trong bảng `Users`).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | VALIDATION_FAILED | Dữ liệu avatarUrl gửi lên không đúng định dạng. |
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc hết hạn. |
| 404 | USER_NOT_FOUND | Người dùng không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ khi lưu DB. |
