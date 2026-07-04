# PATCH /api/v1/judge/scores/{scoreId}

**Role:** Judge (Lecturer)
**Policy:** LecturerPolicy

## Mô tả

Cập nhật điểm đã chấm cho một bài nộp (chỉ áp dụng nếu score chưa được finalize).

## Request

### Parameters

| Tên | Kiểu | Vị trí | Bắt buộc | Mô tả |
|-----|------|--------|----------|-------|
| `scoreId` | `guid` | Path | Có | ID của bảng điểm cần cập nhật |

### Body

```json
{
  "totalScore": 88.0,
  "scores": [
    {
      "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
      "score": 28.0,
      "comment": "Cập nhật nhận xét"
    }
  ]
}
```

## Response

### Status codes

| Code | Mô tả |
|------|-------|
| 200 | Thành công |
| 400 | SCORE_LIMIT_EXCEEDED / SCORE_TOTAL_MISMATCH |
| 401 | Unauthorized |
| 403 | Forbidden |
| 404 | SCORE_NOT_FOUND |
| 409 | SCORE_ALREADY_FINALIZED |

### Body

```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-07-04T08:00:00Z",
  "message": "SCORE_UPDATED_SUCCESSFULLY",
  "data": {
    "scoreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "totalScore": 88.0,
    "isRetake": false,
    "isMock": false,
    "scoreItems": [ ... ]
  }
}
```

## Logic

- Kiểm tra scoreId thuộc về Judge hiện tại.
- Kiểm tra score chưa được finalized.
- Cập nhật lại tổng điểm và các score items.
- Validation điểm không vượt quá maxScore của từng tiêu chí.

→ [📄 Doc chi tiết](../../ApiDocs/Judge/PATCH/api-v1-judge-scores-scoreId-patch.md)
