# Student lấy chi tiết đơn đăng ký

## Tác dụng
Student lấy thông tin chi tiết của một đơn đăng ký tham gia sự kiện của team mình. Trả về đầy đủ thông tin: Tên Team, Tên sự kiện, Lời mô tả, Trạng thái đơn, Lý do từ chối (nếu có).

## URL
`GET /api/v1/register-teams/{registerId}`

## Authorization
Yêu cầu Access Token hợp lệ của User (User bắt buộc phải đang là thành viên của Team có đơn đăng ký này).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `registerId` | `guid` | Có | ID của đơn đăng ký. |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/register-teams/3fa85f64-5717-4562-b3fc-2c963f66afa6
```

## Request body
Không có.

## Response body
Response dùng `ApiResponseFactory.Base(...)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "...",
  "timestampUtc": "2026-06-19T10:00:00.0000000Z",
  "value": {
    "registerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "teamId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
    "teamName": "Team SEAL",
    "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "eventName": "Hackathon ABC",
    "status": "Pending",
    "description": "Lời nhắn của team lúc đăng ký...",
    "rejectionReason": "Đang đợi xét duyệt",
    "createdAt": "2026-06-19T10:00:00.0000000Z"
  }
}
```

## Business rules
- Đơn đăng ký (`RegisterTeam`) phải tồn tại và không bị vô hiệu hóa (`IsDisable = false`).
- User hiện tại phải là thành viên (`Status = Active`, `IsDisable = false`) của cái Team trong đơn đăng ký đó. Nếu không, trả về lỗi `USER_NOT_IN_TEAM`.
- Logic tạo `rejectionReason` trả về:
  - Nếu `Status = Pending`: trả về `"Đang đợi xét duyệt"`.
  - Nếu `Status = Approved`: trả về `"Đã được đồng ý"`.
  - Nếu `Status = Rejected`: trả về đúng lý do lưu trong DB.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 403 | FORBIDDEN | USER_NOT_IN_TEAM |

## Ghi chú Enum
Tham chiếu file [00-enum-values.md](00-enum-values.md) để biết chi tiết các giá trị số (int) trả về cho các trường Trạng thái (Status).
