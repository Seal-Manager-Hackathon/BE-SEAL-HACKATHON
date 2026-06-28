# Get rejection reason

## Tác dụng
Lấy lý do từ chối đơn đăng ký tham gia event của team. Dùng khi team bị từ chối và muốn xem lý do để chỉnh sửa và đăng ký lại.

## URL
`GET /api/v1/register-teams/{registerId}/rejection-reason`

## Authorization
Yêu cầu access token hợp lệ (Student leader của team hoặc Staff/Admin).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `registerId` | `guid` | Có | Id của đơn đăng ký. |

## Ví dụ request
```http
GET /api/v1/register-teams/00000000-0000-0000-0000-000000000000/rejection-reason
Authorization: Bearer {accessToken}
```

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "registerId": "guid",
    "teamId": "guid",
    "eventId": "guid",
    "status": 2, /* Enum: 0: Pending, 1: Approved, 2: Rejected */
    "rejectionReason": "string"
  },
  "message": "SUCCESS"
}
```

## Business rules
- Đơn đăng ký phải tồn tại.
- Chỉ leader của team hoặc Staff/Admin mới xem được lý do từ chối.
- Nếu đơn chưa bị từ chối (chưa có `rejectionReason`), trả về null hoặc lỗi.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | ACCESS_TOKEN_IS_MISSING |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 403 | FORBIDDEN | FORBIDDEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
