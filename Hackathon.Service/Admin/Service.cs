using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Admin;

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
            throw new UnauthorizedException("INVALID_ACCESS_TOKEN");
        }

        if (!Guid.TryParse(userIdValue, out var userId))
        {
            throw new UnauthorizedException("INVALID_ACCESS_TOKEN");
        }

        return userId;
    }

    public async Task<BasePaginationResponse> GetAllUsers(RoleEnum? role, string? keyword, PaginationRequest paginationRequest)
    {
        var q = _dbContext.Users
            .AsNoTracking()
            .Where(x => !x.IsDisable);

        if (role.HasValue)
        {
            q = q.Where(x => x.Role == role.Value);
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var normalized = keyword.Trim().ToLower();
            q = q.Where(x => (x.FirstName + " " + x.LastName).ToLower().Contains(normalized)
                || x.Email.ToLower() == normalized);
        }

        var totalCount = await q.CountAsync();

        paginationRequest.PageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        paginationRequest.PageSize = paginationRequest.PageSize <= 0 ? 10 : System.Math.Min(paginationRequest.PageSize, 100);

        var items = await BuildUserQuery(q, paginationRequest);
        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
    }

    public async Task<BasePaginationResponse> SearchUsers(GetUsersQuery query)
    {
        var q = _dbContext.Users.AsNoTracking();

        if (query.IsDisable.HasValue)
        {
            q = q.Where(x => x.IsDisable == query.IsDisable.Value);
        }

        if (query.IsVerified.HasValue)
        {
            q = q.Where(x => x.IsVerified == query.IsVerified.Value);
        }

        // KeySearch — search across firstName, lastName (partial match)
        if (!string.IsNullOrWhiteSpace(query.KeySearch))
        {
            var normalized = query.KeySearch.Trim().ToLower();
            q = q.Where(x => (x.FirstName + " " + x.LastName).ToLower().Contains(normalized));
        }

        if (!string.IsNullOrWhiteSpace(query.MailSearch))
        {
            var normalized = query.MailSearch.Trim().ToLower();
            q = q.Where(x => x.Email.ToLower() == normalized);
        }

        if (query.IdSearch.HasValue)
        {
            q = q.Where(x => x.Id == query.IdSearch.Value);
        }

        if (query.Role.HasValue)
        {
            q = q.Where(x => x.Role == query.Role.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.StudentIdSearch))
        {
            var normalizedStudentId = query.StudentIdSearch.Trim().ToLower();
            q = q.Where(x => x.StudentId.ToLower() == normalizedStudentId);
        }

        var totalCount = await q.CountAsync();

        query.Pagination.PageIndex = query.Pagination.PageIndex <= 0 ? 1 : query.Pagination.PageIndex;
        query.Pagination.PageSize = query.Pagination.PageSize <= 0 ? 10 : System.Math.Min(query.Pagination.PageSize, 100);

        var items = await BuildUserQuery(q, query.Pagination);
        return ApiResponseFactory.BasePagination(items, query.Pagination.PageIndex, query.Pagination.PageSize, totalCount);
    }

    public async Task<BasePaginationResponse> GetRounds(Guid eventId, GetAdminRoundsRequest request)
    {
        var eventExists = await _dbContext.Events
            .AsNoTracking()
            .AnyAsync(x => x.Id == eventId);

        if (!eventExists)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        request.PageIndex = request.PageIndex <= 0 ? 1 : request.PageIndex;
        request.PageSize = request.PageSize <= 0 ? 10 : System.Math.Min(request.PageSize, 100);

        var q = _dbContext.Rounds
            .AsNoTracking()
            .Where(x => x.EventId == eventId);

        if (request.IsDisable.HasValue)
        {
            q = q.Where(x => x.IsDisable == request.IsDisable.Value);
        }

        var totalCount = await q.CountAsync();
        var items = await BuildRoundQuery(q, request);
        return ApiResponseFactory.BasePagination(items, request.PageIndex, request.PageSize, totalCount);
    }

    public async Task<CreateRoundResponse> CreateRound(Guid eventId, CreateRoundRequest request)
    {
        var eventEntity = await _dbContext.Events
            .FirstOrDefaultAsync(x => x.Id == eventId);

        if (eventEntity == null)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new BadRequestException("ROUND_NAME_REQUIRED");
        }

        // Không thể tạo round mới nếu event đã bắt đầu
        var now = DateTimeOffset.UtcNow;
        if (eventEntity.StartTime.HasValue && now >= eventEntity.StartTime.Value)
        {
            throw new BadRequestException("EVENT_ALREADY_STARTED");
        }

        ValidateRoundTimes(request.StartTime, request.EndTime, request.StartSubmission, request.EndSubmission);
        ValidateLimitTeam(request.LimitTeam);

        // Auto-assign RoundNo: current max RoundNo + 1 (starts at 1)
        var maxRoundNo = await _dbContext.Rounds
            .AsNoTracking()
            .Where(x => x.EventId == eventId && !x.IsDisable)
            .MaxAsync(x => (int?)x.RoundNo) ?? 0;

        var round = new Hackathon.Repository.Entity.Rounds
        {
            Id = Guid.NewGuid(),
            EventId = eventId,
            Name = request.Name.Trim(),
            Description = request.Description?.Trim(),
            RoundNo = maxRoundNo + 1,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            StartSubmission = request.StartSubmission,
            EndSubmission = request.EndSubmission,
            LimitTeam = request.LimitTeam,
            IsDisable = false,
            CreatedAt = now,
            UpdatedAt = now,
        };

        // Increment NumberRound on event
        eventEntity.NumberRound = (eventEntity.NumberRound ?? 0) + 1;
        eventEntity.UpdatedAt = now;

        await _dbContext.Rounds.AddAsync(round);
        await _dbContext.SaveChangesAsync();

        return new CreateRoundResponse
        {
            RoundId = round.Id
        };
    }

    public async Task UpdateRound(Guid roundId, UpdateRoundRequest request)
    {
        var round = await _dbContext.Rounds.FirstOrDefaultAsync(x => x.Id == roundId && !x.IsDisable);
        if (round == null)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        var eventEntity = await _dbContext.Events
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == round.EventId);

        var now = DateTimeOffset.UtcNow;

        // ⚠️ Ràng buộc thời gian (comment để tham khảo):
        // Hiện tại cho phép sửa round kể cả khi event đã bắt đầu.
        // Nếu cần chặn sau này, uncomment block dưới:
        // if (eventEntity.StartTime.HasValue && now >= eventEntity.StartTime.Value)
        // {
        //     var isChangingCriticalFields = request.StartTime.HasValue
        //         || request.EndTime.HasValue
        //         || request.StartSubmission.HasValue
        //         || request.EndSubmission.HasValue
        //         || request.RoundNo.HasValue
        //         || request.LimitTeam.HasValue;
        //     if (isChangingCriticalFields) throw new BadRequestException("EVENT_ALREADY_STARTED");
        // }

        var nextStartTime = request.StartTime ?? round.StartTime;
        var nextEndTime = request.EndTime ?? round.EndTime;
        var nextStartSubmission = request.StartSubmission ?? round.StartSubmission;
        var nextEndSubmission = request.EndSubmission ?? round.EndSubmission;
        var nextLimitTeam = request.LimitTeam ?? round.LimitTeam;

        if (request.Name != null && string.IsNullOrWhiteSpace(request.Name))
        {
            throw new BadRequestException("ROUND_NAME_REQUIRED");
        }

        ValidateRoundTimes(nextStartTime, nextEndTime, nextStartSubmission, nextEndSubmission);
        ValidateLimitTeam(nextLimitTeam);

        // Swap RoundNo with target round if provided
        var targetRoundNo = request.RoundNo;
        if (targetRoundNo.HasValue && targetRoundNo.Value != round.RoundNo)
        {
            if (targetRoundNo.Value <= 0)
            {
                throw new BadRequestException("ROUND_NO_MUST_BE_POSITIVE");
            }

            var targetRound = await _dbContext.Rounds
                .FirstOrDefaultAsync(x => x.EventId == round.EventId
                    && x.RoundNo == targetRoundNo.Value
                    && !x.IsDisable
                    && x.Id != round.Id);

            if (targetRound == null)
            {
                throw new NotFoundException("TARGET_ROUND_NOT_FOUND");
            }

            // Swap RoundNo
            (targetRound.RoundNo, round.RoundNo) = (round.RoundNo, targetRound.RoundNo);
            targetRound.UpdatedAt = now;
            _dbContext.Rounds.Update(targetRound);
        }

        if (request.Name != null)
        {
            round.Name = request.Name.Trim();
        }

        if (request.Description != null)
        {
            round.Description = request.Description.Trim();
        }

        if (request.StartTime.HasValue)
        {
            round.StartTime = request.StartTime;
        }

        if (request.EndTime.HasValue)
        {
            round.EndTime = request.EndTime;
        }

        if (request.StartSubmission.HasValue)
        {
            round.StartSubmission = request.StartSubmission;
        }

        if (request.EndSubmission.HasValue)
        {
            round.EndSubmission = request.EndSubmission;
        }

        if (request.LimitTeam.HasValue)
        {
            round.LimitTeam = request.LimitTeam;
        }

        round.UpdatedAt = now;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteRound(Guid roundId)
    {
        var round = await _dbContext.Rounds.FirstOrDefaultAsync(x => x.Id == roundId);
        if (round == null)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        var eventEntity = await _dbContext.Events
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == round.EventId && !x.IsDisable);

        var now = DateTimeOffset.UtcNow;
        if (eventEntity != null && eventEntity.StartTime.HasValue && now >= eventEntity.StartTime.Value)
        {
            throw new BadRequestException("EVENT_ALREADY_STARTED");
        }

        var eventId = round.EventId;
        var deletedRoundNo = round.RoundNo;

        round.IsDisable = true;
        round.UpdatedAt = now;

        // Soft-delete all criteria templates and items of this round
        var templates = await _dbContext.CriteriaTemplates
            .Include(x => x.CriteriaItems)
            .Where(x => x.RoundId == roundId && !x.IsDisable)
            .ToListAsync();

        foreach (var template in templates)
        {
            template.IsDisable = true;
            template.UpdatedAt = now;

            foreach (var item in template.CriteriaItems.Where(x => !x.IsDisable))
            {
                item.IsDisable = true;
                item.UpdatedAt = now;
            }
        }

        // Renumber: decrement RoundNo for all rounds > deleted round
        var roundsToRenumber = await _dbContext.Rounds
            .Where(x => x.EventId == eventId && !x.IsDisable && x.RoundNo > deletedRoundNo)
            .ToListAsync();

        foreach (var r in roundsToRenumber)
        {
            r.RoundNo--;
            r.UpdatedAt = now;
        }

        // Decrement NumberRound on event
        if (eventEntity != null)
        {
            eventEntity.NumberRound = Math.Max(0, (eventEntity.NumberRound ?? 1) - 1);
            eventEntity.UpdatedAt = now;
            _dbContext.Events.Update(eventEntity);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<string> RestoreRound(Guid roundId)
    {
        var round = await _dbContext.Rounds.FirstOrDefaultAsync(x => x.Id == roundId);
        if (round == null)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        if (!round.IsDisable)
        {
            throw new ConflictException("ROUND_NOT_DISABLED");
        }

        var eventEntity = await _dbContext.Events
            .FirstOrDefaultAsync(x => x.Id == round.EventId && !x.IsDisable);

        if (eventEntity == null)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        var now = DateTimeOffset.UtcNow;
        round.IsDisable = false;
        round.UpdatedAt = now;

        // Restore RoundNo = current max + 1 (put at end)
        var maxRoundNo = await _dbContext.Rounds
            .AsNoTracking()
            .Where(x => x.EventId == round.EventId && !x.IsDisable && x.Id != round.Id)
            .MaxAsync(x => (int?)x.RoundNo) ?? 0;

        round.RoundNo = maxRoundNo + 1;

        // Increment NumberRound
        eventEntity.NumberRound = (eventEntity.NumberRound ?? 0) + 1;
        eventEntity.UpdatedAt = now;

        // NOTE: Criteria templates/items remain disabled — admin must re-create or manually re-enable

        await _dbContext.SaveChangesAsync();

        return "ROUND_RESTORED_SUCCESSFULLY";
    }

    public async Task<SendSystemNotificationResponse> SendSystemNotification(SendSystemNotificationRequest request)
    {
        GetCurrentUserId();

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new BadRequestException("TITLE_REQUIRED");
        }

        if (string.IsNullOrWhiteSpace(request.Description))
        {
            throw new BadRequestException("DESCRIPTION_REQUIRED");
        }

        var title = request.Title.Trim();
        var description = request.Description.Trim();
        var now = DateTimeOffset.UtcNow;
        var userIds = await _dbContext.Users
            .AsNoTracking()
            .Where(x => !x.IsDisable)
            .Select(x => x.Id)
            .ToListAsync();

        var notifications = userIds.Select(userId => new Hackathon.Repository.Entity.Notifications
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            TeamId = null,
            Title = title,
            Description = description,
            Status = NotificationStatusEnum.Unread,
            TargetType = NotificationTargetTypeEnum.System,
            IsDisable = false,
            CreatedAt = now,
            UpdatedAt = now
        }).ToList();

        await _dbContext.Notifications.AddRangeAsync(notifications);
        await _dbContext.SaveChangesAsync();

        return new SendSystemNotificationResponse
        {
            NotificationIds = notifications.Select(x => x.Id).ToList(),
            TotalSent = notifications.Count
        };
    }

    public async Task<BasePaginationResponse> GetRoundSubmissions(Guid roundId, Rounds.Request.GetStaffRoundSubmissionsQuery query)
    {
        var round = await _dbContext.Rounds
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == roundId && !x.IsDisable);

        if (round == null)
        {
            throw new NotFoundException("ROUND_NOT_FOUND");
        }

        var roundDetailsQuery = _dbContext.RoundDetails
            .AsNoTracking()
            .Include(x => x.RegisterTeam).ThenInclude(x => x.Team)
            .Include(x => x.RegisterTeam).ThenInclude(x => x.Track)
            .Include(x => x.RegisterTeam).ThenInclude(x => x.Topic)
            .Include(x => x.Submissions).ThenInclude(x => x.Scores).ThenInclude(x => x.ScoreItems).ThenInclude(x => x.CriteriaItem)
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

        var items = roundDetails.Select(roundDetail =>
        {
            var submissions = roundDetail.Submissions
                .Where(x => !x.IsDisable)
                .OrderByDescending(x => x.SubmittedAt ?? x.CreatedAt)
                .ToList();

            var allSubmissions = submissions.Select(submission =>
            {
                var judges = BuildAssignedJudges(submission);
                var score = CalculateTotalScore(submission);
                var gradingStatus = GetGradingStatus(submission, judges);

                return new AdminSubmissionHistoryResponse
                {
                    SubmissionId = submission.Id,
                    Url = submission.Url,
                    Description = submission.Description,
                    Status = submission.Status,
                    SubmittedAt = submission.SubmittedAt,
                    IsLatest = false,
                    AverageScore = score,
                    GradingStatus = gradingStatus,
                    AssignedJudges = judges,
                };
            }).ToList();

            // Mark latest
            if (allSubmissions.Count > 0)
            {
                allSubmissions[0].IsLatest = true;
            }

            // Overall status from latest submission
            var latestSubmission = submissions.FirstOrDefault();
            List<Rounds.Response.AssignedJudgeResponse> latestJudges = new();
            decimal? overallScore = null;
            string? overallGradingStatus = null;
            if (latestSubmission != null)
            {
                latestJudges = BuildAssignedJudges(latestSubmission);
                overallScore = CalculateTotalScore(latestSubmission);
                overallGradingStatus = GetGradingStatus(latestSubmission, latestJudges);
            }

            return new AdminRoundTeamSubmissionResponse
            {
                RegisterTeamId = roundDetail.RegisterTeamId,
                TeamId = roundDetail.RegisterTeam.TeamId,
                TeamName = roundDetail.RegisterTeam.Team.Name,
                TrackId = roundDetail.RegisterTeam.TrackId,
                TrackTitle = roundDetail.RegisterTeam.Track?.Title,
                TopicId = roundDetail.RegisterTeam.TopicId,
                TopicTitle = roundDetail.RegisterTeam.Topic?.Title,
                Submissions = allSubmissions,
                HasLatestSubmission = latestSubmission != null,
                AverageScore = overallScore,
                GradingStatus = overallGradingStatus,
                AssignedJudges = latestJudges,
            };
        }).ToList();

        if (!string.IsNullOrWhiteSpace(query.SubmissionStatus) && !query.SubmissionStatus.Equals("All", StringComparison.OrdinalIgnoreCase))
        {
            if (Enum.TryParse<Hackathon.Repository.Enum.SubmissionStatusEnum>(query.SubmissionStatus, true, out var filterStatus))
            {
                items = items.Where(x => x.Submissions.Any(s => s.Status == filterStatus)).ToList();
            }
        }

        if (!string.IsNullOrWhiteSpace(query.GradingStatus) && !query.GradingStatus.Equals("All", StringComparison.OrdinalIgnoreCase))
        {
            items = items.Where(x => string.Equals(x.GradingStatus, query.GradingStatus, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        var totalCount = items.Count;
        var paged = items
            .OrderBy(x => x.TrackTitle)
            .ThenBy(x => x.TeamName)
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return ApiResponseFactory.BasePagination(paged, query.PageIndex, query.PageSize, totalCount);
    }

    private static List<Rounds.Response.AssignedJudgeResponse> BuildAssignedJudges(Hackathon.Repository.Entity.Submissions submission)
    {
        return submission.Scores
            .Where(s => !s.IsDisable && !s.IsMock)
            .GroupBy(s => s.AssignTrackId)
            .Select(g => g.OrderByDescending(s => s.UpdatedAt).First())
            .Select(s => new Rounds.Response.AssignedJudgeResponse
            {
                JudgeId = s.AssignTrack.AssignEvent.UserId,
                JudgeName = $"{s.AssignTrack.AssignEvent.User.FirstName} {s.AssignTrack.AssignEvent.User.LastName}".Trim(),
                Email = s.AssignTrack.AssignEvent.User.Email,
                HasScored = s.TotalScore.HasValue,
                TotalScore = s.TotalScore,
                IsFinalized = false,
            })
            .ToList();
    }

    private static decimal? CalculateTotalScore(Hackathon.Repository.Entity.Submissions submission)
    {
        var latestScores = submission.Scores
            .Where(s => !s.IsDisable && !s.IsMock)
            .GroupBy(s => s.AssignTrackId)
            .Select(g => g.OrderByDescending(s => s.UpdatedAt).First())
            .ToList();

        var allScoreItems = latestScores
            .SelectMany(s => s.ScoreItems)
            .Where(si => !si.IsDisable && si.Score.HasValue && !si.CriteriaItem.IsDisable)
            .ToList();

        if (allScoreItems.Count == 0)
            return null;

        return allScoreItems
            .GroupBy(x => x.CriteriaItemId)
            .Select(g => g.Average(x => x.Score!.Value))
            .Sum();
    }

    private static string? GetGradingStatus(Hackathon.Repository.Entity.Submissions submission, List<Rounds.Response.AssignedJudgeResponse> assignedJudges)
    {
        if (submission.Status != Hackathon.Repository.Enum.SubmissionStatusEnum.Submitted)
            return null;

        if (assignedJudges.Count == 0)
            return "NoJudgesAssigned";

        var scoredCount = assignedJudges.Count(x => x.HasScored);
        if (scoredCount == 0)
            return "PendingGrading";

        if (scoredCount < assignedJudges.Count)
            return "PartialGraded";

        return "Graded";
    }

    public async Task<string> ChangeUserRole(Guid userId, ChangeUserRoleRequest request)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        if (user.Role == request.Role)
        {
            throw new BadRequestException("ROLE_ALREADY_SET");
        }

        user.Role = request.Role;
        user.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();

        return "USER_ROLE_UPDATED_SUCCESSFULLY";
    }

    private async Task<List<AdminRoundResponse>> BuildRoundQuery(IQueryable<Hackathon.Repository.Entity.Rounds> q, PaginationRequest pagination)
    {
        return await q
            .OrderBy(x => x.RoundNo)
            .ThenBy(x => x.CreatedAt)
            .Skip((pagination.PageIndex - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .Select(x => new AdminRoundResponse
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
    }

    private async Task ValidateRoundNo(Guid eventId, int? roundNo, Guid? currentRoundId)
    {
        if (!roundNo.HasValue)
        {
            return;
        }

        if (roundNo.Value <= 0)
        {
            throw new BadRequestException("ROUND_NO_MUST_BE_POSITIVE");
        }

        var roundNoExists = await _dbContext.Rounds
            .AsNoTracking()
            .AnyAsync(x => x.EventId == eventId
                && x.RoundNo == roundNo.Value
                && !x.IsDisable
                && (!currentRoundId.HasValue || x.Id != currentRoundId.Value));

        if (roundNoExists)
        {
            throw new ConflictException("ROUND_NO_ALREADY_EXISTS");
        }
    }

    private static void ValidateRoundTimes(DateTimeOffset? startTime, DateTimeOffset? endTime, DateTimeOffset? startSubmission, DateTimeOffset? endSubmission)
    {
        if (startTime.HasValue && endTime.HasValue && startTime.Value > endTime.Value)
        {
            throw new BadRequestException("INVALID_ROUND_TIME_RANGE");
        }

        if (startSubmission.HasValue != endSubmission.HasValue)
        {
            throw new BadRequestException("SUBMISSION_TIME_RANGE_REQUIRED");
        }

        if (startSubmission.HasValue && endSubmission.HasValue && startSubmission.Value > endSubmission.Value)
        {
            throw new BadRequestException("INVALID_SUBMISSION_TIME_RANGE");
        }

        if (startTime.HasValue && endTime.HasValue && startSubmission.HasValue && endSubmission.HasValue
            && (startSubmission.Value < startTime.Value || endSubmission.Value > endTime.Value))
        {
            throw new BadRequestException("SUBMISSION_TIME_OUTSIDE_ROUND_TIME");
        }
    }

    private static void ValidateLimitTeam(int? limitTeam)
    {
        if (limitTeam.HasValue && limitTeam.Value <= 0)
        {
            throw new BadRequestException("LIMIT_TEAM_MUST_BE_POSITIVE");
        }
    }

    private async Task<List<AdminUserResponse>> BuildUserQuery(IQueryable<Hackathon.Repository.Entity.Users> q, PaginationRequest pagination)
    {
        return await q
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pagination.PageIndex - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .Select(x => new AdminUserResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                AvatarUrl = x.AvatarUrl,
                StudentId = x.StudentId,
                College = x.College,
                Role = x.Role,
                Status = x.Status,
                IsVerified = x.IsVerified,
                IsDisable = x.IsDisable,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
    }
}
