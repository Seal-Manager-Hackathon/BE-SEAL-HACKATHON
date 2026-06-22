using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Rounds;

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

    public async Task<List<Response.RoundResponse>> GetRounds(Guid eventId)
    {
        var eventExists = await _dbContext.Events
            .AsNoTracking()
            .AnyAsync(x => x.Id == eventId && !x.IsDisable);

        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var rounds = await _dbContext.Rounds
            .AsNoTracking()
            .Where(x => x.EventId == eventId && !x.IsDisable)
            .OrderBy(x => x.RoundNo)
            .ThenBy(x => x.CreatedAt)
            .Select(x => new Response.RoundResponse
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

        return rounds;
    }

    public async Task<List<Response.MyRoundResponse>> GetMyRounds(Guid? eventId, Guid teamId)
    {
        var userId = GetCurrentUserId();

        if (eventId.HasValue)
        {
            var eventExists = await _dbContext.Events
                .AsNoTracking()
                .AnyAsync(x => x.Id == eventId.Value && !x.IsDisable);

            if (!eventExists)
            {
                throw new NotFoundException("EVENT_NOT_FOUND");
            }
        }

        var teamExists = await _dbContext.Teams
            .AsNoTracking()
            .AnyAsync(x => x.Id == teamId && !x.IsDisable);

        if (!teamExists)
        {
            throw new NotFoundException("TEAM_NOT_FOUND");
        }

        var query = _dbContext.RoundDetails
            .AsNoTracking()
            .Include(x => x.Round).ThenInclude(r => r.Event)
            .Include(x => x.RegisterTeam).ThenInclude(rt => rt.Team)
            .Where(x => !x.Round.IsDisable && !x.Round.Event.IsDisable && !x.RegisterTeam.Team.IsDisable)
            .Where(x => x.RegisterTeam.Team.TeamDetails.Any(td => td.UserId == userId && !td.IsDisable));

        if (eventId.HasValue)
        {
            query = query.Where(x => x.Round.EventId == eventId.Value);
        }

        query = query.Where(x => x.RegisterTeam.TeamId == teamId);

        var myRounds = await query
            .OrderBy(x => x.Round.RoundNo)
            .ThenBy(x => x.Round.StartTime)
            .Select(x => new Response.MyRoundResponse
            {
                RoundId = x.RoundId,
                EventId = x.Round.EventId,
                RoundName = x.Round.Name,
                EventName = x.Round.Event.Name,
                RoundNo = x.Round.RoundNo,
                TeamId = x.RegisterTeam.TeamId,
                TeamName = x.RegisterTeam.Team.Name,
                RegisterTeamId = x.RegisterTeamId,
                StartTime = x.Round.StartTime,
                EndTime = x.Round.EndTime,
                StartSubmission = x.Round.StartSubmission,
                EndSubmission = x.Round.EndSubmission
            })
            .ToListAsync();

        return myRounds;
    }

    public async Task<Response.MyRoundDetailResponse> GetMyRoundDetail(Guid registerTeamId)
    {
        var userId = GetCurrentUserId();

        var detail = await _dbContext.RoundDetails
            .AsNoTracking()
            .Include(x => x.Round).ThenInclude(r => r.Event)
            .Include(x => x.RegisterTeam).ThenInclude(rt => rt.Team)
            .Include(x => x.RegisterTeam).ThenInclude(rt => rt.Track)
            .Include(x => x.RegisterTeam).ThenInclude(rt => rt.Topic)
            .Where(x => !x.Round.IsDisable && !x.Round.Event.IsDisable && !x.RegisterTeam.Team.IsDisable)
            .Where(x => x.RegisterTeam.Team.TeamDetails.Any(td => td.UserId == userId && !td.IsDisable))
            .Where(x => x.RegisterTeamId == registerTeamId)
            .Select(x => new Response.MyRoundDetailResponse
            {
                RoundId = x.RoundId,
                EventId = x.Round.EventId,
                RoundName = x.Round.Name,
                EventName = x.Round.Event.Name,
                RoundNo = x.Round.RoundNo,
                TeamId = x.RegisterTeam.TeamId,
                TeamName = x.RegisterTeam.Team.Name,
                RegisterTeamId = x.RegisterTeamId,
                TrackId = x.RegisterTeam.TrackId,
                TrackTitle = x.RegisterTeam.Track != null ? x.RegisterTeam.Track.Title : null,
                TopicId = x.RegisterTeam.TopicId,
                TopicTitle = x.RegisterTeam.Topic != null ? x.RegisterTeam.Topic.Title : null,
                StartTime = x.Round.StartTime,
                EndTime = x.Round.EndTime,
                StartSubmission = x.Round.StartSubmission,
                EndSubmission = x.Round.EndSubmission
            })
            .FirstOrDefaultAsync();

        if (detail == null) throw new NotFoundException("ROUND_DETAIL_NOT_FOUND");
        return detail;
    }

    public async Task<Response.SubmitAssignmentResponse> SubmitAssignment(Guid roundId, Request.SubmitAssignmentRequest request)
    {
        if (roundId == Guid.Empty)
        {
            throw new BadRequestException("ROUND_ID_REQUIRED");
        }

        var url = request.Url?.Trim();
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new BadRequestException("URL_REQUIRED");
        }

        var userId = GetCurrentUserId();
        var now = DateTimeOffset.UtcNow;

        var round = await _dbContext.Rounds.FirstOrDefaultAsync(x => x.Id == roundId && !x.IsDisable);
        if (round == null)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        if (round.StartSubmission.HasValue && now < round.StartSubmission.Value)
        {
            throw new BadRequestException("SUBMISSION_NOT_STARTED");
        }

        if (round.EndSubmission.HasValue && now > round.EndSubmission.Value)
        {
            throw new BadRequestException("SUBMISSION_CLOSED");
        }

        var roundDetail = await _dbContext.RoundDetails
            .Include(x => x.RegisterTeam)
            .FirstOrDefaultAsync(x => x.RoundId == roundId
                                      && !x.IsDisable
                                      && !x.RegisterTeam.IsDisable
                                      && !x.RegisterTeam.IsBanned
                                      && x.RegisterTeam.Status == RegisterTeamStatusEnum.Approved
                                      && x.RegisterTeam.Team.TeamDetails.Any(td => td.UserId == userId
                                                                                   && !td.IsDisable
                                                                                   && td.Status == TeamDetailStatusEnum.Active));

        if (roundDetail == null)
        {
            throw new ForbiddenException("USER_TEAM_NOT_ALLOWED_TO_SUBMIT_THIS_ROUND");
        }

        var submission = await _dbContext.Submissions
            .FirstOrDefaultAsync(x => x.RoundDetailId == roundDetail.Id && !x.IsDisable);

        if (submission != null)
        {
            throw new ConflictException("ALREADY_SUBMITTED");
        }

        var newSubmission = new Submissions
        {
            Id = Guid.NewGuid(),
            RoundDetailId = roundDetail.Id,
            Url = url,
            Description = request.Description?.Trim(),
            Status = SubmissionStatusEnum.Submitted,
            SubmittedAt = now,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _dbContext.Submissions.AddAsync(newSubmission);
        await _dbContext.SaveChangesAsync();

        return new Response.SubmitAssignmentResponse
        {
            SubmissionId = newSubmission.Id,
            TeamId = roundDetail.RegisterTeam.TeamId,
            Url = newSubmission.Url,
            SubmittedAt = now,
        };
    }

    public async Task<BasePaginationResponse> GetRoundSubmissions(Guid roundId, Request.GetSubmissionsQuery query)
    {
        var round = await _dbContext.Rounds.AsNoTracking().FirstOrDefaultAsync(x => x.Id == roundId && !x.IsDisable);
        if (round == null)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        var submissionsQuery = _dbContext.Submissions
            .AsNoTracking()
            .Include(x => x.RoundDetail)
            .Where(x => x.RoundDetail.RoundId == roundId && !x.IsDisable);

        var totalCount = await submissionsQuery.CountAsync();

        var submissions = await submissionsQuery
            .OrderByDescending(x => x.SubmittedAt)
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(x => new Response.SubmissionResponse
            {
                SubmissionId = x.Id,
                Url = x.Url,
                SubmittedAt = x.SubmittedAt,
                Status = x.Status.ToString(),
                TotalScore = x.Scores.OrderByDescending(s => s.CreatedAt).FirstOrDefault().TotalScore
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(submissions, query.PageIndex, query.PageSize, totalCount);
    }

    public async Task<(Response.EndRoundResponse Data, string Message)> EndRound(Guid roundId)
    {
        var round = await _dbContext.Rounds
            .Include(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == roundId && !x.IsDisable);

        if (round == null)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        var nextRound = await _dbContext.Rounds
            .Where(x => x.EventId == round.EventId && !x.IsDisable && x.RoundNo == round.RoundNo + 1)
            .FirstOrDefaultAsync();

        var roundDetails = await _dbContext.RoundDetails
            .Include(x => x.Submissions)
                .ThenInclude(s => s.Scores)
            .Where(x => x.RoundId == roundId && !x.IsDisable)
            .ToListAsync();

        int totalTeamsAdvanced = 0;

        if (nextRound != null)
        {
            var limit = nextRound.LimitTeam ?? int.MaxValue;
            if (limit > 0)
            {
                var teamScores = roundDetails.Select(rd => new
                {
                    RoundDetail = rd,
                    MaxScore = rd.Submissions
                        .Where(s => !s.IsDisable)
                        .SelectMany(s => s.Scores)
                        .Where(sc => !sc.IsDisable)
                        .OrderByDescending(sc => sc.CreatedAt)
                        .FirstOrDefault()?.TotalScore ?? 0
                })
                .OrderByDescending(x => x.MaxScore)
                .Take(limit)
                .ToList();

                var nextRoundDetails = new List<RoundDetails>();
                foreach (var ts in teamScores)
                {
                    nextRoundDetails.Add(new RoundDetails
                    {
                        Id = Guid.NewGuid(),
                        RoundId = nextRound.Id,
                        RegisterTeamId = ts.RoundDetail.RegisterTeamId,
                        CreatedAt = DateTimeOffset.UtcNow,
                        UpdatedAt = DateTimeOffset.UtcNow
                    });
                    totalTeamsAdvanced++;
                }

                if (nextRoundDetails.Any())
                {
                    await _dbContext.RoundDetails.AddRangeAsync(nextRoundDetails);
                }
            }
        }

        // Close current round
        round.IsDisable = true;
        await _dbContext.SaveChangesAsync();

        var message = nextRound == null ? "FINAL_ROUND_CLOSED_HACKATHON_ENDED" : "ROUND_ENDED_SUCCESSFULLY";

        return (new Response.EndRoundResponse
        {
            ClosedRoundId = round.Id,
            NextRoundId = nextRound?.Id,
            TotalTeamsAdvanced = totalTeamsAdvanced,
        }, message);
    }
}