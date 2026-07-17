# BE-SEAL-HACKATHON

SEAL Hackathon Management System — 3-layer .NET 8 backend (Api → Service → Repository).

## Modules
- Auth (JWT, BCrypt + Pepper, refresh rotation)
- Events (status machine: Draft → Published → Closed)
- Teams (invite, kick, leave, disband)
- Registration (auto-reject on deadline)
- Scoring (upsert, sum-based total, retake)
- Reports/Regrade (Pending → Resolved/Reject/Canceled)
- Leaderboard (auto-create on publish)
- Background Jobs (Quartz: close, reject, expire)
- Notifications (personal, team, system, mentor)

## Setup
```bash
dotnet restore
dotnet ef database update --project Hackathon.Repository --startup-project Hackathon.Api
dotnet run --project Hackathon.Api
```

## Seed Accounts
Password for all seed users: `String1@`

| Email | Role | Status |
|-------|------|--------|
| admin.active@test.local | Admin | Active |
| staff.active@test.local | Staff | Active |
| judge.active@test.local | Judge (Lecturer) | Active |
| mentor.active@test.local | Mentor (Lecturer) | Active |
| leader1@test.local | Student Leader | Active |
| member1@test.local | Student Member | Active |
