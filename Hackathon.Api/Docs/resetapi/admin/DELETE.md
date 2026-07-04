# DELETE - Admin

## `DELETE /api/v1/admin/events/{eventId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Xóa event (soft-delete).

## `DELETE /api/v1/admin/tracks/{trackId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `TracksController.cs`
- **Ghi chú:** Xóa track (soft-delete).

## `DELETE /api/v1/admin/topics/{topicId}`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `TopicsController.cs`
- **Ghi chú:** Xóa topic (soft-delete).

## `DELETE /api/v1/admin/awards/{id}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Xóa award (soft-delete).

## `DELETE /api/v1/admin/rounds/{roundId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `AdminController.cs`
- **Ghi chú:** Xóa round (soft-delete).

## `DELETE /api/v1/admin/assign-tracks/{id}`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Gỡ phân công giảng viên khỏi track (soft-disable).

## `DELETE /api/v1/admin/assign-events/{id}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `AdminController.cs`
- **Ghi chú:** Gỡ phân công staff khỏi event (RemoveStaffAssignment).

## `DELETE /api/v1/admin/assign-tracks/{id}` (MỚI)
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Nguồn:** Chưa có
- **Ghi chú:** Cần tạo mới. Hiện tại: `DELETE /api/v1/staff/assign-tracks/{id}`. Tạo route admin riêng.

## `DELETE /api/v1/admin/register-teams/{registerId}` (MỚI)
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Nguồn:** Chưa có
- **Ghi chú:** Cần tạo mới. Xóa đơn đăng ký đội.
