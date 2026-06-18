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

    private static bool IsProfileCompleted(Users user)
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

    private async Task<Users> ValidateAndGetStudentAsync(Guid userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null || user.IsDisable == true)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        if (user.Role != RoleEnum.Student)
        {
            throw new ForbiddenException("CURRENT_USER_MUST_BE_STUDENT");
        }

        return user;
    }

    private async Task<Repository.Entity.Teams> ValidateAndGetEditableTeamAsync(Guid teamId)
    {
        var team = await _dbContext.Teams.FirstOrDefaultAsync(x => x.Id == teamId && !x.IsDisable);
        if (team == null)
        {
            throw new NotFoundException("TEAM_NOT_FOUND");
        }

        if (!team.CanEdit)
        {
            throw new ForbiddenException("TEAM_MEMBER_LOCKED");
        }

        var hasPendingOrApproved = await _dbContext.RegisterTeams
            .AnyAsync(x => x.TeamId == teamId && !x.IsDisable && (x.Status == RegisterTeamStatusEnum.Pending || x.Status == RegisterTeamStatusEnum.Approved));

        if (hasPendingOrApproved)
        {
            throw new ForbiddenException("TEAM_LOCKED_DUE_TO_REGISTRATION_STATUS");
        }

        return team;
    }

    private async Task<TeamDetails> ValidateAndGetLeaderDetailAsync(Guid teamId, Guid userId, string errorCode = "ONLY_TEAM_LEADER_CAN_PERFORM_ACTION")
    {
        var leaderDetail = await _dbContext.TeamDetails.FirstOrDefaultAsync(x => x.TeamId == teamId && x.UserId == userId && x.IsLeader && !x.IsDisable);
        if (leaderDetail == null)
        {
            throw new ForbiddenException(errorCode);
        }

        return leaderDetail;
    }

    public async Task<Response.CreateTeamResponse> CreateTeam(Request.CreateTeamRequest request)
    {
        var userId = GetCurrentUserId();
        var teamName = request.TeamName?.Trim();

        if (string.IsNullOrWhiteSpace(teamName))
        {
            throw new BadRequestException("TEAM_NAME_REQUIRED");
        }

        var user = await ValidateAndGetStudentAsync(userId);

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
        await ValidateAndGetStudentAsync(leaderId);

        // Check team status
        var team = await ValidateAndGetEditableTeamAsync(teamId);

        // Check current user is the leader of the team
        await ValidateAndGetLeaderDetailAsync(teamId, leaderId, "ONLY_TEAM_LEADER_CAN_INVITE_MEMBER");

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
            .Skip((paginationRequest.PageIndex - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
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

        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
    }

    public async Task<Response.TeamDetailResponse> GetTeamDetail(Guid teamId)
    {
        var userId = GetCurrentUserId();
        var team = await _dbContext.Teams
            .AsNoTracking()
            .Include(x => x.TeamDetails)
                .ThenInclude(td => td.User)
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
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    DateOfBirth = x.User.DateOfBirth,
                    StudentId = x.User.StudentId,
                    College = x.User.College,
                    IsLeader = x.IsLeader,
                    Status = x.Status?.ToString()
                })
                .ToList()
        };
    }

    public async Task<Response.MessageResponse> UpdateTeam(Guid teamId, Request.UpdateTeamRequest request)
    {
        var userId = GetCurrentUserId();
        var newTeamName = request.TeamName?.Trim();

        if (string.IsNullOrWhiteSpace(newTeamName))
        {
            throw new BadRequestException("TEAM_NAME_REQUIRED");
        }

        // Check current user role & status
        await ValidateAndGetStudentAsync(userId);

        // Find team
        var team = await ValidateAndGetEditableTeamAsync(teamId);

        // Check if current user is leader
        await ValidateAndGetLeaderDetailAsync(teamId, userId, "ONLY_TEAM_LEADER_CAN_UPDATE_TEAM");

        // Check duplicated team name if changed
        if (team.Name.ToLower() != newTeamName.ToLower())
        {
            var isDuplicatedName = await _dbContext.Teams.AnyAsync(x => x.Name.ToLower() == newTeamName.ToLower() && x.Id != teamId);
            if (isDuplicatedName)
            {
                throw new ConflictException("TEAM_NAME_ALREADY_EXISTS");
            }
        }

        // Update team
        team.Name = newTeamName;
        team.UpdatedAt = DateTimeOffset.UtcNow;

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            _dbContext.Teams.Update(team);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return new Response.MessageResponse { Message = "TEAM_UPDATED_SUCCESSFULLY" };
    }

    public async Task<Response.MessageResponse> RemoveMembers(Guid teamId, Request.RemoveMembersRequest request)
    {
        var leaderId = GetCurrentUserId();

        if (request.UserIds == null || request.UserIds.Count == 0)
        {
            throw new BadRequestException("USER_IDS_REQUIRED");
        }

        // Check current user role & status
        await ValidateAndGetStudentAsync(leaderId);

        // Find team
        var team = await ValidateAndGetEditableTeamAsync(teamId);

        // Check if current user is leader
        await ValidateAndGetLeaderDetailAsync(teamId, leaderId, "ONLY_TEAM_LEADER_CAN_REMOVE_MEMBER");

        if (request.UserIds.Contains(leaderId))
        {
            throw new BadRequestException("CANNOT_REMOVE_YOURSELF");
        }

        var membersToRemove = await _dbContext.TeamDetails
            .Where(x => x.TeamId == teamId && request.UserIds.Contains(x.UserId) && !x.IsDisable)
            .ToListAsync();

        if (membersToRemove.Count == 0)
        {
            throw new NotFoundException("NO_MATCHING_MEMBERS_FOUND");
        }

        var now = DateTimeOffset.UtcNow;
        foreach (var member in membersToRemove)
        {
            member.IsDisable = true;
            member.UpdatedAt = now;
        }

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            _dbContext.TeamDetails.UpdateRange(membersToRemove);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return new Response.MessageResponse { Message = "MEMBERS_REMOVED_SUCCESSFULLY" };
    }

    public async Task<Response.MessageResponse> TransferLeader(Guid teamId, Request.TransferLeaderRequest request)
    {
        var leaderId = GetCurrentUserId();

        if (request.NewLeaderId == Guid.Empty)
        {
            throw new BadRequestException("NEW_LEADER_ID_REQUIRED");
        }

        // Check current user role & status
        await ValidateAndGetStudentAsync(leaderId);

        // Find team
        var team = await ValidateAndGetEditableTeamAsync(teamId);

        // Check if current user is leader
        var currentLeaderDetail = await ValidateAndGetLeaderDetailAsync(teamId, leaderId, "ONLY_TEAM_LEADER_CAN_TRANSFER_ROLE");

        if (request.NewLeaderId == leaderId)
        {
            throw new BadRequestException("ALREADY_THE_LEADER");
        }

        var newLeaderDetail = await _dbContext.TeamDetails.FirstOrDefaultAsync(x => x.TeamId == teamId && x.UserId == request.NewLeaderId && !x.IsDisable);
        if (newLeaderDetail == null)
        {
            throw new NotFoundException("NEW_LEADER_NOT_IN_TEAM");
        }

        var now = DateTimeOffset.UtcNow;

        currentLeaderDetail.IsLeader = false;
        currentLeaderDetail.UpdatedAt = now;

        newLeaderDetail.IsLeader = true;
        newLeaderDetail.UpdatedAt = now;

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            _dbContext.TeamDetails.Update(currentLeaderDetail);
            _dbContext.TeamDetails.Update(newLeaderDetail);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return new Response.MessageResponse { Message = "LEADER_TRANSFERRED_SUCCESSFULLY" };
    }

    public async Task<Response.RegisterEventResponse> RegisterEvent(Request.RegisterEventRequest request)
    {
        var userId = GetCurrentUserId();

        if (request.TeamId == Guid.Empty)
        {
            throw new BadRequestException("TEAM_ID_REQUIRED");
        }

        if (request.EventId == Guid.Empty)
        {
            throw new BadRequestException("EVENT_ID_REQUIRED");
        }

        // Validate user
        await ValidateAndGetStudentAsync(userId);

        // Validate team & leadership
        var team = await _dbContext.Teams
            .Include(x => x.TeamDetails)
            .FirstOrDefaultAsync(x => x.Id == request.TeamId && !x.IsDisable);

        if (team == null)
        {
            throw new NotFoundException("TEAM_NOT_FOUND");
        }

        var leaderDetail = team.TeamDetails.FirstOrDefault(x => x.UserId == userId && x.IsLeader && !x.IsDisable);
        if (leaderDetail == null)
        {
            throw new ForbiddenException("ONLY_TEAM_LEADER_CAN_REGISTER_EVENT");
        }

        // Validate event
        var eventEntity = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == request.EventId && !x.IsDisable);
        if (eventEntity == null)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var now = DateTimeOffset.UtcNow;
        if (eventEntity.RegisterLimitTime.HasValue && now > eventEntity.RegisterLimitTime.Value)
        {
            throw new BadRequestException("EVENT_REGISTRATION_CLOSED");
        }

        // Validate team member count limits for this event
        var activeMembersCount = team.TeamDetails.Count(x => !x.IsDisable && x.Status == TeamDetailStatusEnum.Active);

        if (eventEntity.MinMember.HasValue && activeMembersCount < eventEntity.MinMember.Value)
        {
            throw new BadRequestException($"TEAM_DOES_NOT_MEET_MIN_MEMBERS_{eventEntity.MinMember.Value}");
        }

        if (eventEntity.MaxMember.HasValue && activeMembersCount > eventEntity.MaxMember.Value)
        {
            throw new BadRequestException($"TEAM_EXCEEDS_MAX_MEMBERS_{eventEntity.MaxMember.Value}");
        }

        // Check if already registered
        var existingRegistrations = await _dbContext.RegisterTeams
            .Where(x => x.TeamId == request.TeamId && !x.IsDisable)
            .ToListAsync();

        var existingForThisEvent = existingRegistrations.FirstOrDefault(x => x.EventId == request.EventId);

        if (existingForThisEvent != null)
        {
            if (existingForThisEvent.Status == RegisterTeamStatusEnum.Pending || existingForThisEvent.Status == RegisterTeamStatusEnum.Approved)
            {
                throw new ConflictException("TEAM_ALREADY_REGISTERED_FOR_EVENT");
            }
            // If it's Rejected, we allow them to re-register for this event
            existingForThisEvent.Status = RegisterTeamStatusEnum.Pending;
            existingForThisEvent.Description = request.Description;
            existingForThisEvent.UpdatedAt = now;

            // Check limit team of the event
            if (eventEntity.LimitTeam.HasValue)
            {
                var registeredTeamsCount = await _dbContext.RegisterTeams.CountAsync(x => x.EventId == request.EventId && !x.IsDisable && x.Status != RegisterTeamStatusEnum.Rejected);
                if (registeredTeamsCount >= eventEntity.LimitTeam.Value)
                {
                    throw new ConflictException("EVENT_REACHED_MAX_TEAMS_LIMIT");
                }
            }

            var txn = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                _dbContext.RegisterTeams.Update(existingForThisEvent);
                await _dbContext.SaveChangesAsync();
                await txn.CommitAsync();
            }
            catch
            {
                await txn.RollbackAsync();
                throw;
            }

            return new Response.RegisterEventResponse
            {
                RegisterId = existingForThisEvent.Id,
                TeamId = team.Id,
                TeamName = team.Name,
                EventId = eventEntity.Id,
                EventName = eventEntity.Name,
                Status = existingForThisEvent.Status.ToString()!,
                Message = "Đăng ký lại thành công, ban tổ chức đang xét duyệt bạn."
            };
        }

        // If the team has ever been Approved for ANY event, they cannot register for another event
        if (existingRegistrations.Any(x => x.Status == RegisterTeamStatusEnum.Approved))
        {
            throw new ForbiddenException("TEAM_ALREADY_APPROVED_FOR_AN_EVENT");
        }

        // Check limit team of the event
        if (eventEntity.LimitTeam.HasValue)
        {
            var registeredTeamsCount = await _dbContext.RegisterTeams.CountAsync(x => x.EventId == request.EventId && !x.IsDisable && x.Status != RegisterTeamStatusEnum.Rejected);
            if (registeredTeamsCount >= eventEntity.LimitTeam.Value)
            {
                throw new ConflictException("EVENT_REACHED_MAX_TEAMS_LIMIT");
            }
        }

        var registerTeam = new Repository.Entity.RegisterTeams
        {
            Id = Guid.NewGuid(),
            TeamId = team.Id,
            EventId = eventEntity.Id,
            Description = request.Description,
            Status = RegisterTeamStatusEnum.Pending,
            IsBanned = false,
            CreatedAt = now,
            UpdatedAt = now
        };

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _dbContext.RegisterTeams.AddAsync(registerTeam);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return new Response.RegisterEventResponse
        {
            RegisterId = registerTeam.Id,
            TeamId = team.Id,
            TeamName = team.Name,
            EventId = eventEntity.Id,
            EventName = eventEntity.Name,
            Status = registerTeam.Status.ToString()!,
            Message = "Đăng ký thành công, ban tổ chức đang xét duyệt bạn."
        };
    }

    public async Task<Models.BasePaginationResponse> GetMyRegisteredEvents(Request.GetMyRegisteredEventsRequest request, Models.PaginationRequest paginationRequest)
    {
        var userId = GetCurrentUserId();

        // Teams that user is an active member of
        var myTeamIds = await _dbContext.TeamDetails
            .Where(x => x.UserId == userId && !x.IsDisable && x.Status == TeamDetailStatusEnum.Active)
            .Select(x => x.TeamId)
            .ToListAsync();

        var query = _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team)
            .Include(x => x.Event)
            .Where(x => !x.IsDisable && myTeamIds.Contains(x.TeamId));

        var statusStr = string.IsNullOrWhiteSpace(request.Status) ? "" : request.Status.Trim();
        if (!string.IsNullOrWhiteSpace(statusStr))
        {
            if (!Enum.TryParse<RegisterTeamStatusEnum>(statusStr, true, out var statusEnum))
            {
                throw new BadRequestException("INVALID_STATUS");
            }
            query = query.Where(x => x.Status == statusEnum);
        }

        var totalCount = await query.CountAsync();

        if (string.IsNullOrWhiteSpace(request.Status))
        {
            query = query.OrderBy(x => x.Status == RegisterTeamStatusEnum.Pending ? 0 : (x.Status == RegisterTeamStatusEnum.Approved ? 1 : 2)).ThenByDescending(x => x.CreatedAt);
        }
        else
        {
            query = query.OrderByDescending(x => x.CreatedAt);
        }

        var items = await query
            .Skip((paginationRequest.PageIndex - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
            .Select(x => new Response.RegisteredEventItemResponse
            {
                RegisterId = x.Id,
                TeamId = x.TeamId,
                TeamName = x.Team.Name,
                EventId = x.EventId,
                EventName = x.Event.Name,
                Status = x.Status.ToString()!,
                Description = x.Description,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
    }

    public async Task<Response.RejectionReasonResponse> GetRejectionReason(Guid registerId)
    {
        var userId = GetCurrentUserId();

        var registerTeam = await _dbContext.RegisterTeams
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == registerId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        // Check if user is in the team
        var isMember = await _dbContext.TeamDetails.AnyAsync(x => x.TeamId == registerTeam.TeamId && x.UserId == userId && !x.IsDisable && x.Status == TeamDetailStatusEnum.Active);
        if (!isMember)
        {
            throw new ForbiddenException("USER_NOT_IN_TEAM");
        }

        if (registerTeam.Status == RegisterTeamStatusEnum.Pending)
        {
            return new Response.RejectionReasonResponse
            {
                RegisterId = registerTeam.Id,
                Status = registerTeam.Status.ToString()!,
                RejectionReason = "Đang đợi xét duyệt"
            };
        }

        if (registerTeam.Status == RegisterTeamStatusEnum.Approved)
        {
            return new Response.RejectionReasonResponse
            {
                RegisterId = registerTeam.Id,
                Status = registerTeam.Status.ToString()!,
                RejectionReason = "Đã được đồng ý"
            };
        }

        return new Response.RejectionReasonResponse
        {
            RegisterId = registerTeam.Id,
            Status = registerTeam.Status.ToString()!,
            RejectionReason = registerTeam.RejectionReason
        };
    }

    public async Task<Response.RegisterEventResponse> ApproveRegistration(Guid registerId)
    {
        // 1. Staff authentication
        var isStaff = _httpContext.HttpContext?.User.IsInRole(RoleEnum.Staff.ToString()) == true
                      || _httpContext.HttpContext?.User.IsInRole(RoleEnum.Admin.ToString()) == true;
        if (!isStaff)
        {
            throw new ForbiddenException("ONLY_STAFF_CAN_APPROVE");
        }

        var registerTeam = await _dbContext.RegisterTeams
            .Include(x => x.Team)
            .Include(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == registerId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        if (registerTeam.Status != RegisterTeamStatusEnum.Pending)
        {
            throw new BadRequestException("ONLY_PENDING_REGISTRATION_CAN_BE_APPROVED");
        }

        // Lock team editing (it should be locked because it is approved)
        // Set CanEdit = false
        var team = registerTeam.Team;
        team.CanEdit = false;
        team.UpdatedAt = DateTimeOffset.UtcNow;

        registerTeam.Status = RegisterTeamStatusEnum.Approved;
        registerTeam.UpdatedAt = DateTimeOffset.UtcNow;

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            _dbContext.Teams.Update(team);
            _dbContext.RegisterTeams.Update(registerTeam);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return new Response.RegisterEventResponse
        {
            RegisterId = registerTeam.Id,
            TeamId = team.Id,
            TeamName = team.Name,
            EventId = registerTeam.Event.Id,
            EventName = registerTeam.Event.Name,
            Status = registerTeam.Status.ToString()!,
            Message = "Approve thành công."
        };
    }

    public async Task<Response.RegisterEventResponse> RejectRegistration(Guid registerId, Request.RejectTeamRequest request)
    {
        // 1. Staff authentication
        var isStaff = _httpContext.HttpContext?.User.IsInRole(RoleEnum.Staff.ToString()) == true
                      || _httpContext.HttpContext?.User.IsInRole(RoleEnum.Admin.ToString()) == true;
        if (!isStaff)
        {
            throw new ForbiddenException("ONLY_STAFF_CAN_REJECT");
        }

        if (string.IsNullOrWhiteSpace(request.Reason))
        {
            throw new BadRequestException("REJECTION_REASON_REQUIRED");
        }

        var registerTeam = await _dbContext.RegisterTeams
            .Include(x => x.Team)
            .Include(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == registerId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        if (registerTeam.Status != RegisterTeamStatusEnum.Pending)
        {
            throw new BadRequestException("ONLY_PENDING_REGISTRATION_CAN_BE_REJECTED");
        }

        var team = registerTeam.Team;

        // If team has NO other Pending or Approved registrations, it means they are completely rejected from everything.
        // We can unlock the team ONLY IF they don't have another Pending/Approved registration.
        var hasOtherPendingOrApproved = await _dbContext.RegisterTeams.AnyAsync(x => x.TeamId == team.Id && x.Id != registerId && !x.IsDisable && (x.Status == RegisterTeamStatusEnum.Pending || x.Status == RegisterTeamStatusEnum.Approved));

        if (!hasOtherPendingOrApproved)
        {
            team.CanEdit = true; // Unlock if this was their only hope
            team.UpdatedAt = DateTimeOffset.UtcNow;
            _dbContext.Teams.Update(team);
        }

        registerTeam.Status = RegisterTeamStatusEnum.Rejected;
        registerTeam.RejectionReason = request.Reason.Trim();
        registerTeam.UpdatedAt = DateTimeOffset.UtcNow;

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            _dbContext.RegisterTeams.Update(registerTeam);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return new Response.RegisterEventResponse
        {
            RegisterId = registerTeam.Id,
            TeamId = team.Id,
            TeamName = team.Name,
            EventId = registerTeam.Event.Id,
            EventName = registerTeam.Event.Name,
            Status = registerTeam.Status.ToString()!,
            Message = "Reject thành công."
        };
    }
}
