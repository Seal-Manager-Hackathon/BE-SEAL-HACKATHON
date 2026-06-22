# Xem thành viên trong nhóm (Get Team Members)

## Tác dụng
Lấy danh sách các thành viên hiện tại trong team kèm vai trò trưởng nhóm/thành viên và trạng thái hoạt động.

## URL
`GET /api/v1/teams/{teamId}/members`

## Quyền
Authenticated User (Cho phép thành viên trong team, Staff hoặc Admin xem)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa danh sách thành viên.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Value": [
    {
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "firstName": "Hoàng",
      "lastName": "Phạm",
      "studentId": "STU123456",
      "college": "Đại Học Bách Khoa",
      "isLeader": true,
      "Status": 0 /* Active */
    }
  ]
}
```

## Business rules
- Team phải tồn tại trong DB và chưa bị disable.
- Chỉ hiển thị các thành viên có trong bảng `TeamDetails` của team.

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
  "Detail": "Không tìm thấy team để xem danh sách thành viên.",
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
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
