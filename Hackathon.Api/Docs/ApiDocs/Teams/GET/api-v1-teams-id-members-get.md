# Get team members

## Tác dụng
Lấy danh sách các thành viên hiện tại trong team kèm vai trò trưởng nhóm/thành viên và trạng thái hoạt động.

## URL
`GET /api/v1/teams/{teamId}/members`

## Authorization
Yêu cầu access token hợp lệ với role `Student`, `Staff` hoặc `Admin`.

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa danh sách thành viên.*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "SUCCESS",
  "data": [
    {
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "firstName": "Hoàng",
      "lastName": "Phạm",
      "studentId": "STU123456",
      "college": "Đại Học Bách Khoa",
      "isLeader": true,
      "status": 0 /* 0: Active, 1: Inactive */
    }
  ]
}
```

## Business rules
- Team phải tồn tại trong DB và chưa bị disable.
- Chỉ hiển thị các thành viên có trong bảng `TeamDetails` của team.

### Bảng trạng thái TeamDetailStatusEnum (Integer)
| Giá trị (Value) | Trạng thái (Status) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Active | Thành viên đang hoạt động |
| `1` | Inactive | Thành viên đã rời nhóm hoặc ngưng hoạt động |

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
| 403 | FORBIDDEN | TEAM_NOT_VISIBLE_TO_USER |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
