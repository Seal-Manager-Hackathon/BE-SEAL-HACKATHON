using System;
using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
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
        var role = _httpContext.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
        return Enum.TryParse<RoleEnum>(role, true, out var userRole) && userRole == RoleEnum.Admin;
    }

    private static Response.TrackResponse ToTrackResponse(Repository.Entity.Tracks track)
    {
        return new Response.TrackResponse
        {
            Id = track.Id,
            EventId = track.EventId,
            Title = track.Title,
            Description = track.Description,
            MaxTeam = track.MaxTeam,
            IsDisable = track.IsDisable,
            CreatedAt = track.CreatedAt,
            UpdatedAt = track.UpdatedAt,
        };
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

            if (Guid.TryParse(keyword.Trim(), out var trackId))
            {
                query = query.Where(x => x.Id == trackId
                    || x.Title.ToLower().Contains(normalizedKeyword)
                    || (x.Description != null && x.Description.ToLower().Contains(normalizedKeyword)));
            }
            else
            {
                query = query.Where(x => x.Title.ToLower().Contains(normalizedKeyword)
                    || (x.Description != null && x.Description.ToLower().Contains(normalizedKeyword)));
            }
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
                UpdatedAt = x.UpdatedAt,
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
    }

    public async Task<Response.TrackResponse> GetTrack(Guid trackId)
    {
        var track = await _dbContext.Tracks
            .AsNoTracking()
            .Include(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == trackId && !x.IsDisable);
        if (track == null)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        if (track.Event.IsDisable)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        return ToTrackResponse(track);
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

    public async Task<BasePaginationResponse> GetAdminTopicsByTrack(Guid trackId, string? keyword, bool? isDisable, PaginationRequest paginationRequest)
    {
        var trackEventId = await _dbContext.Tracks
            .AsNoTracking()
            .Where(x => x.Id == trackId)
            .Select(x => x.EventId)
            .FirstOrDefaultAsync();

        if (trackEventId == Guid.Empty)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(trackEventId);
        }

        return await GetTopicsByTrack(trackId, keyword, isDisable, paginationRequest);
    }

    public async Task<Response.TrackResponse> CreateTrack(Guid eventId, Request.CreateTrackRequest request)
    {
        if (eventId == Guid.Empty)
        {
            throw new BadRequestException("EVENT_ID_REQUIRED");
        }

        var title = request.Title?.Trim();
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new BadRequestException("TRACK_TITLE_REQUIRED");
        }

        var eventExists = await _dbContext.Events.AsNoTracking().AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var titleExists = await _dbContext.Tracks.AsNoTracking().AnyAsync(x => x.EventId == eventId
            && x.Title == title
            && !x.IsDisable);
        if (titleExists)
        {
            throw new ConflictException("TRACK_TITLE_ALREADY_EXISTS");
        }

        var now = DateTimeOffset.UtcNow;
        var track = new Repository.Entity.Tracks
        {
            Id = Guid.NewGuid(),
            EventId = eventId,
            Title = title,
            Description = request.Description,
            MaxTeam = request.MaxTeam,
            IsDisable = false,
            CreatedAt = now,
            UpdatedAt = now,
        };

        await _dbContext.Tracks.AddAsync(track);
        await _dbContext.SaveChangesAsync();

        return ToTrackResponse(track);
    }

    public async Task<Response.TrackResponse> UpdateTrack(Guid trackId, Request.UpdateTrackRequest request)
    {
        if (trackId == Guid.Empty)
        {
            throw new BadRequestException("TRACK_ID_REQUIRED");
        }

        var track = await _dbContext.Tracks
            .Include(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == trackId && !x.IsDisable);
        if (track == null)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        if (track.Event.IsDisable)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (request.Title != null)
        {
            var title = request.Title.Trim();
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new BadRequestException("TRACK_TITLE_REQUIRED");
            }

            var titleExists = await _dbContext.Tracks.AsNoTracking().AnyAsync(x => x.EventId == track.EventId
                && x.Id != trackId
                && x.Title == title
                && !x.IsDisable);
            if (titleExists)
            {
                throw new ConflictException("TRACK_TITLE_ALREADY_EXISTS");
            }

            track.Title = title;
        }

        if (request.Description != null)
        {
            track.Description = request.Description;
        }

        if (request.MaxTeam.HasValue)
        {
            track.MaxTeam = request.MaxTeam;
        }

        track.UpdatedAt = DateTimeOffset.UtcNow;
        _dbContext.Tracks.Update(track);
        await _dbContext.SaveChangesAsync();

        return ToTrackResponse(track);
    }

    public async Task<Response.TrackResponse> UpdateTrackVisibility(Guid trackId, Request.UpdateTrackVisibilityRequest request)
    {
        if (trackId == Guid.Empty)
        {
            throw new BadRequestException("TRACK_ID_REQUIRED");
        }

        var track = await _dbContext.Tracks
            .Include(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == trackId);
        if (track == null)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        if (track.Event.IsDisable)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(track.EventId);
        }

        track.IsDisable = !request.IsVisible;
        track.UpdatedAt = DateTimeOffset.UtcNow;
        _dbContext.Tracks.Update(track);
        await _dbContext.SaveChangesAsync();

        return ToTrackResponse(track);
    }

    public async Task<Response.TrackResponse> DeleteTrack(Guid trackId)
    {
        if (trackId == Guid.Empty)
        {
            throw new BadRequestException("TRACK_ID_REQUIRED");
        }

        var track = await _dbContext.Tracks
            .Include(x => x.Event)
            .Include(x => x.Topics)
            .FirstOrDefaultAsync(x => x.Id == trackId && !x.IsDisable);
        if (track == null)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        if (track.Event.IsDisable)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var now = DateTimeOffset.UtcNow;
        track.IsDisable = true;
        track.UpdatedAt = now;
        _dbContext.Tracks.Update(track);

        foreach (var topic in track.Topics.Where(x => !x.IsDisable))
        {
            topic.IsDisable = true;
            topic.UpdatedAt = now;
        }

        await _dbContext.SaveChangesAsync();

        return ToTrackResponse(track);
    }

    public async Task<BasePaginationResponse> GetApprovedTeamsByEvent(Guid eventId, string? keyword, RegisterTeamStatusEnum? status, bool? isDisable, PaginationRequest paginationRequest)
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
                Status = x.Status ?? RegisterTeamStatusEnum.Pending,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
    }

    public async Task<Response.TeamTrackAssignmentResponse> AssignTrackToTeam(Guid eventId, Guid teamId, Request.AssignTrackToTeamRequest request)
    {
        if (eventId == Guid.Empty)
        {
            throw new BadRequestException("EVENT_ID_REQUIRED");
        }

        if (request.TrackId == Guid.Empty)
        {
            throw new BadRequestException("TRACK_ID_REQUIRED");
        }

        var staffId = GetCurrentUserId();
        var staffUser = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == staffId && !x.IsDisable);
        if (staffUser == null || staffUser.Role != RoleEnum.Staff)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        await EnsureStaffAssignedToEvent(eventId);

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

        if (track.EventId != eventId)
        {
            throw new ConflictException("TRACK_NOT_IN_EVENT");
        }

        if (track.Event.IsDisable)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var registerTeam = await _dbContext.RegisterTeams.FirstOrDefaultAsync(x => x.TeamId == teamId
            && x.EventId == eventId
            && !x.IsDisable);
        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        if (registerTeam.TrackId == track.Id)
        {
            throw new ConflictException("TRACK_ALREADY_ASSIGNED");
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
        };
    }

    public async Task<Response.TeamTopicAssignmentResponse> AssignTopicToTeam(Guid eventId, Guid teamId, Request.AssignTopicToTeamRequest request)
    {
        if (eventId == Guid.Empty)
        {
            throw new BadRequestException("EVENT_ID_REQUIRED");
        }

        if (request.TopicId == Guid.Empty)
        {
            throw new BadRequestException("TOPIC_ID_REQUIRED");
        }

        var staffId = GetCurrentUserId();
        var staffUser = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == staffId && !x.IsDisable);
        if (staffUser == null || staffUser.Role != RoleEnum.Staff)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        await EnsureStaffAssignedToEvent(eventId);

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

        if (topic.Track.EventId != eventId)
        {
            throw new ConflictException("TOPIC_NOT_IN_EVENT");
        }

        if (topic.Track.IsDisable)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        var registerTeam = await _dbContext.RegisterTeams.FirstOrDefaultAsync(x => x.TeamId == teamId
            && x.EventId == eventId
            && !x.IsDisable);
        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        if (registerTeam.TopicId == topic.Id)
        {
            throw new ConflictException("TOPIC_ALREADY_ASSIGNED");
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
        };
    }

    public async Task<Response.TrackTeamCountResponse> GetTrackTeamCount(Guid trackId)
    {
        var track = await _dbContext.Tracks
            .AsNoTracking()
            .Include(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == trackId && !x.IsDisable);

        if (track == null)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        if (track.Event.IsDisable)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var teamCount = await _dbContext.RegisterTeams
            .AsNoTracking()
            .CountAsync(x => x.TrackId == trackId
                             && !x.IsDisable
                             && !x.Team.IsDisable
                             && x.Status == RegisterTeamStatusEnum.Approved);

        return new Response.TrackTeamCountResponse
        {
            TrackId = track.Id,
            EventId = track.EventId,
            Title = track.Title,
            MaxTeam = track.MaxTeam,
            CurrentTeamCount = teamCount
        };
    }

    public async Task<Response.MyEventAssignmentResponse> GetMyEventAssignment(Guid eventId, Hackathon.Repository.Enum.EventRoleEnum? role)
    {
        var userId = GetCurrentUserId();

        var assignEventQuery = _dbContext.AssignEvents
            .AsNoTracking()
            .Include(x => x.Event)
            .Include(x => x.EventRole)
            .Where(x => x.UserId == userId
                        && x.EventId == eventId
                        && !x.IsDisable
                        && !x.Event.IsDisable);

        if (role.HasValue)
        {
            assignEventQuery = assignEventQuery.Where(x =>
                x.EventRoleId != null
                && _dbContext.EventRoles.Any(er =>
                    er.Id == x.EventRoleId &&
                    er.Name == role.Value &&
                    !er.IsDisable));
        }

        var assignEvent = await assignEventQuery.FirstOrDefaultAsync();

        if (assignEvent == null)
        {
            throw new NotFoundException("NOT_ASSIGNED_TO_EVENT");
        }

        var tracks = await _dbContext.AssignTracks
            .AsNoTracking()
            .Include(x => x.Track)
            .Where(x =>
                x.AssignEventId == assignEvent.Id
                && !x.IsDisable
                && !x.Track.IsDisable)
            .OrderBy(x => x.Track.Title)
            .Select(x => new Response.MyEventTrackResponse
            {
                AssignTrackId = x.Id,
                TrackId = x.TrackId,
                TrackTitle = x.Track.Title,
                TrackDescription = x.Track.Description,
            })
            .ToListAsync();

        return new Response.MyEventAssignmentResponse
        {
            AssignEventId = assignEvent.Id,
            EventId = assignEvent.EventId,
            EventName = assignEvent.Event.Name,
            Role = assignEvent.EventRole != null ? (Hackathon.Repository.Enum.EventRoleEnum?)assignEvent.EventRole.Name : null,
            Tracks = tracks,
        };
    }
}
