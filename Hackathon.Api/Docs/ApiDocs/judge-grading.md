# Judge grading

## Tác dụng
Giám khảo nhập điểm, gửi nhận xét và chốt điểm cho bài nộp của đội thi.

## URL
`POST /api/judge/submissions/{submissionId}/grades`

## Authorization
Yêu cầu access token hợp lệ và role `Lecturer` với event role `Judge`.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `submissionId` | `guid` | Có | Id bài nộp cần chấm điểm. |

## Request body
```json
{
  "comment": "string|null",
  "scoreItems": [
    {
      "criteriaItemId": "guid",
      "score": 8.5,
      "comment": "string|null"
    }
  ]
}
```

## Response body
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "traceId": "string",
  "timestampUtc": "datetime",
  "value": {
    "scoreId": "guid",
    "submissionId": "guid",
    "assignTrackId": "guid",
    "totalScore": 8.5,
    "comment": "string|null",
    "status": "string|null",
    "scoreItems": [
      {
        "criteriaItemId": "guid",
        "score": 8.5,
        "comment": "string|null"
      }
    ],
    "message": "SUBMISSION_GRADED_SUCCESSFULLY"
  }
}
```

## Business rules
- Request phải có access token hợp lệ.
- User hiện tại phải là Judge được assign vào track chứa submission.
- Submission phải tồn tại và chưa bị soft-disable.
- Criteria item phải thuộc criteria template của round tương ứng.
- Điểm từng criteria item phải nằm trong khoảng hợp lệ theo cấu hình criteria.
- Một judge chỉ được chấm một score chính cho một submission theo assign track của mình, trừ khi business rule cho phép cập nhật/chấm lại.
- `Scores.TotalScore` là tổng hoặc trung bình theo rule service chấm điểm.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | MISSING_ACCESS_TOKEN | Access token is missing. |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | JUDGE_REQUIRED |
| 403 | FORBIDDEN | JUDGE_NOT_ASSIGNED_TO_SUBMISSION |
| 404 | NOT_FOUND | SUBMISSION_NOT_FOUND |
| 404 | NOT_FOUND | CRITERIA_ITEM_NOT_FOUND |
| 400 | BAD_REQUEST | SCORE_ITEMS_REQUIRED |
| 400 | BAD_REQUEST | SCORE_OUT_OF_RANGE |
| 409 | CONFLICT | SUBMISSION_ALREADY_GRADED |
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
