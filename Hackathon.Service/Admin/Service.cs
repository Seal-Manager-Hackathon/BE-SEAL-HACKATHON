using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Admin;

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

    public async Task<BasePaginationResponse> GetAllUsers(RoleEnum? role, PaginationRequest paginationRequest)
    {
        var q = _dbContext.Users
            .AsNoTracking()
            .Where(x => !x.IsDisable);

        if (role.HasValue)
        {
            q = q.Where(x => x.Role == role.Value);
        }

        var totalCount = await q.CountAsync();

        paginationRequest.PageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        paginationRequest.PageSize = paginationRequest.PageSize <= 0 ? 10 : System.Math.Min(paginationRequest.PageSize, 100);

        var items = await BuildUserQuery(q, paginationRequest);
        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
    }

    public async Task<BasePaginationResponse> SearchUsers(GetUsersQuery query)
    {
        var q = _dbContext.Users.AsNoTracking();

        if (query.IsDisable.HasValue)
        {
            q = q.Where(x => x.IsDisable == query.IsDisable.Value);
        }

        if (query.IsVerified.HasValue)
        {
            q = q.Where(x => x.IsVerified == query.IsVerified.Value);
        }

        // KeySearch — search across email, userId, studentId, firstName, lastName
        if (!string.IsNullOrWhiteSpace(query.KeySearch))
        {
            var normalized = query.KeySearch.Trim().ToLower();
            q = q.Where(x => x.Email.ToLower().Contains(normalized)
                || x.StudentId.ToLower().Contains(normalized)
                || x.FirstName.ToLower().Contains(normalized)
                || x.LastName.ToLower().Contains(normalized));
        }

        if (!string.IsNullOrWhiteSpace(query.MailSearch))
        {
            var normalized = query.MailSearch.Trim().ToLower();
            q = q.Where(x => x.Email.ToLower().Contains(normalized));
        }

        if (query.IdSearch.HasValue)
        {
            q = q.Where(x => x.Id == query.IdSearch.Value);
        }

        if (query.Role.HasValue)
        {
            q = q.Where(x => x.Role == query.Role.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.StudentIdSearch))
        {
            var normalizedStudentId = query.StudentIdSearch.Trim().ToLower();
            q = q.Where(x => x.StudentId.ToLower().Contains(normalizedStudentId));
        }

        var totalCount = await q.CountAsync();

        query.Pagination.PageIndex = query.Pagination.PageIndex <= 0 ? 1 : query.Pagination.PageIndex;
        query.Pagination.PageSize = query.Pagination.PageSize <= 0 ? 10 : System.Math.Min(query.Pagination.PageSize, 100);

        var items = await BuildUserQuery(q, query.Pagination);
        return ApiResponseFactory.BasePagination(items, query.Pagination.PageIndex, query.Pagination.PageSize, totalCount);
    }

    public async Task<BasePaginationResponse> GetRounds(Guid eventId, GetAdminRoundsRequest request)
    {
        var eventExists = await _dbContext.Events
            .AsNoTracking()
            .AnyAsync(x => x.Id == eventId);

        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        request.PageIndex = request.PageIndex <= 0 ? 1 : request.PageIndex;
        request.PageSize = request.PageSize <= 0 ? 10 : System.Math.Min(request.PageSize, 100);

        var q = _dbContext.Rounds
            .AsNoTracking()
            .Where(x => x.EventId == eventId);

        if (request.IsDisable.HasValue)
        {
            q = q.Where(x => x.IsDisable == request.IsDisable.Value);
        }

        var totalCount = await q.CountAsync();
        var items = await BuildRoundQuery(q, request);
        return ApiResponseFactory.BasePagination(items, request.PageIndex, request.PageSize, totalCount);
    }

    public async Task<CreateRoundResponse> CreateRound(Guid eventId, CreateRoundRequest request)
    {
        var eventExists = await _dbContext.Events
            .AsNoTracking()
            .AnyAsync(x => x.Id == eventId && !x.IsDisable);

        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new BadRequestException("ROUND_NAME_REQUIRED");
        }

        await ValidateRoundNo(eventId, request.RoundNo, null);
        ValidateRoundTimes(request.StartTime, request.EndTime, request.StartSubmission, request.EndSubmission);
        ValidateLimitTeam(request.LimitTeam);

        var now = DateTimeOffset.UtcNow;
        var round = new Hackathon.Repository.Entity.Rounds
        {
            Id = Guid.NewGuid(),
            EventId = eventId,
            Name = request.Name.Trim(),
            Description = request.Description?.Trim(),
            RoundNo = request.RoundNo,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            StartSubmission = request.StartSubmission,
            EndSubmission = request.EndSubmission,
            LimitTeam = request.LimitTeam,
            IsDisable = false,
            CreatedAt = now,
            UpdatedAt = now,
        };

        await _dbContext.Rounds.AddAsync(round);
        await _dbContext.SaveChangesAsync();

        return new CreateRoundResponse
        {
            RoundId = round.Id
        };
    }

    public async Task UpdateRound(Guid roundId, CreateRoundRequest request)
    {
        var round = await _dbContext.Rounds.FirstOrDefaultAsync(x => x.Id == roundId && !x.IsDisable);
        if (round == null)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        var nextRoundNo = request.RoundNo ?? round.RoundNo;
        var nextStartTime = request.StartTime ?? round.StartTime;
        var nextEndTime = request.EndTime ?? round.EndTime;
        var nextStartSubmission = request.StartSubmission ?? round.StartSubmission;
        var nextEndSubmission = request.EndSubmission ?? round.EndSubmission;
        var nextLimitTeam = request.LimitTeam ?? round.LimitTeam;

        if (request.Name != null && string.IsNullOrWhiteSpace(request.Name))
        {
            throw new BadRequestException("ROUND_NAME_REQUIRED");
        }

        await ValidateRoundNo(round.EventId, nextRoundNo, round.Id);
        ValidateRoundTimes(nextStartTime, nextEndTime, nextStartSubmission, nextEndSubmission);
        ValidateLimitTeam(nextLimitTeam);

        if (request.Name != null)
        {
            round.Name = request.Name.Trim();
        }

        if (request.Description != null)
        {
            round.Description = request.Description.Trim();
        }

        if (request.RoundNo.HasValue)
        {
            round.RoundNo = request.RoundNo;
        }

        if (request.StartTime.HasValue)
        {
            round.StartTime = request.StartTime;
        }

        if (request.EndTime.HasValue)
        {
            round.EndTime = request.EndTime;
        }

        if (request.StartSubmission.HasValue)
        {
            round.StartSubmission = request.StartSubmission;
        }

        if (request.EndSubmission.HasValue)
        {
            round.EndSubmission = request.EndSubmission;
        }

        if (request.LimitTeam.HasValue)
        {
            round.LimitTeam = request.LimitTeam;
        }

        round.UpdatedAt = DateTimeOffset.UtcNow;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteRound(Guid roundId)
    {
        var round = await _dbContext.Rounds.FirstOrDefaultAsync(x => x.Id == roundId);
        if (round == null)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        round.IsDisable = true;
        round.UpdatedAt = DateTimeOffset.UtcNow;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<SendSystemNotificationResponse> SendSystemNotification(SendSystemNotificationRequest request)
    {
        GetCurrentUserId();

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new BadRequestException("TITLE_REQUIRED");
        }

        if (string.IsNullOrWhiteSpace(request.Description))
        {
            throw new BadRequestException("DESCRIPTION_REQUIRED");
        }

        var title = request.Title.Trim();
        var description = request.Description.Trim();
        var now = DateTimeOffset.UtcNow;
        var userIds = await _dbContext.Users
            .AsNoTracking()
            .Where(x => !x.IsDisable)
            .Select(x => x.Id)
            .ToListAsync();

        var notifications = userIds.Select(userId => new Hackathon.Repository.Entity.Notifications
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            TeamId = null,
            Title = title,
            Description = description,
            Status = NotificationStatusEnum.Unread,
            TargetType = NotificationTargetTypeEnum.System,
            IsDisable = false,
            CreatedAt = now,
            UpdatedAt = now
        }).ToList();

        await _dbContext.Notifications.AddRangeAsync(notifications);
        await _dbContext.SaveChangesAsync();

        return new SendSystemNotificationResponse
        {
            NotificationIds = notifications.Select(x => x.Id).ToList(),
            TotalSent = notifications.Count
        };
    }

    public async Task<string> ChangeUserRole(Guid userId, ChangeUserRoleRequest request)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        if (user.Role == request.Role)
        {
            throw new BadRequestException("ROLE_ALREADY_SET");
        }

        user.Role = request.Role;
        user.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();

        return "USER_ROLE_UPDATED_SUCCESSFULLY";
    }

    private async Task<List<AdminRoundResponse>> BuildRoundQuery(IQueryable<Hackathon.Repository.Entity.Rounds> q, PaginationRequest pagination)
    {
        return await q
            .OrderBy(x => x.RoundNo)
            .ThenBy(x => x.CreatedAt)
            .Skip((pagination.PageIndex - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .Select(x => new AdminRoundResponse
            {
                Id = x.Id,
                EventId = x.EventId,
                Name = x.Name,
                Description = x.Description,
                RoundNo = x.RoundNo,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                StartSubmission = x.StartSubmission,
                EndSubmission = x.EndSubmission,
                LimitTeam = x.LimitTeam,
                IsDisable = x.IsDisable,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
    }

    private async Task ValidateRoundNo(Guid eventId, int? roundNo, Guid? currentRoundId)
    {
        if (!roundNo.HasValue)
        {
            return;
        }

        if (roundNo.Value <= 0)
        {
            throw new BadRequestException("ROUND_NO_MUST_BE_POSITIVE");
        }

        var roundNoExists = await _dbContext.Rounds
            .AsNoTracking()
            .AnyAsync(x => x.EventId == eventId
                && x.RoundNo == roundNo.Value
                && !x.IsDisable
                && (!currentRoundId.HasValue || x.Id != currentRoundId.Value));

        if (roundNoExists)
        {
            throw new ConflictException("ROUND_NO_ALREADY_EXISTS");
        }
    }

    private static void ValidateRoundTimes(DateTimeOffset? startTime, DateTimeOffset? endTime, DateTimeOffset? startSubmission, DateTimeOffset? endSubmission)
    {
        if (startTime.HasValue && endTime.HasValue && startTime.Value > endTime.Value)
        {
            throw new BadRequestException("INVALID_ROUND_TIME_RANGE");
        }

        if (startSubmission.HasValue != endSubmission.HasValue)
        {
            throw new BadRequestException("SUBMISSION_TIME_RANGE_REQUIRED");
        }

        if (startSubmission.HasValue && endSubmission.HasValue && startSubmission.Value > endSubmission.Value)
        {
            throw new BadRequestException("INVALID_SUBMISSION_TIME_RANGE");
        }

        if (startTime.HasValue && endTime.HasValue && startSubmission.HasValue && endSubmission.HasValue
            && (startSubmission.Value < startTime.Value || endSubmission.Value > endTime.Value))
        {
            throw new BadRequestException("SUBMISSION_TIME_OUTSIDE_ROUND_TIME");
        }
    }

    private static void ValidateLimitTeam(int? limitTeam)
    {
        if (limitTeam.HasValue && limitTeam.Value <= 0)
        {
            throw new BadRequestException("LIMIT_TEAM_MUST_BE_POSITIVE");
        }
    }

    private async Task<List<AdminUserResponse>> BuildUserQuery(IQueryable<Hackathon.Repository.Entity.Users> q, PaginationRequest pagination)
    {
        return await q
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pagination.PageIndex - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .Select(x => new AdminUserResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                AvatarUrl = x.AvatarUrl,
                StudentId = x.StudentId,
                College = x.College,
                Role = x.Role,
                Status = x.Status,
                IsVerified = x.IsVerified,
                IsDisable = x.IsDisable,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
    }
}
