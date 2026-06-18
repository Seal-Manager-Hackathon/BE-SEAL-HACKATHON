using System;
using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Tracks;

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

    public Task<BasePaginationResponse> GetTracksByEvent(Guid eventId, string? keyword, bool? isDisable, PaginationRequest paginationRequest)
    {
        return GetTracks(eventId, keyword, isDisable, paginationRequest);
    }

    public async Task<BasePaginationResponse> GetTracks(Guid? eventId, string? keyword, bool? isDisable, PaginationRequest paginationRequest)
    {
        if (eventId.HasValue)
        {
            var eventExists = await _dbContext.Events.AnyAsync(x => x.Id == eventId.Value && !x.IsDisable);
            if (!eventExists)
            {
                throw new NotFoundException("EVENT_NOT_FOUND");
            }
        }

        var query = _dbContext.Tracks.AsNoTracking().AsQueryable();
        query = query.Where(x => x.IsDisable == (isDisable ?? false));

        if (eventId.HasValue)
        {
            query = query.Where(x => x.EventId == eventId.Value);
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var normalizedKeyword = keyword.Trim().ToLower();
            query = query.Where(x => x.Title.ToLower().Contains(normalizedKeyword)
                                     || (x.Description != null && x.Description.ToLower().Contains(normalizedKeyword)));
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(x => x.Title)
            .ThenBy(x => x.CreatedAt)
            .Skip((paginationRequest.PageIndex - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
            .Select(x => new Response.TrackResponse
            {
                Id = x.Id,
                EventId = x.EventId,
                Title = x.Title,
                Description = x.Description,
                MaxTeam = x.MaxTeam,
                IsDisable = x.IsDisable,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
    }

    public async Task<BasePaginationResponse> GetTopicsByTrack(Guid trackId, string? keyword, bool? isDisable, PaginationRequest paginationRequest)
    {
        var trackExists = await _dbContext.Tracks.AsNoTracking().AnyAsync(x => x.Id == trackId && !x.IsDisable);
        if (!trackExists)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        var query = _dbContext.Topics.AsNoTracking().Where(x => x.TrackId == trackId && x.IsDisable == (isDisable ?? false));

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var normalizedKeyword = keyword.Trim().ToLower();
            query = query.Where(x => x.Title.ToLower().Contains(normalizedKeyword)
                                     || (x.Description != null && x.Description.ToLower().Contains(normalizedKeyword)));
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(x => x.Title)
            .ThenBy(x => x.CreatedAt)
            .Skip((paginationRequest.PageIndex - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
            .Select(x => new Response.TopicResponse
            {
                Id = x.Id,
                TrackId = x.TrackId,
                Title = x.Title,
                Description = x.Description,
                IsDisable = x.IsDisable,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
    }

    public async Task<BasePaginationResponse> GetApprovedTeamsByEvent(Guid eventId, string? keyword, bool? isDisable, PaginationRequest paginationRequest)
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
                        && x.Status == RegisterTeamStatusEnum.Approved
                        && !x.Team.IsDisable);

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
            .Select(x => new Response.ApprovedTeamResponse
            {
                TeamId = x.TeamId,
                TeamName = x.Team.Name,
                TrackId = x.TrackId,
                TrackTitle = x.Track != null ? x.Track.Title : null,
                TopicId = x.TopicId,
                TopicTitle = x.Topic != null ? x.Topic.Title : null,
                Members = x.Team.TeamDetails
                    .Where(td => !td.IsDisable && td.Status == TeamDetailStatusEnum.Active)
                    .OrderByDescending(td => td.IsLeader)
                    .ThenBy(td => td.User.FirstName)
                    .ThenBy(td => td.User.LastName)
                    .Select(td => new Response.ApprovedTeamMemberResponse
                    {
                        UserId = td.UserId,
                        FullName = (td.User.FirstName + " " + td.User.LastName).Trim(),
                        Email = td.User.Email,
                        StudentId = td.User.StudentId,
                        IsLeader = td.IsLeader,
                    })
                    .ToList(),
                IsBanned = x.IsBanned,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
    }

    public async Task<Response.TeamTrackAssignmentResponse> AssignTrackToTeam(Guid teamId, Request.AssignTrackToTeamRequest request)
    {
        if (request.TrackId == Guid.Empty)
        {
            throw new BadRequestException("TRACK_ID_REQUIRED");
        }

        var team = await _dbContext.Teams.FirstOrDefaultAsync(x => x.Id == teamId && !x.IsDisable);
        if (team == null)
        {
            throw new NotFoundException("TEAM_NOT_FOUND");
        }

        var track = await _dbContext.Tracks
            .Include(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == request.TrackId && !x.IsDisable);
        if (track == null)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        if (track.Event.IsDisable)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        await EnsureStaffAssignedToEvent(track.EventId);

        var registerTeam = await _dbContext.RegisterTeams.FirstOrDefaultAsync(x => x.TeamId == teamId
            && x.EventId == track.EventId
            && !x.IsDisable);
        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        if (registerTeam.Status != RegisterTeamStatusEnum.Approved)
        {
            throw new ForbiddenException("REGISTER_TEAM_NOT_APPROVED");
        }

        if (registerTeam.IsBanned)
        {
            throw new ConflictException("TEAM_IS_BANNED_FROM_EVENT");
        }

        registerTeam.TrackId = track.Id;
        registerTeam.TopicId = null;
        registerTeam.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.RegisterTeams.Update(registerTeam);

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

        return new Response.TeamTrackAssignmentResponse
        {
            TeamId = team.Id,
            TeamName = team.Name,
            EventId = track.EventId,
            TrackId = track.Id,
            TrackTitle = track.Title,
            Message = "TRACK_ASSIGNED_TO_TEAM_SUCCESSFULLY",
        };
    }

    public async Task<Response.TeamTopicAssignmentResponse> AssignTopicToTeam(Guid teamId, Request.AssignTopicToTeamRequest request)
    {
        if (request.TopicId == Guid.Empty)
        {
            throw new BadRequestException("TOPIC_ID_REQUIRED");
        }

        var team = await _dbContext.Teams.FirstOrDefaultAsync(x => x.Id == teamId && !x.IsDisable);
        if (team == null)
        {
            throw new NotFoundException("TEAM_NOT_FOUND");
        }

        var topic = await _dbContext.Topics
            .Include(x => x.Track)
            .FirstOrDefaultAsync(x => x.Id == request.TopicId && !x.IsDisable);
        if (topic == null)
        {
            throw new NotFoundException("TOPIC_NOT_FOUND");
        }

        if (topic.Track.IsDisable)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        await EnsureStaffAssignedToEvent(topic.Track.EventId);

        var registerTeam = await _dbContext.RegisterTeams.FirstOrDefaultAsync(x => x.TeamId == teamId
            && x.EventId == topic.Track.EventId
            && !x.IsDisable);
        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        if (registerTeam.Status != RegisterTeamStatusEnum.Approved)
        {
            throw new ForbiddenException("REGISTER_TEAM_NOT_APPROVED");
        }

        if (registerTeam.IsBanned)
        {
            throw new ConflictException("TEAM_IS_BANNED_FROM_EVENT");
        }

        if (!registerTeam.TrackId.HasValue)
        {
            throw new ConflictException("TEAM_TRACK_NOT_ASSIGNED");
        }

        if (registerTeam.TrackId.Value != topic.TrackId)
        {
            throw new ConflictException("TOPIC_NOT_BELONG_TO_TEAM_TRACK");
        }

        registerTeam.TopicId = topic.Id;
        registerTeam.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.RegisterTeams.Update(registerTeam);

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

        return new Response.TeamTopicAssignmentResponse
        {
            TeamId = team.Id,
            TeamName = team.Name,
            EventId = topic.Track.EventId,
            TrackId = topic.TrackId,
            TrackTitle = topic.Track.Title,
            TopicId = topic.Id,
            TopicTitle = topic.Title,
            Message = "TOPIC_ASSIGNED_TO_TEAM_SUCCESSFULLY",
        };
    }
}
