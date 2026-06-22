# Tổng hợp API SEAL Hackathon Management System (Theo Luồng Nghiệp Vụ)

Ngày rà soát: 2026-06-22

## Quy ước & Thống kê
- **Tổng số API đã có code (Được đánh số 1-59)**: **59** API
- **Tổng số API đề xuất còn thiếu (Bắt đầu bằng `-`)**: **114** API
- Dòng **có đánh số ở đầu**: API **đã có route/controller trong code hiện tại**.
- Dòng **không đánh số (bắt đầu bằng `-`)**: API **chưa thấy route/controller**, được suy luận từ business logic, DBML, entity, service, request/response và docs hiện có.
- Với API còn thiếu, mỗi dòng ghi rõ: route đề xuất, nội dung chính, quyền dự kiến, entity liên quan, lý do vì sao cần, và ghi chú nếu DB/schema hiện tại chưa đủ field.
- Route trong phần có đánh số lấy theo controller hiện tại, không lấy theo docs cũ nếu route đang lệch.

---

# CHI TIẾT CÁC LUỒNG API (HIỆN CÓ & CÒN THIẾU)

## 1. Auth & Account
1. [`POST /api/v1/auth/register`](Auth/POST/POST-api-v1-auth-register.md) — Đăng ký tài khoản student mới. Quyền: Public.
2. [`POST /api/v1/auth/login`](Auth/POST/POST-api-v1-auth-login.md) — Đăng nhập, trả access token/refresh token qua cookie. Quyền: Public.
3. [`POST /api/v1/auth/tokens/refresh`](Auth/POST/POST-api-v1-auth-tokens-refresh.md) — Refresh access token bằng refresh token. Quyền: Public.
4. [`POST /api/v1/auth/email-verifications`](Auth/POST/POST-api-v1-auth-email-verifications.md) — Verify email bằng token. Quyền: Public.
5. [`GET /api/v1/auth/me`](Auth/GET/GET-api-v1-auth-me.md) — Lấy thông tin user đang đăng nhập. Quyền: Authenticated.
6. [`POST /api/v1/auth/logout`](Auth/POST/POST-api-v1-auth-logout.md) — Đăng xuất và xóa auth cookies. Quyền: Authenticated.
7. [`PATCH /api/v1/auth/change-password`](Auth/PATCH/PATCH-api-v1-auth-change-password.md) — Đổi mật khẩu user đang đăng nhập. Quyền: Authenticated.
8. [`POST /api/v1/auth/forgot-password`](Auth/POST/POST-api-v1-auth-forgot-password.md) — Gửi yêu cầu quên mật khẩu. Quyền: Public.
9. [`POST /api/v1/auth/reset-password`](Auth/POST/POST-api-v1-auth-reset-password.md) — Reset mật khẩu bằng token. Quyền: Public.
10. [`POST /api/v1/auth/email-verifications/resend`](Auth/POST/POST-api-v1-auth-email-verifications-resend.md) — Gửi lại email verification. Quyền: Public.
- [`POST /api/v1/auth/tokens/revoke-all`](Auth/POST/POST-api-v1-auth-tokens-revoke-all.md) — Revoke toàn bộ refresh token của user hiện tại. Quyền: Authenticated. Entity: `RefreshTokens`. Lý do: Hỗ trợ logout khỏi tất cả thiết bị/bảo mật khi đổi pass.
- [`GET /api/v1/auth/sessions`](Auth/GET/GET-api-v1-auth-sessions.md) — Xem các phiên refresh token/thiết bị đang active. Quyền: Authenticated. Entity: `RefreshTokens`. Lý do: Quản lý phiên đăng nhập trực quan. DB đã có `IpAddress`, `UserAgent`, `DeviceLabel`, `RevokedAt`.

## 2. User Profile & Reports (Self-service)
11. [`GET /api/v1/users/profile`](Users/GET/GET-api-v1-users-profile.md) — Xem profile user đang đăng nhập. Quyền: Authenticated.
12. [`PATCH /api/v1/users/profile`](Users/PATCH/PATCH-api-v1-users-profile.md) — Cập nhật profile user. Quyền: Authenticated.
13. [`POST /api/v1/users/system-report`](Users/POST/POST-api-v1-users-system-report.md) — User gửi report/khiếu nại/hỗ trợ hệ thống. Quyền: Authenticated.
- [`PATCH /api/v1/users/me/avatar`](Users/PATCH/PATCH-api-v1-users-me-avatar.md) — Cập nhật avatar riêng. Quyền: Authenticated. Entity: `Users`. Lý do: Tách biệt luồng upload avatar với update thông tin cá nhân.
- [`GET /api/v1/users/reports/me`](Users/GET/GET-api-v1-users-reports-me.md) — User xem lại danh sách report/khiếu nại mình đã gửi. Quyền: Authenticated. Entity: `Reports`. Lý do: Có gửi report thì phải có xem lại lịch sử và trạng thái xử lý.
- [`GET /api/v1/users/reports/{reportId}`](Users/GET/GET-api-v1-users-reports-reportId.md) — User xem chi tiết tiến độ giải quyết report của mình. Quyền: Authenticated (Owner). Entity: `Reports`. Lý do: Theo dõi phản hồi từ Staff/Admin (`Reason` xử lý).

## 3. Admin User Management
- `GET /api/v1/admin/users` — Admin xem danh sách user toàn hệ thống. Quyền: Admin. Entity: `Users`. Filter: `keyword`, `role`, `status`, `isVerified`, `isDisable`. Lý do: Quản lý và tra cứu người dùng hệ thống.
- `GET /api/v1/admin/users/{userId}` — Admin xem chi tiết user (bao gồm lịch sử team, các event tham gia, các report đã gửi). Quyền: Admin. Entity: `Users` + liên quan. Lý do: Hỗ trợ thanh tra, điều tra trước khi ra quyết định ban/disable.
- `PATCH /api/v1/admin/users/{userId}` — Admin cập nhật thông tin user thủ công. Quyền: Admin. Entity: `Users`. Lý do: Sửa lỗi thông tin hộ user khi được yêu cầu.
- `PATCH /api/v1/admin/users/{userId}/role` — Admin thay đổi global role (`Admin/Staff/Student/Lecturer`). Quyền: Admin. Entity: `Users.Role`. Lý do: Gán quyền cho các nhân viên, giảng viên mới.
- `PATCH /api/v1/admin/users/{userId}/ban` — Admin ban user, ghi lý do (`BanReason`, `BannedAt`, `Status`). Quyền: Admin. Entity: `Users`. Lý do: Xử lý tài khoản vi phạm quy chế.
- `PATCH /api/v1/admin/users/{userId}/unban` — Admin mở ban user. Quyền: Admin. Entity: `Users`. Lý do: Hủy ban user.
- `PATCH /api/v1/admin/users/{userId}/disable` — Admin disable user (soft delete). Quyền: Admin. Entity: `Users.IsDisable`. Lý do: Tắt hoạt động tài khoản tạm thời.
- `PATCH /api/v1/admin/users/{userId}/enable` — Admin kích hoạt lại user bị disable. Quyền: Admin. Entity: `Users.IsDisable`. Lý do: Khôi phục hoạt động tài khoản.

