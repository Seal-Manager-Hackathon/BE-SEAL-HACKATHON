# Cập nhật thông tin cá nhân (Profile)

## Tác dụng
Cho phép người dùng đã đăng nhập cập nhật thông tin cá nhân của họ. Các trường gửi lên sẽ ghi đè dữ liệu cũ.

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
PATCH /api/users/profile
Authorization: Bearer {accessToken}
Content-Type: application/json

{
    "firstName": "John updated",
    "lastName": "Doe updated",
    "phoneNumber": "0987654321",
    "avatarUrl": "https://example.com/new-avatar.jpg",
    "bio": "Updated bio",
    "address": "456 Updated St",
    "dateOfBirth": "2002-10-18T00:00:00+07:00",
    "studentId": "STU123456",
    "college": "FPT University"
}
```

## Request body
| Tên           | Kiểu dữ liệu | Bắt buộc | Mô tả                           |
|---------------|---|---:|---------------------------------|
| `firstName`   | `string` | Không | Tên của người dùng.             |
| `lastName`    | `string` | Không | Họ của người dùng.              |
| `phoneNumber` | `string` | Không | Số điện thoại.                  |
| `avatarUrl`   | `string` | Không | Link ảnh đại diện.              |
| `bio`         | `string` | Không | Giới thiệu ngắn về bản thân.    |
| `address`     | `string` | Không | Địa chỉ.                        |
| `dateOfBirth` | `DateTimeOffset` | Không | Ngày tháng năm sinh người dùng. |
| `studentId`   | `string` | Không | Mã sinh viên.                   |
| `college`     | `string` | Không | Trường đại học/Cao đẳng.        |

*Lưu ý: Chỉ các trường được gửi (khác null) mới được cập nhật.*

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "2026-06-18T23:00:00Z",
  "value": "PROFILE_UPDATED_SUCCESSFULLY"
}
```

## Business rules
- Các trường gửi lên nếu khác null sẽ được cập nhật.
- User đang gọi API phải tồn tại trong hệ thống.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
