using Hackathon.Repository;
using Hackathon.Service.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Rounds;

public class Service : IService
{
    private readonly AppDbContext _dbContext;

    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Response.RoundResponse>> GetRounds(Guid? eventId, bool? isDisable)
    {
        if (eventId.HasValue)
        {
            var eventExists = await _dbContext.Events.AnyAsync(x => x.Id == eventId.Value && !x.IsDisable);
            if (!eventExists)
            {
                throw new NotFoundException("EVENT_NOT_FOUND");
            }
        }

        var query = _dbContext.Rounds
            .AsNoTracking()
            .Include(x => x.Event)
            .Where(x => x.IsDisable == (isDisable ?? false) && !x.Event.IsDisable);

        if (eventId.HasValue)
        {
            query = query.Where(x => x.EventId == eventId.Value);
        }

        return await query
            .OrderBy(x => x.StartTime)
            .ThenBy(x => x.CreatedAt)
            .Select(x => new Response.RoundResponse
            {
                Id = x.Id,
                EventId = x.EventId,
                Name = x.Name,
                Description = x.Description,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                StartSubmission = x.StartSubmission,
                EndSubmission = x.EndSubmission,
                LimitTeam = x.LimitTeam,
                IsDisable = x.IsDisable,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();
    }
}
