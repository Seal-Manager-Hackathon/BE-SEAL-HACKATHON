using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Mentors;

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

    public async Task<BasePaginationResponse> GetMentorEvents(Request.GetMentorEventsRequest request)
    {
        var userId = GetCurrentUserId();

        // Check if user exists and is a Lecturer
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null || user.Role != RoleEnum.Lecturer)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        var pageIndex = request.PageIndex <= 0 ? 1 : request.PageIndex;
        var pageSize = request.PageSize <= 0 ? 10 : Math.Min(request.PageSize, 100);

        // 2. Query assignments for this user in AssignEvents
        var query = _dbContext.AssignEvents
            .AsNoTracking()
            .Include(x => x.Event)
            .Include(x => x.EventRole)
            .Where(x => x.UserId == userId
                        && !x.IsDisable
                        && !x.Event.IsDisable);

        var totalCount = await query.CountAsync();

        if (totalCount == 0)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.MentorEventResponse
            {
                AssignEventId = x.Id,
                EventId = x.EventId,
                EventName = x.Event.Name,
                Role = x.EventRole != null ? x.EventRole.Name : null
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
    }

    public async Task<List<Response.MentorTrackResponse>> GetMentorTracks(Guid? eventId)
    {
        var userId = GetCurrentUserId();

        var query = GetMentorAssignmentsQuery(userId);

        if (eventId.HasValue)
        {
            query = query.Where(x => x.Track.EventId == eventId.Value);
        }

        return await query
            .OrderBy(x => x.Track.Event.Name)
            .ThenBy(x => x.Track.Title)
            .Select(x => new Response.MentorTrackResponse
            {
                AssignTrackId = x.Id,
                TrackId = x.TrackId,
                TrackTitle = x.Track.Title,
                TrackDescription = x.Track.Description,
                EventId = x.Track.EventId,
                EventName = x.Track.Event.Name
            })
            .ToListAsync();
    }

    public async Task<BasePaginationResponse> GetMentorTrackTeams(Guid trackId, PaginationRequest paginationRequest)
    {
        var userId = GetCurrentUserId();

        // Ensure mentor assigned to this track
        await EnsureMentorAssignedToTrack(userId, trackId);

        var pageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        var pageSize = paginationRequest.PageSize <= 0 ? 10 : Math.Min(paginationRequest.PageSize, 100);

        // Get approved teams in this track
        var query = _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team).ThenInclude(x => x.TeamDetails.Where(td => !td.IsDisable && td.IsLeader)).ThenInclude(td => td.User)
            .Include(x => x.Team).ThenInclude(x => x.TeamDetails.Where(td => !td.IsDisable && td.Status == TeamDetailStatusEnum.Active))
            .Include(x => x.Topic)
            .Where(x => x.TrackId == trackId
                        && x.Status == RegisterTeamStatusEnum.Approved
                        && !x.IsDisable
                        && !x.IsBanned
                        && !x.Team.IsDisable);

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Team.Name)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.MentorTrackTeamResponse
            {
                RegisterTeamId = x.Id,
                TeamId = x.TeamId,
                TeamName = x.Team.Name,
                TopicId = x.TopicId,
                TopicTitle = x.Topic != null ? x.Topic.Title : null,
                LeaderName = x.Team.TeamDetails
                    .Where(td => td.IsLeader)
                    .Select(td => (td.User.FirstName + " " + td.User.LastName).Trim())
                    .FirstOrDefault(),
                MemberCount = x.Team.TeamDetails.Count
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
    }

    public async Task<Response.MentorNotificationResponse> SendTrackNotification(Guid trackId, Request.SendNotificationRequest request)
    {
        var userId = GetCurrentUserId();
        var assignTrackId = await EnsureMentorAssignedToTrack(userId, trackId);

        var now = DateTimeOffset.UtcNow;
        var notification = new Hackathon.Repository.Entity.MentorNotifications
        {
            Id = Guid.NewGuid(),
            AssignTrackId = assignTrackId,
            Title = request.Title,
            Description = request.Description,
            CreatedAt = now,
            UpdatedAt = now
        };

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _dbContext.MentorNotifications.AddAsync(notification);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return new Response.MentorNotificationResponse
        {
            MentorNotificationId = notification.Id,
            Message = "MENTOR_NOTIFICATION_SENT"
        };
    }

    public async Task<Response.MentorNotificationResponse> SendTeamNotification(Guid teamId, Guid? requestTrackId, Request.SendNotificationRequest request)
    {
        var userId = GetCurrentUserId();

        // Find the register team
        var registerTeam = await _dbContext.RegisterTeams
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TeamId == teamId
                                      && x.Status == RegisterTeamStatusEnum.Approved
                                      && !x.IsDisable
                                      && !x.IsBanned);

        if (registerTeam == null || !registerTeam.TrackId.HasValue)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        var trackId = registerTeam.TrackId.Value;
        if (requestTrackId.HasValue && requestTrackId.Value != trackId)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        var assignTrackId = await EnsureMentorAssignedToTrack(userId, trackId);

        var now = DateTimeOffset.UtcNow;
        var notification = new Hackathon.Repository.Entity.MentorNotifications
        {
            Id = Guid.NewGuid(),
            AssignTrackId = assignTrackId,
            Title = request.Title,
            Description = request.Description,
            CreatedAt = now,
            UpdatedAt = now
        };

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _dbContext.MentorNotifications.AddAsync(notification);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return new Response.MentorNotificationResponse
        {
            MentorNotificationId = notification.Id,
            Message = "MENTOR_NOTIFICATION_SENT"
        };
    }

    private IQueryable<Hackathon.Repository.Entity.AssignTracks> GetMentorAssignmentsQuery(Guid userId)
    {
        return _dbContext.AssignTracks
            .AsNoTracking()
            .Where(x =>
                !x.IsDisable &&
                !x.AssignEvent.IsDisable &&
                !x.AssignEvent.Event.IsDisable &&
                !x.Track.IsDisable &&
                !x.Track.Event.IsDisable &&
                x.AssignEvent.UserId == userId &&
                x.AssignEvent.EventRole != null &&
                x.AssignEvent.EventRole.Name == EventRoleEnum.Mentor);
    }

    private async Task<Guid> EnsureMentorAssignedToTrack(Guid userId, Guid trackId)
    {
        var trackExists = await _dbContext.Tracks
            .AsNoTracking()
            .AnyAsync(x => x.Id == trackId && !x.IsDisable);

        if (!trackExists)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        var assignTrackId = await GetMentorAssignmentsQuery(userId)
            .Where(x => x.TrackId == trackId)
            .Select(x => (Guid?)x.Id)
            .FirstOrDefaultAsync();

        if (!assignTrackId.HasValue)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        return assignTrackId.Value;
    }
}
