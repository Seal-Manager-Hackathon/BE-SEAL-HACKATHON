using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
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
}
