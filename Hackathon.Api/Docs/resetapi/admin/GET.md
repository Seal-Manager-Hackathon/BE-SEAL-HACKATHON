# GET - Admin

## `GET /api/v1/admin/events`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN` — EventsController.cs
- **Ghi chú:** Danh sách tất cả events (kể cả IsDisable=true, Draft). Phân trang, search, filter status, year.

## `GET /api/v1/admin/events/{eventId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN` — EventsController.cs
- **Tách từ:** `GET /api/v1/events/{eventId}` (public → student-only)
- **Ghi chú:** Admin thấy tất cả (kể cả IsDisable=true, Draft). Student chỉ thấy Published/Closed & IsDisable=false.

## `GET /api/v1/admin/events/{eventId}/assignments`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN` (đã sửa) — EventsController.cs
- **Ghi chú:** Danh sách **staff** được phân công vào event. Phân trang. Chỉ `User.Role == Staff`.
- **Khác:** `staff/events/{eventId}/assignments` trả lecturers (có filter EventRole)

## `GET /api/v1/admin/events/{eventId}/setup-status`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN` — EventsController.cs
- **Dùng chung với:** Staff
- **Ghi chú:** Kiểm tra trạng thái setup (rounds, criteria, tracks, awards, staff).

## `GET /api/v1/admin/events/{eventId}/staff/available`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI` — EventsController.cs (vừa tạo)
- **Ghi chú:** Danh sách staff chưa được assign (Active, ko disable, ko trùng).

## `GET /api/v1/admin/events/{eventId}/rounds`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN` — AdminController.cs
- **Ghi chú:** Danh sách rounds của event (kể cả IsDisable=true). Filter IsDisable.

## `GET /api/v1/admin/rounds/{roundId}/submissions`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN` — AdminController.cs
- **Ghi chú:** Danh sách submissions của round. Filter track, topic, keyword, submission status, grading status.
- **Khác:** `staff/rounds/{roundId}/submissions` có EnsureStaffAssignedToEvent

## `GET /api/v1/admin/tracks/{trackId}/topics`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN` — TracksController.cs
- **Dùng chung với:** Staff
- **Ghi chú:** Danh sách topics của track (kể cả IsDisable).

## `GET /api/v1/admin/events/{eventId}/rounds/{roundId}/criteria`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN` — CriticalController.cs
- **Ghi chú:** Danh sách criteria templates của round (cả active & inactive).

## `GET /api/v1/admin/teams`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN` — TeamController.cs
- **Ghi chú:** Danh sách tất cả teams. Search, filter IsDisable.

## `GET /api/v1/admin/users`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN` — AdminController.cs
- **Ghi chú:** Danh sách users. Filter role, keyword.

## `GET /api/v1/admin/users/search`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN` — AdminController.cs
- **Ghi chú:** Tìm kiếm users nâng cao. Filter IsDisable, IsVerified, KeySearch, MailSearch, IdSearch, Role, StudentId.

---

### CÁC API MỚI CẦN THÊM (chưa có route /api/v1/admin/)

| API mới | API hiện tại đang dùng chung | Lý do tách |
|---------|-----------------------------|------------|
| `GET /api/v1/admin/events/{eventId}/tracks` | `GET /api/v1/events/{eventId}/tracks` (public) | Admin xem tracks kể cả IsDisable=true |
| `GET /api/v1/admin/events/{eventId}/awards` | `GET /api/v1/events/{eventId}/awards` (public) | Admin xem awards |
| `GET /api/v1/admin/events/{eventId}/leaderboard` | `GET /api/v1/events/{eventId}/leaderboard` (public) | Admin xem leaderboard mọi trạng thái |
| `GET /api/v1/admin/events/{eventId}/summary` | `GET /api/v1/events/{eventId}/summary` (public) | Admin xem summary mọi event |
| `GET /api/v1/admin/tracks` | `GET /api/v1/tracks` (public) | Admin xem tất cả tracks kể cả disable |
| `GET /api/v1/admin/events/{eventId}/register-teams` | `GET /api/v1/staff/events/{eventId}/register-teams` | Admin xem register-teams |
| `GET /api/v1/admin/events/{eventId}/lecturers` | `GET /api/v1/staff/events/{eventId}/assignments?eventRole=...` | Admin xem lecturers |
| `GET /api/v1/admin/events/{eventId}/lecturers/available` | `GET /api/v1/staff/events/{eventId}/lecturers/available` | Admin tách riêng |
| `GET /api/v1/admin/rounds/{roundId}` | `GET /api/v1/rounds/{roundId}` (public) | Admin xem round detail |
