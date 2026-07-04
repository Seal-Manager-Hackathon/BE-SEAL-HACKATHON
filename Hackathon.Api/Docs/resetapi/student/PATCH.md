# PATCH - Student

## `PATCH /api/v1/teams/{teamId}/lock`
- **Policy:** `[Authorize(Policy = StaffOrAdminPolicy)]`
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Staff, Admin
- **Ghi chú:** Khóa đội (không cho thay đổi). Không phải student endpoint, cần tách sang Staff/Admin doc.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/PATCH/api-v1-teams-id-lock-patch.md)

## `PATCH /api/v1/teams/{teamId}/unlock`
- **Policy:** `[Authorize(Policy = StaffOrAdminPolicy)]`
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Staff, Admin
- **Ghi chú:** Mở khóa đội. Không phải student endpoint, cần tách sang Staff/Admin doc.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/PATCH/api-v1-teams-id-unlock-patch.md)
