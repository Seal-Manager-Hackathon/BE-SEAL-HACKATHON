using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.RegisterTeams;

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

    private async Task EnsureStaffAssignedToEvent(Guid eventId)
    {
        var staffId = GetCurrentUserId();
        var isAssigned = await _dbContext.AssignEvents.AnyAsync(x => x.UserId == staffId
            && x.EventId == eventId
            && !x.IsDisable
            && !x.Event.IsDisable);

        if (!isAssigned)
        {
            throw new ForbiddenException("STAFF_NOT_ASSIGNED_TO_EVENT");
        }
    }

    private bool IsCurrentUserAdmin()
    {
        var role = _httpContext.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
        return Enum.TryParse<RoleEnum>(role, true, out var userRole) && userRole == RoleEnum.Admin;
    }

    public async Task<(Response.RegisterTeamActionResponse Data, string Message)> RegisterEvent(Request.RegisterEventRequest request)
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
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null || user.IsDisable == true)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        if (user.Role != RoleEnum.Student)
        {
            throw new ForbiddenException("CURRENT_USER_MUST_BE_STUDENT");
        }

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

        if (eventEntity.Status != EventStatusEnum.Published)
        {
            throw new BadRequestException("EVENT_NOT_OPEN_FOR_REGISTRATION");
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

            return (new Response.RegisterTeamActionResponse
            {
                Id = existingForThisEvent.Id,
                TeamId = team.Id,
                TeamName = team.Name,
                EventId = eventEntity.Id,
                EventName = eventEntity.Name,
                Status = existingForThisEvent.Status.Value,
            }, "REGISTERED_AGAIN_SUCCESSFULLY");
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

        return (new Response.RegisterTeamActionResponse
        {
            Id = registerTeam.Id,
            TeamId = team.Id,
            TeamName = team.Name,
            EventId = eventEntity.Id,
            EventName = eventEntity.Name,
            Status = registerTeam.Status.Value,
            IsBanned = registerTeam.IsBanned
        }, "REGISTERED_SUCCESSFULLY");
    }

    public async Task<BasePaginationResponse> GetMyRegisteredEvents(Request.GetMyRegisteredEventsRequest request, PaginationRequest paginationRequest)
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
                Status = x.Status,
                Description = x.Description,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
    }

    public async Task<Response.RegisterTeamDetailForStudentResponse> GetRegisterTeamDetailForStudent(Guid registerId)
    {
        var userId = GetCurrentUserId();

        var registerTeam = await _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team)
            .Include(x => x.Event)
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

        var rejectionReason = registerTeam.RejectionReason;
        if (registerTeam.Status == RegisterTeamStatusEnum.Pending)
        {
            rejectionReason = "Đang đợi xét duyệt";
        }
        else if (registerTeam.Status == RegisterTeamStatusEnum.Approved)
        {
            rejectionReason = "Đã được đồng ý";
        }

        return new Response.RegisterTeamDetailForStudentResponse
        {
            RegisterId = registerTeam.Id,
            TeamId = registerTeam.TeamId,
            TeamName = registerTeam.Team.Name,
            EventId = registerTeam.EventId,
            EventName = registerTeam.Event.Name,
            Status = registerTeam.Status,
            Description = registerTeam.Description,
            RejectionReason = rejectionReason,
            CreatedAt = registerTeam.CreatedAt
        };
    }

    public async Task<Response.RegisterTeamRejectionReasonResponse> GetRejectionReason(Guid registerId)
    {
        var userId = GetCurrentUserId();

        var registerTeam = await _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team)
            .Include(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == registerId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        if (registerTeam.Event.IsDisable)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (registerTeam.Team.IsDisable)
        {
            throw new NotFoundException("TEAM_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            var userRoleClaim = _httpContext.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
            Enum.TryParse<RoleEnum>(userRoleClaim, true, out var userRole);
            var isStaff = userRole == RoleEnum.Staff;

            if (isStaff)
            {
                await EnsureStaffAssignedToEvent(registerTeam.EventId);
            }
            else
            {
                var isMember = await _dbContext.TeamDetails.AnyAsync(x => x.TeamId == registerTeam.TeamId
                    && x.UserId == userId
                    && !x.IsDisable
                    && x.Status == TeamDetailStatusEnum.Active);

                if (!isMember)
                {
                    throw new ForbiddenException("USER_NOT_IN_TEAM");
                }
            }
        }

        return new Response.RegisterTeamRejectionReasonResponse
        {
            RegisterId = registerTeam.Id,
            TeamId = registerTeam.TeamId,
            EventId = registerTeam.EventId,
            Status = registerTeam.Status ?? RegisterTeamStatusEnum.Pending,
            RejectionReason = registerTeam.RejectionReason,
        };
    }

    public async Task<BasePaginationResponse> GetRegisterTeamsByEvent(Guid eventId, string? keyword, RegisterTeamStatusEnum? status, bool? isDisable, PaginationRequest paginationRequest)
    {
        var eventExists = await _dbContext.Events.AsNoTracking().AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(eventId);
        }

        var query = _dbContext.RegisterTeams
            .AsNoTracking()
            .Where(x => x.EventId == eventId
                        && x.IsDisable == (isDisable ?? false)
                        && !x.Team.IsDisable);

        if (status.HasValue)
        {
            query = query.Where(x => x.Status == status.Value);
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var normalizedKeyword = keyword.Trim().ToLower();
            query = query.Where(x => x.Team.Name.ToLower().Contains(normalizedKeyword));
        }



        var totalCount = await query.CountAsync();

        if (!status.HasValue)
        {
            query = query.OrderBy(x => x.Status == RegisterTeamStatusEnum.Pending ? 0 : (x.Status == RegisterTeamStatusEnum.Approved ? 1 : 2)).ThenBy(x => x.Team.Name).ThenBy(x => x.CreatedAt);
        }
        else
        {
            query = query.OrderBy(x => x.Team.Name).ThenBy(x => x.CreatedAt);
        }

        var items = await query


            .Skip((paginationRequest.PageIndex - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
            .Select(x => new Response.RegisterTeamResponse
            {
                Id = x.Id,
                TeamId = x.TeamId,
                TeamName = x.Team.Name,
                EventId = x.EventId,
                TrackId = x.TrackId,
                TrackTitle = x.Track != null ? x.Track.Title : null,
                TopicId = x.TopicId,
                TopicTitle = x.Topic != null ? x.Topic.Title : null,
                Description = x.Description,
                RejectionReason = x.RejectionReason,
                Status = x.Status,
                IsBanned = x.IsBanned,
                IsDisable = x.IsDisable,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
    }

    public async Task<Response.RegisterTeamDetailResponse> GetRegisterTeamDetail(Guid registerTeamId)
    {
        var registerTeam = await _dbContext.RegisterTeams
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == registerTeamId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(registerTeam.EventId);
        }

        var eventId = registerTeam.EventId;
        var teamRegisterId = registerTeam.Id;

        // Check active rounds to compute IsEliminated.
        var activeRounds = await _dbContext.Rounds.AsNoTracking()
            .Where(x => x.EventId == eventId && !x.IsDisable)
            .Select(x => new { x.Id, x.RoundNo })
            .ToListAsync();

        var latestActiveRound = activeRounds
            .OrderByDescending(x => x.RoundNo ?? 0)
            .Select(x => x.Id)
            .FirstOrDefault();

        bool isEliminated;
        Guid? currentRoundId = null;
        string? currentRoundName = null;
        int? currentRoundNo = null;

        if (activeRounds.Count == 0)
        {
            isEliminated = false;
        }
        else
        {
            var hasActiveRoundDetail = await _dbContext.RoundDetails
                .AsNoTracking()
                .AnyAsync(x => x.RegisterTeamId == teamRegisterId
                               && !x.IsDisable
                               && !x.Round.IsDisable
                               && x.Round.EventId == eventId);

            if (!hasActiveRoundDetail)
            {
                isEliminated = true;
            }
            else
            {
                isEliminated = false;

                var currentRoundData = await _dbContext.RoundDetails
                    .AsNoTracking()
                    .Where(x => x.RegisterTeamId == teamRegisterId
                                && !x.IsDisable
                                && !x.Round.IsDisable
                                && x.Round.EventId == eventId)
                    .Select(x => new { x.Round.Id, x.Round.Name, x.Round.RoundNo })
                    .OrderByDescending(x => x.RoundNo ?? 0)
                    .FirstOrDefaultAsync();

                if (currentRoundData != null)
                {
                    currentRoundId = currentRoundData.Id;
                    currentRoundName = currentRoundData.Name;
                    currentRoundNo = currentRoundData.RoundNo;
                }
            }
        }

        return await _dbContext.RegisterTeams
            .AsNoTracking()
            .Where(x => x.Id == teamRegisterId)
            .Select(x => new Response.RegisterTeamDetailResponse
            {
                Id = x.Id,
                TeamId = x.TeamId,
                TeamName = x.Team.Name,
                EventId = x.EventId,
                EventName = x.Event.Name,
                TrackId = x.TrackId,
                TrackTitle = x.Track != null ? x.Track.Title : null,
                TopicId = x.TopicId,
                TopicTitle = x.Topic != null ? x.Topic.Title : null,
                Description = x.Description,
                RejectionReason = x.RejectionReason,
                Status = x.Status ?? RegisterTeamStatusEnum.Pending,
                IsBanned = x.IsBanned,
                IsDisable = x.IsDisable,
                IsEliminated = isEliminated,
                CurrentRoundId = currentRoundId,
                CurrentRoundName = currentRoundName,
                CurrentRoundNo = currentRoundNo,
                Members = x.Team.TeamDetails
                    .Where(td => !td.IsDisable && td.Status == TeamDetailStatusEnum.Active)
                    .OrderByDescending(td => td.IsLeader)
                    .ThenBy(td => td.User.FirstName)
                    .ThenBy(td => td.User.LastName)
                    .Select(td => new Response.RegisterTeamMemberResponse
                    {
                        UserId = td.UserId,
                        FullName = (td.User.FirstName + " " + td.User.LastName).Trim(),
                        Email = td.User.Email,
                        StudentId = td.User.StudentId,
                        IsLeader = td.IsLeader,
                    })
                    .ToList(),
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
            })
            .FirstAsync();
    }

    public async Task<Response.RegisterTeamActionResponse> AcceptRegisterTeam(Guid registerTeamId)
    {
        var registerTeam = await _dbContext.RegisterTeams
            .Include(x => x.Team)
            .Include(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == registerTeamId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        if (registerTeam.Event.IsDisable)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (registerTeam.Team.IsDisable)
        {
            throw new NotFoundException("TEAM_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(registerTeam.EventId);
        }

        if (registerTeam.Status == RegisterTeamStatusEnum.Approved)
        {
            throw new ConflictException("REGISTER_TEAM_ALREADY_APPROVED");
        }

        if (registerTeam.Status == RegisterTeamStatusEnum.Rejected)
        {
            throw new ConflictException("REGISTER_TEAM_ALREADY_REJECTED");
        }

        if (registerTeam.IsBanned)
        {
            throw new ConflictException("TEAM_IS_BANNED_FROM_EVENT");
        }

        var now = DateTimeOffset.UtcNow;
        registerTeam.Status = RegisterTeamStatusEnum.Approved;
        registerTeam.RejectionReason = null;
        registerTeam.UpdatedAt = now;
        registerTeam.Team.CanEdit = false;
        registerTeam.Team.UpdatedAt = now;

        _dbContext.RegisterTeams.Update(registerTeam);
        _dbContext.Teams.Update(registerTeam.Team);

        var round1 = await _dbContext.Rounds
            .FirstOrDefaultAsync(r => r.EventId == registerTeam.EventId && r.RoundNo == 1 && !r.IsDisable);

        if (round1 != null)
        {
            _dbContext.RoundDetails.Add(new Hackathon.Repository.Entity.RoundDetails
            {
                RoundId = round1.Id,
                RegisterTeamId = registerTeam.Id
            });
        }

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return new Response.RegisterTeamActionResponse
        {
            Id = registerTeam.Id,
            TeamId = registerTeam.TeamId,
            TeamName = registerTeam.Team.Name,
            EventId = registerTeam.EventId,
            EventName = registerTeam.Event.Name,
            Status = registerTeam.Status.Value,
            IsBanned = registerTeam.IsBanned,
            RejectionReason = registerTeam.RejectionReason,
        };
    }

    public async Task<Response.RegisterTeamActionResponse> RejectRegisterTeam(Guid registerTeamId, Request.RejectRegisterTeamRequest request)
    {
        var reason = request.Reason?.Trim();
        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new BadRequestException("REASON_REQUIRED");
        }

        var registerTeam = await _dbContext.RegisterTeams
            .Include(x => x.Team)
            .Include(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == registerTeamId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        if (registerTeam.Event.IsDisable)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (registerTeam.Team.IsDisable)
        {
            throw new NotFoundException("TEAM_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(registerTeam.EventId);
        }

        if (registerTeam.Status == RegisterTeamStatusEnum.Approved)
        {
            throw new ConflictException("REGISTER_TEAM_ALREADY_APPROVED");
        }

        if (registerTeam.Status == RegisterTeamStatusEnum.Rejected)
        {
            throw new ConflictException("REGISTER_TEAM_ALREADY_REJECTED");
        }

        var now = DateTimeOffset.UtcNow;
        registerTeam.Status = RegisterTeamStatusEnum.Rejected;
        registerTeam.RejectionReason = reason;
        registerTeam.UpdatedAt = now;
        registerTeam.Team.CanEdit = true;
        registerTeam.Team.UpdatedAt = now;

        _dbContext.RegisterTeams.Update(registerTeam);
        _dbContext.Teams.Update(registerTeam.Team);

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return new Response.RegisterTeamActionResponse
        {
            Id = registerTeam.Id,
            TeamId = registerTeam.TeamId,
            TeamName = registerTeam.Team.Name,
            EventId = registerTeam.EventId,
            EventName = registerTeam.Event.Name,
            Status = registerTeam.Status.Value,
            RejectionReason = registerTeam.RejectionReason,
            IsBanned = registerTeam.IsBanned
        };
    }

    public async Task<Response.RegisterTeamActionResponse> BanRegisterTeam(Guid registerTeamId, Request.BanTeamRequest request)
    {
        var registerTeam = await _dbContext.RegisterTeams
            .Include(x => x.Event)
            .Include(x => x.Team)
            .FirstOrDefaultAsync(x => x.Id == registerTeamId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(registerTeam.EventId);
        }

        if (registerTeam.IsBanned)
        {
            throw new ConflictException("TEAM_IS_ALREADY_BANNED");
        }

        var now = DateTimeOffset.UtcNow;
        registerTeam.IsBanned = true;
        registerTeam.Status = RegisterTeamStatusEnum.Rejected;
        registerTeam.RejectionReason = request.Reason;
        registerTeam.UpdatedAt = now;

        // Unlock team
        registerTeam.Team.CanEdit = true;
        registerTeam.Team.UpdatedAt = now;

        _dbContext.RegisterTeams.Update(registerTeam);
        await _dbContext.SaveChangesAsync();

        return new Response.RegisterTeamActionResponse
        {
            Id = registerTeam.Id,
            TeamId = registerTeam.TeamId,
            TeamName = registerTeam.Team.Name,
            EventId = registerTeam.EventId,
            EventName = registerTeam.Event.Name,
            Status = registerTeam.Status.Value,
            RejectionReason = registerTeam.RejectionReason,
            IsBanned = registerTeam.IsBanned
        };
    }

    public async Task<Response.RegisterTeamActionResponse> UnbanRegisterTeam(Guid registerTeamId)
    {
        var registerTeam = await _dbContext.RegisterTeams
            .Include(x => x.Event)
            .Include(x => x.Team)
            .FirstOrDefaultAsync(x => x.Id == registerTeamId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(registerTeam.EventId);
        }

        if (!registerTeam.IsBanned)
        {
            throw new ConflictException("TEAM_IS_NOT_BANNED");
        }

        var now = DateTimeOffset.UtcNow;
        registerTeam.IsBanned = false;
        registerTeam.UpdatedAt = now;

        _dbContext.RegisterTeams.Update(registerTeam);
        await _dbContext.SaveChangesAsync();

        return new Response.RegisterTeamActionResponse
        {
            Id = registerTeam.Id,
            TeamId = registerTeam.TeamId,
            TeamName = registerTeam.Team.Name,
            EventId = registerTeam.EventId,
            EventName = registerTeam.Event.Name,
            Status = registerTeam.Status.Value,
            RejectionReason = registerTeam.RejectionReason,
            IsBanned = registerTeam.IsBanned
        };
    }

    public async Task<(List<Response.RegisterTeamByRoundResponse> Data, string Message)> GetTeamsByRound(Guid eventId, Request.GetTeamsByRoundRequest request)
    {
        if (eventId == Guid.Empty)
        {
            throw new BadRequestException("EVENT_ID_REQUIRED");
        }

        var eventExists = await _dbContext.Events.AsNoTracking().AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        // If roundId provided, validate the round belongs to the event
        if (request.RoundId.HasValue)
        {
            var roundExists = await _dbContext.Rounds.AsNoTracking()
                .AnyAsync(x => x.Id == request.RoundId.Value && !x.IsDisable && x.EventId == eventId);
            if (!roundExists)
            {
                throw new NotFoundException("ROUND_NOT_FOUND");
            }
        }

        // If trackId provided, validate the track belongs to the event
        if (request.TrackId.HasValue)
        {
            var trackExists = await _dbContext.Tracks.AsNoTracking()
                .AnyAsync(x => x.Id == request.TrackId.Value && !x.IsDisable && x.EventId == eventId);
            if (!trackExists)
            {
                throw new NotFoundException("TRACK_NOT_FOUND");
            }
        }

        // Build query from RegisterTeams
        var registerTeamsQuery = _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team)
            .Include(x => x.Track)
            .Include(x => x.Topic)
            .Where(x => x.EventId == eventId
                        && !x.IsDisable
                        && !x.Team.IsDisable);

        // Filter by trackId if provided
        if (request.TrackId.HasValue)
        {
            registerTeamsQuery = registerTeamsQuery.Where(x => x.TrackId == request.TrackId.Value);
        }

        // If roundId provided, only include teams that have RoundDetails for that round
        if (request.RoundId.HasValue)
        {
            var teamIdsInRound = _dbContext.RoundDetails
                .AsNoTracking()
                .Where(rd => rd.RoundId == request.RoundId.Value
                             && !rd.IsDisable)
                .Select(rd => rd.RegisterTeamId);

            registerTeamsQuery = registerTeamsQuery.Where(x => teamIdsInRound.Contains(x.Id));
        }

        var teams = await registerTeamsQuery
            .Select(x => new Response.RegisterTeamByRoundResponse
            {
                RegisterTeamId = x.Id,
                TeamId = x.TeamId,
                TeamName = x.Team.Name,
                TrackId = x.TrackId,
                TrackTitle = x.Track != null ? x.Track.Title : null,
                TopicId = x.TopicId,
                TopicTitle = x.Topic != null ? x.Topic.Title : null,
                Status = x.Status ?? RegisterTeamStatusEnum.Pending,
                IsBanned = x.IsBanned,
                CreatedAt = x.CreatedAt,
            })
            .OrderBy(x => x.TeamName)
            .ToListAsync();

        return (teams, teams.Count == 0 ? "NO_TEAMS_FOUND" : "SUCCESS");
    }

    public async Task<(List<Response.RegisterTeamTrackResponse> Data, string Message)> GetTeamsByTrack(Guid eventId, Guid trackId, Request.GetTeamsByTrackRequest request)
    {
        if (eventId == Guid.Empty)
        {
            throw new BadRequestException("EVENT_ID_REQUIRED");
        }

        if (trackId == Guid.Empty)
        {
            throw new BadRequestException("TRACK_ID_REQUIRED");
        }

        var eventEntity = await _dbContext.Events.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == eventId && !x.IsDisable);
        if (eventEntity == null)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var track = await _dbContext.Tracks.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == trackId && !x.IsDisable);
        if (track == null || track.EventId != eventId)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(eventId);
        }

        // Pull active rounds for the event once.
        var activeRounds = await _dbContext.Rounds.AsNoTracking()
            .Where(x => x.EventId == eventId && !x.IsDisable)
            .Select(x => new { x.Id, x.Name, x.RoundNo })
            .ToListAsync();

        // Pull all active register teams for this track in memory (bounded by LimitTeam).
        var registerTeamsQuery = _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team)
            .Include(x => x.Topic)
            .Where(x => x.EventId == eventId
                        && x.TrackId == trackId
                        && !x.IsDisable
                        && !x.Team.IsDisable);

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var normalizedKeyword = request.Keyword.Trim().ToLower();
            registerTeamsQuery = registerTeamsQuery.Where(x => x.Team.Name.ToLower().Contains(normalizedKeyword));
        }

        var registerTeams = await registerTeamsQuery.ToListAsync();

        // For each register team, look up their active RoundDetails once.
        var registerTeamIds = registerTeams.Select(x => x.Id).ToList();
        var activeRoundDetails = await _dbContext.RoundDetails.AsNoTracking()
            .Where(x => registerTeamIds.Contains(x.RegisterTeamId)
                        && !x.IsDisable
                        && !x.Round.IsDisable
                        && x.Round.EventId == eventId)
            .Select(x => new { x.RegisterTeamId, x.RoundId })
            .ToListAsync();

        var result = registerTeams.Select(rt =>
        {
            var teamRoundIds = activeRoundDetails
                .Where(rd => rd.RegisterTeamId == rt.Id)
                .Select(rd => rd.RoundId)
                .ToList();

            bool isTeamEliminated;
            Guid? currentRoundId = null;
            string? currentRoundName = null;
            int? currentRoundNo = null;

            if (activeRounds.Count == 0)
            {
                // Event has not started its rounds yet.
                isTeamEliminated = false;
            }
            else
            {
                var currentRound = activeRounds
                    .Where(r => teamRoundIds.Contains(r.Id))
                    .OrderByDescending(r => r.RoundNo ?? 0)
                    .FirstOrDefault();

                if (currentRound != null)
                {
                    isTeamEliminated = false;
                    currentRoundId = currentRound.Id;
                    currentRoundName = currentRound.Name;
                    currentRoundNo = currentRound.RoundNo;
                }
                else
                {
                    isTeamEliminated = true;
                }
            }

            return new
            {
                RegisterTeam = rt,
                IsEliminated = isTeamEliminated,
                CurrentRoundId = currentRoundId,
                CurrentRoundName = currentRoundName,
                CurrentRoundNo = currentRoundNo,
            };
        });

        // Apply filters.
        if (request.Status.HasValue)
        {
            result = result.Where(x => x.RegisterTeam.Status == request.Status.Value);
        }

        if (request.IsEliminated.HasValue)
        {
            result = result.Where(x => x.IsEliminated == request.IsEliminated.Value);
        }

        // Sort: not-eliminated first, then by TeamName asc, then CreatedAt desc.
        var teams = result
            .OrderBy(x => x.IsEliminated)
            .ThenBy(x => x.RegisterTeam.Team.Name)
            .ThenByDescending(x => x.RegisterTeam.CreatedAt)
            .Select(x => new Response.RegisterTeamTrackResponse
            {
                RegisterTeamId = x.RegisterTeam.Id,
                TeamId = x.RegisterTeam.TeamId,
                TeamName = x.RegisterTeam.Team.Name,
                Status = x.RegisterTeam.Status ?? RegisterTeamStatusEnum.Pending,
                TopicId = x.RegisterTeam.TopicId,
                TopicTitle = x.RegisterTeam.Topic != null ? x.RegisterTeam.Topic.Title : null,
                CurrentRoundId = x.CurrentRoundId,
                CurrentRoundName = x.CurrentRoundName,
                CurrentRoundNo = x.CurrentRoundNo,
                IsEliminated = x.IsEliminated,
            })
            .ToList();

        return (teams, teams.Count == 0 ? "NO_TEAMS_FOUND" : "SUCCESS");
    }

    public async Task<(List<Response.RegisterTeamApprovedResponse> Data, string Message)> GetApprovedTeams(Guid eventId, Request.GetApprovedTeamsRequest request)
    {
        if (eventId == Guid.Empty)
        {
            throw new BadRequestException("EVENT_ID_REQUIRED");
        }

        var eventEntity = await _dbContext.Events.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == eventId && !x.IsDisable);
        if (eventEntity == null)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(eventId);
        }

        var activeRounds = await _dbContext.Rounds.AsNoTracking()
            .Where(x => x.EventId == eventId && !x.IsDisable)
            .Select(x => new { x.Id, x.Name, x.RoundNo })
            .ToListAsync();

        var registerTeamsQuery = _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team)
            .Include(x => x.Track)
            .Include(x => x.Topic)
            .Where(x => x.EventId == eventId
                        && x.Status == RegisterTeamStatusEnum.Approved
                        && !x.IsDisable
                        && !x.Team.IsDisable);

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var normalizedKeyword = request.Keyword.Trim().ToLower();
            registerTeamsQuery = registerTeamsQuery.Where(x => x.Team.Name.ToLower().Contains(normalizedKeyword));
        }

        var registerTeams = await registerTeamsQuery.ToListAsync();

        var registerTeamIds = registerTeams.Select(x => x.Id).ToList();
        var activeRoundDetails = await _dbContext.RoundDetails.AsNoTracking()
            .Where(x => registerTeamIds.Contains(x.RegisterTeamId)
                        && !x.IsDisable
                        && !x.Round.IsDisable
                        && x.Round.EventId == eventId)
            .Select(x => new { x.RegisterTeamId, x.RoundId })
            .ToListAsync();

        var result = registerTeams.Select(rt =>
        {
            var teamRoundIds = activeRoundDetails
                .Where(rd => rd.RegisterTeamId == rt.Id)
                .Select(rd => rd.RoundId)
                .ToList();

            bool isTeamEliminated;
            Guid? currentRoundId = null;
            string? currentRoundName = null;
            int? currentRoundNo = null;

            if (activeRounds.Count == 0)
            {
                isTeamEliminated = false;
            }
            else
            {
                var currentRound = activeRounds
                    .Where(r => teamRoundIds.Contains(r.Id))
                    .OrderByDescending(r => r.RoundNo ?? 0)
                    .FirstOrDefault();

                if (currentRound != null)
                {
                    isTeamEliminated = false;
                    currentRoundId = currentRound.Id;
                    currentRoundName = currentRound.Name;
                    currentRoundNo = currentRound.RoundNo;
                }
                else
                {
                    isTeamEliminated = true;
                }
            }

            return new
            {
                RegisterTeam = rt,
                IsEliminated = isTeamEliminated,
                CurrentRoundId = currentRoundId,
                CurrentRoundName = currentRoundName,
                CurrentRoundNo = currentRoundNo,
            };
        });

        if (request.IsEliminated.HasValue)
        {
            result = result.Where(x => x.IsEliminated == request.IsEliminated.Value);
        }

        var teams = result
            .OrderBy(x => x.IsEliminated)
            .ThenBy(x => x.RegisterTeam.Team.Name)
            .ThenByDescending(x => x.RegisterTeam.CreatedAt)
            .Select(x => new Response.RegisterTeamApprovedResponse
            {
                RegisterTeamId = x.RegisterTeam.Id,
                TeamId = x.RegisterTeam.TeamId,
                TeamName = x.RegisterTeam.Team.Name,
                TrackId = x.RegisterTeam.TrackId,
                TrackTitle = x.RegisterTeam.Track != null ? x.RegisterTeam.Track.Title : null,
                TopicId = x.RegisterTeam.TopicId,
                TopicTitle = x.RegisterTeam.Topic != null ? x.RegisterTeam.Topic.Title : null,
                CurrentRoundId = x.CurrentRoundId,
                CurrentRoundName = x.CurrentRoundName,
                CurrentRoundNo = x.CurrentRoundNo,
                IsEliminated = x.IsEliminated,
            })
            .ToList();

        return (teams, teams.Count == 0 ? "NO_TEAMS_FOUND" : "SUCCESS");
    }

    public async Task<Response.TeamRoundSubmissionResponse> GetTeamRoundSubmissions(Guid registerTeamId, Guid? roundId)
    {
        // Validate register team exists
        var registerTeam = await _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team)
            .Include(x => x.Track)
            .FirstOrDefaultAsync(x => x.Id == registerTeamId && !x.IsDisable && !x.Team.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        // If roundId provided, validate it belongs to the same event
        if (roundId.HasValue)
        {
            var roundExists = await _dbContext.Rounds.AsNoTracking()
                .AnyAsync(x => x.Id == roundId.Value && !x.IsDisable && x.EventId == registerTeam.EventId);
            if (!roundExists)
            {
                throw new NotFoundException("ROUND_NOT_FOUND");
            }
        }

        // Build RoundDetails query
        var roundDetailsQuery = _dbContext.RoundDetails
            .AsNoTracking()
            .Include(x => x.Round)
            .Where(x => x.RegisterTeamId == registerTeamId
                        && !x.IsDisable
                        && !x.Round.IsDisable);

        if (roundId.HasValue)
        {
            roundDetailsQuery = roundDetailsQuery.Where(x => x.RoundId == roundId.Value);
        }

        var roundDetails = await roundDetailsQuery.ToListAsync();

        if (roundDetails.Count == 0)
        {
            throw new NotFoundException("ROUND_DETAIL_NOT_FOUND");
        }

        // Get all submissions for all matching round details
        var roundDetailIds = roundDetails.Select(x => x.Id).ToList();
        var roundLookup = roundDetails.ToDictionary(x => x.Id, x => x.Round);

        var submissions = await _dbContext.Submissions
            .AsNoTracking()
            .Include(x => x.Scores).ThenInclude(x => x.ScoreItems).ThenInclude(x => x.CriteriaItem)
            .Where(x => roundDetailIds.Contains(x.RoundDetailId) && !x.IsDisable)
            .OrderBy(x => x.SubmittedAt)
            .ToListAsync();

        // Group submissions by RoundDetail to compute isLatest per round
        var submissionsByRoundDetail = submissions.GroupBy(x => x.RoundDetailId);

        var submissionDtos = submissionsByRoundDetail.SelectMany(group =>
        {
            var roundDetail = roundLookup[group.Key];
            var groupList = group.OrderBy(x => x.SubmittedAt).ToList();
            var latestSubmittedAt = groupList
                .Where(x => x.SubmittedAt.HasValue)
                .OrderByDescending(x => x.SubmittedAt)
                .Select(x => x.SubmittedAt)
                .FirstOrDefault();

            return groupList.Select(sub =>
            {
                var isLatest = sub.SubmittedAt.HasValue && sub.SubmittedAt == latestSubmittedAt;
                var activeScores = sub.Scores.Where(s => !s.IsDisable && s.TotalScore.HasValue).ToList();
                var gradingStatus = activeScores.Count == 0 ? "NotGraded" : "Graded";

                Response.SubmissionScoreDto? scoreDto = null;
                if (activeScores.Count > 0)
                {
                    var latestScore = activeScores.OrderByDescending(s => s.CreatedAt).First();
                    scoreDto = new Response.SubmissionScoreDto
                    {
                        ScoreId = latestScore.Id,
                        TotalScore = latestScore.TotalScore,
                        IsRetake = latestScore.IsRetake,
                        IsMock = latestScore.IsMock,
                        ScoreItems = latestScore.ScoreItems
                            .Where(si => !si.IsDisable)
                            .Select(si => new Response.ScoreItemDto
                            {
                                ScoreItemId = si.Id,
                                CriteriaItemId = si.CriteriaItemId,
                                CriteriaItemName = si.CriteriaItem.Name,
                                Score = si.Score,
                                MaxScore = si.CriteriaItem.Score,
                                Comment = si.Comment,
                            })
                            .ToList(),
                    };
                }

                return new Response.SubmissionDetailDto
                {
                    SubmissionId = sub.Id,
                    RoundId = roundDetail.Id,
                    RoundNo = roundDetail.RoundNo,
                    Url = sub.Url,
                    Description = sub.Description,
                    Status = sub.Status,
                    SubmittedAt = sub.SubmittedAt,
                    IsLatest = isLatest,
                    GradingStatus = gradingStatus,
                    Score = scoreDto,
                };
            });
        }).ToList();

        return new Response.TeamRoundSubmissionResponse
        {
            RegisterTeamId = registerTeamId,
            TeamId = registerTeam.TeamId,
            TeamName = registerTeam.Team.Name,
            TrackId = registerTeam.TrackId,
            TrackTitle = registerTeam.Track != null ? registerTeam.Track.Title : null,
            Submissions = submissionDtos,
        };
    }
}