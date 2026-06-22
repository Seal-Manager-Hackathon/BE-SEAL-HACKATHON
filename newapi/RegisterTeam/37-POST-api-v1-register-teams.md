# API 37: Đăng ký tham gia Event (Register Event)

## Tác dụng
Cho phép Student (phải là Nhóm trưởng - Leader) nộp đơn đăng ký tham gia một sự kiện (Event) thay mặt cho Team của mình.

## URL
`POST /api/v1/register-teams`

## Quyền
Student Leader (Yêu cầu đăng nhập tài khoản Trưởng nhóm)

## Request Headers
- \`Authorization: Bearer <"AccessToken">\`

## Request Body
```json
{
  "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
  "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
  "description": "Chúng em rất mong được tham gia cuộc thi này."
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa thông tin đăng ký.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Value": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "teamName": "Chiến binh công nghệ",
    "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
    "eventName": "SEAL Hackathon 2026",
    "Status": 0, /* Pending */
    "rejectionReason": null,
    "message": "Đăng ký thành công, ban tổ chức đang xét duyệt bạn."
  }
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

### Bảng trạng thái RegisterTeamStatusEnum
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Pending | Đang chờ duyệt đăng ký |
| `1` | Approved | Đã duyệt tham gia sự kiện |
| `2` | Rejected | Bị từ chối tham gia sự kiện |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Đội của bạn đã được duyệt tham gia một sự kiện khác.",
  "MessageCode": "TEAM_ALREADY_APPROVED_FOR_AN_EVENT",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
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
