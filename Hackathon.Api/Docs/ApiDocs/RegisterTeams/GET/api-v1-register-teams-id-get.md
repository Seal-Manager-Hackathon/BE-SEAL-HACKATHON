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

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*

```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Status": 200,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Message": "SUCCESS",
  "Data": {
    "registerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "teamName": "Chiến binh công nghệ",
    "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "eventName": "SEAL Hackathon 2026",
    "status": 1, /* 0: Pending, 1: Approved, 2: Rejected, 3: Banned */
    "description": "Dự án xe tự hành thông minh",
    "rejectionReason": "Đã được đồng ý",
    "createdAt": "2026-06-19T10:00:00Z"
  }
}
```

## Business rules
- User phải đăng nhập và có role `Student`.
- `registerId` phải tồn tại và chưa bị soft-disable, nếu không trả `REGISTER_TEAM_NOT_FOUND`.
- User phải là thành viên đang active (`Status = Active`) của team sở hữu đơn đăng ký đó, nếu không trả `USER_NOT_IN_TEAM`.
- Nếu đơn đăng ký đang ở trạng thái `Pending`, field `rejectionReason` trả về "Đang đợi xét duyệt".
- Nếu đơn đăng ký ở trạng thái `Approved`, field `rejectionReason` trả về "Đã được đồng ý".
- Nếu đơn đăng ký ở trạng thái `Rejected`, field `rejectionReason` trả về lý do từ chối từ BTC.

### Bảng trạng thái RegisterTeamStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ Staff duyệt đơn |
| `1` | Approved | Đơn đăng ký đã được chấp nhận tham gia sự kiện |
| `2` | Rejected | Đơn đăng ký bị từ chối |
| `3` | Banned | Đội thi bị cấm thi đấu trong sự kiện |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT |
| 403 | FORBIDDEN | USER_NOT_IN_TEAM |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
