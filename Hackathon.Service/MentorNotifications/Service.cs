using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.MentorNotifications;

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

    public async Task<(List<Response.MentorNotificationResponse> Items, int TotalCount)> GetMentorNotifications(Guid? eventId, Guid? trackId, int pageIndex, int pageSize)
    {
        if (pageIndex < 1 || pageSize < 1)
        {
            throw new BadRequestException("BAD_REQUEST");
        }

        var userId = GetCurrentUserId();
        var query = _dbContext.MentorNotifications
            .AsNoTracking()
            .Include(x => x.AssignTrack)
            .ThenInclude(x => x.Track)
            .Include(x => x.AssignTrack)
            .ThenInclude(x => x.AssignEvent)
            .Where(x => !x.IsDisable && !x.AssignTrack.IsDisable && !x.AssignTrack.Track.IsDisable && !x.AssignTrack.AssignEvent.Event.IsDisable);

        if (eventId.HasValue)
        {
            query = query.Where(x => x.AssignTrack.Track.EventId == eventId.Value);
        }

        if (trackId.HasValue)
        {
            query = query.Where(x => x.AssignTrack.TrackId == trackId.Value);
        }

        query = query.Where(x => x.AssignTrack.AssignEvent.UserId == userId
                                 || _dbContext.RegisterTeams.Any(rt => !rt.IsDisable
                                                                       && rt.EventId == x.AssignTrack.Track.EventId
                                                                       && !rt.Team.IsDisable
                                                                       && rt.Team.TeamDetails.Any(td => td.UserId == userId && !td.IsDisable)));

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.MentorNotificationResponse
            {
                Id = x.Id,
                AssignTrackId = x.AssignTrackId,
                TrackId = x.AssignTrack.TrackId,
                EventId = x.AssignTrack.Track.EventId,
                Title = x.Title,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();

        if (items.Count == 0 && (eventId.HasValue || trackId.HasValue))
        {
            throw new ForbiddenException("MENTOR_NOTIFICATION_NOT_VISIBLE_TO_USER");
        }

        return (items, totalCount);
    }
}
