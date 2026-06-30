# Lecturer xem danh sách track được phân công trong event

## Tác dụng
Giúp Lecturer xem danh sách các bảng đấu (Track) mình được phân công trong một sự kiện cụ thể. Lecturer có thể là Mentor, Judge hoặc Staff của event.

## URL
`GET /api/v1/lecturers/events/{eventId}/tracks`

## Authorization
Yêu cầu access token hợp lệ của tài khoản Giảng viên (Lecturer policy).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `eventId` | `guid` | Có | ID của sự kiện cần lấy danh sách track. |

## Response body (Success - 200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": {
    "eventId": "e1f2a3b4-c5d6-7890-abcd-ef1234567890",
    "eventName": "Hackathon 2026",
    "role": 0, /* 0: Mentor, 1: Judge, 2: Staff */
    "tracks": [
      {
        "assignTrackId": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
        "trackId": "b2c3d4e5-f6a7-8901-bcde-f12345678901",
        "trackTitle": "Web Development",
        "trackDescription": "Phát triển ứng dụng web",
        "maxTeam": 20
      }
    ]
  }
}
```

### Fields
| Tên | Kiểu | Mô tả |
|---|---|---|
| `eventId` | `guid` | ID của event. |
| `eventName` | `string` | Tên event. |
| `role` | `int?` | Vai trò của lecturer trong event này (0: Mentor, 1: Judge, 2: Staff). |
| `tracks` | `array` | Danh sách track lecturer được phân công. |

### Track fields
| Tên | Kiểu | Mô tả |
|---|---|---|
| `assignTrackId` | `guid` | ID của bản ghi phân công lecturer vào track. |
| `trackId` | `guid` | ID của track. |
| `trackTitle` | `string` | Tên track. |
| `trackDescription` | `string?` | Mô tả track. |
| `maxTeam` | `int?` | Số lượng team tối đa của track. |

### Bảng vai trò EventRoleEnum (Integer)
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |
| `2` | Staff | Nhân viên vận hành sự kiện |

## Business rules
- Lecturer phải được phân công vào event (`AssignEvents` liên kết với `EventId` và `UserId`). Nếu không có → `NOT_ASSIGNED_TO_EVENT`.
- Trả về role của lecturer trong event (`Mentor`, `Judge`, `Staff`).
- Chỉ trả về các track lecturer được phân công trong event đó (qua `AssignTracks`).
- Sắp xếp theo `Track.Title` tăng dần.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Not Found",
  "status": 404,
  "message": "NOT_ASSIGNED_TO_EVENT",
  "messageCode": "NOT_ASSIGNED_TO_EVENT",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 404 | NOT_ASSIGNED_TO_EVENT | Không có phân công trong sự kiện này. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.LecturersController`.
- Route: `GET /api/v1/lecturers/events/{eventId}/tracks`.
- Sử dụng policy `LecturerPolicy`.
