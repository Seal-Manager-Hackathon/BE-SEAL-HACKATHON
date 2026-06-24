# Get current user

## Tác dụng
Lấy thông tin profile ngắn gọn của user đang đăng nhập.

## URL
`GET /api/v1/auth/me`

## Authorization
Yêu cầu access token hợp lệ.

## Request body
Không có.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Message": "SUCCESS",
  "Data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "role": 2, /* 0: Admin, 1: Staff, 2: Student, 3: Lecturer */
    "firstName": "John",
    "lastName": "Doe",
    "email": "user@example.com",
    "avatar": "https://example.com/avatar.jpg"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ trả thông tin của user đang đăng nhập, không truyền userId từ client.
- User phải còn tồn tại và chưa bị disable.

### Bảng vai trò RoleEnum (Integer)
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Admin | Quản trị viên hệ thống |
| `1` | Staff | Nhân viên vận hành sự kiện |
| `2` | Student | Sinh viên / Thí sinh tham gia thi đấu |
| `3` | Lecturer | Giảng viên hỗ trợ chuyên môn hoặc chấm thi |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