## 4. Event Setup & Lifecycle
14. [`GET /api/v1/events`](Events/GET/GET-api-v1-events.md) — Xem danh sách event public/filter theo keyword/year/status. Quyền: Public/Authenticated.
15. [`GET /api/v1/events/{eventId}`](Events/GET/GET-api-v1-events-eventId.md) — Xem chi tiết event. Quyền: Public/Authenticated.
16. [`GET /api/v1/events/most-participants`](Events/GET/GET-api-v1-events-most-participants.md) — Lấy danh sách event có nhiều participant nhất. Quyền: Public/Authenticated.
18. [`GET /api/v1/events/events/joined`](Events/GET/GET-api-v1-events-events-joined.md) — Lấy event user/team đã tham gia. **(Chú ý: route lặp chữ events, nên chỉnh lại thành `/api/v1/events/joined`)**. Quyền: Authenticated.
19. [`POST /api/v1/admin/events`](Events/POST/POST-api-v1-admin-events.md) — Admin tạo event (thông tin cơ bản). Quyền: Admin.
20. [`PATCH /api/v1/admin/events/{eventId}`](Events/PATCH/PATCH-api-v1-admin-events-eventId.md) — Admin cập nhật cấu hình event. Quyền: Admin.
21. [`DELETE /api/v1/admin/events/{eventId}`](Events/DELETE/DELETE-api-v1-admin-events-eventId.md) — Admin xóa/disable event. Quyền: Admin.
22. [`PATCH /api/v1/admin/events/{eventId}/publish`](Events/PATCH/PATCH-api-v1-admin-events-eventId-publish.md) — Admin publish công bố event. Quyền: Admin.
23. [`GET /api/v1/admin/events`](Events/GET/GET-api-v1-admin-events.md) — Admin xem danh sách event bao gồm cả hidden/disabled. Quyền: Admin.
- [`PATCH /api/v1/admin/events/{eventId}/unpublish`](Events/PATCH/PATCH-api-v1-admin-events-eventId-unpublish.md) — Admin đưa event từ Published về Draft/Ẩn. Quyền: Admin. Entity: `Events.Status`. Lý do: Tạm ẩn giải đấu khi có thay đổi đột xuất.
- [`PATCH /api/v1/admin/events/{eventId}/close`](Events/PATCH/PATCH-api-v1-admin-events-eventId-close.md) — Admin đóng event khi kết thúc. Quyền: Admin. Entity: `Events.Status = Closed`. Lý do: Chuyển trạng thái kết thúc cuộc thi.
- [`PATCH /api/v1/admin/events/{eventId}/cancel`](Events/PATCH/PATCH-api-v1-admin-events-eventId-cancel.md) — Admin hủy event. Quyền: Admin. Entity: `Events.Status = Cancelled`. Lý do: Hủy giải đấu khi có sự cố bất khả kháng.
- [`PATCH /api/v1/admin/events/{eventId}/restore`](Events/PATCH/PATCH-api-v1-admin-events-eventId-restore.md) — Khôi phục event bị delete mềm (disable). Quyền: Admin. Entity: `Events.IsDisable`. Lý do: Phục hồi lại event bị xóa nhầm.
- [`GET /api/v1/admin/events/{eventId}/setup-status`](Events/GET/GET-api-v1-admin-events-eventId-setup-status.md) — Kiểm tra xem event đã cấu hình đầy đủ chưa (đã gán rounds, criteria, tracks, topics, awards, staff chưa). Quyền: Admin/Staff. Entity: `Events` + liên quan. Lý do: Đảm bảo event đầy đủ cấu hình trước khi công bố.
- [`GET /api/v1/events/{eventId}/summary`](Events/GET/GET-api-v1-events-eventId-summary.md) — Xem tóm tắt nhanh số lượng team đăng ký, số track, số vòng thi. Quyền: Public/Auth. Entity: `Events`. Lý do: Vẽ dashboard/card hiển thị thông tin nhanh trên FE.

