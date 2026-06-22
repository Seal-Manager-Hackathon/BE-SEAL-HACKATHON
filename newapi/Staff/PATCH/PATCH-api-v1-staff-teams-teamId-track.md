# API 47: BTC gán bảng đấu cho Team (Staff Assign Track)

## Tác dụng
Cho phép Staff gán một bảng đấu (Track) cho team đã được duyệt vào event sau khi BTC chọn event và có kết quả bốc thăm offline.

## URL
`PATCH /api/v1/staff/teams/{teamId}/track`

## Quyền
Staff (Yêu cầu đăng nhập tài khoản Staff; route hiện tại dùng `StaffPolicy` trong controller)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team.

## Request Body
```json
{
  "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa kết quả.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "message": "TRACK_ASSIGNED_SUCCESSFULLY"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- BTC chọn event trước, sau đó chọn team đã `Approved` trong event để nhập kết quả bốc thăm offline.
- Team và Track gán phải tồn tại trong DB, không bị soft-disable và thuộc cùng event.
- Đơn đăng ký thi đấu của team vào event phải ở trạng thái `Approved` (BR-TRACK-03).
- Cập nhật trường `TrackId` của bản ghi trong bảng `RegisterTeams`.
- Staff thực hiện phải có quyền quản lý sự kiện tương ứng.
- Gán Track là bước bắt buộc trước khi gán Topic cho team.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Đơn đăng ký của đội chưa được duyệt thi đấu.",
  "MessageCode": "REGISTER_TEAM_NOT_APPROVED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không được gán quyền quản lý sự kiện này. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 404 | TRACK_NOT_FOUND | Track không tồn tại trong hệ thống. |
| 409 | REGISTER_TEAM_NOT_APPROVED | Team đăng ký thi đấu nhưng đơn chưa được duyệt Approved. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
