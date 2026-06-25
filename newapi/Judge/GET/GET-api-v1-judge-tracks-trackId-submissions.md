# Judge xem danh sách bài thi cần chấm

## Tác dụng
Giúp Judge xem danh sách các bài thi đã nộp của các team thuộc bảng đấu (Track) mình được phân công chấm điểm. Judge chỉ chấm các team thuộc track này, không chấm team ở track khác.

## URL
`GET /api/v1/judge/tracks/{trackId}/submissions`

## Quyền
Judge phụ trách track (Yêu cầu đăng nhập tài khoản Giảng viên được phân công)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `trackId` (Guid, Bắt buộc): ID của bảng đấu cần lấy bài thi.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa danh sách bài nộp.*
```json
{
  "IsSuccess": true,
  "IsFailed": false,
  "Value": [
    {
      "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
      "roundDetailId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "roundId": "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
      "roundName": "Vòng loại",
      "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
      "teamName": "Chiến binh công nghệ",
      "url": "https://github.com/seal-hackathon/team-project-web",
      "description": "Bài thi hoàn thiện.",
      "submittedAt": "2026-06-22T08:00:00Z",
      "isGraded": false
    }
  ],
  "Error": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Giám khảo gọi API phải được phân công chấm bảng đấu này (`AssignTracks` liên kết `TrackId` và `AssignEventId` của Judge). Nếu sai, từ chối xem và báo lỗi `FORBIDDEN`.
- Chỉ trả ra các bài nộp mới nhất của các team thuộc đúng track mà judge được phân công.
- Judge chỉ được xem/chấm submission của team trong track được phân công; submission thuộc track khác phải bị từ chối.
- `isGraded` báo xem giám khảo hiện tại đã cho điểm bài thi này chưa.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "Title": "Forbidden",
  "Status": 403,
  "Detail": "Bạn không được phân công chấm bảng đấu này.",
  "MessageCode": "FORBIDDEN",
  "Errors": null,
  "TraceId": "0HN1A2B3C4D5E",
  "TimestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ hoặc thiếu. |
| 403 | FORBIDDEN | Không có quyền chấm bảng đấu này (check BR-ASG-03). |
| 404 | TRACK_NOT_FOUND | Bảng đấu không tồn tại trong DB. |
| 500 | INTERNAL_SERVER_ERROR | Lỗi máy chủ phát sinh. |
