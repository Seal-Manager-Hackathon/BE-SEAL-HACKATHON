# Judge xem điểm đã chấm (Judge Get My Score)

## Tác dụng
Giúp Judge xem lại bảng điểm mình đã chấm cho một bài thi cụ thể.

## URL
`GET /api/v1/judge/submissions/{submissionId}/scores/me`

## Authorization
Yêu cầu access token hợp lệ của tài khoản Giảng viên đã chấm bài thi.

## Path parameters
| Tên | Kiểu dữ liệu | Bắt buộc | Mô tả |
|---|---|---:|---|
| `submissionId` | `guid` | Có | ID của bài nộp. |

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa chi tiết bảng điểm.*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "SUCCESS",
  "data": {
    "scoreId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
    "assignTrackId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "totalScore": 85.5,
    "isRetake": false,
    "isMock": false,
    "scoreItems": [
      {
        "criteriaItemId": "e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0",
        "criteriaItemName": "Tính thực tiễn",
        "score": 25.5,
        "comment": "Ý tưởng tốt."
      }
    ]
  }
}
```

## Business rules
- Bài thi phải tồn tại trong DB.
- Trích xuất thông tin điểm số trong bảng `Scores` và `ScoreItems` của giám khảo gọi API liên kết với `submissionId`.
- **Empty state**: Nếu chưa chấm, trả về `data: null`:
  ```json
  {
    "isSuccess": true,
    "isFailed": false,
    "status": 200,
    "error": null,
    "traceId": "0HN1A2B3C4D5E",
    "timestampUtc": "2026-06-22T08:00:00Z",
    "message": "SUCCESS",
    "data": null
  }
  ```

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

```json
{
  "title": "Unauthorized",
  "status": 401,
  "message": "Vui lòng xác thực tài khoản.",
  "messageCode": "UNAUTHORIZED",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không được phân công chấm bảng đấu chứa bài thi. |
| 404 | SUBMISSION_NOT_FOUND | Bài nộp thi không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi hệ thống phát sinh. |

## Trạng thái implement
- ✅ Đã implement trong `Hackathon.Api.Controllers.JudgeController`.
- Route: `GET /api/v1/judge/submissions/{submissionId}/scores/me`.
- Sử dụng policy `LecturerPolicy`.
