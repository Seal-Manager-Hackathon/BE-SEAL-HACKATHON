# Xem danh sách giải thưởng (Get Event Awards)

## Tác dụng
Xem danh sách cơ cấu giải thưởng của một event thi đấu (hạng mục giải, số lượng giải, giá trị giải thưởng).

## URL
`GET /api/v1/events/{eventId}/awards`

## Authorization
Public (không yêu cầu access token).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | ID của event. |

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": [
    {
      "id": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
      "name": "Giải Nhất",
      "description": "Đội thi xuất sắc nhất toàn giải.",
      "levelAward": "First",
      "numberOfAward": 1,
      "prize": 10000000
    }
  ],
  "message": "SUCCESS"
}
```

## Business rules
- Event phải tồn tại trong DB, không bị soft-disable.
- Trả ra danh sách các giải thưởng chưa bị disable, sắp xếp theo `LevelAward` tăng dần.
- Public: không yêu cầu xác thực.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.EventsController`.
- Route: `GET /api/v1/events/{eventId}/awards`.
- Public endpoint.
- Entity: `Awards`.
