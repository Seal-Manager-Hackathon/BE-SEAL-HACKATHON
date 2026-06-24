# Phân công Giảng viên vào Event (Assign Lecturer)

## Tác dụng
Cho phép Admin/Staff phân công giảng viên làm Mentor hoặc Judge cho sự kiện thi đấu.

## URL
`POST /api/v1/staff/events/{eventId}/assign-lecturers`

## Authorization
Yêu cầu access token hợp lệ với role `Admin` hoặc `Staff`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `eventId` | `guid` | Có | ID của sự kiện. |

## Request body
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "eventRole": "Judge"
}
```

| Field | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `userId` | `guid` | Có | ID của giảng viên cần phân công. |
| `eventRole` | `string` | Có | Vai trò trong event: `Mentor` hoặc `Judge`. |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "assignEventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  },
  "message": "LECTURER_ASSIGNED_TO_EVENT_SUCCESSFULLY"
}
```

## Business rules
- Event phải tồn tại trong DB, không bị soft-disable.
- User `userId` được gán phải tồn tại và có global role là Giảng viên (`Role = Lecturer` trong `Users`).
- `eventRole` phải là một trong hai giá trị enum: `Mentor` (giá trị 0) hoặc `Judge` (giá trị 1).
- Ràng buộc nghiệp vụ: Một giảng viên KHÔNG được vừa làm Mentor vừa làm Judge trong cùng một giải đấu (nếu đã có bản ghi phân công của giảng viên này trong event đó, từ chối và báo lỗi `LECTURER_ALREADY_ASSIGNED_IN_EVENT`).
- Tạo bản ghi mới trong bảng `AssignEvents` liên kết `UserId`, `EventId` và `EventRoleId`.
- Endpoint này nằm ở `Staff` controller với policy `StaffOrAdminPolicy` nên cả Admin và Staff đều có thể gọi.

### Bảng vai trò EventRoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | INVALID_EVENT_ROLE |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 409 | CONFLICT | LECTURER_ALREADY_ASSIGNED_IN_EVENT |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.Staff` (`Staff.cs:91`).
- Route: `POST /api/v1/staff/events/{eventId}/assign-lecturers`.
- Sử dụng policy `StaffOrAdminPolicy`.
- Message: `LECTURER_ASSIGNED_TO_EVENT_SUCCESSFULLY`.