## 5. Track & Topic Setup
17. [`GET /api/v1/events/{eventId}/tracks`](Events/GET/GET-api-v1-events-eventId-tracks.md) — Lấy danh sách track của event. Quyền: Public/Auth.
44. [`GET /api/v1/staff/events/{eventId}/tracks`](Staff/GET/GET-api-v1-staff-events-eventId-tracks.md) — Staff xem tracks của event để phục vụ vận hành. Quyền: Staff.
45. [`GET /api/v1/staff/tracks/{trackId}/topics`](Staff/GET/GET-api-v1-staff-tracks-trackId-topics.md) — Staff xem topics của track. Quyền: Staff.
49. [`GET /api/v1/tracks/{trackId}/teams/count`](Tracks/GET/GET-api-v1-tracks-trackId-teams-count.md) — Đếm số team trong track. Quyền: Public/Auth.
50. [`GET /api/v1/tracks/{trackId}/topics`](Tracks/GET/GET-api-v1-tracks-trackId-topics.md) — Lấy danh sách topic theo track. Quyền: Public/Auth.
51. [`GET /api/v1/events/{eventId}/register-teams/{registerTeamId}/topic`](Topics/GET/GET-api-v1-events-eventId-register-teams-registerTeamId-topic.md) — Lấy topic đã gán cho register team trong event. Quyền: Public/Auth.
- [`GET /api/v1/tracks`](Tracks/GET/GET-api-v1-tracks.md) — Search và liệt kê các track toàn hệ thống (có phân trang, keyword). Quyền: Public/Auth. Entity: `Tracks`. Lý do: Tìm kiếm track độc lập.
- [`GET /api/v1/tracks/{trackId}`](Tracks/GET/GET-api-v1-tracks-trackId.md) — Xem thông tin chi tiết một track. Quyền: Public/Auth. Entity: `Tracks`. Lý do: Xem thông tin bảng đấu và danh sách đề thi.
- [`POST /api/v1/admin/events/{eventId}/tracks`](Tracks/POST/POST-api-v1-admin-events-eventId-tracks.md) — Admin tạo track mới trong event. Quyền: Admin. Entity: `Tracks`. Lý do: Thiết lập bảng đấu.
- [`PATCH /api/v1/admin/tracks/{trackId}`](Tracks/PATCH/PATCH-api-v1-admin-tracks-trackId.md) — Admin cập nhật thông tin track (mô tả, số lượng team tối đa). Quyền: Admin. Entity: `Tracks`. Lý do: Sửa mô tả hoặc đổi số lượng team tối đa (`MaxTeam`).
- [`DELETE /api/v1/admin/tracks/{trackId}`](Tracks/DELETE/DELETE-api-v1-admin-tracks-trackId.md) — Admin disable/xóa track. Quyền: Admin. Entity: `Tracks.IsDisable`. Lý do: Loại bỏ bảng đấu.
- [`POST /api/v1/admin/tracks/{trackId}/topics`](Tracks/POST/POST-api-v1-admin-tracks-trackId-topics.md) — Admin/Staff tạo topic (đề thi) trong track. Quyền: Admin/Staff. Entity: `Topics`. Lý do: Tạo đề thi mới.
- [`PATCH /api/v1/admin/topics/{topicId}`](Topics/PATCH/PATCH-api-v1-admin-topics-topicId.md) — Admin/Staff cập nhật đề thi. Quyền: Admin/Staff. Entity: `Topics`. Lý do: Cập nhật đề, sửa link tài liệu. Việc sửa đề thi cần được ghi nhận/audit.
- [`DELETE /api/v1/admin/topics/{topicId}`](Topics/DELETE/DELETE-api-v1-admin-topics-topicId.md) — Admin/Staff xóa topic. Quyền: Admin/Staff. Entity: `Topics.IsDisable`.
- [`PATCH /api/v1/admin/tracks/{trackId}/show`](Tracks/PATCH/PATCH-api-v1-admin-tracks-trackId-visibility.md) và `hide` — Staff/Admin ẩn/hiện bảng đấu. Quyền: Admin/Staff. Entity: `Tracks`. Lý do: Ẩn bảng đấu khi chưa tới giờ bốc thăm. *DB hiện chưa có trường visibility.*
- [`PATCH /api/v1/admin/topics/{topicId}/show`](Topics/PATCH/PATCH-api-v1-admin-topics-topicId-visibility.md) và `hide` — Staff/Admin ẩn/hiện đề thi. Quyền: Admin/Staff. Entity: `Topics`. Lý do: Giữ bí mật đề thi cho tới khi bắt đầu vòng làm bài.

## 6. Awards Setup
- [`GET /api/v1/events/{eventId}/awards`](Events/GET/GET-api-v1-events-eventId-awards.md) — Xem danh sách giải thưởng của event. Quyền: Public. Entity: `Awards`. Lý do: Công bố cơ cấu giải cho thí sinh.
- [`POST /api/v1/admin/events/{eventId}/awards`](Events/POST/POST-api-v1-admin-events-eventId-awards.md) — Admin tạo giải thưởng mới. Quyền: Admin. Entity: `Awards`. Body: `Name`, `Description`, `LevelAward`, `NumberOfAward`, `Prize`. Lý do: Cấu hình giải thưởng cho cuộc thi.
- [`PATCH /api/v1/admin/awards/{awardId}`](Events/PATCH/PATCH-api-v1-admin-awards-awardId.md) — Admin cập nhật giải thưởng. Quyền: Admin. Entity: `Awards`. Lý do: Sửa đổi giá trị hoặc tên giải thưởng.
- [`DELETE /api/v1/admin/awards/{awardId}`](Events/DELETE/DELETE-api-v1-admin-awards-awardId.md) — Admin xóa giải thưởng. Quyền: Admin. Entity: `Awards.IsDisable`.

## 7. Team Management
24. [`GET /api/v1/teams/me`](Teams/GET/GET-api-v1-teams-me.md) — Lấy danh sách team của user đang đăng nhập. Quyền: Authenticated.
25. [`GET /api/v1/teams/{teamId}`](Teams/GET/GET-api-v1-teams-teamId.md) — Xem chi tiết team (thông tin chung, leader, danh sách member). Quyền: Authenticated.
26. [`POST /api/v1/teams`](Teams/POST/POST-api-v1-teams.md) — Tạo team mới (người tạo tự động làm Leader). Quyền: Authenticated.
27. [`POST /api/v1/teams/{teamId}/invitations`](Teams/POST/POST-api-v1-teams-teamId-invitations.md) — Leader mời member vào team qua Email. Quyền: Authenticated (Leader).
28. [`PUT /api/v1/teams/{teamId}`](Teams/PUT/PUT-api-v1-teams-teamId.md) — Cập nhật tên team. Quyền: Authenticated (Leader).
29. [`DELETE /api/v1/teams/{teamId}/members`](Teams/DELETE/DELETE-api-v1-teams-teamId-members.md) — Leader xóa member khỏi team. Quyền: Authenticated (Leader).
30. [`PUT /api/v1/teams/{teamId}/leader`](Teams/PUT/PUT-api-v1-teams-teamId-leader.md) — Chuyển quyền leader cho member khác. Quyền: Authenticated (Leader).
31. [`GET /api/v1/teams/{teamId}/events`](Teams/GET/GET-api-v1-teams-teamId-events.md) — Xem danh sách các event team đã đăng ký/tham gia; bấm vào event thì dùng API 15 để xem chi tiết event. Quyền: Authenticated.
32. [`GET /api/v1/teams/{teamId}/events/approved-count`](Teams/GET/GET-api-v1-teams-teamId-events-approved-count.md) — Đếm số event team đã được approve tham gia thi đấu. Quyền: Authenticated.
33. [`GET /api/v1/teams/{teamId}/events/latest`](Teams/GET/GET-api-v1-teams-teamId-events-latest.md) — Lấy event đăng ký mới nhất của team. Quyền: Authenticated.
- [`GET /api/v1/teams/{teamId}/members`](Teams/GET/GET-api-v1-teams-teamId-members.md) — Xem danh sách chi tiết thành viên của team kèm trạng thái (Active/Inactive). Quyền: Authenticated. Entity: `TeamDetails` + `Users`. Lý do: FE hiển thị danh sách thành viên ở trang quản lý team độc lập.
- [`POST /api/v1/teams/{teamId}/leave`](Teams/POST/POST-api-v1-teams-teamId-leave.md) — Thành viên tự rời khỏi team. Quyền: Authenticated (Member). Entity: `TeamDetails`. Lý do: User tự rút khỏi đội thi (chỉ khi team chưa khóa đăng ký).
- [`PATCH /api/v1/teams/{teamId}/lock`](Teams/PATCH/PATCH-api-v1-teams-teamId-lock.md) — Staff/Admin khóa cứng team (không cho sửa member, đổi tên). Quyền: Staff/Admin. Entity: `Teams.CanEdit = false`. Lý do: Tự động khóa khi team được duyệt vào giải.
- [`PATCH /api/v1/teams/{teamId}/unlock`](Teams/PATCH/PATCH-api-v1-teams-teamId-unlock.md) — Staff/Admin mở khóa team cho phép sửa đổi. Quyền: Staff/Admin. Entity: `Teams.CanEdit = true`.
- [`GET /api/v1/admin/teams`](Teams/GET/GET-api-v1-admin-teams.md) — Admin xem và tìm kiếm tất cả các team trong hệ thống. Quyền: Admin. Entity: `Teams`.
- [`PATCH /api/v1/admin/teams/{teamId}/disable`](Teams/PATCH/PATCH-api-v1-admin-teams-teamId-disable.md) — Admin disable team toàn cục. Quyền: Admin. Entity: `Teams.IsDisable`.

