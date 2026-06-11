# Project Context

## Overview
SEAL Hackathon Management System là backend quản lý hackathon end-to-end: account, team, event, registration, offline draw, submission, judging, report/regrade, advancement, leaderboard, notification.

## Tech
- .NET 8, C#, EF Core, PostgreSQL.
- Main projects: `Hackathon.Api`, `Hackathon.Service`, `Hackathon.Repository`.
- Entities: `Hackathon.Repository/Entity`.
- Enums: `Hackathon.Repository/Enum`.
- DbContext: `Hackathon.Repository/AppDbContext.cs`.

## Schema
- Source of truth: `Hackathon2026.dbml`.
- Current schema: 29 tables.
- Tables/entities plural, fields singular.
- PK/FK: `Guid`.
- Time: `DateTimeOffset`.
- Soft-disable: `IsDisable`.
- No `IsActive`, no `IsDelete`.

## Removed concepts
Do not re-add unless user explicitly changes schema:

```text
Profile
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
Users/Roles
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
