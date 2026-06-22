# API 42: BTC duyệt đơn đăng ký (Staff Approve Register Team)

## Tác dụng
Staff duyệt đơn đăng ký tham gia event của một team. Khi đơn được duyệt, trạng thái đăng ký chuyển sang `Approved` và team bị khóa chỉnh sửa thành viên để đảm bảo danh sách thành viên tham gia event không thay đổi sau khi được chấp nhận.

## URL
`PUT /api/v1/register-teams/staff/{registerId}/approve`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `registerId` (Guid, Bắt buộc): Id của đơn đăng ký cần duyệt.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa kết quả.*
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
    "Status": 1, /* Approved */
    "message": "REGISTER_TEAM_ACCEPTED_SUCCESSFULLY"
  }
}
```

## Business rules
- Staff hoặc Admin phải đăng nhập bằng access token hợp lệ.
- Endpoint này cho phép Staff hoặc Admin qua `[Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]`.
- `registerId` là bắt buộc trên path.
- Đơn đăng ký phải tồn tại và chưa bị soft-disable, nếu không trả `REGISTER_TEAM_NOT_FOUND`.
- Event của đơn đăng ký phải tồn tại và chưa bị soft-disable, nếu không trả `EVENT_NOT_FOUND`.
- Team của đơn đăng ký phải tồn tại và chưa bị soft-disable, nếu không trả `TEAM_NOT_FOUND`.
- Staff phải được phân công vào event của đơn đăng ký đó (`AssignEvents`) thì mới được duyệt, nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- Admin được duyệt trực tiếp, không cần kiểm tra phân công vào event.
- Chỉ được duyệt đơn đang ở trạng thái `Pending`; nếu đơn đã `Approved` hoặc `Rejected` thì trả conflict.
- Nếu team đang bị banned khỏi event (`IsBanned == true`) thì không được duyệt.
- Khi duyệt thành công:
  - `RegisterTeams.Status` được cập nhật thành `Approved`.
  - `RegisterTeams.RejectionReason` được reset về `null`.
  - `RegisterTeams.UpdatedAt` được cập nhật theo thời gian hiện tại.
  - `Teams.CanEdit` được cập nhật thành `false` để khóa chỉnh sửa thành viên.
  - `Teams.UpdatedAt` được cập nhật theo thời gian hiện tại.
- Nên thực hiện cập nhật trong transaction để tránh trạng thái đơn đã duyệt nhưng team chưa bị khóa.

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
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Đội thi này hiện đang bị cấm tham gia giải đấu.",
  "MessageCode": "TEAM_IS_BANNED_FROM_EVENT",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | User không có role `Staff`. |
| 403 | FORBIDDEN | STAFF_NOT_ASSIGNED_TO_EVENT |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 409 | CONFLICT | REGISTER_TEAM_ALREADY_APPROVED |
| 409 | CONFLICT | REGISTER_TEAM_ALREADY_REJECTED |
| 409 | CONFLICT | TEAM_IS_BANNED_FROM_EVENT |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
