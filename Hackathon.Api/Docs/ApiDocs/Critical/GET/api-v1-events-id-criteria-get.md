# Lấy tiêu chí chấm điểm của toàn bộ sự kiện (Event)

## Tác dụng
Lấy danh sách tất cả các vòng thi (Rounds) trong một sự kiện. Mỗi vòng thi đi kèm với mẫu tiêu chí chấm điểm (Criteria Template) và các tiêu chí chi tiết (Criteria Items) kèm theo điểm số.

## URL
`GET /api/v1/events/{eventId}/criteria`

## Authorization
Không yêu cầu Access Token (Public API).

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---|
| `eventId` | `guid` | Có | ID của sự kiện. |

## Query parameters
Không có.

## Ví dụ request
```http
GET /api/v1/events/3fa85f64-5717-4562-b3fc-2c963f66afa6/criteria
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
  "data": [
    {
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
    {
      "roundId": "b1c2d3e4-f5a6-7b8c-9d0e-1f2a3b4c5d6e",
      "eventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "roundName": "Vòng Chung kết",
      "template": null
    }
  ],
  "message": "SUCCESS"
}
```

## Business rules
- Không yêu cầu đăng nhập.
- `eventId` là bắt buộc.
- Nếu sự kiện (`Event`) bị soft-disable (`IsDisable == true`) hoặc không tồn tại, trả về `EVENT_NOT_FOUND`.
- Trả về danh sách tất cả các vòng thi (`Round`) chưa bị disable của Event, sắp xếp theo `RoundNo` tăng dần.
- Đối với mỗi vòng thi, đính kèm `template` duy nhất tương ứng (nếu có).
- Trong mỗi `template` chứa danh sách `items` (CriteriaItems), mỗi item có bao gồm `score` (điểm).
- Chỉ lấy các `template` và `items` chưa bị disable (`IsDisable == false`).
- Dữ liệu `items` sắp xếp theo thời gian tạo, sau đó theo tên.
- Nếu một vòng thi chưa được tạo mẫu tiêu chí, trường `template` của vòng đó sẽ mang giá trị `null`.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---|---|---|
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
