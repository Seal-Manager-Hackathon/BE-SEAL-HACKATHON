---
name: create-background-job-skill
description: Use when adding or modifying background jobs, scheduled jobs, recurring tasks, Quartz jobs, hosted services, or non-request background processing in the Hackathon .NET backend
---

# Create Background Job Skill

## Overview

Create background jobs as explicit, scheduled, observable units of work. In this repository, prefer Quartz for scheduled jobs because `Hackathon.Service` already references `Quartz` and `Quartz.Extensions.Hosting`.

The user decides the job type and business purpose. Do not invent the task, schedule, or job technology when the request is ambiguous.

## Required User Inputs

Before editing, identify or ask for:

- Job type: scheduled/recurring, delayed one-time, fire-and-forget, cleanup, notification, email, report, sync, or other type the user names.
- Business task: exactly what the job should do.
- Schedule: cron, interval, daily time, event trigger, or manual trigger.
- Timezone when schedule is time-based.
- Required dependencies: services, repositories, DbContext, mail service, external APIs.
- Duplicate/concurrency policy: whether overlapping runs are allowed.

Ask one focused question if any of these affect behavior and are not clear.

## Technology Choice

| Job need | Preferred approach |
| --- | --- |
| Recurring/scheduled background task | Quartz `IJob` |
| Long-running worker loop | `BackgroundService` |
| Simple startup-only task | Hosted service or startup migration pattern |
| User asks for a specific job type/library | Follow the requested type unless it conflicts with project constraints |

For this project, scheduled jobs should normally use Quartz. Do not add a different scheduler unless the user asks or Quartz cannot satisfy the requirement.

## Quartz Workflow

1. Inspect existing packages and startup configuration.
   - `Hackathon.Service.csproj` already has Quartz packages.
   - `Hackathon.Api/Program.cs` must register Quartz and the hosted service for jobs to run.
2. Create the job class under a clear folder such as `Hackathon.Service/BackgroundJobs/<JobName>Job.cs`.
3. Implement `Quartz.IJob`.
4. Add `[DisallowConcurrentExecution]` unless overlapping runs are explicitly allowed.
5. Keep `Execute(IJobExecutionContext context)` small.
   - Resolve/query data.
   - Call existing services for business logic.
   - Log start, meaningful counts/results, and failures.
6. Register the job and trigger in `Hackathon.Api/Program.cs` or a dedicated extension method if multiple jobs exist.
7. Add `builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);` if not already configured.
8. Build and verify the app starts.

## Safe Dependency Rules

Background jobs run outside HTTP requests. Be careful with scoped dependencies.

Preferred safe patterns:

- Inject scoped services directly only when Quartz is configured with Microsoft DI job factory/lifecycle support.
- Otherwise inject `IServiceScopeFactory`, create a scope inside `Execute`, and resolve scoped services from that scope.
- Never store `DbContext`, scoped services, or per-run state in static fields.
- Do not use `IHttpContextAccessor` as the source of user identity inside jobs. Jobs do not have a request user.

## Quartz Registration Pattern

Use a stable `JobKey`, clear trigger identity, and explicit schedule.

```csharp
builder.Services.AddQuartz(options =>
{
    var jobKey = new JobKey("DeadlineReminderJob");

    options.AddJob<DeadlineReminderJob>(job => job.WithIdentity(jobKey));

    options.AddTrigger(trigger => trigger
        .ForJob(jobKey)
        .WithIdentity("DeadlineReminderJob-trigger")
        .WithCronSchedule("0 0 8 ? * *", cron => cron
            .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"))));
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});
```

Use the timezone ID that matches the deployment OS. On Windows, Vietnam time is usually `SE Asia Standard Time`. On Linux, it may be `Asia/Ho_Chi_Minh` depending on runtime support.

## Job Implementation Pattern

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Hackathon.Service.BackgroundJobs;

[DisallowConcurrentExecution]
public class DeadlineReminderJob : IJob
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<DeadlineReminderJob> _logger;

    public DeadlineReminderJob(IServiceScopeFactory scopeFactory, ILogger<DeadlineReminderJob> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("DeadlineReminderJob started");

        using var scope = _scopeFactory.CreateScope();
        // Resolve scoped services here, then call business logic.
        // var service = scope.ServiceProvider.GetRequiredService<SomeService.IService>();

        _logger.LogInformation("DeadlineReminderJob completed");
    }
}
```

## Clarify Before Editing

Ask before coding when:

- The user says "job" but not the job type or schedule.
- The job could be Quartz or `BackgroundService`.
- The job may send emails, notifications, or external requests without idempotency rules.
- The schedule has a local-time requirement but no timezone.
- The job needs current user context.
- The job can affect many rows and needs batching, retry, or failure policy.

## Hard Rules

- Do not create a job without knowing the job type or schedule source.
- Do not create a scheduled job without registering Quartz hosted service.
- Do not query or mutate data in an infinite loop when a Quartz schedule is appropriate.
- Do not use `Thread.Sleep`, `Task.Delay` loops, or fire-and-forget `Task.Run` for scheduled work.
- Do not allow overlapping executions unless explicitly required.
- Do not send duplicate emails/notifications without an idempotency plan.
- Do not depend on `HttpContext` or current request user inside background jobs.
- Do not silently swallow exceptions; log failures with enough context.

## Common Mistakes

| Mistake | Correct action |
| --- | --- |
| Adding a job class but no hosted service | Add `AddQuartzHostedService` |
| Hardcoding server local time | Set or clarify timezone |
| Injecting DbContext into singleton-like code | Use Quartz DI correctly or create a scope per execution |
| Letting jobs overlap | Add `[DisallowConcurrentExecution]` unless overlap is intended |
| Putting all business logic in the job | Keep job orchestration thin and call services |
| Using request user in job | Store required IDs/data explicitly; jobs run without HTTP context |
| No logs | Log start, end, key counts, and failures |

## Verification

After changes:

1. Run `dotnet build`.
2. Start the API and confirm Quartz starts without DI errors.
3. For short test schedules, temporarily use a frequent trigger only in local testing, then restore the real schedule.
4. Confirm the job is idempotent or protected from duplicate effects.
