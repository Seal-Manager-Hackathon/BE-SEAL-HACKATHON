# Team member xem chi tiết đơn đăng ký của team

## Tác dụng
API dùng cho thành viên trong team (cả Leader và Member) xem chi tiết một đơn đăng ký tham gia event của team mình dựa trên `registerId`.

## URL
`GET /api/v1/register-teams/{registerId}`

## Authorization
Yêu cầu access token hợp lệ với role `Student` và user hiện tại phải là thành viên đang active của team sở hữu đơn đăng ký đó.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `registerId` | `guid` | Có | Id của đơn đăng ký cần xem chi tiết. |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/register-teams/3fa85f64-5717-4562-b3fc-2c963f66afa6
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
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "registerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "teamName": "Tên team",
    "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "eventName": "Hackathon ABC",
    "status": 1 /* Approved */,
    "description": "Mô tả đơn đăng ký",
    "rejectionReason": null,
    "createdAt": "2026-06-19T10:00:00.0000000Z"
  },
  "message": "SUCCESS"
}
```

## Business rules
- User phải đăng nhập và có role `Student`.
- `registerId` phải tồn tại và chưa bị soft-disable, nếu không trả `REGISTER_TEAM_NOT_FOUND`.
- User phải là thành viên đang active (`Status = Active`) của team sở hữu đơn đăng ký đó, nếu không trả `USER_NOT_IN_TEAM`.
- Nếu đơn đăng ký đang ở trạng thái `Pending`, field `rejectionReason` trả về "Đang đợi xét duyệt".
- Nếu đơn đăng ký ở trạng thái `Approved`, field `rejectionReason` trả về "Đã được đồng ý".
- Nếu đơn đăng ký ở trạng thái `Rejected`, field `rejectionReason` trả về lý do từ chối từ BTC.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT |
| 403 | FORBIDDEN | USER_NOT_IN_TEAM |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
