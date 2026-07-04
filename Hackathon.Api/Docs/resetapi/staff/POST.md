# POST - Staff

Tổng hợp các API `POST` dành cho Staff.

---

## `POST /api/v1/staff/events/{eventId}/assign-lecturers`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Phân công một giảng viên vào event với vai trò Mentor (0) hoặc Judge (1). Request body gồm `lecturerId` và `eventRole`.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/POST/api-v1-staff-events-id-assign-lecturers-post.md)

## `POST /api/v1/staff/events/{eventId}/tracks/{trackId}/assign-lecturers`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Phân công một giảng viên vào track cụ thể (thường là Judge cho track đó).
→ [📄 Doc chi tiết](../../ApiDocs/Staff/POST/api-v1-staff-tracks-id-assign-judges-post.md)

## `POST /api/v1/staff/submissions/{submissionId}/assign-judges`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Phân công giám khảo chấm bài nộp. Request body chứa danh sách judge IDs.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/POST/api-v1-staff-submissions-id-assign-judges.md)

## `POST /api/v1/staff/reports/{reportId}/regrade`
- **Policy:** `StaffOrAdminPolicy` (class-level)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Admin
- **Ghi chú:** Duyệt yêu cầu regrade (chấm lại) cho một báo cáo.
→ [📄 Doc chi tiết](../../ApiDocs/Staff/POST/api-v1-staff-reports-reportId-regrade-post.md)

## `POST /api/v1/staff/rounds/{roundId}/end`
- **Policy:** `StaffOrAdminPolicy`
- **Trạng thái:** `CẦN TÁCH` (đang ở RoundsController, route `/api/v1/rounds/{roundId}/end`)
- **Dùng chung với:** Admin
- **Ghi chú:** Kết thúc một round (không phải final). Nên tách route staff riêng hoặc giữ nguyên nếu đã có policy phù hợp.
- **Chưa có doc riêng**

## `PUT /api/v1/register-teams/staff/{registerId}/approve`
- **Policy:** `StaffOrAdminPolicy`
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Admin
- **Ghi chú:** Duyệt đơn đăng ký đội. Đang dùng `PUT` thay vì `POST`. Nên tách thành `POST /api/v1/staff/register-teams/{registerId}/approve`.
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/PUT/api-v1-register-teams-staff-id-approve-put.md)

## `PUT /api/v1/register-teams/staff/{registerId}/reject`
- **Policy:** `StaffOrAdminPolicy`
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Admin
- **Ghi chú:** Từ chối đơn đăng ký đội kèm lý do. Đang dùng `PUT` thay vì `POST`. Nên tách thành `POST /api/v1/staff/register-teams/{registerId}/reject`.
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/PUT/api-v1-register-teams-staff-id-reject-put.md)
