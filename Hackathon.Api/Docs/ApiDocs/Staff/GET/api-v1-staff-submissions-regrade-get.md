# Staff/Admin xem danh sách bài nộp regrade

## Tác dụng
Staff/Admin xem danh sách submission đã được duyệt phúc khảo (`Submissions.IsRegrade = true`), kèm tiến độ judge cũ chấm lại điểm. Flow này không có bước assign judge riêng: judge được chấm lại là judge đã chấm score gốc.

## URL
`GET /api/v1/staff/submissions/regrade`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Query parameters
| Tên | Kiểu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `eventId` | `guid` | Không | Lọc theo event. |
| `trackId` | `guid` | Không | Lọc theo track. |
| `regradeStatus` | `string` | Không | `All`, `PendingRegrade`, `PartiallyRegraded`, `RegradeCompleted`. Mặc định `All`. |
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
        "roundNo": 1,
        "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
        "teamName": "Chiến binh công nghệ",
        "trackId": "4b5c6d7e-8f9a-0b1c-2d3e-4f5a6b7c8d9e",
        "trackTitle": "Web Development",
        "eventId": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
        "eventName": "SEAL Hackathon 2026",
        "reportId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "reportTitle": "Yêu cầu phúc khảo bài nộp Vòng loại",
        "regradeStatus": "PartiallyRegraded",
        "approvedAt": "2026-06-24T09:00:00Z",
        "sourceScores": [
          {
            "scoreId": "8a9b0c1d-2e3f-4a5b-6c7d-8e9f0a1b2c3d",
            "judgeId": "7a8b9c0d-1e2f-3a4b-5c6d-7e8f9a0b1c2d",
            "judgeName": "Nguyễn Văn A",
            "totalScore": 75.0,
            "hasRegraded": true,
            "regradeScoreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "regradeTotalScore": 88.0,
            "regradedAt": "2026-06-26T14:00:00Z"
          },
          {
            "scoreId": "9b0c1d2e-3f4a-5b6c-7d8e-9f0a1b2c3d4e",
            "judgeId": "8b9c0d1e-2f3a-4b5c-6d7e-8f9a0b1c2d3e",
            "judgeName": "Trần Thị B",
            "totalScore": 80.0,
            "hasRegraded": false,
            "regradeScoreId": null,
            "regradeTotalScore": null,
            "regradedAt": null
          }
        ]
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 5,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

## Regrade status
| Giá trị | Điều kiện |
|---|---|
| `PendingRegrade` | Submission đã duyệt regrade nhưng chưa có score phúc khảo nào (`RetakeFromScoreId` chưa xuất hiện). |
| `PartiallyRegraded` | Một số score gốc đã có score phúc khảo, nhưng chưa đủ toàn bộ score gốc. |
| `RegradeCompleted` | Tất cả score gốc active của submission đều đã có score phúc khảo active. |

## Business rules
- Chỉ hiển thị submission có `Submissions.IsRegrade = true` và report liên kết đang `Approved`.
- Staff chỉ xem được submission thuộc event mình được phân công quản lý; Admin không cần kiểm tra phân công.
- Score gốc được tính là `Scores.IsRetake = false`, `Scores.IsMock = false`, `IsDisable = false`.
- Score phúc khảo được liên kết với score gốc bằng `Scores.RetakeFromScoreId`.
- Mỗi score gốc chỉ có tối đa một score phúc khảo active.
- Submission, report và score bị soft-delete không được trả về.
- Sắp xếp mặc định theo thời điểm duyệt report giảm dần.

## Errors
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | QUERY_PARAMETER_INVALID |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | FORBIDDEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
