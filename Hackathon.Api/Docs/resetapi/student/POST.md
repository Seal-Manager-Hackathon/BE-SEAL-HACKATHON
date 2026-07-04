# POST - Student

## `POST /api/v1/teams`
- **Policy:** `[Authorize(Policy = StudentPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Student
- **Ghi chú:** Tạo đội mới.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/POST/api-v1-teams-post.md)

## `POST /api/v1/teams/{teamId}/invitations`
- **Policy:** `[Authorize(Policy = StudentPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Student
- **Ghi chú:** Mời thành viên vào đội.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/POST/api-v1-teams-id-invitations-post.md)

## `POST /api/v1/teams/{teamId}/leave`
- **Policy:** `[Authorize(Policy = StudentPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Student
- **Ghi chú:** Rời khỏi đội.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/POST/api-v1-teams-id-leave-post.md)

## `POST /api/v1/teams/{teamId}/rounds/{roundId}/appeal`
- **Policy:** `[Authorize(Policy = StudentPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Student
- **Ghi chú:** Khiếu nại kết quả vòng thi.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/POST/api-v1-teams-id-rounds-id-appeal-post.md)

## `POST /api/v1/teams/{teamId}/submissions/{submissionId}/appeal`
- **Policy:** `[Authorize(Policy = StudentPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Student
- **Ghi chú:** Khiếu nại bài nộp.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/POST/api-v1-teams-id-submissions-id-appeal-post.md)

## `POST /api/v1/rounds/{roundId}/submit-assignment`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Nộp bài tập cho vòng thi.
→ [📄 Doc chi tiết](../../ApiDocs/Rounds/POST/api-v1-rounds-id-submit-assignment-post.md)

## `POST /api/v1/submissions/rounds/{roundId}/register-teams/{registerTeamId}`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Nộp dự án cho vòng thi (theo đăng ký đội).
→ [📄 Doc chi tiết](../../ApiDocs/Submissions/POST/api-v1-rounds-id-register-teams-id-submissions-post.md)

## `POST /api/v1/register-teams`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Đăng ký tham gia sự kiện.
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/POST/api-v1-register-teams-post.md)
