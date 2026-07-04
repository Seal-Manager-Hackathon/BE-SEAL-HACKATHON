# DELETE - Admin

## `DELETE /api/v1/admin/events/{eventId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Xóa event (soft-delete).
→ [📄 Doc chi tiết](../../ApiDocs/Events/DELETE/api-v1-admin-events-id-delete.md)

## `DELETE /api/v1/admin/tracks/{trackId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Xóa track (soft-delete).
→ [📄 Doc chi tiết](../../ApiDocs/Tracks/DELETE/api-v1-admin-tracks-id-delete.md)

## `DELETE /api/v1/admin/topics/{topicId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Xóa topic (soft-delete).
→ [📄 Doc chi tiết](../../ApiDocs/Topics/DELETE/api-v1-admin-topics-id-delete.md)

## `DELETE /api/v1/admin/awards/{id}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Xóa award (soft-delete).
→ [📄 Doc chi tiết](../../ApiDocs/Events/DELETE/api-v1-admin-awards-id-delete.md)

## `DELETE /api/v1/admin/rounds/{roundId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Xóa round (soft-delete).
→ [📄 Doc chi tiết](../../ApiDocs/Admin/DELETE/api-v1-admin-rounds-id-delete.md)

## `DELETE /api/v1/admin/assign-tracks/{id}`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Staff (cùng policy)
- **Ghi chú:** Gỡ phân công giảng viên khỏi track (soft-disable).
→ [📄 Doc chi tiết](../../ApiDocs/Events/DELETE/api-v1-admin-assign-tracks-id-delete.md)

## `DELETE /api/v1/admin/assign-events/{id}`
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Cần tạo mới. Hiện tại: `DELETE /api/v1/staff/assign-events/{id}`. Tạo route admin riêng.
- **Chưa có doc — cần tạo mới**

## `DELETE /api/v1/admin/assign-tracks/{id}` (MỚI)
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Cần tạo mới. Hiện tại: `DELETE /api/v1/staff/assign-tracks/{id}`. Tạo route admin riêng.
- **Chưa có doc — cần tạo mới**

## `DELETE /api/v1/admin/register-teams/{registerId}` (MỚI)
- **Policy:** AdminPolicy
- **Trạng thái:** `MỚI`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Cần tạo mới. Xóa đơn đăng ký đội.
- **Chưa có doc — cần tạo mới**
