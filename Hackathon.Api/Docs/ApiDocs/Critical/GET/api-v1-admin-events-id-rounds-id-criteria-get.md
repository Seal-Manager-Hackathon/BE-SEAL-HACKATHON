# Admin lấy danh sách tiêu chí chấm điểm của vòng thi (Admin Get Criteria Templates)

## Tác dụng
Admin lấy tất cả bộ tiêu chí (CriteriaTemplate) của một vòng thi, bao gồm cả template đang active (`IsDisable = false`) và inactive (`IsDisable = true`). Mỗi template kèm danh sách các tiêu chí chi tiết (CriteriaItems).

## URL
`GET /api/v1/admin/events/{eventId}/rounds/{roundId}/criteria`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `eventId` | `guid` | Có | ID của sự kiện. |
| `roundId` | `guid` | Có | ID của vòng thi. |

## Query parameters
Không có.

## Request body
Không có.

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
      "title": "UI/UX Evaluation",
      "description": "Đánh giá về giao diện và trải nghiệm người dùng",
      "isDisable": true,
      "createdAt": "2026-06-19T08:00:00+00:00",
      "items": [
        {
          "id": "b2c3d4e5-f6a7-8b9c-0d1e-2f3a4b5c6d7e",
          "name": "Thiết kế giao diện",
          "description": "Màu sắc, bố cục hài hòa",
          "score": 50,
          "isDisable": false,
          "createdAt": "2026-06-19T08:05:00+00:00"
        }
      ]
    },
    {
      "id": "c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f",
      "title": "Technical Evaluation",
      "description": "Đánh giá kỹ thuật",
      "isDisable": true,
      "createdAt": "2026-06-20T10:00:00+00:00",
      "items": [
        {
          "id": "d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a",
          "name": "Code quality",
          "description": "Chất lượng code",
          "score": 100,
          "isDisable": false,
          "createdAt": "2026-06-20T10:05:00+00:00"
        }
      ]
    }
  ],
  "message": "SUCCESS"
}
```

## Business rules
- Event phải tồn tại, không bị soft-disable.
- Round phải thuộc event, không bị soft-disable.
- Trả về danh sách tất cả template của round, sắp xếp theo `CreatedAt` giảm dần (mới nhất trước).
- Template có `isDisable = true` là template đang được gắn vào round.
- Template có `isDisable = false` là template đã tạo nhưng chưa được gắn.
- Khác với API GET public (`/api/v1/rounds/{roundId}/criteria`) — API đó chỉ trả 1 template đang được gắn (`isDisable = true`).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND / ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement trong `Hackathon.Api.Controllers.CriticalController`.
- Route: `GET /api/v1/admin/events/{eventId}/rounds/{roundId}/criteria`.
- Sử dụng policy `AdminPolicy`.
