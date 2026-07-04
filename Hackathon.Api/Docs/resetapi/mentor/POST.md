# POST - Mentor

## `POST /api/v1/mentor/tracks/{trackId}/notifications`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Cần policy riêng cho Mentor khi tách role. Gửi thông báo đến tất cả đội trong track.
→ [📄 Doc chi tiết](../../ApiDocs/Mentor/POST/api-v1-mentor-tracks-id-notifications-post.md)

## `POST /api/v1/mentor/teams/{teamId}/notifications`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Cần policy riêng cho Mentor khi tách role. Gửi thông báo đến một đội cụ thể. Query params: trackId.
→ [📄 Doc chi tiết](../../ApiDocs/Mentor/POST/api-v1-mentor-teams-id-notifications-post.md)
