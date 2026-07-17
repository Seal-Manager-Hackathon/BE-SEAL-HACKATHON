using Hackathon.Repository;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Hackathon.Service.BackgroundJobService;

[DisallowConcurrentExecution]
public class ExpirePendingInvitationsJob : IJob
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<ExpirePendingInvitationsJob> _logger;

    public ExpirePendingInvitationsJob(
        AppDbContext dbContext,
        ILogger<ExpirePendingInvitationsJob> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var now = DateTimeOffset.UtcNow;
        var cancellationToken = context.CancellationToken;

        var expiredInvitations = await _dbContext.Invitations
            .Where(x =>
                !x.IsDisable &&
                x.Status == InvitationStatusEnum.Pending &&
                x.LimitTime.HasValue &&
                x.LimitTime.Value <= now)
            .ToListAsync(cancellationToken);

        if (expiredInvitations.Count == 0)
        {
            _logger.LogInformation("ExpirePendingInvitationsJob: no expired invitations found.");
            return;
        }

        foreach (var invitation in expiredInvitations)
        {
            invitation.Status = InvitationStatusEnum.Expired;
            invitation.UpdatedAt = now;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "ExpirePendingInvitationsJob: expired {Count} pending invitations.",
            expiredInvitations.Count);
    }
}
