using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.AssignEvents;

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
        if (IsCurrentUserAdmin())
        {
            return;
        }

        var staffId = GetCurrentUserId();
        var isAssigned = await _dbContext.AssignEvents.AnyAsync(x => x.UserId == staffId
            && x.EventId == eventId
            && !x.IsDisable);

        if (!isAssigned)
        {
            throw new ForbiddenException("STAFF_NOT_ASSIGNED_TO_EVENT");
        }
    }

    public async Task<BasePaginationResponse> GetEventAssignments(Guid eventId, EventRoleEnum? eventRole, string? keyword, Guid? trackId, bool? isDisable, PaginationRequest paginationRequest)
    {
        var eventExists = await _dbContext.Events.AsNoTracking().AnyAsync(x => x.Id == eventId);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(eventId);
        }

        var query = _dbContext.AssignEvents
            .Include(x => x.User)
            .Include(x => x.EventRole)
            .Include(x => x.AssignTracks)
                .ThenInclude(at => at.Track)
            .AsNoTracking()
            .Where(x => x.EventId == eventId
                     && x.IsDisable == (isDisable ?? false));

        // Staff chỉ thấy Lecturer (ko thấy Staff assignments)
        if (!IsCurrentUserAdmin())
        {
            query = query.Where(x => x.User.Role == RoleEnum.Lecturer);
        }

        if (eventRole.HasValue)
        {
            query = query.Where(x => x.EventRole != null && x.EventRole.Name == eventRole.Value);
        }

        if (trackId.HasValue)
        {
            query = query.Where(x => x.AssignTracks.Any(at => at.TrackId == trackId && !at.IsDisable));
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var normalizedKeyword = keyword.Trim().ToLower();
            query = query.Where(x => (x.User.FirstName + " " + x.User.LastName).ToLower().Contains(normalizedKeyword)
                                  || x.User.Email.ToLower().Contains(normalizedKeyword));
        }

        var totalCount = await query.CountAsync();

        paginationRequest.PageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        paginationRequest.PageSize = paginationRequest.PageSize <= 0 ? 10 : Math.Min(paginationRequest.PageSize, 100);

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((paginationRequest.PageIndex - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
            .Select(x => new Response.AssignLecturerDetailResponse
            {
                Id = x.Id,
                UserId = x.UserId,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                Email = x.User.Email,
                EventRoleId = x.EventRoleId,
                EventRole = x.EventRole != null ? (EventRoleEnum?)x.EventRole.Name : null,
                Role = x.User.Role,
                IsDisable = x.IsDisable,
                CreatedAt = x.CreatedAt,
                AssignedTracks = x.AssignTracks
                    .Where(at => !at.IsDisable)
                    .Select(at => new Response.AssignedTrackInfo
                    {
                        AssignTrackId = at.Id,
                        TrackId = at.TrackId,
                        TrackTitle = at.Track.Title,
                        IsDisable = at.IsDisable
                    }).ToList()
            })
            .ToListAsync();

        if (items.Count == 0)
        {
            throw new NotFoundException("NO_ONE_ASSIGNED_TO_EVENT");
        }

        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
    }

    public async Task<BasePaginationResponse> GetAvailableLecturers(Guid eventId, Request.GetAvailableLecturersRequest request)
    {
        var eventExists = await _dbContext.Events.AsNoTracking().AnyAsync(x => x.Id == eventId);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(eventId);
        }

        var unavailableLecturerIds = _dbContext.AssignEvents.AsNoTracking()
            .Where(x => x.EventId == eventId && !x.IsDisable)
            .Select(x => x.UserId);

        var query = _dbContext.Users.AsNoTracking()
            .Where(x => x.Role == RoleEnum.Lecturer
                        && !x.IsDisable
                        && x.Status == UserStatusEnum.Active
                        && !unavailableLecturerIds.Contains(x.Id));

        if (request.UserId.HasValue)
        {
            query = query.Where(x => x.Id == request.UserId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var normalizedKeyword = request.Keyword.Trim().ToLower();
            query = query.Where(x => (x.FirstName + " " + x.LastName).ToLower().Contains(normalizedKeyword)
                                  || x.Email.ToLower().Contains(normalizedKeyword));
        }

        var totalCount = await query.CountAsync();

        request.PageIndex = request.PageIndex <= 0 ? 1 : request.PageIndex;
        request.PageSize = request.PageSize <= 0 ? 10 : Math.Min(request.PageSize, 100);

        var items = await query
            .OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new Response.AvailableLecturerResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                FullName = (x.FirstName + " " + x.LastName).Trim(),
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                AvatarUrl = x.AvatarUrl,
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, request.PageIndex, request.PageSize, totalCount);
    }

    public async Task<Guid> RemoveLecturerAssignment(Guid assignEventId)
    {
        var assignEvent = await _dbContext.AssignEvents
            .Include(x => x.AssignTracks)
            .FirstOrDefaultAsync(x => x.Id == assignEventId && !x.IsDisable);

        if (assignEvent == null)
        {
            throw new NotFoundException("ASSIGN_EVENT_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(assignEvent.EventId);
        }

        assignEvent.IsDisable = true;
        assignEvent.UpdatedAt = DateTimeOffset.UtcNow;
        _dbContext.AssignEvents.Update(assignEvent);

        // Cascade delete tracks
        if (assignEvent.AssignTracks != null && assignEvent.AssignTracks.Any())
        {
            foreach (var track in assignEvent.AssignTracks.Where(t => !t.IsDisable))
            {
                track.IsDisable = true;
                track.UpdatedAt = DateTimeOffset.UtcNow;
                _dbContext.AssignTracks.Update(track);
            }
        }

        await _dbContext.SaveChangesAsync();

        return assignEvent.Id;
    }

    public async Task<Response.AssignEventResponse> AssignLecturerToEvent(Guid eventId, Request.AssignLecturerRequest request)
    {
        var eventEntity = await _dbContext.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == eventId);
        if (eventEntity == null)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (eventEntity.Status == EventStatusEnum.Closed)
        {
            throw new BadRequestException("EVENT_IS_CLOSED");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(eventId);
        }

        var lecturer = await _dbContext.Users.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.LecturerId && !x.IsDisable && x.Status == UserStatusEnum.Active);

        if (lecturer == null || lecturer.Role != RoleEnum.Lecturer)
        {
            throw new NotFoundException("LECTURER_NOT_FOUND");
        }

        var eventRole = await _dbContext.EventRoles.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == request.EventRole && !x.IsDisable);

        if (eventRole == null)
        {
            throw new NotFoundException("EVENT_ROLE_NOT_FOUND");
        }

        // A lecturer cannot be both Mentor and Judge in the same event
        var existingAssignments = await _dbContext.AssignEvents.AsNoTracking()
            .Where(x => x.UserId == request.LecturerId && x.EventId == eventId && !x.IsDisable)
            .ToListAsync();

        if (existingAssignments.Any(x => x.EventRoleId == eventRole.Id))
        {
            throw new ConflictException("LECTURER_ALREADY_ASSIGNED_THIS_ROLE");
        }

        if (existingAssignments.Any(x => x.EventRoleId != eventRole.Id))
        {
            // Already assigned as the other role
            throw new ConflictException("LECTURER_CANNOT_BE_BOTH_MENTOR_AND_JUDGE");
        }

        var newAssignment = new Repository.Entity.AssignEvents
        {
            UserId = request.LecturerId,
            EventRoleId = eventRole.Id,
            EventId = eventId,
            IsDisable = false,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _dbContext.AssignEvents.Add(newAssignment);
        await _dbContext.SaveChangesAsync();

        return new Response.AssignEventResponse
        {
            Id = newAssignment.Id,
            UserId = newAssignment.UserId,
            EventRoleId = newAssignment.EventRoleId,
            EventId = newAssignment.EventId
        };
    }
}