## 8. Invitations (Mời vào Team)
34. [`GET /api/v1/invitations/me`](Invitations/GET/GET-api-v1-invitations-me.md) — Xem danh sách invitation mời vào team gửi tới user hiện tại. Quyền: Authenticated.
35. [`POST /api/v1/invitations/{invitationId}/accept`](Invitations/POST/POST-api-v1-invitations-invitationId-accept.md) — Chấp nhận lời mời tham gia team. Quyền: Authenticated.
36. [`POST /api/v1/invitations/{invitationId}/reject`](Invitations/POST/POST-api-v1-invitations-invitationId-reject.md) — Từ chối lời mời tham gia team. Quyền: Authenticated.
- `DELETE /api/v1/invitations/{invitationId}` — Leader hủy lời mời đã gửi (khi lời mời còn pending). Quyền: Authenticated (Leader). Entity: `Invitations`. Lý do: Rút lại lời mời khi gửi nhầm hoặc thay đổi nhân sự.
- `POST /api/v1/invitations/{invitationId}/resend` — Gửi lại lời mời đã bị hết hạn (`Expired`). Quyền: Authenticated (Leader). Entity: `Invitations`.

## 9. Event Registration (RegisterTeams)
37. [`POST /api/v1/register-teams`](RegisterTeams/POST/POST-api-v1-register-teams.md) — Team leader đăng ký team tham gia event. Quyền: Authenticated (Leader).
38. [`GET /api/v1/register-teams/me`](RegisterTeams/GET/GET-api-v1-register-teams-me.md) — Xem các đăng ký event của team/user hiện tại. Quyền: Authenticated.
39. [`GET /api/v1/register-teams/{registerId}/rejection-reason`](RegisterTeams/GET/GET-api-v1-register-teams-registerId-rejection-reason.md) — Xem lý do bị reject của đơn đăng ký. Quyền: Authenticated (Team).
40. [`GET /api/v1/register-teams/staff/events/{eventId}`](RegisterTeams/GET/GET-api-v1-register-teams-staff-events-eventId.md) — Staff/Admin xem danh sách team đăng ký tham gia một event. Quyền: Staff/Admin.
41. [`GET /api/v1/register-teams/staff/{registerTeamId}`](RegisterTeams/GET/GET-api-v1-register-teams-staff-registerTeamId.md) — Staff/Admin xem chi tiết đơn đăng ký team (kèm thông tin profile của từng member). Quyền: Staff/Admin.
42. [`PUT /api/v1/register-teams/staff/{registerId}/approve`](RegisterTeams/PUT/PUT-api-v1-register-teams-staff-registerId-approve.md) — Staff/Admin duyệt cho team tham gia event. Quyền: Staff/Admin (Assigned).
43. [`PUT /api/v1/register-teams/staff/{registerId}/reject`](RegisterTeams/PUT/PUT-api-v1-register-teams-staff-registerId-reject.md) — Staff/Admin reject đơn đăng ký team kèm lý do. Quyền: Staff/Admin (Assigned).
- `PATCH /api/v1/register-teams/staff/{registerId}/ban` — BTC cấm (ban) team tham gia giải đấu này. Quyền: Staff/Admin. Entity: `RegisterTeams.IsBanned = true`. Lý do: Xử lý các đội vi phạm quy chế thi. *DB chưa có BanReason riêng cho RegisterTeams.*
- `PATCH /api/v1/register-teams/staff/{registerId}/disable` — Staff/Admin tạm tắt đăng ký của team. Quyền: Staff/Admin. Entity: `RegisterTeams.IsDisable`.

## 10. Offline Draw (Bốc thăm Track/Topic)
46. [`GET /api/v1/staff/events/{eventId}/teams`](Staff/GET/GET-api-v1-staff-events-eventId-teams.md) — Staff/Admin chọn event và xem danh sách team đã được duyệt (`Approved`) để chuẩn bị bốc thăm offline. Quyền: Staff/Admin.
47. [`PATCH /api/v1/staff/teams/{teamId}/track`](Staff/PATCH/PATCH-api-v1-staff-teams-teamId-track.md) — BTC chọn team rồi gán track/bảng đấu theo kết quả bốc thăm offline. Quyền hiện tại theo code: Staff.
48. [`PATCH /api/v1/staff/teams/{teamId}/topic`](Staff/PATCH/PATCH-api-v1-staff-teams-teamId-topic.md) — BTC chọn team rồi gán topic/đề thi theo kết quả bốc thăm offline; sau khi gán topic hệ thống tạo `RoundDetails` cho `RoundNo = 1`. Quyền hiện tại theo code: Staff.

> Ghi chú: Các API bulk/import/publish draw-results là đề xuất dư thừa so với flow hiện tại nên không đưa vào luồng chính. Flow chuẩn hiện tại là BTC chọn event → chọn team đã duyệt → gán track → gán topic trực tiếp cho từng team.

