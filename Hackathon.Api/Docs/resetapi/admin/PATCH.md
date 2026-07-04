# PATCH - Admin

## `PATCH /api/v1/admin/events/{eventId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Cập nhật thông tin event.

## `PATCH /api/v1/admin/events/{eventId}/publish`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Đăng event (chuyển từ Draft lên Published).

## `PATCH /api/v1/admin/events/{eventId}/unpublish`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Hủy đăng event (chuyển về Draft).

## `PATCH /api/v1/admin/events/{eventId}/close`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Đóng event sau khi kết thúc.

## `PATCH /api/v1/admin/events/{eventId}/restore`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Khôi phục event đã đóng/disable.

## `PATCH /api/v1/admin/events/{eventId}/leaderboard/lock`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Khóa bảng xếp hạng (không cho cập nhật).

## `PATCH /api/v1/admin/events/{eventId}/leaderboard/publish`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Đăng bảng xếp hạng chính thức.

## `PATCH /api/v1/admin/events/{eventId}/rounds/{roundId}/criteria/{templateId}/activate`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `CriticalController.cs`
- **Ghi chú:** Kích hoạt template criteria cho round.

## `PATCH /api/v1/admin/tracks/{trackId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `TracksController.cs`
- **Ghi chú:** Cập nhật thông tin track.

## `PATCH /api/v1/admin/tracks/{trackId}/visibility`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `TracksController.cs`
- **Ghi chú:** Cập nhật trạng thái hiển thị của track.

## `PATCH /api/v1/admin/topics/{topicId}`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `TopicsController.cs`
- **Ghi chú:** Cập nhật thông tin topic.

## `PATCH /api/v1/admin/rounds/{roundId}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `AdminController.cs`
- **Ghi chú:** Cập nhật thông tin round.

## `PATCH /api/v1/admin/rounds/{roundId}/restore`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `AdminController.cs`
- **Ghi chú:** Khôi phục round đã xóa/disable.

## `PATCH /api/v1/admin/assign-events/{id}/role`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Cập nhật vai trò của lecturer trong event (AssignEvents).

## `PATCH /api/v1/admin/leaderboards/{leaderBoardId}/details/{teamId}`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `LeaderBoardsController.cs`
- **Ghi chú:** Cập nhật chi tiết điểm trong leaderboard cho team.

## `PATCH /api/v1/admin/users/{userId}/role`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `AdminController.cs`
- **Ghi chú:** Thay đổi global role của user.

## `PATCH /api/v1/admin/teams/{teamId}/disable`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `TeamController.cs`
- **Ghi chú:** Vô hiệu hóa team.

## `PATCH /api/v1/admin/teams/{teamId}/enable`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `TeamController.cs`
- **Ghi chú:** Kích hoạt lại team đã bị vô hiệu hóa.

## `PATCH /api/v1/admin/awards/{id}`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** `EventsController.cs`
- **Ghi chú:** Cập nhật thông tin award.
