# Judge chấm điểm phúc khảo

## Tác dụng
Judge tạo một bảng điểm phúc khảo riêng biệt từ bảng điểm gốc của chính mình. Bản ghi mới có `IsRetake = true` và `RetakeFromScoreId` trỏ về score gốc; score gốc được giữ nguyên.

## URL
`POST /api/v1/judge/scores/{scoreId}/retake`

## Authorization
Yêu cầu access token hợp lệ của tài khoản Giảng viên sở hữu bảng điểm gốc.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `scoreId` | `guid` | Có | ID của score gốc cần chấm lại. |

## Request body
```json
{
  "totalScore": 88.0,
  "scores": [
    {
      "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
      "score": 28.0,
      "comment": "Chấm lại: Đã xem xét kỹ khiếu nại của thí sinh."
    }
  ]
}
```

## Response body (200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-26T14:00:00Z",
  "message": "REGRADE_SCORE_SUBMITTED",
  "data": {
    "scoreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "assignTrackId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "retakeFromScoreId": "8a9b0c1d-2e3f-4a5b-6c7d-8e9f0a1b2c3d",
    "totalScore": 88.0,
    "isRetake": true,
    "isMock": false,
    "scoreItems": [
      {
        "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "criteriaItemName": "Tính thực tiễn",
        "score": 28.0,
        "comment": "Chấm lại: Đã xem xét kỹ khiếu nại của thí sinh."
      }
    ]
  }
}
```

## Business rules
- `scoreId` trong path là ID của score gốc; API tạo score mới, không ghi đè score gốc.
- Score gốc phải tồn tại, active và thuộc sở hữu của Judge hiện tại.
- Score gốc không được là mock (`IsMock = false`) và không được là retake (`IsRetake = false`).
- Submission của score gốc phải có `Submissions.IsRegrade = true`.
- Report phúc khảo liên kết với submission phải ở trạng thái `Approved`.
- Chỉ cho tạo một score phúc khảo active cho cùng score gốc (`RetakeFromScoreId = scoreId`).
- Score mới dùng cùng `SubmissionId` và `AssignTrackId` với score gốc.
- Score mới có `IsRetake = true`, `IsMock = false`, `RetakeFromScoreId = scoreId`.
- Server validate criteria thuộc round của submission, điểm không vượt `maxScore`, và tổng điểm chi tiết khớp `totalScore`.

## Errors
| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | MOCK_SCORE_CANNOT_BE_RETAKEN |
| 400 | BAD_REQUEST | RETAKE_SCORE_CANNOT_BE_RETAKEN |
| 400 | BAD_REQUEST | SUBMISSION_NOT_IN_REGRADE |
| 400 | BAD_REQUEST | REPORT_NOT_APPROVED |
| 400 | BAD_REQUEST | SCORE_LIMIT_EXCEEDED |
| 400 | BAD_REQUEST | SCORE_TOTAL_MISMATCH |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | SCORE_NOT_OWNED_BY_JUDGE |
| 404 | NOT_FOUND | SCORE_NOT_FOUND |
| 409 | CONFLICT | SCORE_ALREADY_RETAKEN |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
