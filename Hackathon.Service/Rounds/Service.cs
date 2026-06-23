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

    private bool IsCurrentUserAdmin()
    {
        return _httpContext.HttpContext?.User.IsInRole(RoleEnum.Admin.ToString()) == true;
    }

    private async Task EnsureStaffAssignedToEvent(Guid eventId)
    {
        if (IsCurrentUserAdmin())
        {
            return;
        }

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

        var newSubmission = new Hackathon.Repository.Entity.Submissions
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

    public async Task<BasePaginationResponse> GetStaffRoundSubmissions(Guid roundId, Request.GetStaffRoundSubmissionsQuery query)
    {
        var round = await _dbContext.Rounds
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == roundId && !x.IsDisable);

        if (round == null)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        await EnsureStaffAssignedToEvent(round.EventId);

        var roundDetailsQuery = _dbContext.RoundDetails
            .AsNoTracking()
            .Include(x => x.RegisterTeam).ThenInclude(x => x.Team)
            .Include(x => x.RegisterTeam).ThenInclude(x => x.Track)
            .Include(x => x.RegisterTeam).ThenInclude(x => x.Topic)
            .Include(x => x.Submissions).ThenInclude(x => x.Scores)
            .Where(x => x.RoundId == roundId && !x.IsDisable && !x.RegisterTeam.IsDisable && !x.RegisterTeam.Team.IsDisable);

        if (query.TrackId.HasValue)
        {
            roundDetailsQuery = roundDetailsQuery.Where(x => x.RegisterTeam.TrackId == query.TrackId.Value);
        }

        if (query.TopicId.HasValue)
        {
            roundDetailsQuery = roundDetailsQuery.Where(x => x.RegisterTeam.TopicId == query.TopicId.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var keyword = query.Keyword.Trim().ToLower();
            roundDetailsQuery = roundDetailsQuery.Where(x => x.RegisterTeam.Team.Name.ToLower().Contains(keyword));
        }

        var roundDetails = await roundDetailsQuery.ToListAsync();
        var trackIds = roundDetails.Select(x => x.RegisterTeam.TrackId).Where(x => x.HasValue).Select(x => x!.Value).Distinct().ToList();
        var assignTracks = await GetJudgeAssignTracks(round.EventId, trackIds);

        var items = roundDetails.Select(roundDetail =>
        {
            var submission = roundDetail.Submissions
                .Where(x => !x.IsDisable)
                .OrderByDescending(x => x.SubmittedAt ?? x.CreatedAt)
                .FirstOrDefault();
            var trackAssignTracks = roundDetail.RegisterTeam.TrackId.HasValue
                ? assignTracks.Where(x => x.TrackId == roundDetail.RegisterTeam.TrackId.Value).ToList()
                : new List<AssignTracks>();

            if (submission == null)
            {
                return new Response.StaffRoundSubmissionResponse
                {
                    SubmissionId = null,
                    RoundDetailId = roundDetail.Id,
                    TeamId = roundDetail.RegisterTeam.TeamId,
                    TeamName = roundDetail.RegisterTeam.Team.Name,
                    TrackId = roundDetail.RegisterTeam.TrackId,
                    TrackTitle = roundDetail.RegisterTeam.Track?.Title,
                    TopicId = roundDetail.RegisterTeam.TopicId,
                    TopicTitle = roundDetail.RegisterTeam.Topic?.Title,
                    SubmissionStatus = SubmissionStatusEnum.Unsubmitted.ToString(),
                    GradingStatus = null,
                    AssignedJudges = BuildAssignedJudges(null, trackAssignTracks),
                };
            }

            var assignedJudges = BuildAssignedJudges(submission, trackAssignTracks);
            var scoredValues = assignedJudges
                .Where(x => x.TotalScore.HasValue)
                .Select(x => x.TotalScore!.Value)
                .ToList();

            return new Response.StaffRoundSubmissionResponse
            {
                SubmissionId = submission.Id,
                RoundDetailId = roundDetail.Id,
                TeamId = roundDetail.RegisterTeam.TeamId,
                TeamName = roundDetail.RegisterTeam.Team.Name,
                TrackId = roundDetail.RegisterTeam.TrackId,
                TrackTitle = roundDetail.RegisterTeam.Track?.Title,
                TopicId = roundDetail.RegisterTeam.TopicId,
                TopicTitle = roundDetail.RegisterTeam.Topic?.Title,
                Url = submission.Url,
                Description = submission.Description,
                SubmissionStatus = submission.Status?.ToString() ?? SubmissionStatusEnum.Unsubmitted.ToString(),
                SubmittedAt = submission.SubmittedAt,
                GradingStatus = GetGradingStatus(submission, assignedJudges),
                AssignedJudges = assignedJudges,
                AverageScore = scoredValues.Count == 0 ? null : scoredValues.Average(),
                MinScore = scoredValues.Count == 0 ? null : scoredValues.Min(),
                MaxScore = scoredValues.Count == 0 ? null : scoredValues.Max(),
            };
        }).ToList();

        if (!string.IsNullOrWhiteSpace(query.SubmissionStatus) && !query.SubmissionStatus.Equals("All", StringComparison.OrdinalIgnoreCase))
        {
            items = items.Where(x => x.SubmissionStatus.Equals(query.SubmissionStatus, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (!string.IsNullOrWhiteSpace(query.GradingStatus) && !query.GradingStatus.Equals("All", StringComparison.OrdinalIgnoreCase))
        {
            items = items.Where(x => string.Equals(x.GradingStatus, query.GradingStatus, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        var totalCount = items.Count;
        var pagedItems = items
            .OrderBy(x => x.TrackTitle)
            .ThenBy(x => x.TopicTitle)
            .ThenBy(x => x.TeamName)
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return ApiResponseFactory.BasePagination(pagedItems, query.PageIndex, query.PageSize, totalCount);
    }

    public async Task<Response.AssignJudgesToSubmissionResponse> AssignJudgesToSubmission(Guid submissionId, Request.AssignJudgesToSubmissionRequest request)
    {
        var submission = await _dbContext.Submissions
            .Include(x => x.RoundDetail).ThenInclude(x => x.Round)
            .Include(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam)
            .Include(x => x.Scores)
            .FirstOrDefaultAsync(x => x.Id == submissionId && !x.IsDisable);

        if (submission == null)
        {
            throw new NotFoundException("SUBMISSION_NOT_FOUND");
        }

        var trackId = submission.RoundDetail.RegisterTeam.TrackId;
        if (!trackId.HasValue)
        {
            throw new BadRequestException("TRACK_NOT_FOUND_FOR_SUBMISSION");
        }

        await EnsureStaffAssignedToEvent(submission.RoundDetail.Round.EventId);

        var judgeIds = request.JudgeIds.Distinct().ToList();
        var validAssignTracks = await GetJudgeAssignTracks(submission.RoundDetail.Round.EventId, new List<Guid> { trackId.Value }, judgeIds);

        if (validAssignTracks.Count != judgeIds.Count)
        {
            var eventJudgeIds = await _dbContext.AssignEvents
                .AsNoTracking()
                .Include(x => x.EventRole)
                .Where(x => !x.IsDisable
                    && x.EventId == submission.RoundDetail.Round.EventId
                    && x.EventRole.Name == EventRoleEnum.Judge
                    && x.User.Role == RoleEnum.Lecturer
                    && judgeIds.Contains(x.UserId))
                .Select(x => x.UserId)
                .ToListAsync();

            if (eventJudgeIds.Count != judgeIds.Count)
            {
                throw new BadRequestException("JUDGE_NOT_VALID");
            }

            throw new BadRequestException("JUDGE_NOT_ASSIGNED_TO_TRACK");
        }

        return new Response.AssignJudgesToSubmissionResponse
        {
            SubmissionId = submissionId,
            AssignedJudges = BuildAssignedJudges(submission, validAssignTracks),
        };
    }

    private async Task<List<AssignTracks>> GetJudgeAssignTracks(Guid eventId, List<Guid> trackIds, List<Guid>? judgeIds = null)
    {
        var query = _dbContext.AssignTracks
            .Include(x => x.AssignEvent).ThenInclude(x => x.EventRole)
            .Include(x => x.AssignEvent).ThenInclude(x => x.User)
            .Where(x => !x.IsDisable
                && trackIds.Contains(x.TrackId)
                && !x.AssignEvent.IsDisable
                && x.AssignEvent.EventId == eventId
                && x.AssignEvent.EventRole.Name == EventRoleEnum.Judge
                && x.AssignEvent.User.Role == RoleEnum.Lecturer);

        if (judgeIds != null)
        {
            query = query.Where(x => judgeIds.Contains(x.AssignEvent.UserId));
        }

        return await query.ToListAsync();
    }

    private static List<Response.AssignedJudgeResponse> BuildAssignedJudges(Hackathon.Repository.Entity.Submissions? submission, List<AssignTracks> assignTracks)
    {
        return assignTracks.Select(assignTrack =>
        {
            var score = submission?.Scores
                .Where(x => !x.IsDisable && x.AssignTrackId == assignTrack.Id)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefault();

            return new Response.AssignedJudgeResponse
            {
                JudgeId = assignTrack.AssignEvent.UserId,
                JudgeName = $"{assignTrack.AssignEvent.User.FirstName} {assignTrack.AssignEvent.User.LastName}".Trim(),
                Email = assignTrack.AssignEvent.User.Email,
                HasScored = score?.TotalScore.HasValue == true,
                TotalScore = score?.TotalScore,
                IsFinalized = false,
            };
        }).ToList();
    }

    private static string? GetGradingStatus(Hackathon.Repository.Entity.Submissions submission, List<Response.AssignedJudgeResponse> assignedJudges)
    {
        if (submission.Status != SubmissionStatusEnum.Submitted)
        {
            return null;
        }

        if (assignedJudges.Count == 0)
        {
            return "NoJudgesAssigned";
        }

        var scoredCount = assignedJudges.Count(x => x.HasScored);
        if (scoredCount == 0)
        {
            return "PendingGrading";
        }

        if (scoredCount < assignedJudges.Count)
        {
            return "GradingInProgress";
        }

        return assignedJudges.All(x => x.IsFinalized) ? "Finalized" : "Graded";
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