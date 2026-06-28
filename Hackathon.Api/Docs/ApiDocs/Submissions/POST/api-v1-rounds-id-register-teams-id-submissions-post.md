# Submit Round Project

## Tác dụng
Nộp dự án/bài làm của đội thi tại một vòng đấu chỉ định.

## URL
`POST /api/v1/submissions/rounds/{roundId}/register-teams/{registerTeamId}`

## Quyền
Student là Leader của team (Yêu cầu đăng nhập tài khoản sinh viên).

## Request Headers
- `Authorization: Bearer <AccessToken>`

## Request Parameters
*   **Path Parameters:**
    *   `roundId` (Guid, Bắt buộc): ID vòng thi nộp bài.
    *   `registerTeamId` (Guid, Bắt buộc): ID đơn đăng ký của đội thi.

## Request Body (JSON)
*Cấu trúc Request:*
```json
{
  "url": "https://github.com/myteam/project-repo",
  "description": "Mô tả dự án nộp bài vòng Idea"
}
```

*Các thuộc tính:*
- `url` (string, Bắt buộc): Đường dẫn tài liệu/mã nguồn nộp bài (phải đúng định dạng URL).
- `description` (string, Không bắt buộc): Mô tả ngắn gọn về bài nộp.

## Response body (Success - 200 OK)
*Cấu trúc trả về dạng `BaseResponse`:*
```json
{
  "isSuccess": true,
  "isFailed": false,
  "status": 200,
  "error": null,
  "traceId": "0HN1A2B3C4D5E",
  "timestampUtc": "2026-06-22T08:00:00Z",
  "message": "SUBMISSION_CREATED_SUCCESSFULLY",
  "data": {
    "submissionId": "f9b8c7d6-e5a4-3210-9c0d-1e2f3a4b5c6d",
    "teamId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "submittedAt": "2026-06-22T08:00:00Z",
    "status": "Submitted",
    "isSuccess": true
  }
}
```

## Business rules
- Yêu cầu đăng nhập tài khoản `Student`.
- Chỉ cho phép **Trưởng nhóm (Leader)** thực hiện nộp bài. Kiểm tra bằng cách tìm `TeamDetails` tương ứng với `UserId == currentUserId`, `IsLeader = true`, `Status = Active` và chưa bị disable.
- Vòng thi `roundId` và đội thi đăng ký `registerTeamId` phải khớp nhau trên cùng sự kiện (`Round.EventId == RegisterTeam.EventId`). Nếu không khớp hoặc không tìm thấy, trả lỗi `404 NotFound` (`ROUND_NOT_FOUND` hoặc `REGISTER_TEAM_NOT_FOUND`).
- Kiểm tra thời gian nộp bài: chỉ cho phép nộp trong khoảng thời gian diễn ra vòng đấu (`Round.StartSubmission <= now <= Round.EndSubmission`). Nếu ngoài khoảng thời gian này, trả lỗi `400 BadRequest` (`ROUND_SUBMISSION_CLOSED`).
- Nếu chưa có `RoundDetails` liên kết `roundId` và `registerTeamId` này, hệ thống sẽ tự động khởi tạo mới.
- Tạo mới bài nộp `Submissions` liên kết với `RoundDetails` này với trạng thái mặc định là `Submitted` và `SubmittedAt` bằng thời gian hiện tại.
- Bọc toàn bộ quá trình cập nhật vào database transaction.

## Lỗi có thể xảy ra
*Khi gặp lỗi, API trả về cấu trúc lỗi chuẩn `ErrorResponse` từ Middleware:*

| HTTP | messageCode | message/detail |
|---:|---|---|
| 400 | BAD_REQUEST | URL_REQUIRED |
| 400 | BAD_REQUEST | INVALID_URL_FORMAT |
| 400 | BAD_REQUEST | ROUND_SUBMISSION_CLOSED |
| 401 | MISSING_ACCESS_TOKEN | ACCESS_TOKEN_IS_MISSING |
| 401 | UNAUTHORIZED | INVALID_ACCESS_TOKEN |
| 403 | FORBIDDEN | ONLY_TEAM_LEADER_CAN_SUBMIT |
| 404 | NOT_FOUND | REGISTER_TEAM_NOT_FOUND |
| 404 | NOT_FOUND | ROUND_NOT_FOUND |
| 500 | INTERNAL_SERVER_ERROR | AN_UNEXPECTED_ERROR_OCCURRED |
