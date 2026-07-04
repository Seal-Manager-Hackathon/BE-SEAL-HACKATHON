# GET /api/v1/judge/submissions/{submissionId}/criteria

**Role:** Judge (Lecturer)
**Policy:** LecturerPolicy

## Mô tả

Lấy danh sách tiêu chí chấm điểm (rubric) cho một bài nộp cụ thể, giúp Judge thiết lập form nhập điểm.

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
| 200 | Thành công |
| 401 | Unauthorized |
| 403 | Forbidden - Judge không được phân công chấm bài này |
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
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "templateId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "templateTitle": "Rubric Vòng loại",
    "criteriaItems": [
      {
        "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "name": "Tính thực tiễn",
        "description": "Mức độ khả thi của dự án",
        "maxScore": 30
      },
      {
        "id": "d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a",
        "name": "Sáng tạo",
        "description": "Tính mới của giải pháp",
        "maxScore": 40
      }
    ]
  }
}
```

## Logic

- Kiểm tra bài nộp tồn tại.
- Judge phải được phân công chấm bảng đấu chứa bài nộp này.
- Lấy template tiêu chí (rubric) của round tương ứng với bài nộp.
- Trả về danh sách các tiêu chí và điểm tối đa để Judge nhập điểm.

→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-submissions-submissionId-criteria-get.md)
