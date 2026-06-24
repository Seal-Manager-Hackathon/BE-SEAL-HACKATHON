# Staff assign topic to team

## Tác dụng
Staff gán một topic cho team, sau khi team đã được gán track tương ứng.

## URL
`PATCH /api/v1/staff/events/{eventId}/teams/{teamId}/topic`

## Authorization
Yêu cầu access token hợp lệ với role `Staff`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event diễn ra. |
| `teamId` | `guid` | Có | Id của team cần được gán topic. |

## Request body
```json
{
  "topicId": "guid"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Message": "TOPIC_ASSIGNED_TO_TEAM_SUCCESSFULLY",
  "Data": {
    "teamId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
    "teamName": "Chiến binh công nghệ",
    "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "trackTitle": "Bảng A - Web Application",
    "topicId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "topicTitle": "Hệ thống quản lý thông minh"
  }
}
```

## Business rules
- Staff phải đăng nhập bằng access token hợp lệ.
- Staff phải được phân công vào event chứa track/topic (`AssignEvents`) thì mới được gán topic cho team trong event đó.
- Team phải tồn tại và chưa bị soft-disable.
- Topic phải tồn tại và chưa bị soft-disable.
- Topic phải thuộc một track đang tồn tại và chưa bị soft-disable.
- Team phải có đơn đăng ký event tương ứng với `topic.track.eventId`.
- Đơn đăng ký event của team không được bị soft-disable, không bị banned và nên ở trạng thái `Approved`.
- Team phải đã được gán track trước khi gán topic.
- Chỉ khi team đã được phân công vào track có chứa topic đó thì staff mới được phân công topic đó cho team.
- Topic được gán phải thuộc đúng track đã gán cho team.
- Nếu team đã được gán topic trước đó, API cập nhật sang topic mới.
- Không dùng request body để truyền `teamId`; `teamId` lấy từ path.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | EVENT_ID_REQUIRED |
| 400 | BAD_REQUEST | TOPIC_ID_REQUIRED |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 403 | FORBIDDEN | REGISTER_TEAM_NOT_APPROVED |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 404 | NOT_FOUND | TOPIC_NOT_FOUND |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 409 | CONFLICT | TEAM_IS_BANNED_FROM_EVENT |
| 409 | CONFLICT | TEAM_TRACK_NOT_ASSIGNED |
| 409 | CONFLICT | TOPIC_NOT_BELONG_TO_TEAM_TRACK |
| 409 | CONFLICT | TOPIC_NOT_IN_EVENT |
| 409 | CONFLICT | TOPIC_ALREADY_ASSIGNED |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement trong `StaffTracksController` và `TracksService.AssignTopicToTeam`.
- Assignment được lưu bằng `RegisterTeams.TopicId`.
- API yêu cầu team đã có `RegisterTeams.TrackId` và topic phải thuộc đúng track đó.
