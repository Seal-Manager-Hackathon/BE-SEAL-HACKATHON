# API 25: Xem chi tiết team (Student View Team)

## Tác dụng
Lấy thông tin chi tiết cấu hình chung và danh sách thành viên của một team.

## URL
`GET /api/v1/teams/{teamId}`

## Quyền
Student, Staff hoặc Admin (Yêu cầu đăng nhập)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần xem.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa thông tin chi tiết team và mảng members.*
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
    "isLeader": true,
    "createdAt": "2026-06-21T10:00:00Z",
    "members": [
      {
        "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "firstName": "Hoàng",
        "lastName": "Phạm",
        "dateOfBirth": "2004-06-20T00:00:00Z",
        "studentId": "STU123456",
        "college": "Đại Học Bách Khoa",
        "isLeader": true,
        "Status": 0 /* Active */
      }
    ]
  }
}
```

## Business rules
- Team phải tồn tại trong DB và chưa bị disable (`IsDisable = false`), nếu không có báo lỗi `TEAM_NOT_FOUND`.
- Trả ra đầy đủ thông tin cá nhân của các thành viên phục vụ hiển thị ở FE quản lý nhóm.

### Bảng trạng thái TeamDetailStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Active | Thành viên đang hoạt động |
| `1` | Inactive | Thành viên đã rời nhóm hoặc ngưng hoạt động |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy team chỉ định.",
  "MessageCode": "TEAM_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | TEAM_NOT_VISIBLE_TO_USER | Không có quyền xem thông tin chi tiết của team này. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại hoặc đã bị disable. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
