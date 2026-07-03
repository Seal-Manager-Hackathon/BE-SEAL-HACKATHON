using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Hackathon.Repository;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Staff;

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

    public async Task<List<Response.StaffEventResponse>> GetCurrentStaffEvents()
    {
        var userId = GetCurrentUserId();

        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null || user.Role != RoleEnum.Staff)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        var now = DateTimeOffset.UtcNow;

        var items = await _dbContext.AssignEvents
            .AsNoTracking()
            .Include(x => x.Event)
            .Include(x => x.EventRole)
            .Where(x => x.UserId == userId
                        && !x.IsDisable
                        && x.Event.Status == EventStatusEnum.Published
                        && x.Event.StartTime.HasValue
                        && x.Event.StartTime.Value <= now
                        && x.Event.EndTime.HasValue
                        && x.Event.EndTime.Value >= now)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new Response.StaffEventResponse
            {
                AssignEventId = x.Id,
                EventId = x.EventId,
                EventName = x.Event.Name,
                Season = x.Event.Season,
                StartTime = x.Event.StartTime,
                EndTime = x.Event.EndTime,
                Role = x.EventRole != null ? (EventRoleEnum?)x.EventRole.Name : null,
                EventStatus = x.Event.Status
            })
            .ToListAsync();

        if (items.Count == 0)
        {
            throw new NotFoundException("NOT_ASSIGNED_TO_ANY_EVENT");
        }

        return items;
    }

    public async Task<BasePaginationResponse> GetStaffEvents(PaginationRequest request)
    {
        var userId = GetCurrentUserId();

        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null || user.Role != RoleEnum.Staff)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        var pageIndex = request.PageIndex <= 0 ? 1 : request.PageIndex;
        var pageSize = request.PageSize <= 0 ? 10 : Math.Min(request.PageSize, 100);

        var query = _dbContext.AssignEvents
            .AsNoTracking()
            .Include(x => x.Event)
            .Include(x => x.EventRole)
            .Where(x => x.UserId == userId
                        && !x.IsDisable
                        && x.Event.Status == EventStatusEnum.Published);

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.Event.StartTime)
            .ThenByDescending(x => x.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.StaffEventResponse
            {
                AssignEventId = x.Id,
                EventId = x.EventId,
                EventName = x.Event.Name,
                Season = x.Event.Season,
                StartTime = x.Event.StartTime,
                EndTime = x.Event.EndTime,
                Role = x.EventRole != null ? (EventRoleEnum?)x.EventRole.Name : null,
                EventStatus = x.Event.Status
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
    }

    private async Task<bool> IsAdmin(Guid userId)
    {
        return await _dbContext.Users.AsNoTracking().AnyAsync(x => x.Id == userId && !x.IsDisable && x.Role == RoleEnum.Admin);
    }

    private async Task EnsureCanAccessEvent(Guid userId, Guid eventId)
    {
        if (await IsAdmin(userId))
        {
            return;
        }

        var assigned = await _dbContext.AssignEvents.AnyAsync(x =>
            !x.IsDisable &&
            x.UserId == userId &&
            x.EventId == eventId &&
            x.EventRole != null &&
            x.EventRole.Name == EventRoleEnum.Staff);

        if (!assigned)
        {
            throw new ForbiddenException("STAFF_NOT_ASSIGNED_TO_EVENT");
        }
    }

    private IQueryable<Hackathon.Repository.Entity.Reports> ReportsForUser(Guid userId)
    {
        var query = _dbContext.Reports.AsNoTracking().Where(x => !x.IsDisable);

        var isAdmin = _dbContext.Users.Any(x => x.Id == userId && !x.IsDisable && x.Role == RoleEnum.Admin);
        if (isAdmin)
        {
            return query;
        }

        return query.Where(x => _dbContext.AssignEvents.Any(a =>
            !a.IsDisable &&
            a.UserId == userId &&
            a.EventId == x.AssignEvent.EventId &&
            a.EventRole != null &&
            a.EventRole.Name == EventRoleEnum.Staff));
    }

    public async Task<BasePaginationResponse> SearchStaffEvents(Request.SearchStaffEventsRequest request)
    {
        var userId = GetCurrentUserId();

        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null || user.Role != RoleEnum.Staff)
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        var pageIndex = request.PageIndex <= 0 ? 1 : request.PageIndex;
        var pageSize = request.PageSize <= 0 ? 10 : Math.Min(request.PageSize, 100);

        var query = _dbContext.AssignEvents
            .AsNoTracking()
            .Include(x => x.Event)
            .Include(x => x.EventRole)
            .Where(x => x.UserId == userId
                        && !x.IsDisable
                        && !x.Event.IsDisable
                        && x.Event.Status != EventStatusEnum.Draft);

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var normKeyword = request.Keyword.Trim().ToLower();
            query = query.Where(x => x.Event.Name.ToLower().Contains(normKeyword));
        }

        if (request.Status.HasValue)
        {
            query = query.Where(x => x.Event.Status == request.Status.Value);
        }

        if (request.Year.HasValue || request.Month.HasValue)
        {
            var year = request.Year ?? DateTimeOffset.UtcNow.Year;
            DateTimeOffset startDate;
            DateTimeOffset endDate;

            if (request.Month.HasValue)
            {
                startDate = new DateTimeOffset(year, request.Month.Value, 1, 0, 0, 0, TimeSpan.Zero);
                endDate = startDate.AddMonths(1);
            }
            else
            {
                startDate = new DateTimeOffset(year, 1, 1, 0, 0, 0, TimeSpan.Zero);
                endDate = startDate.AddYears(1);
            }

            query = query.Where(x => x.Event.StartTime.HasValue
                        && x.Event.StartTime.Value >= startDate
                        && x.Event.EndTime.HasValue
                        && x.Event.EndTime.Value < endDate);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.Event.StartTime)
            .ThenBy(x => x.Event.Name)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.StaffEventResponse
            {
                AssignEventId = x.Id,
                EventId = x.EventId,
                EventName = x.Event.Name,
                Season = x.Event.Season,
                StartTime = x.Event.StartTime,
                EndTime = x.Event.EndTime,
                Role = x.EventRole != null ? (EventRoleEnum?)x.EventRole.Name : null,
                EventStatus = x.Event.Status
            })
            .ToListAsync();

        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
    }

    public async Task<BasePaginationResponse> GetReports(Request.GetStaffReportsRequest request)
    {
        var userId = GetCurrentUserId();
        var pageIndex = request.PageIndex <= 0 ? 1 : request.PageIndex;
        var pageSize = request.PageSize <= 0 ? 10 : Math.Min(request.PageSize, 100);

        var query = ReportsForUser(userId);

        if (request.Status.HasValue)
        {
            query = query.Where(x => x.Status == request.Status.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.TypeReport))
        {
            var typeReport = request.TypeReport.Trim().ToLower();
            query = query.Where(x => x.TypeReport != null && x.TypeReport.ToLower() == typeReport);
        }

        if (request.EventId.HasValue)
        {
            query = query.Where(x => x.AssignEvent.EventId == request.EventId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var keyword = request.Keyword.Trim().ToLower();
            query = query.Where(x => x.Title != null && x.Title.ToLower().Contains(keyword));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new Response.StaffReportListItemResponse
            {
                ReportId = x.Id,
                SubmissionId = x.SubmissionId,
                TeamName = x.Submission != null ? x.Submission.RoundDetail.RegisterTeam.Team.Name : null,
                EventName = x.AssignEvent != null ? x.AssignEvent.Event.Name : null,
                Title = x.Title,
                TypeReport = x.TypeReport,
                Status = x.Status,
                StatusName = x.Status.ToString(),
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
 
        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
    }
 
    public async Task<Response.StaffReportDetailResponse> GetReportDetail(Guid reportId)
    {
        var userId = GetCurrentUserId();
        var report = await ReportsForUser(userId)
            .Where(x => x.Id == reportId)
            .Select(x => new Response.StaffReportDetailResponse
            {
                ReportId = x.Id,
                SubmissionId = x.SubmissionId,
                UserId = x.UserId,
                UserName = x.User.FirstName + " " + x.User.LastName,
                AssignEventId = x.AssignEventId,
                EventName = x.AssignEvent != null ? x.AssignEvent.Event.Name : null,
                TeamId = x.Submission != null ? (Guid?)x.Submission.RoundDetail.RegisterTeam.TeamId : null,
                TeamName = x.Submission != null ? x.Submission.RoundDetail.RegisterTeam.Team.Name : null,
                RoundId = x.Submission != null ? (Guid?)x.Submission.RoundDetail.RoundId : null,
                RoundNo = x.Submission != null ? x.Submission.RoundDetail.Round.RoundNo : null,
                Title = x.Title,
                Description = x.Description,
                ImgUrl = x.ImgUrl,
                FileUrl = x.FileUrl,
                TypeReport = x.TypeReport,
                Status = x.Status,
                StatusName = x.Status.ToString(),
                Reason = x.Reason,
                IsRegrade = x.Submission != null ? x.Submission.IsRegrade : false,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .FirstOrDefaultAsync();

        return report ?? throw new NotFoundException("REPORT_NOT_FOUND");
    }

    public async Task<Response.ApproveRegradeResponse> ApproveRegrade(Guid reportId)
    {
        var userId = GetCurrentUserId();
        var report = await _dbContext.Reports
            .Include(x => x.AssignEvent)
            .Include(x => x.Submission)
            .FirstOrDefaultAsync(x => x.Id == reportId && !x.IsDisable);

        if (report == null)
        {
            throw new NotFoundException("REPORT_NOT_FOUND");
        }

        await EnsureCanAccessEvent(userId, report.AssignEvent.EventId);

        if (report.Status != ReportStatusEnum.Open)
        {
            throw new BadRequestException(report.Status == ReportStatusEnum.Closed ? "REPORT_ALREADY_CLOSED" : "REPORT_MUST_BE_OPEN");
        }

        if (!string.Equals(report.TypeReport, "Phúc khảo", StringComparison.OrdinalIgnoreCase))
        {
            throw new BadRequestException("NOT_APPEAL_TYPE_REPORT");
        }

        if (report.SubmissionId == null)
        {
            throw new BadRequestException("REPORT_NOT_LINKED_TO_SUBMISSION");
        }
 
        if (report.Submission == null)
        {
            throw new NotFoundException("SUBMISSION_NOT_FOUND");
        }
 
        if (report.Submission.IsDisable)
        {
            throw new NotFoundException("SUBMISSION_NOT_FOUND");
        }
 
        if (report.Submission.IsRegrade)
        {
            throw new ConflictException("SUBMISSION_ALREADY_IN_REGRADE");
        }
 
        var hasSourceScore = await _dbContext.Scores.AnyAsync(x =>
            !x.IsDisable &&
            !x.IsMock &&
            !x.IsRetake &&
            x.SubmissionId == report.SubmissionId.Value);
 
        if (!hasSourceScore)
        {
            throw new BadRequestException("SUBMISSION_NOT_GRADED");
        }
 
        report.Status = ReportStatusEnum.Approved;
        report.UpdatedAt = DateTimeOffset.UtcNow;
        report.Submission.IsRegrade = true;
        report.Submission.UpdatedAt = report.UpdatedAt;
 
        await _dbContext.SaveChangesAsync();
 
        return new Response.ApproveRegradeResponse
        {
            ReportId = report.Id,
            SubmissionId = report.SubmissionId,
            Status = ReportStatusEnum.Approved,
            StatusName = ReportStatusEnum.Approved.ToString(),
            IsRegrade = true
        };
    }
 
    public async Task UpdateReportStatus(Guid reportId, Request.UpdateReportStatusRequest request)
    {
        var userId = GetCurrentUserId();
        var report = await _dbContext.Reports
            .Include(x => x.AssignEvent)
            .Include(x => x.Submission)
            .FirstOrDefaultAsync(x => x.Id == reportId && !x.IsDisable);
 
        if (report == null)
        {
            throw new NotFoundException("REPORT_NOT_FOUND");
        }
 
        if (report.AssignEvent == null)
        {
            throw new BadRequestException("REPORT_NOT_LINKED_TO_EVENT");
        }
 
        await EnsureCanAccessEvent(userId, report.AssignEvent.EventId);
 
        if (request.Status == ReportStatusEnum.Approved)
        {
            throw new BadRequestException("CANNOT_SET_APPROVED_DIRECTLY");
        }
 
        if (report.Status == ReportStatusEnum.Closed)
        {
            throw new BadRequestException("CANNOT_REOPEN_CLOSED_REPORT");
        }
 
        if (request.Status != ReportStatusEnum.Closed)
        {
            throw new BadRequestException("CANNOT_REOPEN_CLOSED_REPORT");
        }
 
        if (string.IsNullOrWhiteSpace(request.Reason))
        {
            throw new BadRequestException("REASON_REQUIRED_WHEN_CLOSING");
        }
 
        if (report.Status == ReportStatusEnum.Approved)
        {
            if (report.SubmissionId == null || !await IsRegradeCompleted(report.SubmissionId.Value))
            {
                throw new BadRequestException("REGRADE_NOT_COMPLETED");
            }
        }
 
        report.Status = ReportStatusEnum.Closed;
        report.Reason = request.Reason.Trim();
        report.UpdatedAt = DateTimeOffset.UtcNow;
 
        await _dbContext.SaveChangesAsync();
    }

    public async Task<BasePaginationResponse> GetRegradeSubmissions(Request.GetRegradeSubmissionsRequest request)
    {
        var userId = GetCurrentUserId();
        var pageIndex = request.PageIndex <= 0 ? 1 : request.PageIndex;
        var pageSize = request.PageSize <= 0 ? 10 : Math.Min(request.PageSize, 100);

        var query = ReportsForUser(userId)
            .Where(x => x.Status == ReportStatusEnum.Approved && x.Submission.IsRegrade && !x.Submission.IsDisable);

        if (request.EventId.HasValue)
        {
            query = query.Where(x => x.AssignEvent.EventId == request.EventId.Value);
        }

        if (request.TrackId.HasValue)
        {
            query = query.Where(x => x.Submission.RoundDetail.RegisterTeam.TrackId == request.TrackId.Value);
        }

        var rawReports = await query
            .Include(x => x.AssignEvent).ThenInclude(x => x.Event)
            .Include(x => x.Submission).ThenInclude(x => x.RoundDetail).ThenInclude(x => x.Round)
            .Include(x => x.Submission).ThenInclude(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Team)
            .Include(x => x.Submission).ThenInclude(x => x.RoundDetail).ThenInclude(x => x.RegisterTeam).ThenInclude(x => x.Track)
            .Include(x => x.Submission).ThenInclude(x => x.Scores).ThenInclude(x => x.RetakeScores)
            .Include(x => x.Submission).ThenInclude(x => x.Scores).ThenInclude(x => x.AssignTrack).ThenInclude(x => x.AssignEvent).ThenInclude(x => x.User)
            .OrderByDescending(x => x.UpdatedAt)
            .ToListAsync();

        var regradeItems = rawReports.Select(report =>
        {
            var sourceScores = report.Submission.Scores
                .Where(score => !score.IsDisable && !score.IsMock && !score.IsRetake)
                .Select(score => new
                {
                    Score = score,
                    Retake = score.RetakeScores
                        .Where(retake => !retake.IsDisable && retake.IsRetake)
                        .OrderByDescending(retake => retake.UpdatedAt)
                        .FirstOrDefault()
                })
                .ToList();

            var retakeCount = sourceScores.Count(score => score.Retake != null);
            var regradeStatus = retakeCount == 0
                ? "PendingRegrade"
                : retakeCount == sourceScores.Count ? "RegradeCompleted" : "PartiallyRegraded";

            return new Response.StaffRegradeSubmissionResponse
            {
                SubmissionId = report.SubmissionId,
                RoundDetailId = report.Submission != null ? report.Submission.RoundDetailId : Guid.Empty,
                RoundName = report.Submission != null ? report.Submission.RoundDetail.Round.Name : string.Empty,
                RoundNo = report.Submission != null ? report.Submission.RoundDetail.Round.RoundNo : null,
                TeamId = report.Submission != null ? report.Submission.RoundDetail.RegisterTeam.TeamId : Guid.Empty,
                TeamName = report.Submission != null ? report.Submission.RoundDetail.RegisterTeam.Team.Name : string.Empty,
                TrackId = report.Submission != null ? report.Submission.RoundDetail.RegisterTeam.TrackId : null,
                TrackTitle = report.Submission != null ? report.Submission.RoundDetail.RegisterTeam.Track?.Title : null,
                EventId = report.AssignEvent != null ? report.AssignEvent.EventId : Guid.Empty,
                EventName = report.AssignEvent != null ? report.AssignEvent.Event.Name : string.Empty,
                ReportId = report.Id,
                ReportTitle = report.Title,
                RegradeStatus = regradeStatus,
                ApprovedAt = report.UpdatedAt,
                SourceScores = sourceScores.Select(score => new Response.SourceScoreRegradeResponse
                {
                    ScoreId = score.Score.Id,
                    JudgeId = score.Score.AssignTrack.AssignEvent.UserId,
                    JudgeName = score.Score.AssignTrack.AssignEvent.User.FirstName + " " + score.Score.AssignTrack.AssignEvent.User.LastName,
                    TotalScore = score.Score.TotalScore,
                    HasRegraded = score.Retake != null,
                    RegradeScoreId = score.Retake?.Id,
                    RegradeTotalScore = score.Retake?.TotalScore,
                    RegradedAt = score.Retake?.UpdatedAt
                }).ToList()
            };
        });

        if (!string.IsNullOrWhiteSpace(request.RegradeStatus) && !string.Equals(request.RegradeStatus, "All", StringComparison.OrdinalIgnoreCase))
        {
            var status = request.RegradeStatus.Trim();
            if (status is not ("PendingRegrade" or "PartiallyRegraded" or "RegradeCompleted"))
            {
                throw new BadRequestException("QUERY_PARAMETER_INVALID");
            }

            regradeItems = regradeItems.Where(x => x.RegradeStatus == status);
        }

        var itemList = regradeItems.ToList();
        var totalCount = itemList.Count;
        var items = itemList
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return ApiResponseFactory.BasePagination(items, pageIndex, pageSize, totalCount);
    }

    private async Task<bool> IsRegradeCompleted(Guid submissionId)
    {
        var sourceScores = await _dbContext.Scores
            .AsNoTracking()
            .Where(x => !x.IsDisable && !x.IsMock && !x.IsRetake && x.SubmissionId == submissionId)
            .Select(x => new
            {
                x.Id,
                HasRetake = x.RetakeScores.Any(r => !r.IsDisable && r.IsRetake)
            })
            .ToListAsync();

        return sourceScores.Count > 0 && sourceScores.All(x => x.HasRetake);
    }

    public async Task<string> ChangeUserRole(Guid userId, Request.StaffChangeUserRoleRequest request)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        // Staff can only set Student or Lecturer, cannot promote to Admin/Staff
        if (request.Role is RoleEnum.Admin or RoleEnum.Staff)
        {
            throw new ForbiddenException("FORBIDDEN");
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
}
