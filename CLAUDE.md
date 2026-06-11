# AI Agent Guide - .NET Backend

## Read once per session
- Read this file once at the start of a session.
- Only read extra context when the task needs it.
- If more context is needed, read in this order:
  1. `doc/README.md`
  2. `doc/PROJECT.md`
  3. `doc/RULES.md`
  4. `doc/FEATURES.md`
  5. `Hackathon2026.dbml`

## Dev environment tips
- This is a .NET backend project using C#, EF Core, and PostgreSQL.
- Main projects are expected under `Hackathon.Api`, `Hackathon.Service`, and `Hackathon.Repository`.
- Entities live in `Hackathon.Repository/Entity`.
- Enums live in `Hackathon.Repository/Enum`.
- `AppDbContext` lives in `Hackathon.Repository/AppDbContext.cs`.
- Prefer existing project patterns before introducing new architecture.
- Do not scan the whole repo if the task is limited to known files.

## Schema and context rules
- Database/schema source of truth: `Hackathon2026.dbml`.
- Project context source: `doc/*.md`.
- If docs conflict with DBML, DBML wins.
- Tables/entities are plural; fields are singular.
- PK/FK use `Guid`.
- Time fields use `DateTimeOffset`.
- Soft-disable uses `IsDisable`.
- Do not use `IsActive` or `IsDelete`.
- If schema changes, update DBML, context docs, entities, and `AppDbContext` together.
- Do not add removed concepts unless the user explicitly changes schema: `Profile`, `Permissions`, `RolePermissions`, `UserPermissions`, `EventRolePermissions`, `UserEventRoles`, `TrackOfRound`, `TeamInEvent`, `AuditLogs`, `Chapters`, `ExamPapers`.

## Authorization rules
- Do not use dynamic Permission/RBAC tables.
- API authorization is hard-coded by role/action.
- Global roles: `Admin`, `Staff`, `Student`, `Lecture`.
- Event roles: `Mentor`, `Judge`.
- Event/track APIs must also check `AssignEvents`, `AssignTracks`, and business rules.

## Core business rules
- Student must complete profile before creating/joining a team.
- Profile fields live in `Users`; no `Profile` table.
- A student cannot join multiple teams in the same event.
- Team leader submits event registration and submissions.
- Staff approves/rejects whole teams.
- Offline track/topic draw; staff records the result.
- `Topics` are exam topics/papers.
- `RoundDetails` connects `Rounds` and `RegisterTeams`.
- Mentor supports and sends notices; mentor does not score.
- Judge scores assigned track submissions by criteria.
- Round score = average of judges' `Scores.TotalScore`.
- Reports/regrade use `Reports`; no separate `Appeals` table.
- Event leaderboard = total round scores.
- Year leaderboard = total event leaderboard scores.

## Coding standards
- Use modern C# and existing project style.
- Use file-scoped namespaces when adding new files.
- Use async/await for I/O.
- Do not block async with `.Result` or `.Wait()`.
- Do not swallow exceptions with empty catch blocks.
- Use DI and interfaces when appropriate.
- Naming: PascalCase for types/members, camelCase for locals.
- Match current entity and `AppDbContext` relationship patterns.

## Testing instructions
- Run the narrowest relevant build/test first.
- Prefer project-level checks, for example:
  - `dotnet build Hackathon.Repository/Hackathon.Repository.csproj`
  - `dotnet build Hackathon.Service/Hackathon.Service.csproj`
  - `dotnet build Hackathon.Api/Hackathon.Api.csproj`
- If a solution file exists, run `dotnet build` from the repo root before finalizing larger changes.
- Add or update tests for changed business logic when the test project exists.
- If build/test cannot be run, say why and list what was verified manually.

## Work rules
- Ask before broad, destructive, or cross-cutting changes.
- Do not delete files unless the user explicitly asks.
- Do not commit or push unless the user asks.
- When fixing bugs, state root cause before the fix.
- When changing schema/business rules, keep DBML, context docs, and code consistent.

## PR / handoff instructions
- Summarize changed files and why.
- Mention build/test results or why they were not run.
- Call out any schema/business-rule impact.
- Keep responses in Vietnamese, concise, and technical.
