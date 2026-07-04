# GET - Admin

## `GET /api/v1/admin/events/{eventId}/assignments`
- **Policy:** AdminPolicy
- **Trạng thái:** `CÓ SẴN` (đã sửa)
- **Dùng chung với:** (riêng)
- **Ghi chú:** Danh sách staff được phân công vào event. Phân trang. Chỉ lấy User.Role == Staff. Có filter mặc định: IsDisable=false.
- **Params:** `pageIndex`, `pageSize`
- **Khác biệt so với staff/assignments:** API này chỉ trả staff, còn staff/assignments trả lecturers (có filter EventRole)
→ [📄 Doc chi tiết](../../ApiDocs/Events/GET/api-v1-admin-events-id-assignments-get.md)
