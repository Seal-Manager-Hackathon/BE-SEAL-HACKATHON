# BTC lock team

## Tác dụng
Cho phép Staff/Admin khóa cứng thông tin của một team cụ thể (không cho phép đổi tên nhóm, thêm/mời thành viên mới, xóa thành viên cũ, hoặc tự rời nhóm).

## URL
`PATCH /api/v1/teams/{teamId}/lock`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần khóa.

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
  "message": "TEAM_LOCKED_SUCCESSFULLY",
  "data": null
}
```

## Business rules
- Team phải tồn tại trong DB, nếu không báo lỗi `TEAM_NOT_FOUND`.
- Đặt trường `CanEdit = false` trong bảng `Teams` và cập nhật `UpdatedAt = DateTimeOffset.UtcNow`.
- Hệ thống tự động kích hoạt API này khi một trong số các đơn đăng ký thi của team được chuyển sang trạng thái `Approved` (duyệt tham gia event, BR-TEAM-07).

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
