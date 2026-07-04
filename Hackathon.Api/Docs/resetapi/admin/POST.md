# POST - Admin

## `POST /api/v1/admin/events`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Tạo event mới.

## `POST /api/v1/admin/events/{eventId}/staff`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Gán staff vào event.

## `POST /api/v1/admin/events/{eventId}/awards`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Tạo award cho event.

## `POST /api/v1/admin/events/{eventId}/tracks`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `TracksController.cs`
- **Ghi chú:** Tạo track mới trong event.

## `POST /api/v1/admin/events/{eventId}/rounds`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `AdminController.cs`
- **Ghi chú:** Tạo round mới cho event.

## `POST /api/v1/admin/tracks/{trackId}/topics`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `TracksController.cs`
- **Ghi chú:** Tạo topic mới trong track.

## `POST /api/v1/admin/events/{eventId}/rounds/{roundId}/criteria`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `CriticalController.cs`
- **Ghi chú:** Tạo criteria mới cho round.

## `POST /api/v1/admin/assign-events/{id}/tracks`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Gán event vào track (AssignEventToTrack).

## `POST /api/v1/admin/events/{eventId}/leaderboard/recalculate`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Tính lại leaderboard.

## `POST /api/v1/admin/notifications`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `AdminController.cs`
- **Ghi chú:** Gửi system notification.

## `POST /api/v1/admin/events/{eventId}/assign-lecturers`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Nguồn:** Chưa có
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `POST /api/v1/staff/events/{eventId}/assign-lecturers` (StaffOrAdminPolicy). Tạo route admin riêng để tách policy.

## `POST /api/v1/admin/events/{eventId}/tracks/{trackId}/assign-lecturers`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Nguồn:** Chưa có
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `POST /api/v1/staff/events/{eventId}/tracks/{trackId}/assign-lecturers` (StaffOrAdminPolicy). Tạo route admin riêng để tách policy.

## `POST /api/v1/admin/rounds/{roundId}/end`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Nguồn:** Chưa có
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `POST /api/v1/rounds/{roundId}/end` (StaffOrAdminPolicy). Tạo route admin riêng.

## `POST /api/v1/admin/rounds/{roundId}/endFinal`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Nguồn:** Chưa có
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `POST /api/v1/rounds/{roundId}/endFinal` (AdminPolicy). Tạo route admin riêng.
