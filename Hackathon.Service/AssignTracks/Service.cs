using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.AssignTracks.Request;
using Hackathon.Service.AssignTracks.Response;
using Hackathon.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.AssignTracks;

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

    private bool IsCurrentUserAdmin()
    {
        var role = _httpContext.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
        return Enum.TryParse<RoleEnum>(role, true, out var userRole) && userRole == RoleEnum.Admin;
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

    public async Task<AssignTrackResponse> AssignJudgeToTrack(Guid trackId, AssignJudgeRequest request)
    {
        var track = await _dbContext.Tracks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == trackId && !x.IsDisable);
        if (track == null)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(track.EventId);
        }

        var assignEvent = await _dbContext.AssignEvents
            .Include(x => x.EventRole)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.AssignEventId && !x.IsDisable);

        if (assignEvent == null)
        {
            throw new NotFoundException("ASSIGN_EVENT_NOT_FOUND");
        }

        if (assignEvent.EventId != track.EventId)
        {
            throw new ConflictException("ASSIGN_EVENT_NOT_MATCH_TRACK_EVENT");
        }

        if (assignEvent.EventRole?.Name != EventRoleEnum.Judge)
        {
            throw new ConflictException("ONLY_JUDGE_CAN_BE_ASSIGNED_TO_TRACK");
        }

        var existingAssignment = await _dbContext.AssignTracks.AsNoTracking()
            .AnyAsync(x => x.AssignEventId == request.AssignEventId && x.TrackId == trackId && !x.IsDisable);

        if (existingAssignment)
        {
            throw new ConflictException("JUDGE_ALREADY_ASSIGNED_TO_TRACK");
        }

        var newAssignTrack = new Repository.Entity.AssignTracks
        {
            AssignEventId = request.AssignEventId,
            TrackId = trackId,
            IsDisable = false,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _dbContext.AssignTracks.Add(newAssignTrack);
        await _dbContext.SaveChangesAsync();

        return new AssignTrackResponse
        {
            Id = newAssignTrack.Id,
            AssignEventId = newAssignTrack.AssignEventId,
            TrackId = newAssignTrack.TrackId
        };
    }

    public async Task<List<AssignTrackLecturerResponse>> GetLecturersAssignedToTrack(Guid eventId, Guid trackId, bool? isDisable)
    {
        var eventExists = await _dbContext.Events.AsNoTracking().AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var trackExists = await _dbContext.Tracks.AsNoTracking().AnyAsync(x => x.Id == trackId && x.EventId == eventId && !x.IsDisable);
        if (!trackExists)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(eventId);
        }

        var query = _dbContext.AssignTracks
            .AsNoTracking()
            .Include(x => x.AssignEvent)
                .ThenInclude(ae => ae.User)
            .Include(x => x.AssignEvent)
                .ThenInclude(ae => ae.EventRole)
            .Where(x => x.TrackId == trackId
                        && x.AssignEvent.EventId == eventId
                        && !x.AssignEvent.IsDisable
                        && (x.AssignEvent.EventRole != null && (x.AssignEvent.EventRole.Name == EventRoleEnum.Mentor || x.AssignEvent.EventRole.Name == EventRoleEnum.Judge)));

        if (isDisable.HasValue)
        {
            query = query.Where(x => x.IsDisable == isDisable.Value);
        }

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new AssignTrackLecturerResponse
            {
                Id = x.Id,
                AssignEventId = x.AssignEventId,
                UserId = x.AssignEvent.UserId,
                FirstName = x.AssignEvent.User.FirstName,
                LastName = x.AssignEvent.User.LastName,
                Email = x.AssignEvent.User.Email,
                EventRole = x.AssignEvent.EventRole != null ? (EventRoleEnum?)x.AssignEvent.EventRole.Name : null,
                Role = x.AssignEvent.User.Role,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        if (items.Count == 0)
        {
            throw new NotFoundException("NO_ONE_ASSIGNED_TO_TRACK");
        }

        return items;
    }
}
