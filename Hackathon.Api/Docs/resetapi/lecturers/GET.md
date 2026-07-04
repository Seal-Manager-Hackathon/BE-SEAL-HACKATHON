# GET - Lecturers

## `GET /api/v1/lecturers/events`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Lecturer
- **Ghi chú:** Lấy danh sách sự kiện được phân công cho giảng viên. Hỗ trợ pagination.
→ [📄 Doc chi tiết](../../ApiDocs/Lecturers/GET/api-v1-lecturers-events-get.md)

## `GET /api/v1/lecturers/events/search`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Lecturer
- **Ghi chú:** Tìm kiếm sự kiện được phân công cho giảng viên.
→ [📄 Doc chi tiết](../../ApiDocs/Lecturers/GET/api-v1-lecturers-events-search-get.md)

## `GET /api/v1/lecturers/events/current`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Lecturer
- **Ghi chú:** Lấy danh sách sự kiện đang diễn ra của giảng viên.
→ [📄 Doc chi tiết](../../ApiDocs/Lecturers/GET/api-v1-lecturers-events-current-get.md)

## `GET /api/v1/lecturers/events/{eventId}/tracks`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Lecturer
- **Ghi chú:** Lấy danh sách tracks trong sự kiện mà giảng viên được phân công.
→ [📄 Doc chi tiết](../../ApiDocs/Lecturers/GET/api-v1-lecturers-events-eventid-tracks-get.md)

## `GET /api/v1/lecturers/rounds/{roundId}/submissions`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Lecturer
- **Ghi chú:** Lấy danh sách bài nộp của vòng thi (dành cho giảng viên).
→ [📄 Doc chi tiết](../../ApiDocs/Lecturers/GET/api-v1-lecturers-rounds-id-submissions-get.md)
