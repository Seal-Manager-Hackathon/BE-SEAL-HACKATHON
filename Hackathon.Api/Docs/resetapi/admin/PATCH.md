# PATCH - Admin

## `PATCH /api/v1/admin/events/{eventId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Cập nhật thông tin event.
→ [📄 Doc chi tiết](../../ApiDocs/Events/PATCH/api-v1-admin-events-id-patch.md)

## `PATCH /api/v1/admin/events/{eventId}/publish`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đăng event (chuyển từ Draft lên Published).
→ [📄 Doc chi tiết](../../ApiDocs/Events/PATCH/api-v1-admin-events-id-publish-patch.md)

## `PATCH /api/v1/admin/events/{eventId}/unpublish`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Hủy đăng event (chuyển về Draft).
→ [📄 Doc chi tiết](../../ApiDocs/Events/PATCH/api-v1-admin-events-id-unpublish-patch.md)

## `PATCH /api/v1/admin/events/{eventId}/close`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đóng event sau khi kết thúc.
→ [📄 Doc chi tiết](../../ApiDocs/Events/PATCH/api-v1-admin-events-id-close-patch.md)

## `PATCH /api/v1/admin/events/{eventId}/restore`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Khôi phục event đã đóng/disable.
→ [📄 Doc chi tiết](../../ApiDocs/Events/PATCH/api-v1-admin-events-id-restore-patch.md)

## `PATCH /api/v1/admin/events/{eventId}/leaderboard/lock`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Khóa bảng xếp hạng (không cho cập nhật).
→ [📄 Doc chi tiết](../../ApiDocs/Events/PATCH/api-v1-admin-events-id-leaderboard-lock-patch.md)

## `PATCH /api/v1/admin/events/{eventId}/leaderboard/publish`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Đăng bảng xếp hạng chính thức.
→ [📄 Doc chi tiết](../../ApiDocs/Events/PATCH/api-v1-admin-events-id-leaderboard-publish-patch.md)

## `PATCH /api/v1/admin/events/{eventId}/rounds/{roundId}/criteria/{templateId}/activate`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Kích hoạt template criteria cho round.
→ [📄 Doc chi tiết](../../ApiDocs/Critical/PATCH/api-v1-admin-events-id-rounds-id-criteria-templateid-activate-patch.md)

## `PATCH /api/v1/admin/tracks/{trackId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Cập nhật thông tin track.
→ [📄 Doc chi tiết](../../ApiDocs/Tracks/PATCH/api-v1-admin-tracks-id-patch.md)

## `PATCH /api/v1/admin/tracks/{trackId}/visibility`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Cập nhật trạng thái hiển thị của track.
→ [📄 Doc chi tiết](../../ApiDocs/Tracks/PATCH/api-v1-admin-tracks-id-visibility-patch.md)

## `PATCH /api/v1/admin/topics/{topicId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Cập nhật thông tin topic.
→ [📄 Doc chi tiết](../../ApiDocs/Topics/PATCH/api-v1-admin-topics-id-patch.md)

## `PATCH /api/v1/admin/rounds/{roundId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Cập nhật thông tin round.
→ [📄 Doc chi tiết](../../ApiDocs/Admin/PATCH/api-v1-admin-rounds-id-patch.md)

## `PATCH /api/v1/admin/rounds/{roundId}/restore`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Khôi phục round đã xóa/disable.
→ [📄 Doc chi tiết](../../ApiDocs/Admin/PATCH/api-v1-admin-rounds-id-restore-patch.md)

## `PATCH /api/v1/admin/assign-events/{id}/role`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Cập nhật vai trò của lecturer trong event (AssignEvents).
→ [📄 Doc chi tiết](../../ApiDocs/Events/PATCH/api-v1-admin-assign-events-id-role-patch.md)

## `PATCH /api/v1/admin/leaderboards/{leaderBoardId}/details/{teamId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Cập nhật chi tiết điểm trong leaderboard cho team.
→ [📄 Doc chi tiết](../../ApiDocs/LeaderBoards/PATCH/api-v1-admin-leaderboards-leaderboardid-details-teamid-patch.md)

## `PATCH /api/v1/admin/users/{userId}/role`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Thay đổi global role của user.
→ [📄 Doc chi tiết](../../ApiDocs/Admin/PATCH/api-v1-admin-users-id-role-patch.md)

## `PATCH /api/v1/admin/teams/{teamId}/disable`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Vô hiệu hóa team.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/PATCH/api-v1-admin-teams-id-disable-patch.md)

## `PATCH /api/v1/admin/teams/{teamId}/enable`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Kích hoạt lại team đã bị vô hiệu hóa.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/PATCH/api-v1-admin-teams-id-enable-patch.md)

## `PATCH /api/v1/admin/awards/{id}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** (riêng)
- **Ghi chú:** Cập nhật thông tin award.
→ [📄 Doc chi tiết](../../ApiDocs/Events/PATCH/api-v1-admin-awards-id-patch.md)
