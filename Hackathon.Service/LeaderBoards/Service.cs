using Hackathon.Repository;
using Hackathon.Service.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.LeaderBoards;

public class Service : IService
{
    private readonly AppDbContext _dbContext;

    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Response.LeaderBoardItemResponse>> GetEventLeaderBoard(Guid eventId)
    {
        var eventExists = await _dbContext.Events.AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var items = await _dbContext.RegisterTeams
            .AsNoTracking()
            .Where(x => x.EventId == eventId && !x.IsDisable && !x.Team.IsDisable)
            .Select(x => new Response.LeaderBoardItemResponse
            {
                TeamId = x.TeamId,
                TeamName = x.Team.Name,
                TotalScore = x.RoundDetails
                    .Where(rd => !rd.IsDisable)
                    .SelectMany(rd => rd.Submissions.Where(s => !s.IsDisable))
                    .Select(s => s.Scores.Where(sc => !sc.IsDisable && sc.TotalScore.HasValue).Average(sc => (decimal?)sc.TotalScore) ?? 0)
                    .Sum(),
            })
            .Where(x => x.TotalScore > 0)
            .OrderByDescending(x => x.TotalScore)
            .ThenBy(x => x.TeamName)
            .ToListAsync();

        if (items.Count == 0)
        {
            throw new NotFoundException("LEADERBOARD_NOT_FOUND");
        }

        for (var i = 0; i < items.Count; i++)
        {
            items[i].Rank = i + 1;
        }

        return items;
    }

    public async Task<(List<Response.LeaderBoardItemResponse> Items, int TotalCount)> GetYearLeaderBoard(int? year, int pageIndex, int pageSize)
    {
        if (!year.HasValue)
        {
            throw new BadRequestException("YEAR_REQUIRED");
        }

        if (pageIndex < 1 || pageSize < 1)
        {
            throw new BadRequestException("BAD_REQUEST");
        }

        var items = await _dbContext.RegisterTeams
            .AsNoTracking()
            .Where(x => !x.IsDisable
                        && !x.Team.IsDisable
                        && !x.Event.IsDisable
                        && x.Event.StartTime.HasValue
                        && x.Event.StartTime.Value.Year == year.Value)
            .Select(x => new
            {
                x.TeamId,
                TeamName = x.Team.Name,
                Score = x.RoundDetails
                    .Where(rd => !rd.IsDisable)
                    .SelectMany(rd => rd.Submissions.Where(s => !s.IsDisable))
                    .Select(s => s.Scores.Where(sc => !sc.IsDisable && sc.TotalScore.HasValue).Average(sc => (decimal?)sc.TotalScore) ?? 0)
                    .Sum(),
            })
            .GroupBy(x => new { x.TeamId, x.TeamName })
            .Select(x => new Response.LeaderBoardItemResponse
            {
                TeamId = x.Key.TeamId,
                TeamName = x.Key.TeamName,
                TotalScore = x.Sum(y => y.Score),
            })
            .Where(x => x.TotalScore > 0)
            .OrderByDescending(x => x.TotalScore)
            .ThenBy(x => x.TeamName)
            .ToListAsync();

        for (var i = 0; i < items.Count; i++)
        {
            items[i].Rank = i + 1;
        }

        var totalCount = items.Count;
        var pagedItems = items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return (pagedItems, totalCount);
    }
}
