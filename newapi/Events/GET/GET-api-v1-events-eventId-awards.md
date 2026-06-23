# Xem danh sách giải thưởng (Get Event Awards)

## Tác dụng
Xem danh sách cơ cấu giải thưởng của một event thi đấu (hạng mục giải, số lượng giải, giá trị giải thưởng).

## URL
`GET /api/v1/events/{eventId}/awards`

## Quyền
Public API (Không yêu cầu đăng nhập)

## Request Parameters
*   **Path Parameters:**
    *   `eventId` (Guid, Bắt buộc): ID của event.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa danh sách giải thưởng.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z",
  "Value": [
    {
      "id": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
      "name": "Giải Nhất",
      "description": "Đội thi xuất sắc nhất toàn giải.",
      "levelAward": 1,
      "numberOfAward": 1,
      "prize": 10000000
    }
  ]
}
```

## Business rules
- Event phải tồn tại trong DB, không bị soft-disable.
- Trả ra danh sách các giải thưởng chưa bị disable, sắp xếp theo `LevelAward` tăng dần (1: Nhất, 2: Nhì, 3: Ba, 4: Khuyến khích, v.v.).
- Public: không yêu cầu xác thực.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse`:*

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
| 404 | EVENT_NOT_FOUND | Event không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống phát sinh. |
