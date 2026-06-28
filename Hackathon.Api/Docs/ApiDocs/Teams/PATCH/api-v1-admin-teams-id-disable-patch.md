# Admin disable team

## Tác dụng
Cho phép Admin vô hiệu hóa (disable) một team cụ thể trên toàn cục (đặt cờ IsDisable = true để xóa mềm nhóm khỏi hệ thống).

## URL
`PATCH /api/v1/admin/teams/{teamId}/disable`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần vô hiệu hóa.

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
  "message": "TEAM_DISABLED_SUCCESSFULLY",
  "data": null
}
```

## Business rules
- Team phải tồn tại trong DB, nếu không báo lỗi `TEAM_NOT_FOUND`.
- Cập nhật trường `IsDisable = true` trong bảng `Teams` và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.
- Toàn bộ thành viên thuộc nhóm đó bị ngắt quyền truy cập thông tin nhóm, đơn đăng ký giải đấu liên đới sẽ bị ẩn ở giao diện public.

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
| 403 | FORBIDDEN | ADMIN_ROLE_REQUIRED |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