## 11. Round & Criteria Setup
52. [`GET /api/v1/rounds`](Rounds/GET/GET-api-v1-rounds.md) — Lấy danh sách round của event (ví dụ: Vòng loại, Vòng bán kết, Chung kết), sắp theo `RoundNo`; team mới vào event bắt đầu ở `RoundNo = 1`. Quyền: Public/Auth. **(Chú ý: route dùng query string `?eventId={eventId}`)**.
53. [`GET /api/v1/rounds/teams/{teamId}`](Rounds/GET/GET-api-v1-rounds-teams-teamId.md) — Lấy danh sách các vòng đấu của team, có thể filter theo eventId; round sau chỉ xuất hiện khi team được chọn top sau khi kết thúc round trước. Quyền: Authenticated.
54. [`GET /api/v1/rounds/register-teams/{registerTeamId}`](Rounds/GET/GET-api-v1-rounds-register-teams-registerTeamId.md) — Lấy thông tin round detail của register team (trạng thái đi tiếp/dừng lại). Quyền: Authenticated.
- [`GET /api/v1/rounds/{roundId}`](Rounds/GET/GET-api-v1-rounds-roundId.md) — Xem chi tiết round khi user bấm vào một vòng thi. Quyền: Public/Auth. Entity: `Rounds`. Lý do: FE cần màn hình chi tiết round trước khi xem tiêu chí, track và ranking theo round.
58. [`GET /api/v1/rounds/{roundId}/criteria`](Rounds/GET/GET-api-v1-rounds-roundId-criteria.md) — Lấy danh sách criteria (tiêu chí chấm điểm) theo round. Quyền: Public/Auth.
59. [`GET /api/v1/events/{eventId}/criteria`](Events/GET/GET-api-v1-events-eventId-criteria.md) — Lấy toàn bộ tiêu chí chấm điểm của event. Quyền: Public/Auth.
- `POST /api/v1/admin/events/{eventId}/rounds` — Admin tạo round thi đấu mới. Quyền: Admin. Entity: `Rounds`. Body: `Name`, `Description`, `RoundNo`, `StartTime`, `EndTime`, `StartSubmission`, `EndSubmission`, `LimitTeam`. Lý do: Thiết lập các vòng đấu cho giải.
- `PATCH /api/v1/admin/rounds/{roundId}` — Admin cập nhật thông tin vòng thi (sửa thời gian thi, hạn nộp bài). Quyền: Admin. Entity: `Rounds`. Lý do: Sửa đổi timeline vận hành.
- `DELETE /api/v1/admin/rounds/{roundId}` — Admin xóa/disable vòng thi. Quyền: Admin. Entity: `Rounds.IsDisable`.
- `POST /api/v1/admin/rounds/{roundId}/criteria-templates` — Tạo criteria template (bộ tiêu chí) cho round. Quyền: Admin. Entity: `CriteriaTemplates`. Body: `Title`, `Description`.
- `POST /api/v1/admin/criteria-templates/{criteriaTemplateId}/items` — Tạo criteria item (tiêu chí chấm điểm chi tiết). Quyền: Admin. Entity: `CriteriaItems`. Body: `Name`, `Description`, `Score` (điểm tối đa). Lý do: Cấu hình rubric chi tiết để Judge chấm điểm.
- `PATCH /api/v1/admin/criteria-items/{criteriaItemId}` — Admin cập nhật tiêu chí chi tiết. Quyền: Admin. Entity: `CriteriaItems`.
- `DELETE /api/v1/admin/criteria-items/{criteriaItemId}` — Xóa tiêu chí chi tiết. Quyền: Admin. Entity: `CriteriaItems.IsDisable`.
- `POST /api/v1/admin/rounds/{roundId}/criteria/copy` — Sao chép toàn bộ tiêu chí chấm điểm từ round khác sang round hiện tại. Quyền: Admin. Entity: `CriteriaTemplates` + `CriteriaItems`. Lý do: Nhân bản tiêu chí chấm nhanh cho các vòng thi tương đồng.
- `PATCH /api/v1/admin/rounds/{roundId}/criteria/lock` — Khóa bộ tiêu chí chấm điểm. Quyền: Admin. Entity: `Rounds`. Lý do: Tránh sửa đổi rubric khi đang trong quá trình chấm điểm (BR-EVT-06). *DB hiện chưa có trường IsCriteriaLocked.*

## 12. Submission Management
55. [`POST /api/v1/rounds/{roundId}/submit-assignment`](Rounds/POST/POST-api-v1-rounds-roundId-submit-assignment.md) — Team leader nộp bài thi cho round. Quyền: Authenticated (Leader).
56. [`GET /api/v1/rounds/{roundId}/submissions`](Rounds/GET/GET-api-v1-rounds-roundId-submissions.md) — Xem danh sách submissions của round. Quyền: Authenticated. **(Chú ý: route này hiện mở cho mọi user, cần check phân quyền tại service)**.
- [`GET /api/v1/rounds/{roundId}/my-submissions`](Rounds/GET/GET-api-v1-rounds-roundId-my-submissions.md) — Team xem lịch sử các lần nộp bài trong round; submission mới nhất được dùng để chấm khi hết hạn nộp. Quyền: Authenticated (Team member). Entity: `Submissions` + `RoundDetails`. Lý do: Màn hình chi tiết round có thẻ/nút "Bài nộp" để xem lịch sử bài nộp (BR-SUB-04/05).
- [`GET /api/v1/submissions/{submissionId}`](Submissions/GET/GET-api-v1-submissions-submissionId.md) — Xem chi tiết bài nộp, trạng thái `NotGraded` nếu chưa có điểm, hoặc điểm/kết quả nếu đã chấm. Quyền: Authenticated (Team/BTC/Judge). Entity: `Submissions` + `Scores`. Lý do: FE hiển thị chi tiết bài nộp và nút khiếu nại khi bài đã có kết quả.
- `DELETE /api/v1/submissions/{submissionId}` — Disable bài nộp thi. Quyền: Authenticated (Leader)/Staff. Entity: `Submissions.IsDisable`.
- `PATCH /api/v1/staff/submissions/{submissionId}/status` — Staff cập nhật trạng thái bài nộp (nộp muộn, bài thi không hợp lệ). Quyền: Staff/Admin. Entity: `Submissions.Status`.
- `PATCH /api/v1/admin/rounds/{roundId}/unlock-submission` — BTC mở khóa nộp bài sau deadline cho một team cụ thể kèm lý do. Quyền: Staff/Admin. Entity: `Rounds`. Lý do: Hỗ trợ nộp muộn có kiểm soát khi có sự cố được phê duyệt (BR-SUB-06).

