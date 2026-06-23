# API 58: Lấy tiêu chí chấm của vòng thi (Get Round Criteria)

## Tác dụng
Lấy danh sách các tiêu chí chấm điểm (rubrics) chi tiết thiết lập cho một vòng thi đấu cụ thể.

## URL
`GET /api/v1/rounds/{roundId}/criteria`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Path Parameters:**
    *   `roundId` (Guid, Bắt buộc): ID của vòng thi đấu cần xem criteria.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa thông tin template và các criteria items.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "templateId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "templateTitle": "Tiêu chí chấm Vòng 1",
    "templateDescription": "Bộ khung đánh giá sản phẩm kỹ thuật.",
    "criteriaItems": [
      {
        "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "name": "Tính thực tiễn",
        "description": "Mức độ khả thi của giải pháp trong thực tế.",
        "maxScore": 30
      },
      {
        "id": "f1f2a3b4-c5d6-e7f8-a9b0-c5d6e7f8a9b1",
        "name": "Độ hoàn thiện kỹ thuật",
        "description": "Chất lượng code, kiến trúc hệ thống và tính bảo mật.",
        "maxScore": 70
      }
    ]
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Vòng thi đấu phải tồn tại trong hệ thống, không bị soft-disable.
- Trả ra template tiêu chí của vòng đấu và mảng tiêu chí chi tiết cùng với điểm số tối đa tương ứng.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy vòng thi.",
  "MessageCode": "ROUND_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | ROUND_NOT_FOUND | Vòng thi không tồn tại trong DB. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
