using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
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

        if (user.IsVerified != true)
        {
            throw new ForbiddenException("USER_NOT_VERIFIED");
        }

        return user;
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
            // Note: Depending on rules, you might want to create a new registration record
            // or update the existing one. For simplicity, since the old one isn't IsDisable=true,
            // we will let the query continue and insert a new one OR we should update the old one.
            // I will update the old one to Pending.
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

        // Parse status (default to Approved if not provided or empty)
        var statusStr = string.IsNullOrWhiteSpace(request.Status) ? "Approved" : request.Status.Trim();
        if (!Enum.TryParse<RegisterTeamStatusEnum>(statusStr, true, out var statusEnum))
        {
            throw new BadRequestException("INVALID_STATUS");
        }

        // Teams that user is an active member of
        var myTeamIds = await _dbContext.TeamDetails
            .Where(x => x.UserId == userId && !x.IsDisable && x.Status == TeamDetailStatusEnum.Active)
            .Select(x => x.TeamId)
            .ToListAsync();

        var query = _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team)
            .Include(x => x.Event)
            .Where(x => !x.IsDisable && myTeamIds.Contains(x.TeamId) && x.Status == statusEnum);

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
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

        return new Response.RejectionReasonResponse
        {
            RegisterId = registerTeam.Id,
            Status = registerTeam.Status.ToString()!,
            RejectionReason = registerTeam.RejectionReason
        };
    }
}
