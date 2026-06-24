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

        // 1. Check if user exists and is a Lecturer
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
                Role = x.EventRole != null ? x.EventRole.Name.ToString() : string.Empty
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
    }
}
