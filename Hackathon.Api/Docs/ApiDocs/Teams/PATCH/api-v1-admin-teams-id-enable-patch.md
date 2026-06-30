# Admin enable team

## Tác dụng
Cho phép Admin khôi phục (enable) một team đã bị vô hiệu hóa trước đó (đặt cờ IsDisable = false để team hoạt động trở lại).

## URL
`PATCH /api/v1/admin/teams/{teamId}/enable`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần khôi phục.

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
  "message": "TEAM_ENABLED_SUCCESSFULLY",
  "data": null
}
```

## Business rules
- Team phải tồn tại trong DB và đang bị disable (`IsDisable = true`), nếu không báo lỗi `TEAM_NOT_FOUND`.
- Cập nhật trường `IsDisable = false` trong bảng `Teams` và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.
- Team và toàn bộ thành viên, đơn đăng ký giải đấu liên đới sẽ hiển thị trở lại ở giao diện public.

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

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.TeamController`.
- Route: `PATCH /api/v1/admin/teams/{teamId}/enable`.
- Sử dụng policy `AdminPolicy`.
