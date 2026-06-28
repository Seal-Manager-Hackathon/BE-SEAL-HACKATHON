# Staff assign lecturer to track

## Tác dụng
Staff phân công một Giảng viên (Lecturer) phụ trách một Track cụ thể (với vai trò Judge hoặc Mentor). Dữ liệu được lưu vào bảng `AssignTracks`.

## URL
`POST /api/v1/staff/events/{eventId}/tracks/{trackId}/assign-lecturers`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event chứa track. |
| `trackId` | `guid` | Có | Id của track cần phân công. |

## Request body
```json
{
  "assignEventId": "guid"
}
```
*Ghi chú*: `assignEventId` là ID trả về từ bảng `AssignEvents` khi phân công Lecturer vào sự kiện chứa track này.

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
    "id": "guid",
    "assignEventId": "guid",
    "trackId": "guid"
  },
  "message": "LECTURER_ASSIGNED_TO_TRACK_SUCCESSFULLY"
}
```

## Business rules
- Người gọi phải là `Staff` hoặc `Admin`.
- Nếu là `Staff`, phải được phân công quản lý sự kiện chứa track này.
- `eventId` và `trackId` phải tồn tại và không bị disable.
- `trackId` phải thuộc về event có `eventId` tương ứng.
- `assignEventId` phải tồn tại trong bảng `AssignEvents` và thuộc về cùng sự kiện (`EventId` của track == `EventId` của AssignEvents).
- Vai trò của `AssignEvents` đó phải là `Judge` hoặc `Mentor`. Nếu sai trả lỗi `ONLY_JUDGE_OR_MENTOR_CAN_BE_ASSIGNED_TO_TRACK`.
- Nếu lecturer đã được phân công vào track này rồi thì trả lỗi `LECTURER_ALREADY_ASSIGNED_TO_TRACK`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | FORBIDDEN / STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 404 | NOT_FOUND | ASSIGN_EVENT_NOT_FOUND |
| 409 | CONFLICT | ONLY_JUDGE_OR_MENTOR_CAN_BE_ASSIGNED_TO_TRACK |
| 409 | CONFLICT | LECTURER_ALREADY_ASSIGNED_TO_TRACK |
| 409 | CONFLICT | ASSIGN_EVENT_NOT_MATCH_TRACK_EVENT |
| 409 | CONFLICT | TRACK_NOT_IN_EVENT |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement trong `Hackathon.Api.Controllers.Staff`.
- Route: `POST /api/v1/staff/events/{eventId:guid}/tracks/{trackId:guid}/assign-lecturers`.
- Sử dụng policy `StaffOrAdminPolicy` (class-level).
