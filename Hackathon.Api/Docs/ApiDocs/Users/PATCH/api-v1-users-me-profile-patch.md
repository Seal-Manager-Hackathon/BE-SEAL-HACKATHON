# Cập nhật thông tin cá nhân (Profile)

## Tác dụng
Cho phép người dùng đã đăng nhập cập nhật thông tin cá nhân của họ. Các trường gửi lên sẽ ghi đè dữ liệu cũ. Hỗ trợ upload ảnh đại diện (file) thay vì link URL.

## URL
`PATCH /api/v1/users/profile`

## Authorization
Yêu cầu access token hợp lệ.

## Path parameters
Không có.

## Query parameters
Không có.

## Ví dụ request
```http
PATCH /api/v1/users/profile
Authorization: Bearer {accessToken}
Content-Type: multipart/form-data

{
    "firstName": "John updated",
    "lastName": "Doe updated",
    "phoneNumber": "0987654321",
    "avatarUrl": (file upload),
    "bio": "Updated bio",
    "address": "456 Updated St",
    "dateOfBirth": "2002-10-18",
    "studentId": "STU123456",
    "college": "FPT University"
}
```

## Request body
Gửi dưới dạng `application/json`.

| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `firstName` | `string` | Không | Tên của người dùng. |
| `lastName` | `string` | Không | Họ của người dùng. |
| `phoneNumber` | `string` | Không | Số điện thoại. |
| `avatarUrl` | `string` | Không | URL ảnh đại diện. Upload file qua API `PATCH /api/v1/users/avatar`. |
| `bio` | `string` | Không | Giới thiệu ngắn về bản thân. |
| `address` | `string` | Không | Địa chỉ. |
| `dateOfBirth` | `date` | Không | Ngày tháng năm sinh (VD: 2002-10-18). |
| `studentId` | `string` | Không | Mã sinh viên. |
| `college` | `string` | Không | Trường đại học/Cao đẳng. |

*Lưu ý: Chỉ các trường được gửi (khác null) mới được cập nhật.*

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string",
  "timestampUtc": "2026-06-18T23:00:00Z",
  "data": null,
  "message": "PROFILE_UPDATED_SUCCESSFULLY"
}
```

## Business rules
- Các trường gửi lên nếu khác null sẽ được cập nhật.
- User đang gọi API phải tồn tại trong hệ thống.
- `avatarUrl` là string URL (link ảnh).
- Upload file ảnh dùng API riêng `PATCH /api/v1/users/avatar` (`multipart/form-data`).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | FILE_EMPTY / INVALID_IMAGE_FORMAT / FILE_TOO_LARGE |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong `Hackathon.Api.Controllers.UserController`.
- Method: `UpdateProfile(Request.UpdateProfileRequest requestBody)`.
- Route: `PATCH /api/v1/users/profile`.
- Sử dụng `[FromBody]` để nhận dữ liệu JSON.
