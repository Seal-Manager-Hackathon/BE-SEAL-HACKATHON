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
  "data": null,
  "message": "LECTURER_ASSIGNMENT_REMOVED_SUCCESSFULLY"
}
```

## Business rules
- Bản ghi `AssignEvents` phải tồn tại.
- Xóa mềm bản ghi bằng cách đặt `IsDisable = true`.
- Toàn bộ các phân công bảng đấu liên quan (`AssignTracks`) của giảng viên này trong event cũng được tự động disable theo để tránh phân quyền chấm điểm sai lệch.
- Việc vô hiệu hóa các bảng liên quan bắt buộc thực hiện trong một **Database Transaction**.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | ASSIGNMENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement endpoint trong controller tương ứng.
- Soft delete: đặt `IsDisable = true` trên `AssignEvents`.
- Cascade disable `AssignTracks` liên quan trong transaction.
