using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Judges;

public class Service : IService
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContext;

    public Service(AppDbContext dbContext, IHttpContextAccessor httpContext)
    {
        _dbContext = dbContext;
        _httpContext = httpContext;
    }

    public async Task<List<Response.JudgeTrackResponse>> GetMyTracks()
    {
        var userId = GetCurrentUserId();

        return await GetJudgeAssignmentsQuery(userId)
            .OrderBy(x => x.Track.Event.Name)
            .ThenBy(x => x.Track.Title)
            .Select(x => new Response.JudgeTrackResponse
            {
                AssignTrackId = x.Id,
                TrackId = x.TrackId,
                TrackTitle = x.Track.Title,
                TrackDescription = x.Track.Description,
                EventId = x.Track.EventId,
                EventName = x.Track.Event.Name,
                SubmissionCount = _dbContext.Submissions.Count(s =>
                    !s.IsDisable &&
                    !s.RoundDetail.IsDisable &&
                    !s.RoundDetail.RegisterTeam.IsDisable &&
                    !s.RoundDetail.RegisterTeam.IsBanned &&
                    s.RoundDetail.RegisterTeam.Status == RegisterTeamStatusEnum.Approved &&
                    s.RoundDetail.RegisterTeam.TrackId == x.TrackId),
                GradedSubmissionCount = _dbContext.Scores.Count(s =>
                    !s.IsDisable &&
                    !s.IsMock &&
                    s.AssignTrackId == x.Id)
            })
            .ToListAsync();
    }

    public async Task<List<Response.JudgeTrackSubmissionResponse>> GetTrackSubmissions(Guid trackId)
    {
        var userId = GetCurrentUserId();
        var assignTrackId = await EnsureJudgeAssignedToTrack(userId, trackId);

        return await _dbContext.Submissions
            .AsNoTracking()
            .Where(x =>
                !x.IsDisable &&
                !x.RoundDetail.IsDisable &&
                !x.RoundDetail.Round.IsDisable &&
                !x.RoundDetail.RegisterTeam.IsDisable &&
                !x.RoundDetail.RegisterTeam.IsBanned &&
                !x.RoundDetail.RegisterTeam.Team.IsDisable &&
                x.RoundDetail.RegisterTeam.Status == RegisterTeamStatusEnum.Approved &&
                x.RoundDetail.RegisterTeam.TrackId == trackId)
            .OrderByDescending(x => x.SubmittedAt)
            .ThenBy(x => x.RoundDetail.RegisterTeam.Team.Name)
            .Select(x => new Response.JudgeTrackSubmissionResponse
            {
                SubmissionId = x.Id,
                RoundDetailId = x.RoundDetailId,
                RoundId = x.RoundDetail.RoundId,
                RoundName = x.RoundDetail.Round.Name,
                TeamId = x.RoundDetail.RegisterTeam.TeamId,
                TeamName = x.RoundDetail.RegisterTeam.Team.Name,
                Url = x.Url,
                Description = x.Description,
                Status = x.Status,
                SubmittedAt = x.SubmittedAt,
                IsGraded = x.Scores.Any(s => !s.IsDisable && !s.IsMock && s.AssignTrackId == assignTrackId),
                ScoreId = x.Scores
                    .Where(s => !s.IsDisable && !s.IsMock && s.AssignTrackId == assignTrackId)
                    .OrderByDescending(s => s.UpdatedAt)
                    .Select(s => (Guid?)s.Id)
                    .FirstOrDefault(),
                TotalScore = x.Scores
                    .Where(s => !s.IsDisable && !s.IsMock && s.AssignTrackId == assignTrackId)
                    .OrderByDescending(s => s.UpdatedAt)
                    .Select(s => s.TotalScore)
                    .FirstOrDefault()
            })
            .ToListAsync();
    }

    public async Task<Response.SubmissionCriteriaResponse> GetSubmissionCriteria(Guid submissionId)
    {
        var userId = GetCurrentUserId();
        var submissionAccess = await EnsureJudgeCanAccessSubmission(userId, submissionId);

        var template = await _dbContext.CriteriaTemplates
            .AsNoTracking()
            .Where(x => !x.IsDisable && x.RoundId == submissionAccess.RoundId)
            .OrderBy(x => x.CreatedAt)
            .Select(x => new
            {
                x.Id,
                x.Title,
                Items = x.CriteriaItems
                    .Where(i => !i.IsDisable)
                    .OrderBy(i => i.CreatedAt)
                    .Select(i => new Response.CriteriaItemResponse
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Description = i.Description,
                        MaxScore = i.Score
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        return new Response.SubmissionCriteriaResponse
        {
            SubmissionId = submissionId,
            RoundId = submissionAccess.RoundId,
            TemplateId = template?.Id,
            TemplateTitle = template?.Title,
            CriteriaItems = template?.Items ?? []
        };
    }

    public async Task<Response.JudgeSubmissionScoreResponse?> GetMySubmissionScore(Guid submissionId)
    {
        var userId = GetCurrentUserId();
        var submissionAccess = await EnsureJudgeCanAccessSubmission(userId, submissionId);

        return await _dbContext.Scores
            .AsNoTracking()
            .Where(x =>
                !x.IsDisable &&
                !x.IsMock &&
                x.SubmissionId == submissionId &&
                x.AssignTrackId == submissionAccess.AssignTrackId)
            .OrderByDescending(x => x.UpdatedAt)
            .Select(x => new Response.JudgeSubmissionScoreResponse
            {
                ScoreId = x.Id,
                SubmissionId = x.SubmissionId,
                AssignTrackId = x.AssignTrackId,
                TotalScore = x.TotalScore,
                IsRetake = x.IsRetake,
                IsMock = x.IsMock,
                ScoreItems = x.ScoreItems
                    .Where(i => !i.IsDisable && i.AssignTrackId == submissionAccess.AssignTrackId)
                    .OrderBy(i => i.CriteriaItem.CreatedAt)
                    .Select(i => new Response.JudgeScoreItemResponse
                    {
                        CriteriaItemId = i.CriteriaItemId,
                        CriteriaItemName = i.CriteriaItem.Name,
                        Score = i.Score,
                        Comment = i.Comment
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Response.JudgeScoreDashboardResponse> GetMyScores()
    {
        var userId = GetCurrentUserId();
        var assignTrackIds = await GetJudgeAssignmentsQuery(userId)
            .Select(x => x.Id)
            .ToListAsync();

        if (assignTrackIds.Count == 0)
        {
            return new Response.JudgeScoreDashboardResponse();
        }

        var totalAssignedSubmissions = await _dbContext.Submissions
            .AsNoTracking()
            .CountAsync(x =>
                !x.IsDisable &&
                !x.RoundDetail.IsDisable &&
                !x.RoundDetail.Round.IsDisable &&
                !x.RoundDetail.RegisterTeam.IsDisable &&
                !x.RoundDetail.RegisterTeam.IsBanned &&
                !x.RoundDetail.RegisterTeam.Team.IsDisable &&
                x.RoundDetail.RegisterTeam.Status == RegisterTeamStatusEnum.Approved &&
                x.RoundDetail.RegisterTeam.TrackId.HasValue &&
                _dbContext.AssignTracks.Any(a =>
                    assignTrackIds.Contains(a.Id) &&
                    a.TrackId == x.RoundDetail.RegisterTeam.TrackId.Value));

        var totalGradedSubmissions = await _dbContext.Scores
            .AsNoTracking()
            .Where(x =>
                !x.IsDisable &&
                !x.IsMock &&
                assignTrackIds.Contains(x.AssignTrackId))
            .Select(x => x.SubmissionId)
            .Distinct()
            .CountAsync();

        var totalPendingSubmissions = Math.Max(totalAssignedSubmissions - totalGradedSubmissions, 0);
        var gradedPercentage = totalAssignedSubmissions == 0
            ? 0
            : Math.Round(totalGradedSubmissions * 100m / totalAssignedSubmissions, 2);

        return new Response.JudgeScoreDashboardResponse
        {
            TotalAssignedSubmissions = totalAssignedSubmissions,
            TotalGradedSubmissions = totalGradedSubmissions,
            TotalPendingSubmissions = totalPendingSubmissions,
            GradedPercentage = gradedPercentage
        };
    }

    public Task<Response.JudgeSubmissionScoreResponse> SubmitScore(Guid submissionId, Request.SubmitScoreRequest request)
    {
        return CreateScore(submissionId, request, isMock: false, isRetake: false, sourceScore: null);
    }

    public Task<Response.JudgeSubmissionScoreResponse> SubmitMockScore(Guid submissionId, Request.SubmitScoreRequest request)
    {
        return CreateScore(submissionId, request, isMock: true, isRetake: false, sourceScore: null);
    }

    public async Task<Response.JudgeSubmissionScoreResponse> UpdateScore(Guid scoreId, Request.SubmitScoreRequest request)
    {
        var userId = GetCurrentUserId();
        var score = await LoadOwnedScore(scoreId, userId);

        var submissionAccess = await EnsureJudgeCanAccessSubmission(userId, score.SubmissionId);
        var criteriaItems = await ValidateScoreRequest(request, submissionAccess.RoundId);
        var now = DateTimeOffset.UtcNow;

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            score.TotalScore = request.TotalScore;
            score.UpdatedAt = now;

            foreach (var item in score.ScoreItems.Where(x => !x.IsDisable))
            {
                item.IsDisable = true;
                item.UpdatedAt = now;
            }

            foreach (var itemRequest in request.Scores)
            {
                _dbContext.ScoreItems.Add(new ScoreItems
                {
                    Id = Guid.NewGuid(),
                    ScoreId = score.Id,
                    CriteriaItemId = itemRequest.CriteriaItemId,
                    AssignTrackId = score.AssignTrackId,
                    Score = itemRequest.Score,
                    Comment = itemRequest.Comment,
                    CreatedAt = now,
                    UpdatedAt = now,
                    IsDisable = false
                });
            }

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return await BuildScoreResponse(score.Id);
    }

    public async Task<string> FinalizeScore(Guid scoreId)
    {
        var userId = GetCurrentUserId();
        var score = await LoadOwnedScore(scoreId, userId);

        if (score.IsMock)
        {
            throw new BadRequestException("MOCK_SCORE_CANNOT_BE_FINALIZED");
        }

        if (!score.ScoreItems.Any(x => !x.IsDisable))
        {
            throw new BadRequestException("SCORE_ITEMS_REQUIRED");
        }

        return await Task.FromResult("SCORE_FINALIZED");
    }

    public async Task<Response.JudgeSubmissionScoreResponse> SubmitRetakeScore(Guid scoreId, Request.SubmitScoreRequest request)
    {
        var userId = GetCurrentUserId();
        var sourceScore = await LoadOwnedScore(scoreId, userId);

        if (sourceScore.IsMock)
        {
            throw new BadRequestException("MOCK_SCORE_CANNOT_BE_RETAKEN");
        }

        var hasRetake = await _dbContext.Scores.AnyAsync(x =>
            !x.IsDisable &&
            x.IsRetake &&
            x.SubmissionId == sourceScore.SubmissionId &&
            x.AssignTrackId == sourceScore.AssignTrackId);

        if (hasRetake)
        {
            throw new ConflictException("SCORE_ALREADY_RETAKEN");
        }

        return await CreateScore(sourceScore.SubmissionId, request, isMock: false, isRetake: true, sourceScore);
    }

    private async Task<Response.JudgeSubmissionScoreResponse> CreateScore(Guid submissionId, Request.SubmitScoreRequest request, bool isMock, bool isRetake, Scores? sourceScore)
    {
        var userId = GetCurrentUserId();
        var submissionAccess = await EnsureJudgeCanAccessSubmission(userId, submissionId);
        var criteriaItems = await ValidateScoreRequest(request, submissionAccess.RoundId);

        if (!isMock && !isRetake)
        {
            var scoreExists = await _dbContext.Scores.AnyAsync(x =>
                !x.IsDisable &&
                !x.IsMock &&
                !x.IsRetake &&
                x.SubmissionId == submissionId &&
                x.AssignTrackId == submissionAccess.AssignTrackId);

            if (scoreExists)
            {
                throw new ConflictException("SCORE_ALREADY_EXISTS");
            }
        }

        if (sourceScore != null && sourceScore.AssignTrackId != submissionAccess.AssignTrackId)
        {
            throw new ForbiddenException("SCORE_NOT_OWNED_BY_JUDGE");
        }

        var now = DateTimeOffset.UtcNow;
        var score = new Scores
        {
            Id = Guid.NewGuid(),
            SubmissionId = submissionId,
            AssignTrackId = submissionAccess.AssignTrackId,
            IsRetake = isRetake,
            TotalScore = request.TotalScore,
            IsMock = isMock,
            CreatedAt = now,
            UpdatedAt = now,
            IsDisable = false
        };

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _dbContext.Scores.AddAsync(score);

            foreach (var itemRequest in request.Scores)
            {
                await _dbContext.ScoreItems.AddAsync(new ScoreItems
                {
                    Id = Guid.NewGuid(),
                    ScoreId = score.Id,
                    CriteriaItemId = itemRequest.CriteriaItemId,
                    AssignTrackId = submissionAccess.AssignTrackId,
                    Score = itemRequest.Score,
                    Comment = itemRequest.Comment,
                    CreatedAt = now,
                    UpdatedAt = now,
                    IsDisable = false
                });
            }

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return await BuildScoreResponse(score.Id);
    }

    private async Task<List<CriteriaItems>> ValidateScoreRequest(Request.SubmitScoreRequest request, Guid roundId)
    {
        if (request.Scores.Count == 0)
        {
            throw new BadRequestException("SCORE_ITEMS_REQUIRED");
        }

        if (request.Scores.Any(x => x.CriteriaItemId == Guid.Empty))
        {
            throw new BadRequestException("CRITERIA_ITEM_REQUIRED");
        }

        if (request.Scores.Any(x => x.Score < 0))
        {
            throw new BadRequestException("SCORE_MUST_BE_NON_NEGATIVE");
        }

        if (request.Scores.Select(x => x.CriteriaItemId).Distinct().Count() != request.Scores.Count)
        {
            throw new BadRequestException("DUPLICATE_CRITERIA_ITEM");
        }

        var total = request.Scores.Sum(x => x.Score);
        if (total != request.TotalScore)
        {
            throw new BadRequestException("SCORE_TOTAL_MISMATCH");
        }

        var criteriaIds = request.Scores.Select(x => x.CriteriaItemId).ToList();
        var criteriaItems = await _dbContext.CriteriaItems
            .Include(x => x.CriteriaTemplate)
            .Where(x => criteriaIds.Contains(x.Id) && !x.IsDisable && !x.CriteriaTemplate.IsDisable)
            .ToListAsync();

        if (criteriaItems.Count != criteriaIds.Count)
        {
            throw new NotFoundException("CRITERIA_ITEM_NOT_FOUND");
        }

        if (criteriaItems.Any(x => x.CriteriaTemplate.RoundId != roundId))
        {
            throw new BadRequestException("CRITERIA_ITEM_NOT_IN_SUBMISSION_ROUND");
        }

        var scoreByCriteriaId = request.Scores.ToDictionary(x => x.CriteriaItemId, x => x.Score);
        if (criteriaItems.Any(x => scoreByCriteriaId[x.Id] > x.Score))
        {
            throw new BadRequestException("SCORE_LIMIT_EXCEEDED");
        }

        return criteriaItems;
    }

    private async Task<Scores> LoadOwnedScore(Guid scoreId, Guid userId)
    {
        var score = await _dbContext.Scores
            .Include(x => x.AssignTrack).ThenInclude(x => x.AssignEvent).ThenInclude(x => x.EventRole)
            .Include(x => x.Submission).ThenInclude(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam)
            .Include(x => x.ScoreItems).ThenInclude(x => x.CriteriaItem)
            .FirstOrDefaultAsync(x => x.Id == scoreId && !x.IsDisable);

        if (score == null)
        {
            throw new NotFoundException("SCORE_NOT_FOUND");
        }

        if (score.AssignTrack.AssignEvent.UserId != userId ||
            score.AssignTrack.AssignEvent.EventRole?.Name != EventRoleEnum.Judge)
        {
            throw new ForbiddenException("SCORE_NOT_OWNED_BY_JUDGE");
        }

        return score;
    }

    private async Task<Response.JudgeSubmissionScoreResponse> BuildScoreResponse(Guid scoreId)
    {
        var response = await _dbContext.Scores
            .AsNoTracking()
            .Where(x => x.Id == scoreId && !x.IsDisable)
            .Select(x => new Response.JudgeSubmissionScoreResponse
            {
                ScoreId = x.Id,
                SubmissionId = x.SubmissionId,
                AssignTrackId = x.AssignTrackId,
                TotalScore = x.TotalScore,
                IsRetake = x.IsRetake,
                IsMock = x.IsMock,
                ScoreItems = x.ScoreItems
                    .Where(i => !i.IsDisable)
                    .OrderBy(i => i.CriteriaItem.CreatedAt)
                    .Select(i => new Response.JudgeScoreItemResponse
                    {
                        CriteriaItemId = i.CriteriaItemId,
                        CriteriaItemName = i.CriteriaItem.Name,
                        Score = i.Score,
                        Comment = i.Comment
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        return response ?? throw new NotFoundException("SCORE_NOT_FOUND");
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

    private IQueryable<Hackathon.Repository.Entity.AssignTracks> GetJudgeAssignmentsQuery(Guid userId)
    {
        return _dbContext.AssignTracks
            .AsNoTracking()
            .Where(x =>
                !x.IsDisable &&
                !x.AssignEvent.IsDisable &&
                !x.AssignEvent.Event.IsDisable &&
                !x.Track.IsDisable &&
                !x.Track.Event.IsDisable &&
                x.AssignEvent.UserId == userId &&
                x.AssignEvent.EventRole != null &&
                x.AssignEvent.EventRole.Name == EventRoleEnum.Judge);
    }

    private async Task<Guid> EnsureJudgeAssignedToTrack(Guid userId, Guid trackId)
    {
        var trackExists = await _dbContext.Tracks
            .AsNoTracking()
            .AnyAsync(x => x.Id == trackId && !x.IsDisable);

        if (!trackExists)
        {
            throw new NotFoundException("TRACK_NOT_FOUND");
        }

        var assignTrackId = await GetJudgeAssignmentsQuery(userId)
            .Where(x => x.TrackId == trackId)
            .Select(x => (Guid?)x.Id)
            .FirstOrDefaultAsync();

        if (!assignTrackId.HasValue)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        return assignTrackId.Value;
    }

    private async Task<SubmissionAccess> EnsureJudgeCanAccessSubmission(Guid userId, Guid submissionId)
    {
        var submission = await _dbContext.Submissions
            .AsNoTracking()
            .Where(x => x.Id == submissionId && !x.IsDisable)
            .Select(x => new
            {
                x.Id,
                x.RoundDetail.RoundId,
                TrackId = x.RoundDetail.RegisterTeam.TrackId
            })
            .FirstOrDefaultAsync();

        if (submission == null)
        {
            throw new NotFoundException("SUBMISSION_NOT_FOUND");
        }

        if (!submission.TrackId.HasValue)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        var assignTrackId = await GetJudgeAssignmentsQuery(userId)
            .Where(x => x.TrackId == submission.TrackId.Value)
            .Select(x => (Guid?)x.Id)
            .FirstOrDefaultAsync();

        if (!assignTrackId.HasValue)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        return new SubmissionAccess(submission.RoundId, submission.TrackId.Value, assignTrackId.Value);
    }

    private sealed record SubmissionAccess(Guid RoundId, Guid TrackId, Guid AssignTrackId);
}
