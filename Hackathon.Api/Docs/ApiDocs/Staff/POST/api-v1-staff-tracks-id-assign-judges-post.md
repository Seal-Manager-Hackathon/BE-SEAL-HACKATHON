# Staff assign judge to track

## Tác dụng
Staff phân công một Giám khảo (`Judge`) phụ trách chấm thi cho một `Track` cụ thể. Dữ liệu được lưu vào bảng `AssignTracks`.

## URL
`POST /api/v1/staff/tracks/{trackId}/assign-judges`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `trackId` | `guid` | Có | Id của track cần phân công. |

## Request body
```json
{
  "assignEventId": "guid"
}
```
*Ghi chú*: `assignEventId` là ID trả về từ bảng `AssignEvents` khi phân công Lecturer làm Judge cho sự kiện chứa track này.

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
  "message": "JUDGE_ASSIGNED_TO_TRACK_SUCCESSFULLY"
}
```

## Business rules
- Người gọi phải là `Staff` hoặc `Admin`.
- Nếu là `Staff`, phải được phân công quản lý sự kiện chứa track này.
- `trackId` phải tồn tại và không bị disable.
- `assignEventId` phải tồn tại trong bảng `AssignEvents` và thuộc về cùng sự kiện (`EventId` của track == `EventId` của AssignEvents).
- Vai trò của `AssignEvents` đó **bắt buộc phải là Judge**. Không được phân công `Mentor` vào track (theo luật: Mentor hỗ trợ chung, Judge chấm theo track). Nếu sai trả lỗi `ONLY_JUDGE_CAN_BE_ASSIGNED_TO_TRACK`.
- Nếu Judge đã được phân công vào track này rồi thì trả lỗi `JUDGE_ALREADY_ASSIGNED_TO_TRACK`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | FORBIDDEN / STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 404 | NOT_FOUND | ASSIGN_EVENT_NOT_FOUND |
| 409 | CONFLICT | ONLY_JUDGE_CAN_BE_ASSIGNED_TO_TRACK |
| 409 | CONFLICT | JUDGE_ALREADY_ASSIGNED_TO_TRACK |
| 409 | CONFLICT | ASSIGN_EVENT_NOT_MATCH_TRACK_EVENT |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |