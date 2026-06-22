# Judge lấy tiêu chí chấm điểm bài thi

## Tác dụng
Giúp Judge lấy danh sách rubrics tiêu chí chi tiết để thiết lập form nhập điểm thi cho một bài làm cụ thể.

## URL
`GET /api/v1/judge/submissions/{submissionId}/criteria`

## Quyền
Judge phụ trách track (Yêu cầu đăng nhập tài khoản Giảng viên được phân công)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `submissionId` (Guid, Bắt buộc): ID của bài nộp cần lấy rubric.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": {
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
    "templateId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "templateTitle": "Rubric Vòng loại",
    "criteriaItems": [
      {
        "id": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "name": "Tính thực tiễn",
        "description": "Mức độ khả thi.",
        "maxScore": 30
      }
    ]
  },
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Bài nộp `submissionId` phải tồn tại trong DB.
- Giám khảo gọi API phải được phân công chấm bảng đấu có team nộp bài thi này.
- Lấy thông tin bộ tiêu chí của round thi đấu tương ứng với bài nộp (`Submissions.RoundDetail.RoundId` -> `CriteriaTemplates` -> `CriteriaItems`).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Not Found",
  "Status": 404,
  "Detail": "Không tìm thấy bài thi.",
  "MessageCode": "SUBMISSION_NOT_FOUND",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không được phân công chấm bảng đấu có bài thi này. |
| 404 | SUBMISSION_NOT_FOUND | Bài nộp thi không tồn tại trong hệ thống. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
