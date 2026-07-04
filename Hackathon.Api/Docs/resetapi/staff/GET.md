# GET - Staff

Tổng hợp các API `GET` dành cho Staff.

---

## `GET /api/v1/staff/events`
- **Policy:** `StaffPolicy`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin (class-level `StaffOrAdminPolicy`)
- **Ghi chú:** Lấy danh sách sự kiện staff được phân công (không bao gồm Draft). Sắp xếp theo StartTime giảm dần.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/GET/api-v1-staff-events-get.md)

## `GET /api/v1/staff/events/search`
- **Policy:** `StaffPolicy`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin (class-level `StaffOrAdminPolicy`)
- **Ghi chú:** Tìm kiếm sự kiện theo từ khóa, hỗ trợ phân trang.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/GET/api-v1-staff-events-search-get.md)

## `GET /api/v1/staff/events/current`
- **Policy:** `StaffLecturerOrAdminPolicy`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Lecturer, Admin
- **Ghi chú:** Lấy sự kiện hiện tại (đang diễn ra) mà staff được phân công.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/GET/api-v1-staff-events-current-get.md)

## `GET /api/v1/staff/events/{eventId}/tracks`
- **Policy:** `StaffPolicy`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin (class-level `StaffOrAdminPolicy`)
- **Ghi chú:** Lấy danh sách tracks theo event. Hỗ trợ keyword, isDisable, phân trang.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/GET/api-v1-staff-events-tracks-get.md)

## `GET /api/v1/staff/tracks/{trackId}/topics`
- **Policy:** `StaffPolicy`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin (class-level `StaffOrAdminPolicy`)
- **Ghi chú:** Lấy danh sách topics theo track. Hỗ trợ keyword, isDisable, phân trang.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/GET/api-v1-staff-tracks-topics-get.md)

## `GET /api/v1/staff/events/{eventId}/teams`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Lấy danh sách đội đã được duyệt theo event. Hỗ trợ keyword, status, isDisable, phân trang.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/GET/api-v1-staff-events-id-teams-get.md)

## `GET /api/v1/staff/events/{eventId}/register-teams`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Lấy danh sách đơn đăng ký (register teams) theo event. Hỗ trợ keyword, status, phân trang. Không hỗ trợ isDisable.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/GET/api-v1-staff-events-eventid-register-teams-get.md)

## `GET /api/v1/staff/events/{eventId}/assignments`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Lấy danh sách phân công (giảng viên, mentor, judge) trong event. Hỗ trợ lọc theo eventRole, keyword, trackId, isDisable, phân trang.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/GET/api-v1-staff-events-id-assignments-get.md)

## `GET /api/v1/staff/events/{eventId}/lecturers/available`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Lấy danh sách giảng viên có sẵn để phân công vào event. Dùng cho form assign lecturer.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/GET/api-v1-staff-events-id-lecturers-available-get.md)

## `GET /api/v1/staff/events/{eventId}/tracks/{trackId}/lecturers`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Lấy danh sách giảng viên đã được phân công vào một track cụ thể. Hỗ trợ isDisable.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/GET/api-v1-staff-events-id-tracks-id-lecturers-get.md)

## `GET /api/v1/staff/rounds/{roundId}/submissions`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Lấy danh sách bài nộp của một round (dành cho staff). Hỗ trợ query filter.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/GET/api-v1-staff-rounds-id-submissions.md)

## `GET /api/v1/staff/reports`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Lấy danh sách báo cáo. Hỗ trợ filter, phân trang.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/GET/api-v1-staff-reports-get.md)

## `GET /api/v1/staff/reports/{reportId}`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Lấy chi tiết một báo cáo theo ID.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/GET/api-v1-staff-reports-reportId-get.md)

## `GET /api/v1/staff/submissions/regrade`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Lấy danh sách bài nộp yêu cầu regrade (chấm lại). Hỗ trợ filter, phân trang.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/GET/api-v1-staff-submissions-regrade-get.md)

## `GET /api/v1/register-teams/staff/events/{eventId}`
- **Policy:** `StaffOrAdminPolicy`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Lấy danh sách đơn đăng ký theo event (từ RegisterTeamController). Hỗ trợ keyword, status, isDisable, phân trang.
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/GET/api-v1-register-teams-staff-events-id-get.md)

## `GET /api/v1/register-teams/staff/{registerTeamId}`
- **Policy:** `StaffLecturerOrAdminPolicy`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Lecturer, Admin
- **Ghi chú:** Lấy chi tiết một đơn đăng ký (dành cho staff/lecturer/admin).
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/GET/api-v1-register-teams-staff-id-get.md)

## `GET /api/v1/register-teams/staff/register-teams/{registerTeamId}/submissions`
- **Policy:** `StaffOrAdminPolicy`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Lấy danh sách bài nộp của một đội đã đăng ký theo round. Hỗ trợ lọc theo roundId.
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/GET/api-v1-register-teams-staff-register-teams-registerteamid-submissions-get.md)
