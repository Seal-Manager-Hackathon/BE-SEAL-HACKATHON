# GET - Mentor

## `GET /api/v1/mentor/events`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Cần policy riêng cho Mentor khi tách role. Lấy danh sách sự kiện được phân công làm mentor.
→ [📄 Doc chi tiết](../../ApiDocs/Mentor/GET/api-v1-mentor-events-get.md)

## `GET /api/v1/mentor/tracks`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Cần policy riêng cho Mentor khi tách role. Lấy danh sách tracks được phân công. Query params: eventId.
→ [📄 Doc chi tiết](../../ApiDocs/Mentor/GET/api-v1-mentor-tracks-get.md)

## `GET /api/v1/mentor/tracks/{trackId}/teams`
- **Policy:** `[Authorize]`
- **Trạng thái:** `CẦN TÁCH`
- **Dùng chung với:** Tất cả roles đã xác thực
- **Ghi chú:** Cần policy riêng cho Mentor khi tách role. Lấy danh sách đội trong track. Hỗ trợ pagination.
→ [📄 Doc chi tiết](../../ApiDocs/Mentor/GET/api-v1-mentor-tracks-id-teams-get.md)
