# GET - Staff

## `GET /api/v1/staff/events`
- **Policy:** StaffPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Lấy danh sách sự kiện dành cho staff (có phân trang).

## `GET /api/v1/staff/events/search`
- **Policy:** StaffPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Tìm kiếm sự kiện dành cho staff theo từ khóa (có phân trang).

## `GET /api/v1/staff/events/current`
- **Policy:** StaffLecturerOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Lấy danh sách sự kiện hiện tại dành cho staff/lecturer/admin.

## `GET /api/v1/staff/events/{eventId}/tracks`
- **Policy:** StaffPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Lấy danh sách track của một sự kiện (hỗ trợ filter keyword, isDisable, phân trang).

## `GET /api/v1/staff/events/{eventId}/teams`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Lấy danh sách đội đã được duyệt của một sự kiện (hỗ trợ filter keyword, status, isDisable, phân trang).

## `GET /api/v1/staff/events/{eventId}/assignments`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Lấy danh sách phân công giảng viên cho sự kiện (hỗ trợ filter EventRole, keyword, trackId, isDisable, phân trang).

## `GET /api/v1/staff/events/{eventId}/lecturers/available`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Lấy danh sách giảng viên có sẵn để phân công cho sự kiện.

## `GET /api/v1/staff/events/{eventId}/tracks/{trackId}/lecturers`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Lấy danh sách giảng viên đã được phân công vào một track (hỗ trợ filter isDisable).

## `GET /api/v1/staff/events/{eventId}/register-teams`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Lấy danh sách đăng ký đội của một sự kiện (hỗ trợ filter keyword, status, phân trang).

## `GET /api/v1/staff/tracks/{trackId}/topics`
- **Policy:** StaffPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Lấy danh sách chủ đề (topic) của một track (hỗ trợ filter keyword, isDisable, phân trang).

## `GET /api/v1/staff/rounds/{roundId}/submissions`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Lấy danh sách bài nộp (submission) của một vòng (round).

## `GET /api/v1/staff/reports`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Lấy danh sách báo cáo (report) với các bộ lọc.

## `GET /api/v1/staff/reports/{reportId}`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Lấy chi tiết một báo cáo theo ID.

## `GET /api/v1/staff/submissions/regrade`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Lấy danh sách bài nộp yêu cầu chấm lại (regrade).

## `GET /api/v1/register-teams/staff/{registerTeamId}`
- **Policy:** StaffLecturerOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** RegisterTeamController.cs
- **Ghi chú:** Lấy chi tiết đăng ký đội dành cho staff.

## `GET /api/v1/register-teams/staff/events/{eventId}`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** RegisterTeamController.cs
- **Ghi chú:** Lấy danh sách đăng ký đội theo sự kiện dành cho staff (hỗ trợ filter keyword, status, isDisable, phân trang).

## `GET /api/v1/register-teams/staff/register-teams/{registerTeamId}/submissions`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** RegisterTeamController.cs
- **Ghi chú:** Lấy danh sách bài nộp theo vòng của một đội đã đăng ký (hỗ trợ filter roundId).
