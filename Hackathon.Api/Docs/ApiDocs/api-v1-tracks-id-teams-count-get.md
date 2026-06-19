# Đếm số Team đăng ký vào Track

## Tác dụng
Lấy số lượng Team hiện đang được assign (gán) vào một Track nhất định trong hệ thống, bao gồm số team giới hạn tối đa (`MaxTeam`) của phân ban.

## URL
`GET /api/v1/tracks/{trackId}/teams/count`

## Authorization
Không yêu cầu Access Token (Public API).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `trackId` | `guid` | Có | ID của phân ban (Track) cần đếm số team. |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/tracks/c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d/teams/count
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.Base(result)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "00-84a1e9df64619d8...",
  "timestampUtc": "2026-06-19T10:00:00.0000000Z",
  "value": {
    "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "eventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "title": "Web Application",
    "maxTeam": 50,
    "currentTeamCount": 12
  }
}
```

## Business rules
- Không yêu cầu đăng nhập.
- `trackId` là bắt buộc, Track không bị soft-disable.
- Event chứa Track đó cũng phải chưa bị soft-disable. Nếu không thoả, trả về `TRACK_NOT_FOUND` hoặc `EVENT_NOT_FOUND`.
- `currentTeamCount` chỉ đếm các Team thuộc về Track đó đã được phê duyệt đơn đăng ký (`Status = Approved`), và cả đơn `RegisterTeams` cũng như `Teams` đều không bị soft-disable.
- Trả về `maxTeam` (nếu có cấu hình) để frontend dễ dàng xác định xem Track đó đã đầy hay chưa.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 404 | NOT_FOUND | TRACK_NOT_FOUND |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |