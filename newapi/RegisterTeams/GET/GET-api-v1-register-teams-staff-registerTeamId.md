# API 41: BTC xem chi tiết đơn đăng ký (Staff Get Register Team Detail)

## Tác dụng
Staff xem chi tiết đơn đăng ký tham gia event của một team, bao gồm thông tin team, danh sách thành viên, track/topic đã gán và trạng thái đơn.

## URL
`GET /api/v1/staff/register-teams/{registerTeamId}`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `registerTeamId` (Guid, Bắt buộc): Id của đơn đăng ký cần xem chi tiết.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa thông tin chi tiết đơn và danh sách members.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Value": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "teamName": "Chiến binh công nghệ",
    "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "eventName": "SEAL Hackathon 2026",
    "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "trackTitle": "Bảng A - Web Application",
    "topicId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "topicTitle": "Hệ thống số hóa y tế",
    "description": "Lời nhắn từ team",
    "rejectionReason": null,
    "Status": 0, /* Pending */
    "isBanned": false,
    "isDisable": false,
    "members": [
      {
        "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "fullName": "Hoàng Phạm",
        "email": "student@college.edu.vn",
        "studentId": "STU123456",
        "isLeader": true
      }
    ],
    "createdAt": "2026-06-22T08:00:00Z",
    "updatedAt": "2026-06-22T08:00:00Z"
  }
}
```

## Business rules
- Staff phải đăng nhập bằng access token hợp lệ.
- Endpoint này dùng policy `StaffOrAdminPolicy`.
- Admin có thể xem tất cả đơn đăng ký mà không cần phân công.
- Staff phải được phân công vào event của đơn đăng ký đó (`AssignEvents`) thì mới được xem chi tiết, nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- `registerTeamId` là bắt buộc trên path.
- Đơn đăng ký phải tồn tại và chưa bị soft-disable, nếu không trả `REGISTER_TEAM_NOT_FOUND`.
- Kết quả bao gồm danh sách thành viên của team (`TeamDetails`) đang active, cùng với thông tin event, track/topic đã được gán (nếu có).

### Bảng trạng thái RegisterTeamStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ duyệt đăng ký |
| `1` | Approved | Đã duyệt tham gia sự kiện |
| `2` | Rejected | Bị từ chối tham gia sự kiện |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy đơn đăng ký thi đấu.",
  "MessageCode": "REGISTER_TEAM_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | User không có role `Staff`. |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
