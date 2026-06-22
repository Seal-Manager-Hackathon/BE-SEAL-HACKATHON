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
        return _httpContext.HttpContext?.User.IsInRole(RoleEnum.Admin.ToString()) == true;
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
                Status = x.Status.ToString()!,
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
            Status = registerTeam.Status.ToString()!,
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
            var isStaff = _httpContext.HttpContext?.User.IsInRole(RoleEnum.Staff.ToString()) == true;
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

        return await _dbContext.RegisterTeams
            .AsNoTracking()
            .Where(x => x.Id == registerTeamId)
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
        };
    }
}
