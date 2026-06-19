# Get my rounds by team

## Tác dụng
Lấy danh sách vòng thi (rounds) mà team đang tham gia, dùng để xác định team đang ở round nào trong event.

## URL
`GET /api/v1/rounds/teams/{teamId}`

## Authorization
Yêu cầu access token hợp lệ (Student thuộc team).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `teamId` | `guid` | Có | Id của team cần lấy danh sách round. |

## Query parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Không | Lọc round thuộc một event cụ thể. |

## Ví dụ request
```http
GET /api/v1/rounds/teams/00000000-0000-0000-0000-000000000000?eventId=00000000-0000-0000-0000-000000000000
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.Base(data)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": [
    {
      "roundId": "guid",
      "eventId": "guid",
      "roundName": "string",
      "eventName": "string",
      "roundNo": 0,
      "teamId": "guid",
      "teamName": "string",
      "registerTeamId": "guid",
      "startTime": "datetimeoffset|null",
      "endTime": "datetimeoffset|null",
      "startSubmission": "datetimeoffset|null",
      "endSubmission": "datetimeoffset|null"
    }
  ]
}
```

## Business rules
- Yêu cầu user đã đăng nhập (access token hợp lệ).
- User phải là thành viên của team (không bị soft-disable).
- Nếu truyền `eventId`, event phải tồn tại.
- Chỉ trả về các round mà team đang tham gia, không bị disable.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
