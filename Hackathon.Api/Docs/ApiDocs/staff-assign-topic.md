# Staff assign topic

## Tác dụng
Staff gán đội thi đã được duyệt vào một Bảng đấu (Track) và một Chủ đề (Topic) cụ thể.

## URL
`PATCH /api/staff/registerteams/{registerTeamId}/allocation`

## Authorization
Yêu cầu access token hợp lệ và role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `registerTeamId` | `guid` | Có | Id đơn đăng ký team cần được phân bổ track/topic. |

## Request body
```json
{
  "trackId": "guid",
  "topicId": "guid"
}
```

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "registerTeamId": "guid",
    "teamId": "guid",
    "eventId": "guid",
    "trackId": "guid",
    "topicId": "guid",
    "message": "TEAM_ALLOCATION_UPDATED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- Chỉ Staff/Admin được phân bổ đội thi vào track/topic.
- Đơn đăng ký team phải tồn tại, chưa bị soft-disable và đã được duyệt.
- Track phải thuộc cùng event với đơn đăng ký team.
- Topic phải thuộc track được chọn.
- Staff chỉ được phân bổ trong event mình được assign nếu áp dụng rule `AssignEvents`.
- Phân bổ topic/track dùng để ghi nhận kết quả bốc thăm offline.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | STAFF_OR_ADMIN_REQUIRED |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 404 | NOT_FOUND | TOPIC_NOT_FOUND |
| 400 | BAD_REQUEST | REGISTER_TEAM_NOT_APPROVED |
| 400 | BAD_REQUEST | TRACK_NOT_IN_EVENT |
| 400 | BAD_REQUEST | TOPIC_NOT_IN_TRACK |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
