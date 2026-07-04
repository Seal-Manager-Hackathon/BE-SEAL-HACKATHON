# DELETE - Staff

Tổng hợp các API `DELETE` dành cho Staff.

---

## `DELETE /api/v1/staff/assign-events/{id}`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Gỡ phân công giảng viên khỏi event (soft-disable record trong bảng AssignEvents). Nếu giảng viên đang là Judge và đã được phân công vào track, tự động gỡ luôn các AssignTracks liên quan.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/DELETE/api-v1-staff-assign-events-id-delete.md)

## `DELETE /api/v1/staff/assign-tracks/{id}`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Gỡ phân công giảng viên khỏi một track cụ thể (soft-disable record trong bảng AssignTracks).
→ [📄 Doc chi tiết](../../ApiDocs/Staff/DELETE/api-v1-staff-assign-tracks-id-delete.md)

## `DELETE /api/v1/register-teams/staff/{registerId}` *(không tồn tại)*
- **Trạng thái:** `MỚI`
- **Ghi chú:** Chưa có API xóa đơn đăng ký đội cho staff. Có thể cần trong tương lai nếu staff cần xóa đơn đăng ký lỗi/trùng.
