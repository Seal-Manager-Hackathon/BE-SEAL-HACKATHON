# GET - Student

## `GET /api/v1/events`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy danh sách sự kiện. Hỗ trợ query params: keyword, isDisable, PaginationRequest.
→ [📄 Doc chi tiết](../../ApiDocs/Events/GET/api-v1-events-get.md)

## `GET /api/v1/events/{eventId}`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy chi tiết một sự kiện.
→ [📄 Doc chi tiết](../../ApiDocs/Events/GET/api-v1-events-id-get.md)

## `GET /api/v1/events/joined`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy danh sách sự kiện mà user đã tham gia. Hỗ trợ query params.
→ [📄 Doc chi tiết](../../ApiDocs/Events/GET/api-v1-events-joined-get.md)

## `GET /api/v1/events/most-participants`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy danh sách sự kiện có nhiều người tham gia nhất. Hỗ trợ query params: limit, isDisable.
→ [📄 Doc chi tiết](../../ApiDocs/Events/GET/api-v1-events-most-participants.md)

## `GET /api/v1/events/{eventId}/tracks`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy danh sách tracks của sự kiện. Hỗ trợ query params: keyword, isDisable, PaginationRequest.
→ [📄 Doc chi tiết](../../ApiDocs/Events/GET/api-v1-events-id-tracks-get.md)

## `GET /api/v1/events/{eventId}/awards`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy danh sách giải thưởng của sự kiện.
→ [📄 Doc chi tiết](../../ApiDocs/Events/GET/api-v1-events-id-awards-get.md)

## `GET /api/v1/events/{eventId}/leaderboard`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy bảng xếp hạng của sự kiện.
→ [📄 Doc chi tiết](../../ApiDocs/Events/GET/api-v1-events-id-leaderboard-get.md)

## `GET /api/v1/events/{eventId}/summary`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy thông tin tổng quan của sự kiện.
→ [📄 Doc chi tiết](../../ApiDocs/Events/GET/api-v1-events-id-summary-get.md)

## `GET /api/v1/events/{eventId}/teams/{teamId}/scores`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy điểm số của một đội trong sự kiện.
→ [📄 Doc chi tiết](../../ApiDocs/Events/GET/api-v1-events-id-teams-id-scores-get.md)

## `GET /api/v1/tracks`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy danh sách tracks. Hỗ trợ query params: eventId, keyword, isDisable, PaginationRequest.
→ [📄 Doc chi tiết](../../ApiDocs/Tracks/GET/api-v1-tracks-get.md)

## `GET /api/v1/tracks/{trackId}`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy chi tiết một track.
→ [📄 Doc chi tiết](../../ApiDocs/Tracks/GET/api-v1-tracks-id-get.md)

## `GET /api/v1/tracks/{trackId}/topics`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy danh sách chủ đề của track. Hỗ trợ query params: keyword, isDisable, PaginationRequest.
→ [📄 Doc chi tiết](../../ApiDocs/Tracks/GET/api-v1-tracks-id-topics-get.md)

## `GET /api/v1/tracks/{trackId}/teams/count`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy số lượng đội đã đăng ký track.
→ [📄 Doc chi tiết](../../ApiDocs/Tracks/GET/api-v1-tracks-id-teams-count-get.md)

## `GET /api/v1/tracks/my-assignment`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy thông tin phân công của user hiện tại theo sự kiện và role. Query params: eventId, role.
- **Chưa có doc riêng — cần tạo mới**

## `GET /api/v1/rounds?eventId=X`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy danh sách vòng thi của sự kiện. Bắt buộc query param: eventId.
→ [📄 Doc chi tiết](../../ApiDocs/Rounds/GET/api-v1-rounds-get.md)

## `GET /api/v1/rounds/{roundId}`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy chi tiết một vòng thi.
→ [📄 Doc chi tiết](../../ApiDocs/Rounds/GET/api-v1-rounds-id-get.md)

## `GET /api/v1/rounds/{roundId}/ranking`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy bảng xếp hạng của vòng thi. Hỗ trợ query params: keyword, isGraded, PaginationRequest.
→ [📄 Doc chi tiết](../../ApiDocs/Rounds/GET/api-v1-rounds-id-ranking-get.md)

## `GET /api/v1/rounds/{roundId}/my-submissions`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy danh sách bài nộp của user hiện tại trong vòng thi.
→ [📄 Doc chi tiết](../../ApiDocs/Rounds/GET/api-v1-rounds-id-my-submissions-get.md)

## `GET /api/v1/rounds/{roundId}/scores/me`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy điểm số của user hiện tại trong vòng thi.
→ [📄 Doc chi tiết](../../ApiDocs/Rounds/GET/api-v1-rounds-id-scores-me-get.md)

## `GET /api/v1/rounds/{roundId}/teams/{teamId}/latest-submission-score`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy điểm bài nộp mới nhất của đội trong vòng thi.
→ [📄 Doc chi tiết](../../ApiDocs/Rounds/GET/api-v1-rounds-id-teams-id-latest-submission-score-get.md)

## `GET /api/v1/rounds/teams/{teamId}`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy danh sách vòng thi của đội. Query params: eventId.
→ [📄 Doc chi tiết](../../ApiDocs/Rounds/GET/api-v1-rounds-teams-id-get.md)

## `GET /api/v1/rounds/register-teams/{registerTeamId}`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy chi tiết vòng thi dựa trên registerTeamId (dành cho đội đã đăng ký).
→ [📄 Doc chi tiết](../../ApiDocs/Rounds/GET/api-v1-rounds-register-teams-id-get.md)

## `GET /api/v1/submissions/{submissionId}`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy chi tiết một bài nộp.
→ [📄 Doc chi tiết](../../ApiDocs/Submissions/GET/api-v1-submissions-id-get.md)

