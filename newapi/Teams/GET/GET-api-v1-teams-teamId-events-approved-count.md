# API 32: Số sự kiện được duyệt (Team Approved Count)

## Tác dụng
API dùng để đếm số lượng các sự kiện (events) mà một team cụ thể đã được ban tổ chức chấp nhận tham gia (đơn đăng ký có trạng thái là `Approved`).

## URL
`GET /api/v1/teams/{teamId}/events/approved-count`

## Quyền
Authenticated User (Yêu cầu đăng nhập)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của Team cần đếm.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa số lượng count.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "count": 2
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Team phải tồn tại và đang không bị soft-disable (`IsDisable = false`).
- Chỉ đếm các đơn đăng ký (`RegisterTeams`) của `teamId` có `Status = Approved` và `IsDisable = false`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy team.",
  "MessageCode": "TEAM_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại trong DB. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
