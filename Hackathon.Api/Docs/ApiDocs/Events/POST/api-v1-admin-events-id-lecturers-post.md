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
  "lecturerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "eventRoleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

| Field | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `lecturerId` | `guid` | Có | ID của giảng viên cần phân công. |
| `eventRoleId` | `guid` | Có | ID của vai trò trong event (tham chiếu bảng `EventRoles`). |

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
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "eventRoleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "eventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  },
  "message": "LECTURER_ASSIGNED_TO_EVENT_SUCCESSFULLY"
}
```

## Business rules
- Event phải tồn tại trong DB, không bị soft-disable.
- User `lecturerId` được gán phải tồn tại và có global role là Giảng viên (`Role = Lecturer` trong `Users`), nếu không trả `LECTURER_NOT_FOUND`.
- `eventRoleId` phải tồn tại trong bảng `EventRoles`, nếu không trả `EVENT_ROLE_NOT_FOUND`.
- Một giảng viên không được phân công trùng role (cùng `LecturerId` + `EventId` + `EventRoleId`), nếu không trả `LECTURER_ALREADY_ASSIGNED_THIS_ROLE`.
- Một giảng viên không được vừa làm Mentor vừa làm Judge trong cùng một giải đấu, nếu không trả `LECTURER_CANNOT_BE_BOTH_MENTOR_AND_JUDGE`.
- Tạo bản ghi mới trong bảng `AssignEvents` liên kết `UserId`, `EventId` và `EventRoleId`.
- Endpoint này nằm ở `Staff` controller với policy `StaffOrAdminPolicy` nên cả Admin và Staff đều có thể gọi.
- Nếu caller là Staff, kiểm tra quyền qua `EnsureStaffAssignedToEvent`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | LECTURER_NOT_FOUND |
| 404 | NOT_FOUND | EVENT_ROLE_NOT_FOUND |
| 409 | CONFLICT | LECTURER_ALREADY_ASSIGNED_THIS_ROLE |
| 409 | CONFLICT | LECTURER_CANNOT_BE_BOTH_MENTOR_AND_JUDGE |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.Staff` (`Staff.cs:91`).
- Route: `POST /api/v1/staff/events/{eventId}/assign-lecturers`.
- Sử dụng policy `StaffOrAdminPolicy`.
- Message: `LECTURER_ASSIGNED_TO_EVENT_SUCCESSFULLY`.
