using Hackathon.Repository;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Hackathon.Service.BackgroundJobService;

[DisallowConcurrentExecution]
public class AutoCloseExpiredEventsJob : IJob
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<AutoCloseExpiredEventsJob> _logger;

    public AutoCloseExpiredEventsJob(
        AppDbContext dbContext,
        ILogger<AutoCloseExpiredEventsJob> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var now = DateTimeOffset.UtcNow;
        var cancellationToken = context.CancellationToken;

        var expiredEvents = await _dbContext.Events
            .Where(x => !x.IsDisable
                && x.Status == EventStatusEnum.Published
                && x.EndTime.HasValue
                && x.EndTime.Value <= now)
            .ToListAsync(cancellationToken);

        if (expiredEvents.Count == 0)
        {
            _logger.LogInformation("AutoCloseExpiredEventsJob: no expired events found.");
            return;
        }

        foreach (var ev in expiredEvents)
        {
            ev.Status = EventStatusEnum.Closed;
            ev.UpdatedAt = now;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "AutoCloseExpiredEventsJob: closed {Count} events whose EndTime has passed.",
            expiredEvents.Count);
    }
}
