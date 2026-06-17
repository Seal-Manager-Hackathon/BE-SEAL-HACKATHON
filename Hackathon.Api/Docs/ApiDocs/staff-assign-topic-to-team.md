# Staff assign topic to team

## Tác dụng
Staff gán một topic cho team, sau khi team đã được gán track tương ứng.

## URL
`PATCH /api/v1/staff/teams/{teamId}/topic`

## Authorization
Yêu cầu access token hợp lệ với role `Staff`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `teamId` | `guid` | Có | Id của team cần được gán topic. |

## Request body
```json
{
  "topicId": "guid"
}
```

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "value": {
    "teamId": "guid",
    "teamName": "string",
    "eventId": "guid",
    "trackId": "guid",
    "trackTitle": "string",
    "topicId": "guid",
    "topicTitle": "string",
    "message": "TOPIC_ASSIGNED_TO_TEAM_SUCCESSFULLY"
  }
}
```

## Business rules
- Staff phải đăng nhập bằng access token hợp lệ.
- Team phải tồn tại và chưa bị soft-disable.
- Topic phải tồn tại và chưa bị soft-disable.
- Topic phải thuộc một track đang tồn tại và chưa bị soft-disable.
- Team phải có đơn đăng ký event tương ứng với `topic.track.eventId`.
- Đơn đăng ký event của team không được bị soft-disable, không bị banned và nên ở trạng thái `Approved`.
- Team phải đã được gán track trước khi gán topic.
- Topic được gán phải thuộc đúng track đã gán cho team.
- Nếu team đã được gán topic trước đó, API cập nhật sang topic mới.
- Không dùng request body để truyền `teamId`; `teamId` lấy từ path.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | User không có role `Staff`. |
| 400 | BAD_REQUEST | TOPIC_ID_REQUIRED |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 404 | NOT_FOUND | TOPIC_NOT_FOUND |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 403 | FORBIDDEN | REGISTER_TEAM_NOT_APPROVED |
| 409 | CONFLICT | TEAM_IS_BANNED_FROM_EVENT |
| 409 | CONFLICT | TEAM_TRACK_NOT_ASSIGNED |
| 409 | CONFLICT | TOPIC_NOT_BELONG_TO_TEAM_TRACK |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |

## Trạng thái implement
- Đã implement trong `StaffTracksController` và `TracksService.AssignTopicToTeam`.
- Assignment được lưu bằng `RegisterTeams.TopicId`.
- API yêu cầu team đã có `RegisterTeams.TrackId` và topic phải thuộc đúng track đó.