## 13. Judging & Scoring (Chấm điểm)
- [`GET /api/v1/judge/tracks`](Judge/GET/GET-api-v1-judge-tracks.md) — Judge xem danh sách track/bảng đấu mình được phân công chấm điểm. Quyền: Lecturer + Judge Role. Entity: `AssignEvents` + `AssignTracks`. Lý do: Giới hạn phạm vi chấm theo track được phân công.
- [`GET /api/v1/judge/tracks/{trackId}/submissions`](Judge/GET/GET-api-v1-judge-tracks-trackId-submissions.md) — Judge xem danh sách bài nộp của các team thuộc track mình được phân công; Judge chỉ được chấm team trong track đó. Quyền: Judge assigned. Entity: `Submissions` + `AssignTracks`. Lý do: Lấy đúng danh sách bài thi cần chấm theo bảng đấu.
- [`GET /api/v1/judge/submissions/{submissionId}/criteria`](Judge/GET/GET-api-v1-judge-submissions-submissionId-criteria.md) — Judge lấy bộ tiêu chí chấm điểm áp dụng cho bài thi này. Quyền: Judge assigned. Entity: `CriteriaItems`. Lý do: Đổ dữ liệu rubric ra form chấm điểm.
- [`POST /api/v1/judge/submissions/{submissionId}/scores`](Judge/POST/POST-api-v1-judge-submissions-submissionId-scores.md) — Judge nhập điểm chấm bài thi (lưu điểm tổng và điểm chi tiết từng criteria item + feedback). Quyền: Judge assigned. Entity: `Scores` + `ScoreItems`. Lý do: Nhập điểm chính thức (BR-SCO-01, BR-SCO-03).
- [`GET /api/v1/judge/submissions/{submissionId}/scores/me`](Judge/GET/GET-api-v1-judge-submissions-submissionId-scores-me.md) — Judge xem lại điểm mình đã chấm cho bài thi này. Quyền: Judge assigned. Entity: `Scores` + `ScoreItems`. Lý do: Xem lại để sửa nếu chưa khóa sổ điểm.
- [`PATCH /api/v1/judge/scores/{scoreId}`](Judge/PATCH/PATCH-api-v1-judge-scores-scoreId.md) — Judge cập nhật lại điểm số. Quyền: Judge (Owner). Entity: `Scores` + `ScoreItems`. Lý do: Cập nhật điểm chấm (BR-SCO-06).
- [`POST /api/v1/judge/scores/{scoreId}/finalize`](Judge/POST/POST-api-v1-judge-scores-scoreId-finalize.md) — Judge xác nhận khóa điểm bài thi (không cho sửa nữa). Quyền: Judge (Owner). Entity: `Scores`. *DB hiện chưa có trường IsFinalized.*
- [`POST /api/v1/judge/scores/{scoreId}/retake`](Judge/POST/POST-api-v1-judge-scores-scoreId-retake.md) — Chấm lại điểm phúc khảo. Quyền: Judge assigned for regrade. Entity: `Scores.IsRetake = true`. Lý do: Lưu điểm phúc khảo riêng biệt (BR-SCO-04, BR-REP-06).
- [`POST /api/v1/judge/submissions/{submissionId}/scores/mock`](Judge/POST/POST-api-v1-judge-submissions-submissionId-scores-mock.md) — Lưu điểm chấm thử/chấm nháp. Quyền: Judge/Admin. Entity: `Scores.IsMock = true`.
- [`GET /api/v1/judge/scores/me`](Judge/GET/GET-api-v1-judge-scores-me.md) — Judge xem lịch sử tất cả các bài thi mình đã chấm trong event. Quyền: Lecturer + Judge Role. Entity: `Scores`.

## 14. Staff Scoring, Reveal & Regrade
57. [`POST /api/v1/rounds/{roundId}/end`](Rounds/POST/POST-api-v1-rounds-roundId-end.md) — Staff/Admin kết thúc round (tự động tính điểm trung bình cho các team). Quyền: Staff/Admin.
- `GET /api/v1/staff/submissions/{submissionId}/scores` — Staff xem toàn bộ điểm chi tiết của tất cả các judge chấm bài thi này. Quyền: Staff/Admin. Entity: `Scores` + `ScoreItems`. Lý do: Phát hiện giám khảo lệch điểm bất thường.
- `GET /api/v1/staff/rounds/{roundId}/scores` — Staff xem bảng điểm tổng hợp của vòng đấu. Quyền: Staff/Admin. Entity: `Scores` + `Submissions` + `RoundDetails`. Lý do: Xem thứ tự tổng sắp điểm số.
- `PATCH /api/v1/staff/scores/{scoreId}/reopen` — Staff mở lại điểm thi cho phép Judge chỉnh sửa. Quyền: Staff/Admin. Entity: `Scores`. Lý do: Sửa điểm khi có lỗi nhập liệu được duyệt.
- `PATCH /api/v1/staff/rounds/{roundId}/scores/reveal` — BTC công bố điểm số của vòng thi cho thí sinh biết. Quyền: Staff/Admin. Entity: `Rounds`. Lý do: Cho phép thí sinh xem điểm. Trước thời điểm này, judge không được xem điểm judge khác (BR-SCO-05). *DB chưa có ScoreRevealAt.*
- [`GET /api/v1/events/{eventId}/teams/{teamId}/scores`](Events/GET/GET-api-v1-events-eventId-teams-teamId-scores.md) — Xem chi tiết điểm của một team trong event theo từng round và từng tiêu chí chấm điểm. Quyền: Public/Auth tùy thời điểm reveal. Entity: `Scores` + `ScoreItems` + `CriteriaItems`. Lý do: Từ leaderboard event, user bấm vào một team để xem breakdown điểm theo round/criteria.
- [`GET /api/v1/rounds/{roundId}/scores/me`](Rounds/GET/GET-api-v1-rounds-roundId-scores-me.md) — Team xem kết quả/điểm round hiện tại; nếu chưa chấm thì trả trạng thái bài chưa được chấm. Quyền: Team member. Entity: `Scores` + `RoundDetails` + `Submissions`.

## 15. Advancement & Round Results (Thăng vòng)
57. [`POST /api/v1/rounds/{roundId}/end`](Rounds/POST/POST-api-v1-rounds-roundId-end.md) — Staff/Admin kết thúc round, chốt sổ điểm và tự động tạo `RoundDetails` cho top team vào round kế tiếp theo `LimitTeam` của round sau.
- [`GET /api/v1/staff/rounds/{roundId}/ranking`](Staff/GET/GET-api-v1-staff-rounds-roundId-ranking.md) — Staff/Admin xem ranking theo round để kiểm tra kết quả trước/sau khi kết thúc round; entity: `Scores`, `RoundDetails`, `RegisterTeams`.
- [`GET /api/v1/rounds/{roundId}/ranking`](Rounds/GET/GET-api-v1-rounds-roundId-ranking.md) — User xem ranking đã công bố; quyền: Public/Auth theo rule; entity: `Scores`, `RoundDetails`.
- [`GET /api/v1/rounds/teams/{teamId}`](Rounds/GET/GET-api-v1-rounds-teams-teamId.md) — Team xem các round mình đang/đã tham gia. Team mới vào event mặc định có `RoundNo = 1`; các round sau chỉ xuất hiện nếu team nằm trong top được chọn khi kết thúc round trước.

