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

    public async Task<BasePaginationResponse> GetMyInvitations(PaginationRequest paginationRequest)
    {
        var userId = GetCurrentUserId();

        var pageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        var pageSize = paginationRequest.PageSize <= 0 ? 10 : Math.Min(paginationRequest.PageSize, 100);

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
                CreatedAt = x.CreatedAt,
                LeaderName = _dbContext.TeamDetails
                    .Where(td => td.TeamId == x.TeamId && td.IsLeader && !td.IsDisable)
                    .Select(td => td.User.FirstName + " " + td.User.LastName)
                    .FirstOrDefault()
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
    }

    private static Response.InvitationItemResponse MapToResponse(Repository.Entity.Invitations x)
    {
        return new Response.InvitationItemResponse
        {
            Id = x.Id,
            TeamId = x.TeamId,
            TeamName = x.Team?.Name ?? string.Empty,
            Status = x.Status.ToString(),
            Description = x.Description,
            LimitTime = x.LimitTime,
            CreatedAt = x.CreatedAt
        };
    }

    public async Task<Response.InvitationItemResponse> AcceptInvitation(Guid invitationId)
    {
        var userId = GetCurrentUserId();

        // Check current user role & status
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null || user.IsDisable == true)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        if (user.Role != RoleEnum.Student)
        {
            throw new ForbiddenException("CURRENT_USER_MUST_BE_STUDENT");
        }

        // Find invitation
        var invitation = await _dbContext.Invitations
            .Include(x => x.Team)
            .FirstOrDefaultAsync(x => x.Id == invitationId && !x.IsDisable);

        if (invitation == null)
        {
            throw new NotFoundException("INVITATION_NOT_FOUND");
        }

        if (invitation.UserId != userId)
        {
            throw new ForbiddenException("INVITATION_NOT_FOR_CURRENT_USER");
        }

        if (invitation.Status != InvitationStatusEnum.Pending)
        {
            throw new ConflictException("INVITATION_ALREADY_RESPONDED");
        }

        var now = DateTimeOffset.UtcNow;
        if (invitation.LimitTime.HasValue && invitation.LimitTime.Value < now)
        {
            invitation.Status = InvitationStatusEnum.Expired;
            invitation.UpdatedAt = now;
            await _dbContext.SaveChangesAsync();
            throw new BadRequestException("INVITATION_EXPIRED");
        }

        var team = invitation.Team;
        if (team == null || team.IsDisable)
        {
            throw new NotFoundException("TEAM_NOT_FOUND");
        }

        if (!team.CanEdit)
        {
            throw new ForbiddenException("TEAM_MEMBER_LOCKED");
        }

        // Check already member
        var isAlreadyMember = await _dbContext.TeamDetails.AnyAsync(x => x.TeamId == team.Id && x.UserId == userId && !x.IsDisable);
        if (isAlreadyMember)
        {
            throw new ConflictException("USER_ALREADY_IN_TEAM");
        }

        // Check team limits
        var currentMemberCount = await _dbContext.TeamDetails.CountAsync(x => x.TeamId == team.Id && !x.IsDisable);
        if (currentMemberCount >= 50)
        {
            throw new ConflictException("TEAM_MEMBER_LIMIT_EXCEEDED");
        }

        // Get team leader
        var leaderId = await _dbContext.TeamDetails
            .Where(x => x.TeamId == team.Id && x.IsLeader && !x.IsDisable)
            .Select(x => x.UserId)
            .FirstOrDefaultAsync();

        if (leaderId == Guid.Empty)
        {
            throw new NotFoundException("TEAM_LEADER_NOT_FOUND");
        }

        // Perform DB updates in transaction
        var teamDetail = new Repository.Entity.TeamDetails
        {
            Id = Guid.NewGuid(),
            TeamId = team.Id,
            UserId = userId,
            IsLeader = false,
            Status = TeamDetailStatusEnum.Active,
            CreatedAt = now,
            UpdatedAt = now
        };

        invitation.Status = InvitationStatusEnum.Accepted;
        invitation.UpdatedAt = now;

        var notification = new Repository.Entity.Notifications
        {
            Id = Guid.NewGuid(),
            TeamId = team.Id,
            UserId = leaderId,
            Title = "TEAM_INVITATION_ACCEPTED",
            Status = NotificationStatusEnum.Unread,
            Description = $"Thành viên {user.FirstName} {user.LastName} đã chấp nhận lời mời vào team {team.Name}.",
            CreatedAt = now,
            UpdatedAt = now
        };

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _dbContext.TeamDetails.AddAsync(teamDetail);
            await _dbContext.Notifications.AddAsync(notification);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return MapToResponse(invitation);
    }

    public async Task<Response.InvitationItemResponse> RejectInvitation(Guid invitationId)
    {
        var userId = GetCurrentUserId();

        // Check current user role & status
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null || user.IsDisable == true)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        if (user.Role != RoleEnum.Student)
        {
            throw new ForbiddenException("CURRENT_USER_MUST_BE_STUDENT");
        }

        // Find invitation
        var invitation = await _dbContext.Invitations
            .Include(x => x.Team)
            .FirstOrDefaultAsync(x => x.Id == invitationId && !x.IsDisable);

        if (invitation == null)
        {
            throw new NotFoundException("INVITATION_NOT_FOUND");
        }

        if (invitation.UserId != userId)
        {
            throw new ForbiddenException("INVITATION_NOT_FOR_CURRENT_USER");
        }

        if (invitation.Status != InvitationStatusEnum.Pending)
        {
            throw new ConflictException("INVITATION_ALREADY_RESPONDED");
        }

        var now = DateTimeOffset.UtcNow;
        if (invitation.LimitTime.HasValue && invitation.LimitTime.Value < now)
        {
            invitation.Status = InvitationStatusEnum.Expired;
            invitation.UpdatedAt = now;
            await _dbContext.SaveChangesAsync();
            throw new BadRequestException("INVITATION_EXPIRED");
        }

        // Get team leader
        var leaderId = await _dbContext.TeamDetails
            .Where(x => x.TeamId == invitation.TeamId && x.IsLeader && !x.IsDisable)
            .Select(x => x.UserId)
            .FirstOrDefaultAsync();

        if (leaderId == Guid.Empty)
        {
            throw new NotFoundException("TEAM_LEADER_NOT_FOUND");
        }

        // Perform DB updates in transaction
        invitation.Status = InvitationStatusEnum.Rejected;
        invitation.UpdatedAt = now;

        var notification = new Repository.Entity.Notifications
        {
            Id = Guid.NewGuid(),
            TeamId = invitation.TeamId,
            UserId = leaderId,
            Title = "TEAM_INVITATION_REJECTED",
            Status = NotificationStatusEnum.Unread,
            Description = $"Thành viên {user.FirstName} {user.LastName} đã từ chối lời mời vào team {invitation.Team?.Name}.",
            CreatedAt = now,
            UpdatedAt = now
        };

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _dbContext.Notifications.AddAsync(notification);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return MapToResponse(invitation);
    }
}
