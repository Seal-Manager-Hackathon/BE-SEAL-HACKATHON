# Lấy Topic và Track cho Team

## Tác dụng
Lấy thông tin chủ đề (Topic) và phân ban (Track) của một Team (đơn đăng ký) trong phạm vi sự kiện.

## URL
`GET /api/v1/events/{eventId}/register-teams/{registerTeamId}/topic`

## Authorization
Không yêu cầu Access Token (Public API).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `eventId` | `guid` | Có | ID của sự kiện. |
| `registerTeamId` | `guid` | Có | ID đơn đăng ký của team vào sự kiện đó. |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/events/3fa85f64-5717-4562-b3fc-2c963f66afa6/register-teams/d1e2f3a4-b5c6-d7e8-f9a0-b1c2d3e4f5a6/topic
Authorization: Bearer {accessToken}
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
  "status": 200,
  "traceId": "00-84a1e9df64619d8...",
  "timestampUtc": "2026-06-19T10:00:00.0000000Z",
  "data": {
    "registerTeamId": "d1e2f3a4-b5c6-d7e8-f9a0-b1c2d3e4f5a6",
    "eventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "trackTitle": "Web Application",
    "trackDescription": "Phát triển các ứng dụng nền tảng Web",
    "topicId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "topicTitle": "Quản lý Bệnh viện",
    "topicDescription": "Xây dựng hệ thống số hóa quy trình khám chữa bệnh"
  },
  "message": "SUCCESS"
}
```

## Business rules
- Trả về Track và Topic của team đã được assign trong bảng `RegisterTeams`.
- `registerTeamId` và `eventId` phải khớp với bản ghi thực tế, nếu không trả `REGISTER_TEAM_NOT_FOUND`.
- Nếu chưa được assign Topic hay Track nào, các trường Id/Title sẽ là `null`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
