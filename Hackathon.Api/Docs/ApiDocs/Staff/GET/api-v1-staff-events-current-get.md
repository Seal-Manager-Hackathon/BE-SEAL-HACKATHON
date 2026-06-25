# Staff xem sự kiện đang diễn ra

## URL
`GET /api/v1/staff/events/current`

## Quyền
Yêu cầu access token hợp lệ với role `Staff`.

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "SUCCESS",
  "data": [
    {
      "assignEventId": "b1a7d6c2-4821-4f9b-bd5e-3c2fa56789e0",
      "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
      "eventName": "SEAL Hackathon 2026",
      "season": "Mùa hè 2026",
      "startTime": "2026-07-01T08:00:00Z",
      "endTime": "2026-07-10T17:00:00Z",
      "role": 0, /* 0: Mentor, 1: Judge, 2: Staff */
      "eventStatus": 1 /* 0: Draft, 1: Published, 2: Closed, 3: Cancelled */
    }
  ]
}
```

### Bảng vai trò EventRoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |
| `2` | Staff | Nhân viên vận hành sự kiện |

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | NOT_ASSIGNED_TO_ANY_EVENT |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
