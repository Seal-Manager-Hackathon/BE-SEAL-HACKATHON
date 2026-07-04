# GET - Admin

## `GET /api/v1/admin/events`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `EventsController`. Danh sách event cho admin, bao gồm cả event đã disable.
→ [📄 Doc chi tiết](../../ApiDocs/Events/GET/api-v1-admin-events-get.md)

## `GET /api/v1/admin/events/{eventId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `EventsController`. Chi tiết event cho admin, bao gồm cả event đã disable.
→ [📄 Doc chi tiết](../../ApiDocs/Admin/GET/api-v1-admin-events-id-get.md)

## `GET /api/v1/admin/events/{eventId}/assignments`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `EventsController`. Danh sách phân công lecturer vào event.
→ [📄 Doc chi tiết](../../ApiDocs/Events/GET/api-v1-admin-events-id-assignments-get.md)

## `GET /api/v1/admin/events/{eventId}/setup-status`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Staff (cùng policy, không cần tách)
- **Ghi chú:** Đã có trong `EventsController`. Kiểm tra trạng thái setup của event.
→ [📄 Doc chi tiết](../../ApiDocs/Events/GET/api-v1-admin-events-id-setup-status-get.md)

## `GET /api/v1/admin/events/{eventId}/rounds`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `AdminController`. Danh sách round của event cho admin.
→ [📄 Doc chi tiết](../../ApiDocs/Admin/GET/api-v1-admin-events-id-rounds-get.md)

## `GET /api/v1/admin/rounds/{roundId}/submissions`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `AdminController`. Danh sách submission của round. Staff có route riêng `GET /api/v1/staff/rounds/{roundId}/submissions`.
- **Chưa có doc riêng — cần tạo mới**

## `GET /api/v1/admin/tracks/{trackId}/topics`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Staff (cùng policy, không cần tách)
- **Ghi chú:** Đã có trong `TracksController`. Danh sách topics của track cho admin/staff.
→ [📄 Doc chi tiết](../../ApiDocs/Tracks/GET/api-v1-admin-tracks-id-topics-get.md)

## `GET /api/v1/admin/events/{eventId}/rounds/{roundId}/criteria`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `CriticalController`. Danh sách criteria templates của round.
→ [📄 Doc chi tiết](../../ApiDocs/Critical/GET/api-v1-admin-events-id-rounds-id-criteria-get.md)

## `GET /api/v1/admin/teams`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `TeamController`. Danh sách teams cho admin.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/GET/api-v1-admin-teams-get.md)

## `GET /api/v1/admin/users`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `AdminController`. Danh sách users cho admin, filter theo role.
→ [📄 Doc chi tiết](../../ApiDocs/Admin/GET/api-v1-admin-users-get.md)

## `GET /api/v1/admin/users/search`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đã có trong `AdminController`. Tìm kiếm users cho admin.
→ [📄 Doc chi tiết](../../ApiDocs/Admin/GET/api-v1-admin-users-search-get.md)

## `GET /api/v1/admin/events/{eventId}/tracks`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** Public
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `GET /api/v1/events/{eventId}/tracks` (public, không auth). Tạo route admin riêng để admin xem tracks kể cả đã disable.
- **Chưa có doc — cần tạo mới**

## `GET /api/v1/admin/events/{eventId}/awards`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** Public
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `GET /api/v1/events/{eventId}/awards` (public, không auth). Tạo route admin riêng để admin quản lý awards.
- **Chưa có doc — cần tạo mới**

## `GET /api/v1/admin/events/{eventId}/leaderboard`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** Public
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `GET /api/v1/events/{eventId}/leaderboard` (public, không auth). Tạo route admin riêng.
- **Chưa có doc — cần tạo mới**

## `GET /api/v1/admin/events/{eventId}/summary`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** Public
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `GET /api/v1/events/{eventId}/summary` (public, không auth). Tạo route admin riêng.
- **Chưa có doc — cần tạo mới**

## `GET /api/v1/admin/tracks`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** Public
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `GET /api/v1/tracks` (public, không auth). Tạo route admin riêng để admin xem tất cả tracks.
- **Chưa có doc — cần tạo mới**

## `GET /api/v1/admin/events/{eventId}/teams`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** Staff
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `GET /api/v1/staff/events/{eventId}/teams` (StaffOrAdminPolicy). Tạo route admin riêng.
- **Chưa có doc — cần tạo mới**

## `GET /api/v1/admin/events/{eventId}/lecturers`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** Staff
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `GET /api/v1/staff/events/{eventId}/assignments` với filter role=Lecturer. Tạo route admin riêng.
- **Chưa có doc — cần tạo mới**

## `GET /api/v1/admin/events/{eventId}/lecturers/available`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** Staff
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `GET /api/v1/staff/events/{eventId}/lecturers/available` (StaffOrAdminPolicy). Tạo route admin riêng.
- **Chưa có doc — cần tạo mới**

## `GET /api/v1/admin/rounds/{roundId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** Public
- **Ghi chú:** Cần tạo mới. Hiện tại đang dùng chung `GET /api/v1/rounds/{roundId}` (public, không auth). Tạo route admin riêng.
- **Chưa có doc — cần tạo mới**
