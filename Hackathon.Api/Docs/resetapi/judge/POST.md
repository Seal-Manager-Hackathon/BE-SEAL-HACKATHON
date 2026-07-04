# POST - Judge

## `POST /api/v1/judge/submissions/{submissionId}/scores`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Chấm điểm bài nộp.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/POST/api-v1-judge-submissions-submissionId-scores-post.md)

## `POST /api/v1/judge/submissions/{submissionId}/scores/mock`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Chấm điểm thử (mock) - điểm không chính thức.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/POST/api-v1-judge-submissions-submissionId-scores-mock-post.md)

## `POST /api/v1/judge/scores/{scoreId}/finalize`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Xác nhận điểm chính thức (không thể sửa sau khi finalize).
→ [📄 Doc chi tiết](../../ApiDocs/Judge/POST/api-v1-judge-scores-scoreId-finalize-post.md)

## `POST /api/v1/judge/scores/{scoreId}/retake`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Chấm lại bài nộp (regrade) sau khi có yêu cầu.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/POST/api-v1-judge-scores-scoreId-retake-post.md)
