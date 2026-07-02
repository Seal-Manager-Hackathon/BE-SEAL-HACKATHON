using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
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

    public async Task<BasePaginationResponse> GetTrackSubmissions(Guid trackId, Guid roundId, string? status, PaginationRequest paginationRequest)
    {
        var userId = GetCurrentUserId();
        var assignTrackId = await EnsureJudgeAssignedToTrack(userId, trackId);

        var pageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        var pageSize = paginationRequest.PageSize <= 0 ? 10 : Math.Min(paginationRequest.PageSize, 100);

        // Get active criteria template for this round to know total criteria items count
        var activeTemplate = await _dbContext.CriteriaTemplates
            .AsNoTracking()
            .Where(x => x.RoundId == roundId && !x.IsDisable)
            .Select(x => new
            {
                x.Id,
                CriteriaCount = x.CriteriaItems.Count(c => !c.IsDisable)
            })
            .FirstOrDefaultAsync();

        var totalCriteriaItems = activeTemplate?.CriteriaCount ?? 0;

        // Get approved register teams in this track that have RoundDetails for the specified round
        var registerTeamsQuery = _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team)
            .Where(x => x.TrackId == trackId
                        && x.Status == RegisterTeamStatusEnum.Approved
                        && !x.IsDisable
                        && !x.IsBanned
                        && !x.Team.IsDisable
                        && x.RoundDetails.Any(rd => rd.RoundId == roundId && !rd.IsDisable));

        var registerTeams = await registerTeamsQuery
            .Select(x => new
            {
                x.Id,
                x.TeamId,
                TeamName = x.Team.Name,
                RoundDetailId = x.RoundDetails
                    .Where(rd => rd.RoundId == roundId && !rd.IsDisable)
                    .Select(rd => rd.Id)
                    .FirstOrDefault()
            })
            .ToListAsync();

        if (registerTeams.Count == 0)
        {
            return ApiResponseFactory.BasePagination(new List<Response.JudgeTrackSubmissionResponse>(), pageIndex, pageSize, 0);
        }

        var roundDetailIds = registerTeams.Select(x => x.RoundDetailId).ToList();

        // Get latest submission per round detail with scores
        var latestSubmissions = await _dbContext.Submissions
            .AsNoTracking()
            .Include(x => x.Scores.Where(s => !s.IsDisable && !s.IsMock && s.AssignTrackId == assignTrackId))
            .ThenInclude(x => x.ScoreItems.Where(si => !si.IsDisable))
            .Where(x => roundDetailIds.Contains(x.RoundDetailId) && !x.IsDisable)
            .GroupBy(x => x.RoundDetailId)
            .Select(g => g.OrderByDescending(x => x.SubmittedAt ?? x.CreatedAt).First())
            .ToListAsync();

        var submissionLookup = latestSubmissions.ToDictionary(x => x.RoundDetailId);

        // Build items with grading status and score
        var items = registerTeams.Select(rt =>
        {
            submissionLookup.TryGetValue(rt.RoundDetailId, out var submission);

            string gradingStatus = "NoSubmission";
            Guid? scoreId = null;
            decimal? totalScore = null;

            if (submission != null)
            {
                var myScore = submission.Scores
                    .OrderByDescending(s => s.UpdatedAt)
                    .FirstOrDefault();

                if (myScore != null)
                {
                    scoreId = myScore.Id;
                    totalScore = myScore.TotalScore;

                    var scoredCriteriaCount = myScore.ScoreItems.Count(si => !si.IsDisable && si.Score.HasValue);
                    gradingStatus = (totalCriteriaItems > 0 && scoredCriteriaCount >= totalCriteriaItems) ? "Graded" : "Pending";
                }
                else
                {
                    gradingStatus = "Pending";
                }
            }

            return new Response.JudgeTrackSubmissionResponse
            {
                SubmissionId = submission?.Id,
                RoundDetailId = rt.RoundDetailId,
                RoundId = roundId,
                RoundName = "",
                TeamId = rt.TeamId,
                TeamName = rt.TeamName,
                Url = submission?.Url,
                Description = submission?.Description,
                Status = submission?.Status,
                SubmittedAt = submission?.SubmittedAt,
                GradingStatus = gradingStatus,
                ScoreId = scoreId,
                TotalScore = totalScore
            };
        }).ToList();

        // Filter by status
        if (!string.IsNullOrWhiteSpace(status) && !status.Equals("all", StringComparison.OrdinalIgnoreCase))
        {
            if (status.Equals("graded", StringComparison.OrdinalIgnoreCase))
            {
                items = items.Where(x => x.GradingStatus == "Graded").ToList();
            }
            else if (status.Equals("pending", StringComparison.OrdinalIgnoreCase))
            {
                items = items.Where(x => x.GradingStatus != "Graded").ToList();
            }
        }

        // Sort: pending (NoSubmission) first, then by SubmittedAt DESC
        items = items
            .OrderBy(x => x.GradingStatus == "Graded" ? 1 : 0)
            .ThenByDescending(x => x.SubmittedAt)
            .ToList();

        var totalCount = items.Count;
        var paged = items
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Get round name
        var round = await _dbContext.Rounds
            .AsNoTracking()
            .Where(x => x.Id == roundId)
            .Select(x => x.Name)
            .FirstOrDefaultAsync();

        foreach (var item in paged)
        {
            item.RoundName = round ?? "";
        }

        return ApiResponseFactory.BasePagination(paged, pageIndex, pageSize, totalCount);
    }

    public async Task<BasePaginationResponse> GetRegradeSubmissions(Guid? eventId, Guid? trackId, bool? isRegraded, PaginationRequest paginationRequest)
    {
        var userId = GetCurrentUserId();
        var pageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        var pageSize = paginationRequest.PageSize <= 0 ? 10 : Math.Min(paginationRequest.PageSize, 100);

        var query = _dbContext.Scores
            .AsNoTracking()
            .Where(x =>
                !x.IsDisable &&
                !x.IsMock &&
                !x.IsRetake &&
                x.AssignTrack.AssignEvent.UserId == userId &&
                x.AssignTrack.AssignEvent.EventRole != null &&
                x.AssignTrack.AssignEvent.EventRole.Name == EventRoleEnum.Judge &&
                x.Submission.IsRegrade &&
                !x.Submission.IsDisable &&
                x.Submission.Report != null &&
                !x.Submission.Report.IsDisable &&
                x.Submission.Report.Status == ReportStatusEnum.Approved);

        if (eventId.HasValue)
        {
            query = query.Where(x => x.Submission.RoundDetail.RegisterTeam.EventId == eventId.Value);
        }

        if (trackId.HasValue)
        {
            query = query.Where(x => x.Submission.RoundDetail.RegisterTeam.TrackId == trackId.Value);
        }

        if (isRegraded.HasValue)
        {
            query = isRegraded.Value
                ? query.Where(x => x.RetakeScores.Any(r => !r.IsDisable && r.IsRetake))
                : query.Where(x => !x.RetakeScores.Any(r => !r.IsDisable && r.IsRetake));
        }
        else
        {
            query = query.Where(x => !x.RetakeScores.Any(r => !r.IsDisable && r.IsRetake));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.Submission.Report!.UpdatedAt)
            .ThenByDescending(x => x.Submission.SubmittedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.JudgeRegradeSubmissionResponse
            {
                SubmissionId = x.SubmissionId,
                RoundDetailId = x.Submission.RoundDetailId,
                RoundName = x.Submission.RoundDetail.Round.Name,
                TeamId = x.Submission.RoundDetail.RegisterTeam.TeamId,
                TeamName = x.Submission.RoundDetail.RegisterTeam.Team.Name,
                EventName = x.Submission.RoundDetail.RegisterTeam.Event.Name,
                TrackTitle = x.Submission.RoundDetail.RegisterTeam.Track != null ? x.Submission.RoundDetail.RegisterTeam.Track.Title : null,
                Url = x.Submission.Url,
                Description = x.Submission.Description,
                ReportId = x.Submission.Report!.Id,
                ReportTitle = x.Submission.Report.Title,
                SourceScoreId = x.Id,
                SourceTotalScore = x.TotalScore,
                IsRegraded = x.RetakeScores.Any(r => !r.IsDisable && r.IsRetake),
                RegradeScoreId = x.RetakeScores
                    .Where(r => !r.IsDisable && r.IsRetake)
                    .OrderByDescending(r => r.UpdatedAt)
                    .Select(r => (Guid?)r.Id)
                    .FirstOrDefault(),
                RegradeTotalScore = x.RetakeScores
                    .Where(r => !r.IsDisable && r.IsRetake)
                    .OrderByDescending(r => r.UpdatedAt)
                    .Select(r => r.TotalScore)
                    .FirstOrDefault(),
                ApprovedAt = x.Submission.Report.UpdatedAt
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
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
                RetakeFromScoreId = x.RetakeFromScoreId,
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

    public async Task<BasePaginationResponse> GetMyScores(Guid eventId, Guid? trackId, bool? isGraded, PaginationRequest paginationRequest)
    {
        var userId = GetCurrentUserId();
        var assignTrackIds = await GetJudgeAssignmentsQuery(userId)
            .Where(x => x.Track.EventId == eventId)
            .Select(x => x.Id)
            .ToListAsync();

        if (trackId.HasValue)
        {
            assignTrackIds = assignTrackIds.Intersect(
                await GetJudgeAssignmentsQuery(userId)
                    .Where(x => x.TrackId == trackId.Value)
                    .Select(x => x.Id)
                    .ToListAsync()
            ).ToList();
        }

        var pageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        var pageSize = paginationRequest.PageSize <= 0 ? 10 : Math.Min(paginationRequest.PageSize, 100);

        if (assignTrackIds.Count == 0)
        {
            return ApiResponseFactory.BasePagination(new List<object>(), pageIndex, pageSize, 0);
        }

        var query = _dbContext.Scores
            .AsNoTracking()
            .Include(x => x.Submission).ThenInclude(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Team)
            .Include(x => x.Submission).ThenInclude(x => x.RoundDetail).ThenInclude(x => x.Round)
            .Include(x => x.AssignTrack).ThenInclude(x => x.Track)
            .Where(x =>
                !x.IsDisable &&
                !x.IsMock &&
                assignTrackIds.Contains(x.AssignTrackId));

        if (isGraded == false)
        {
            query = query.Where(x => !x.ScoreItems.Any(si => !si.IsDisable));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.UpdatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.JudgeMyScoreItemResponse
            {
                ScoreId = x.Id,
                SubmissionId = x.SubmissionId,
                TrackId = x.AssignTrack.TrackId,
                TrackTitle = x.AssignTrack.Track.Title,
                TeamId = x.Submission.RoundDetail.RegisterTeam.TeamId,
                TeamName = x.Submission.RoundDetail.RegisterTeam.Team.Name,
                TotalScore = x.TotalScore,
                IsRetake = x.IsRetake,
                IsMock = x.IsMock,
                SubmittedAt = x.Submission.SubmittedAt,
                UpdatedAt = x.UpdatedAt
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
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

        if (sourceScore.IsRetake)
        {
            throw new BadRequestException("RETAKE_SCORE_CANNOT_BE_RETAKEN");
        }

        if (!sourceScore.Submission.IsRegrade)
        {
            throw new BadRequestException("SUBMISSION_NOT_IN_REGRADE");
        }

        var reportApproved = await _dbContext.Reports.AnyAsync(x =>
            !x.IsDisable &&
            x.SubmissionId == sourceScore.SubmissionId &&
            x.Status == ReportStatusEnum.Approved);

        if (!reportApproved)
        {
            throw new BadRequestException("REPORT_NOT_APPROVED");
        }

        var hasRetake = await _dbContext.Scores.AnyAsync(x =>
            !x.IsDisable &&
            x.IsRetake &&
            x.RetakeFromScoreId == sourceScore.Id);

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
            RetakeFromScoreId = sourceScore?.Id,
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
                RetakeFromScoreId = x.RetakeFromScoreId,
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

    public async Task<(List<Response.JudgeTrackTeamResponse> Data, string Message)> GetJudgeTeamsByEvent(Guid eventId, Guid? roundId)
    {
        var userId = GetCurrentUserId();

        // Get all tracks assigned to this judge in this event
        var assignedTracks = await GetJudgeAssignmentsQuery(userId)
            .Where(x => x.Track.EventId == eventId)
            .Select(x => new
            {
                x.Id,
                x.TrackId,
                TrackTitle = x.Track.Title
            })
            .ToListAsync();

        if (assignedTracks.Count == 0)
        {
            return (new List<Response.JudgeTrackTeamResponse>(), "NO_TEAMS_FOUND");
        }

        var assignTrackIds = assignedTracks.Select(x => x.Id).ToList();
        var trackLookup = assignedTracks.ToDictionary(x => x.Id, x => new { x.TrackId, x.TrackTitle });

        // Get all approved register teams in these tracks
        var registerTeams = await _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team)
            .Include(x => x.Topic)
            .Where(x => x.EventId == eventId
                        && x.Status == RegisterTeamStatusEnum.Approved
                        && !x.IsDisable
                        && !x.IsBanned
                        && !x.Team.IsDisable
                        && x.TrackId.HasValue
                        && assignedTracks.Select(a => a.TrackId).Contains(x.TrackId.Value))
            .ToListAsync();

        if (registerTeams.Count == 0)
        {
            return (new List<Response.JudgeTrackTeamResponse>(), "NO_TEAMS_FOUND");
        }

        var registerTeamIds = registerTeams.Select(x => x.Id).ToList();

        // Get RoundDetails for these teams — optionally filtered by round
        var roundDetailsQuery = _dbContext.RoundDetails
            .AsNoTracking()
            .Where(x => registerTeamIds.Contains(x.RegisterTeamId)
                        && !x.IsDisable
                        && !x.Round.IsDisable
                        && x.Round.EventId == eventId);

        if (roundId.HasValue)
        {
            roundDetailsQuery = roundDetailsQuery.Where(x => x.RoundId == roundId.Value);
        }

        var roundDetails = await roundDetailsQuery
            .Select(x => new { x.Id, x.RegisterTeamId, x.RoundId })
            .ToListAsync();

        if (roundDetails.Count == 0)
        {
            return (new List<Response.JudgeTrackTeamResponse>(), "NO_TEAMS_FOUND");
        }

        var roundDetailIds = roundDetails.Select(x => x.Id).ToList();
        var roundDetailTeamLookup = roundDetails
            .GroupBy(x => x.RegisterTeamId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Id).ToList());

        // Get the latest submission per RoundDetail (per team per round)
        var submissions = await _dbContext.Submissions
            .AsNoTracking()
            .Where(x => roundDetailIds.Contains(x.RoundDetailId) && !x.IsDisable)
            .ToListAsync();

        // For each register team, find the latest submission across its round details
        var latestSubmissionPerTeam = new Dictionary<Guid, (Hackathon.Repository.Entity.Submissions? Submission, bool IsGraded)>();

        foreach (var rt in registerTeams)
        {
            if (!roundDetailTeamLookup.TryGetValue(rt.Id, out var rtRoundDetailIds))
                continue;

            var teamSubmissions = submissions.Where(s => rtRoundDetailIds.Contains(s.RoundDetailId)).ToList();
            if (teamSubmissions.Count == 0)
                continue;

            var latest = teamSubmissions
                .Where(s => s.SubmittedAt.HasValue)
                .OrderByDescending(s => s.SubmittedAt)
                .FirstOrDefault()
                ?? teamSubmissions.First();

            // Check if graded by this judge (any assign track)
            var isGraded = await _dbContext.Scores
                .AsNoTracking()
                .AnyAsync(x => !x.IsDisable
                               && !x.IsMock
                               && x.SubmissionId == latest.Id
                               && assignTrackIds.Contains(x.AssignTrackId));

            latestSubmissionPerTeam[rt.Id] = (latest, isGraded);
        }

        // Build response grouped by track
        var result = assignedTracks.Select(track =>
        {
            var trackTeams = registerTeams
                .Where(rt => rt.TrackId == track.TrackId
                             && latestSubmissionPerTeam.ContainsKey(rt.Id))
                .Select(rt =>
                {
                    var (submission, isGraded) = latestSubmissionPerTeam[rt.Id];
                    return new Response.JudgeTeamSubmissionInfo
                    {
                        RegisterTeamId = rt.Id,
                        TeamId = rt.TeamId,
                        TeamName = rt.Team.Name,
                        TopicId = rt.TopicId,
                        TopicTitle = rt.Topic?.Title,
                        SubmissionId = submission?.Id,
                        SubmissionStatus = submission?.Status,
                        SubmittedAt = submission?.SubmittedAt,
                        IsGraded = isGraded,
                    };
                })
                .OrderBy(x => x.TeamName)
                .ToList();

            return new Response.JudgeTrackTeamResponse
            {
                TrackId = track.TrackId,
                TrackTitle = track.TrackTitle,
                Teams = trackTeams,
            };
        })
        .Where(x => x.Teams.Count > 0)
        .ToList();

        return (result, result.Count == 0 ? "NO_TEAMS_FOUND" : "SUCCESS");
    }

    public async Task<BaseResponse> GetEventSubmissions(Guid eventId, Guid? trackId, Guid? roundId, PaginationRequest paginationRequest)
    {
        var userId = GetCurrentUserId();
        var now = DateTimeOffset.UtcNow;
        var pageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        var pageSize = paginationRequest.PageSize <= 0 ? 10 : Math.Min(paginationRequest.PageSize, 100);

        var assignmentsQuery = GetJudgeAssignmentsQuery(userId)
            .Where(x => x.Track.EventId == eventId);

        if (trackId.HasValue)
        {
            assignmentsQuery = assignmentsQuery.Where(x => x.TrackId == trackId.Value);
        }

        var assignments = await assignmentsQuery
            .Select(x => new
            {
                AssignTrackId = x.Id,
                x.TrackId,
                TrackTitle = x.Track.Title,
            })
            .ToListAsync();

        if (assignments.Count == 0)
        {
            return ApiResponseFactory.Base(new List<Response.JudgeEventRoundSubmissionsResponse>(), 200, "SUCCESS");
        }

        var assignTrackIds = assignments.Select(x => x.AssignTrackId).ToList();
        var trackIds = assignments.Select(x => x.TrackId).Distinct().ToList();

        var submissionsQuery = _dbContext.Submissions
            .AsNoTracking()
            .Include(x => x.Scores.Where(s => !s.IsDisable && !s.IsMock && assignTrackIds.Contains(s.AssignTrackId)))
            .Include(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Team)
            .Include(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Track)
            .Include(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Topic)
            .Include(x => x.RoundDetail).ThenInclude(x => x.Round)
            .Where(x => !x.IsDisable
                        && x.RoundDetail.Round.EventId == eventId
                        && x.RoundDetail.Round.EndSubmission.HasValue
                        && x.RoundDetail.Round.EndSubmission.Value <= now
                        && x.RoundDetail.RegisterTeam.TrackId.HasValue
                        && trackIds.Contains(x.RoundDetail.RegisterTeam.TrackId.Value)
                        && x.RoundDetail.RegisterTeam.Status == RegisterTeamStatusEnum.Approved
                        && !x.RoundDetail.RegisterTeam.IsDisable
                        && !x.RoundDetail.RegisterTeam.IsBanned
                        && !x.RoundDetail.RegisterTeam.Team.IsDisable);

        if (roundId.HasValue)
        {
            submissionsQuery = submissionsQuery.Where(x => x.RoundDetail.RoundId == roundId.Value);
        }

        var submissions = await submissionsQuery
            .OrderByDescending(x => x.SubmittedAt)
            .ToListAsync();

        // Judge only sees the LATEST submission per team per round
        var latestPerTeamPerRound = submissions
            .GroupBy(x => new { x.RoundDetail.RegisterTeamId, x.RoundDetail.RoundId })
            .Select(g => g.First())
            .OrderByDescending(x => x.SubmittedAt)
            .ToList();

        var result = latestPerTeamPerRound
            .Select(x => new
            {
                RoundId = x.RoundDetail.RoundId,
                RoundName = x.RoundDetail.Round.Name,
                TrackId = x.RoundDetail.RegisterTeam.TrackId!.Value,
                TrackTitle = x.RoundDetail.RegisterTeam.Track!.Title,
                Submission = new Response.JudgeStatusSubmissionResponse
                {
                    RegisterTeamId = x.RoundDetail.RegisterTeamId,
                    TeamId = x.RoundDetail.RegisterTeam.TeamId,
                    TeamName = x.RoundDetail.RegisterTeam.Team.Name,
                    TopicId = x.RoundDetail.RegisterTeam.TopicId,
                    TopicTitle = x.RoundDetail.RegisterTeam.Topic != null ? x.RoundDetail.RegisterTeam.Topic.Title : null,
                    SubmissionId = x.Id,
                    SubmissionStatus = x.Status,
                    SubmittedAt = x.SubmittedAt,
                    ScoreId = x.Scores
                        .Where(s => !s.IsDisable && !s.IsMock && assignTrackIds.Contains(s.AssignTrackId))
                        .OrderByDescending(s => s.UpdatedAt)
                        .Select(s => (Guid?)s.Id)
                        .FirstOrDefault(),
                    TotalScore = x.Scores
                        .Where(s => !s.IsDisable && !s.IsMock && assignTrackIds.Contains(s.AssignTrackId))
                        .OrderByDescending(s => s.UpdatedAt)
                        .Select(s => s.TotalScore)
                        .FirstOrDefault()
                }
            })
            .GroupBy(x => new { x.RoundId, x.RoundName })
            .OrderBy(x => x.Key.RoundName)
            .Select(roundGroup => new Response.JudgeEventRoundSubmissionsResponse
            {
                RoundId = roundGroup.Key.RoundId,
                RoundName = roundGroup.Key.RoundName,
                Tracks = roundGroup
                    .GroupBy(x => new { x.TrackId, x.TrackTitle })
                    .OrderBy(x => x.Key.TrackTitle)
                    .Select(trackGroup =>
                    {
                        var totalCount = trackGroup.Count();
                        var items = trackGroup
                            .Select(x => x.Submission)
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .Cast<object>()
                            .ToList();

                        return new Response.JudgeEventTrackSubmissionsResponse
                        {
                            TrackId = trackGroup.Key.TrackId,
                            TrackTitle = trackGroup.Key.TrackTitle,
                            Submissions = new PaginationValue
                            {
                                Items = items,
                                PageIndex = pageIndex,
                                PageSize = pageSize,
                                TotalCount = totalCount
                            }
                        };
                    })
                    .ToList()
            })
            .ToList();

        return ApiResponseFactory.Base(result, 200, "SUCCESS");
    }

    public async Task<BasePaginationResponse> GetPendingSubmissions(Guid eventId, Guid? trackId, Guid? roundId, bool? isGraded, PaginationRequest paginationRequest)
    {
        var userId = GetCurrentUserId();
        var assignTrackIds = await GetJudgeAssignmentsQuery(userId)
            .Where(x => x.Track.EventId == eventId)
            .Select(x => x.Id)
            .ToListAsync();

        if (trackId.HasValue)
        {
            assignTrackIds = assignTrackIds.Intersect(
                await GetJudgeAssignmentsQuery(userId)
                    .Where(x => x.TrackId == trackId.Value)
                    .Select(x => x.Id)
                    .ToListAsync()
            ).ToList();
        }

        return await BuildSubmissionsResponse(eventId, roundId, assignTrackIds, isGraded ?? false, paginationRequest);
    }

    public async Task<BasePaginationResponse> GetCurrentEventPendingSubmissions(Guid? trackId, Guid? roundId, PaginationRequest paginationRequest)
    {
        var userId = GetCurrentUserId();
        var now = DateTimeOffset.UtcNow;

        // Find current event judge is assigned to
        var currentEventId = await _dbContext.AssignEvents
            .AsNoTracking()
            .Where(x => x.UserId == userId
                        && !x.IsDisable
                        && !x.Event.IsDisable
                        && x.EventRole != null
                        && x.EventRole.Name == EventRoleEnum.Judge
                        && x.Event.StartTime.HasValue
                        && x.Event.StartTime.Value <= now
                        && x.Event.EndTime.HasValue
                        && x.Event.EndTime.Value >= now)
            .Select(x => (Guid?)x.EventId)
            .FirstOrDefaultAsync();

        if (!currentEventId.HasValue)
        {
            var pageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
            var pageSize = paginationRequest.PageSize <= 0 ? 10 : Math.Min(paginationRequest.PageSize, 100);
            return ApiResponseFactory.BasePagination(new List<object>(), pageIndex, pageSize, 0);
        }

        var assignTrackIds = await GetJudgeAssignmentsQuery(userId)
            .Where(x => x.Track.EventId == currentEventId.Value)
            .Select(x => x.Id)
            .ToListAsync();

        if (trackId.HasValue)
        {
            assignTrackIds = assignTrackIds.Intersect(
                await GetJudgeAssignmentsQuery(userId)
                    .Where(x => x.TrackId == trackId.Value)
                    .Select(x => x.Id)
                    .ToListAsync()
            ).ToList();
        }

        return await BuildSubmissionsResponse(currentEventId.Value, roundId, assignTrackIds, false, paginationRequest);
    }

    public async Task<BasePaginationResponse> SearchSubmissions(Guid eventId, Guid? trackId, string? keyword, PaginationRequest paginationRequest)
    {
        var userId = GetCurrentUserId();
        var pageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        var pageSize = paginationRequest.PageSize <= 0 ? 10 : Math.Min(paginationRequest.PageSize, 100);

        // Get judge's assigned tracks
        var trackIds = await GetJudgeAssignmentsQuery(userId)
            .Where(x => x.Track.EventId == eventId)
            .Select(x => x.TrackId)
            .Distinct()
            .ToListAsync();

        if (trackId.HasValue)
        {
            if (!trackIds.Contains(trackId.Value))
                throw new ForbiddenException("FORBIDDEN");
            trackIds = new List<Guid> { trackId.Value };
        }

        if (trackIds.Count == 0)
        {
            return ApiResponseFactory.BasePagination(new List<object>(), pageIndex, pageSize, 0);
        }

        var query = _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team)
            .Include(x => x.Topic)
            .Where(x => x.EventId == eventId
                        && x.Status == RegisterTeamStatusEnum.Approved
                        && !x.IsDisable
                        && !x.IsBanned
                        && !x.Team.IsDisable
                        && x.TrackId.HasValue
                        && trackIds.Contains(x.TrackId.Value));

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var normalizedKeyword = keyword.Trim().ToLower();
            query = query.Where(x => x.Team.Name.ToLower().Contains(normalizedKeyword));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Team.Name)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.JudgeStatusSubmissionResponse
            {
                RegisterTeamId = x.Id,
                TeamId = x.TeamId,
                TeamName = x.Team.Name,
                TopicId = x.TopicId,
                TopicTitle = x.Topic != null ? x.Topic.Title : null,
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
    }

    private async Task<BasePaginationResponse> BuildSubmissionsResponse(Guid eventId, Guid? roundId, List<Guid> assignTrackIds, bool? isGraded, PaginationRequest paginationRequest)
    {
        var pageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        var pageSize = paginationRequest.PageSize <= 0 ? 10 : Math.Min(paginationRequest.PageSize, 100);

        if (assignTrackIds.Count == 0)
        {
            return ApiResponseFactory.BasePagination(new List<object>(), pageIndex, pageSize, 0);
        }

        var registerTeamsQuery = _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team)
            .Include(x => x.Topic)
            .Where(x => x.EventId == eventId
                        && x.Status == RegisterTeamStatusEnum.Approved
                        && !x.IsDisable
                        && !x.IsBanned
                        && !x.Team.IsDisable
                        && x.TrackId.HasValue
                        && _dbContext.AssignTracks.Any(at =>
                            assignTrackIds.Contains(at.Id) &&
                            at.TrackId == x.TrackId.Value));

        // Filter by round if provided
        if (roundId.HasValue)
        {
            registerTeamsQuery = registerTeamsQuery.Where(x =>
                _dbContext.RoundDetails.Any(rd =>
                    rd.RegisterTeamId == x.Id &&
                    rd.RoundId == roundId.Value &&
                    !rd.IsDisable));
        }

        var teamIds = await registerTeamsQuery.Select(x => x.Id).ToListAsync();

        if (teamIds.Count == 0)
        {
            return ApiResponseFactory.BasePagination(new List<object>(), pageIndex, pageSize, 0);
        }

        // Get submissions for these teams
        var submissionsQuery = _dbContext.Submissions
            .AsNoTracking()
            .Include(x => x.Scores.Where(s => !s.IsDisable && !s.IsMock && assignTrackIds.Contains(s.AssignTrackId)))
            .Include(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Team)
            .Include(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Track)
            .Include(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Topic)
            .Include(x => x.RoundDetail).ThenInclude(x => x.Round)
            .Where(x => !x.IsDisable
                        && x.RoundDetail.RegisterTeam.EventId == eventId
                        && teamIds.Contains(x.RoundDetail.RegisterTeamId));

        if (roundId.HasValue)
        {
            submissionsQuery = submissionsQuery.Where(x => x.RoundDetail.RoundId == roundId.Value);
        }

        // Filter by graded status
        if (isGraded.HasValue)
        {
            if (isGraded.Value)
            {
                submissionsQuery = submissionsQuery.Where(x =>
                    x.Scores.Any(s => !s.IsDisable && !s.IsMock && assignTrackIds.Contains(s.AssignTrackId)));
            }
            else
            {
                submissionsQuery = submissionsQuery.Where(x =>
                    !x.Scores.Any(s => !s.IsDisable && !s.IsMock && assignTrackIds.Contains(s.AssignTrackId)));
            }
        }

        var totalCount = await submissionsQuery.CountAsync();

        // Judge only sees latest submission per team per round
        var allSubmissions = await submissionsQuery
            .OrderByDescending(x => x.SubmittedAt)
            .ToListAsync();

        var latestGrouped = allSubmissions
            .GroupBy(x => x.RoundDetail.RegisterTeamId)
            .Select(g => g.OrderByDescending(x => x.SubmittedAt).First())
            .ToList();

        var items = latestGrouped.Select(x => new Response.JudgeStatusSubmissionResponse
        {
            RegisterTeamId = x.RoundDetail.RegisterTeamId,
            TeamId = x.RoundDetail.RegisterTeam.TeamId,
            TeamName = x.RoundDetail.RegisterTeam.Team.Name,
            TopicId = x.RoundDetail.RegisterTeam.TopicId,
            TopicTitle = x.RoundDetail.RegisterTeam.Topic != null ? x.RoundDetail.RegisterTeam.Topic.Title : null,
            SubmissionId = x.Id,
            SubmissionStatus = x.Status,
            SubmittedAt = x.SubmittedAt,
            ScoreId = x.Scores
                .Where(s => !s.IsDisable && !s.IsMock && assignTrackIds.Contains(s.AssignTrackId))
                .OrderByDescending(s => s.UpdatedAt)
                .Select(s => (Guid?)s.Id)
                .FirstOrDefault(),
            TotalScore = x.Scores
                .Where(s => !s.IsDisable && !s.IsMock && assignTrackIds.Contains(s.AssignTrackId))
                .OrderByDescending(s => s.UpdatedAt)
                .Select(s => s.TotalScore)
                .FirstOrDefault()
        }).ToList();

        var totalCountAfter = items.Count;
        var paged = items
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return ApiResponseFactory.BasePagination(paged, pageIndex, pageSize, totalCountAfter);
    }

    public async Task<BasePaginationResponse> GetJudgeRoundTeams(Guid eventId, Guid roundId, Guid? trackId, string? status, PaginationRequest paginationRequest)
    {
        var userId = GetCurrentUserId();
        var pageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        var pageSize = paginationRequest.PageSize <= 0 ? 10 : Math.Min(paginationRequest.PageSize, 100);

        // Get all tracks assigned to this judge in this event
        var assignedTracks = await GetJudgeAssignmentsQuery(userId)
            .Where(x => x.Track.EventId == eventId)
            .Select(x => new { x.Id, x.TrackId, TrackTitle = x.Track.Title })
            .ToListAsync();

        if (assignedTracks.Count == 0)
        {
            return ApiResponseFactory.BasePagination(new List<object>(), pageIndex, pageSize, 0);
        }

        var assignTrackIds = assignedTracks.Select(x => x.Id).ToList();
        var trackIds = assignedTracks.Select(x => x.TrackId).ToHashSet();
        var trackLookup = assignedTracks.ToDictionary(x => x.TrackId, x => x.TrackTitle);

        // Filter by trackId if provided
        if (trackId.HasValue)
        {
            if (!trackIds.Contains(trackId.Value))
                return ApiResponseFactory.BasePagination(new List<object>(), pageIndex, pageSize, 0);
            trackIds = [trackId.Value];
        }

        // Get active criteria template for this round to know total criteria items count
        var activeTemplate = await _dbContext.CriteriaTemplates
            .AsNoTracking()
            .Where(x => x.RoundId == roundId && x.IsDisable)
            .Select(x => new
            {
                x.Id,
                CriteriaCount = x.CriteriaItems.Count(c => !c.IsDisable)
            })
            .FirstOrDefaultAsync();

        var totalCriteriaItems = activeTemplate?.CriteriaCount ?? 0;

        // Get register teams in judge's tracks that have RoundDetails for this round
        var registerTeamsQuery = _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team)
            .Include(x => x.Topic)
            .Where(x => x.EventId == eventId
                        && x.Status == RegisterTeamStatusEnum.Approved
                        && !x.IsDisable
                        && !x.IsBanned
                        && !x.Team.IsDisable
                        && x.TrackId.HasValue
                        && trackIds.Contains(x.TrackId.Value)
                        && _dbContext.RoundDetails.Any(rd =>
                            rd.RegisterTeamId == x.Id
                            && rd.RoundId == roundId
                            && !rd.IsDisable));

        // Get submissions for these register teams in this round
        var registerTeamIds = await registerTeamsQuery.Select(x => x.Id).ToListAsync();

        if (registerTeamIds.Count == 0)
        {
            return ApiResponseFactory.BasePagination(new List<object>(), pageIndex, pageSize, 0);
        }

        var roundDetailIds = await _dbContext.RoundDetails
            .AsNoTracking()
            .Where(x => registerTeamIds.Contains(x.RegisterTeamId) && x.RoundId == roundId && !x.IsDisable)
            .Select(x => new { x.Id, x.RegisterTeamId })
            .ToListAsync();

        var rdLookup = roundDetailIds.ToDictionary(x => x.RegisterTeamId, x => x.Id);

        // Get latest submission per round detail
        var submissions = await _dbContext.Submissions
            .AsNoTracking()
            .Include(x => x.Scores.Where(s => !s.IsDisable && !s.IsMock && assignTrackIds.Contains(s.AssignTrackId)))
            .Where(x => roundDetailIds.Select(r => r.Id).Contains(x.RoundDetailId) && !x.IsDisable)
            .ToListAsync();

        var latestSubmissions = submissions
            .GroupBy(x => x.RoundDetailId)
            .Select(g => g.OrderByDescending(x => x.SubmittedAt ?? x.CreatedAt).First())
            .ToDictionary(x => x.RoundDetailId);

        // Get register team details for response
        var registerTeams = await registerTeamsQuery.ToListAsync();

        var items = new List<Response.JudgeRoundTeamResponse>();
        foreach (var rt in registerTeams)
        {
            var hasRoundDetail = rdLookup.TryGetValue(rt.Id, out var rdId);
            if (!hasRoundDetail) continue;

            latestSubmissions.TryGetValue(rdId, out var submission);

            // Determine grading status for THIS judge
            string gradingStatus = "Pending";
            decimal? totalScore = null;
            Guid? scoreId = null;

            if (submission != null)
            {
                var myScore = submission.Scores
                    .Where(s => assignTrackIds.Contains(s.AssignTrackId))
                    .OrderByDescending(s => s.UpdatedAt)
                    .FirstOrDefault();

                if (myScore != null)
                {
                    scoreId = myScore.Id;
                    totalScore = myScore.TotalScore;

                    var scoredCriteriaCount = myScore.ScoreItems.Count(si => !si.IsDisable && si.Score.HasValue);
                    gradingStatus = (totalCriteriaItems > 0 && scoredCriteriaCount >= totalCriteriaItems) ? "Graded" : "Pending";
                }
            }

            items.Add(new Response.JudgeRoundTeamResponse
            {
                RegisterTeamId = rt.Id,
                TeamId = rt.TeamId,
                TeamName = rt.Team.Name,
                TrackId = rt.TrackId,
                TrackTitle = rt.TrackId.HasValue && trackLookup.TryGetValue(rt.TrackId.Value, out var tt) ? tt : null,
                TopicId = rt.TopicId,
                TopicTitle = rt.Topic?.Title,
                SubmissionId = submission?.Id,
                SubmissionStatus = submission?.Status,
                SubmittedAt = submission?.SubmittedAt,
                GradingStatus = gradingStatus,
                TotalScore = totalScore,
            });
        }

        // Filter by status
        if (!string.IsNullOrWhiteSpace(status) && !status.Equals("all", StringComparison.OrdinalIgnoreCase))
        {
            var isGraded = status.Equals("graded", StringComparison.OrdinalIgnoreCase);
            items = items.Where(x => isGraded
                ? x.GradingStatus == "Graded"
                : x.GradingStatus == "Pending").ToList();
        }

        // Sort & paginate
        var sorted = items
            .OrderByDescending(x => x.SubmittedAt)
            .ThenBy(x => x.TeamName)
            .ToList();

        var totalCount = sorted.Count;
        var paged = sorted
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return ApiResponseFactory.BasePagination(paged, pageIndex, pageSize, totalCount);
    }

    public async Task<BasePaginationResponse> GetJudgeTeamSubmissions(Guid registerTeamId, PaginationRequest paginationRequest)
    {
        var userId = GetCurrentUserId();
        var pageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        var pageSize = paginationRequest.PageSize <= 0 ? 10 : Math.Min(paginationRequest.PageSize, 100);

        // Get judge's assigned tracks
        var assignedTracks = await GetJudgeAssignmentsQuery(userId)
            .Select(x => new { x.Id, x.TrackId })
            .ToListAsync();

        if (assignedTracks.Count == 0)
        {
            return ApiResponseFactory.BasePagination(new List<object>(), pageIndex, pageSize, 0);
        }

        var assignTrackIds = assignedTracks.Select(x => x.Id).ToList();
        var trackIds = assignedTracks.Select(x => x.TrackId).ToHashSet();

        // Verify register team exists and belongs to judge's track
        var registerTeam = await _dbContext.RegisterTeams
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == registerTeamId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        if (!registerTeam.TrackId.HasValue || !trackIds.Contains(registerTeam.TrackId.Value))
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        // Get all round details for this register team
        var roundDetails = await _dbContext.RoundDetails
            .AsNoTracking()
            .Include(x => x.Round)
            .Where(x => x.RegisterTeamId == registerTeamId && !x.IsDisable && !x.Round.IsDisable)
            .ToListAsync();

        if (roundDetails.Count == 0)
        {
            return ApiResponseFactory.BasePagination(new List<object>(), pageIndex, pageSize, 0);
        }

        var roundDetailIds = roundDetails.Select(x => x.Id).ToList();
        var roundDetailLookup = roundDetails.ToDictionary(x => x.Id);

        // Get latest submission per round detail (Judge only sees latest)
        var latestSubmissions = await _dbContext.Submissions
            .AsNoTracking()
            .Include(x => x.Scores.Where(s => !s.IsDisable && !s.IsMock && assignTrackIds.Contains(s.AssignTrackId)))
            .Where(x => roundDetailIds.Contains(x.RoundDetailId) && !x.IsDisable)
            .GroupBy(x => x.RoundDetailId)
            .Select(g => g.OrderByDescending(x => x.SubmittedAt ?? x.CreatedAt).First())
            .ToListAsync();

        var items = latestSubmissions.Select(s =>
        {
            var rd = roundDetailLookup[s.RoundDetailId];
            var myScore = s.Scores
                .OrderByDescending(sc => sc.UpdatedAt)
                .FirstOrDefault();

            return new Response.JudgeTeamSubmissionListResponse
            {
                SubmissionId = s.Id,
                RoundId = rd.RoundId,
                RoundName = rd.Round.Name,
                RoundNo = rd.Round.RoundNo,
                RoundDetailId = s.RoundDetailId,
                Url = s.Url,
                Description = s.Description,
                Status = s.Status,
                SubmittedAt = s.SubmittedAt,
                GradingStatus = myScore != null ? "Graded" : "Pending",
                ScoreId = myScore?.Id,
                TotalScore = myScore?.TotalScore,
            };
        }).ToList();

        var totalCount = items.Count;
        var paged = items
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
    }

    public async Task<BasePaginationResponse> GetJudgeRoundAllSubmissions(Guid roundId, string? status, PaginationRequest paginationRequest)
    {
        var userId = GetCurrentUserId();
        var pageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        var pageSize = paginationRequest.PageSize <= 0 ? 10 : Math.Min(paginationRequest.PageSize, 100);

        // Get judge's assigned tracks with their info
        var assignedTracks = await GetJudgeAssignmentsQuery(userId)
            .Where(x => x.Track.Event.Rounds.Any(r => r.Id == roundId))
            .Select(x => new { x.Id, x.TrackId, TrackTitle = x.Track.Title })
            .ToListAsync();

        if (assignedTracks.Count == 0)
        {
            return ApiResponseFactory.BasePagination(new List<Response.JudgeRoundAllSubmissionResponse>(), pageIndex, pageSize, 0);
        }

        var assignTrackIds = assignedTracks.Select(x => x.Id).ToList();
        var trackLookup = assignedTracks.ToDictionary(x => x.Id, x => new { x.TrackId, x.TrackTitle });
        var trackIds = assignedTracks.Select(x => x.TrackId).ToHashSet();

        // Get active criteria template for this round
        var activeTemplate = await _dbContext.CriteriaTemplates
            .AsNoTracking()
            .Where(x => x.RoundId == roundId && !x.IsDisable)
            .Select(x => new
            {
                x.Id,
                CriteriaCount = x.CriteriaItems.Count(c => !c.IsDisable)
            })
            .FirstOrDefaultAsync();

        var totalCriteriaItems = activeTemplate?.CriteriaCount ?? 0;

        // Get all register teams in judge's tracks that have round details for this round
        var registerTeamsQuery = _dbContext.RegisterTeams
            .AsNoTracking()
            .Include(x => x.Team)
            .Include(x => x.Topic)
            .Where(x => !x.IsDisable
                        && !x.IsBanned
                        && x.Status == RegisterTeamStatusEnum.Approved
                        && !x.Team.IsDisable
                        && x.TrackId.HasValue
                        && trackIds.Contains(x.TrackId.Value)
                        && x.RoundDetails.Any(rd => rd.RoundId == roundId && !rd.IsDisable));

        var registerTeams = await registerTeamsQuery
            .Select(x => new
            {
                x.Id,
                x.TeamId,
                TeamName = x.Team.Name,
                x.TrackId,
                x.TopicId,
                TopicTitle = x.Topic != null ? x.Topic.Title : null,
                RoundDetailId = x.RoundDetails
                    .Where(rd => rd.RoundId == roundId && !rd.IsDisable)
                    .Select(rd => rd.Id)
                    .FirstOrDefault()
            })
            .ToListAsync();

        if (registerTeams.Count == 0)
        {
            return ApiResponseFactory.BasePagination(new List<Response.JudgeRoundAllSubmissionResponse>(), pageIndex, pageSize, 0);
        }

        var roundDetailIds = registerTeams.Select(x => x.RoundDetailId).ToList();

        // Get latest submission per round detail with scores
        var latestSubmissions = await _dbContext.Submissions
            .AsNoTracking()
            .Include(x => x.Scores.Where(s => !s.IsDisable && !s.IsMock && assignTrackIds.Contains(s.AssignTrackId)))
            .ThenInclude(x => x.ScoreItems.Where(si => !si.IsDisable))
            .Where(x => roundDetailIds.Contains(x.RoundDetailId) && !x.IsDisable)
            .GroupBy(x => x.RoundDetailId)
            .Select(g => g.OrderByDescending(x => x.SubmittedAt ?? x.CreatedAt).First())
            .ToListAsync();

        var submissionLookup = latestSubmissions.ToDictionary(x => x.RoundDetailId);

        // Build items
        var items = registerTeams.Select(rt =>
        {
            submissionLookup.TryGetValue(rt.RoundDetailId, out var submission);
            var track = assignedTracks.FirstOrDefault(t => t.TrackId == rt.TrackId);

            string gradingStatus = "NoSubmission";
            Guid? scoreId = null;
            decimal? totalScore = null;

            if (submission != null)
            {
                var myScore = submission.Scores
                    .OrderByDescending(s => s.UpdatedAt)
                    .FirstOrDefault();

                if (myScore != null)
                {
                    scoreId = myScore.Id;
                    totalScore = myScore.TotalScore;

                    var scoredCriteriaCount = myScore.ScoreItems.Count(si => !si.IsDisable && si.Score.HasValue);
                    gradingStatus = (totalCriteriaItems > 0 && scoredCriteriaCount >= totalCriteriaItems) ? "Graded" : "Pending";
                }
                else
                {
                    gradingStatus = "Pending";
                }
            }

            return new Response.JudgeRoundAllSubmissionResponse
            {
                TrackId = rt.TrackId ?? Guid.Empty,
                TrackTitle = track?.TrackTitle ?? "",
                RegisterTeamId = rt.Id,
                TeamId = rt.TeamId,
                TeamName = rt.TeamName,
                TopicId = rt.TopicId,
                TopicTitle = rt.TopicTitle,
                SubmissionId = submission?.Id,
                Url = submission?.Url,
                SubmissionStatus = submission?.Status,
                SubmittedAt = submission?.SubmittedAt,
                GradingStatus = gradingStatus,
                ScoreId = scoreId,
                TotalScore = totalScore,
            };
        }).ToList();

        // Filter by status
        if (!string.IsNullOrWhiteSpace(status) && !status.Equals("all", StringComparison.OrdinalIgnoreCase))
        {
            if (status.Equals("graded", StringComparison.OrdinalIgnoreCase))
            {
                items = items.Where(x => x.GradingStatus == "Graded").ToList();
            }
            else if (status.Equals("pending", StringComparison.OrdinalIgnoreCase))
            {
                items = items.Where(x => x.GradingStatus != "Graded").ToList();
            }
        }

        // Sort: pending first, then by SubmittedAt DESC, then by track title
        items = items
            .OrderBy(x => x.GradingStatus == "Graded" ? 1 : 0)
            .ThenByDescending(x => x.SubmittedAt)
            .ThenBy(x => x.TrackTitle)
            .ThenBy(x => x.TeamName)
            .ToList();

        var totalCount = items.Count;
        var paged = items
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return ApiResponseFactory.BasePagination(paged, pageIndex, pageSize, totalCount);
    }

    private sealed record SubmissionAccess(Guid RoundId, Guid TrackId, Guid AssignTrackId);
}
