# POST /api/v1/judge/scores/{scoreId}/retake

**Role:** Judge (Lecturer)
**Policy:** LecturerPolicy

## Mô tả

Judge nhập điểm lại (regrade/retake) cho một bài nộp đã được yêu cầu chấm lại (appeal). Tạo bản ghi điểm mới thay thế điểm cũ.

## Request

### Parameters

| Tên | Kiểu | Vị trí | Bắt buộc | Mô tả |
|-----|------|--------|----------|-------|
| `scoreId` | `guid` | Path | Có | ID của bảng điểm cũ muốn chấm lại |

### Body

```json
{
  "totalScore": 90.0,
  "scores": [
    {
      "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
      "score": 30.0,
      "comment": "Đã xem xét lại, điểm hợp lý"
    }
  ]
}
```

## Response

### Status codes

| Code | Mô tả |
|------|-------|
| 200 | Thành công |
| 401 | Unauthorized |
| 403 | Forbidden |
| 404 | SCORE_NOT_FOUND |

### Body

```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-07-04T08:00:00Z",
  "message": "REGRADE_SCORE_SUBMITTED",
  "data": {
    "scoreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "totalScore": 90.0,
    "isRetake": true,
    "isMock": false,
    "scoreItems": [ ... ]
  }
}
```

## Logic

- Gọi `_judgeService.SubmitRetakeScore()`.
- Tạo bản ghi điểm mới với `isRetake = true`.
- Đánh dấu bài nộp đã được regrade.

→ [📄 Doc chi tiết](../../ApiDocs/Judge/POST/api-v1-judge-scores-scoreId-retake-post.md)
