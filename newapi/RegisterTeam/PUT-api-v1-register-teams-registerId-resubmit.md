# Gửi lại đơn đăng ký (Register Team Resubmit)

## Tác dụng
Cho phép Trưởng nhóm gửi lại đơn đăng ký đã bị từ chối, chuyển trạng thái về Pending để BTC duyệt lại. API này chỉ thực hiện thao tác gửi lại đơn, không sửa thông tin team/thành viên trong request này.

## URL
`PUT /api/v1/register-teams/{registerId}/resubmit`

## Quyền
Student Leader (Yêu cầu đăng nhập tài khoản Trưởng nhóm)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `registerId` (Guid, Bắt buộc): ID của đơn đăng ký cần nộp lại.

## Request Body
Không yêu cầu body. Client chỉ gọi API để gửi lại đơn đăng ký đã bị reject.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Value": {
    "registerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "Status": 0, /* Pending */
    "message": "REGISTER_TEAM_RESUBMITTED"
  }
}
```

## Business rules
- Đơn đăng ký phải tồn tại trong DB và đang ở trạng thái `Rejected` (BTC đã từ chối duyệt).
- Người gọi phải là Leader của team đăng ký.
- Cập nhật trạng thái `Status = Pending` (chờ duyệt), làm trống trường lý do `RejectionReason = null` và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.
- API này không cập nhật thông tin/thành viên team và không cập nhật mô tả đăng ký; các thay đổi liên quan phải được xử lý ở luồng/API phù hợp trước khi gửi lại.

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
  "Detail": "Đơn đăng ký không ở trạng thái bị từ chối.",
  "MessageCode": "REGISTER_TEAM_NOT_REJECTED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | ONLY_TEAM_LEADER_CAN_RESUBMIT | Chỉ trưởng nhóm mới được nộp lại đơn. |
| 404 | REGISTER_TEAM_NOT_FOUND | Đơn đăng ký không tồn tại. |
| 409 | REGISTER_TEAM_NOT_REJECTED | Đơn không ở trạng thái Rejected, không thể resubmit. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
