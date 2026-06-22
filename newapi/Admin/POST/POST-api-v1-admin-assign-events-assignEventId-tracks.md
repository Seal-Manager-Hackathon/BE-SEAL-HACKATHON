# Phân công Mentor/Judge vào Track (Assign Track)

## Tác dụng
Cho phép Admin/Staff gán giảng viên đã có vai trò Mentor hoặc Judge trong event vào một bảng đấu (Track) cụ thể. Phân công theo track quyết định phạm vi trách nhiệm: Mentor phụ trách các team thuộc track đó; Judge chỉ xem và chấm các team/submission thuộc track đó.

## URL
`POST /api/v1/admin/assign-events/{assignEventId}/tracks`

## Quyền
Admin hoặc Staff (Yêu cầu đăng nhập tài khoản BTC)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `assignEventId` (Guid, Bắt buộc): ID phân công sự kiện của giảng viên.

## Request Body
```json
{
  "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d"
}
```

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa ID bản ghi gán.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "assignTrackId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "message": "TRACK_ASSIGNED"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Bản ghi `AssignEvents` và `Tracks` liên đới phải tồn tại trong DB, không bị soft-disable.
- Track gán phải thuộc cùng Event mà giảng viên đã được phân công.
- Tạo bản ghi mới trong bảng `AssignTracks`. Nếu giảng viên đã được gán vào track này rồi, báo conflict `TRACK_ALREADY_ASSIGNED`.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Conflict",
  "Status": 409,
  "Detail": "Giảng viên này đã được phân công bảng đấu này rồi.",
  "MessageCode": "TRACK_ALREADY_ASSIGNED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Không có quyền quản trị hoặc vận hành. |
| 404 | ASSIGNMENT_NOT_FOUND | Không tìm thấy bản ghi phân công event. |
| 404 | TRACK_NOT_FOUND | Bảng đấu không tồn tại. |
| 409 | TRACK_ALREADY_ASSIGNED | Giảng viên đã được gán vào track này từ trước. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
