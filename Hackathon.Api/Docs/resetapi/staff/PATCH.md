# PATCH - Staff

Tổng hợp các API `PATCH` dành cho Staff.

---

## `PATCH /api/v1/staff/events/{eventId}/teams/{teamId}/track`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Gán track cho đội trong event. Request body gồm `trackId`. Nếu đội đã có track, cập nhật sang track mới; nếu đã có topic thuộc track cũ thì reset topic.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/PATCH/api-v1-staff-teams-id-track-patch.md)

## `PATCH /api/v1/staff/events/{eventId}/teams/{teamId}/topic`
- **Policy:** `StaffPolicy`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin (yêu cầu StaffPolicy riêng, chặt hơn class-level)
- **Ghi chú:** Gán topic cho đội trong event. Yêu cầu quyền Staff (không áp dụng cho Admin mặc dù class có StaffOrAdminPolicy).
→ [📄 Doc chi tiết](../../ApiDocs/Staff/PATCH/api-v1-staff-teams-id-topic-patch.md)

## `PATCH /api/v1/staff/reports/{reportId}/status`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Cập nhật trạng thái báo cáo.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/PATCH/api-v1-staff-reports-reportId-status-patch.md)

## `PATCH /api/v1/staff/users/{userId}/role`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Thay đổi vai trò (global role) của người dùng. Request body chứa role mới.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/PATCH/api-v1-staff-users-id-role-patch.md)

## `PATCH /api/v1/register-teams/staff/{registerId}/ban`
- **Policy:** `StaffOrAdminPolicy`
- **Trạng thái:** `CÓ SẴN` (đang ở RegisterTeamController)
- **Dùng chung với:** Admin
- **Ghi chú:** Cấm đội tham gia sự kiện (soft ban). Request body gồm lý do cấm.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/PATCH/api-v1-staff-register-teams-id-ban-patch.md)

## `PATCH /api/v1/register-teams/staff/{registerId}/unban`
- **Policy:** `StaffOrAdminPolicy`
- **Trạng thái:** `CÓ SẴN` (đang ở RegisterTeamController)
- **Dùng chung với:** Admin
- **Ghi chú:** Bỏ cấm đội, khôi phục quyền tham gia sự kiện.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/PATCH/api-v1-staff-register-teams-id-unban-patch.md)
