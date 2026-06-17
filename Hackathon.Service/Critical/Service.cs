using Hackathon.Repository;
using Hackathon.Service.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Critical;

public class Service : IService
{
    private readonly AppDbContext _dbContext;

    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response.RoundCriteriaResponse> GetCriteriaByRound(Guid roundId, bool? isDisable)
    {
        var round = await _dbContext.Rounds
            .AsNoTracking()
            .Include(x => x.Event)
            .Where(x => x.Id == roundId && !x.IsDisable && !x.Event.IsDisable)
            .Select(x => new
            {
                x.Id,
                x.EventId,
                x.Name,
            })
            .FirstOrDefaultAsync();

        if (round == null)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        var criteriaTemplates = await _dbContext.CriteriaTemplates
            .AsNoTracking()
            .Where(x => x.RoundId == roundId && x.IsDisable == (isDisable ?? false))
            .OrderBy(x => x.CreatedAt)
            .ThenBy(x => x.Title)
            .Select(x => new Response.CriteriaTemplateResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                IsDisable = x.IsDisable,
                CreatedAt = x.CreatedAt,
                Items = x.CriteriaItems
                    .Where(item => item.IsDisable == (isDisable ?? false))
                    .OrderBy(item => item.CreatedAt)
                    .ThenBy(item => item.Name)
                    .Select(item => new Response.CriteriaItemResponse
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Score = item.Score,
                        IsDisable = item.IsDisable,
                        CreatedAt = item.CreatedAt,
                    })
                    .ToList(),
            })
            .ToListAsync();

        return new Response.RoundCriteriaResponse
        {
            RoundId = round.Id,
            EventId = round.EventId,
            RoundName = round.Name,
            Templates = criteriaTemplates,
        };
    }
}
