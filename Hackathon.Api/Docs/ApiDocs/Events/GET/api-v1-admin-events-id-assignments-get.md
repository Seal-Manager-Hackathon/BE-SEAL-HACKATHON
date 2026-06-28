# Xem danh sách phân công sự kiện (Event Assignments)

## Tác dụng
Cho phép Admin xem danh sách toàn bộ các nhân sự (Staff, Giảng viên) được phân công nhiệm vụ vận hành và chấm điểm trong sự kiện.

## URL
`GET /api/v1/admin/events/{eventId}/assignments`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | ID của sự kiện. |

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
  "message": "SUCCESS",
  "data": [
    {
      "assignEventId": "b1a7d6c2-4821-4f9b-bd5e-3c2fa56789e0",
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "fullName": "Nguyễn Văn A",
      "email": "lecturerA@college.edu.vn",
      "eventRoleId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
      "eventRoleName": 1,
      "assignedTracks": [
        {
          "assignTrackId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
          "trackTitle": "Bảng A - Web Application"
        }
      ]
    }
  ]
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
| `2` | Staff | Nhân viên vận hành sự kiện |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Not Found",
  "status": 404,
  "message": "Event không tồn tại hoặc đã bị ẩn.",
  "messageCode": "EVENT_NOT_FOUND",
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
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | EVENT_NOT_FOUND | Event không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.EventsController`.
- Route: `GET /api/v1/admin/events/{eventId}/assignments`.
- Sử dụng policy `AdminPolicy`.
- Entity: `AssignEvents` + `EventRoles`.
