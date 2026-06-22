# Phân công Giảng viên vào Event (Assign Lecturer)

## Tác dụng
Cho phép Admin phân công giảng viên làm Mentor hoặc Judge cho sự kiện thi đấu.

## URL
`POST /api/v1/admin/events/{eventId}/lecturers`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của sự kiện.

## Request Body
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "eventRole": "Judge" /* Mentor hoặc Judge */
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa ID bản ghi gán.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "assignEventId": "b1a7d6c2-4821-4f9b-bd5e-3c2fa56789e0",
    "message": "LECTURER_ASSIGNED_TO_EVENT"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Event phải tồn tại trong DB, không bị soft-disable.
- User `userId` được gán phải tồn tại và có global role là Giảng viên (`Role = Lecturer` trong `Users`).
- `eventRole` phải là một trong hai giá trị enum: `Mentor` (giá trị 0) hoặc `Judge` (giá trị 1) (check BR-ASG-02).
- Ràng buộc nghiệp vụ: Một giảng viên KHÔNG được vừa làm Mentor vừa làm Judge trong cùng một giải đấu (check BR-ASG-04, nếu đã có bản ghi phân công của giảng viên này trong event đó, từ chối và báo lỗi `LECTURER_ALREADY_ASSIGNED_IN_EVENT`).
- Tạo bản ghi mới trong bảng `AssignEvents` liên kết `UserId`, `EventId` và `EventRoleId`.

### Bảng vai trò EventRoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Giảng viên này đã được phân công vai trò khác trong sự kiện.",
  "MessageCode": "LECTURER_ALREADY_ASSIGNED_IN_EVENT",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | INVALID_EVENT_ROLE | Vai trò sự kiện gửi lên sai enum. |
| 404 | USER_NOT_FOUND | Không tìm thấy giảng viên hoặc tài khoản bị disable. |
| 404 | EVENT_NOT_FOUND | Sự kiện không tồn tại. |
| 409 | LECTURER_ALREADY_ASSIGNED_IN_EVENT | Giảng viên đã được gán vai trò Mentor hoặc Judge trước đó của event này. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
