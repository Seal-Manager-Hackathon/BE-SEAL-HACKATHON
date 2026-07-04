# PATCH - Shared (Staff/Lecturer/Admin)

## `PATCH /api/v1/users/profile`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Cập nhật thông tin profile.
→ [📄 Doc chi tiết](../../ApiDocs/Users/PATCH/api-v1-users-me-profile-patch.md)

## `PATCH /api/v1/users/me/avatar`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Cập nhật avatar. Form-data upload.
→ [📄 Doc chi tiết](../../ApiDocs/Users/PATCH/api-v1-users-me-avatar-patch.md)

## `PATCH /api/v1/notifications/{notificationId}/read`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Đánh dấu thông báo đã đọc.
→ [📄 Doc chi tiết](../../ApiDocs/Notifications/PATCH/PATCH-api-v1-notifications-notificationId-read.md)

## `PATCH /api/v1/notifications/read-all`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Đánh dấu tất cả thông báo đã đọc.
→ [📄 Doc chi tiết](../../ApiDocs/Notifications/PATCH/PATCH-api-v1-notifications-read-all.md)

## `PATCH /api/v1/notifications/all/disable`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Tắt tất cả thông báo.
→ [📄 Doc chi tiết](../../ApiDocs/Notifications/PATCH/PATCH-api-v1-notifications-all-disable.md)

## `PATCH /api/v1/register-teams/staff/{registerId}/ban`
- **Policy:** `[Authorize(Policy = StaffOrAdminPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Staff, Admin
- **Ghi chú:** Cấm đội tham gia sự kiện.
- **Chưa có doc riêng — dùng chung với Staff PATCH**

## `PATCH /api/v1/register-teams/staff/{registerId}/unban`
- **Policy:** `[Authorize(Policy = StaffOrAdminPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Staff, Admin
- **Ghi chú:** Bỏ cấm đội tham gia sự kiện.
- **Chưa có doc riêng — dùng chung với Staff PATCH**

## `POST /api/v1/rounds/{roundId}/end`
- **Policy:** `[Authorize(Policy = StaffOrAdminPolicy)]`
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Staff, Admin
- **Ghi chú:** Kết thúc vòng thi (sớm). HTTP POST nhưng mang tính chất cập nhật trạng thái.
→ [📄 Doc chi tiết](../../ApiDocs/Rounds/POST/api-v1-rounds-id-end-post.md)

## `POST /api/v1/rounds/{roundId}/endFinal`
- **Policy:** `[Authorize(Policy = AdminPolicy)]`
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Admin
- **Ghi chú:** Kết thúc vòng thi cuối cùng (dứt điểm). HTTP POST nhưng mang tính chất cập nhật trạng thái.
→ [📄 Doc chi tiết](../../ApiDocs/Rounds/POST/api-v1-rounds-id-endFinal-post.md)
