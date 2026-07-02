using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Hackathon.Repository;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.LeaderBoards;

public class Service : IService
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContext;

    public Service(AppDbContext dbContext, IHttpContextAccessor httpContext)
    {
        _dbContext = dbContext;
        _httpContext = httpContext;
    }

    private Guid GetCurrentUserId()
    {
        var userIdValue = _httpContext.HttpContext?.User.FindFirst("UserId")?.Value
            ?? _httpContext.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdValue))
        {
            throw new MissingAccessTokenException();
        }

        if (!Guid.TryParse(userIdValue, out var userId))
        {
            throw new UnauthorizedException("INVALID_ACCESS_TOKEN");
        }

        return userId;
    }

    private async Task EnsureStaffAssignedToEvent(Guid eventId)
    {
        var staffId = GetCurrentUserId();
        var isAssigned = await _dbContext.AssignEvents.AnyAsync(x => x.UserId == staffId
            && x.EventId == eventId
            && !x.IsDisable
            && !x.Event.IsDisable);

        if (!isAssigned)
        {
            throw new ForbiddenException("STAFF_NOT_ASSIGNED_TO_EVENT");
        }
    }

    private bool IsCurrentUserAdmin()
    {
        var role = _httpContext.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
        return Enum.TryParse<RoleEnum>(role, true, out var userRole) && userRole == RoleEnum.Admin;
    }

    public async Task<List<Response.YearLeaderboardResponse>> GetYearLeaderboard(int year)
    {
        // 1. Verify that the year exists and has active leaderboards/events
        var hasLeaderBoards = await _dbContext.LeaderBoards
            .AnyAsync(lb => lb.Event.EndTime.HasValue
                && lb.Event.EndTime.Value.Year == year
                && !lb.IsDisable
                && !lb.Event.IsDisable);

        if (!hasLeaderBoards)
        {
            throw new InvalidYearException();
        }

        // 2. Fetch leaderboard details matching the year
        // We group by TeamId and sum their scores.
        // We only consider active teams, non-disabled events, leaderboards, and details.
        var detailsQuery = _dbContext.LeaderBoardDetails
            .AsNoTracking()
            .Include(lbd => lbd.Team)
            .Include(lbd => lbd.LeaderBoard)
            .ThenInclude(lb => lb.Event)
            .Where(lbd => lbd.LeaderBoard.Event.EndTime.HasValue
                          && lbd.LeaderBoard.Event.EndTime.Value.Year == year
                          && !lbd.LeaderBoard.IsDisable
                          && !lbd.LeaderBoard.Event.IsDisable
                          && !lbd.IsDisable
                          && !lbd.Team.IsDisable);

        var groupedDetails = await detailsQuery
            .GroupBy(lbd => new { lbd.TeamId, TeamName = lbd.Team.Name })
            .Select(g => new
            {
                TeamId = g.Key.TeamId,
                TeamName = g.Key.TeamName,
                TotalYearScore = g.Sum(lbd => lbd.Score ?? 0m),
                EventsParticipated = g.Count()
            })
            .ToListAsync();

        // 3. Assign sequential ranks
        var rankedList = groupedDetails
            .OrderByDescending(x => x.TotalYearScore)
            .Select((x, index) => new Response.YearLeaderboardResponse
            {
                Rank = index + 1,
                TeamId = x.TeamId,
                TeamName = x.TeamName,
                TotalYearScore = x.TotalYearScore,
                EventsParticipated = x.EventsParticipated
            })
            .ToList();

        return rankedList;
    }

    public async Task<string> AssignAward(Guid leaderBoardId, Guid teamId, Request.AssignAwardRequest request)
    {
        // 1. Find the leaderboard detail record
        var detail = await _dbContext.LeaderBoardDetails
            .Include(lbd => lbd.LeaderBoard)
            .ThenInclude(lb => lb.Event)
            .FirstOrDefaultAsync(lbd => lbd.LeaderBoardId == leaderBoardId && lbd.TeamId == teamId);

        if (detail == null)
        {
            throw new LeaderBoardDetailNotFoundException();
        }

        // 2. Check permission: must be Admin or Staff assigned to this event
        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(detail.LeaderBoard.EventId);
        }

        // 3. Business rule check: only allow edit when event/leaderboard is not locked (read-only) or disabled
        if (detail.IsDisable || detail.LeaderBoard.IsDisable || detail.LeaderBoard.Event.IsDisable)
        {
            throw new ForbiddenException("LEADERBOARD_OR_EVENT_DISABLED");
        }

        if (detail.LeaderBoard.Event.Status == EventStatusEnum.Closed)
        {
            throw new ForbiddenException("EVENT_IS_CLOSED");
        }

        // 4. Update the values
        detail.Score = request.Score;
        detail.LevelAward = request.LevelAward;
        detail.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.LeaderBoardDetails.Update(detail);
        await _dbContext.SaveChangesAsync();

        return "AWARD_ASSIGNED_SUCCESSFULLY";
    }
}
