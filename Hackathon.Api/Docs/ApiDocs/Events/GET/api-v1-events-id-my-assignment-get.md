# Get my event assignment

## Tác dụng
Xem phân công của user hiện tại trong một event: event role (Mentor/Judge/Staff) và danh sách track được gán. Dùng cho Judge/Mentor muốn biết nhiệm vụ của mình trong event.

## URL
`GET /api/v1/events/{eventId}/my-assignment`

## Authorization
Yêu cầu access token hợp lệ của Judge/Mentor/Staff đã được phân công vào event.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | ID của event. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `role` | `EventRoleEnum` | Không | Lọc theo role: `Judge`, `Mentor`, hoặc `Staff`. Không truyền thì trả về role đầu tiên tìm thấy. |

## Ví dụ request
```http
GET /api/v1/events/20000000-0000-0000-0000-000000000019/my-assignment?role=Judge
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-07-03T08:00:00Z",
  "message": "SUCCESS",
  "data": {
    "assignEventId": "60000000-0000-0000-0000-000000000100",
    "eventId": "20000000-0000-0000-0000-000000000019",
    "eventName": "SEAL Hackathon 2026",
    "role": "Judge",
    "tracks": [
      {
        "assignTrackId": "61000000-0000-0000-0000-000000000100",
        "trackId": "24000000-0000-0000-0000-000000000019",
        "trackTitle": "Robotic Pathfinding",
        "trackDescription": "Track về robot và đường đi"
      }
    ]
  }
}
```

## Business rules
- Người dùng phải được phân công vào event (có bản ghi `AssignEvents`).
- Nếu không tìm thấy phân công, trả 404 `NOT_ASSIGNED_TO_EVENT`.
- `role` query cho phép lọc theo role cụ thể (hữu ích nếu user có nhiều role trong cùng event).
- `tracks` là danh sách các track được phân công (dành cho Judge/Mentor).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | NOT_ASSIGNED_TO_EVENT |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
