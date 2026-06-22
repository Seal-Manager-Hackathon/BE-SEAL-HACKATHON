# Giảng viên tự xem danh sách phân công (Get My Assignments)

## Tác dụng
Giúp giảng viên/nhân viên tự tra cứu danh sách các sự kiện và bảng đấu mình được phân công nhiệm vụ vận hành, hướng dẫn hoặc chấm điểm.

## URL
`GET /api/v1/me/assignments`

## Quyền
Staff hoặc Lecturer (Yêu cầu đăng nhập tài khoản giảng viên/nhân viên)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": [
    {
      "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
      "eventName": "SEAL Hackathon 2026",
      "role": "Judge",
      "tracks": [
        {
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
- Tài khoản đăng nhập phải có global role là `Staff` hoặc `Lecturer`.
- Trích xuất thông tin gán từ bảng `AssignEvents` và `AssignTracks` liên kết với `UserId` hiện tại.
- Chỉ hiển thị các event và track chưa bị disable.

### Bảng vai trò EventRoleEnum
| Giá trị (Value) | Vai trò (Role) | Mô tả (Description) |
| :--- | :--- | :--- |
| `0` | Mentor | Người hướng dẫn chuyên môn cho đội thi |
| `1` | Judge | Giám khảo chấm điểm bài thi |

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Unauthorized",
  "Status": 401,
  "Detail": "Vui lòng xác thực tài khoản.",
  "MessageCode": "UNAUTHORIZED",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Tài khoản của bạn không được phân quyền. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
