# Project Context

## Overview
SEAL Hackathon Management System là backend quản lý hackathon end-to-end: account, team, event, registration, offline draw, submission, judging, report/regrade, advancement, leaderboard, notification.

## Tech
- .NET 8, C#, EF Core, PostgreSQL.
- 3-layer architecture: `Hackathon.Api` → `Hackathon.Service` → `Hackathon.Repository`.
- Entities: `Hackathon.Repository/Entity`.
- Enums: `Hackathon.Repository/Enum`.
- DbContext: `Hackathon.Repository/AppDbContext.cs`.
- Background jobs: Quartz.
- Auth: JWT + BCrypt Enhanced (SHA256) + Pepper.

## Schema
- Current schema: 27+ tables/entities in `Hackathon.Repository/AppDbContext.cs`.
- Tables/entities plural, fields singular.
- PK/FK: `Guid`.
- Time: `DateTimeOffset`.
- Soft-disable: `IsDisable` (no `IsActive`, no `IsDelete`).

## Removed concepts
Do not re-add unless user explicitly changes schema:

```text
Profile
Role
UserRole
Permissions
RolePermissions
UserPermissions
EventRolePermissions
UserEventRoles
TrackOfRound
TeamInEvent
AuditLogs
Chapters
ExamPapers
```

## Main data flow
```text
Users (`RoleEnum` on `Users.Role`)
-> Teams/TeamDetails
-> RegisterTeams
-> RoundDetails
-> Submissions
-> Scores/ScoreItems
-> LeaderBoards/LeaderBoardDetails
```

## Assignment flow
```text
Users
-> AssignEvents
-> EventRoles
-> AssignTracks
-> Tracks
```

## Report flow
```text
Users -> Reports (no FK to AssignEvent or Submission)
```

## Background job flow
```text
Quartz scheduler
-> AutoCloseExpiredEventsJob (10 min)
-> AutoRejectPendingRegistrationsJob (12 h)
-> ExpirePendingEmailVerificationsJob (2 min)
-> ExpirePendingInvitationsJob (15 min)
-> EndRoundJob (Channel-based BackgroundService)
```
