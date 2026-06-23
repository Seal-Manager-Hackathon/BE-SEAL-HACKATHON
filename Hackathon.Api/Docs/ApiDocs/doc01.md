# Kịch Bản & Luồng Vận Hành SEAL Hackathon Management System

Tài liệu này mô tả chi tiết 3 luồng vận hành lớn của hệ thống. Trong mỗi luồng lớn có các phân đoạn nhỏ hơn, chỉ rõ quy trình nghiệp vụ và các API tương ứng được gọi.

- Các API **có đánh số ở đầu (từ 1 đến 59)** là API **đã có route/controller trong hệ thống** (đối chiếu chính xác với `doc.md`).
- Các API **không đánh số (bắt đầu bằng `-`)** là API **đề xuất còn thiếu** để luồng hoạt động khép kín và hoàn chỉnh.
- **Thống kê tổng số lượng API**:
  * **Đã có code (đánh số)**: **59** API
  * **Chưa có code (đề xuất `-`)**: **114** API

---

# LUỒNG 1: TRẢI NGHIỆM THÍ SINH (STUDENT & TEAM WORKFLOW)
Luồng này mô tả hành trình của một sinh viên từ lúc tham gia hệ thống cho đến khi hoàn thành các vòng thi và xem bảng xếp hạng chung cuộc.

## Luồng 1.1: Khởi tạo tài khoản & Hoàn thiện hồ sơ
*Mô tả*: Sinh viên đăng ký tài khoản mới, xác thực email, đăng nhập và bắt buộc phải điền đầy đủ hồ sơ để đủ điều kiện tham gia/tạo team.

1. **Đăng ký tài khoản mới**:
   * API 1: [`POST /api/v1/auth/register`](Auth/POST/POST-api-v1-auth-register.md) — Đăng ký tài khoản student mới.
2. **Xác thực Email**:
   * API 4: [`POST /api/v1/auth/email-verifications`](Auth/POST/POST-api-v1-auth-email-verifications.md) — Verify email bằng token nhận được từ hòm thư.
   * *Dự phòng/Resend*: API 10: [`POST /api/v1/auth/email-verifications/resend`](Auth/POST/POST-api-v1-auth-email-verifications-resend.md) — Gửi lại email xác thực.
3. **Quên mật khẩu / Reset password**:
   * API 8: [`POST /api/v1/auth/forgot-password`](Auth/POST/POST-api-v1-auth-forgot-password.md) — Gửi yêu cầu quên mật khẩu; hệ thống gửi email chứa link reset kèm token.
   * FE mở trang reset password từ link trong email, lấy `token` trên URL, cho user nhập `newPassword` và `confirmPassword`.
   * API 9: [`POST /api/v1/auth/reset-password`](Auth/POST/POST-api-v1-auth-reset-password.md) — Gửi `token`, mật khẩu mới và xác nhận mật khẩu để hoàn tất đặt lại mật khẩu.
4. **Đăng nhập hệ thống**:
   * API 2: [`POST /api/v1/auth/login`](Auth/POST/POST-api-v1-auth-login.md) — Đăng nhập, nhận JWT Token qua Cookie.
   * *Lấy thông tin hiện tại*: API 5: [`GET /api/v1/auth/me`](Auth/GET/GET-api-v1-auth-me.md) — Lấy thông tin user đăng nhập.
   * *Làm mới phiên*: API 3: [`POST /api/v1/auth/tokens/refresh`](Auth/POST/POST-api-v1-auth-tokens-refresh.md) — Refresh access token bằng refresh token.
   * *Quản lý phiên*: [`- GET /api/v1/auth/sessions`](Auth/GET/GET-api-v1-auth-sessions.md) — Xem các phiên đăng nhập đang hoạt động.
5. **Cập nhật & Hoàn thiện Profile**:
   * *Xem Profile*: API 11: [`GET /api/v1/users/profile`](Users/GET/GET-api-v1-users-profile.md) — Lấy thông tin chi tiết profile.
   * *Cập nhật Profile*: API 12: [`PATCH /api/v1/users/profile`](Users/PATCH/PATCH-api-v1-users-profile.md) — Cập nhật thông tin profile (FirstName, LastName, PhoneNumber, StudentId, College, AvatarUrl).
	   * *Đổi mật khẩu*: API 7: [`PATCH /api/v1/auth/change-password`](Auth/PATCH/PATCH-api-v1-auth-change-password.md) — Đổi mật khẩu tài khoản.

---

## Luồng 1.2: Khám phá sự kiện (Event Discovery)
*Mô tả*: Thí sinh tìm kiếm, lọc và xem thông tin chi tiết các giải đấu Hackathon đang mở đăng ký hoặc đã diễn ra.

1. **Tìm kiếm & Phân trang sự kiện**:
   * API 14: [`GET /api/v1/events`](Events/GET/GET-api-v1-events.md) — Xem danh sách event public (filter theo keyword, status, year).
   * API 16: [`GET /api/v1/events/most-participants`](Events/GET/GET-api-v1-events-most-participants.md) — Lấy danh sách event nổi bật (nhiều participant nhất).
   * API 18: [`GET /api/v1/events/events/joined`](Events/GET/GET-api-v1-events-events-joined.md) — Xem các event user đã tham gia (route lặp chữ events, nên sửa thành `/api/v1/events/joined`).
