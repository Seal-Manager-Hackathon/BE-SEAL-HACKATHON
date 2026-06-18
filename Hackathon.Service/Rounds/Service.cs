using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Rounds;

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
            .OrderBy(x => x.RoundNo)
            .ThenBy(x => x.CreatedAt)
            .Select(x => new Response.RoundResponse
            {
                Id = x.Id,
                EventId = x.EventId,
                Name = x.Name,
                Description = x.Description,
                RoundNo = x.RoundNo,
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

    public async Task<List<Response.MyRoundResponse>> GetMyRounds(Guid? eventId, Guid teamId)
    {
        var userId = GetCurrentUserId();

        if (eventId.HasValue)
        {
            var eventExists = await _dbContext.Events
                .AsNoTracking()
                .AnyAsync(x => x.Id == eventId.Value && !x.IsDisable);

            if (!eventExists)
            {
                throw new NotFoundException("EVENT_NOT_FOUND");
            }
        }

        var teamExists = await _dbContext.Teams
            .AsNoTracking()
            .AnyAsync(x => x.Id == teamId && !x.IsDisable);

        if (!teamExists)
        {
            throw new NotFoundException("TEAM_NOT_FOUND");
        }

        var query = _dbContext.RoundDetails
            .AsNoTracking()
            .Include(x => x.Round).ThenInclude(r => r.Event)
            .Include(x => x.RegisterTeam).ThenInclude(rt => rt.Team)
            .Where(x => !x.Round.IsDisable && !x.Round.Event.IsDisable && !x.RegisterTeam.Team.IsDisable)
            .Where(x => x.RegisterTeam.Team.TeamDetails.Any(td => td.UserId == userId && !td.IsDisable));

        if (eventId.HasValue)
        {
            query = query.Where(x => x.Round.EventId == eventId.Value);
        }

        query = query.Where(x => x.RegisterTeam.TeamId == teamId);

        var myRounds = await query
            .OrderBy(x => x.Round.RoundNo)
            .ThenBy(x => x.Round.StartTime)
            .Select(x => new Response.MyRoundResponse
            {
                RoundId = x.RoundId,
                EventId = x.Round.EventId,
                RoundName = x.Round.Name,
                EventName = x.Round.Event.Name,
                RoundNo = x.Round.RoundNo,
                TeamId = x.RegisterTeam.TeamId,
                TeamName = x.RegisterTeam.Team.Name,
                RegisterTeamId = x.RegisterTeamId,
                StartTime = x.Round.StartTime,
                EndTime = x.Round.EndTime,
                StartSubmission = x.Round.StartSubmission,
                EndSubmission = x.Round.EndSubmission
            })
            .ToListAsync();

        return myRounds;
    }

    public async Task<Response.MyRoundDetailResponse> GetMyRoundDetail(Guid roundId, Guid registerTeamId)
    {
        var userId = GetCurrentUserId();

        var detail = await _dbContext.RoundDetails
            .AsNoTracking()
            .Include(x => x.Round).ThenInclude(r => r.Event)
            .Include(x => x.RegisterTeam).ThenInclude(rt => rt.Team)
            .Include(x => x.RegisterTeam).ThenInclude(rt => rt.Track)
            .Include(x => x.RegisterTeam).ThenInclude(rt => rt.Topic)
            .Where(x => !x.Round.IsDisable && !x.Round.Event.IsDisable && !x.RegisterTeam.Team.IsDisable)
            .Where(x => x.RegisterTeam.Team.TeamDetails.Any(td => td.UserId == userId && !td.IsDisable))
            .Where(x => x.RoundId == roundId && x.RegisterTeamId == registerTeamId)
            .Select(x => new Response.MyRoundDetailResponse
            {
                RoundId = x.RoundId,
                EventId = x.Round.EventId,
                RoundName = x.Round.Name,
                EventName = x.Round.Event.Name,
                RoundNo = x.Round.RoundNo,
                TeamId = x.RegisterTeam.TeamId,
                TeamName = x.RegisterTeam.Team.Name,
                RegisterTeamId = x.RegisterTeamId,
                TrackId = x.RegisterTeam.TrackId,
                TrackTitle = x.RegisterTeam.Track != null ? x.RegisterTeam.Track.Title : null,
                TopicId = x.RegisterTeam.TopicId,
                TopicTitle = x.RegisterTeam.Topic != null ? x.RegisterTeam.Topic.Title : null,
                StartTime = x.Round.StartTime,
                EndTime = x.Round.EndTime,
                StartSubmission = x.Round.StartSubmission,
                EndSubmission = x.Round.EndSubmission
            })
            .FirstOrDefaultAsync();

        if (detail == null) throw new NotFoundException("ROUND_DETAIL_NOT_FOUND");
        return detail;
    }
}
