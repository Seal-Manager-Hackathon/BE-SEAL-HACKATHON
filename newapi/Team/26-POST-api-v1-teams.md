# API 26: Tạo nhóm mới (Create Team)

## Tác dụng
Tạo một team mới cho học sinh (Student) đang đăng nhập và tự động chỉ định học sinh đó làm Leader của team.

## URL
`POST /api/v1/teams`

## Quyền
Student (Yêu cầu đăng nhập tài khoản vai trò Student)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Body
```json
{
  "teamName": "Chiến binh công nghệ"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa thông tin team và mảng members.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Value": {
    "id": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "name": "Chiến binh công nghệ",
    "canEdit": true,
    "createdAt": "2026-06-22T08:00:00Z",
    "message": "TEAM_CREATED_SUCCESSFULLY",
    "members": [
      {
        "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "isLeader": true,
        "Status": 0 /* Active */
      }
    ]
  }
}
```

## Business rules
- Hồ sơ (Profile) của người tạo team phải đầy đủ thông tin bắt buộc (email, phone, MSSV, trường học...) theo đúng quy tắc hoàn thiện profile (check BR-ACC-03, nếu không báo lỗi `USER_PROFILE_NOT_COMPLETED`).
- Người tạo phải có role là `Student`, đã xác thực email (`IsVerified = true`), chưa bị disable.
- Tên team không được để trống và không được trùng với team đã tồn tại trong DB (không phân biệt hoa thường, báo lỗi `TEAM_NAME_ALREADY_EXISTS`).
- Việc lưu thông tin team vào bảng `Teams` và thêm thành viên vào `TeamDetails` phải được bọc trong cùng một **Database Transaction**.

### Bảng trạng thái TeamDetailStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Active | Thành viên đang hoạt động |
| `1` | Inactive | Thành viên đã rời nhóm hoặc ngưng hoạt động |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Tên đội thi này đã được sử dụng.",
  "MessageCode": "TEAM_NAME_ALREADY_EXISTS",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | TEAM_NAME_REQUIRED | Tên team không được bỏ trống. |
| 400 | USER_PROFILE_NOT_COMPLETED | Hồ sơ sinh viên chưa được điền đầy đủ các trường bắt buộc. |
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | USER_NOT_VERIFIED | Tài khoản chưa verify email. |
| 409 | TEAM_NAME_ALREADY_EXISTS | Trùng tên team đã có. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