2. **Xem chi tiết thông tin Event**:
   * API 15: [`GET /api/v1/events/{eventId}`](Events/GET/GET-api-v1-events-eventId.md) — Xem chi tiết một event (timeline, mô tả, thể lệ, số thành viên min/max).
   * API 52: [`GET /api/v1/{eventId}/round`](Rounds/GET/GET-api-v1-rounds.md) — Từ event lấy danh sách các round của event.
   * API 17: [`GET /api/v1/events/{eventId}/tracks`](Events/GET/GET-api-v1-events-eventId-tracks.md) — Từ event lấy danh sách track/bảng đấu của event.
   * [`- GET /api/v1/events/{eventId}/summary`](Events/GET/GET-api-v1-events-eventId-summary.md) — Thống kê số team đã duyệt, số track, số round.
3. **Xem chi tiết Round trong Event**:
   * [`- GET /api/v1/rounds/{roundId}`](Rounds/GET/GET-api-v1-rounds-roundId.md) — Khi bấm vào chi tiết round, lấy thông tin timeline, hạn nộp bài và cấu hình của round.
   * API 58: [`GET /api/v1/rounds/{roundId}/criteria`](Rounds/GET/GET-api-v1-rounds-roundId-criteria.md) — Trong chi tiết round, hiển thị tiêu chí chấm điểm của round đó.
   * API 50: [`GET /api/v1/tracks/{trackId}/topics`](Tracks/GET/GET-api-v1-tracks-trackId-topics.md) — Khi bấm vào một track, lấy danh sách topic/đề bài của track.
4. **Xem bảng xếp hạng khi khám phá event**:
   * [`- GET /api/v1/rounds/{roundId}/ranking`](Rounds/GET/GET-api-v1-rounds-roundId-ranking.md) — Từ round xem bảng xếp hạng theo điểm trung bình submission của round đó.
   * [`- GET /api/v1/events/{eventId}/leaderboard`](Events/GET/GET-api-v1-events-eventId-leaderboard.md) — Từ event xem bảng xếp hạng chung của event, trả về `teamId` và tổng điểm của từng team.
   * [`- GET /api/v1/events/{eventId}/teams/{teamId}/scores`](Events/GET/GET-api-v1-events-eventId-teams-teamId-scores.md) — Khi bấm vào một team trong leaderboard, xem điểm của team đó theo từng round và điểm chi tiết của từng tiêu chí.
5. **Xem cơ cấu giải thưởng**:
   * [`- GET /api/v1/events/{eventId}/awards`](Events/GET/GET-api-v1-events-eventId-awards.md) — Xem danh sách giải thưởng công bố của event (giá trị giải, số lượng).
6. **Xem tiêu chí chấm điểm toàn event**:
   * API 59: [`GET /api/v1/events/{eventId}/criteria`](Events/GET/GET-api-v1-events-eventId-criteria.md) — Xem toàn bộ tiêu chí chấm điểm (rubrics) của event.

---

## Luồng 1.3: Quản lý Team & Lời mời (Team Formation)
*Mô tả*: Thí sinh tiến hành tạo đội thi, mời các thành viên khác bằng email, hoặc nhận và phản hồi các lời mời gia nhập đội từ người khác.

1. **Khởi tạo Team mới**:
   * API 26: [`POST /api/v1/teams`](Teams/POST/POST-api-v1-teams.md) — Tạo team mới (người tạo tự động trở thành Leader với cờ `IsLeader = true`).
2. **Mời thành viên (Dành cho Leader)**:
   * API 27: [`POST /api/v1/teams/{teamId}/invitations`](Teams/POST/POST-api-v1-teams-teamId-invitations.md) — Leader gửi lời mời gia nhập team tới email sinh viên khác.
   * *Quản lý lời mời đã gửi*: [`- GET /api/v1/teams/{teamId}/invitations`](Teams/POST/POST-api-v1-teams-teamId-invitations.md) — Leader xem các lời mời đang pending của team.
3. **Quản trị Team (Sửa đổi nhân sự - Chỉ khi chưa khóa đăng ký)**:
   * *Đổi tên team*: API 28: [`PUT /api/v1/teams/{teamId}`](Teams/PUT/PUT-api-v1-teams-teamId.md) — Leader cập nhật tên team.
   * *Xóa thành viên*: API 29: [`DELETE /api/v1/teams/{teamId}/members`](Teams/DELETE/DELETE-api-v1-teams-teamId-members.md) — Leader trục xuất thành viên khỏi team.
   * *Nhường Leader*: API 30: [`PUT /api/v1/teams/{teamId}/leader`](Teams/PUT/PUT-api-v1-teams-teamId-leader.md) — Chuyển quyền Leader cho thành viên khác.
   * *Xem chi tiết*: API 25: [`GET /api/v1/teams/{teamId}`](Teams/GET/GET-api-v1-teams-teamId.md) — Xem chi tiết team (leader, danh sách member).
   * *Xem team của tôi*: API 24: [`GET /api/v1/teams/me`](Teams/GET/GET-api-v1-teams-me.md) — Lấy danh sách team của user đang đăng nhập.
   * *Tự rời team*: [`- POST /api/v1/teams/{teamId}/leave`](Teams/POST/POST-api-v1-teams-teamId-leave.md) — Member tự rời khỏi team.

---

## Luồng 1.4: Đăng ký tham gia Event (Event Registration)
*Mô tả*: Team leader đại diện cho đội nộp đơn đăng ký tham gia sự kiện. Sau đó theo dõi trạng thái phê duyệt từ BTC.

1. **Gửi đơn đăng ký tham gia**:
   * API 37: [`POST /api/v1/register-teams`](RegisterTeams/POST/POST-api-v1-register-teams.md) — Team leader gửi đơn đăng ký team vào event. Hệ thống sẽ validate profile, số lượng thành viên, và trùng lịch tham gia.
