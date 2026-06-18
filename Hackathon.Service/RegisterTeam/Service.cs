using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.RegisterTeam;

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
        var items = await query
            .OrderBy(x => x.Team.Name)
            .ThenBy(x => x.CreatedAt)
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

        await EnsureStaffAssignedToEvent(registerTeam.EventId);

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
            Message = "REGISTER_TEAM_ACCEPTED_SUCCESSFULLY",
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

        await EnsureStaffAssignedToEvent(registerTeam.EventId);

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
            Message = "REGISTER_TEAM_REJECTED_SUCCESSFULLY",
        };
    }
}
