 # Staff assign track to team

## Tác dụng
Staff gán một track cho team đã đăng ký event, để xác định team thuộc track nào trong event đó.

## URL
`PATCH /api/v1/staff/teams/{teamId}/track`

## Authorization
Yêu cầu access token hợp lệ với role `Staff`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `teamId` | `guid` | Có | Id của team cần được gán track. |

## Request body
```json
{
  "trackId": "guid"
}
```

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
    "teamId": "guid",
    "teamName": "string",
    "eventId": "guid",
    "trackId": "guid",
    "trackTitle": "string"
  },
  "message": "TRACK_ASSIGNED_TO_TEAM_SUCCESSFULLY"
}
```

## Business rules
- Staff phải đăng nhập bằng access token hợp lệ.
- Staff phải được phân công vào event chứa track (`AssignEvents`) thì mới được gán team vào track của event đó.
- Team phải tồn tại và chưa bị soft-disable.
- Track phải tồn tại và chưa bị soft-disable.
- Track phải thuộc một event đang tồn tại và chưa bị soft-disable.
- Team phải có đơn đăng ký event tương ứng với `track.eventId`.
- Đơn đăng ký event của team không được bị soft-disable, không bị banned và nên ở trạng thái `Approved`.
- Nếu team đã được gán track trước đó, API cập nhật sang track mới.
- Khi đổi track, nếu team đã được gán topic thuộc track cũ thì cần xóa hoặc reset topic assignment để tránh topic không còn thuộc track mới.
- Không dùng request body để truyền `teamId`; `teamId` lấy từ path.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 400 | BAD_REQUEST | TRACK_ID_REQUIRED |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 403 | FORBIDDEN | REGISTER_TEAM_NOT_APPROVED |
| 409 | CONFLICT | TEAM_IS_BANNED_FROM_EVENT |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement trong `StaffTracksController` và `TracksService.AssignTrackToTeam`.
- Assignment được lưu bằng `RegisterTeams.TrackId`.
- Khi gán track mới, `RegisterTeams.TopicId` được reset về `null` để tránh topic cũ không thuộc track mới.