2. **Theo dõi trạng thái đơn đăng ký**:
   * API 38: [`GET /api/v1/register-teams/me`](RegisterTeams/GET/GET-api-v1-register-teams-me.md) — Xem trạng thái đơn đăng ký của đội mình (`Pending`, `Approved`, `Rejected`).
   * *Thành viên xem đơn đăng ký của team*: [`- GET /api/v1/teams/me/register-teams`](Teams/GET/GET-api-v1-teams-me-register-teams.md) — Cả Leader và Member xem danh sách đơn đăng ký vào event của team mình (cả 3 trạng thái).
   * *Lấy lý do từ chối (nếu có)*: API 39: [`GET /api/v1/register-teams/{registerId}/rejection-reason`](RegisterTeams/GET/GET-api-v1-register-teams-registerId-rejection-reason.md) — Lấy lý do bị reject từ BTC.
   * *Xem chi tiết đơn đăng ký*: [`- GET /api/v1/register-teams/{registerId}`](RegisterTeams/GET/GET-api-v1-register-teams-id-get.md) — Cả Leader và Member xem chi tiết một đơn đăng ký của team.
3. **Xem các event mà Team tham gia & chi tiết Event**:
   * API 31: [`GET /api/v1/teams/{teamId}/events`](Teams/GET/GET-api-v1-teams-teamId-events.md) — Xem danh sách các event team đã đăng ký/tham gia.
   * API 15: [`GET /api/v1/events/{eventId}`](Events/GET/GET-api-v1-events-eventId.md) — Khi bấm vào một event trong danh sách, xem chi tiết thông tin event.
   * API 32: [`GET /api/v1/teams/{teamId}/events/approved-count`](Teams/GET/GET-api-v1-teams-teamId-events-approved-count.md) — Đếm số event team đã được approve tham gia thi đấu.
   * API 33: [`GET /api/v1/teams/{teamId}/events/latest`](Teams/GET/GET-api-v1-teams-teamId-events-latest.md) — Lấy event đăng ký mới nhất của team.

---

## Luồng 1.5: Bốc thăm đề tài & Nhận bảng đấu (Draw & Topic Assignment)
*Mô tả*: Sau khi được duyệt, team được BTC bốc thăm offline để nhận track/bảng đấu và topic/đề thi trong event. Khi BTC gán xong topic, hệ thống mặc định đưa team vào vòng đầu tiên (`RoundNo = 1`).

1. **Xem track & topic hiện tại của team trong event**:
   * API 51: [`GET /api/v1/events/{eventId}/register-teams/{registerTeamId}/topic`](Topics/GET/GET-api-v1-events-eventId-register-teams-registerTeamId-topic.md) — Ở ngoài màn hình event, team xem mình đang thuộc track nào và topic nào.
   * *Đếm số đội cùng bảng*: API 49: [`GET /api/v1/tracks/{trackId}/teams/count`](Tracks/GET/GET-api-v1-tracks-trackId-teams-count.md) — Đếm số team trong bảng đấu.
   * *Xem các topic trong track*: API 50: [`GET /api/v1/tracks/{trackId}/topics`](Tracks/GET/GET-api-v1-tracks-trackId-topics.md) — Lấy danh sách topic theo track.
   * *Xem chi tiết đề bài*: [`- GET /api/v1/topics/{topicId}`](Topics/GET/GET-api-v1-topics-topicId.md) — Đọc nội dung đề thi, link đính kèm, yêu cầu nộp bài.

---

## Luồng 1.6: Tham gia thi đấu & Nộp bài (Submission Workflow)
*Mô tả*: Thí sinh theo dõi các round của event, xem tiêu chí chấm điểm, nộp bài cho từng round và xem lịch sử bài nộp.

1. **Xem round và tiêu chí chấm điểm**:
   * API 52: [`GET /api/v1/rounds`](Rounds/GET/GET-api-v1-rounds.md) — Lấy danh sách các round của event (Vòng loại, Bán kết, Chung kết) kèm timeline; danh sách sắp theo `RoundNo`.
   * API 53: [`GET /api/v1/rounds/teams/{teamId}`](Rounds/GET/GET-api-v1-rounds-teams-teamId.md) — Lấy danh sách các round mà team được quyền tham gia thi đấu, có thể lọc theo event. Team mới được gán Track + Topic sẽ mặc định thấy `RoundNo = 1`; các round sau chỉ xuất hiện nếu team được chọn đi tiếp khi kết thúc round trước.
   * API 58: [`GET /api/v1/rounds/{roundId}/criteria`](Rounds/GET/GET-api-v1-rounds-roundId-criteria.md) — Xem tiêu chí chấm điểm của round.
2. **Xem chi tiết round và danh sách bài nộp**:
   * [`- GET /api/v1/rounds/{roundId}`](Rounds/GET/GET-api-v1-rounds-roundId.md) — Khi bấm vào chi tiết round, xem timeline, hạn nộp và cấu hình round.
   * [`- GET /api/v1/rounds/{roundId}/my-submissions`](Rounds/GET/GET-api-v1-rounds-roundId-my-submissions.md) — Trong chi tiết round có nút/thẻ "Bài nộp" để xem lịch sử bài nộp của team trong round; hệ thống lấy bài nộp cuối cùng để chấm khi đến thời gian kết thúc.
   * [`- GET /api/v1/submissions/{submissionId}`](Submissions/GET/GET-api-v1-submissions-submissionId.md) — Xem chi tiết một bài nộp trong lịch sử.
