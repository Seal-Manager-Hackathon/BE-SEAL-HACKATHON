# BTC unlock team

## Tác dụng
Cho phép Staff/Admin mở khóa cho một team cụ thể, cho phép trưởng nhóm cập nhật nhân sự/thông tin nhóm trong trường hợp đặc biệt.

## URL
`PATCH /api/v1/teams/{teamId}/unlock`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần mở khóa.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "TEAM_UNLOCKED_SUCCESSFULLY",
  "data": null
}
```

## Business rules
- Team phải tồn tại trong DB, nếu không báo lỗi `TEAM_NOT_FOUND`.
- Đặt trường `CanEdit = true` trong bảng `Teams` and cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.
- Giúp mở khóa để leader team có quyền sửa đổi thành viên khi đơn đăng ký bị `Rejected` và cần gửi lại.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Not Found",
  "status": 404,
  "message": "TEAM_NOT_FOUND",
  "messageCode": "NOT_FOUND",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | STAFF_OR_ADMIN_ROLE_REQUIRED |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
