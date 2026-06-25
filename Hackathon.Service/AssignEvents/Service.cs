using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.AssignEvents.Request;
using Hackathon.Service.AssignEvents.Response;
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

    public async Task<BasePaginationResponse> GetEventAssignments(Guid eventId, EventRoleEnum? eventRole, string? keyword, bool? isDisable, PaginationRequest paginationRequest)
    {
        var eventExists = await _dbContext.Events.AsNoTracking().AnyAsync(x => x.Id == eventId && !x.IsDisable);
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
            .AsNoTracking()
            .Where(x => x.EventId == eventId
                     && x.IsDisable == (isDisable ?? false));

        if (eventRole.HasValue)
        {
            query = query.Where(x => x.EventRole != null && x.EventRole.Name == eventRole.Value);
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
            .Select(x => new AssignLecturerDetailResponse
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
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        if (items.Count == 0)
        {
            throw new NotFoundException("NO_ONE_ASSIGNED_TO_EVENT");
        }

        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
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

    public async Task<AssignEventResponse> AssignLecturerToEvent(Guid eventId, AssignLecturerRequest request)
    {
        var eventExists = await _dbContext.Events.AsNoTracking().AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
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
            .FirstOrDefaultAsync(x => x.Id == request.EventRoleId && !x.IsDisable);

        if (eventRole == null)
        {
            throw new NotFoundException("EVENT_ROLE_NOT_FOUND");
        }

        // A lecturer cannot be both Mentor and Judge in the same event
        var existingAssignments = await _dbContext.AssignEvents.AsNoTracking()
            .Where(x => x.UserId == request.LecturerId && x.EventId == eventId && !x.IsDisable)
            .ToListAsync();

        if (existingAssignments.Any(x => x.EventRoleId == request.EventRoleId))
        {
            throw new ConflictException("LECTURER_ALREADY_ASSIGNED_THIS_ROLE");
        }

        if (existingAssignments.Any(x => x.EventRoleId != request.EventRoleId))
        {
            // Already assigned as the other role
            throw new ConflictException("LECTURER_CANNOT_BE_BOTH_MENTOR_AND_JUDGE");
        }

        var newAssignment = new Repository.Entity.AssignEvents
        {
            UserId = request.LecturerId,
            EventRoleId = request.EventRoleId,
            EventId = eventId,
            IsDisable = false,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _dbContext.AssignEvents.Add(newAssignment);
        await _dbContext.SaveChangesAsync();

        return new AssignEventResponse
        {
            Id = newAssignment.Id,
            UserId = newAssignment.UserId,
            EventRoleId = newAssignment.EventRoleId,
            EventId = newAssignment.EventId
        };
    }
}
