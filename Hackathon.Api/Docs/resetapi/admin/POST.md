# POST - Admin

## `POST /api/v1/admin/events`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `EventsController`. Tạo event mới.
→ [📄 Doc chi tiết](../../ApiDocs/Events/POST/api-v1-admin-events-post.md)

## `POST /api/v1/admin/events/{eventId}/staff`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `EventsController`. Gán staff vào event.
→ [📄 Doc chi tiết](../../ApiDocs/Events/POST/api-v1-admin-events-id-staff-post.md)

## `POST /api/v1/admin/events/{eventId}/awards`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `EventsController`. Tạo award cho event.
→ [📄 Doc chi tiết](../../ApiDocs/Events/POST/api-v1-admin-events-id-awards-post.md)

## `POST /api/v1/admin/events/{eventId}/tracks`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `TracksController`. Tạo track mới trong event.
→ [📄 Doc chi tiết](../../ApiDocs/Tracks/POST/api-v1-admin-events-id-tracks-post.md)

## `POST /api/v1/admin/events/{eventId}/rounds`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `AdminController`. Tạo round mới cho event.
→ [📄 Doc chi tiết](../../ApiDocs/Admin/POST/api-v1-admin-events-id-rounds-post.md)

## `POST /api/v1/admin/tracks/{trackId}/topics`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Staff (cùng policy, không cần tách)
- **Ghi chú:** Đã có trong `TracksController`. Tạo topic mới trong track.
→ [📄 Doc chi tiết](../../ApiDocs/Tracks/POST/api-v1-admin-tracks-id-topics-post.md)

## `POST /api/v1/admin/events/{eventId}/rounds/{roundId}/criteria`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `CriticalController`. Tạo criteria mới cho round.
→ [📄 Doc chi tiết](../../ApiDocs/Critical/POST/api-v1-admin-events-id-rounds-id-criteria-post.md)

## `POST /api/v1/admin/assign-events/{id}/tracks`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Staff (cùng policy, không cần tách)
- **Ghi chú:** Đã có trong `EventsController`. Gán event vào track (AssignEventToTrack).
→ [📄 Doc chi tiết](../../ApiDocs/Events/POST/api-v1-admin-assign-events-id-tracks-post.md)

## `POST /api/v1/admin/events/{eventId}/leaderboard/recalculate`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Staff (cùng policy, không cần tách)
- **Ghi chú:** Đã có trong `EventsController`. Tính lại leaderboard.
→ [📄 Doc chi tiết](../../ApiDocs/Events/POST/api-v1-admin-events-id-leaderboard-recalculate-post.md)

## `POST /api/v1/admin/notifications`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `AdminController`. Gửi system notification.
→ [📄 Doc chi tiết](../../ApiDocs/Admin/POST/api-v1-admin-notifications-post.md)

## `POST /api/v1/admin/events/{eventId}/assign-lecturers`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** Staff
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `POST /api/v1/staff/events/{eventId}/assign-lecturers` (StaffOrAdminPolicy). Tạo route admin riêng để tách policy.
- **Chưa có doc — cần tạo mới**

## `POST /api/v1/admin/events/{eventId}/tracks/{trackId}/assign-lecturers`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** Staff
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `POST /api/v1/staff/events/{eventId}/tracks/{trackId}/assign-lecturers` (StaffOrAdminPolicy). Tạo route admin riêng để tách policy.
- **Chưa có doc — cần tạo mới**

## `POST /api/v1/admin/rounds/{roundId}/end`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** Staff
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `POST /api/v1/rounds/{roundId}/end` (StaffOrAdminPolicy). Tạo route admin riêng.
- **Chưa có doc — cần tạo mới**

## `POST /api/v1/admin/rounds/{roundId}/endFinal`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `POST /api/v1/rounds/{roundId}/endFinal` (AdminPolicy). Tạo route admin riêng.
- **Chưa có doc — cần tạo mới**
