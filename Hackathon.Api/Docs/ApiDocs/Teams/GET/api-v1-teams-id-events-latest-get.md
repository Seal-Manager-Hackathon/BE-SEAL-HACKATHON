# Lấy event team tham gia mới nhất (Approved)

## Tác dụng
Lấy ra duy nhất **một** event tham gia gần đây nhất của một team (đã được chấp nhận - Approved).

## URL
`GET /api/v1/teams/{teamId}/events/latest`

## Authorization
Yêu cầu access token hợp lệ với role `Student`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `teamId` | `guid` | Có | ID của team cần tra cứu. |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/teams/c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d/events/latest
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.Base(...)`. Nếu team không tham gia event nào được duyệt, value sẽ là `null`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "...",
  "timestampUtc": "2026-06-19T...",
  "value": {
    "registerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "eventName": "Hackathon ABC",
    "status": "Approved",
    "createdAt": "2026-06-19T10:00:00.0000000Z"
  }
}
```

## Business rules
- Team phải tồn tại và không bị vô hiệu hóa (`IsDisable = false`).
- Chỉ lấy đơn đăng ký có `Status = Approved`.
- Sắp xếp đơn đăng ký theo thời gian `CreatedAt` giảm dần và lấy cái đầu tiên (mới nhất).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
