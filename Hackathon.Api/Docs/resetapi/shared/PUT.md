# PUT - Shared (Staff/Lecturer/Admin)

## `PUT /api/v1/register-teams/staff/{registerId}/approve`
- **Policy:** `[Authorize(Policy = StaffOrAdminPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Staff, Admin
- **Ghi chú:** Duyệt đăng ký tham gia sự kiện của đội.
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/PUT/api-v1-register-teams-staff-id-approve-put.md)

## `PUT /api/v1/register-teams/staff/{registerId}/reject`
- **Policy:** `[Authorize(Policy = StaffOrAdminPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Staff, Admin
- **Ghi chú:** Từ chối đăng ký tham gia sự kiện của đội (kèm lý do).
→ [📄 Doc chi tiết](../../ApiDocs/RegisterTeams/PUT/api-v1-register-teams-staff-id-reject-put.md)
