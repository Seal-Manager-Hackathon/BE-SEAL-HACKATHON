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
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 201,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Message": "TEAM_CREATED_SUCCESSFULLY",
  "Data": {
    "id": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
    "name": "Chiến binh công nghệ",
    "canEdit": true,
    "createdAt": "2026-06-22T08:00:00Z",
    "members": [
      {
        "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "isLeader": true,
        "status": 1 /* 0: Pending, 1: Active, 2: Rejected */
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

### Bảng trạng thái thành viên TeamDetailStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Đang đợi trưởng nhóm duyệt vào |
| `1` | Active | Thành viên chính thức hoạt động |
| `2` | Rejected | Yêu cầu tham gia bị từ chối |

## Lỗi có thể xảy ra
*Khi gặp lỗi Validation hoặc nghiệp vụ, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "Title": "Bad Request",
  "Status": 400,
  "Message": "TEAM_NAME_REQUIRED",
  "MessageCode": "BAD_REQUEST",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | TEAM_NAME_REQUIRED |
| 400 | BAD_REQUEST | USER_PROFILE_NOT_COMPLETED |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | USER_NOT_VERIFIED |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 409 | CONFLICT | TEAM_NAME_ALREADY_EXISTS |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
