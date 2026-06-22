# Xem danh sách phân công sự kiện (Event Assignments)

## Tác dụng
Cho phép Admin xem danh sách toàn bộ các nhân sự (Staff, Giảng viên) được phân công nhiệm vụ vận hành và chấm điểm trong sự kiện.

## URL
`GET /api/v1/admin/events/{eventId}/assignments`

## Quyền
Admin-only (Yêu cầu đăng nhập tài khoản Admin)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của sự kiện.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa danh sách các bản ghi phân công.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": [
    {
      "assignEventId": "b1a7d6c2-4821-4f9b-bd5e-3c2fa56789e0",
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "fullName": "Nguyễn Văn A",
      "email": "lecturerA@college.edu.vn",
      "eventRoleId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
      "eventRoleName": "Judge",
      "assignedTracks": [
        {
          "assignTrackId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
          "trackTitle": "Bảng A - Web Application"
        }
      ]
    }
  ],
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Event phải tồn tại trong DB, không bị soft-disable.
- Trả ra đầy đủ thông tin nhân sự và vai trò theo event (`AssignEvents`), cũng như danh sách các bảng đấu được gán chấm điểm (`AssignTracks`).

### Bảng vai trò EventRoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Không có quyền quản lý danh sách phân công.",
  "MessageCode": "FORBIDDEN",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Người gọi không phải Admin toàn cục. |
| 404 | EVENT_NOT_FOUND | Event không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