3. **Nộp bài thi cho round (Chỉ dành cho Team Leader)**:
   * API 55: [`POST /api/v1/rounds/{roundId}/submit-assignment`](Rounds/POST/POST-api-v1-rounds-roundId-submit-assignment.md) — Phần nộp bài của round, ghi nhận link sản phẩm và mô tả bài làm.
4. **Xem kết quả bài nộp**:
   * [`- GET /api/v1/rounds/{roundId}/scores/me`](Rounds/GET/GET-api-v1-rounds-roundId-scores-me.md) — Xem kết quả/điểm của team trong round; nếu chưa có kết quả thì hiển thị "Bài chưa được chấm".
   * [`- GET /api/v1/submissions/{submissionId}`](Submissions/GET/GET-api-v1-submissions-submissionId.md) — Xem chi tiết bài nộp kèm trạng thái `NotGraded` hoặc điểm đã chấm nếu có.

---

## Luồng 1.7: Xem xếp hạng, Nhận điểm số & Phúc khảo
*Mô tả*: Khi BTC công bố điểm/kết quả, thí sinh xem điểm theo round/event, xem thứ hạng event và gửi khiếu nại theo bài nộp nếu cần.

1. **Xem điểm số của đội mình**:
   * [`- GET /api/v1/events/{eventId}/teams/{teamId}/scores`](Events/GET/GET-api-v1-events-eventId-teams-teamId-scores.md) — Khi bấm chi tiết điểm, xem điểm của team trong event theo từng round và điểm chi tiết của từng tiêu chí.
   * [`- GET /api/v1/rounds/{roundId}/scores/me`](Rounds/GET/GET-api-v1-rounds-roundId-scores-me.md) — Xem điểm/kết quả của team ở round đang chọn.
2. **Xem bảng xếp hạng**:
   * [`- GET /api/v1/rounds/{roundId}/ranking`](Rounds/GET/GET-api-v1-rounds-roundId-ranking.md) — Xem bảng xếp hạng theo round.
   * [`- GET /api/v1/events/{eventId}/leaderboard`](Events/GET/GET-api-v1-events-eventId-leaderboard.md) — Xem bảng xếp hạng event; trong danh sách này team biết mình đang đứng hạng thứ mấy trong event.
   * `- GET /api/v1/teams/{teamId}/round-results` — Xem kết quả tổng hợp của team qua từng vòng (Đi tiếp - Advanced / Dừng lại - Stopped).
3. **Gửi khiếu nại/phúc khảo khi bài đã có kết quả**:
   * [`- POST /api/v1/teams/{teamId}/submissions/{submissionId}/appeal`](Teams/POST/POST-api-v1-teams-teamId-submissions-submissionId-appeal.md) — Khi bài nộp đã có kết quả, FE hiển thị nút khiếu nại; request truyền `submissionId` để Staff/Admin xem đúng bài và phân công judge khác chấm lại nếu cần.

---

## Luồng 1.8: Khiếu nại hệ thống & Xem bảng vàng chung cuộc
*Mô tả*: Thí sinh gửi khiếu nại/báo cáo lỗi hệ thống, xem bảng xếp hạng sự kiện và mùa giải, sau đó kết thúc phiên làm việc.

1. **Báo cáo lỗi / Khiếu nại chung**:
   * API 13: [`POST /api/v1/users/system-report`](Users/POST/POST-api-v1-users-system-report.md) — User gửi report/khiếu nại/hỗ trợ chung lên hệ thống.
   * *Xem lại khiếu nại*: [`- GET /api/v1/users/reports/me`](Users/GET/GET-api-v1-users-reports-me.md) — Xem lịch sử report cá nhân.
   * *Xem chi tiết khiếu nại*: [`- GET /api/v1/users/reports/{reportId}`](Users/GET/GET-api-v1-users-reports-reportId.md) — Xem chi tiết tiến độ giải quyết khiếu nại.
2. **Xem xếp hạng chung cuộc & Xếp hạng năm**:
   * [`- GET /api/v1/events/{eventId}/leaderboard`](Events/GET/GET-api-v1-events-eventId-leaderboard.md) — Xem bảng vàng xếp hạng chung cuộc của event.
   * [`- GET /api/v1/leaderboards/year/{year}`](LeaderBoards/GET/GET-api-v1-leaderboards-year-year.md) — Xem bảng vàng tích lũy điểm số của mùa giải trong năm.
3. **Đăng xuất**:
   * API 6: [`POST /api/v1/auth/logout`](Auth/POST/POST-api-v1-auth-logout.md) — Đăng xuất hệ thống.

---
---

# LUỒNG 2: ADMIN & BAN TỔ CHỨC (ADMIN & LIFECYCLE WORKFLOW)
Luồng này dành cho Admin/BTC để khởi tạo, cấu hình, phân công nhân sự, vận hành bốc thăm, điều phối thăng vòng và đóng giải đấu.

## Luồng 2.1: Khởi tạo và Thiết lập Event cơ bản
*Mô tả*: Admin thiết lập các thông tin cơ bản cho một giải đấu Hackathon mới.

1. **Tạo Event mới (Trạng thái mặc định: Draft)**:
   * API 19: [`POST /api/v1/admin/events`](Events/POST/POST-api-v1-admin-events.md) — Admin tạo event mới với các thông tin: tên, mô tả, season, min/max member, thời hạn đăng ký.
2. **Cập nhật cấu hình sự kiện**:
   * API 20: [`PATCH /api/v1/admin/events/{eventId}`](Events/PATCH/PATCH-api-v1-admin-events-eventId.md) — Điều chỉnh các mốc thời gian, giới hạn số lượng team tham gia.
