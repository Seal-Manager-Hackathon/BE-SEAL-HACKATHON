# Student xem chi tiết team

## Tác dụng
Lấy thông tin chi tiết của một team, bao gồm các thành viên với chi tiết như Tên, Ngày sinh, MSSV, Trường học.

## URL
`GET /api/v1/teams/{teamId}`

## Authorization
Yêu cầu access token hợp lệ với role `Student`, `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `teamId` | `guid` | Có | Id của team cần xem. |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/teams/00000000-0000-0000-0000-000000000000
Authorization: Bearer {accessToken}
```

## Request body
Không có.

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "id": "guid",
    "name": "string",
    "canEdit": true,
    "isLeader": true,
    "createdAt": "datetimeoffset",
    "members": [
      {
        "userId": "guid",
        "firstName": "string",
        "lastName": "string",
        "dateOfBirth": "datetimeoffset",
        "studentId": "string",
        "college": "string",
        "isLeader": true,
        "status": 0 /* Active */
      }
    ]
  }
}
```

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | TEAM_NOT_VISIBLE_TO_USER |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
