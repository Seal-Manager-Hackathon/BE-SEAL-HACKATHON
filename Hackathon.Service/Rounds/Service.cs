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
        var role = _httpContext.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
        return Enum.TryParse<RoleEnum>(role, true, out var userRole) && userRole == RoleEnum.Admin;
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

    public async Task<Response.RoundDetailResponse> GetRound(Guid roundId)
    {
        var round = await _dbContext.Rounds
            .AsNoTracking()
            .Where(x => x.Id == roundId && !x.IsDisable && !x.Event.IsDisable)
            .Select(x => new Response.RoundDetailResponse
            {
                Id = x.Id,
                EventId = x.EventId,
                EventName = x.Event.Name,
                Name = x.Name,
                Description = x.Description,
                RoundNo = x.RoundNo,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                StartSubmission = x.StartSubmission,
                EndSubmission = x.EndSubmission,
                LimitTeam = x.LimitTeam,
                IsDisable = x.IsDisable
            })
            .FirstOrDefaultAsync();

        if (round == null)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        return round;
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

    public async Task<Response.CreateSubmissionResponse> CreateSubmission(Guid roundId, Request.CreateSubmissionRequest request)
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
                                                                                   && td.Status == TeamDetailStatusEnum.Active
                                                                                   && td.IsLeader));

        if (roundDetail == null)
        {
            throw new ForbiddenException("USER_TEAM_NOT_ALLOWED_TO_SUBMIT_THIS_ROUND");
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

        return new Response.CreateSubmissionResponse
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

        var userId = GetCurrentUserId();

        var latestSubmission = await _dbContext.Submissions
            .AsNoTracking()
            .Include(x => x.RoundDetail)
            .Where(x => x.RoundDetail.RoundId == roundId && !x.IsDisable)
            .Where(x => x.RoundDetail.RegisterTeam.Team.TeamDetails.Any(td => td.UserId == userId && !td.IsDisable && td.Status == TeamDetailStatusEnum.Active))
            .OrderByDescending(x => x.SubmittedAt)
            .Select(x => new Response.SubmissionResponse
            {
                SubmissionId = x.Id,
                Url = x.Url,
                SubmittedAt = x.SubmittedAt,
                Status = x.Status,
                TotalScore = x.Scores.Where(s => !s.IsDisable).OrderByDescending(s => s.CreatedAt).Select(s => s.TotalScore).FirstOrDefault()
            })
            .FirstOrDefaultAsync();

        var list = latestSubmission != null
            ? new List<Response.SubmissionResponse> { latestSubmission }
            : new List<Response.SubmissionResponse>();

        return ApiResponseFactory.BasePagination(list, 1, 10, list.Count);
    }

    public async Task<BasePaginationResponse> GetMyRoundSubmissions(Guid roundId, Request.GetSubmissionsQuery query)
    {
        var roundExists = await _dbContext.Rounds
            .AsNoTracking()
            .AnyAsync(x => x.Id == roundId && !x.IsDisable && !x.Event.IsDisable);

        if (!roundExists)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        var userId = GetCurrentUserId();

        var isUserInAnyTeam = await _dbContext.TeamDetails
            .AsNoTracking()
            .AnyAsync(td => td.UserId == userId && !td.IsDisable && td.Status == TeamDetailStatusEnum.Active);

        if (!isUserInAnyTeam)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        var hasRoundDetail = await _dbContext.RoundDetails
            .AsNoTracking()
            .AnyAsync(x => x.RoundId == roundId
                && !x.IsDisable
                && !x.Round.IsDisable
                && !x.Round.Event.IsDisable
                && !x.RegisterTeam.IsDisable
                && !x.RegisterTeam.Team.IsDisable
                && x.RegisterTeam.Team.TeamDetails.Any(td => td.UserId == userId
                    && !td.IsDisable
                    && td.Status == TeamDetailStatusEnum.Active));

        if (!hasRoundDetail)
        {
            throw new NotFoundException("ROUND_DETAIL_NOT_FOUND");
        }

        var latestSubmission = await _dbContext.Submissions
            .AsNoTracking()
            .Where(x => x.RoundDetail.RoundId == roundId
                && !x.IsDisable
                && !x.RoundDetail.IsDisable
                && !x.RoundDetail.Round.IsDisable
                && !x.RoundDetail.Round.Event.IsDisable
                && !x.RoundDetail.RegisterTeam.IsDisable
                && !x.RoundDetail.RegisterTeam.Team.IsDisable
                && x.RoundDetail.RegisterTeam.Team.TeamDetails.Any(td => td.UserId == userId
                    && !td.IsDisable
                    && td.Status == TeamDetailStatusEnum.Active))
            .OrderByDescending(x => x.SubmittedAt ?? x.CreatedAt)
            .Select(x => new Response.MyRoundSubmissionResponse
            {
                SubmissionId = x.Id,
                RoundId = x.RoundDetail.RoundId,
                RoundName = x.RoundDetail.Round.Name,
                RoundDetailId = x.RoundDetailId,
                Url = x.Url,
                Description = x.Description,
                Status = x.Status,
                SubmittedAt = x.SubmittedAt,
                IsLatest = true,
                GradingStatus = x.Scores.Any(s => !s.IsDisable && s.TotalScore.HasValue) ? "Graded" : "NotGraded"
            })
            .FirstOrDefaultAsync();

        var list = latestSubmission != null ? new List<Response.MyRoundSubmissionResponse> { latestSubmission } : new List<Response.MyRoundSubmissionResponse>();
        return ApiResponseFactory.BasePagination(list, 1, 10, list.Count);
    }

    public async Task<Response.MyRoundScoreResponse> GetMyRoundScore(Guid roundId)
    {
        var roundExists = await _dbContext.Rounds
            .AsNoTracking()
            .AnyAsync(x => x.Id == roundId && !x.IsDisable && !x.Event.IsDisable);

        if (!roundExists)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        var userId = GetCurrentUserId();

        var isUserInAnyTeam = await _dbContext.TeamDetails
            .AsNoTracking()
            .AnyAsync(td => td.UserId == userId && !td.IsDisable && td.Status == TeamDetailStatusEnum.Active);

        if (!isUserInAnyTeam)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        var roundDetail = await _dbContext.RoundDetails
            .AsNoTracking()
            .Include(x => x.Round)
            .Include(x => x.RegisterTeam).ThenInclude(x => x.Team)
            .Include(x => x.Submissions).ThenInclude(x => x.Scores).ThenInclude(x => x.ScoreItems).ThenInclude(x => x.CriteriaItem)
            .Where(x => x.RoundId == roundId
                && !x.IsDisable
                && !x.Round.IsDisable
                && !x.Round.Event.IsDisable
                && !x.RegisterTeam.IsDisable
                && !x.RegisterTeam.Team.IsDisable
                && x.RegisterTeam.Team.TeamDetails.Any(td => td.UserId == userId
                    && !td.IsDisable
                    && td.Status == TeamDetailStatusEnum.Active))
            .FirstOrDefaultAsync();

        if (roundDetail == null)
        {
            throw new NotFoundException("ROUND_DETAIL_NOT_FOUND");
        }

        var submission = roundDetail.Submissions
            .Where(x => !x.IsDisable && x.Status == SubmissionStatusEnum.Submitted)
            .OrderByDescending(x => x.SubmittedAt ?? x.CreatedAt)
            .FirstOrDefault();

        if (submission == null)
        {
            throw new NotFoundException("SUBMISSION_NOT_FOUND");
        }

        var scores = submission.Scores
            .Where(x => !x.IsDisable && !x.IsMock && x.TotalScore.HasValue)
            .ToList();

        if (scores.Count == 0)
        {
            return new Response.MyRoundScoreResponse
            {
                RoundId = roundDetail.RoundId,
                RoundName = roundDetail.Round.Name,
                TeamId = roundDetail.RegisterTeam.TeamId,
                TeamName = roundDetail.RegisterTeam.Team.Name,
                SubmissionId = submission.Id,
                GradingStatus = "NotGraded",
                Message = "NOT_GRADED",
                AverageTotalScore = null,
                IsAppealable = false,
                CriteriaScores = new List<Response.MyRoundCriteriaScoreResponse>()
            };
        }

        var criteriaScores = scores
            .SelectMany(x => x.ScoreItems)
            .Where(x => !x.IsDisable && x.Score.HasValue && !x.CriteriaItem.IsDisable)
            .GroupBy(x => new { x.CriteriaItemId, x.CriteriaItem.Name, x.CriteriaItem.Score })
            .Select(x => new Response.MyRoundCriteriaScoreResponse
            {
                CriteriaItemId = x.Key.CriteriaItemId,
                CriteriaItemName = x.Key.Name,
                AverageCriteriaScore = x.Average(item => item.Score!.Value),
                MaxScore = x.Key.Score
            })
            .OrderBy(x => x.CriteriaItemName)
            .ToList();

        return new Response.MyRoundScoreResponse
        {
            RoundId = roundDetail.RoundId,
            RoundName = roundDetail.Round.Name,
            TeamId = roundDetail.RegisterTeam.TeamId,
            TeamName = roundDetail.RegisterTeam.Team.Name,
            SubmissionId = submission.Id,
            GradingStatus = "Graded",
            AverageTotalScore = scores.Average(x => x.TotalScore!.Value),
            IsAppealable = true,
            CriteriaScores = criteriaScores
        };
    }

    public async Task<BasePaginationResponse> GetRoundRanking(Guid roundId, Request.GetSubmissionsQuery query)
    {
        var roundExists = await _dbContext.Rounds
            .AsNoTracking()
            .AnyAsync(x => x.Id == roundId && !x.IsDisable && !x.Event.IsDisable);

        if (!roundExists)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        var roundDetails = await _dbContext.RoundDetails
            .AsNoTracking()
            .Include(x => x.RegisterTeam).ThenInclude(x => x.Team)
            .Include(x => x.Submissions).ThenInclude(x => x.Scores)
            .Where(x => x.RoundId == roundId
                && !x.IsDisable
                && !x.RegisterTeam.IsDisable
                && !x.RegisterTeam.Team.IsDisable)
            .ToListAsync();

        var rankings = roundDetails
            .Select(x => new
            {
                RoundDetail = x,
                Submission = x.Submissions
                    .Where(s => !s.IsDisable && s.Status == SubmissionStatusEnum.Submitted)
                    .OrderByDescending(s => s.SubmittedAt ?? s.CreatedAt)
                    .FirstOrDefault()
            })
            .Where(x => x.Submission != null)
            .Select(x => new
            {
                x.RoundDetail.RegisterTeam.TeamId,
                TeamName = x.RoundDetail.RegisterTeam.Team.Name,
                SubmissionId = x.Submission!.Id,
                Scores = x.Submission.Scores
                    .Where(s => !s.IsDisable && !s.IsMock && s.TotalScore.HasValue)
                    .Select(s => s.TotalScore!.Value)
                    .ToList()
            })
            .Where(x => x.Scores.Count > 0)
            .Select(x => new
            {
                x.TeamId,
                x.TeamName,
                x.SubmissionId,
                AverageScore = x.Scores.Average()
            })
            .OrderByDescending(x => x.AverageScore)
            .ThenBy(x => x.TeamName)
            .Select((x, index) => new Response.RoundRankingResponse
            {
                Rank = index + 1,
                TeamId = x.TeamId,
                TeamName = x.TeamName,
                SubmissionId = x.SubmissionId,
                AverageScore = x.AverageScore
            })
            .ToList();

        var totalCount = rankings.Count;
        var pagedRankings = rankings
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return ApiResponseFactory.BasePagination(pagedRankings, query.PageIndex, query.PageSize, totalCount);
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

        var items = roundDetails.SelectMany(roundDetail =>
        {
            var submissions = roundDetail.Submissions
                .Where(x => !x.IsDisable)
                .OrderByDescending(x => x.SubmittedAt ?? x.CreatedAt)
                .ToList();

            // Staff/Admin sees ALL versions
            var trackAssignTracks = roundDetail.RegisterTeam.TrackId.HasValue
                ? assignTracks.Where(x => x.TrackId == roundDetail.RegisterTeam.TrackId.Value).ToList()
                : new List<Hackathon.Repository.Entity.AssignTracks>();

            if (submissions.Count == 0)
            {
                return new List<Response.StaffRoundSubmissionResponse>
                {
                    new()
                    {
                        SubmissionId = null,
                        RoundDetailId = roundDetail.Id,
                        TeamId = roundDetail.RegisterTeam.TeamId,
                        TeamName = roundDetail.RegisterTeam.Team.Name,
                        TrackId = roundDetail.RegisterTeam.TrackId,
                        TrackTitle = roundDetail.RegisterTeam.Track?.Title,
                        TopicId = roundDetail.RegisterTeam.TopicId,
                        TopicTitle = roundDetail.RegisterTeam.Topic?.Title,
                        SubmissionStatus = SubmissionStatusEnum.Unsubmitted,
                        GradingStatus = null,
                        AssignedJudges = BuildAssignedJudges(null, trackAssignTracks),
                    }
                };
            }

            return submissions.Select(submission =>
            {
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
                SubmissionStatus = submission.Status ?? SubmissionStatusEnum.Unsubmitted,
                SubmittedAt = submission.SubmittedAt,
                GradingStatus = GetGradingStatus(submission, assignedJudges),
                AssignedJudges = assignedJudges,
                AverageScore = scoredValues.Count == 0 ? null : scoredValues.Average(),
                MinScore = scoredValues.Count == 0 ? null : scoredValues.Min(),
                MaxScore = scoredValues.Count == 0 ? null : scoredValues.Max(),
            };
        }).ToList();
        }).ToList();

        if (!string.IsNullOrWhiteSpace(query.SubmissionStatus) && !query.SubmissionStatus.Equals("All", StringComparison.OrdinalIgnoreCase))
        {
            if (Enum.TryParse<SubmissionStatusEnum>(query.SubmissionStatus, true, out var filterStatus))
            {
                items = items.Where(x => x.SubmissionStatus == filterStatus).ToList();
            }
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
                    && x.EventRole != null
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

    private async Task<List<Hackathon.Repository.Entity.AssignTracks>> GetJudgeAssignTracks(Guid eventId, List<Guid> trackIds, List<Guid>? judgeIds = null)
    {
        var query = _dbContext.AssignTracks
            .Include(x => x.AssignEvent).ThenInclude(x => x.EventRole)
            .Include(x => x.AssignEvent).ThenInclude(x => x.User)
            .Where(x => !x.IsDisable
                && trackIds.Contains(x.TrackId)
                && !x.AssignEvent.IsDisable
                && x.AssignEvent.EventId == eventId
                && x.AssignEvent.EventRole != null
                && x.AssignEvent.EventRole.Name == EventRoleEnum.Judge
                && x.AssignEvent.User.Role == RoleEnum.Lecturer);

        if (judgeIds != null)
        {
            query = query.Where(x => judgeIds.Contains(x.AssignEvent.UserId));
        }

        return await query.ToListAsync();
    }

    private static List<Response.AssignedJudgeResponse> BuildAssignedJudges(Hackathon.Repository.Entity.Submissions? submission, List<Hackathon.Repository.Entity.AssignTracks> assignTracks)
    {
        return assignTracks.Select(assignTrack =>
        {
            var score = submission?.Scores
                ?.Where(x => !x.IsDisable && x.AssignTrackId == assignTrack.Id)
                ?.OrderByDescending(x => x.CreatedAt)
                ?.FirstOrDefault();

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

    public async Task<BasePaginationResponse> GetLecturerRoundSubmissions(Guid roundId, Request.GetSubmissionsQuery query)
    {
        var round = await _dbContext.Rounds
            .AsNoTracking()
            .Include(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == roundId && !x.IsDisable);

        if (round == null)
            throw new NotFoundException("ROUND_NOT_FOUND");

        // Check judge is assigned to this event
        var userId = GetCurrentUserId();
        var judgeAssignEvents = await _dbContext.AssignEvents
            .AsNoTracking()
            .Where(x => x.UserId == userId
                && x.EventId == round.EventId
                && !x.IsDisable
                && x.EventRole != null
                && x.EventRole.Name == EventRoleEnum.Judge)
            .Select(x => x.Id)
            .ToListAsync();

        if (judgeAssignEvents.Count == 0)
            throw new ForbiddenException("JUDGE_NOT_ASSIGNED_TO_EVENT");

        // Get tracks assigned to this judge
        var judgeTrackIds = await _dbContext.AssignTracks
            .AsNoTracking()
            .Where(x => judgeAssignEvents.Contains(x.AssignEventId) && !x.IsDisable)
            .Select(x => x.TrackId)
            .ToListAsync();

        // Check round submission time — if still open, judge cannot view
        var now = DateTimeOffset.UtcNow;
        if (!round.EndSubmission.HasValue || now <= round.EndSubmission.Value)
            throw new BadRequestException("ROUND_SUBMISSION_STILL_OPEN");

        var roundDetails = await _dbContext.RoundDetails
            .AsNoTracking()
            .Include(x => x.RegisterTeam).ThenInclude(x => x.Team)
            .Include(x => x.RegisterTeam).ThenInclude(x => x.Track)
            .Include(x => x.RegisterTeam).ThenInclude(x => x.Topic)
            .Include(x => x.Submissions).ThenInclude(x => x.Scores)
            .Where(x => x.RoundId == roundId
                && !x.IsDisable
                && !x.RegisterTeam.IsDisable
                && !x.RegisterTeam.Team.IsDisable
                && x.RegisterTeam.TrackId.HasValue
                && judgeTrackIds.Contains(x.RegisterTeam.TrackId.Value))
            .ToListAsync();

        var items = roundDetails.Select(roundDetail =>
        {
            var submission = roundDetail.Submissions
                .Where(x => !x.IsDisable)
                .OrderByDescending(x => x.SubmittedAt ?? x.CreatedAt)
                .FirstOrDefault();

            return new Response.StaffRoundSubmissionResponse
            {
                SubmissionId = submission?.Id,
                RoundDetailId = roundDetail.Id,
                TeamId = roundDetail.RegisterTeam.TeamId,
                TeamName = roundDetail.RegisterTeam.Team.Name,
                TrackId = roundDetail.RegisterTeam.TrackId,
                TrackTitle = roundDetail.RegisterTeam.Track?.Title,
                TopicId = roundDetail.RegisterTeam.TopicId,
                TopicTitle = roundDetail.RegisterTeam.Topic?.Title,
                Url = submission?.Url,
                Description = submission?.Description,
                SubmissionStatus = submission?.Status,
                SubmittedAt = submission?.SubmittedAt,
                AverageScore = submission?.Scores != null
                    ? submission.Scores
                        .Where(s => !s.IsDisable && !s.IsMock && s.TotalScore.HasValue)
                        .Select(s => s.TotalScore!.Value)
                        .DefaultIfEmpty()
                        .Average()
                    : null
            };
        }).ToList();

        var totalCount = items.Count;
        var pagedItems = items
            .OrderBy(x => x.TeamName)
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return ApiResponseFactory.BasePagination(pagedItems, query.PageIndex, query.PageSize, totalCount);
    }

    public async Task UpdateRound(Guid roundId, Request.UpdateRoundRequest request)
    {
        var round = await _dbContext.Rounds.FirstOrDefaultAsync(x => x.Id == roundId && !x.IsDisable);
        if (round == null)
            throw new NotFoundException("ROUND_NOT_FOUND");

        if (request.Name != null)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new BadRequestException("ROUND_NAME_REQUIRED");
            round.Name = request.Name.Trim();
        }

        if (request.Description != null)
            round.Description = request.Description?.Trim();

        if (request.RoundNo.HasValue)
            round.RoundNo = request.RoundNo;

        if (request.StartTime.HasValue)
            round.StartTime = request.StartTime;

        if (request.EndTime.HasValue)
            round.EndTime = request.EndTime;

        if (request.StartSubmission.HasValue)
            round.StartSubmission = request.StartSubmission;

        if (request.EndSubmission.HasValue)
            round.EndSubmission = request.EndSubmission;

        if (request.LimitTeam.HasValue)
            round.LimitTeam = request.LimitTeam;

        round.UpdatedAt = DateTimeOffset.UtcNow;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Response.EndRoundResponse> EndRound(Guid roundId)
    {
        var round = await _dbContext.Rounds
            .Include(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == roundId);

        if (round == null)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        await EnsureStaffAssignedToEvent(round.EventId);

        // Check time: round must be ended (current time > round.EndTime)
        var now = DateTimeOffset.UtcNow;
        if (!round.EndTime.HasValue || now <= round.EndTime.Value)
        {
            throw new BadRequestException("ROUND_NOT_ENDED_YET");
        }

        // If round is not yet closed by the job, do advancement now
        var isAlreadyClosed = round.IsDisable;
        if (!isAlreadyClosed)
        {
            await CloseAndAdvanceRoundAsync(_dbContext, round, now);
        }

        // Build ranking response
        var nextRound = await _dbContext.Rounds
            .Where(x => x.EventId == round.EventId && x.RoundNo == round.RoundNo + 1)
            .FirstOrDefaultAsync();

        var roundDetails = await _dbContext.RoundDetails
            .AsNoTracking()
            .Include(x => x.RegisterTeam).ThenInclude(x => x.Team)
            .Include(x => x.Submissions.Where(s => !s.IsDisable && s.Status == SubmissionStatusEnum.Submitted))
                .ThenInclude(s => s.Scores.Where(sc => !sc.IsDisable && !sc.IsMock))
            .Where(x => x.RoundId == roundId && !x.IsDisable && !x.RegisterTeam.IsDisable && !x.RegisterTeam.Team.IsDisable
                && x.RegisterTeam.Status == RegisterTeamStatusEnum.Approved && !x.RegisterTeam.IsBanned)
            .ToListAsync();

        // Calculate team scores
        var teamScores = roundDetails
            .Select(rd =>
            {
                var latestSubmission = rd.Submissions
                    .OrderByDescending(s => s.SubmittedAt ?? s.CreatedAt)
                    .FirstOrDefault();

                decimal avgScore = 0;
                var hasScore = false;
                if (latestSubmission != null)
                {
                    var validScores = latestSubmission.Scores
                        .Where(s => s.TotalScore.HasValue)
                        .GroupBy(s => s.AssignTrackId)
                        .Select(g => g.OrderByDescending(s => s.CreatedAt).First().TotalScore!.Value)
                        .ToList();

                    if (validScores.Count != 0)
                    {
                        avgScore = validScores.Average();
                        hasScore = true;
                    }
                }

                return new
                {
                    RoundDetail = rd,
                    LatestSubmission = latestSubmission,
                    AverageScore = avgScore,
                    HasScore = hasScore
                };
            })
            .OrderByDescending(x => x.HasScore)
            .ThenByDescending(x => x.AverageScore)
            .ThenBy(x => x.RoundDetail.RegisterTeam.Team.Name)
            .ToList();

        var limit = nextRound?.LimitTeam ?? int.MaxValue;
        var advancedTeams = new List<Response.AdvancedTeamResponse>();
        int rank = 0;

        foreach (var ts in teamScores)
        {
            rank++;

            advancedTeams.Add(new Response.AdvancedTeamResponse
            {
                Rank = rank,
                TeamId = ts.RoundDetail.RegisterTeam.TeamId,
                TeamName = ts.RoundDetail.RegisterTeam.Team.Name,
                AverageScore = ts.AverageScore,
                LatestSubmissionId = ts.LatestSubmission?.Id ?? Guid.Empty,
                IsAdvanced = ts.HasScore && ts.AverageScore > 0 && rank <= limit,
            });
        }

        string message;
        if (isAlreadyClosed)
        {
            message = nextRound == null ? "FINAL_ROUND_CLOSED_HACKATHON_ENDED" : "ROUND_ALREADY_CLOSED";
        }
        else
        {
            message = nextRound == null ? "FINAL_ROUND_CLOSED_HACKATHON_ENDED" : "ROUND_ENDED_SUCCESSFULLY";
        }

        return new Response.EndRoundResponse
        {
            RoundId = round.Id,
            RoundName = round.Name,
            EventId = round.EventId,
            NextRoundId = nextRound?.Id,
            NextRoundName = nextRound?.Name,
            NextRoundLimitTeam = nextRound?.LimitTeam,
            TotalTeams = advancedTeams.Count,
            TotalAdvanced = advancedTeams.Count(x => x.IsAdvanced),
            Message = message,
            Teams = advancedTeams,
        };
    }

    /// <summary>
    /// Close an expired round: advance top teams to next round, close current round.
    /// This is the write variant used only by EndRoundJob (not by the read-only API).
    /// </summary>
    internal static async Task CloseAndAdvanceRoundAsync(AppDbContext dbContext, Repository.Entity.Rounds round, DateTimeOffset now)
    {
        var eventId = round.EventId;

        var nextRound = await dbContext.Rounds
            .Where(x => x.EventId == eventId && !x.IsDisable && x.RoundNo == round.RoundNo + 1)
            .FirstOrDefaultAsync();

        var roundDetails = await dbContext.RoundDetails
            .AsNoTracking()
            .Include(x => x.RegisterTeam).ThenInclude(x => x.Team)
            .Include(x => x.Submissions.Where(s => !s.IsDisable && s.Status == SubmissionStatusEnum.Submitted))
                .ThenInclude(s => s.Scores.Where(sc => !sc.IsDisable && !sc.IsMock))
            .Where(x => x.RoundId == round.Id && !x.IsDisable && !x.RegisterTeam.IsDisable && !x.RegisterTeam.Team.IsDisable
                && x.RegisterTeam.Status == RegisterTeamStatusEnum.Approved && !x.RegisterTeam.IsBanned)
            .ToListAsync();

        // Calculate team scores
        var teamScores = roundDetails
            .Select(rd =>
            {
                var latestSubmission = rd.Submissions
                    .OrderByDescending(s => s.SubmittedAt ?? s.CreatedAt)
                    .FirstOrDefault();

                decimal avgScore = 0;
                var hasScore = false;
                if (latestSubmission != null)
                {
                    var validScores = latestSubmission.Scores
                        .Where(s => s.TotalScore.HasValue)
                        .GroupBy(s => s.AssignTrackId)
                        .Select(g => g.OrderByDescending(s => s.CreatedAt).First().TotalScore!.Value)
                        .ToList();

                    if (validScores.Count != 0)
                    {
                        avgScore = validScores.Average();
                        hasScore = true;
                    }
                }

                return new { RoundDetail = rd, AverageScore = avgScore, HasScore = hasScore };
            })
            .OrderByDescending(x => x.HasScore)
            .ThenByDescending(x => x.AverageScore)
            .ThenBy(x => x.RoundDetail.RegisterTeam.Team.Name)
            .ToList();

        if (nextRound != null)
        {
            var limit = nextRound.LimitTeam ?? int.MaxValue;
            if (limit > 0)
            {
                var topTeams = teamScores
                    .Where(x => x.HasScore && x.AverageScore > 0)
                    .Take(limit)
                    .ToList();

                await using var transaction = await dbContext.Database.BeginTransactionAsync();

                var existingRegisterTeamIds = await dbContext.RoundDetails
                    .Where(x => x.RoundId == nextRound.Id)
                    .Select(x => x.RegisterTeamId)
                    .ToListAsync();

                var existingSet = new HashSet<Guid>(existingRegisterTeamIds);
                var nextRoundDetails = new List<Repository.Entity.RoundDetails>();

                foreach (var ts in topTeams)
                {
                    if (!existingSet.Contains(ts.RoundDetail.RegisterTeamId))
                    {
                        nextRoundDetails.Add(new Repository.Entity.RoundDetails
                        {
                            Id = Guid.NewGuid(),
                            RoundId = nextRound.Id,
                            RegisterTeamId = ts.RoundDetail.RegisterTeamId,
                            CreatedAt = now,
                            UpdatedAt = now
                        });
                    }
                }

                if (nextRoundDetails.Count != 0)
                {
                    await dbContext.RoundDetails.AddRangeAsync(nextRoundDetails);
                }

                round.IsDisable = true;
                round.UpdatedAt = now;
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return;
            }
        }

        // No next round or limit = 0: just close the round
        round.IsDisable = true;
        round.UpdatedAt = now;
        await dbContext.SaveChangesAsync();
    }
}