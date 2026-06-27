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

    private bool IsCurrentUserAdmin()
    {
        var role = _httpContext.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
        return Enum.TryParse<RoleEnum>(role, true, out var userRole) && userRole == RoleEnum.Admin;
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
            Status = eventEntity.Status,
            NumberRound = eventEntity.NumberRound,
            Season = eventEntity.Season,
            IsDisable = eventEntity.IsDisable,
            CreatedAt = eventEntity.CreatedAt,
        };
    }

    public async Task<List<Response.EventAssignmentResponse>> GetEventAssignments(Guid eventId)
    {
        var eventExists = await _dbContext.Events.AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        return await _dbContext.AssignEvents
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.EventRole)
            .Include(x => x.AssignTracks.Where(at => !at.IsDisable))
                .ThenInclude(at => at.Track)
            .Where(x => x.EventId == eventId && !x.IsDisable)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new Response.EventAssignmentResponse
            {
                AssignEventId = x.Id,
                UserId = x.UserId,
                FullName = x.User.FirstName + " " + x.User.LastName,
                Email = x.User.Email,
                EventRoleId = x.EventRoleId,
                EventRoleName = x.EventRole != null ? x.EventRole.Name : null,
                AssignedTracks = x.AssignTracks
                    .Where(at => !at.IsDisable)
                    .Select(at => new Response.AssignedTrackResponse
                    {
                        AssignTrackId = at.Id,
                        TrackId = at.TrackId,
                        TrackTitle = at.Track.Title
                    }).ToList()
            })
            .ToListAsync();
    }

    public async Task<Response.SetupStatusResponse> GetSetupStatus(Guid eventId)
    {
        var eventExists = await _dbContext.Events.AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(eventId);
        }

        var hasRounds = await _dbContext.Rounds.AnyAsync(x => x.EventId == eventId && !x.IsDisable);
        var hasCriteria = await _dbContext.Rounds
            .Where(x => x.EventId == eventId && !x.IsDisable)
            .SelectMany(x => x.CriteriaTemplates)
            .AnyAsync(ct => !ct.IsDisable && ct.CriteriaItems.Any(ci => !ci.IsDisable));
        var hasTracks = await _dbContext.Tracks.AnyAsync(x => x.EventId == eventId && !x.IsDisable);
        var hasTopics = await _dbContext.Tracks
            .Where(x => x.EventId == eventId && !x.IsDisable)
            .SelectMany(x => x.Topics)
            .AnyAsync(t => !t.IsDisable);
        var hasAwards = await _dbContext.Awards.AnyAsync(x => x.EventId == eventId && !x.IsDisable);
        var hasAssignedStaff = await _dbContext.AssignEvents.AnyAsync(x => x.EventId == eventId && !x.IsDisable);

        var checks = new Response.SetupChecks
        {
            HasRounds = hasRounds,
            HasCriteria = hasCriteria,
            HasTracks = hasTracks,
            HasTopics = hasTopics,
            HasAwards = hasAwards,
            HasAssignedStaff = hasAssignedStaff
        };

        var isReady = hasRounds && hasCriteria && hasTracks && hasTopics && hasAwards && hasAssignedStaff;
        string? message = null;
        if (!isReady)
        {
            if (!hasRounds) message = "NO_ROUNDS";
            else if (!hasCriteria) message = "NO_CRITERIA";
            else if (!hasTracks) message = "NO_TRACKS";
            else if (!hasTopics) message = "NO_TOPICS";
            else if (!hasAwards) message = "NO_AWARDS";
            else if (!hasAssignedStaff) message = "NO_ASSIGNED_STAFF";
        }

        return new Response.SetupStatusResponse
        {
            IsReadyToPublish = isReady,
            Checks = checks,
            Message = message
        };
    }

    public async Task<List<Response.AwardResponse>> GetAwards(Guid eventId)
    {
        var eventExists = await _dbContext.Events.AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        return await _dbContext.Awards
            .AsNoTracking()
            .Where(x => x.EventId == eventId && !x.IsDisable)
            .OrderByDescending(x => x.LevelAward)
            .Select(x => new Response.AwardResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                LevelAward = x.LevelAward,
                NumberOfAward = x.NumberOfAward,
                Prize = x.Prize
            })
            .ToListAsync();
    }

    public async Task<List<Response.LeaderboardResponse>> GetLeaderboard(Guid eventId)
    {
        var eventExists = await _dbContext.Events.AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var leaderboard = await _dbContext.LeaderBoards
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.EventId == eventId && !x.IsDisable);
        if (leaderboard == null)
        {
            return new List<Response.LeaderboardResponse>();
        }

        var items = await _dbContext.LeaderBoardDetails
            .AsNoTracking()
            .Include(x => x.Team)
            .Where(x => x.LeaderBoardId == leaderboard.Id && !x.IsDisable && !x.Team.IsDisable)
            .OrderByDescending(x => x.Score)
            .ThenBy(x => x.Team.Name)
            .Select(x => new
            {
                x.TeamId,
                TeamName = x.Team.Name,
                x.Score,
                x.LevelAward
            })
            .ToListAsync();

        return items.Select((x, index) => new Response.LeaderboardResponse
        {
            Rank = index + 1,
            TeamId = x.TeamId,
            TeamName = x.TeamName,
            TotalScore = x.Score,
            LevelAward = x.LevelAward
        }).ToList();
    }

    public async Task<Response.EventSummaryResponse> GetSummary(Guid eventId)
    {
        var eventExists = await _dbContext.Events.AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var totalApprovedTeams = await _dbContext.RegisterTeams.CountAsync(x => x.EventId == eventId
            && !x.IsDisable
            && !x.IsBanned
            && x.Status == RegisterTeamStatusEnum.Approved
            && !x.Team.IsDisable);
        var totalTracks = await _dbContext.Tracks.CountAsync(x => x.EventId == eventId && !x.IsDisable);
        var totalRounds = await _dbContext.Rounds.CountAsync(x => x.EventId == eventId && !x.IsDisable);
        var totalAwards = await _dbContext.Awards.CountAsync(x => x.EventId == eventId && !x.IsDisable);

        return new Response.EventSummaryResponse
        {
            TotalApprovedTeams = totalApprovedTeams,
            TotalTracks = totalTracks,
            TotalRounds = totalRounds,
            TotalAwards = totalAwards
        };
    }

    public async Task<List<Response.TeamScoreResponse>> GetTeamScores(Guid eventId, Guid teamId)
    {
        var eventExists = await _dbContext.Events.AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var teamExists = await _dbContext.RegisterTeams.AnyAsync(x => x.EventId == eventId
            && x.TeamId == teamId
            && !x.IsDisable
            && !x.Team.IsDisable);
        if (!teamExists)
        {
            throw new NotFoundException("TEAM_NOT_FOUND");
        }

        var rounds = await _dbContext.RoundDetails
            .AsNoTracking()
            .Include(x => x.Round)
            .Include(x => x.Submissions)
                .ThenInclude(x => x.Scores)
                    .ThenInclude(x => x.ScoreItems)
                        .ThenInclude(x => x.CriteriaItem)
            .Where(x => x.RegisterTeam.EventId == eventId
                        && x.RegisterTeam.TeamId == teamId
                        && !x.IsDisable
                        && !x.Round.IsDisable
                        && !x.RegisterTeam.IsDisable)
            .OrderByDescending(x => x.Round.RoundNo)
            .ThenByDescending(x => x.Round.CreatedAt)
            .ToListAsync();

        return rounds.Select(roundDetail =>
        {
            var scores = roundDetail.Submissions
                .Where(submission => !submission.IsDisable)
                .SelectMany(submission => submission.Scores)
                .Where(score => !score.IsDisable)
                .ToList();

            var criteriaScores = scores
                .SelectMany(score => score.ScoreItems)
                .Where(scoreItem => !scoreItem.IsDisable && !scoreItem.CriteriaItem.IsDisable)
                .GroupBy(scoreItem => new
                {
                    scoreItem.CriteriaItemId,
                    scoreItem.CriteriaItem.Name,
                    scoreItem.CriteriaItem.Score
                })
                .Select(group => new Response.CriteriaScoreResponse
                {
                    CriteriaItemId = group.Key.CriteriaItemId,
                    CriteriaItemName = group.Key.Name,
                    AverageCriteriaScore = group.Where(x => x.Score.HasValue).Select(x => x.Score).Average(),
                    MaxScore = group.Key.Score
                })
                .ToList();

            return new Response.TeamScoreResponse
            {
                RoundId = roundDetail.RoundId,
                RoundName = roundDetail.Round.Name,
                RoundNo = roundDetail.Round.RoundNo,
                AverageTotalScore = scores.Where(x => x.TotalScore.HasValue).Select(x => x.TotalScore).Average(),
                CriteriaScores = criteriaScores
            };
        }).ToList();
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
        };
    }

    public async Task<Response.AssignStaffToEventResponse> AssignStaffToEvent(Guid eventId, Request.AssignStaffToEventRequest request)
    {
        var eventExists = await _dbContext.Events.AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var staff = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId && !x.IsDisable);
        if (staff == null)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        if (staff.Role != RoleEnum.Staff)
        {
            throw new BadRequestException("USER_MUST_BE_STAFF");
        }

        var alreadyAssigned = await _dbContext.AssignEvents.AnyAsync(x => x.EventId == eventId
            && x.UserId == request.UserId
            && !x.IsDisable);
        if (alreadyAssigned)
        {
            throw new ConflictException("STAFF_ALREADY_ASSIGNED_TO_EVENT");
        }

        var now = DateTimeOffset.UtcNow;
        var assignEvent = new Repository.Entity.AssignEvents()
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            EventId = eventId,
            EventRoleId = null,
            IsDisable = false,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _dbContext.AssignEvents.AddAsync(assignEvent);
        await _dbContext.SaveChangesAsync();

        return new Response.AssignStaffToEventResponse
        {
            Id = assignEvent.Id
        };
    }

    public async Task<Response.CreateAwardResponse> CreateAward(Guid eventId, Request.CreateAwardRequest request)
    {
        var eventExists = await _dbContext.Events.AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new BadRequestException("AWARD_NAME_REQUIRED");
        }

        var now = DateTimeOffset.UtcNow;
        var award = new Awards
        {
            Id = Guid.NewGuid(),
            EventId = eventId,
            Name = request.Name.Trim(),
            Description = request.Description,
            LevelAward = request.LevelAward ?? 0,
            NumberOfAward = request.NumberOfAward,
            Prize = request.Prize,
            IsDisable = false,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _dbContext.Awards.AddAsync(award);
        await _dbContext.SaveChangesAsync();

        return new Response.CreateAwardResponse
        {
            Id = award.Id
        };
    }

    public async Task<Response.AssignEventToTrackResponse> AssignEventToTrack(Guid assignEventId, Request.AssignEventToTrackRequest request)
    {
        var assignEvent = await _dbContext.AssignEvents
            .Include(x => x.EventRole)
            .FirstOrDefaultAsync(x => x.Id == assignEventId && !x.IsDisable && !x.Event.IsDisable);
        if (assignEvent == null)
        {
            throw new NotFoundException("ASSIGNMENT_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(assignEvent.EventId);
        }

        if (assignEvent.EventRole?.Name != EventRoleEnum.Mentor && assignEvent.EventRole?.Name != EventRoleEnum.Judge)
        {
            throw new BadRequestException("INVALID_EVENT_ROLE");
        }

        var track = await _dbContext.Tracks.FirstOrDefaultAsync(x => x.Id == request.TrackId && !x.IsDisable);
        if (track == null)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        if (track.EventId != assignEvent.EventId)
        {
            throw new ConflictException("TRACK_NOT_IN_ASSIGNMENT_EVENT");
        }

        var alreadyAssigned = await _dbContext.AssignTracks.AnyAsync(x => x.AssignEventId == assignEventId
            && x.TrackId == request.TrackId
            && !x.IsDisable);
        if (alreadyAssigned)
        {
            throw new ConflictException("TRACK_ALREADY_ASSIGNED");
        }

        var now = DateTimeOffset.UtcNow;
        var assignTrack = new Repository.Entity.AssignTracks
        {
            Id = Guid.NewGuid(),
            AssignEventId = assignEventId,
            TrackId = request.TrackId,
            IsDisable = false,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _dbContext.AssignTracks.AddAsync(assignTrack);
        await _dbContext.SaveChangesAsync();

        return new Response.AssignEventToTrackResponse
        {
            AssignTrackId = assignTrack.Id
        };
    }

    public async Task<string> RecalculateLeaderboard(Guid eventId)
    {
        var eventExists = await _dbContext.Events.AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(eventId);
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        var now = DateTimeOffset.UtcNow;
        var leaderboard = await _dbContext.LeaderBoards.FirstOrDefaultAsync(x => x.EventId == eventId && !x.IsDisable);
        if (leaderboard == null)
        {
            leaderboard = new Repository.Entity.LeaderBoards()
            {
                Id = Guid.NewGuid(),
                EventId = eventId,
                Year = DateTimeOffset.UtcNow.Year,
                IsDisable = false,
                CreatedAt = now,
                UpdatedAt = now
            };
            await _dbContext.LeaderBoards.AddAsync(leaderboard);
            await _dbContext.SaveChangesAsync();
        }

        var teamScores = await _dbContext.RegisterTeams
            .Where(rt => rt.EventId == eventId
                         && !rt.IsDisable
                         && rt.Status == RegisterTeamStatusEnum.Approved
                         && !rt.Team.IsDisable)
            .Select(rt => new
            {
                rt.TeamId,
                Score = rt.RoundDetails
                    .SelectMany(rd => rd.Submissions)
                    .SelectMany(s => s.Scores)
                    .Where(s => !s.IsDisable && s.TotalScore.HasValue)
                    .Average(s => s.TotalScore)
            })
            .ToListAsync();

        foreach (var teamScore in teamScores)
        {
            var leaderboardDetail = await _dbContext.LeaderBoardDetails.FirstOrDefaultAsync(x => x.LeaderBoardId == leaderboard.Id
                && x.TeamId == teamScore.TeamId
                && !x.IsDisable);

            if (leaderboardDetail == null)
            {
                leaderboardDetail = new LeaderBoardDetails
                {
                    Id = Guid.NewGuid(),
                    LeaderBoardId = leaderboard.Id,
                    TeamId = teamScore.TeamId,
                    Score = teamScore.Score,
                    IsDisable = false,
                    CreatedAt = now,
                    UpdatedAt = now
                };
                await _dbContext.LeaderBoardDetails.AddAsync(leaderboardDetail);
            }
            else
            {
                leaderboardDetail.Score = teamScore.Score;
                leaderboardDetail.UpdatedAt = now;
                _dbContext.LeaderBoardDetails.Update(leaderboardDetail);
            }
        }

        leaderboard.UpdatedAt = now;
        _dbContext.LeaderBoards.Update(leaderboard);
        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();

        return "LEADERBOARD_RECALCULATED";
    }

    public async Task<string> LockLeaderboard(Guid eventId)
    {
        var eventExists = await _dbContext.Events.AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(eventId);
        }

        var leaderboard = await _dbContext.LeaderBoards.FirstOrDefaultAsync(x => x.EventId == eventId && !x.IsDisable);
        if (leaderboard == null)
        {
            throw new NotFoundException("LEADERBOARD_NOT_FOUND");
        }

        leaderboard.IsLocked = true;
        leaderboard.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.LeaderBoards.Update(leaderboard);
        await _dbContext.SaveChangesAsync();

        return "LEADERBOARD_LOCKED";
    }

    public async Task<string> PublishLeaderboard(Guid eventId)
    {
        var eventExists = await _dbContext.Events.AnyAsync(x => x.Id == eventId && !x.IsDisable);
        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(eventId);
        }

        var leaderboard = await _dbContext.LeaderBoards.FirstOrDefaultAsync(x => x.EventId == eventId && !x.IsDisable);
        if (leaderboard == null)
        {
            throw new NotFoundException("LEADERBOARD_NOT_FOUND");
        }

        leaderboard.IsPublished = true;
        leaderboard.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.LeaderBoards.Update(leaderboard);
        await _dbContext.SaveChangesAsync();

        return "LEADERBOARD_PUBLISHED";
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

        var newStartTime = request.StartTime ?? eventEntity.StartTime;
        var newEndTime = request.EndTime ?? eventEntity.EndTime;
        var newRegisterLimitTime = request.RegisterLimitTime ?? eventEntity.RegisterLimitTime;

        if (newStartTime.HasValue && newEndTime.HasValue && newStartTime.Value >= newEndTime.Value)
        {
            throw new BadRequestException("START_TIME_MUST_BE_BEFORE_END_TIME");
        }

        if (newRegisterLimitTime.HasValue && newStartTime.HasValue && newRegisterLimitTime.Value >= newStartTime.Value)
        {
            throw new BadRequestException("REGISTER_LIMIT_TIME_MUST_BE_BEFORE_START_TIME");
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

    public async Task<string> DeleteAward(Guid awardId)
    {
        var award = await _dbContext.Awards.FirstOrDefaultAsync(x => x.Id == awardId && !x.IsDisable);
        if (award == null)
        {
            throw new NotFoundException("AWARD_NOT_FOUND");
        }

        award.IsDisable = true;
        award.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.Awards.Update(award);
        await _dbContext.SaveChangesAsync();

        return "AWARD_DELETED_SUCCESSFULLY";
    }

    public async Task<Guid> RemoveTrackAssignment(Guid assignTrackId)
    {
        var assignTrack = await _dbContext.AssignTracks
            .Include(x => x.AssignEvent)
            .FirstOrDefaultAsync(x => x.Id == assignTrackId && !x.IsDisable);

        if (assignTrack == null)
        {
            throw new NotFoundException("ASSIGN_TRACK_NOT_FOUND");
        }

        if (!IsCurrentUserAdmin())
        {
            await EnsureStaffAssignedToEvent(assignTrack.AssignEvent.EventId);
        }

        assignTrack.IsDisable = true;
        assignTrack.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.AssignTracks.Update(assignTrack);
        await _dbContext.SaveChangesAsync();

        return assignTrack.Id;
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

    public async Task<string> UpdateAward(Guid id, Request.UpdateAwardRequest request)
    {
        var award = await _dbContext.Awards.FirstOrDefaultAsync(x => x.Id == id && !x.IsDisable);
        if (award == null)
        {
            throw new NotFoundException("AWARD_NOT_FOUND");
        }

        if (request.Name != null)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new BadRequestException("AWARD_NAME_REQUIRED");
            }

            award.Name = request.Name.Trim();
        }

        if (request.Description != null)
        {
            award.Description = request.Description;
        }

        if (request.LevelAward != null)
        {
            award.LevelAward = request.LevelAward.Value;
        }

        if (request.NumberOfAward.HasValue)
        {
            award.NumberOfAward = request.NumberOfAward;
        }

        if (request.Prize.HasValue)
        {
            award.Prize = request.Prize;
        }

        award.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.Awards.Update(award);
        await _dbContext.SaveChangesAsync();

        return "AWARD_UPDATED_SUCCESSFULLY";
    }

    public async Task<string> CancelEvent(Guid eventId)
    {
        var eventEntity = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == eventId && !x.IsDisable);
        if (eventEntity == null)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        eventEntity.Status = EventStatusEnum.Cancelled;
        eventEntity.UpdatedAt = DateTimeOffset.UtcNow;

        var rounds = await _dbContext.Rounds
            .Where(x => x.EventId == eventId && !x.IsDisable)
            .ToListAsync();

        var now = DateTimeOffset.UtcNow;
        foreach (var round in rounds)
        {
            round.IsDisable = true;
            round.UpdatedAt = now;
        }

        _dbContext.Events.Update(eventEntity);
        await _dbContext.SaveChangesAsync();

        return "EVENT_CANCELLED_SUCCESSFULLY";
    }

    public async Task<string> CloseEvent(Guid eventId)
    {
        var eventEntity = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == eventId && !x.IsDisable);
        if (eventEntity == null)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        eventEntity.Status = EventStatusEnum.Closed;
        eventEntity.UpdatedAt = DateTimeOffset.UtcNow;

        var leaderboard = await _dbContext.LeaderBoards.FirstOrDefaultAsync(x => x.EventId == eventId && !x.IsDisable);
        if (leaderboard != null)
        {
            leaderboard.IsLocked = true;
            leaderboard.UpdatedAt = DateTimeOffset.UtcNow;
        }

        _dbContext.Events.Update(eventEntity);
        await _dbContext.SaveChangesAsync();

        return "EVENT_CLOSED_SUCCESSFULLY";
    }

    public async Task<string> RestoreEvent(Guid eventId)
    {
        var eventEntity = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == eventId);
        if (eventEntity == null)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        eventEntity.IsDisable = false;
        eventEntity.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.Events.Update(eventEntity);
        await _dbContext.SaveChangesAsync();

        return "EVENT_RESTORED_SUCCESSFULLY";
    }

    public async Task<string> UnpublishEvent(Guid eventId)
    {
        var eventEntity = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == eventId && !x.IsDisable);
        if (eventEntity == null)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (eventEntity.Status != EventStatusEnum.Published)
        {
            throw new ConflictException("EVENT_NOT_IN_PUBLISHED_STATUS");
        }

        eventEntity.Status = EventStatusEnum.Draft;
        eventEntity.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.Events.Update(eventEntity);
        await _dbContext.SaveChangesAsync();

        return "EVENT_UNPUBLISHED_SUCCESSFULLY";
    }

    public async Task<string> UpdateLecturerRole(Guid id, Request.UpdateLecturerRoleRequest request)
    {
        var assignEvent = await _dbContext.AssignEvents
            .Include(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDisable && !x.Event.IsDisable);
        if (assignEvent == null)
        {
            throw new NotFoundException("ASSIGN_EVENT_NOT_FOUND");
        }

        if (string.IsNullOrWhiteSpace(request.EventRole))
        {
            throw new BadRequestException("INVALID_EVENT_ROLE");
        }

        if (!Enum.TryParse<EventRoleEnum>(request.EventRole, true, out var roleEnum))
        {
            throw new BadRequestException("INVALID_EVENT_ROLE");
        }

        var eventRole = await _dbContext.EventRoles.FirstOrDefaultAsync(x => x.Name == roleEnum);
        if (eventRole == null)
        {
            throw new BadRequestException("INVALID_EVENT_ROLE");
        }

        assignEvent.EventRoleId = eventRole.Id;
        assignEvent.UpdatedAt = DateTimeOffset.UtcNow;

        // Remove all existing track assignments for this assign event
        var existingTracks = await _dbContext.AssignTracks
            .Where(x => x.AssignEventId == id && !x.IsDisable)
            .ToListAsync();

        foreach (var track in existingTracks)
        {
            track.IsDisable = true;
            track.UpdatedAt = DateTimeOffset.UtcNow;
        }

        _dbContext.AssignEvents.Update(assignEvent);
        await _dbContext.SaveChangesAsync();

        return "LECTURER_ROLE_UPDATED";
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
                throw new BadRequestException("INVALID_EVENT_STATUS");
            }

            query = query.Where(x => x.Status == eventStatus);
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.StartTime)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new Response.StudentEventResponse
            {
                Id = x.Id,
                Name = x.Name,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Status = x.Status,
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
                throw new BadRequestException("INVALID_EVENT_STATUS");
            }

            query = query.Where(x => x.Status == eventStatus);
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderByDescending(x => x.StartTime)
            .ThenByDescending(x => x.CreatedAt)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new Response.AdminEventResponse
            {
                Id = x.Id,
                Name = x.Name,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Status = x.Status,
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
                throw new BadRequestException("INVALID_EVENT_STATUS");
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
                Status = x.Status,
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
            throw new BadRequestException("INVALID_LIMIT_PARAMETER");
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
                Status = x.Status,
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
