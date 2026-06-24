# Xem chi tiết đề bài (Topic Detail)

## Tác dụng
Xem thông tin cấu hình chi tiết của một đề thi/chủ đề thi (Topic).

## URL
`GET /api/v1/topics/{topicId}`

## Quyền
Public API (Hoặc Authenticated tùy theo cài đặt ẩn hiện đề thi)

## Request Parameters
*   **Path Parameters:**
    *   `topicId` (Guid, Bắt buộc): ID của đề thi.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "trackId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "Title": "Hệ thống số hóa y tế",
    "description": "Xây dựng ứng dụng quản lý quy trình khám chữa bệnh.",
    "createdAt": "2026-06-21T08:00:00Z",
    "updatedAt": "2026-06-21T08:00:00Z"
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Đề thi được tra cứu phải tồn tại trong DB, nếu không báo lỗi `TOPIC_NOT_FOUND`.
- Nếu đề thi đang ở trạng thái ẩn (chưa bắt đầu round hoặc chưa bốc thăm xong), từ chối cho xem đối với user thường (báo lỗi `FORBIDDEN`).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy đề bài thi đấu.",
  "MessageCode": "TOPIC_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 403 | FORBIDDEN | Đề thi chưa được công bố. |
| 404 | TOPIC_NOT_FOUND | Đề thi không tồn tại trong hệ thống. |
| 500 | INTERNAL_SERVER_ERROR | Gặp sự cố không mong muốn tại server. |
