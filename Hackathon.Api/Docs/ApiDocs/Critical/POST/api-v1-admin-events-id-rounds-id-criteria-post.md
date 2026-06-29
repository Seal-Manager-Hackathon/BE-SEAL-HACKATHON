# Admin tạo tiêu chí chấm điểm cho vòng thi (Admin Create Criteria)

## Tác dụng
Admin tạo một bộ tiêu chí chấm điểm (CriteriaTemplate) kèm danh sách các tiêu chí chi tiết (CriteriaItems) cho một vòng thi trong sự kiện. Mặc định template được tạo ở trạng thái chưa gắn (`IsDisable = false`) — cần gọi API activate để gắn vào round.

## URL
`POST /api/v1/admin/events/{eventId}/rounds/{roundId}/criteria`

## Authorization
Yêu cầu access token hợp lệ với role `Admin`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `eventId` | `guid` | Có | ID của sự kiện. |
| `roundId` | `guid` | Có | ID của vòng thi. |

## Request body
```json
{
  "title": "UI/UX Evaluation",
  "description": "Đánh giá về giao diện và trải nghiệm người dùng",
  "items": [
    {
      "name": "Thiết kế giao diện",
      "description": "Màu sắc, bố cục hài hòa",
      "score": 50
    },
    {
      "name": "Trải nghiệm người dùng",
      "description": "Dễ sử dụng, thân thiện",
      "score": 50
    }
  ]
}
```

| Field | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `title` | `string` | Có | Tên của bộ tiêu chí. |
| `description` | `string` | Không | Mô tả bộ tiêu chí. |
| `items` | `array` | Có | Danh sách các tiêu chí chi tiết. |
| `items[].name` | `string` | Có | Tên tiêu chí. |
| `items[].description` | `string` | Không | Mô tả tiêu chí. |
| `items[].score` | `decimal` | Có | Điểm tối đa của tiêu chí. |

**Lưu ý FE:** Gửi 1 lần duy nhất gồm thông tin template + tất cả items. Khi admin ấn "+" để thêm item, FE collect tất cả items trên form rồi gửi 1 request.

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 201,
  "traceId": "string|null",
  "timestampUtc": "datetime",
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  },
  "message": "CRITERIA_CREATED_SUCCESSFULLY"
}
```

## Business rules
- Event phải tồn tại, không bị soft-disable.
- Round phải thuộc event, không bị soft-disable.
- `title` là bắt buộc, không được để trống.
- Khi tạo mới, template có `IsDisable = false` (chưa gắn vào round) và items có `IsDisable = false`.
- Một round có thể có nhiều template, nhưng chỉ 1 template được gắn (activate) — tương ứng `IsDisable = true`.
- Sau khi tạo, cần gọi API activate để gắn template vào round.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 400 | BAD_REQUEST | CRITERIA_TITLE_REQUIRED |
| 401 | UNAUTHORIZED | UNAUTHORIZED |
| 403 | FORBIDDEN | FORBIDDEN |
| 404 | NOT_FOUND | EVENT_NOT_FOUND / ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- Đã implement trong `Hackathon.Api.Controllers.CriticalController`.
- Route: `POST /api/v1/admin/events/{eventId}/rounds/{roundId}/criteria`.
- Sử dụng policy `AdminPolicy`.
- Message: `CRITERIA_CREATED_SUCCESSFULLY`.