> Ghi chú: Các API đề xuất `advance`, `advance/top`, `round-details/{id}/stopped`, `results/publish` là dư thừa với flow hiện tại vì logic thăng vòng đã gộp vào API 57 `EndRound`. Không đưa các API đó vào luồng chính.

## 16. Leaderboard Management
- [`GET /api/v1/events/{eventId}/leaderboard`](Events/GET/GET-api-v1-events-eventId-leaderboard.md) — Xem bảng xếp hạng chung cuộc của một event (điểm event = tổng điểm các round), bao gồm thứ hạng hiện tại của từng team trong event. Quyền: Public/Auth. Entity: `LeaderBoards` + `LeaderBoardDetails`. Lý do: Xếp hạng chung cuộc giải đấu (BR-LB-03).
- [`GET /api/v1/leaderboards/year/{year}`](LeaderBoards/GET/GET-api-v1-leaderboards-year-year.md) — Xem bảng xếp hạng tích lũy theo năm/season. Quyền: Public/Auth. Entity: `LeaderBoards` + `LeaderBoardDetails`. Lý do: Xếp hạng tích lũy các mùa trong năm (BR-LB-04).
- `GET /api/v1/teams/{teamId}/leaderboards` — Xem thành tích xếp hạng lịch sử của một team. Quyền: Public/Auth. Entity: `LeaderBoardDetails`.
- [`POST /api/v1/admin/events/{eventId}/leaderboard/recalculate`](Events/POST/POST-api-v1-admin-events-eventId-leaderboard-recalculate.md) — BTC chạy tính toán/cập nhật lại leaderboard event từ điểm số các round. Quyền: Admin/Staff. Entity: `LeaderBoards` + `LeaderBoardDetails`. Lý do: Đồng bộ thứ hạng khi điểm số thay đổi.
- [`PATCH /api/v1/admin/leaderboards/{leaderBoardId}/details/{teamId}`](LeaderBoards/PATCH/PATCH-api-v1-admin-leaderboards-leaderBoardId-details-teamId.md) — BTC điều chỉnh điểm số leaderboard hoặc gán giải thưởng (`LevelAward`) thủ công. Quyền: Admin/Staff. Entity: `LeaderBoardDetails`. Lý do: Thiết lập danh hiệu Nhất, Nhì, Ba cho các đội (BR-LB-06).
- [`PATCH /api/v1/admin/events/{eventId}/leaderboard/publish`](Events/PATCH/PATCH-api-v1-admin-events-eventId-leaderboard-publish.md) — BTC công bố leaderboard sự kiện. Quyền: Admin/Staff. Entity: `LeaderBoards`.
- [`PATCH /api/v1/admin/events/{eventId}/leaderboard/lock`](Events/PATCH/PATCH-api-v1-admin-events-eventId-leaderboard-lock.md) — Khóa leaderboard (chuyển sang chế độ Read-only). Quyền: Admin/Staff. Entity: `LeaderBoards`. Lý do: Khóa cứng kết quả khi giải đấu kết thúc (BR-LB-06). *DB chưa có lock flag.*
- `GET /api/v1/admin/leaderboards/year/{year}/recalculate` — Tính toán lại leaderboard tích lũy năm. Quyền: Admin. Entity: `LeaderBoards`.

## 17. Reports & Regrade Management (Khiếu nại & Phúc khảo)
- [`GET /api/v1/staff/reports`](Staff/GET/GET-api-v1-staff-reports.md) — Staff/Admin xem danh sách các báo cáo/khiếu nại/yêu cầu hỗ trợ trong hệ thống. Quyền: Staff/Admin. Entity: `Reports`. Filter: `status`, `typeReport`, `eventId`. Lý do: Quản lý các case khiếu nại.
- [`GET /api/v1/staff/reports/{reportId}`](Staff/GET/GET-api-v1-staff-reports-reportId.md) — Staff xem chi tiết khiếu nại (bao gồm link file, ảnh minh chứng, bài nộp đính kèm). Quyền: Staff/Admin. Entity: `Reports`.
- [`PATCH /api/v1/staff/reports/{reportId}/status`](Staff/PATCH/PATCH-api-v1-staff-reports-reportId-status.md) — Staff cập nhật trạng thái xử lý report (`Open/Closed`) kèm lý do phản hồi. Quyền: Staff/Admin. Entity: `Reports.Status` + `Reports.Reason`. Lý do: Đóng/Mở khiếu nại (BR-REP-05).
- [`POST /api/v1/staff/reports/{reportId}/regrade`](Staff/POST/POST-api-v1-staff-reports-reportId-regrade.md) — BTC phê duyệt cho phép chấm lại bài thi (đánh dấu bài thi cần regrade). Quyền: Staff/Admin. Entity: `Reports` + `Scores.IsRetake`. Lý do: BTC phê chuẩn phúc khảo.
- [`POST /api/v1/staff/reports/{reportId}/assign-judge`](Staff/POST/POST-api-v1-staff-reports-reportId-assign-judge.md) — Phân công giám khảo (cũ hoặc mới) chấm lại bài thi phúc khảo. Quyền: Staff/Admin. Entity: `AssignTracks` + `Scores`.
- [`POST /api/v1/teams/{teamId}/submissions/{submissionId}/appeal`](Teams/POST/POST-api-v1-teams-teamId-submissions-submissionId-appeal.md) — Team leader gửi khiếu nại/phúc khảo cho bài nộp đã có kết quả; request truyền `submissionId` để Staff/Admin xem đúng bài và phân công judge khác chấm lại nếu cần. Quyền: Authenticated (Leader). Entity: `Reports` + `Submissions`.

