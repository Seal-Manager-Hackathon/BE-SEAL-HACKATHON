# Judge xem danh sách bài nộp trong event (Judge Event Submissions)

## Tác dụng
Giúp Judge xem danh sách các bài nộp (submissions) của các team thuộc các track được phân công trong một event. Response được nhóm theo **Round → Track → Submissions** và hỗ trợ lọc theo `roundId`, `trackId`, phân trang submissions.

## URL
`GET /api/v1/judge/events/{eventId}/submissions`

## Authorization
Yêu cầu access token hợp lệ với role `Lecturer` và đã được phân công vai trò `Judge` trong event.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `eventId` | `guid` | Có | ID của event. |

## Query Parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---|---:|---|
| `roundId` | `guid` | Không | Lọc theo vòng thi cụ thể. Nếu không truyền, trả về tất cả round hợp lệ trong event. |
| `trackId` | `guid` | Không | Lọc theo track cụ thể. Nếu không truyền, trả về tất cả track mà judge được phân công. |
| `pageIndex` | `int` | Không | Số trang submissions (mặc định: 1). |
| `pageSize` | `int` | Không | Số phần tử submissions trên trang (mặc định: 10, tối đa: 100). |

## Ví dụ request
```http
GET /api/v1/judge/events/00000000-0000-0000-0000-000000000000/submissions?roundId=2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e&trackId=4b5c6d7e-8f9a-0b1c-2d3e-4f5a6b7c8d9e&pageIndex=1&pageSize=10
Authorization: Bearer {accessToken}
```

## Response body (Success - 200 OK)
*Trả về danh sách rounds, mỗi round chứa danh sách tracks, mỗi track chứa danh sách submissions (phân trang theo submission).*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "message": "SUCCESS",
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": [
    {
      "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
      "roundName": "Vòng loại",
      "tracks": [
        {
          "trackId": "4b5c6d7e-8f9a-0b1c-2d3e-4f5a6b7c8d9e",
          "trackTitle": "Web Development",
          "submissions": {
            "items": [
              {
                "registerTeamId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
                "teamName": "Chiến binh công nghệ",
                "topicId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
                "topicTitle": "Hệ thống quản lý y tế thông minh",
                "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
                "submissionStatus": 0,
                "submittedAt": "2026-06-22T08:00:00Z",
                "scoreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "totalScore": 85.5
              }
            ],
            "pageIndex": 1,
            "pageSize": 10,
            "totalCount": 1,
            "hasNextPage": false,
            "hasPreviousPage": false
          }
        }
      ]
    }
  ]
}
```

## Business rules
- Judge chỉ xem được submissions của các track được phân công.
- Data trả về dạng cây: **Round → Track → Submissions (phân trang)**.
- Chỉ trả về các round thuộc event và đã đóng nộp bài (`EndSubmission` đã qua).
- Chỉ trả về các track mà judge được phân công trong event.
- Nếu truyền `roundId`: chỉ trả về round đó nếu round thuộc event và đã đóng nộp bài.
- Nếu truyền `trackId`: chỉ trả về track đó nếu judge được phân công track này.
- Nếu truyền cả `roundId` và `trackId`: trả về giao của round + track; data vẫn giữ cấu trúc `rounds[].tracks[].submissions`.
- Nếu không truyền `roundId`/`trackId`: trả về tất cả round đã đóng và tất cả track được phân công có submissions hợp lệ.
- Phân trang áp dụng trên danh sách submissions trong từng track.
- `scoreId` / `totalScore` = null nếu judge chưa chấm bài này.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 403 | FORBIDDEN | FORBIDDEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |

## Trạng thái implement
- ✅ Route: `GET /api/v1/judge/events/{eventId:guid}/submissions`.
- Sử dụng policy `LecturerPolicy`.
