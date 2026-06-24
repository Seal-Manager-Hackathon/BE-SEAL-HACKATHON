# Gỡ phân công Giảng viên khỏi Event (Remove Lecturer From Event)

## Tác dụng
Cho phép Admin thu hồi toàn bộ phân công của giảng viên khỏi một sự kiện cụ thể.

## URL
`DELETE /api/v1/staff/assign-events/{id}`

## Authorization
Yêu cầu access token hợp lệ với role `Admin` hoặc `Staff` (`StaffOrAdminPolicy`).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `id` | `guid` | Có | ID của bản ghi phân công. |

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
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  },
  "message": "LECTURER_ASSIGNMENT_REMOVED_SUCCESSFULLY"
}
```

## Business rules
- Bản ghi `AssignEvents` phải tồn tại, nếu không trả `ASSIGN_EVENT_NOT_FOUND`.
- Nếu caller là Staff, kiểm tra quyền qua `EnsureStaffAssignedToEvent`.
- Xóa mềm bản ghi bằng cách đặt `IsDisable = true`.
- Toàn bộ các phân công bảng đấu liên quan (`AssignTracks`) của giảng viên này trong event cũng được tự động disable theo để tránh phân quyền chấm điểm sai lệch.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | ASSIGN_EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.Staff` (`Staff.cs:105`).
- Route: `DELETE /api/v1/staff/assign-events/{id:guid}`.
- Sử dụng policy `StaffOrAdminPolicy`.
- Soft delete: đặt `IsDisable = true` trên `AssignEvents`.
- Cascade disable `AssignTracks` liên quan.
