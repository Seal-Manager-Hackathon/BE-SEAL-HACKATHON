# Bug fix: Judge API — 500 + empty results khi xem submissions

## Ngày fix: 2026-07-02

## Bug 1: 500 NullReferenceException

### API bị ảnh hưởng
- `GET /api/v1/judge/events/{eventId}/submissions`
- `GET /api/v1/judge/events/{eventId}/submissions/pending`

### Root cause
Query thiếu `.Include()` cho chain `RegisterTeam → Team/Track/Topic`. Code access trong memory `.RoundDetail.RegisterTeam.Team.Name` sau `.ToListAsync()` nhưng EF chưa load → **NullReferenceException** → 500.

**Fix:** Thêm 3 dòng Include:
```csharp
.Include(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Team)
.Include(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Track)
.Include(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Topic)
```

## Bug 2: `GetEventSubmissions` luôn trả về empty dù có submission

### API bị ảnh hưởng
- `GET /api/v1/judge/events/{eventId}/submissions`

### Root cause
Query có filter `x.RoundDetail.Round.EndSubmission.Value <= now` — chỉ trả về submission của các round **đã đóng nộp bài**.

Trong khi `GET /api/v1/judge/tracks/{trackId}/submissions` **không có** filter này. Kết quả:
- Gọi theo **track** → ra submission ✅
- Gọi theo **event** (cùng track, cùng round) → rỗng vì round chưa đóng ❌

**Fix:** Xoá 2 dòng filter `EndSubmission` khỏi `GetEventSubmissions()`.

### Vị trí fix
`Judges/Service.cs` — `GetEventSubmissions()` method, xoá:
```csharp
&& x.RoundDetail.Round.EndSubmission.HasValue
&& x.RoundDetail.Round.EndSubmission.Value <= now
```

## Bug 3: `GetJudgeAssignmentsQuery` luôn trả về rỗng (Judge không thấy track/submission nào)

### API bị ảnh hưởng
- `GET /api/v1/judge/tracks` (GetMyTracks)
- Tất cả Judge APIs lấy submissions

### Root cause
`GetJudgeAssignmentsQuery` dùng **2-step navigation** `x.AssignEvent.EventRole.Name` để filter judge role:
```csharp
x.AssignEvent.EventRole != null && x.AssignEvent.EventRole.Name == EventRoleEnum.Judge
```
EF Core dịch thành JOIN `AssignTracks → AssignEvents → EventRoles`. Trong 1 số trường hợp, JOIN 2-step không translate đúng → trả về 0 records → tất cả Judge API trả rỗng.

### Fix
Thay navigation 2-step bằng **subquery**:
```csharp
// Trước (có thể fail):
x.AssignEvent.EventRole != null &&
x.AssignEvent.EventRole.Name == EventRoleEnum.Judge

// Sau (subquery):
x.AssignEvent.EventRoleId != null &&
_dbContext.EventRoles.Any(er =>
    er.Id == x.AssignEvent.EventRoleId &&
    er.Name == EventRoleEnum.Judge &&
    !er.IsDisable)
```

### Vị trí fix
`Judges/Service.cs` — 2 chỗ:
1. `GetJudgeAssignmentsQuery()` — filter Judge role
2. `GetCurrentEventPendingSubmissions()` — filter Judge role