## 18. Notifications (Thông báo)
- [`GET /api/v1/notifications/me`](Notifications/GET/GET-api-v1-notifications-me.md) — User xem danh sách thông báo cá nhân/thông báo team của mình. Quyền: Authenticated. Entity: `Notifications`.
- [`PATCH /api/v1/notifications/{notificationId}/read`](Notifications/PATCH/PATCH-api-v1-notifications-notificationId-read.md) — Đánh dấu thông báo đã đọc. Quyền: Authenticated (Owner). Entity: `Notifications.Status = Read`.
- [`PATCH /api/v1/notifications/read-all`](Notifications/PATCH/PATCH-api-v1-notifications-read-all.md) — Đánh dấu tất cả thông báo là đã đọc. Quyền: Authenticated. Entity: `Notifications`.
- [`POST /api/v1/staff/notifications`](Staff/POST/POST-api-v1-staff-notifications.md) — BTC gửi thông báo hệ thống (gửi cho một user, một team hoặc toàn bộ event). Quyền: Staff/Admin. Entity: `Notifications`. Body: `userId` (nullable), `teamId` (nullable), `title`, `description`. Lý do: BTC gửi thông báo chung. *Cần đổi mối quan hệ trong DB thành Nullable.*
- [`GET /api/v1/teams/{teamId}/notifications`](Teams/GET/GET-api-v1-teams-teamId-notifications.md) — Xem thông báo gửi riêng cho team. Quyền: Authenticated (Team member). Entity: `Notifications`.

## 19. Mentor APIs (Khu vực của Mentor)
- [`GET /api/v1/mentor/events`](Mentor/GET/GET-api-v1-mentor-events.md) — Mentor xem danh sách event mình được gán vai trò Mentor. Quyền: Lecturer + Mentor Role. Entity: `AssignEvents`.
- [`GET /api/v1/mentor/tracks`](Mentor/GET/GET-api-v1-mentor-tracks.md) — Mentor xem danh sách track mình phụ trách trong event. Quyền: Lecturer + Mentor Role. Entity: `AssignTracks`.
- [`GET /api/v1/mentor/tracks/{trackId}/teams`](Mentor/GET/GET-api-v1-mentor-tracks-trackId-teams.md) — Mentor xem danh sách các team thuộc track mình phụ trách; team nào chọn/được gán vào track đó thì thuộc phạm vi mentor đảm nhiệm. Quyền: Mentor assigned. Entity: `RegisterTeams` + `Teams`.
- [`GET /api/v1/mentor/teams/{teamId}/progress`](Mentor/GET/GET-api-v1-mentor-teams-teamId-progress.md) — Mentor xem thông tin chi tiết team và tiến độ bài làm, chỉ khi team thuộc track mentor được phân công. Quyền: Mentor assigned. Entity: `Submissions` + `RoundDetails`.
- [`POST /api/v1/mentor/tracks/{trackId}/notifications`](Mentor/POST/POST-api-v1-mentor-tracks-trackId-notifications.md) — Mentor gửi thông báo một chiều tới toàn bộ team trong track mình phụ trách; team chỉ đọc, không phản hồi/chat ngược lại qua luồng này. Quyền: Mentor assigned. Entity: `MentorNotifications`.
- `GET /api/v1/mentor/tracks/{trackId}/notifications` — Mentor xem lại lịch sử các thông báo mình đã gửi trong track. Quyền: Mentor assigned. Entity: `MentorNotifications`.

## 20. Staff / Lecturer Assignment Management
- [`GET /api/v1/admin/events/{eventId}/assignments`](Events/GET/GET-api-v1-admin-events-eventId-assignments.md) — Admin xem danh sách giảng viên/nhân sự đã được gán vào event. Quyền: Admin. Entity: `AssignEvents` + `EventRoles`.
- [`POST /api/v1/admin/events/{eventId}/lecturers`](Events/POST/POST-api-v1-admin-events-eventId-lecturers.md) — Admin phân công giảng viên làm Mentor hoặc Judge trong event. Quyền: Admin. Entity: `AssignEvents` + `EventRoles`. Body: `userId`, `eventRole` (Mentor/Judge). Lý do: Phân vai trò giảng viên tham gia giải đấu (BR-ASG-02).
- [`PATCH /api/v1/admin/assign-events/{assignEventId}/role`](Events/PATCH/PATCH-api-v1-admin-assign-events-assignEventId-role.md) — Admin thay đổi vai trò của giảng viên trong event. Quyền: Admin. Entity: `AssignEvents`.
- [`DELETE /api/v1/admin/assign-events/{assignEventId}`](Events/DELETE/DELETE-api-v1-admin-assign-events-assignEventId.md) — Admin gỡ giảng viên khỏi event. Quyền: Admin. Entity: `AssignEvents.IsDisable`.
- [`POST /api/v1/admin/assign-events/{assignEventId}/tracks`](Events/POST/POST-api-v1-admin-assign-events-assignEventId-tracks.md) — Admin/Staff gán Mentor/Judge vào track cụ thể. Quyền: Admin/Staff. Entity: `AssignTracks`. Lý do: Giới hạn phạm vi chấm thi theo phân công bảng (BR-ASG-03).
- [`DELETE /api/v1/admin/assign-tracks/{assignTrackId}`](Events/DELETE/DELETE-api-v1-admin-assign-tracks-assignTrackId.md) — Gỡ Mentor/Judge khỏi track. Quyền: Admin/Staff. Entity: `AssignTracks.IsDisable`.
- [`GET /api/v1/me/assignments`](Users/GET/GET-api-v1-me-assignments.md) — Giảng viên/Nhân sự tự xem danh sách event/track mình được phân công phụ trách. Quyền: Authenticated (Staff/Lecturer). Entity: `AssignEvents` + `AssignTracks`.

## 21. Dashboards & System Metadata
- `GET /api/v1/admin/dashboard/summary` — Thống kê nhanh toàn hệ thống. Quyền: Admin.
- `GET /api/v1/admin/events/{eventId}/dashboard` — Dashboard chi tiết quản trị event. Quyền: Admin.
- `GET /api/v1/staff/events/{eventId}/dashboard` — Dashboard vận hành của Staff. Quyền: Staff assigned.
- `GET /api/v1/judge/events/{eventId}/dashboard` — Dashboard của Judge (tiến độ chấm thi). Quyền: Judge assigned.
- `GET /api/v1/mentor/events/{eventId}/dashboard` — Dashboard của Mentor (tiến độ nộp bài của các team). Quyền: Mentor assigned.
- [`GET /api/v1/enums`](System/GET/GET-api-v1-enums.md) — FE lấy danh sách enum value tập trung; các API có field enum/status sẽ kèm bảng enum ngay trong doc riêng của API đó. Quyền: Public/Auth.
- [`GET /api/v1/health`](System/GET/GET-api-v1-health.md) — Health check API. Quyền: Public.
- [`GET /api/v1/version`](System/GET/GET-api-v1-version.md) — Xem thông tin phiên bản backend. Quyền: Public.
- [`POST /api/v1/files/upload`](System/POST/POST-api-v1-files-upload.md) — API upload tài liệu/hình ảnh chung. Quyền: Authenticated. Lý do: Cung cấp giải pháp lưu trữ hình ảnh/file đính kèm cho FE.
