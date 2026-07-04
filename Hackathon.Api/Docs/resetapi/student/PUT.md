# PUT - Student

## `PUT /api/v1/teams/{teamId}`
- **Policy:** `[Authorize(Policy = StudentPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Student
- **Ghi chú:** Cập nhật thông tin đội.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/PUT/api-v1-teams-id-put.md)

## `PUT /api/v1/teams/{teamId}/leader`
- **Policy:** `[Authorize(Policy = StudentPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Student
- **Ghi chú:** Chuyển quyền trưởng nhóm cho thành viên khác.
→ [📄 Doc chi tiết](../../ApiDocs/Teams/PUT/api-v1-teams-id-leader-put.md)
