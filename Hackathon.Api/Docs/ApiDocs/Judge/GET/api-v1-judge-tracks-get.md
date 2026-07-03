# Judge xem danh sách bảng đấu được phân công

## Tác dụng
Giúp giảng viên được phân công vai trò Judge xem danh sách các bảng đấu (track) mà mình chịu trách nhiệm chấm điểm trong sự kiện.

## URL
`GET /api/v1/judge/tracks`

## ⛔ ĐÃ XOÁ — CHUYỂN SANG API MỚI
API này đã bị xoá. Thay bằng:  
**`GET /api/v1/tracks/my-assignment?eventId={eventId}&role=Judge`**

Xem doc tại: `Docs/ApiDocs/Tracks/GET/api-v1-tracks-my-assignment-get.md`

## Authorization
Yêu cầu access token hợp lệ của tài khoản Giảng viên với vai trò Judge trong event.
Yêu cầu access token hợp lệ của tài khoản Giảng viên với vai trò Judge trong event.

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
      "assignTrackId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
      "trackTitle": "Bảng A - Web Application",
      "trackDescription": "Các đội thi phát triển ứng dụng web",
      "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
      "eventName": "SEAL Hackathon 2026",
      "submissionCount": 15,
      "gradedSubmissionCount": 10
    }
  ]
}
```

## Business rules
- Người gọi phải là giảng viên (`Role = Lecturer` trong `Users`).
- Người gọi phải có `AssignEvents` với `eventRole = Judge` trong event đang hoạt động, nếu không trả về mảng rỗng.
- Trả về danh sách các track mà giám khảo này được phân công thông qua bảng nối `AssignTracks` và `AssignEvents` có cờ role là `Judge` trong event (BR-ASG-03).
- Chỉ lấy các track và event đang hoạt động (`IsDisable = false`).
- **Trường hợp không có track nào**: trả về 200 OK với mảng `data: []` (không phải 403).

### Bảng vai trò EventRoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Unauthorized",
  "status": 401,
  "message": "Vui lòng xác thực tài khoản.",
  "messageCode": "UNAUTHORIZED",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.JudgeController`.
- Route: `GET /api/v1/judge/tracks`.
- Sử dụng policy `LecturerPolicy`.