3. **Thiết lập giải thưởng (Awards)**:
   * `- POST /api/v1/admin/events/{eventId}/awards` — Tạo cơ cấu giải thưởng (Nhất, Nhì, Ba, Khuyến khích, Giải phụ) kèm số tiền thưởng tương ứng.
4. **Quản lý trạng thái hiển thị của Event**:
   * API 22: [`PATCH /api/v1/admin/events/{eventId}/publish`](Events/PATCH/PATCH-api-v1-admin-events-eventId-publish.md) — Công bố event ra ngoài giao diện cho sinh viên đăng ký.
   * [`- PATCH /api/v1/admin/events/{eventId}/unpublish`](Events/PATCH/PATCH-api-v1-admin-events-eventId-unpublish.md) — Ẩn event về trạng thái Draft khi cần setup lại thông tin.
   * API 23: [`GET /api/v1/admin/events`](Events/GET/GET-api-v1-admin-events.md) — Admin xem danh sách quản lý tất cả event.
   * API 21: [`DELETE /api/v1/admin/events/{eventId}`](Events/DELETE/DELETE-api-v1-admin-events-eventId.md) — Xóa/Disable event (xóa mềm).

---

## Luồng 2.2: Thiết lập vòng thi & Bộ tiêu chí chấm (Rounds & Criteria)
*Mô tả*: Admin thiết lập cơ cấu các vòng thi trong event và xây dựng bộ rubric tiêu chí chấm điểm chi tiết cho từng vòng.

1. **Tạo các vòng thi (Rounds)**:
   * `- POST /api/v1/admin/events/{eventId}/rounds` — Tạo round (Vòng 1, Vòng 2) kèm mốc thời gian làm bài, hạn nộp bài (`StartSubmission`, `EndSubmission`).
   * `- PATCH /api/v1/admin/rounds/{roundId}` — Điều chỉnh timeline nộp bài hoặc tên vòng thi.
2. **Thiết lập bộ tiêu chí chấm điểm (Criteria templates)**:
   * `- POST /api/v1/admin/rounds/{roundId}/criteria-templates` — Tạo nhóm tiêu chí (ví dụ: Bộ tiêu chí chấm Source Code, Bộ chấm Pitching).
3. **Thêm tiêu chí chi tiết (Criteria items)**:
   * `- POST /api/v1/admin/criteria-templates/{criteriaTemplateId}/items` — Thêm tiêu chí chi tiết kèm điểm tối đa (ví dụ: Tính sáng tạo - Max 20đ, Độ hoàn thiện kỹ thuật - Max 30đ).
   * *Sao chép nhanh*: `- POST /api/v1/admin/rounds/{roundId}/criteria/copy` — Sao chép toàn bộ bộ tiêu chí của round khác/giải khác để tiết kiệm thời gian cấu hình.
4. **Khóa bộ tiêu chí**:
   * `- PATCH /api/v1/admin/rounds/{roundId}/criteria/lock` — BTC khóa bộ tiêu chí chấm, cấm chỉnh sửa rubric một khi giải đấu bắt đầu (BR-EVT-06).

---

## Luồng 2.3: Phân bảng đấu & Đề thi (Tracks & Topics)
*Mô tả*: Admin cấu hình các bảng thi đấu (Track) và thiết lập đề bài/chủ đề thi (Topic) cho từng bảng.

1. **Tạo bảng đấu (Tracks)**:
   * API 17: [`GET /api/v1/events/{eventId}/tracks`](Events/GET/GET-api-v1-events-eventId-tracks.md) — Lấy danh sách track của event (route này được thí sinh xem và admin kiểm tra).
   * [`- POST /api/v1/admin/events/{eventId}/tracks`](Tracks/POST/POST-api-v1-admin-events-eventId-tracks.md) — Admin tạo track mới trong event.
2. **Tạo đề bài/Chủ đề (Topics)**:
   * [`- POST /api/v1/admin/tracks/{trackId}/topics`](Tracks/POST/POST-api-v1-admin-tracks-trackId-topics.md) — Tạo đề thi lồng trong track (topic chính là đề thi - BR-TRACK-02).
3. **Ẩn/Hiện đề thi và bảng đấu**:
   * [`- PATCH /api/v1/admin/tracks/{trackId}/hide`](Tracks/PATCH/PATCH-api-v1-admin-tracks-trackId-visibility.md) — Ẩn bảng đấu trước giờ bốc thăm.
   * [`- PATCH /api/v1/admin/topics/{topicId}/hide`](Topics/PATCH/PATCH-api-v1-admin-topics-topicId-visibility.md) — Khóa đề bài, ẩn đi cho tới thời điểm bắt đầu thi đấu.

---

## Luồng 2.4: Phân công nhân sự & Giảng viên (Personnel Assignment)
*Mô tả*: Admin phân công Staff phụ trách vận hành event, phân công giảng viên làm Mentor hoặc Judge cho từng track.

1. **Phân công Nhân viên vận hành (Staff)**:
   * [`- POST /api/v1/admin/events/{eventId}/staff`](Events/POST/POST-api-v1-admin-events-id-staff-post.md) — Phân công Staff vận hành sự kiện (chỉ staff được gán mới có quyền duyệt đơn đăng ký của event đó - BR-ASG-01).
