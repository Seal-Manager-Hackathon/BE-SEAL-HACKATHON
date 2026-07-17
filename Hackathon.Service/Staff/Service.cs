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
        // Staff and Admin can see all reports
        return _dbContext.Reports.AsNoTracking().Where(x => !x.IsDisable);
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
                UserId = x.UserId,
                UserName = x.User.FirstName + " " + x.User.LastName,
                Title = x.Title,
                Description = x.Description,
                TypeReport = x.TypeReport,
                Status = x.Status,
                StatusName = x.Status.ToString(),
                Reason = x.Reason,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .FirstOrDefaultAsync();

        return report ?? throw new NotFoundException("REPORT_NOT_FOUND");
    }

    public async Task<Response.ApproveRegradeResponse> ApproveRegrade(Guid reportId)
    {
        var userId = GetCurrentUserId();
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null || (user.Role != RoleEnum.Staff && user.Role != RoleEnum.Admin))
        {
            throw new ForbiddenException("FORBIDDEN");
        }

        var report = await _dbContext.Reports
            .FirstOrDefaultAsync(x => x.Id == reportId && !x.IsDisable);
        if (report == null)
        {
            throw new NotFoundException("REPORT_NOT_FOUND");
        }

        if (report.Status != ReportStatusEnum.Pending)
        {
            throw new BadRequestException("REPORT_ALREADY_PROCESSED");
        }

        if (report.TypeReport != "RegradeRequest")
        {
            throw new BadRequestException("REPORT_NOT_REGRADE_REQUEST");
        }

        // Find submission for this user's team that has existing scores
        var submission = await _dbContext.Submissions
            .Include(x => x.RoundDetail)
                .ThenInclude(x => x.RegisterTeam)
            .Include(x => x.Scores)
            .FirstOrDefaultAsync(x =>
                !x.IsDisable &&
                x.RoundDetail.RegisterTeam.Team.TeamDetails.Any(td => td.UserId == report.UserId && !td.IsDisable) &&
                x.Scores.Any(s => !s.IsDisable && !s.IsMock));

        if (submission == null)
        {
            throw new BadRequestException("NO_ELIGIBLE_SUBMISSION_FOR_REGRADE");
        }

        var now = DateTimeOffset.UtcNow;
        report.Status = ReportStatusEnum.Resolved;
        report.Reason = "Regrade approved by staff";
        report.UpdatedAt = now;

        submission.IsRegrade = true;
        submission.UpdatedAt = now;

        await _dbContext.SaveChangesAsync();

        return new Response.ApproveRegradeResponse
        {
            ReportId = report.Id,
            Status = ReportStatusEnum.Resolved,
            StatusName = ReportStatusEnum.Resolved.ToString(),
            IsRegrade = true
        };
    }

    public async Task UpdateReportStatus(Guid reportId, Request.UpdateReportStatusRequest request)
    {
        var userId = GetCurrentUserId();
        var report = await _dbContext.Reports
            .FirstOrDefaultAsync(x => x.Id == reportId && !x.IsDisable);

        if (report == null)
        {
            throw new NotFoundException("REPORT_NOT_FOUND");
        }

        if (report.Status == ReportStatusEnum.Resolved || report.Status == ReportStatusEnum.Canceled)
        {
            throw new BadRequestException("CANNOT_MODIFY_RESOLVED_REPORT");
        }

        report.Status = request.Status;
        report.Reason = request.Reason?.Trim();
        report.UpdatedAt = DateTimeOffset.UtcNow;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<BasePaginationResponse> GetRegradeSubmissions(Request.GetRegradeSubmissionsRequest request)
    {
        // Regrade submissions are now handled through the submission's IsRegrade flag
        // Reports no longer link directly to submissions
        return ApiResponseFactory.BasePagination(new List<object>(), 1, 10, 0);
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
