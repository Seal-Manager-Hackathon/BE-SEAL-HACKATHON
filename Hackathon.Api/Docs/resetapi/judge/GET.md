# GET - Judge

## `GET /api/v1/judge/tracks`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge (giảng viên được phân công role Judge)
- **Ghi chú:** Lấy danh sách tracks được phân công chấm thi.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-tracks-get.md)

## `GET /api/v1/judge/tracks/{trackId}/submissions`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Lấy danh sách bài nộp của track. Query params: roundId, isGraded, PaginationRequest.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-tracks-trackId-submissions-get.md)

## `GET /api/v1/judge/submissions/{submissionId}/criteria`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Lấy danh sách tiêu chí chấm điểm cho bài nộp.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-submissions-submissionId-criteria-get.md)

## `GET /api/v1/judge/submissions/{submissionId}/scores/me`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Lấy điểm của giám khảo hiện tại cho một bài nộp.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-submissions-submissionId-scores-me-get.md)

## `GET /api/v1/judge/scores/me`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Lấy danh sách điểm đã chấm của giám khảo. Query params: eventId, trackId, isGraded, PaginationRequest.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-scores-me-get.md)

## `GET /api/v1/judge/submissions/regrade`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Lấy danh sách bài nộp yêu cầu chấm lại. Query params: eventId, trackId, isRegraded, PaginationRequest.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-submissions-regrade-get.md)

## `GET /api/v1/judge/events/{eventId}/submissions`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Lấy danh sách bài nộp trong sự kiện. Query params: trackId, roundId, PaginationRequest.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-events-eventid-submissions-get.md)

## `GET /api/v1/judge/events/current/submissions/pending`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Lấy danh sách bài nộp chưa chấm trong sự kiện hiện tại. Query params: trackId, roundId, PaginationRequest.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-events-current-submissions-pending-get.md)

## `GET /api/v1/judge/events/{eventId}/submissions/pending`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Lấy danh sách bài nộp chưa chấm trong sự kiện. Query params: trackId, roundId, isGraded, PaginationRequest.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-events-eventid-submissions-pending-get.md)

## `GET /api/v1/judge/events/{eventId}/submissions/search`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Tìm kiếm bài nộp trong sự kiện. Query params: trackId, keyword, isGraded, PaginationRequest.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-events-eventid-submissions-search-get.md)

## `GET /api/v1/judge/events/{eventId}/teams`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Lấy danh sách đội trong sự kiện (theo round). Query params: roundId.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-events-eventid-teams-get.md)

## `GET /api/v1/judge/events/{eventId}/rounds/{roundId}`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Lấy danh sách đội và trạng thái trong round cụ thể. Query params: trackId, status, PaginationRequest.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-events-eventid-rounds-roundid-get.md)

## `GET /api/v1/judge/rounds/{roundId}/submissions`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Lấy danh sách bài nộp (submissions) theo round. Query params: status, PaginationRequest.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-rounds-roundid-submissions-get.md)

## `GET /api/v1/judge/register-teams/{registerTeamId}/submissions`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Lấy danh sách bài nộp của một đội đã đăng ký.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/GET/api-v1-judge-register-teams-registerteamid-submissions-get.md)