2. **Phân công Giảng viên vào Event**:
   * [`- POST /api/v1/admin/events/{eventId}/lecturers`](Events/POST/POST-api-v1-admin-events-eventId-lecturers.md) — Gán giảng viên làm Judge hoặc Mentor của Event (kiểm tra BR-ASG-04: một giảng viên không vừa làm Judge vừa làm Mentor trong cùng event).
3. **Phân công Giám khảo/Mentor vào bảng đấu chi tiết**:
   * [`- POST /api/v1/admin/assign-events/{assignEventId}/tracks`](Events/POST/POST-api-v1-admin-assign-events-assignEventId-tracks.md) — Gán Mentor/Judge vào track cụ thể để phân quyền chấm thi và quản lý (BR-ASG-03).
   * *Gỡ phân công*: [`- DELETE /api/v1/admin/assign-tracks/{assignTrackId}`](Events/DELETE/DELETE-api-v1-admin-assign-tracks-assignTrackId.md) — Gỡ giám khảo/mentor khỏi track.

---

## Luồng 2.5: Phê duyệt đơn đăng ký & Bốc thăm đề thi
*Mô tả*: Staff phụ trách event duyệt đơn đăng ký của các đội thi, nhập kết quả bốc thăm offline vào hệ thống để chia bảng đấu và gán đề tài.

1. **Duyệt/Từ chối đơn đăng ký thi**:
   * *Xem danh sách đăng ký*: API 40: [`GET /api/v1/register-teams/staff/events/{eventId}`](RegisterTeams/GET/GET-api-v1-register-teams-staff-events-eventId.md) — Staff lọc danh sách các đơn đăng ký pending.
   * *Xem chi tiết team*: API 41: [`GET /api/v1/register-teams/staff/{registerTeamId}`](RegisterTeams/GET/GET-api-v1-register-teams-staff-registerTeamId.md) — Xem chi tiết đội thi và profile thành viên.
   * *Duyệt*: API 42: [`PUT /api/v1/register-teams/staff/{registerId}/approve`](RegisterTeams/PUT/PUT-api-v1-register-teams-staff-registerId-approve.md) — BTC duyệt team vào event (BR-REG-03, tự động khóa nhân sự team BR-TEAM-07).
   * *Từ chối*: API 43: [`PUT /api/v1/register-teams/staff/{registerId}/reject`](RegisterTeams/PUT/PUT-api-v1-register-teams-staff-registerId-reject.md) — BTC từ chối đơn kèm lý do từ chối (BR-REG-04).
2. **Xem phân bổ Bảng/Đề phục vụ bốc thăm**:
   * API 44: [`GET /api/v1/staff/events/{eventId}/tracks`](Staff/GET/GET-api-v1-staff-events-eventId-tracks.md) — Staff xem tracks của event để phục vụ bốc thăm.
   * API 45: [`GET /api/v1/staff/tracks/{trackId}/topics`](Staff/GET/GET-api-v1-staff-tracks-trackId-topics.md) — Staff xem topics của track để chuẩn bị gán đề.
3. **Nhập kết quả bốc thăm chia đề/bảng**:
   * *Lấy danh sách đội thi*: API 46: [`GET /api/v1/staff/events/{eventId}/teams`](Staff/GET/GET-api-v1-staff-events-eventId-teams.md) — BTC chọn event và lấy danh sách team đã được duyệt để chuẩn bị gán kết quả bốc thăm offline.
   * *Gán Track*: API 47: [`PATCH /api/v1/staff/teams/{teamId}/track`](Staff/PATCH/PATCH-api-v1-staff-teams-teamId-track.md) — BTC chọn team và gán track/bảng đấu cho team theo kết quả bốc thăm offline.
   * *Gán Topic*: API 48: [`PATCH /api/v1/staff/teams/{teamId}/topic`](Staff/PATCH/PATCH-api-v1-staff-teams-teamId-topic.md) — BTC chọn team và gán đề thi cho team; sau bước này hệ thống tạo `RoundDetails` cho `RoundNo = 1`.
4. **Hoàn tất bốc thăm**:
   * Flow hiện tại dùng gán trực tiếp từng team qua API 47/48. Các API bulk/import/publish draw-results là đề xuất dư thừa nên không đưa vào luồng chính.

---

## Luồng 2.6: Phân công Giám khảo chấm bài & Theo dõi tiến độ
*Mô tả*: Sau khi các đội đã nộp bài, BTC xem danh sách bài nộp phân loại theo track/topic và chủ động phân công giám khảo phù hợp cho từng bài thi.

1. **Xem danh sách bài nộp phân loại theo track/topic**:
   * [`- GET /api/v1/staff/rounds/{roundId}/submissions`](Staff/GET/GET-api-v1-staff-rounds-id-submissions.md) — Staff/Admin xem danh sách bài nộp của vòng thi, lọc theo `trackId`, `topicId`, `gradingStatus`, có kèm thông tin judge đã phân công và điểm nếu có.
2. **Phân công giám khảo chấm bài**:
   * [`- POST /api/v1/staff/submissions/{submissionId}/assign-judges`](Staff/POST/GET-api-v1-staff-submissions-id-assign-judges.md) — Staff/Admin chọn một bài nộp và gán một hoặc nhiều judge chấm bài đó, giúp BTC chủ động phân công theo chuyên môn thay vì gán judge theo track toàn bộ.
3. **Gỡ giám khảo khỏi bài nộp**:
   * Gọi API assign-judges với mảng `judgeIds: []` rỗng để gỡ toàn bộ judge khỏi bài nộp.

---

## Luồng 2.7: Theo dõi và Điều phối thăng vòng (Round End & Advancement)
*Mô tả*: Hết giờ làm bài, BTC kết thúc vòng thi, rà soát bảng điểm tổng hợp từ các giám khảo, chọn các đội xuất sắc nhất để thăng vòng đấu tiếp theo.

