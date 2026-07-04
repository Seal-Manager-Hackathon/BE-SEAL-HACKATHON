# POST - Shared (Staff/Lecturer/Admin)

## `POST /api/v1/users/system-report`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Tạo báo cáo hệ thống.
→ [📄 Doc chi tiết](../../ApiDocs/Users/POST/api-v1-users-reports-post.md)

## `POST /api/v1/invitations/{invitationId}/accept`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Chấp nhận lời mời vào đội.
→ [📄 Doc chi tiết](../../ApiDocs/Invitations/POST/api-v1-invitations-id-accept-post.md)

## `POST /api/v1/invitations/{invitationId}/reject`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Từ chối lời mời vào đội.
→ [📄 Doc chi tiết](../../ApiDocs/Invitations/POST/api-v1-invitations-id-reject-post.md)
