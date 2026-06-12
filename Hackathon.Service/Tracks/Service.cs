using Hackathon.Repository;
using Hackathon.Service.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Tracks;

public class Service : IService
{
    private readonly AppDbContext _dbContext;

    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(List<Response.TrackResponse> Items, int TotalCount)> GetTracks(Guid? eventId, string? keyword, bool? isDisable, int pageIndex, int pageSize)
    {
        if (pageIndex < 1 || pageSize < 1)
        {
            throw new BadRequestException("BAD_REQUEST");
        }

        if (eventId.HasValue)
        {
            var eventExists = await _dbContext.Events.AnyAsync(x => x.Id == eventId.Value && !x.IsDisable);
            if (!eventExists)
            {
                throw new NotFoundException("EVENT_NOT_FOUND");
            }
        }

        var query = _dbContext.Tracks.AsNoTracking().AsQueryable();
        query = query.Where(x => x.IsDisable == (isDisable ?? false));

        if (eventId.HasValue)
        {
            query = query.Where(x => x.EventId == eventId.Value);
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var normalizedKeyword = keyword.Trim().ToLower();
            query = query.Where(x => x.Title.ToLower().Contains(normalizedKeyword)
                                     || (x.Description != null && x.Description.ToLower().Contains(normalizedKeyword)));
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(x => x.Title)
            .ThenBy(x => x.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.TrackResponse
            {
                Id = x.Id,
                EventId = x.EventId,
                Title = x.Title,
                Description = x.Description,
                MaxTeam = x.MaxTeam,
                IsDisable = x.IsDisable,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();

        return (items, totalCount);
    }
}
