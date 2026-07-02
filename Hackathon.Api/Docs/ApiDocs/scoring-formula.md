# Scoring Formula — Quy tắc tính điểm

## Công thức (áp dụng từ 2026-07-02)

```
Mỗi submission:
  Bước 1: Lấy Score mới nhất của mỗi judge (group by AssignTrackId, OrderByDesc UpdatedAt)
          — retake thay thế bài cũ, không cộng dồn
  Bước 2: Với mỗi tiêu chí → điểm_tb = trung bình điểm của tất cả judge ĐÃ CHẤM
          (chỉ tính judge có ScoreItems, không tính judge chưa chấm)
  Bước 3: Điểm tổng = tổng(tiêu_chí.điểm_tb)

Event ranking (PublishLeaderBoard):
  Điểm event của 1 team = trung bình(điểm_tổng các round team đó tham gia)

Year ranking (GetYearLeaderboard):
  Điểm năm của 1 team = tổng(điểm các event trong năm)
```

## Quy tắc
- KHÔNG dùng `Score.TotalScore` để tính điểm tổng — chỉ dùng `ScoreItems`
- Chỉ tính judge đã chấm thực tế (có Score.ScoreItems với Score.HasValue)
- Judge chấm lại (retake) → bản cũ bị bỏ qua, chỉ lấy bản mới nhất
- IsMock bị loại khỏi mọi tính toán

## Các API bị ảnh hưởng

| API | File | Thay đổi |
|-----|------|----------|
| `GET /api/v1/submissions/{id}` | Submissions/Service.cs | `AverageTotalScore` = sum avg criteria thay vì avg TotalScore |
| `GET /api/v1/rounds/{id}/scores/me` | Rounds/Service.cs | `AverageTotalScore` = sum avg criteria thay vì avg TotalScore |
| `GET /api/v1/rounds/{id}/ranking` | Rounds/Service.cs | Ranking score = CalculateTotalScore |
| `GET /api/v1/staff/rounds/{id}/submissions` | Rounds/Service.cs | `AverageScore` = CalculateTotalScore |
| `GET /api/v1/lecturers/rounds/{id}/submissions` | Rounds/Service.cs | `AverageScore` = CalculateTotalScore |
| `POST /api/v1/admin/events/{id}/leaderboard` | Events/Service.cs | Score = avg criteria per round → avg across rounds |
| `POST /api/v1/admin/rounds/{id}/end` | Rounds/Service.cs | Ranking = CalculateTotalScore |
| `GET /api/v1/leaderboards/year/{year}` | LeaderBoards/Service.cs | Dùng score từ LeaderBoardDetails đã tính |
