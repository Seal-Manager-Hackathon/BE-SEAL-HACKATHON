# Judge xem danh sách bài thi cần chấm lại

## Tác dụng
Giúp Judge xem các bài phúc khảo cần chấm lại dựa trên score gốc của chính mình. Chỉ judge đã từng chấm score gốc của submission mới được chấm lại.

## URL
`GET /api/v1/judge/submissions/regrade`

## Authorization
Yêu cầu access token hợp lệ với role `Lecturer` và đã được phân công vai trò `Judge`.

## Query parameters
| Tên | Kiểu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Không | Lọc theo event. |
| `trackId` | `guid` | Không | Lọc theo track cụ thể. |
| `isRegraded` | `bool` | Không | `false`: score gốc chưa có score phúc khảo (mặc định), `true`: đã chấm lại. |
| `pageIndex` | `int` | Không | Trang hiện tại (mặc định `1`). |
| `pageSize` | `int` | Không | Số item mỗi trang (mặc định `10`, tối đa `100`). |

## Response body (200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-26T14:00:00Z",
  "data": {
    "items": [
      {
        "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
        "roundDetailId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "roundName": "Vòng loại",
        "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "teamName": "Chiến binh công nghệ",
        "eventName": "SEAL Hackathon 2026",
        "trackTitle": "Web Development",
        "url": "https://github.com/seal-hackathon/team-project",
        "description": "Bài thi vòng loại",
        "reportId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "reportTitle": "Yêu cầu phúc khảo bài nộp Vòng loại",
        "sourceScoreId": "8a9b0c1d-2e3f-4a5b-6c7d-8e9f0a1b2c3d",
        "sourceTotalScore": 75.0,
        "isRegraded": false,
        "regradeScoreId": null,
        "regradeTotalScore": null,
        "approvedAt": "2026-06-24T09:00:00Z"
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 3,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

## Business rules
- Judge chỉ thấy score gốc do chính mình tạo (`Scores.AssignTrack.AssignEvent.UserId` khớp user hiện tại).
- Score gốc phải active, không mock, không phải retake (`IsDisable = false`, `IsMock = false`, `IsRetake = false`).
- Submission phải có `Submissions.IsRegrade = true`.
- Report liên kết phải ở trạng thái `Approved`.
- `isRegraded = false`: chưa tồn tại score active có `RetakeFromScoreId = sourceScoreId`.
- `isRegraded = true`: đã tồn tại score active có `RetakeFromScoreId = sourceScoreId`.
- Nếu Judge không có score gốc nào cần regrade, trả danh sách rỗng.

## Errors
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | QUERY_PARAMETER_INVALID |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
