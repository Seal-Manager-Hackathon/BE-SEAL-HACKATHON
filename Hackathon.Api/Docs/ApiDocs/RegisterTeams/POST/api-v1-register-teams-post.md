# Register team for event

## Tác dụng
Cho phép team đăng ký tham gia một event. Sau khi đăng ký, đơn sẽ ở trạng thái `Pending` chờ Staff/Admin duyệt.

## URL
`POST /api/v1/register-teams`

## Authorization
Yêu cầu access token hợp lệ (Student đã đăng nhập).

## Request body
```json
{
  "eventId": "guid",
  "teamId": "guid"
}
```

| Field | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | Id của event muốn đăng ký. |
| `teamId` | `guid` | Có | Id của team sẽ tham gia event. |

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
    "id": "guid",
    "teamId": "guid",
    "eventId": "guid",
    "status": 0
  },
  "message": "REGISTERED_SUCCESSFULLY"
}
```

## Business rules
- Người dùng đăng ký phải là `Student` đang hoạt động (không bị disable).
- Chỉ Đội trưởng (Team Leader) có trạng thái `Active` và không bị disable mới có quyền đăng ký cho đội tham gia sự kiện.
- Đội thi (`Team`) và Sự kiện (`Event`) phải tồn tại và không bị disable.
- Hạn đăng ký: Thời gian hiện tại phải trước hạn đăng ký của sự kiện (`RegisterLimitTime`). Nếu quá hạn, trả lỗi `EVENT_REGISTRATION_CLOSED`.
- Số lượng thành viên của đội (số thành viên có trạng thái `Active` và không bị disable) phải đáp ứng yêu cầu tối thiểu (`MinMember`) và tối đa (`MaxMember`) của sự kiện.
- Nếu đội thi đã có đơn đăng ký sự kiện này với trạng thái `Pending` hoặc `Approved`, trả lỗi `TEAM_ALREADY_REGISTERED_FOR_EVENT`.
- Nếu đội thi từng đăng ký và đơn bị từ chối (`Rejected`), đội thi được phép đăng ký lại. Khi đó, trạng thái đơn sẽ cập nhật lại thành `Pending` và trả về thông báo `"REGISTERED_AGAIN_SUCCESSFULLY"`.
- Nếu đội đã được chấp nhận (`Approved`) tham gia vào bất kỳ sự kiện nào khác trước đó, đội thi sẽ không được phép đăng ký sự kiện mới (`TEAM_ALREADY_APPROVED_FOR_AN_EVENT`).
- Số lượng đội tối đa: Tổng số đội đã được đăng ký cho sự kiện này (không tính các đội bị từ chối `Rejected`) không được vượt quá số lượng đội tối đa của sự kiện (`LimitTeam`).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN / ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | CURRENT_USER_MUST_BE_STUDENT |
| 403 | FORBIDDEN | ONLY_TEAM_LEADER_CAN_REGISTER_EVENT |
| 403 | FORBIDDEN | TEAM_ALREADY_APPROVED_FOR_AN_EVENT |
| 404 | NOT_FOUND | USER_NOT_FOUND |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 400 | BAD_REQUEST | EVENT_REGISTRATION_CLOSED |
| 400 | BAD_REQUEST | TEAM_DOES_NOT_MEET_MIN_MEMBERS_{limit} |
| 400 | BAD_REQUEST | TEAM_EXCEEDS_MAX_MEMBERS_{limit} |
| 409 | CONFLICT | TEAM_ALREADY_REGISTERED_FOR_EVENT |
| 409 | CONFLICT | EVENT_REACHED_MAX_TEAMS_LIMIT |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
