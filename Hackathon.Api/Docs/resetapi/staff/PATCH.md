# PATCH - Staff

## `PATCH /api/v1/staff/events/{eventId}/teams/{teamId}/track`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Gán track cho một đội trong sự kiện.

## `PATCH /api/v1/staff/events/{eventId}/teams/{teamId}/topic`
- **Policy:** StaffPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Gán chủ đề (topic) cho một đội trong sự kiện.

## `PATCH /api/v1/staff/reports/{reportId}/status`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Cập nhật trạng thái của một báo cáo.

## `PATCH /api/v1/staff/users/{userId}/role`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Thay đổi vai trò (role) của một người dùng.

## `PATCH /api/v1/register-teams/staff/{registerId}/ban`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** RegisterTeamController.cs
- **Ghi chú:** Cấm (ban) một đội đã đăng ký.

## `PATCH /api/v1/register-teams/staff/{registerId}/unban`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** RegisterTeamController.cs
- **Ghi chú:** Bỏ cấm (unban) một đội đã đăng ký.