1. **Khóa cổng nộp bài thi**:
   * `- PATCH /api/v1/admin/rounds/{roundId}/close-submission` — BTC chủ động khóa cổng nộp bài khi hết giờ (hoặc hệ thống tự động khóa dựa trên `EndSubmission` của round).
2. **Kết thúc vòng thi và chốt sổ**:
   * API 57: [`POST /api/v1/rounds/{roundId}/end`](Rounds/POST/POST-api-v1-rounds-roundId-end.md) — Staff/Admin chính thức kết thúc vòng thi, khóa bài nộp, chốt sổ điểm và tính điểm trung bình cuối cùng của round.
3. **Tự động thăng vòng theo top-N**:
   * Khi kết thúc round, hệ thống tìm round kế tiếp trong cùng event (`RoundNo` hiện tại + 1).
   * Hệ thống lấy `LimitTeam` của round kế tiếp làm số lượng team được đi tiếp. Ví dụ: Round 2 có `limitTeam = 5` thì kết thúc Round 1 sẽ lấy 5 team điểm cao nhất của Round 1 để tạo `RoundDetails` cho Round 2.
   * Nếu không còn round kế tiếp thì chỉ chốt sổ round hiện tại và không tạo thêm dữ liệu thăng vòng.
4. **Rà soát điểm và xếp hạng**:
   * *Xem bảng điểm tổng hợp*: `- GET /api/v1/staff/rounds/{roundId}/scores` — BTC xem bảng điểm trung bình từ các giám khảo chấm thi.
   * *Xem bảng xếp hạng round*: [`- GET /api/v1/staff/rounds/{roundId}/ranking`](Staff/GET/GET-api-v1-staff-rounds-roundId-ranking.md) — BTC xem danh sách sắp xếp điểm số trước/sau khi kết thúc round.
5. **Ghi chú API dư thừa**:
   * Các API đề xuất `advance`, `advance/top`, `round-details/{roundDetailId}/stopped`, `results/publish` không đưa vào luồng chính vì logic thăng vòng đã gộp vào API 57 `EndRound`.

---

## Luồng 2.8: Giải quyết phúc khảo (Regrade Workflow)
*Mô tả*: BTC tiếp nhận đơn khiếu nại điểm số từ thí sinh, phê duyệt chấm lại và chỉ định giám khảo thực hiện chấm lại.

1. **Xem danh sách và chi tiết khiếu nại**:
   * [`- GET /api/v1/staff/reports`](Staff/GET/GET-api-v1-staff-reports.md) — BTC lọc danh sách khiếu nại của thí sinh.
   * [`- GET /api/v1/staff/reports/{reportId}`](Staff/GET/GET-api-v1-staff-reports-reportId.md) — BTC xem chi tiết report, trong đó có `submissionId` để mở đúng bài nộp cần xử lý.
2. **Xử lý đơn phúc khảo**:
   * *Đồng ý chấm lại*: [`- POST /api/v1/staff/reports/{reportId}/regrade`](Staff/POST/POST-api-v1-staff-reports-reportId-regrade.md) — BTC đồng ý cho chấm lại bài nộp gắn với report.
   * *Giao việc cho Giám khảo*: [`- POST /api/v1/staff/reports/{reportId}/assign-judge`](Staff/POST/POST-api-v1-staff-reports-reportId-assign-judge.md) — Phân công judge khác/phù hợp chấm lại bài nộp theo `submissionId` của report (BR-REP-05).
   * *Từ chối đơn*: [`- PATCH /api/v1/staff/reports/{reportId}/status`](Staff/PATCH/PATCH-api-v1-staff-reports-reportId-status.md) — BTC từ chối phúc khảo (chuyển trạng thái Closed) kèm lý do giải thích rõ ràng.

---

## Luồng 2.9: Tổng kết & Khóa giải đấu (Leaderboard & Lock)
*Mô tả*: BTC tính toán bảng xếp hạng chung cuộc, trao giải thưởng và khóa Read-only toàn bộ dữ liệu giải đấu.

1. **Tính toán Leaderboard**:
   * [`- POST /api/v1/admin/events/{eventId}/leaderboard/recalculate`](Events/POST/POST-api-v1-admin-events-eventId-leaderboard-recalculate.md) — Hệ thống tính tổng điểm các vòng để ra điểm chung cuộc (BR-LB-03).
2. **Gán giải thưởng**:
   * [`- PATCH /api/v1/admin/leaderboards/{leaderBoardId}/details/{teamId}`](LeaderBoards/PATCH/PATCH-api-v1-admin-leaderboards-leaderBoardId-details-teamId.md) — BTC gán giải thưởng đạt được (`LevelAward`) cho các đội thi theo đúng cơ cấu giải.
3. **Công bố kết quả chung cuộc**:
   * [`- PATCH /api/v1/admin/events/{eventId}/leaderboard/publish`](Events/PATCH/PATCH-api-v1-admin-events-eventId-leaderboard-publish.md) — BTC công bố bảng vàng xếp hạng chung cuộc.
4. **Khóa Read-only sự kiện**:
   * [`- PATCH /api/v1/admin/events/{eventId}/leaderboard/lock`](Events/PATCH/PATCH-api-v1-admin-events-eventId-leaderboard-lock.md) — BTC khóa Leaderboard.
   * [`- PATCH /api/v1/admin/events/{eventId}/close`](Events/PATCH/PATCH-api-v1-admin-events-eventId-close.md) — Chuyển trạng thái Event thành Closed. Kể từ thời điểm này, toàn bộ điểm số, xếp hạng và bài nộp thi của event ở trạng thái chỉ đọc (BR-SCO-07, BR-LB-06).

