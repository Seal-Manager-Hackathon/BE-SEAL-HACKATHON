# Create Team

## Tác dụng
Tạo một team mới cho học sinh (Student) đang đăng nhập và tự động chỉ định học sinh đó làm Leader của team.

## URL
`POST /api/v1/teams`

## Authorization
Yêu cầu access token hợp lệ với role `Student`.

## Request body
```json
{
  "teamName": "string"
}
```

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "id": "guid",
    "name": "string",
    "canEdit": true,
    "createdAt": "datetime",
    "message": "TEAM_CREATED_SUCCESSFULLY",
    "members": [
      {
        "userId": "guid",
        "isLeader": true,
        "status": 0 /* Active */
      }
    ]
  }
}
```

## Business rules
- Yêu cầu xác thực tài khoản qua Access Token ở Header.
- Hồ sơ (Profile) của người tạo team phải đầy đủ thông tin bắt buộc (không được null/trống): Email, Password (hash), FirstName, LastName, PhoneNumber, Address, DateOfBirth, StudentId, College.
- Tài khoản người tạo phải thỏa mãn:
  - Role là `Student`.
  - Đã xác thực email (`IsVerified = true`).
  - Chưa bị vô hiệu hóa (`IsDisable = false`).
- Tên team (`teamName`) không được để trống (bắt buộc nhập) và không được trùng lặp với tên team đã tồn tại trong hệ thống (không phân biệt chữ hoa/chữ thường).
- Khi tạo thành công:
  - Bản ghi team mới được lưu vào bảng `Teams`.
  - Thành viên tạo được lưu vào bảng `TeamDetails` với trạng thái `Active` và vai trò Leader (`IsLeader = true`).
  - Quá trình ghi nhận hai bảng này được thực hiện trong cùng một Database Transaction.

## Lỗi có thể xảy ra
*Khi gặp lỗi Validation hoặc nghiệp vụ, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

```json
{
  "title": "string",
  "status": "integer",
  "detail": "string",
  "messageCode": "string",
  "errors": "object|null",
  "traceId": "string|null",
  "timestampUtc": "datetime"
}
```

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | TEAM_NAME_REQUIRED (khi `teamName` trống hoặc null) |
| 400 | BAD_REQUEST | USER_PROFILE_NOT_COMPLETED (hồ sơ thiếu thông tin bắt buộc) |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | INVALID_ACCESS_TOKEN | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | USER_NOT_VERIFIED (tài khoản chưa xác thực email) |
| 404 | NOT_FOUND | USER_NOT_FOUND (tài khoản không tồn tại hoặc bị khóa) |
| 409 | CONFLICT | TEAM_NAME_ALREADY_EXISTS (tên team đã tồn tại) |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
