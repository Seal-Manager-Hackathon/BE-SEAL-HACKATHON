# GET /api/v1/judge/submissions/{submissionId}/scores/me

**Role:** Judge (Lecturer)
**Policy:** LecturerPolicy

## Mô tả

Lấy điểm số mà Judge hiện tại đã chấm cho một bài nộp cụ thể.

## Request

### Parameters

| Tên | Kiểu | Vị trí | Bắt buộc | Mô tả |
|-----|------|--------|----------|-------|
| `submissionId` | `guid` | Path | Có | ID của bài nộp |

### Body

Không có.

## Response

### Status codes

| Code | Mô tả |
|------|-------|
| 200 | Thành công (data có thể null nếu chưa chấm) |
| 401 | Unauthorized |
| 403 | Forbidden |
| 404 | Not Found - Không tìm thấy bài nộp |

### Body

```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-07-04T08:00:00Z",
  "message": "SUCCESS",
  "data": {
    "scoreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "totalScore": 85.5,
    "isRetake": false,
    "isMock": false,
    "isFinalize": true,
    "scoreItems": [
      {
        "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "criteriaItemName": "Tính thực tiễn",
        "score": 25.5,
        "comment": "Ý tưởng tốt"
      }
    ]
  }
}
```

## Logic

- Gọi `_judgeService.GetMySubmissionScore(submissionId)` để lấy điểm của Judge hiện tại cho bài nộp.
- Nếu Judge chưa chấm, trả về `data: null` nhưng status vẫn 200.

→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-submissions-submissionId-scores-me-get.md)
