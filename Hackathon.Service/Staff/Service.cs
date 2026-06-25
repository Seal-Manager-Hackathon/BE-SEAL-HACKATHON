using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Hackathon.Repository;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Staff;

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

    public async Task<List<Response.StaffEventResponse>> GetCurrentStaffEvents()
    {
        var userId = GetCurrentUserId();

        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null || user.Role != RoleEnum.Staff)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        var now = DateTimeOffset.UtcNow;

        var items = await _dbContext.AssignEvents
            .AsNoTracking()
            .Include(x => x.Event)
            .Include(x => x.EventRole)
            .Where(x => x.UserId == userId
                        && !x.IsDisable
                        && !x.Event.IsDisable
                        && x.Event.StartTime.HasValue
                        && x.Event.StartTime.Value <= now
                        && x.Event.EndTime.HasValue
                        && x.Event.EndTime.Value >= now)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new Response.StaffEventResponse
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

        if (items.Count == 0)
        {
            throw new NotFoundException("NOT_ASSIGNED_TO_ANY_EVENT");
        }

        return items;
    }

    public async Task<BasePaginationResponse> GetStaffEvents(PaginationRequest request)
    {
        var userId = GetCurrentUserId();

        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null || user.Role != RoleEnum.Staff)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        var pageIndex = request.PageIndex <= 0 ? 1 : request.PageIndex;
        var pageSize = request.PageSize <= 0 ? 10 : Math.Min(request.PageSize, 100);

        var query = _dbContext.AssignEvents
            .AsNoTracking()
            .Include(x => x.Event)
            .Include(x => x.EventRole)
            .Where(x => x.UserId == userId
                        && !x.IsDisable
                        && !x.Event.IsDisable);

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.StaffEventResponse
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

    public async Task<BasePaginationResponse> SearchStaffEvents(Request.SearchStaffEventsRequest request)
    {
        var userId = GetCurrentUserId();

        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null || user.Role != RoleEnum.Staff)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        var pageIndex = request.PageIndex <= 0 ? 1 : request.PageIndex;
        var pageSize = request.PageSize <= 0 ? 10 : Math.Min(request.PageSize, 100);

        var query = _dbContext.AssignEvents
            .AsNoTracking()
            .Include(x => x.Event)
            .Include(x => x.EventRole)
            .Where(x => x.UserId == userId
                        && !x.IsDisable
                        && !x.Event.IsDisable);

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var normKeyword = request.Keyword.Trim().ToLower();
            query = query.Where(x => x.Event.Name.ToLower().Contains(normKeyword));
        }

        if (request.Status.HasValue)
        {
            query = query.Where(x => x.Event.Status == request.Status.Value);
        }

        if (request.Year.HasValue || request.Month.HasValue)
        {
            var year = request.Year ?? DateTimeOffset.UtcNow.Year;
            DateTimeOffset startDate;
            DateTimeOffset endDate;

            if (request.Month.HasValue)
            {
                startDate = new DateTimeOffset(year, request.Month.Value, 1, 0, 0, 0, TimeSpan.Zero);
                endDate = startDate.AddMonths(1);
            }
            else
            {
                startDate = new DateTimeOffset(year, 1, 1, 0, 0, 0, TimeSpan.Zero);
                endDate = startDate.AddYears(1);
            }

            query = query.Where(x => x.Event.StartTime.HasValue
                        && x.Event.StartTime.Value >= startDate
                        && x.Event.EndTime.HasValue
                        && x.Event.EndTime.Value < endDate);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.Event.StartTime)
            .ThenBy(x => x.Event.Name)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.StaffEventResponse
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
