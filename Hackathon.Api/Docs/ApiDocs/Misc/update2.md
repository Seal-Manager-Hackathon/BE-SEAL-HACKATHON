# Update 2 — Testing & Bug Fixes (2026-07-02)

> **Lưu ý:** Các thay đổi này CHƯA deploy lên server. Cần commit + push để test thực tế.

## 1. Scoring Formula — Sửa toàn bộ

### Công thức cũ
```csharp
// Dùng Score.TotalScore để tính điểm trung bình
AverageScore = scores.Where(s => s.TotalScore.HasValue).Average(s => s.TotalScore)
```

### Công thức mới
```
Với mỗi judge → lấy Score mới nhất (group by AssignTrackId, OrderByDesc UpdatedAt)
Với mỗi tiêu chí → điểm_tb = trung bình điểm của các judge ĐÃ CHẤM (có ScoreItems)
Điểm tổng = tổng(tiêu_chí.điểm_tb)
```

### Các API đã sửa
| API | File | Sửa |
|-----|------|-----|
| `CalculateTotalScore()` (helper) | Rounds/Service.cs | ✅ Mới — latest per judge → avg criteria → sum |
| `GetMyRoundScore` | Rounds/Service.cs | ✅ Thay `scores.Average(TotalScore)` = `criteriaScores.Sum(Avg)` |
| `GetRoundRanking` | Rounds/Service.cs | ✅ Thay `TotalScore.Average()` = `CalculateTotalScore()` |
| `GetStaffRoundSubmissions` | Rounds/Service.cs | ✅ Thay `assignedJudges.TotalScore.Average()` = `CalculateTotalScore()` |
| `GetLecturerRoundSubmissions` | Rounds/Service.cs | ✅ Thay `Scores.TotalScore.Average()` = `CalculateTotalScore()` |
| `BuildSubmissionScore` | Submissions/Service.cs | ✅ Thay `TotalScore.Average()` = `criteriaScores.Sum()` |
| `EndRound` | Rounds/Service.cs | ✅ Thay `validScores.Average()` = `CalculateTotalScore()` |
| `CloseAndAdvanceRoundAsync` | Rounds/Service.cs | ✅ Thay `validScores.Average()` = `CalculateTotalScore()` |
| `PublishLeaderBoard` | Events/Service.cs | ✅ DB query → in-memory: latest per judge → avg criteria → sum |
| `GetEventSubmissions` (pagination) | Judges/Service.cs | ✅ BaseResponse → BasePaginationResponse |
| `EndRoundFinal` | Rounds/Service.cs + Controller | ✅ API mới — force end round ngay |

## 2. Auto-calculate TotalScore khi chấm

### Trước
```csharp
score.TotalScore = request.TotalScore;  // Nhận từ FE
if (total != request.TotalScore) throw BadRequestException("SCORE_TOTAL_MISMATCH");
```

### Sau
```csharp
// BE tự tính từ ScoreItems
var autoTotalScore = request.Scores.Sum(x => x.Score);
score.TotalScore = autoTotalScore;
// Bỏ validate SCORE_TOTAL_MISMATCH
```

**File:** `Judges/Service.cs` — `CreateScore()` + `UpdateScore()` + `ValidateScoreRequest()`

## 3. Judges không thấy track/submission nào

### Root cause
`GetJudgeAssignmentsQuery` dùng navigation 2-step `x.AssignEvent.EventRole.Name` — EF Core không translate đúng trong 1 số môi trường → trả 0 records.

### Fix
Thay navigation 2-step bằng subquery:
```csharp
// Trước
x.AssignEvent.EventRole != null && x.AssignEvent.EventRole.Name == EventRoleEnum.Judge

// Sau
x.AssignEvent.EventRoleId != null &&
_dbContext.EventRoles.Any(er => er.Id == x.AssignEvent.EventRoleId && er.Name == EventRoleEnum.Judge)
```

**File:** `Judges/Service.cs` — `GetJudgeAssignmentsQuery()` + `GetCurrentEventPendingSubmissions()`

## 4. Missing Include gây 500

### `GetEventSubmissions` + `BuildSubmissionsResponse`
Thiếu Include chain `RegisterTeam → Team/Track/Topic` → access navigation trong memory → NullReferenceException → 500.

**Fix:** Thêm 3 dòng Include:
```csharp
.Include(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Team)
.Include(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Track)
.Include(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Topic)
```

### Các query trong Rounds/Service.cs
Thiếu `ScoreItems → CriteriaItem` Include cho `CalculateTotalScore()` (access `si.CriteriaItem.IsDisable`).

**Fix:** Thêm `.ThenInclude(x => x.ScoreItems).ThenInclude(x => x.CriteriaItem)` vào cả 4 query.

## 5. `GetEventSubmissions` luôn trả về empty

### Root cause
Query có filter `x.RoundDetail.Round.EndSubmission.Value <= now` — chỉ lấy round đã đóng. Xoá filter này.

## 6. `SearchSubmissions` — thêm isGraded + submission data

- Thêm query param `bool? isGraded`
- Response thêm: `submissionId`, `submissionStatus`, `submittedAt`, `scoreId`, `totalScore`
- Logic: chỉ lấy submission mới nhất per team

## 7. Pagination bugs

### `GetJudgeTeamSubmissions` (Judges/Service.cs)
Dùng `BasePagination(items, ...)` (full list) thay vì `BasePagination(paged, ...)` → phân trang không hoạt động.

### `GetSubmissions` (Submissions/Service.cs)
`GetSubmissionsRequest` không kế thừa `PaginationRequest`, không sanitize pagination → 500 khi pageIndex = 0.

## 8. `GetCurrentLecturerEvents` sai logic

### Trước
Chỉ lấy event đang diễn ra (`StartTime <= now <= EndTime`), throw 404 nếu không có.

### Sau
Lấy tất cả event lecturer được phân công (`Status != Draft`), trả empty list nếu không có.

## 9. Criteria Template — sửa IsDisable khi tạo

### Trước
Tạo template với `IsDisable = false` (active) → có thể active 2 template cùng lúc.

### Sau
Tạo template với `IsDisable = true` (inactive) → admin phải gọi `ActivateCriteria` để active.

**File:** `Criticals/Service.cs` — `CreateCriteria()`

## 10. API mới

| API | Mô tả |
|-----|-------|
| `POST /api/v1/rounds/{roundId}/endFinal` | Force kết thúc round (set EndTime = now) — chỉ Admin |

## Chưa test được trên server

Cần commit + push `git push origin develop` để deploy. Sau đó mới test thực tế được.
