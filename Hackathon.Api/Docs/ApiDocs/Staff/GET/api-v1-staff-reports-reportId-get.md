# Staff/Admin get report detail

## Tác dụng
Staff/Admin xem chi tiết một khiếu nại, bao gồm thông tin bài nộp, người gửi, file đính kèm và trạng thái xử lý.

## URL
`GET /api/v1/staff/reports/{reportId}`

## Authorization
Yêu cầu access token hợp lệ với role `Staff` hoặc `Admin`.

## Path parameters
| Tên | Kiểu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `reportId` | `guid` | Có | ID của báo cáo/khiếu nại. |

## Response body (200 OK)
```json
{
  "isSuccess": true,
  "isFailed": false,
  "error": null,
  "status": 200,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "data": {
    "reportId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "userId": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
    "userName": "Nguyễn Văn A",
    "assignEventId": "b1a7d6c2-4821-4f9b-bd5e-3c2fa56789e0",
    "eventName": "SEAL Hackathon 2026",
    "teamId": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
    "teamName": "Chiến binh công nghệ",
    "roundId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "roundNo": 1,
    "title": "Yêu cầu phúc khảo bài nộp Vòng loại",
    "description": "Team muốn BTC xem lại điểm tiêu chí kỹ thuật.",
    "imgUrl": "https://example.com/evidence.jpg",
    "fileUrl": "https://example.com/evidence.pdf",
    "typeReport": "Phúc khảo",
    "status": 0,
    "statusName": "Open",
    "reason": null,
    "isRegrade": false,
    "createdAt": "2026-06-22T08:00:00Z",
    "updatedAt": "2026-06-22T08:00:00Z"
  }
}
```

## Business rules
- Report phải tồn tại trong DB, không bị soft-delete.
- Staff chỉ xem được report thuộc event mình được phân công; report ngoài phạm vi được xử lý như không tìm thấy.

## Errors
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 404 | NOT_FOUND | REPORT_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
