# Create Background Job Skill Test Notes

## RED baseline

Scenario: user asks for a job type they will specify, used for background tasks, with example of daily deadline reminder emails.

Baseline behavior chose Quartz because the project already references Quartz packages and described creating a job plus registering Quartz in `Program.cs`.

Baseline risks and ambiguities:

- It did not require the user's exact job type before choosing the implementation.
- It assumed a daily email reminder example and selected a schedule without requiring timezone clarification.
- It mentioned safe scoped service/DbContext handling but mixed direct injection and manual scopes without a firm rule.
- It correctly identified that forgetting `AddQuartzHostedService` prevents jobs from running.
- It correctly identified concurrency, retry, logging, and duplicate email risks.

## Skill requirements derived from baseline

- Force clarification of job type, schedule, timezone, and duplicate/concurrency policy.
- Prefer Quartz for scheduled jobs because the project already references Quartz packages.
- Require `AddQuartzHostedService` for Quartz jobs.
- Require safe scoped dependency handling with per-execution scope when uncertain.
- Require `[DisallowConcurrentExecution]` unless overlap is intentional.
- Prohibit `Thread.Sleep`, `Task.Delay` loops, and fire-and-forget `Task.Run` for scheduled work.
- Require logs and idempotency considerations for email/notification jobs.
