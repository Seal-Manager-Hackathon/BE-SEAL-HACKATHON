# Admin xem chi tiết event

## Tác dụng
Cho admin xem chi tiết event — **kể cả event đã bị xoá (IsDisable = true)**. Không giống `GET /api/v1/events/{eventId}` bên user, API này bỏ qua mọi filter.

## URL
`GET /api/v1/admin/events/{eventId}`

## Authorization
Yêu cầu access token hợp lệ của tài khoản **Admin**.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `eventId` | `guid` | Có | ID của event cần xem. |

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-07-03T10:00:00Z",
  "data": {
    "id": "4d2034e3-80fb-4572-a04e-27df2bae6d25",
    "name": "Hackathon mùa hè",
    "description": "Sự kiện công nghệ lớn nhất năm",
    "startTime": "2026-08-01T08:00:00+07:00",
    "endTime": "2026-08-03T17:00:00+07:00",
    "registerLimitTime": "2026-07-25T23:59:00+07:00",
    "limitTeam": 50,
    "minMember": 2,
    "maxMember": 5,
    "status": "Draft",
    "numberRound": 0,
    "season": "Summer 2026",
    "isDisable": false,
    "createdAt": "2026-07-03T09:00:00Z"
  },
  "message": "SUCCESS"
}
```

## Business rules
- Admin xem được **tất cả** event — kể cả đã bị xoá (`IsDisable = true`).
- Không kiểm tra `Status` hay `IsDisable`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không phải Admin. |
| 404 | EVENT_NOT_FOUND | Event không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.EventsController`.
- Route: `GET /api/v1/admin/events/{eventId}`.
- Sử dụng policy `AdminPolicy`.
- Dùng chung response `EventResponse` với user API.
