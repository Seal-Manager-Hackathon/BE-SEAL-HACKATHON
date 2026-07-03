# Mentor xem danh sách bảng đấu phụ trách

## Tác dụng
Giúp Mentor xem danh sách các track (bảng thi đấu) mà mình được BTC phân công quản lý và hỗ trợ thí sinh.

## URL
`GET /api/v1/mentor/tracks`

## ⛔ ĐÃ XOÁ — CHUYỂN SANG API MỚI
API này đã bị xoá. Thay bằng:  
**`GET /api/v1/tracks/my-assignment?eventId={eventId}&role=Mentor`**

Xem doc tại: `Docs/ApiDocs/Tracks/GET/api-v1-tracks-my-assignment-get.md`

## Quyền
Mentor phụ trách track (Yêu cầu đăng nhập tài khoản Giảng viên được phân công)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "Value": [
    {
      "assignTrackId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
      "trackTitle": "Bảng A - Web Application",
      "eventId": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
      "eventName": "SEAL Hackathon 2026"
    }
  ],
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Trích xuất thông tin các track của mentor từ bảng `AssignTracks` liên kết với bản ghi `AssignEvents` có vai trò `Mentor` (BR-ASG-03).
- Chỉ hiển thị các bảng đấu và sự kiện đang hoạt động.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "title": "Unauthorized",
  "status": 401,
  "Detail": "Vui lòng xác thực tài khoản giảng viên.",
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
| 403 | FORBIDDEN | Không được phân công làm Mentor trong bảng đấu nào. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
