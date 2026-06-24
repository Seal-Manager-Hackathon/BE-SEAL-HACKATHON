using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Hackathon.Repository;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Lecturers;

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

    public async Task<BasePaginationResponse> GetLecturerEvents(PaginationRequest request)
    {
        var userId = GetCurrentUserId();

        // 1. Check if user exists and is a Lecturer (Enum comparison)
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null || user.Role != RoleEnum.Lecturer)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        var pageIndex = request.PageIndex <= 0 ? 1 : request.PageIndex;
        var pageSize = request.PageSize <= 0 ? 10 : Math.Min(request.PageSize, 100);

        // 2. Build Query joining AssignEvents, Events and EventRoles
        var query = _dbContext.AssignEvents
            .AsNoTracking()
            .Include(x => x.Event)
            .Include(x => x.EventRole)
            .Where(x => x.UserId == userId
                        && !x.IsDisable
                        && !x.Event.IsDisable);

        var totalCount = await query.CountAsync();

        // 5. Paginate and map to Response DTO
        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.LecturerEventResponse
            {
                AssignEventId = x.Id,
                EventId = x.EventId,
                EventName = x.Event.Name,
                Season = x.Event.Season,
                StartTime = x.Event.StartTime,
                EndTime = x.Event.EndTime,
                Role = x.EventRole != null ? (EventRoleEnum?)x.EventRole.Name : null,
                EventStatus = x.Event.Status
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
    }

    public async Task<BasePaginationResponse> SearchLecturerEvents(Request.SearchLecturerEventsRequest request)
    {
        var userId = GetCurrentUserId();

        // 1. Check if user exists and is a Lecturer (Enum comparison)
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null || user.Role != RoleEnum.Lecturer)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        var pageIndex = request.PageIndex <= 0 ? 1 : request.PageIndex;
        var pageSize = request.PageSize <= 0 ? 10 : Math.Min(request.PageSize, 100);

        // 2. Build base query
        var query = _dbContext.AssignEvents
            .AsNoTracking()
            .Include(x => x.Event)
            .Include(x => x.EventRole)
            .Where(x => x.UserId == userId
                        && !x.IsDisable
                        && !x.Event.IsDisable);

        // 3. Filter by Keyword (Event.Name)
        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var normKeyword = request.Keyword.Trim().ToLower();
            query = query.Where(x => x.Event.Name.ToLower().Contains(normKeyword));
        }

        // 4. Filter by Year (Event.StartTime.Year)
        if (request.Year.HasValue)
        {
            query = query.Where(x => x.Event.StartTime.HasValue && x.Event.StartTime.Value.Year == request.Year.Value);
        }

        // 5. Filter by EventRole if provided
        if (request.EventRole.HasValue)
        {
            query = query.Where(x => x.EventRole != null && x.EventRole.Name == request.EventRole.Value);
        }

        var totalCount = await query.CountAsync();

        // 6. Sort by StartTime desc, then Event.Name asc, then paginate
        var items = await query
            .OrderByDescending(x => x.Event.StartTime)
            .ThenBy(x => x.Event.Name)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.LecturerEventResponse
            {
                AssignEventId = x.Id,
                EventId = x.EventId,
                EventName = x.Event.Name,
                Season = x.Event.Season,
                StartTime = x.Event.StartTime,
                EndTime = x.Event.EndTime,
                Role = x.EventRole != null ? (EventRoleEnum?)x.EventRole.Name : null,
                EventStatus = x.Event.Status
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
    }
}