## `GET /api/v1/submissions/rounds/{roundId}/register-teams/{registerTeamId}`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy danh sách bài nộp của đội trong vòng thi. Hỗ trợ query params.
→ [📄 Doc chi tiết](../../ApiDocs/Submissions/GET/api-v1-submissions-rounds-id-register-teams-id-get.md)

## `GET /api/v1/teams/me`
- **Policy:** `[Authorize(Policy = StudentPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Student
- **Ghi chú:** Lấy danh sách đội của user hiện tại. Hỗ trợ pagination.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/GET/api-v1-teams-me-get.md)

## `GET /api/v1/teams/{teamId}`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy chi tiết một đội.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/GET/api-v1-teams-id-get.md)

## `GET /api/v1/teams/{teamId}/members`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy danh sách thành viên của đội.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/GET/api-v1-teams-id-members-get.md)

## `GET /api/v1/teams/{teamId}/notifications`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy danh sách thông báo của đội.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/GET/api-v1-teams-id-notifications-get.md)

## `GET /api/v1/teams/{teamId}/events`
- **Policy:** `[Authorize(Policy = StudentPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Student
- **Ghi chú:** Lấy danh sách sự kiện mà đội đã đăng ký. Hỗ trợ query params, pagination.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/GET/api-v1-teams-id-events-get.md)

## `GET /api/v1/teams/{teamId}/events/approved-count`
- **Policy:** `[Authorize(Policy = StudentPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Student
- **Ghi chú:** Lấy số lượng sự kiện đã được duyệt của đội.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/GET/api-v1-teams-id-events-approved-count-get.md)

## `GET /api/v1/teams/{teamId}/events/latest`
- **Policy:** `[Authorize(Policy = StudentPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Student
- **Ghi chú:** Lấy sự kiện đã đăng ký gần nhất của đội.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/GET/api-v1-teams-id-events-latest-get.md)

## `GET /api/v1/teams/my-registrations`
- **Policy:** `[Authorize(Policy = StudentPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Student
- **Ghi chú:** Lấy danh sách đăng ký của user hiện tại theo sự kiện. Query params: eventId.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/GET/api-v1-teams-my-registrations-get.md)

## `GET /api/v1/teams/me/register-teams`
- **Policy:** `[Authorize(Policy = StudentPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Student
- **Ghi chú:** Lấy danh sách sự kiện đã đăng ký của đội user. Query params: status, PaginationRequest.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/GET/api-v1-teams-me-register-teams-get.md)

## `GET /api/v1/register-teams/me`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy danh sách sự kiện đã đăng ký của user hiện tại.
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/GET/api-v1-register-teams-me-get.md)

## `GET /api/v1/register-teams/{registerId}`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy chi tiết đăng ký (dành cho student - chỉ xem được của mình).
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/GET/api-v1-register-teams-id-get.md)

## `GET /api/v1/register-teams/{registerId}/rejection-reason`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy lý do từ chối đăng ký.
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/GET/api-v1-register-teams-rejection-reason-get.md)

## `GET /api/v1/register-teams/events/{eventId}/teams`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy danh sách đội đã đăng ký sự kiện (theo vòng thi). Query params: roundId.
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/GET/api-v1-register-teams-events-eventid-teams-get.md)

## `GET /api/v1/register-teams/events/{eventId}/approved-teams`
- **Policy:** `[Authorize(Policy = StaffLecturerOrAdminPolicy)]`
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Staff, Lecturer, Admin
- **Ghi chú:** Lấy danh sách đội đã được duyệt trong sự kiện. Yêu cầu quyền Staff/Lecturer/Admin.
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/GET/api-v1-register-teams-events-eventid-approved-teams-get.md)

## `GET /api/v1/register-teams/events/{eventId}/tracks/{trackId}/teams`
- **Policy:** `[Authorize(Policy = StaffLecturerOrAdminPolicy)]`
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Staff, Lecturer, Admin
- **Ghi chú:** Lấy danh sách đội theo track trong sự kiện. Yêu cầu quyền Staff/Lecturer/Admin.
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/GET/api-v1-register-teams-events-eventid-tracks-trackid-teams-get.md)

## `GET /api/v1/register-teams/{registerTeamId}/assignment-status`
- **Policy:** `[Authorize]` (Yêu cầu đăng nhập)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy trạng thái phân công (track assignment) của đội đã đăng ký.
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/GET/api-v1-register-teams-registerteamid-assignment-status-get.md)

## `GET /api/v1/events/{eventId}/register-teams/{registerTeamId}/topic`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy thông tin chủ đề (topic) mà đội đã đăng ký trong sự kiện.
→ [📄 Doc chi tiết](../../ApiDocs/Topics/GET/api-v1-events-id-register-teams-id-topic-get.md)

## `GET /api/v1/topics/{topicId}`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy chi tiết một chủ đề.
→ [📄 Doc chi tiết](../../ApiDocs/Topics/GET/api-v1-topics-id-get.md)

## `GET /api/v1/rounds/{roundId}/criteria`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy danh sách tiêu chí chấm điểm của vòng thi.
→ [📄 Doc chi tiết](../../ApiDocs/Critical/GET/api-v1-rounds-id-criteria-get.md)

## `GET /api/v1/events/{eventId}/criteria`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy danh sách tiêu chí chấm điểm của sự kiện.
→ [📄 Doc chi tiết](../../ApiDocs/Critical/GET/api-v1-events-id-criteria-get.md)

## `GET /api/v1/leaderboards/year/{year}`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles
- **Ghi chú:** Lấy bảng xếp hạng theo năm.
→ [📄 Doc chi tiết](../../ApiDocs/LeaderBoards/GET/api-v1-leaderboards-year-year-get.md)
