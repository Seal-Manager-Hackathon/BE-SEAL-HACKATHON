# GET - Shared (Staff/Lecturer/Admin)

## `GET /api/v1/me/assignments`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy danh sách phân công của user hiện tại (sự kiện, vai trò).
→ [📄 Doc chi tiết](../../ApiDocs/Users/GET/api-v1-me-assignments-get.md)

## `GET /api/v1/register-teams/staff/{registerTeamId}`
- **Policy:** `[Authorize(Policy = StaffLecturerOrAdminPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Staff, Lecturer, Admin
- **Ghi chú:** Lấy chi tiết đăng ký đội (dành cho Staff/Lecturer/Admin).
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/GET/api-v1-register-teams-staff-id-get.md)

## `GET /api/v1/register-teams/staff/register-teams/{registerTeamId}/submissions`
- **Policy:** `[Authorize(Policy = StaffOrAdminPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Staff, Admin
- **Ghi chú:** Lấy danh sách bài nộp của đội đã đăng ký. Query params: roundId.
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/GET/api-v1-register-teams-staff-register-teams-registerteamid-submissions-get.md)

## `GET /api/v1/register-teams/events/{eventId}/approved-teams`
- **Policy:** `[Authorize(Policy = StaffLecturerOrAdminPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Staff, Lecturer, Admin
- **Ghi chú:** Lấy danh sách đội đã được duyệt trong sự kiện.
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/GET/api-v1-register-teams-events-eventid-approved-teams-get.md)

## `GET /api/v1/register-teams/events/{eventId}/tracks/{trackId}/teams`
- **Policy:** `[Authorize(Policy = StaffLecturerOrAdminPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Staff, Lecturer, Admin
- **Ghi chú:** Lấy danh sách đội theo track trong sự kiện.
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/GET/api-v1-register-teams-events-eventid-tracks-trackid-teams-get.md)

## `GET /api/v1/users/{userId}`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy thông tin user theo ID.
→ [📄 Doc chi tiết](../../ApiDocs/Users/GET/api-v1-users-userId-get.md)

## `GET /api/v1/users/students`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Tìm kiếm sinh viên. Hỗ trợ query params.
→ [📄 Doc chi tiết](../../ApiDocs/Users/GET/api-v1-users-students-get.md)

## `GET /api/v1/users/profile`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy thông tin profile của user hiện tại.
→ [📄 Doc chi tiết](../../ApiDocs/Users/GET/api-v1-users-me-profile-get.md)

## `GET /api/v1/users/reports/me`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy danh sách báo cáo của user hiện tại. Hỗ trợ pagination.
→ [📄 Doc chi tiết](../../ApiDocs/Users/GET/api-v1-users-reports-me-get.md)

## `GET /api/v1/users/reports/{reportId}`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy chi tiết báo cáo theo ID.
→ [📄 Doc chi tiết](../../ApiDocs/Users/GET/api-v1-users-reports-reportId-get.md)

## `GET /api/v1/notifications/me`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy danh sách thông báo của user hiện tại. Hỗ trợ pagination.
→ [📄 Doc chi tiết](../../ApiDocs/Notifications/GET/GET-api-v1-notifications-me.md)

## `GET /api/v1/notifications/me/unread-count`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy số lượng thông báo chưa đọc.
→ [📄 Doc chi tiết](../../ApiDocs/Notifications/GET/GET-api-v1-notifications-me-unread-count.md)

## `GET /api/v1/invitations/me`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy danh sách lời mời của user hiện tại. Hỗ trợ pagination.
→ [📄 Doc chi tiết](../../ApiDocs/Invitations/GET/api-v1-invitations-me-get.md)

## `GET /api/v1/invitations/pending/count`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Lấy số lượng lời mời đang chờ xử lý.
→ [📄 Doc chi tiết](../../ApiDocs/Invitations/GET/api-v1-invitations-pending-count-get.md)