---
---

# LUỒNG 3: GIẢNG VIÊN - MENTOR & JUDGE (ACADEMIC WORKFLOW)
Luồng này mô tả hoạt động của giảng viên được BTC phân công theo từng track. Một lecturer có thể được gán vai trò Mentor hoặc Judge trong event, sau đó được gán vào track cụ thể; chỉ được thao tác với các team thuộc track mình được phân công.

## Luồng 3.1: Hoạt động của Mentor theo Track
*Mô tả*: Mentor được phân công vào track nào thì đảm nhiệm các team thuộc track đó. Mentor chỉ xem thông tin team/chi tiết team và gửi thông báo một chiều cho các team trong track; team không phản hồi/chat ngược lại trong luồng mentor.

1. **Xem phân công Mentor theo track**:
   * [`- GET /api/v1/mentor/events`](Mentor/GET/GET-api-v1-mentor-events.md) — Mentor xem các event mà mình được gán vai trò Mentor.
   * [`- GET /api/v1/mentor/tracks`](Mentor/GET/GET-api-v1-mentor-tracks.md) — Mentor xem các track mình được phân công phụ trách trong từng event.
2. **Xem các team thuộc track được phân công**:
   * [`- GET /api/v1/mentor/tracks/{trackId}/teams`](Mentor/GET/GET-api-v1-mentor-tracks-trackId-teams.md) — Mentor xem danh sách team đã chọn/được gán vào track mình phụ trách.
   * [`- GET /api/v1/mentor/teams/{teamId}/progress`](Mentor/GET/GET-api-v1-mentor-teams-teamId-progress.md) — Mentor xem thông tin chi tiết team, topic/track và tiến độ/bài nộp của team đó; chỉ hợp lệ nếu team thuộc track mentor được phân công.
3. **Gửi thông báo một chiều trong track**:
   * [`- POST /api/v1/mentor/tracks/{trackId}/notifications`](Mentor/POST/POST-api-v1-mentor-tracks-trackId-notifications.md) — Mentor gửi thông báo một chiều tới các team trong track mình phụ trách.
   * `- GET /api/v1/mentor/tracks/{trackId}/notifications` — Mentor xem lại lịch sử thông báo đã gửi. Team chỉ đọc thông báo, không phản hồi trực tiếp qua API này.

---

## Luồng 3.2: Hoạt động chấm điểm của Judge theo Track (Judging Workflow)
*Mô tả*: Judge được phân công track nào thì chỉ xem và chấm bài của các team thuộc track đó. Judge không được xem/chấm các team ở track khác.

1. **Xem phân công Judge theo track**:
   * [`- GET /api/v1/judge/tracks`](Judge/GET/GET-api-v1-judge-tracks.md) — Judge xem danh sách track mình được phân công chấm điểm.
2. **Xem team/bài nộp cần chấm trong track**:
   * [`- GET /api/v1/judge/tracks/{trackId}/submissions`](Judge/GET/GET-api-v1-judge-tracks-trackId-submissions.md) — Judge xem danh sách bài nộp của các team thuộc track mình được phân công; đây là phạm vi chấm điểm hợp lệ.
3. **Chấm điểm bài thi trong phạm vi track**:
   * *Xem bài thi*: [`- GET /api/v1/submissions/{submissionId}`](Submissions/GET/GET-api-v1-submissions-submissionId.md) — Xem nội dung bài nộp của team thuộc track được phân công.
   * *Xem rubric tiêu chí*: [`- GET /api/v1/judge/submissions/{submissionId}/criteria`](Judge/GET/GET-api-v1-judge-submissions-submissionId-criteria.md) — Lấy danh sách tiêu chí chấm điểm chi tiết của submission hợp lệ.
   * *Nhập điểm*: [`- POST /api/v1/judge/submissions/{submissionId}/scores`](Judge/POST/POST-api-v1-judge-submissions-submissionId-scores.md) — Judge nhập điểm tổng (`TotalScore`), điểm chi tiết từng tiêu chí (`ScoreItems`) và feedback cho team thuộc track mình chấm.
4. **Cập nhật & Khóa điểm số**:
   * *Xem lại điểm đã chấm*: [`- GET /api/v1/judge/submissions/{submissionId}/scores/me`](Judge/GET/GET-api-v1-judge-submissions-submissionId-scores-me.md) — Judge xem lại điểm của chính mình trên submission thuộc track được phân công.
   * *Sửa điểm*: [`- PATCH /api/v1/judge/scores/{scoreId}`](Judge/PATCH/PATCH-api-v1-judge-scores-scoreId.md) — Judge sửa điểm trước khi finalized, chỉ với score do chính judge tạo trong track được phân công.
   * *Khóa điểm*: [`- POST /api/v1/judge/scores/{scoreId}/finalize`](Judge/POST/POST-api-v1-judge-scores-scoreId-finalize.md) — Judge xác nhận kết quả chấm điểm của mình.
5. **Chấm lại phúc khảo (Khi được BTC phân công)**:
   * *Chấm lại*: [`- POST /api/v1/judge/scores/{scoreId}/retake`](Judge/POST/POST-api-v1-judge-scores-scoreId-retake.md) — Nhập điểm chấm lại cho bài thi phúc khảo nếu judge được phân công xử lý phúc khảo trong track liên quan.
