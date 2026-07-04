# PATCH - Judge

## `PATCH /api/v1/judge/scores/{scoreId}`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Cập nhật điểm đã chấm (trước khi finalize).
→ [📄 Doc chi tiết](../../ApiDocs/Judge/PATCH/api-v1-judge-scores-scoreId-patch.md)

## `PATCH /api/v1/judge/scores/{scoreId}/items/{scoreItemId}`
- **Policy:** `[Authorize(Policy = LecturerPolicy)]`
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Chỉ Judge
- **Ghi chú:** Cập nhật điểm từng tiêu chí trong bài chấm.
→ [📄 Doc chi tiết](../../ApiDocs/Judge/PATCH/api-v1-judge-scores-scoreId-items-scoreItemId-patch.md)
