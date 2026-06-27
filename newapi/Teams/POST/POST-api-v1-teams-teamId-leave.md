# Thí sinh tự rời nhóm (Student Leave Team)

## Tác dụng
Cho phép thành viên hiện tại của team tự rời khỏi nhóm thi đấu.

## URL
`POST /api/v1/teams/{teamId}/leave`

## Quyền
Student Member (Không áp dụng cho Trưởng nhóm trực tiếp)

## Request Headers
- \`Authorization: Bearer <"AccessToken">\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần rời.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "message": "TEAM_LEFT_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Người gọi API phải đang là thành viên hoạt động trong team (`Status = Active` trong `TeamDetails`).
- Trưởng nhóm (`IsLeader = true`) KHÔNG được phép tự rời nhóm bằng API này. Nếu muốn rời nhóm, Leader phải nhường/chuyển quyền leader cho thành viên khác trước (API 30) hoặc giải tán nhóm (nếu được hỗ trợ).
- Team phải đang mở cho phép sửa đổi thành viên (`CanEdit = true`, check BR-TEAM-03).
- Cập nhật trạng thái thành viên trong `TeamDetails` sang `Inactive` (xóa mềm).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Trưởng nhóm không thể rời nhóm. Vui lòng chuyển quyền trưởng nhóm trước.",
  "MessageCode": "LEADER_CANNOT_LEAVE_TEAM",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 403 | LEADER_CANNOT_LEAVE_TEAM | Trưởng nhóm trực tiếp thực hiện rời nhóm. |
| 403 | TEAM_MEMBER_LOCKED | Danh sách thành viên bị khóa vì nhóm đã được BTC duyệt thi đấu. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 404 | NOT_A_TEAM_MEMBER | Người gọi không phải thành viên hoạt động của team này. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
