# API 43: BTC từ chối đơn đăng ký (Staff Reject Register Team)

## Tác dụng
Staff từ chối đơn đăng ký tham gia event của một team, chuyển trạng thái đơn từ `Pending` sang `Rejected` kèm lý do từ chối.

## URL
`PUT /api/v1/register-teams/staff/{registerId}/reject`

## Quyền
Staff hoặc Admin (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `registerId` (Guid, Bắt buộc): Id của đơn đăng ký cần từ chối.

## Request Body
```json
{
  "reason": "Danh sách thành viên thiếu thông tin bắt buộc."
}
```

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
    "eventId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "eventName": "SEAL Hackathon 2026",
    "Status": 2, /* Rejected */
    "rejectionReason": "Danh sách thành viên thiếu thông tin bắt buộc.",
    "message": "REGISTER_TEAM_REJECTED_SUCCESSFULLY"
  }
}
```

## Business rules
- Staff hoặc Admin phải đăng nhập bằng access token hợp lệ.
- Endpoint này cho phép Staff hoặc Admin qua `[Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]`.
- `registerId` là bắt buộc trên path.
- `reason` là bắt buộc trong request body, sau khi trim không được để trống, nếu không trả `REASON_REQUIRED`.
- Đơn đăng ký phải tồn tại và chưa bị soft-disable, nếu không trả `REGISTER_TEAM_NOT_FOUND`.
- Event của đơn đăng ký phải tồn tại và chưa bị soft-disable, nếu không trả `EVENT_NOT_FOUND`.
- Team của đơn đăng ký phải tồn tại và chưa bị soft-disable, nếu không trả `TEAM_NOT_FOUND`.
- Staff phải được phân công vào event của đơn đăng ký đó (`AssignEvents`) thì mới được từ chối, nếu không trả `STAFF_NOT_ASSIGNED_TO_EVENT`.
- Admin được từ chối trực tiếp, không cần kiểm tra phân công vào event.
- Chỉ được từ chối đơn đang ở trạng thái `Pending`; nếu đơn đã `Approved` hoặc `Rejected` thì trả conflict.
- Khi từ chối thành công:
  - `RegisterTeams.Status` được cập nhật thành `Rejected`.
  - `RegisterTeams.RejectionReason` được lưu bằng `reason` đã trim.
  - `RegisterTeams.UpdatedAt` được cập nhật theo thời gian hiện tại.
  - `Teams.CanEdit` được cập nhật thành `true` để team có thể chỉnh sửa/gửi lại sau khi bị từ chối.
  - `Teams.UpdatedAt` được cập nhật theo thời gian hiện tại.
- Nên thực hiện cập nhật trong transaction để tránh trạng thái đơn đã bị từ chối nhưng team chưa được mở khóa.

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
  "Title": "Bad Request",
  "Status": 400,
  "Detail": "Vui lòng nhập lý do từ chối đăng ký.",
  "MessageCode": "REASON_REQUIRED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | REASON_REQUIRED | Lý do từ chối trống hoặc chỉ có khoảng trắng. |
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | STAFF_NOT_ASSIGNED_TO_EVENT | Staff chưa được BTC phân công phụ trách quản lý sự kiện này. |
| 404 | REGISTER_TEAM_NOT_FOUND | Đơn đăng ký không tồn tại. |
| 409 | REGISTER_TEAM_ALREADY_REJECTED | Đơn đăng ký đã bị từ chối trước đó. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
