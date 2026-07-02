# Bug fix: 500 error khi Lecturer/Staff lấy submission của team chưa nộp bài

## Ngày fix: 2026-07-02

## Mô tả lỗi

Khi Lecturer hoặc Staff gọi API lấy danh sách submission của round mà có team chưa nộp bài, server trả về **500 Internal Server Error** (`ArgumentNullException`).

### Nguyên nhân (Root Cause)

Pattern code `submission?.Scores.Where(...)` bị null reference:

1. Khi team **không có submission** (`submission == null`)
2. `submission?.Scores` → `null` (do null-conditional `?.`)
3. `.Where(...)` — extension method gọi trên `null` → **`ArgumentNullException`**

Toán tử `?.` chỉ bảo vệ phần `.Scores`, còn `.Where()` vẫn được gọi trên kết quả `null`.

### Các API bị ảnh hưởng

| API | File | Method |
|-----|------|--------|
| `GET /api/v1/lecturers/rounds/{roundId}/submissions` | `Rounds/Service.cs` | `GetLecturerRoundSubmissions()` |
| `GET /api/v1/staff/rounds/{roundId}/submissions` | `Rounds/Service.cs` | `GetStaffRoundSubmissions()` → `BuildAssignedJudges()` |

### Chi tiết fix

#### 1. `GetLecturerRoundSubmissions()` — line ~904

**Trước (bug):**
```csharp
AverageScore = submission?.Scores
    .Where(s => !s.IsDisable && !s.IsMock && s.TotalScore.HasValue)
    .Select(s => s.TotalScore!.Value)
    .DefaultIfEmpty()
    .Average()
```

**Sau (fix):**
```csharp
AverageScore = submission?.Scores != null
    ? submission.Scores
        .Where(s => !s.IsDisable && !s.IsMock && s.TotalScore.HasValue)
        .Select(s => s.TotalScore!.Value)
        .DefaultIfEmpty()
        .Average()
    : null
```

→ Khi team chưa nộp bài: `averageScore = null` thay vì crash 500.

#### 2. `BuildAssignedJudges()` — line ~789

**Trước (bug):**
```csharp
var score = submission?.Scores
    .Where(x => !x.IsDisable && x.AssignTrackId == assignTrack.Id)
    .OrderByDescending(x => x.CreatedAt)
    .FirstOrDefault();
```

**Sau (fix):**
```csharp
var score = submission?.Scores
    ?.Where(x => !x.IsDisable && x.AssignTrackId == assignTrack.Id)
    ?.OrderByDescending(x => x.CreatedAt)
    ?.FirstOrDefault();
```

→ Thêm `?.` trước mỗi LINQ method để null propagate an toàn qua toàn bộ chain.

## Kết quả sau fix

| Tình huống | averageScore | HTTP |
|------------|-------------|------|
| Team chưa nộp bài | `null` | 200 |
| Team đã nộp, chưa chấm | `0` | 200 |
| Team đã nộp, đã chấm | Điểm trung bình | 200 |
