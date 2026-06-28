# Staff remove lecturer from track

## Tác dụng
Staff gỡ bỏ phân công của một Giảng viên (`Lecturer`) khỏi một Track cụ thể (thực hiện soft-disable record trong bảng `AssignTracks`).
Lecturer vẫn giữ nguyên vai trò trong event, chỉ mất quyền truy cập vào track này.

## URL
`DELETE /api/v1/staff/assign-tracks/{id}`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `id` | `guid` | Có | Id của record phân công track (`AssignTracks.Id`). |

## Request body
Không có.

## Ví dụ request
```http
DELETE /api/v1/staff/assign-tracks/00000000-0000-0000-0000-000000000000
Authorization: Bearer {accessToken}
```

## Response body
Response dùng `ApiResponseFactory.Base(...)`.
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "id": "guid"
  },
  "message": "LECTURER_REMOVED_FROM_TRACK_SUCCESSFULLY"
}
```

## Business rules
- Người gọi phải là `Staff` hoặc `Admin`.
- `id` (AssignTrackId) phải tồn tại và chưa bị disable.
- Nếu là `Staff`, phải được phân công quản lý event chứa track tương ứng với `AssignTrack` đó.
- Set `IsDisable = true` cho record trong bảng `AssignTracks`.
- **Không** ảnh hưởng đến các `AssignTracks` khác hay `AssignEvents` của lecturer đó.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | ASSIGN_TRACK_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement trong `Hackathon.Api.Controllers.Staff`.
- Route: `DELETE /api/v1/staff/assign-tracks/{id:guid}`.
- Sử dụng policy `StaffOrAdminPolicy` (class-level).
