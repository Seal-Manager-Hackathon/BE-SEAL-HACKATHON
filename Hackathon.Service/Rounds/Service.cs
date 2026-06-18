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

    public async Task<List<Response.MyRoundResponse>> GetMyRounds(Guid? eventId)
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

        var submission = new Submissions
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

        await _dbContext.Submissions.AddAsync(submission);
        await _dbContext.SaveChangesAsync();

        return new Response.SubmitAssignmentResponse
        {
            SubmissionId = submission.Id,
            TeamId = roundDetail.RegisterTeam.TeamId,
            Url = submission.Url,
            SubmittedAt = now,
            Message = "SUBMISSION_CREATED_SUCCESSFULLY"
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
}