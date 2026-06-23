using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Submissions;

public class Service : IService
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContext;

    public Service(AppDbContext dbContext, IHttpContextAccessor httpContext)
    {
        _dbContext = dbContext;
        _httpContext = httpContext;
    }

    public async Task<Response.SubmissionDetailResponse> GetSubmissionDetail(Guid submissionId)
    {
        var submission = await _dbContext.Submissions
            .AsNoTracking()
            .Include(x => x.RoundDetail).ThenInclude(x => x.Round)
            .Include(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Team)
            .Include(x => x.Scores).ThenInclude(x => x.ScoreItems).ThenInclude(x => x.CriteriaItem)
            .FirstOrDefaultAsync(x => x.Id == submissionId && !x.IsDisable);

        if (submission == null)
        {
            throw new NotFoundException("SUBMISSION_NOT_FOUND");
        }

        await EnsureCanViewSubmission(submission);

        var scores = submission.Scores.Where(x => !x.IsDisable && x.TotalScore.HasValue).ToList();
        var scoreResponse = BuildSubmissionScore(scores);

        return new Response.SubmissionDetailResponse
        {
            SubmissionId = submission.Id,
            RoundDetailId = submission.RoundDetailId,
            RoundId = submission.RoundDetail.RoundId,
            RoundName = submission.RoundDetail.Round.Name,
            TeamId = submission.RoundDetail.RegisterTeam.TeamId,
            TeamName = submission.RoundDetail.RegisterTeam.Team.Name,
            Url = submission.Url,
            Description = submission.Description,
            Status = submission.Status?.ToString(),
            SubmittedAt = submission.SubmittedAt,
            GradingStatus = scoreResponse == null ? "NotGraded" : "Graded",
            Message = scoreResponse == null ? "Bài chưa được chấm" : null,
            Score = scoreResponse,
        };
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

    private async Task EnsureCanViewSubmission(Hackathon.Repository.Entity.Submissions submission)
    {
        var userId = GetCurrentUserId();
        var role = _httpContext.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
        var eventId = submission.RoundDetail.Round.EventId;
        var teamId = submission.RoundDetail.RegisterTeam.TeamId;
        var trackId = submission.RoundDetail.RegisterTeam.TrackId;

        if (role == RoleEnum.Admin.ToString())
        {
            return;
        }

        if (role == RoleEnum.Staff.ToString())
        {
            var isAssignedStaff = await _dbContext.AssignEvents
                .AsNoTracking()
                .AnyAsync(x => x.UserId == userId
                    && x.EventId == eventId
                    && !x.IsDisable
                    && !x.Event.IsDisable);

            if (isAssignedStaff)
            {
                return;
            }
        }

        var isTeamMember = await _dbContext.TeamDetails
            .AsNoTracking()
            .AnyAsync(x => x.TeamId == teamId
                && x.UserId == userId
                && !x.IsDisable
                && x.Status == TeamDetailStatusEnum.Active);

        if (isTeamMember)
        {
            return;
        }

        if (trackId.HasValue)
        {
            var isAssignedJudge = await _dbContext.AssignTracks
                .AsNoTracking()
                .AnyAsync(x => x.TrackId == trackId.Value
                    && !x.IsDisable
                    && !x.AssignEvent.IsDisable
                    && x.AssignEvent.UserId == userId
                    && x.AssignEvent.EventId == eventId
                    && x.AssignEvent.EventRole != null
                    && x.AssignEvent.EventRole.Name == EventRoleEnum.Judge);

            if (isAssignedJudge)
            {
                return;
            }
        }

        throw new ForbiddenException("FORBIDDEN");
    }

    private static Response.SubmissionScoreResponse? BuildSubmissionScore(List<Scores> scores)
    {
        if (scores.Count == 0)
        {
            return null;
        }

        var criteriaScores = scores
            .SelectMany(x => x.ScoreItems)
            .Where(x => !x.IsDisable)
            .GroupBy(x => x.CriteriaItemId)
            .Select(x => new Response.CriteriaScoreResponse
            {
                CriteriaItemId = x.Key,
                CriteriaItemName = x.First().CriteriaItem.Name,
                AverageCriteriaScore = x.Where(scoreItem => scoreItem.Score.HasValue).Select(scoreItem => scoreItem.Score!.Value).DefaultIfEmpty().Average(),
                MaxScore = x.First().CriteriaItem.Score,
            })
            .ToList();

        return new Response.SubmissionScoreResponse
        {
            AverageTotalScore = scores.Select(x => x.TotalScore!.Value).Average(),
            IsAppealable = true,
            CriteriaScores = criteriaScores,
        };
    }

    public async Task<Response.SubmitRoundProjectResponse> SubmitRoundProject(Guid roundId, Guid registerTeamId, Request.SubmitRoundProjectRequest request)
    {
        var userId = GetCurrentUserId();
        var now = DateTimeOffset.UtcNow;

        // 1. Validate RegisterTeam
        var registerTeam = await _dbContext.RegisterTeams
            .FirstOrDefaultAsync(x => x.Id == registerTeamId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        // 2. Validate leadership
        var leaderDetail = await _dbContext.TeamDetails
            .FirstOrDefaultAsync(x => x.TeamId == registerTeam.TeamId
                                      && x.UserId == userId
                                      && x.IsLeader
                                      && !x.IsDisable
                                      && x.Status == TeamDetailStatusEnum.Active);

        if (leaderDetail == null)
        {
            throw new ForbiddenException("ONLY_TEAM_LEADER_CAN_SUBMIT");
        }

        // 3. Validate Round
        var round = await _dbContext.Rounds
            .FirstOrDefaultAsync(x => x.Id == roundId && x.EventId == registerTeam.EventId && !x.IsDisable);

        if (round == null)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        // 4. Validate round submission open time
        if (now < round.StartSubmission || now > round.EndSubmission)
        {
            throw new BadRequestException("ROUND_SUBMISSION_CLOSED");
        }

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            // 5. Check or create RoundDetails
            var roundDetail = await _dbContext.RoundDetails
                .FirstOrDefaultAsync(x => x.RoundId == roundId
                                          && x.RegisterTeamId == registerTeamId
                                          && !x.IsDisable);

            if (roundDetail == null)
            {
                roundDetail = new RoundDetails
                {
                    Id = Guid.NewGuid(),
                    RoundId = roundId,
                    RegisterTeamId = registerTeamId,
                    IsDisable = false,
                    CreatedAt = now,
                    UpdatedAt = now
                };
                await _dbContext.RoundDetails.AddAsync(roundDetail);
                await _dbContext.SaveChangesAsync();
            }

            // 6. Create Submission record
            var submission = new Hackathon.Repository.Entity.Submissions
            {
                Id = Guid.NewGuid(),
                RoundDetailId = roundDetail.Id,
                Url = request.Url,
                Description = request.Description,
                Status = SubmissionStatusEnum.Submitted,
                SubmittedAt = now,
                IsDisable = false,
                CreatedAt = now,
                UpdatedAt = now
            };

            await _dbContext.Submissions.AddAsync(submission);
            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            return new Response.SubmitRoundProjectResponse
            {
                SubmissionId = submission.Id,
                TeamId = registerTeam.TeamId,
                SubmittedAt = now,
                Status = submission.Status.ToString()!,
                IsSuccess = true
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<BasePaginationResponse> GetSubmissions(Guid roundId, Guid registerTeamId, Request.GetSubmissionsRequest request)
    {
        var userId = GetCurrentUserId();

        var roundDetail = await _dbContext.RoundDetails
            .AsNoTracking()
            .Include(x => x.Round)
            .Include(x => x.RegisterTeam).ThenInclude(x => x.Team)
            .FirstOrDefaultAsync(x => x.RoundId == roundId
                                      && x.RegisterTeamId == registerTeamId
                                      && !x.IsDisable);

        if (roundDetail == null)
        {
            throw new NotFoundException("ROUND_DETAIL_NOT_FOUND");
        }

        // Apply same security policy as EnsureCanViewSubmission
        var role = _httpContext.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
        var eventId = roundDetail.Round.EventId;
        var teamId = roundDetail.RegisterTeam.TeamId;
        var trackId = roundDetail.RegisterTeam.TrackId;

        bool hasAccess = false;

        if (role == RoleEnum.Admin.ToString())
        {
            hasAccess = true;
        }
        else if (role == RoleEnum.Staff.ToString())
        {
            var isAssignedStaff = await _dbContext.AssignEvents
                .AsNoTracking()
                .AnyAsync(x => x.UserId == userId
                    && x.EventId == eventId
                    && !x.IsDisable
                    && !x.Event.IsDisable);

            if (isAssignedStaff)
            {
                hasAccess = true;
            }
        }
        else
        {
            // Check if user is a member of the team
            var isTeamMember = await _dbContext.TeamDetails
                .AsNoTracking()
                .AnyAsync(x => x.TeamId == teamId
                    && x.UserId == userId
                    && !x.IsDisable
                    && x.Status == TeamDetailStatusEnum.Active);

            if (isTeamMember)
            {
                hasAccess = true;
            }
            else if (trackId.HasValue)
            {
                // Check if user is assigned as Judge for this track
                var isAssignedJudge = await _dbContext.AssignTracks
                    .AsNoTracking()
                    .AnyAsync(x => x.TrackId == trackId.Value
                        && !x.IsDisable
                        && !x.AssignEvent.IsDisable
                        && x.AssignEvent.UserId == userId
                        && x.AssignEvent.EventId == eventId
                        && x.AssignEvent.EventRole != null
                        && x.AssignEvent.EventRole.Name == EventRoleEnum.Judge);

                if (isAssignedJudge)
                {
                    hasAccess = true;
                }
            }
        }

        if (!hasAccess)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        var reqPageIndex = request.PageIndex;
        var reqPageSize = request.PageSize;

        var query = _dbContext.Submissions
            .AsNoTracking()
            .Where(x => x.RoundDetailId == roundDetail.Id && !x.IsDisable);

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderByDescending(x => x.SubmittedAt)
            .Skip((reqPageIndex - 1) * reqPageSize)
            .Take(reqPageSize)
            .Select(x => new Response.RoundSubmissionItemResponse
            {
                SubmissionId = x.Id,
                Url = x.Url,
                Description = x.Description,
                Status = x.Status.ToString() ?? string.Empty,
                SubmittedAt = x.SubmittedAt
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, reqPageIndex, reqPageSize, totalCount);
    }
}
