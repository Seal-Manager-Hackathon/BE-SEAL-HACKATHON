using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Teams;

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

    private static bool IsProfileCompleted(Hackathon.Repository.Entity.Users user)
    {
        return !string.IsNullOrWhiteSpace(user.Email)
               && !string.IsNullOrWhiteSpace(user.HashPassword)
               && !string.IsNullOrWhiteSpace(user.FirstName)
               && !string.IsNullOrWhiteSpace(user.LastName)
               && !string.IsNullOrWhiteSpace(user.PhoneNumber)
               && !string.IsNullOrWhiteSpace(user.Address)
               && user.DateOfBirth != DateTimeOffset.MinValue
               && !string.IsNullOrWhiteSpace(user.StudentId)
               && !string.IsNullOrWhiteSpace(user.College);
    }

    public async Task<Response.CreateTeamResponse> CreateTeam(Request.CreateTeamRequest request)
    {
        var userId = GetCurrentUserId();
        var teamName = request.TeamName?.Trim();

        if (string.IsNullOrWhiteSpace(teamName))
        {
            throw new BadRequestException("TEAM_NAME_REQUIRED");
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null || user.IsDisable == true)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        if (user.IsVerified != true)
        {
            throw new ForbiddenException("USER_NOT_VERIFIED");
        }

        if (user.Role != RoleEnum.Student)
        {
            throw new ForbiddenException("CURRENT_USER_MUST_BE_STUDENT");
        }

        if (!IsProfileCompleted(user))
        {
            throw new BadRequestException("USER_PROFILE_NOT_COMPLETED");
        }

        var isDuplicatedName = await _dbContext.Teams.AnyAsync(x => x.Name.ToLower() == teamName.ToLower());
        if (isDuplicatedName)
        {
            throw new ConflictException("TEAM_NAME_ALREADY_EXISTS");
        }

        var now = DateTimeOffset.UtcNow;
        var team = new Repository.Entity.Teams
        {
            Id = Guid.NewGuid(),
            Name = teamName,
            CanEdit = true,
            CreatedAt = now,
            UpdatedAt = now,
        };

        var leader = new TeamDetails
        {
            Id = Guid.NewGuid(),
            TeamId = team.Id,
            UserId = user.Id,
            IsLeader = true,
            Status = TeamDetailStatusEnum.Active,
            CreatedAt = now,
            UpdatedAt = now,
        };

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _dbContext.Teams.AddAsync(team);
            await _dbContext.TeamDetails.AddAsync(leader);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return new Response.CreateTeamResponse
        {
            Id = team.Id,
            Name = team.Name,
            CanEdit = team.CanEdit,
            CreatedAt = team.CreatedAt,
            Message = "TEAM_CREATED_SUCCESSFULLY",
            Members = new List<Response.TeamMemberResponse>
            {
                new()
                {
                    UserId = leader.UserId,
                    IsLeader = leader.IsLeader,
                    Status = leader.Status?.ToString(),
                }
            }
        };
    }

    public async Task<Response.MessageResponse> InviteMember(Guid teamId, Request.InviteMemberRequest request)
    {
        var leaderId = GetCurrentUserId();
        var targetEmail = request.Email?.Trim();

        if (string.IsNullOrWhiteSpace(targetEmail))
        {
            throw new BadRequestException("EMAIL_REQUIRED");
        }

        // Check current user role
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == leaderId);
        if (user == null || user.IsDisable == true)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        if (user.Role != RoleEnum.Student)
        {
            throw new ForbiddenException("CURRENT_USER_MUST_BE_STUDENT");
        }

        // Check team status
        var team = await _dbContext.Teams.FirstOrDefaultAsync(x => x.Id == teamId && !x.IsDisable);
        if (team == null)
        {
            throw new NotFoundException("TEAM_NOT_FOUND");
        }

        if (!team.CanEdit)
        {
            throw new ForbiddenException("TEAM_MEMBER_LOCKED");
        }

        // Check current user is the leader of the team
        var isLeader = await _dbContext.TeamDetails.AnyAsync(x => x.TeamId == teamId && x.UserId == leaderId && x.IsLeader && !x.IsDisable);
        if (!isLeader)
        {
            throw new ForbiddenException("ONLY_TEAM_LEADER_CAN_INVITE_MEMBER");
        }

        // Check target user status
        var invitedUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == targetEmail.ToLower() && !x.IsDisable);
        if (invitedUser == null)
        {
            throw new NotFoundException("INVITED_USER_NOT_FOUND");
        }

        if (invitedUser.Id == leaderId)
        {
            throw new BadRequestException("CANNOT_INVITE_YOURSELF");
        }

        if (invitedUser.Role != RoleEnum.Student)
        {
            throw new ForbiddenException("INVITED_USER_MUST_BE_STUDENT");
        }

        if (invitedUser.IsVerified != true)
        {
            throw new ForbiddenException("INVITED_USER_NOT_VERIFIED");
        }

        if (!IsProfileCompleted(invitedUser))
        {
            throw new BadRequestException("INVITED_USER_PROFILE_NOT_COMPLETED");
        }

        // Check team limits
        var currentMemberCount = await _dbContext.TeamDetails.CountAsync(x => x.TeamId == teamId && !x.IsDisable);
        if (currentMemberCount >= 50)
        {
            throw new ConflictException("TEAM_MEMBER_LIMIT_EXCEEDED");
        }

        // Check memberships and pending invitations
        var isAlreadyMember = await _dbContext.TeamDetails.AnyAsync(x => x.TeamId == teamId && x.UserId == invitedUser.Id && !x.IsDisable);
        if (isAlreadyMember)
        {
            throw new ConflictException("USER_ALREADY_IN_TEAM");
        }

        var hasPendingInvitation = await _dbContext.Invitations.AnyAsync(x =>
            x.TeamId == teamId
            && x.UserId == invitedUser.Id
            && x.Status == InvitationStatusEnum.Pending
            && !x.IsDisable);
        if (hasPendingInvitation)
        {
            throw new ConflictException("INVITATION_ALREADY_PENDING");
        }

        // Perform DB changes
        var now = DateTimeOffset.UtcNow;
        var invitation = new Hackathon.Repository.Entity.Invitations
        {
            Id = Guid.NewGuid(),
            TeamId = teamId,
            UserId = invitedUser.Id,
            LimitTime = now.AddDays(7),
            Status = InvitationStatusEnum.Pending,
            Description = request.Description,
            CreatedAt = now,
            UpdatedAt = now,
        };

        var notification = new Hackathon.Repository.Entity.Notifications
        {
            Id = Guid.NewGuid(),
            TeamId = teamId,
            UserId = invitedUser.Id,
            Title = "TEAM_INVITATION_RECEIVED",
            Status = NotificationStatusEnum.Unread,
            Description = $"Bạn nhận được lời mời tham gia team {team.Name}.",
            CreatedAt = now,
            UpdatedAt = now,
        };

        await _dbContext.Invitations.AddAsync(invitation);
        await _dbContext.Notifications.AddAsync(notification);
        await _dbContext.SaveChangesAsync();

        return new Response.MessageResponse { Message = "INVITATION_SENT_SUCCESSFULLY" };
    }

    public async Task<BasePaginationResponse> GetMyTeams(PaginationRequest paginationRequest)
    {
        var userId = GetCurrentUserId();

        var pageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        var pageSize = paginationRequest.PageSize <= 0 ? 10 : Math.Min(paginationRequest.PageSize, 100);

        var query = _dbContext.TeamDetails
            .AsNoTracking()
            .Include(x => x.Team)
            .Where(x => x.UserId == userId
                        && !x.IsDisable
                        && !x.Team.IsDisable
                        && x.Status == TeamDetailStatusEnum.Active);

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.Team.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.MyTeamResponse
            {
                TeamId = x.TeamId,
                TeamName = x.Team.Name,
                CanEdit = x.Team.CanEdit,
                IsLeader = x.IsLeader,
                MemberStatus = x.Status.ToString(),
                JoinedAt = x.CreatedAt
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
    }

    public async Task<Response.TeamDetailResponse> GetTeamDetail(Guid teamId)
    {
        var userId = GetCurrentUserId();
        var team = await _dbContext.Teams
            .AsNoTracking()
            .Include(x => x.TeamDetails)
            .FirstOrDefaultAsync(x => x.Id == teamId && !x.IsDisable);

        if (team == null)
        {
            throw new NotFoundException("TEAM_NOT_FOUND");
        }

        var isMember = team.TeamDetails.Any(x => x.UserId == userId && !x.IsDisable);
        var isStaff = _httpContext.HttpContext?.User.IsInRole(RoleEnum.Staff.ToString()) == true
                      || _httpContext.HttpContext?.User.IsInRole(RoleEnum.Admin.ToString()) == true;
        if (!isMember && !isStaff)
        {
            throw new ForbiddenException("TEAM_NOT_VISIBLE_TO_USER");
        }

        return new Response.TeamDetailResponse
        {
            Id = team.Id,
            Name = team.Name,
            CanEdit = team.CanEdit,
            CreatedAt = team.CreatedAt,
            Members = team.TeamDetails
                .Where(x => !x.IsDisable)
                .OrderByDescending(x => x.IsLeader)
                .ThenBy(x => x.CreatedAt)
                .Select(x => new Response.TeamMemberResponse
                {
                    UserId = x.UserId,
                    IsLeader = x.IsLeader,
                    Status = x.Status?.ToString()
                })
                .ToList()
        };
    }
}
