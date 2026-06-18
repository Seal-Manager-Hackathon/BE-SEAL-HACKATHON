# Xem chi tiết vòng thi đang tham gia (Dành cho Student)

## Tác dụng
Lấy thông tin chi tiết về 1 vòng thi (Round) cụ thể mà Team đang tham gia, bao gồm các thông tin cơ bản của Round và thông tin cụ thể của đơn đăng ký Team (như Track, Topic đã đăng ký).

## URL
`GET /api/v1/rounds/{roundId}/register-teams/{registerTeamId}`

## Authorization
Yêu cầu access token hợp lệ của User (Student) thuộc về Team đang truy vấn.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `roundId` | `guid` | Có | ID của vòng thi. |
| `registerTeamId` | `guid` | Có | ID của đơn đăng ký team (RegisterTeamId) trong sự kiện. |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/rounds/8f3b2553-933e-4861-a577-ab6453664d41/register-teams/d1e2f3a4-b5c6-d7e8-f9a0-b1c2d3e4f5a6
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
  "traceId": "00-84a1e9df64619d8...",
  "timestampUtc": "2026-06-19T10:00:00.0000000Z",
  "value": {
    "roundId": "8f3b2553-933e-4861-a577-ab6453664d41",
    "eventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "roundName": "Vòng Sơ loại",
    "eventName": "Hackathon 2026",
    "roundNo": 1,
    "teamId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
    "teamName": "Lập trình viên nghèo",
    "registerTeamId": "d1e2f3a4-b5c6-d7e8-f9a0-b1c2d3e4f5a6",
    "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "trackTitle": "Web Application",
    "topicId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "topicTitle": "Quản lý Bệnh viện",
    "startTime": "2026-06-20T08:00:00+00:00",
    "endTime": "2026-06-25T17:00:00+00:00",
    "startSubmission": "2026-06-21T08:00:00+00:00",
    "endSubmission": "2026-06-25T17:00:00+00:00"
  }
}
```

## Business rules
- User phải đăng nhập bằng access token hợp lệ.
- Yêu cầu người gọi (Student) phải là thành viên của Team chứa `registerTeamId` đó (và team không bị soft-disable).
- Team phải đăng ký tham gia `roundId` (có bản ghi trong `RoundDetails` khớp với `registerTeamId`).
- Event, Round tương ứng phải không bị disable (`IsDisable == false`).
- Endpoint trả về các thông tin của vòng thi (Name, RoundNo, Time...) và kèm thông tin đăng ký của Team (Track, Topic).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | ROUND_DETAIL_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
