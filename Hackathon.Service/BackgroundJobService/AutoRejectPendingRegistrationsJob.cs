using Hackathon.Repository;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Hackathon.Service.BackgroundJobService;

[DisallowConcurrentExecution]
public class AutoRejectPendingRegistrationsJob : IJob
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<AutoRejectPendingRegistrationsJob> _logger;

    public AutoRejectPendingRegistrationsJob(
        AppDbContext dbContext,
        ILogger<AutoRejectPendingRegistrationsJob> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var now = DateTimeOffset.UtcNow;
        var cancellationToken = context.CancellationToken;

        // Auto-reject pending registrations for events whose RegisterLimitTime has passed
        var expiredRegisterEvents = await _dbContext.Events
            .AsNoTracking()
            .Where(x => !x.IsDisable
                && x.Status == EventStatusEnum.Published
                && x.RegisterLimitTime.HasValue
                && x.RegisterLimitTime.Value <= now)
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        if (expiredRegisterEvents.Count == 0)
        {
            _logger.LogInformation("AutoRejectPendingRegistrationsJob: no events to process.");
            return;
        }

        var pendingRegistrations = await _dbContext.RegisterTeams
            .Where(x => x.Status == RegisterTeamStatusEnum.Pending
                && !x.IsDisable
                && expiredRegisterEvents.Contains(x.EventId))
            .ToListAsync(cancellationToken);

        if (pendingRegistrations.Count == 0)
        {
            _logger.LogInformation("AutoRejectPendingRegistrationsJob: no pending registrations to reject.");
            return;
        }

        foreach (var reg in pendingRegistrations)
        {
            reg.Status = RegisterTeamStatusEnum.Rejected;
            reg.RejectionReason = "registration deadline has passed";
            reg.UpdatedAt = now;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "AutoRejectPendingRegistrationsJob: auto-rejected {Count} pending registrations.",
            pendingRegistrations.Count);
    }
}
