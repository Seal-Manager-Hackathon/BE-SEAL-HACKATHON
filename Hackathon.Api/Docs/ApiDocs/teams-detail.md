# Get Team Detail

## Tác dụng
Xem thông tin chi tiết của một team bao gồm thông tin cơ bản của team và danh sách thành viên (chỉ trả về UserId, IsLeader, Status).

## URL
`GET /api/v1/teams/{teamId:guid}`

## Request Parameters
*   **Route Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần xem chi tiết.

## Request Headers
```
Authorization: Bearer <token>
```

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "value": {
    "id": "guid",
    "name": "string",
    "canEdit": true,
    "createdAt": "datetime",
    "members": [
      {
        "userId": "guid",
        "isLeader": true,
        "status": "Active"
      }
    ]
  }
}
```

## Business rules
- Yêu cầu xác thực tài khoản qua Access Token ở Header.
- Chỉ những đối tượng sau mới có quyền xem thông tin chi tiết của team:
  - Thành viên hiện tại của team đó (đã được lưu trong `TeamDetails` và không bị disable).
  - Người dùng có vai trò là `Staff` hoặc `Admin`.
- Nếu người dùng khác cố tình truy cập sẽ trả về lỗi `403 Forbidden` (`TEAM_NOT_VISIBLE_TO_USER`).
- Danh sách thành viên trả về được sắp xếp theo:
  - Leader lên đầu tiên (`IsLeader = true` trước).
  - Tiếp theo là thời gian tham gia team của các thành viên (`CreatedAt` tăng dần).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. (khi không truyền token) |
| 401 | INVALID_ACCESS_TOKEN | Invalid access token. (khi token sai định dạng) |
| 403 | FORBIDDEN | TEAM_NOT_VISIBLE_TO_USER (không có quyền xem chi tiết team) |
| 404 | NOT_FOUND | TEAM_NOT_FOUND (team không tồn tại hoặc đã bị khóa) |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
