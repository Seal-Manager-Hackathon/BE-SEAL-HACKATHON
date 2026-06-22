# API 59: Lấy toàn bộ tiêu chí của sự kiện (Event Criteria)

## Tác dụng
Lấy danh sách toàn bộ tiêu chí chấm điểm (rubrics) của tất cả các vòng thi nằm trong phạm vi sự kiện.

## URL
`GET /api/v1/events/{eventId}/criteria`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của sự kiện.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa mảng thông tin criteria của từng round.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": [
    {
      "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
      "roundName": "Vòng loại",
      "templateId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
      "templateTitle": "Tiêu chí chấm Vòng 1",
      "criteriaItems": [
        {
          "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
          "name": "Tính thực tiễn",
          "description": "Khả năng ứng dụng giải pháp.",
          "maxScore": 30
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
- Event phải tồn tại và chưa bị soft-disable.
- Trả về danh sách tất cả các vòng thi đang hoạt động của event, mỗi vòng đi kèm với mẫu tiêu chí chấm điểm (Criteria Template) và các tiêu chí chi tiết (Criteria Items) của vòng đó.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy sự kiện.",
  "MessageCode": "EVENT_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | EVENT_NOT_FOUND | Event không tồn tại hoặc bị disable. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
