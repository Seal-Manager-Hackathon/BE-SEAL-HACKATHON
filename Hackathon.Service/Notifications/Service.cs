using Hackathon.Repository;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            throw new UnauthorizedException("INVALID_ACCESS_TOKEN");
        }

        if (!Guid.TryParse(userIdValue, out var userId))
        {
            throw new UnauthorizedException("INVALID_ACCESS_TOKEN");
        }

        return userId;
    }

    public async Task<BasePaginationResponse> GetMyNotifications(PaginationRequest paginationRequest)
    {
        var currentUserId = GetCurrentUserId();

        var query = _dbContext.Notifications
            .AsNoTracking()
            .Where(x => x.UserId == currentUserId && !x.IsDisable);

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Status == NotificationStatusEnum.Unread ? 0 : 1)
            .ThenByDescending(x => x.CreatedAt)
            .Skip((paginationRequest.PageIndex - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
            .Select(x => new Response.NotificationItemResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
    }

    public async Task<string> MarkAsRead(Guid notificationId)
    {
        var currentUserId = GetCurrentUserId();

        var notification = await _dbContext.Notifications
            .FirstOrDefaultAsync(x => x.Id == notificationId && !x.IsDisable);

        if (notification == null)
        {
            throw new NotFoundException("NOTIFICATION_NOT_FOUND");
        }

        if (notification.UserId != currentUserId)
        {
            throw new ForbiddenException("NOTIFICATION_NOT_FOR_CURRENT_USER");
        }

        notification.Status = NotificationStatusEnum.Read;
        notification.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.Notifications.Update(notification);
        await _dbContext.SaveChangesAsync();

        return "NOTIFICATION_MARKED_AS_READ";
    }

    public async Task<string> MarkAllAsRead()
    {
        var currentUserId = GetCurrentUserId();

        var notifications = await _dbContext.Notifications
            .Where(x => x.UserId == currentUserId
                        && !x.IsDisable
                        && (x.Status == NotificationStatusEnum.Unread
                            || x.Status == NotificationStatusEnum.Pending))
            .ToListAsync();

        if (notifications.Any())
        {
            foreach (var notification in notifications)
            {
                notification.Status = NotificationStatusEnum.Read;
                notification.UpdatedAt = DateTimeOffset.UtcNow;
            }

            _dbContext.Notifications.UpdateRange(notifications);
            await _dbContext.SaveChangesAsync();
        }

        return "ALL_NOTIFICATIONS_MARKED_AS_READ";
    }
}
