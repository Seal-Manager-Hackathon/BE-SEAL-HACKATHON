using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Teams;

public class Service : IService
{
    private const int MaxTeamMembersBeforeRegisterEvent = 50;
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
        return !string.IsNullOrWhiteSpace(user.FirstName)
               && !string.IsNullOrWhiteSpace(user.LastName)
               && !string.IsNullOrWhiteSpace(user.PhoneNumber)
               && !string.IsNullOrWhiteSpace(user.StudentId)
               && !string.IsNullOrWhiteSpace(user.College);
    }

    private static Response.InvitationResponse ToInvitationResponse(Hackathon.Repository.Entity.Invitations invitation, string message)
    {
        return new Response.InvitationResponse
        {
            Id = invitation.Id,
            TeamId = invitation.TeamId,
            UserId = invitation.UserId,
            Status = invitation.Status?.ToString(),
            Description = invitation.Description,
            LimitTime = invitation.LimitTime,
            Message = message,
        };
    }

    private void EnsureCurrentUserHasRole(RoleEnum role, string errorCode)
    {
        var hasRole = _httpContext.HttpContext?.User.IsInRole(role.ToString()) == true;

        if (!hasRole)
        {
            throw new ForbiddenException(errorCode);
        }
    }

    private async Task<Hackathon.Repository.Entity.TeamDetails> GetLeaderMembership(Guid teamId, Guid userId)
    {
        var leader = await _dbContext.TeamDetails
            .FirstOrDefaultAsync(x => x.TeamId == teamId && x.UserId == userId && x.IsLeader && !x.IsDisable);

        if (leader == null)
        {
            throw new ForbiddenException("ONLY_TEAM_LEADER_CAN_INVITE_MEMBER");
        }

        return leader;
    }

    private async Task<Guid> GetTeamLeaderId(Guid teamId)
    {
        var leaderId = await _dbContext.TeamDetails
            .Where(x => x.TeamId == teamId && x.IsLeader && !x.IsDisable)
            .Select(x => x.UserId)
            .FirstOrDefaultAsync();

        if (leaderId == Guid.Empty)
        {
            throw new NotFoundException("TEAM_LEADER_NOT_FOUND");
        }

        return leaderId;
    }

    /// <summary>
    /// Tạo team mới cho student hiện tại và tự động thêm student đó vào TeamDetails với vai trò leader.
    /// Các lỗi có thể xảy ra: thiếu access token -> MISSING_ACCESS_TOKEN; token không hợp lệ -> INVALID_ACCESS_TOKEN;
    /// user không tồn tại -> USER_NOT_FOUND; user chưa xác thực email -> USER_NOT_VERIFIED;
    /// profile chưa đủ thông tin bắt buộc để tạo/vào team -> USER_PROFILE_NOT_COMPLETED;
    /// tên team rỗng -> TEAM_NAME_REQUIRED; tên team đã tồn tại -> TEAM_NAME_ALREADY_EXISTS.
    /// </summary>
    public async Task<Response.CreateTeamResponse> CreateTeam(Request.CreateTeamRequest request)
    {
        var userId = GetCurrentUserId();
        var teamName = request.Name?.Trim();

        if (string.IsNullOrWhiteSpace(teamName))
        {
            throw new BadRequestException("TEAM_NAME_REQUIRED");
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        if (user.IsVerified != true)
        {
            throw new ForbiddenException("USER_NOT_VERIFIED");
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
        var team = new Hackathon.Repository.Entity.Teams
        {
            Id = Guid.NewGuid(),
            Name = teamName,
            CanEdit = true,
            CreatedAt = now,
            UpdatedAt = now,
        };

        var leader = new Hackathon.Repository.Entity.TeamDetails
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

    /// <summary>
    /// Team leader gửi lời mời cho một student vào team bằng cách tạo bản ghi Pending trong Invitations và tạo thông báo cho student được mời.
    /// Các lỗi có thể xảy ra: thiếu access token -> MISSING_ACCESS_TOKEN; token không hợp lệ -> INVALID_ACCESS_TOKEN;
    /// team không tồn tại -> TEAM_NOT_FOUND; team đã khóa member -> TEAM_MEMBER_LOCKED;
    /// người gọi không phải leader -> ONLY_TEAM_LEADER_CAN_INVITE_MEMBER; user được mời không tồn tại -> INVITED_USER_NOT_FOUND;
    /// user được mời chưa xác thực email -> INVITED_USER_NOT_VERIFIED; profile user được mời chưa đủ -> INVITED_USER_PROFILE_NOT_COMPLETED;
    /// tự mời chính mình -> CANNOT_INVITE_YOURSELF; team đã đủ 50 member -> TEAM_MEMBER_LIMIT_EXCEEDED;
    /// user đã là member -> USER_ALREADY_IN_TEAM; đã có lời mời pending -> INVITATION_ALREADY_PENDING.
    /// </summary>
    public async Task<Response.InvitationResponse> InviteMember(Guid teamId, Request.InviteMemberRequest request)
    {
        var leaderId = GetCurrentUserId();
        if (request.UserId == Guid.Empty)
        {
            throw new BadRequestException("INVITED_USER_ID_REQUIRED");
        }

        if (request.UserId == leaderId)
        {
            throw new BadRequestException("CANNOT_INVITE_YOURSELF");
        }

        EnsureCurrentUserHasRole(RoleEnum.Student, "CURRENT_USER_MUST_BE_STUDENT");

        var team = await _dbContext.Teams.FirstOrDefaultAsync(x => x.Id == teamId && !x.IsDisable);
        if (team == null)
        {
            throw new NotFoundException("TEAM_NOT_FOUND");
        }

        if (!team.CanEdit)
        {
            throw new ForbiddenException("TEAM_MEMBER_LOCKED");
        }

        await GetLeaderMembership(teamId, leaderId);

        var invitedUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId && !x.IsDisable);
        if (invitedUser == null)
        {
            throw new NotFoundException("INVITED_USER_NOT_FOUND");
        }

        var invitedUserIsStudent = await _dbContext.UserRoles
            .Include(x => x.Role)
            .AnyAsync(x => x.UserId == invitedUser.Id
                           && !x.IsDisable
                           && !x.Role.IsDisable
                           && x.Role.Name == RoleEnum.Student);
        if (!invitedUserIsStudent)
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

        var currentMemberCount = await _dbContext.TeamDetails.CountAsync(x => x.TeamId == teamId && !x.IsDisable);
        if (currentMemberCount >= MaxTeamMembersBeforeRegisterEvent)
        {
            throw new ConflictException("TEAM_MEMBER_LIMIT_EXCEEDED");
        }

        var isAlreadyMember = await _dbContext.TeamDetails
            .AnyAsync(x => x.TeamId == teamId && x.UserId == request.UserId && !x.IsDisable);
        if (isAlreadyMember)
        {
            throw new ConflictException("USER_ALREADY_IN_TEAM");
        }

        var hasPendingInvitation = await _dbContext.Invitations.AnyAsync(x =>
            x.TeamId == teamId
            && x.UserId == request.UserId
            && x.Status == InvitationStatusEnum.Pending
            && !x.IsDisable);
        if (hasPendingInvitation)
        {
            throw new ConflictException("INVITATION_ALREADY_PENDING");
        }

        var now = DateTimeOffset.UtcNow;
        var invitation = new Hackathon.Repository.Entity.Invitations
        {
            Id = Guid.NewGuid(),
            TeamId = teamId,
            UserId = request.UserId,
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
            UserId = request.UserId,
            Title = "TEAM_INVITATION_RECEIVED",
            Status = NotificationStatusEnum.Unread,
            Description = $"Bạn nhận được lời mời tham gia team {team.Name}.",
            CreatedAt = now,
            UpdatedAt = now,
        };

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _dbContext.Invitations.AddAsync(invitation);
            await _dbContext.Notifications.AddAsync(notification);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return ToInvitationResponse(invitation, "TEAM_INVITATION_SENT_SUCCESSFULLY");
    }

    /// <summary>
    /// Student được mời xác nhận lời mời: nếu accept thì thêm vào TeamDetails, cập nhật Invitation thành Accepted và thông báo leader; nếu reject thì cập nhật Rejected và thông báo leader.
    /// Các lỗi có thể xảy ra: thiếu access token -> MISSING_ACCESS_TOKEN; token không hợp lệ -> INVALID_ACCESS_TOKEN;
    /// lời mời không tồn tại -> INVITATION_NOT_FOUND; lời mời không thuộc về user hiện tại -> INVITATION_NOT_FOR_CURRENT_USER;
    /// lời mời không còn pending -> INVITATION_ALREADY_RESPONDED; lời mời hết hạn -> INVITATION_EXPIRED;
    /// team không tồn tại -> TEAM_NOT_FOUND; team đã khóa member -> TEAM_MEMBER_LOCKED;
    /// team đã đủ 50 member -> TEAM_MEMBER_LIMIT_EXCEEDED; user đã là member -> USER_ALREADY_IN_TEAM;
    /// không tìm thấy leader để nhận thông báo -> TEAM_LEADER_NOT_FOUND.
    /// </summary>
    public async Task<Response.InvitationResponse> RespondInvitation(Guid invitationId, Request.RespondInvitationRequest request)
    {
        var userId = GetCurrentUserId();
        EnsureCurrentUserHasRole(RoleEnum.Student, "CURRENT_USER_MUST_BE_STUDENT");

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

        var leaderId = await GetTeamLeaderId(team.Id);
        var notificationTitle = request.IsAccepted ? "TEAM_INVITATION_ACCEPTED" : "TEAM_INVITATION_REJECTED";
        var notificationDescription = request.IsAccepted
            ? $"Member {userId} đã chấp nhận lời mời vào team {team.Name}."
            : $"Member {userId} đã từ chối lời mời vào team {team.Name}.";

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            if (request.IsAccepted)
            {
                var currentMemberCount = await _dbContext.TeamDetails.CountAsync(x => x.TeamId == team.Id && !x.IsDisable);
                if (currentMemberCount >= MaxTeamMembersBeforeRegisterEvent)
                {
                    throw new ConflictException("TEAM_MEMBER_LIMIT_EXCEEDED");
                }

                var isAlreadyMember = await _dbContext.TeamDetails
                    .AnyAsync(x => x.TeamId == team.Id && x.UserId == userId && !x.IsDisable);
                if (isAlreadyMember)
                {
                    throw new ConflictException("USER_ALREADY_IN_TEAM");
                }

                await _dbContext.TeamDetails.AddAsync(new Hackathon.Repository.Entity.TeamDetails
                {
                    Id = Guid.NewGuid(),
                    TeamId = team.Id,
                    UserId = userId,
                    IsLeader = false,
                    Status = TeamDetailStatusEnum.Active,
                    CreatedAt = now,
                    UpdatedAt = now,
                });

                invitation.Status = InvitationStatusEnum.Accepted;
            }
            else
            {
                invitation.Status = InvitationStatusEnum.Rejected;
            }

            invitation.UpdatedAt = now;

            await _dbContext.Notifications.AddAsync(new Hackathon.Repository.Entity.Notifications
            {
                Id = Guid.NewGuid(),
                TeamId = team.Id,
                UserId = leaderId,
                Title = notificationTitle,
                Status = NotificationStatusEnum.Unread,
                Description = notificationDescription,
                CreatedAt = now,
                UpdatedAt = now,
            });

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return ToInvitationResponse(invitation, request.IsAccepted
            ? "TEAM_INVITATION_ACCEPTED_SUCCESSFULLY"
            : "TEAM_INVITATION_REJECTED_SUCCESSFULLY");
    }
}
