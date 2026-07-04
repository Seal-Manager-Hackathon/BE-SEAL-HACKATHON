# GET /api/v1/judge/scores/me

**Role:** Judge (Lecturer)
**Policy:** LecturerPolicy

## Mô tả

Lấy tất cả điểm số mà Judge hiện tại đã chấm, hỗ trợ phân trang và lọc theo event/track/trạng thái graded.

## Request

### Parameters

| Tên | Kiểu | Vị trí | Bắt buộc | Mô tả |
|-----|------|--------|----------|-------|
| `eventId` | `guid` | Query | Có | ID của event |
| `trackId` | `guid` | Query | Không | Lọc theo track |
| `isGraded` | `bool` | Query | Không | Lọc theo trạng thái đã chấm |
| `Page` | `int` | Query | Không | Số trang |
| `PageSize` | `int` | Query | Không | Kích thước trang |

### Body

Không có.

## Response

### Status codes

| Code | Mô tả |
|------|-------|
| 200 | Thành công |
| 401 | Unauthorized |
| 403 | Forbidden |

### Body (BasePaginationResponse)

```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-07-04T08:00:00Z",
  "data": [
    {
      "scoreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
      "teamName": "Team Alpha",
      "trackName": "Bảng A",
      "totalScore": 85.5,
      "isFinalize": true
    }
  ],
  "totalCount": 30,
  "page": 1,
  "pageSize": 10,
  "totalPages": 3
}
```

## Logic

- Gọi `_judgeService.GetMyScores()` để lấy tất cả điểm Judge đã chấm.
- Hỗ trợ lọc theo `eventId` (bắt buộc), `trackId`, `isGraded`.

→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-scores-me-get.md)
