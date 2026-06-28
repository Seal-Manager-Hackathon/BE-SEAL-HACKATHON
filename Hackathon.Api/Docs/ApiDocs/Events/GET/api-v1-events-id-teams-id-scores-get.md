# Xem chi tiết điểm của Team theo từng Round & Tiêu chí (Get Team Score Details)

## Tác dụng
Cho phép xem chi tiết bảng điểm của một team cụ thể qua từng vòng thi (Round) trong sự kiện, bao gồm điểm số chi tiết cho từng tiêu chí chấm điểm (Criteria Items).

## URL
`GET /api/v1/events/{eventId}/teams/{teamId}/scores`

## Authorization
Public (không yêu cầu access token). Trả về điểm chi tiết của team trong event nếu có dữ liệu.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Có | ID của sự kiện. |
| `teamId` | `guid` | Có | ID của đội thi cần xem điểm chi tiết. |

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
      "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
      "roundName": "Vòng loại",
      "roundNo": 1,
      "averageTotalScore": 88.5,
      "criteriaScores": [
        {
          "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
          "criteriaItemName": "Tính thực tiễn",
          "averageCriteriaScore": 27.5,
          "maxScore": 30.0
        },
        {
          "criteriaItemId": "f1f2a3b4-c5d6-e7f8-a9b0-c5d6e7f8a9b1",
          "criteriaItemName": "Độ hoàn thiện kỹ thuật",
          "averageCriteriaScore": 61.0,
          "maxScore": 70.0
        }
      ]
    }
  ],
  "message": "SUCCESS"
}
```

## Business rules
- Event và Team phải tồn tại trong DB và chưa bị soft-disable.
- Điểm trung bình của từng tiêu chí (`averageCriteriaScore`) được tính bằng trung bình cộng điểm số của các giám khảo chấm trên tiêu chí đó (`ScoreItems.Score`).
- Điểm trung bình tổng của round (`averageTotalScore`) bằng trung bình cộng tổng điểm chấm của các giám khảo (`Scores.TotalScore`).

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 404 | NOT_FOUND | EVENT_NOT_FOUND |
| 404 | NOT_FOUND | TEAM_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.EventsController`.
- Route: `GET /api/v1/events/{eventId}/teams/{teamId}/scores`.
- Public endpoint.
- Entity: `Scores` + `ScoreItems` + `CriteriaItems`.
