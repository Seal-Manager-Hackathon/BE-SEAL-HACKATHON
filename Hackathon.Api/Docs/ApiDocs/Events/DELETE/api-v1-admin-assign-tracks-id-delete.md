# Gỡ phân công khỏi Track (Remove Track Assignment)

## Tác dụng
Cho phép BTC gỡ phân công gán bảng đấu (Track) của Mentor hoặc Judge.

## URL
`DELETE /api/v1/admin/assign-tracks/{id}`

## Authorization
Yêu cầu access token hợp lệ với role `Admin` hoặc `Staff`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `id` | `guid` | Có | ID của bản ghi gán track. |

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
  "message": "TRACK_ASSIGNMENT_REMOVED"
}
```

## Business rules
- Bản ghi `AssignTracks` phải tồn tại.
- BTC kiểm tra quyền của Staff.
- Đặt cờ `IsDisable = true` cho bản ghi phân công track.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | ASSIGN_TRACK_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.EventsController` (`EventsController.cs:70`).
- Route: `DELETE /api/v1/admin/assign-tracks/{id:guid}`.
- Sử dụng policy `StaffOrAdminPolicy`.
- Nếu caller là Staff, kiểm tra quyền qua `EnsureStaffAssignedToEvent`.
- Soft delete: đặt `IsDisable = true` trên bản ghi `AssignTracks`.
- Message: `TRACK_ASSIGNMENT_REMOVED`.
