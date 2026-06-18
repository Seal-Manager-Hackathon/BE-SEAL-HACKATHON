using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Events;

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

    private static Response.EventResponse ToResponse(Repository.Entity.Events eventEntity)
    {
        return new Response.EventResponse
        {
            Id = eventEntity.Id,
            Name = eventEntity.Name,
            Description = eventEntity.Description,
            StartTime = eventEntity.StartTime,
            EndTime = eventEntity.EndTime,
            RegisterLimitTime = eventEntity.RegisterLimitTime,
            LimitTeam = eventEntity.LimitTeam,
            MinMember = eventEntity.MinMember,
            MaxMember = eventEntity.MaxMember,
            Status = eventEntity.Status?.ToString(),
            NumberRound = eventEntity.NumberRound,
            Season = eventEntity.Season,
            IsDisable = eventEntity.IsDisable,
            CreatedAt = eventEntity.CreatedAt,
        };
    }

    public async Task<Response.CreateEventResponse> CreateEvent(Request.CreateEventRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new BadRequestException("EVENT_NAME_REQUIRED");
        }

        var normalizedName = request.Name.Trim().ToLower();
        var nameExists = await _dbContext.Events.AnyAsync(x => x.Name.ToLower() == normalizedName);
        if (nameExists)
        {
            throw new ConflictException("EVENT_NAME_ALREADY_EXISTS");
        }

        var now = DateTimeOffset.UtcNow;
        var eventEntity = new Repository.Entity.Events
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Description = request.Description,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            RegisterLimitTime = request.RegisterLimitTime,
            LimitTeam = request.LimitTeam,
            MinMember = request.MinMember,
            MaxMember = request.MaxMember,
            Status = EventStatusEnum.Draft,
            NumberRound = request.NumberRound,
            Season = request.Season,
            IsDisable = false,
            CreatedAt = now,
            UpdatedAt = now,
        };

        await _dbContext.Events.AddAsync(eventEntity);
        await _dbContext.SaveChangesAsync();

        return new Response.CreateEventResponse
        {
            Id = eventEntity.Id,
            Message = "EVENT_CREATED_SUCCESSFULLY",
        };
    }

    public async Task<string> UpdateEvent(Guid eventId, Request.UpdateEventRequest request)
    {
        var eventEntity = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == eventId);
        if (eventEntity == null)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (request.Name != null)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new BadRequestException("EVENT_NAME_REQUIRED");
            }

            var normalizedName = request.Name.Trim().ToLower();
            var nameExists = await _dbContext.Events.AnyAsync(x => x.Id != eventId && x.Name.ToLower() == normalizedName);
            if (nameExists)
            {
                throw new ConflictException("EVENT_NAME_ALREADY_EXISTS");
            }

            eventEntity.Name = request.Name.Trim();
        }

        if (request.Description != null)
        {
            eventEntity.Description = request.Description;
        }

        if (request.StartTime.HasValue)
        {
            eventEntity.StartTime = request.StartTime;
        }

        if (request.EndTime.HasValue)
        {
            eventEntity.EndTime = request.EndTime;
        }

        if (request.RegisterLimitTime.HasValue)
        {
            eventEntity.RegisterLimitTime = request.RegisterLimitTime;
        }

        if (request.LimitTeam.HasValue)
        {
            eventEntity.LimitTeam = request.LimitTeam;
        }

        if (request.MinMember.HasValue)
        {
            eventEntity.MinMember = request.MinMember;
        }

        if (request.MaxMember.HasValue)
        {
            eventEntity.MaxMember = request.MaxMember;
        }

        if (request.Status.HasValue)
        {
            eventEntity.Status = request.Status.Value;
        }

        if (request.NumberRound.HasValue)
        {
            eventEntity.NumberRound = request.NumberRound;
        }

        if (request.Season != null)
        {
            eventEntity.Season = request.Season;
        }

        eventEntity.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.Events.Update(eventEntity);
        await _dbContext.SaveChangesAsync();

        return "EVENT_UPDATED_SUCCESSFULLY";
    }

    public async Task<string> DeleteEvent(Guid eventId)
    {
        var eventEntity = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == eventId);
        if (eventEntity == null)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        eventEntity.IsDisable = true;
        eventEntity.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.Events.Update(eventEntity);
        await _dbContext.SaveChangesAsync();

        return "EVENT_DELETED_SUCCESSFULLY";
    }

    public async Task<string> PublishEvent(Guid eventId)
    {
        var eventEntity = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == eventId && !x.IsDisable);
        if (eventEntity == null)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (eventEntity.Status != EventStatusEnum.Draft)
        {
            throw new ConflictException("EVENT_NOT_IN_DRAFT_STATUS");
        }

        eventEntity.Status = EventStatusEnum.Published;
        eventEntity.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.Events.Update(eventEntity);
        await _dbContext.SaveChangesAsync();

        return "EVENT_PUBLISHED_SUCCESSFULLY";
    }

    public async Task<BasePaginationResponse> GetEvents(Request.GetEventsRequest request)
    {
        var query = _dbContext.Events.AsNoTracking().Where(x => !x.IsDisable);

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var normalizedKeyword = request.Keyword.Trim().ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(normalizedKeyword)
                                     || (x.Description != null && x.Description.ToLower().Contains(normalizedKeyword))
                                     || (x.Season != null && x.Season.ToLower().Contains(normalizedKeyword)));
        }

        if (request.Year.HasValue)
        {
            query = query.Where(x => x.StartTime.HasValue && x.StartTime.Value.Year == request.Year.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            if (!Enum.TryParse<EventStatusEnum>(request.Status, true, out var eventStatus))
            {
                throw new BadRequestException("BAD_REQUEST");
            }

            query = query.Where(x => x.Status == eventStatus);
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(x => x.StartTime)
            .ThenBy(x => x.CreatedAt)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new Response.StudentEventResponse
            {
                Id = x.Id,
                Name = x.Name,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Status = x.Status.ToString(),
                Season = x.Season,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, request.PageIndex, request.PageSize, totalCount);
    }

    public async Task<BasePaginationResponse> GetEventsForAdmin(Request.GetEventsForAdminRequest request)
    {
        var query = _dbContext.Events.AsNoTracking().AsQueryable();

        if (request.IsDisable.HasValue)
        {
            query = query.Where(x => x.IsDisable == request.IsDisable.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var normalizedKeyword = request.Keyword.Trim().ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(normalizedKeyword)
                                     || (x.Description != null && x.Description.ToLower().Contains(normalizedKeyword))
                                     || (x.Season != null && x.Season.ToLower().Contains(normalizedKeyword)));
        }

        if (request.Year.HasValue)
        {
            query = query.Where(x => x.StartTime.HasValue && x.StartTime.Value.Year == request.Year.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            if (!Enum.TryParse<EventStatusEnum>(request.Status, true, out var eventStatus))
            {
                throw new BadRequestException("BAD_REQUEST");
            }

            query = query.Where(x => x.Status == eventStatus);
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(x => x.StartTime)
            .ThenBy(x => x.CreatedAt)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new Response.AdminEventResponse
            {
                Id = x.Id,
                Name = x.Name,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Status = x.Status.ToString(),
                Season = x.Season,
                IsDisable = x.IsDisable,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, request.PageIndex, request.PageSize, totalCount);
    }

    public async Task<Response.EventResponse> GetEvent(Guid eventId)
    {
        var eventEntity = await _dbContext.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == eventId);
        if (eventEntity == null)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        return ToResponse(eventEntity);
    }

    public async Task<BasePaginationResponse> GetJoinedEvents(Request.GetJoinedEventsRequest request)
    {
        var userId = GetCurrentUserId();

        var query = _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Event)
            .Include(x => x.Team)
                .ThenInclude(x => x.TeamDetails)
            .Where(x => !x.IsDisable
                        && !x.Team.IsDisable
                        && !x.Event.IsDisable
                        && x.Team.TeamDetails.Any(td => td.UserId == userId && !td.IsDisable && td.Status == TeamDetailStatusEnum.Active))
            .Select(x => x.Event)
            .Distinct();

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var normalizedKeyword = request.Keyword.Trim().ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(normalizedKeyword)
                                     || (x.Description != null && x.Description.ToLower().Contains(normalizedKeyword))
                                     || (x.Season != null && x.Season.ToLower().Contains(normalizedKeyword)));
        }

        if (request.Year.HasValue)
        {
            query = query.Where(x => x.StartTime.HasValue && x.StartTime.Value.Year == request.Year.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            if (!Enum.TryParse<EventStatusEnum>(request.Status, true, out var eventStatus))
            {
                throw new BadRequestException("BAD_REQUEST");
            }

            query = query.Where(x => x.Status == eventStatus);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.StartTime)
            .ThenByDescending(x => x.CreatedAt)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new Response.StudentEventResponse
            {
                Id = x.Id,
                Name = x.Name,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Status = x.Status.ToString(),
                Season = x.Season,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, request.PageIndex, request.PageSize, totalCount);
    }

    public async Task<List<Response.EventParticipantResponse>> GetMostParticipants(int? limit, bool? isDisable)
    {
        var take = limit.GetValueOrDefault(10);
        if (take < 1)
        {
            throw new BadRequestException("BAD_REQUEST");
        }

        return await _dbContext.Events
            .AsNoTracking()
            .Where(x => x.IsDisable == (isDisable ?? false))
            .Select(x => new Response.EventParticipantResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                RegisterLimitTime = x.RegisterLimitTime,
                LimitTeam = x.LimitTeam,
                MinMember = x.MinMember,
                MaxMember = x.MaxMember,
                Status = x.Status.ToString(),
                NumberRound = x.NumberRound,
                Season = x.Season,
                IsDisable = x.IsDisable,
                CreatedAt = x.CreatedAt,
                TeamCount = x.RegisterTeams.Count(rt => !rt.IsDisable && rt.Status == RegisterTeamStatusEnum.Approved && !rt.Team.IsDisable),
                ParticipantCount = x.RegisterTeams
                    .Where(rt => !rt.IsDisable && rt.Status == RegisterTeamStatusEnum.Approved && !rt.Team.IsDisable)
                    .SelectMany(rt => rt.Team.TeamDetails)
                    .Count(td => !td.IsDisable),
            })
            .OrderByDescending(x => x.ParticipantCount)
            .ThenByDescending(x => x.TeamCount)
            .Take(take)
            .ToListAsync();
    }
}
