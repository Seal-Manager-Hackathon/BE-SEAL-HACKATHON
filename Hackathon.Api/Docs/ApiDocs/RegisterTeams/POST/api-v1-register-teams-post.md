# Đăng ký tham gia Event (Team nộp đơn)

## Tác dụng
Cho phép Student (phải là Nhóm trưởng - Leader) nộp đơn đăng ký tham gia một sự kiện (Event) thay mặt cho Team của mình.

## URL
`POST /api/v1/register-teams`

## Authorization
Yêu cầu Access Token của người dùng với Role `Student`.

## Path parameters
Không có.

## Query parameters
Không có.

## Request body
```json
{
  "teamId": "guid",
  "eventId": "guid",
  "description": "string (Tùy chọn - Lời nhắn gửi tới Ban tổ chức)"
}
```

| Field | Type | Bắt buộc | Mô tả |
|---|---|---|---|
| `teamId` | `guid` | Có | ID của Team mà User đang là nhóm trưởng. |
| `eventId` | `guid` | Có | ID của Event mà Team muốn tham gia. |
| `description` | `string` | Không | Mô tả, định hướng hoặc lời giới thiệu của nhóm. |

## Ví dụ request
```http
POST /api/v1/register-teams
Content-Type: application/json

{
  "teamId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
  "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
  "description": "Chúng em rất mong được tham gia cuộc thi này."
}
```

## Response body
Response dùng `ApiResponseFactory.Base(...)`.

```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "...",
  "timestampUtc": "2026-06-19T10:00:00.0000000Z",
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "teamId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
    "teamName": "Tên team",
    "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "eventName": "Tên sự kiện",
    "status": 0 /* Pending */,
    "rejectionReason": null,
    "isBanned": false
  },
  "message": "REGISTERED_SUCCESSFULLY"
}
```

## Business rules
- User phải có Role `Student` và tài khoản không bị khóa, đã xác minh.
- User hiện tại bắt buộc phải là **Nhóm trưởng (Leader)** của cái `teamId` truyền vào.
- Event phải tồn tại, đang mở đăng ký (`IsDisable = false`), và hiện tại phải nhỏ hơn `RegisterLimitTime` (nếu có).
- Số lượng thành viên Active của Team phải thỏa mãn điều kiện `MinMember` và `MaxMember` của Event.
- Nếu Event có giới hạn số đội (`LimitTeam`), hệ thống sẽ kiểm tra xem đã đủ số lượng đội (Pending + Approved) chưa. Nếu đã đủ, sẽ báo lỗi `EVENT_REACHED_MAX_TEAMS_LIMIT`.
- Nếu Team đã từng nộp đơn vào Event này:
  - Nếu đang ở trạng thái `Pending` hoặc `Approved`: Trả lỗi `TEAM_ALREADY_REGISTERED_FOR_EVENT`.
  - Nếu đã bị từ chối (`Rejected`): Chấp nhận cho gửi lại đơn mới, cập nhật trạng thái đơn đó thành `Pending` và ghi lại mô tả mới.
- Nếu Team đã từng nộp đơn và được `Approved` ở MỘT Event KHÁC, họ sẽ KHÔNG được phép đăng ký thêm Event này nữa (báo lỗi `TEAM_ALREADY_APPROVED_FOR_AN_EVENT`).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | TEAM_ID_REQUIRED, EVENT_ID_REQUIRED |
| 400 | BAD_REQUEST | EVENT_REGISTRATION_CLOSED |
| 400 | BAD_REQUEST | TEAM_DOES_NOT_MEET_MIN_MEMBERS_X |
| 400 | BAD_REQUEST | TEAM_EXCEEDS_MAX_MEMBERS_X |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT |
| 403 | FORBIDDEN | ONLY_TEAM_LEADER_CAN_REGISTER_EVENT |
| 403 | FORBIDDEN | TEAM_ALREADY_APPROVED_FOR_AN_EVENT |
| 404 | NOT_FOUND | TEAM_NOT_FOUND, EVENT_NOT_FOUND, USER_NOT_FOUND |
| 409 | CONFLICT | TEAM_ALREADY_REGISTERED_FOR_EVENT |
| 409 | CONFLICT | EVENT_REACHED_MAX_TEAMS_LIMIT |

## Ghi chú Enum
Tham chiếu file [00-enum-values.md](00-enum-values.md) để biết chi tiết các giá trị số (int) trả về cho các trường Trạng thái (Status).
