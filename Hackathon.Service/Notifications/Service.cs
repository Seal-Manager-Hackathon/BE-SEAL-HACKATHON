using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Notifications;

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

    public async Task<(List<Response.NotificationResponse> Items, int TotalCount)> GetNotifications(string? status, int pageIndex, int pageSize)
    {
        if (pageIndex < 1 || pageSize < 1)
        {
            throw new BadRequestException("BAD_REQUEST");
        }

        var userId = GetCurrentUserId();
        var query = _dbContext.Notifications.AsNoTracking().Where(x => x.UserId == userId && !x.IsDisable);

        if (!string.IsNullOrWhiteSpace(status))
        {
            if (!Enum.TryParse<NotificationStatusEnum>(status, true, out var notificationStatus))
            {
                throw new BadRequestException("BAD_REQUEST");
            }

            query = query.Where(x => x.Status == notificationStatus);
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.NotificationResponse
            {
                Id = x.Id,
                UserId = x.UserId,
                TeamId = x.TeamId,
                Title = x.Title,
                Status = x.Status.ToString(),
                Description = x.Description,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();

        return (items, totalCount);
    }
}
