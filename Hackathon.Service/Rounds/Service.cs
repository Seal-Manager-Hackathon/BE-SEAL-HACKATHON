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

    public async Task<List<Response.RoundResponse>> GetRounds(Guid eventId)
    {
        var eventExists = await _dbContext.Events
            .AsNoTracking()
            .AnyAsync(x => x.Id == eventId && !x.IsDisable);

        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var rounds = await _dbContext.Rounds
            .AsNoTracking()
            .Where(x => x.EventId == eventId && !x.IsDisable)
            .OrderBy(x => x.CreatedAt)
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
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        return rounds;
    }
}
