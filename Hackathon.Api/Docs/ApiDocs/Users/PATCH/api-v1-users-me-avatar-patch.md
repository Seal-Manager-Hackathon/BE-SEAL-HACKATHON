# Cập nhật ảnh đại diện (Avatar Upload)

## Tác dụng
Cho phép người dùng đã đăng nhập upload ảnh đại diện mới. Ảnh được upload lên Cloudinary và lưu URL vào user.

## URL
`PATCH /api/v1/users/me/avatar`

## Authorization
Yêu cầu access token hợp lệ.

## Path parameters
Không có.

## Query parameters
Không có.

## Ví dụ request
```http
PATCH /api/v1/users/me/avatar
Authorization: Bearer {accessToken}
Content-Type: multipart/form-data

--boundary
Content-Disposition: form-data; name="avatarUrl"; filename="avatar.jpg"
Content-Type: image/jpeg

(binary data)
--boundary--
```

## Request body
Gửi dưới dạng `multipart/form-data`.

| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `avatarUrl` | `file` | Có | File ảnh đại diện (jpg, jpeg, png, gif, webp, tối đa 5MB). |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": null,
  "message": "AVATAR_UPDATED_SUCCESSFULLY"
}
```

## Business rules
- `avatarUrl` là file upload, không phải URL string.
- File ảnh phải có định dạng: `.jpg`, `.jpeg`, `.png`, `.gif`, `.webp`.
- File ảnh tối đa 5MB.
- Ảnh được upload lên Cloudinary, URL được lưu vào `AvatarUrl` của user.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | AVATAR_FILE_REQUIRED / FILE_EMPTY / INVALID_IMAGE_FORMAT / FILE_TOO_LARGE |
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc hết hạn. |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ khi lưu DB. |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.UserController`.
- Method: `UpdateAvatar(Request.UpdateAvatarRequest requestBody)`.
- Route: `PATCH /api/v1/users/me/avatar`.
- Sử dụng `[FromForm]` để nhận dữ liệu multipart/form-data.
