using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Hackathon.Repository;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Invitations;

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

    public async Task<BasePaginationResponse> GetMyInvitations(int pageIndex, int pageSize)
    {
        var userId = GetCurrentUserId();

        pageIndex = pageIndex <= 0 ? 1 : pageIndex;
        pageSize = pageSize <= 0 ? 10 : Math.Min(pageSize, 100);

        var query = _dbContext.Invitations
            .AsNoTracking()
            .Include(x => x.Team)
            .Where(x => x.UserId == userId && !x.IsDisable && !x.Team.IsDisable);

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.Status == InvitationStatusEnum.Pending)
            .ThenByDescending(x => x.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.InvitationItemResponse
            {
                Id = x.Id,
                TeamId = x.TeamId,
                TeamName = x.Team.Name,
                Status = x.Status.ToString(),
                Description = x.Description,
                LimitTime = x.LimitTime,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
    }
}
