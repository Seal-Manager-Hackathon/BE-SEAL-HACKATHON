# Lấy tiêu chí chấm điểm của vòng thi

## Tác dụng
Lấy thông tin mẫu tiêu chí (Criteria Template) duy nhất và chi tiết các tiêu chí chấm điểm (Criteria Items) đi kèm số điểm (Score) trong một vòng thi cụ thể.

## URL
`GET /api/v1/rounds/{roundId}/criteria`

## Authorization
Không yêu cầu Access Token (Public).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `roundId` | `guid` | Có | ID của vòng thi. |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/rounds/8f3b2553-933e-4861-a577-ab6453664d41/criteria
```

## Request body
Không có.

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "00-84a1e9df64619d8...",
  "timestampUtc": "2026-06-19T10:00:00.0000000Z",
  "data": {
    "roundId": "8f3b2553-933e-4861-a577-ab6453664d41",
    "eventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "roundName": "Vòng Sơ loại",
    "template": {
      "id": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
      "title": "Giao diện và trải nghiệm người dùng",
      "description": "Đánh giá về UI/UX",
      "isDisable": false,
      "createdAt": "2026-06-19T08:00:00+00:00",
      "items": [
        {
          "id": "b2c3d4e5-f6a7-8b9c-0d1e-2f3a4b5c6d7e",
          "name": "Màu sắc hài hòa",
          "description": "Màu sắc phù hợp với chủ đề",
          "score": 10.5,
          "isDisable": false,
          "createdAt": "2026-06-19T08:05:00+00:00"
        },
        {
          "id": "c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f",
          "name": "Dễ sử dụng",
          "description": "Người dùng dễ dàng thao tác",
          "score": 20.0,
          "isDisable": false,
          "createdAt": "2026-06-19T08:10:00+00:00"
        }
      ]
    }
  },
  "message": "SUCCESS"
}
```

## Business rules
- Không yêu cầu đăng nhập.
- `roundId` là bắt buộc.
- Nếu vòng thi (`Round`) hoặc sự kiện chứa vòng thi đó (`Event`) bị soft-disable (`IsDisable == true`) hoặc không tồn tại, trả về `ROUND_NOT_FOUND`.
- Một vòng thi có thể có nhiều template, nhưng chỉ trả về 1 template đang được gắn (`IsDisable == true`). Trả về `null` nếu chưa có template nào được gắn.
- `template` chứa danh sách `items` (CriteriaItems), mỗi item có bao gồm `score` (điểm).
- Chỉ lấy những tiêu chí chưa bị disable (`IsDisable == false`).
- Dữ liệu `items` sắp xếp theo thời gian tạo, sau đó theo tên.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
