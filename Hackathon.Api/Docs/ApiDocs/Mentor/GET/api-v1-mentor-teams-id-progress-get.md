# Mentor xem tiến độ bài làm của Team

## Tác dụng
Giúp Mentor xem thông tin chi tiết của team thuộc track mình được phân công, bao gồm track/topic và tiến độ bài nộp (Submissions). Mentor chỉ có quyền xem để quản lý/hỗ trợ theo track, không chấm điểm và không trao đổi hai chiều với team qua API này.

## URL
`GET /api/v1/mentor/teams/{teamId}/progress`

## Quyền
Mentor phụ trách team (Yêu cầu đăng nhập tài khoản Giảng viên được phân công)

## Request Headers
- \`Authorization: Bearer <AccessToken>\`

## Request Parameters
*   **Path Parameters:**
    *   `teamId` (Guid, Bắt buộc): ID của team cần xem tiến độ.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse` chứa thông tin tiến trình và bài làm nộp.*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "Value": {
    "teamId": "c4b5a6d7-e8f9-0a1b-2c3d-4e5f6a7b8c9d",
    "teamName": "Chiến binh công nghệ",
    "trackTitle": "Bảng A - Web Application",
    "topicTitle": "Hệ thống quản lý y tế thông minh",
    "submissions": [
      {
        "submissionId": "f7b6d5c4-129b-4e6f-adbd-2c5ea56789ff",
        "roundName": "Vòng loại",
        "roundNo": 1,
        "url": "https://github.com/seal-hackathon/team-project-web",
        "description": "Bài thi hoàn chỉnh.",
        "submittedAt": "2026-06-22T08:00:00Z"
      }
    ]
  },
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

## Business rules
- Team phải tồn tại trong DB và đang hoạt động.
- Mentor gọi API phải được phân công phụ trách bảng đấu của team thi đấu này (`RegisterTeams.TrackId` trùng khớp với `TrackId` được phân công của Mentor, check BR-MEN-01).
- Mentor chỉ xem thông tin team/chi tiết team và tiến độ bài làm trong track mình được gán; không có quyền chấm điểm hoặc quản lý team ngoài track đó.
- Trích xuất toàn bộ bài thi đã nộp của team qua từng vòng thi đấu (`Submissions` liên kết `RoundDetails`).

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn \`ErrorResponse\`:*

```json
{
  "title": "Forbidden",
  "status": 403,
  "Detail": "Đội thi này không thuộc bảng đấu do bạn hướng dẫn.",
  "messageCode": "FORBIDDEN",
  "errors": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z"
}
```

### Các mã lỗi cụ thể:
| HTTP | messageCode | message/detail |
|---:|---|---|
| 401 | UNAUTHORIZED | Access token không hợp lệ. |
| 403 | FORBIDDEN | Không được phân công hướng dẫn đội thi này. |
| 404 | TEAM_NOT_FOUND | Team không tồn tại. |
| 500 | INTERNAL_SERVER_ERROR | Gặp lỗi hệ thống. |
