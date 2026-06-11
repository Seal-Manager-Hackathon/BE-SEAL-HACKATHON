using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.RegisterTeams;

public class Service : IService
{
    private const string ActiveMemberStatus = "Active";
    private const string PendingStatus = "Pending";
    private const string ApprovedStatus = "Approved";
    private const string RejectedStatus = "Rejected";
    private const string UnreadNotificationStatus = "Unread";
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

    private static bool IsProfileCompleted(Hackathon.Repository.Entity.Users user)
    {
        return !string.IsNullOrWhiteSpace(user.FirstName)
               && !string.IsNullOrWhiteSpace(user.LastName)
               && !string.IsNullOrWhiteSpace(user.Email)
               && !string.IsNullOrWhiteSpace(user.PhoneNumber)
               && !string.IsNullOrWhiteSpace(user.StudentId)
               && !string.IsNullOrWhiteSpace(user.College)
               && !string.IsNullOrWhiteSpace(user.HashPassword);
    }

    private static Response.RegisterTeamResponse ToRegisterTeamResponse(
        Hackathon.Repository.Entity.RegisterTeams registerTeam,
        string message)
    {
        return new Response.RegisterTeamResponse
        {
            Id = registerTeam.Id,
            TeamId = registerTeam.TeamId,
            TeamName = registerTeam.Team?.Name,
            TopicId = registerTeam.TopicId,
            TopicTitle = registerTeam.Topic?.Title,
            EventId = registerTeam.Topic?.Track?.EventId ?? Guid.Empty,
            EventName = registerTeam.Topic?.Track?.Event?.Name,
            Description = registerTeam.Description,
            Status = registerTeam.Status,
            RejectionReason = registerTeam.RejectionReason,
            IsBanned = registerTeam.IsBanned,
            CreatedAt = registerTeam.CreatedAt,
            UpdatedAt = registerTeam.UpdatedAt,
            Message = message,
        };
    }

    private async Task EnsureStaffAssignedToEvent(Guid eventId, Guid userId)
    {
        var isAssigned = await _dbContext.AssignEvents.AnyAsync(x =>
            x.EventId == eventId
            && x.UserId == userId
            && !x.IsDisable);

        if (!isAssigned)
        {
            throw new ForbiddenException("STAFF_NOT_ASSIGNED_TO_EVENT");
        }
    }

    private async Task<Guid> GetTeamLeaderId(Guid teamId)
    {
        var leaderId = await _dbContext.TeamDetails
            .Where(x => x.TeamId == teamId && x.IsLeader && !x.IsDisable)
            .Select(x => x.UserId)
            .FirstOrDefaultAsync();

        if (leaderId == Guid.Empty)
        {
            throw new NotFoundException("TEAM_LEADER_NOT_FOUND");
        }

        return leaderId;
    }

    /// <summary>
    /// Team leader gửi đơn đăng ký team tham gia event thông qua topic, tạo hoặc gửi lại đơn ở trạng thái Pending và khóa tạm thời member của team.
    /// Các lỗi có thể xảy ra: thiếu access token -> MISSING_ACCESS_TOKEN; token không hợp lệ -> INVALID_ACCESS_TOKEN;
    /// team/topic/event không tồn tại -> TEAM_NOT_FOUND/TOPIC_NOT_FOUND/EVENT_NOT_FOUND; người gọi không phải leader -> ONLY_TEAM_LEADER_CAN_REGISTER_TEAM;
    /// team đang bị khóa -> TEAM_MEMBER_LOCKED; hết hạn đăng ký -> EVENT_REGISTRATION_CLOSED; số lượng member không hợp lệ -> TEAM_MEMBER_COUNT_NOT_VALID;
    /// member thiếu profile -> TEAM_MEMBER_PROFILE_NOT_COMPLETED; member đã tham gia team khác cùng event -> MEMBER_ALREADY_REGISTERED_IN_EVENT;
    /// team đã có đơn Pending/Approved trong event -> TEAM_ALREADY_REGISTERED_IN_EVENT.
    /// </summary>
    public async Task<Response.RegisterTeamResponse> RegisterTeamForEvent(Request.RegisterTeamRequest request)
    {
        var userId = GetCurrentUserId();
        if (request.TeamId == Guid.Empty)
        {
            throw new BadRequestException("TEAM_ID_REQUIRED");
        }

        if (request.TopicId == Guid.Empty)
        {
            throw new BadRequestException("TOPIC_ID_REQUIRED");
        }

        var team = await _dbContext.Teams.FirstOrDefaultAsync(x => x.Id == request.TeamId && !x.IsDisable);
        if (team == null)
        {
            throw new NotFoundException("TEAM_NOT_FOUND");
        }

        if (!team.CanEdit)
        {
            throw new ForbiddenException("TEAM_MEMBER_LOCKED");
        }

        var isLeader = await _dbContext.TeamDetails.AnyAsync(x =>
            x.TeamId == request.TeamId
            && x.UserId == userId
            && x.IsLeader
            && !x.IsDisable
            && x.Status == ActiveMemberStatus);
        if (!isLeader)
        {
            throw new ForbiddenException("ONLY_TEAM_LEADER_CAN_REGISTER_TEAM");
        }

        var topic = await _dbContext.Topics
            .Include(x => x.Track)
            .ThenInclude(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == request.TopicId && !x.IsDisable);
        if (topic == null)
        {
            throw new NotFoundException("TOPIC_NOT_FOUND");
        }

        var @event = topic.Track?.Event;
        if (@event == null || @event.IsDisable)
        {
            throw new NotFoundException("EVENT_NOT_FOUND");
        }

        if (@event.RegisterLimitTime.HasValue && @event.RegisterLimitTime.Value < DateTimeOffset.UtcNow)
        {
            throw new BadRequestException("EVENT_REGISTRATION_CLOSED");
        }

        var members = await _dbContext.TeamDetails
            .Include(x => x.User)
            .Where(x => x.TeamId == request.TeamId && !x.IsDisable && x.Status == ActiveMemberStatus)
            .ToListAsync();

        var minMember = @event.MinMember ?? 1;
        var maxMember = @event.MaxMember ?? int.MaxValue;
        if (members.Count < minMember || members.Count > maxMember)
        {
            throw new BadRequestException("TEAM_MEMBER_COUNT_NOT_VALID");
        }

        if (members.Any(x => !IsProfileCompleted(x.User)))
        {
            throw new BadRequestException("TEAM_MEMBER_PROFILE_NOT_COMPLETED");
        }

        var memberIds = members.Select(x => x.UserId).ToList();
        var memberAlreadyRegistered = await _dbContext.RegisterTeams
            .Include(x => x.Topic)
            .ThenInclude(x => x.Track)
            .Include(x => x.Team)
            .ThenInclude(x => x.TeamDetails)
            .AnyAsync(x => !x.IsDisable
                           && x.TeamId != request.TeamId
                           && (x.Status == PendingStatus || x.Status == ApprovedStatus)
                           && x.Topic.Track.EventId == @event.Id
                           && x.Team.TeamDetails.Any(td => memberIds.Contains(td.UserId) && !td.IsDisable && td.Status == ActiveMemberStatus));
        if (memberAlreadyRegistered)
        {
            throw new ConflictException("MEMBER_ALREADY_REGISTERED_IN_EVENT");
        }

        var existingRegistrations = await _dbContext.RegisterTeams
            .Include(x => x.Topic)
            .ThenInclude(x => x.Track)
            .Where(x => !x.IsDisable && x.TeamId == request.TeamId && x.Topic.Track.EventId == @event.Id)
            .ToListAsync();

        if (existingRegistrations.Any(x => x.Status == PendingStatus || x.Status == ApprovedStatus))
        {
            throw new ConflictException("TEAM_ALREADY_REGISTERED_IN_EVENT");
        }

        var now = DateTimeOffset.UtcNow;
        var rejectedRegistration = existingRegistrations.FirstOrDefault(x => x.Status == RejectedStatus);
        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            if (rejectedRegistration == null)
            {
                rejectedRegistration = new Hackathon.Repository.Entity.RegisterTeams
                {
                    Id = Guid.NewGuid(),
                    TeamId = request.TeamId,
                    TopicId = request.TopicId,
                    Description = request.Description,
                    Status = PendingStatus,
                    IsBanned = false,
                    CreatedAt = now,
                    UpdatedAt = now,
                };
                await _dbContext.RegisterTeams.AddAsync(rejectedRegistration);
            }
            else
            {
                rejectedRegistration.TopicId = request.TopicId;
                rejectedRegistration.Description = request.Description;
                rejectedRegistration.RejectionReason = null;
                rejectedRegistration.Status = PendingStatus;
                rejectedRegistration.IsBanned = false;
                rejectedRegistration.UpdatedAt = now;
            }

            team.CanEdit = false;
            team.UpdatedAt = now;
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        rejectedRegistration.Team = team;
        rejectedRegistration.Topic = topic;
        return ToRegisterTeamResponse(rejectedRegistration, "REGISTER_TEAM_SUBMITTED_SUCCESSFULLY");
    }

    /// <summary>
    /// Team member xem trạng thái đơn đăng ký của team, bao gồm lý do từ chối nếu đơn bị Rejected.
    /// Các lỗi có thể xảy ra: thiếu access token -> MISSING_ACCESS_TOKEN; token không hợp lệ -> INVALID_ACCESS_TOKEN;
    /// đơn không tồn tại -> REGISTER_TEAM_NOT_FOUND; user không thuộc team của đơn -> REGISTER_TEAM_NOT_VISIBLE_TO_USER.
    /// </summary>
    public async Task<Response.RegisterTeamResponse> GetMyRegistrationStatus(Guid registerTeamId)
    {
        var userId = GetCurrentUserId();
        var registerTeam = await _dbContext.RegisterTeams
            .Include(x => x.Team)
            .Include(x => x.Topic)
            .ThenInclude(x => x.Track)
            .ThenInclude(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == registerTeamId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        var canView = await _dbContext.TeamDetails.AnyAsync(x =>
            x.TeamId == registerTeam.TeamId
            && x.UserId == userId
            && !x.IsDisable);
        if (!canView)
        {
            throw new ForbiddenException("REGISTER_TEAM_NOT_VISIBLE_TO_USER");
        }

        return ToRegisterTeamResponse(registerTeam, "REGISTER_TEAM_STATUS_RETRIEVED_SUCCESSFULLY");
    }

    /// <summary>
    /// Staff xem danh sách event mà mình được phân công quản lý/tham gia xử lý.
    /// Các lỗi có thể xảy ra: thiếu access token -> MISSING_ACCESS_TOKEN; token không hợp lệ -> INVALID_ACCESS_TOKEN.
    /// </summary>
    public async Task<List<Response.AssignedEventResponse>> GetAssignedEvents()
    {
        var userId = GetCurrentUserId();
        return await _dbContext.AssignEvents
            .Include(x => x.Event)
            .Include(x => x.EventRole)
            .Where(x => x.UserId == userId && !x.IsDisable && !x.Event.IsDisable)
            .Select(x => new Response.AssignedEventResponse
            {
                EventId = x.EventId,
                EventName = x.Event.Name,
                EventStatus = x.Event.Status,
                EventRole = x.EventRole.Name,
                RegisterLimitTime = x.Event.RegisterLimitTime,
            })
            .ToListAsync();
    }

    /// <summary>
    /// Staff xem danh sách các team đang Pending chờ duyệt trong event mà staff được phân công.
    /// Các lỗi có thể xảy ra: thiếu access token -> MISSING_ACCESS_TOKEN; token không hợp lệ -> INVALID_ACCESS_TOKEN;
    /// staff không được phân công event -> STAFF_NOT_ASSIGNED_TO_EVENT.
    /// </summary>
    public async Task<List<Response.PendingRegisterTeamResponse>> GetPendingTeamsByEvent(Guid eventId)
    {
        var userId = GetCurrentUserId();
        await EnsureStaffAssignedToEvent(eventId, userId);

        return await _dbContext.RegisterTeams
            .Include(x => x.Team)
            .ThenInclude(x => x.TeamDetails)
            .Include(x => x.Topic)
            .ThenInclude(x => x.Track)
            .Where(x => !x.IsDisable && x.Status == PendingStatus && x.Topic.Track.EventId == eventId)
            .Select(x => new Response.PendingRegisterTeamResponse
            {
                RegisterTeamId = x.Id,
                TeamId = x.TeamId,
                TeamName = x.Team.Name,
                TopicId = x.TopicId,
                TopicTitle = x.Topic.Title,
                MemberCount = x.Team.TeamDetails.Count(td => !td.IsDisable && td.Status == ActiveMemberStatus),
                Status = x.Status,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();
    }

    /// <summary>
    /// Staff xem chi tiết đơn đăng ký và thông tin từng member trong team để ra quyết định duyệt hoặc từ chối.
    /// Các lỗi có thể xảy ra: thiếu access token -> MISSING_ACCESS_TOKEN; token không hợp lệ -> INVALID_ACCESS_TOKEN;
    /// đơn không tồn tại -> REGISTER_TEAM_NOT_FOUND; staff không được phân công event -> STAFF_NOT_ASSIGNED_TO_EVENT.
    /// </summary>
    public async Task<Response.RegisterTeamDetailResponse> GetRegistrationDetailForReview(Guid registerTeamId)
    {
        var userId = GetCurrentUserId();
        var registerTeam = await _dbContext.RegisterTeams
            .Include(x => x.Team)
            .Include(x => x.Topic)
            .ThenInclude(x => x.Track)
            .ThenInclude(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == registerTeamId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        var eventId = registerTeam.Topic.Track.EventId;
        await EnsureStaffAssignedToEvent(eventId, userId);

        var members = await _dbContext.TeamDetails
            .Include(x => x.User)
            .Where(x => x.TeamId == registerTeam.TeamId && !x.IsDisable && x.Status == ActiveMemberStatus)
            .Select(x => new Response.TeamMemberDetailResponse
            {
                UserId = x.UserId,
                Email = x.User.Email,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                PhoneNumber = x.User.PhoneNumber,
                StudentId = x.User.StudentId,
                College = x.User.College,
                AvatarUrl = x.User.AvatarUrl,
                Bio = x.User.Bio,
                IsLeader = x.IsLeader,
                Status = x.Status,
            })
            .ToListAsync();

        return new Response.RegisterTeamDetailResponse
        {
            Id = registerTeam.Id,
            TeamId = registerTeam.TeamId,
            TeamName = registerTeam.Team.Name,
            TopicId = registerTeam.TopicId,
            TopicTitle = registerTeam.Topic.Title,
            TrackId = registerTeam.Topic.TrackId,
            TrackTitle = registerTeam.Topic.Track.Title,
            EventId = eventId,
            EventName = registerTeam.Topic.Track.Event.Name,
            Description = registerTeam.Description,
            Status = registerTeam.Status,
            RejectionReason = registerTeam.RejectionReason,
            IsBanned = registerTeam.IsBanned,
            CreatedAt = registerTeam.CreatedAt,
            UpdatedAt = registerTeam.UpdatedAt,
            Message = "REGISTER_TEAM_DETAIL_RETRIEVED_SUCCESSFULLY",
            Members = members,
        };
    }

    /// <summary>
    /// Staff duyệt đơn đăng ký Pending, chuyển trạng thái thành Approved, khóa cứng team và gửi thông báo cho leader.
    /// Các lỗi có thể xảy ra: thiếu access token -> MISSING_ACCESS_TOKEN; token không hợp lệ -> INVALID_ACCESS_TOKEN;
    /// đơn không tồn tại -> REGISTER_TEAM_NOT_FOUND; staff không được phân công event -> STAFF_NOT_ASSIGNED_TO_EVENT;
    /// đơn không còn Pending -> REGISTER_TEAM_NOT_PENDING; không tìm thấy leader -> TEAM_LEADER_NOT_FOUND.
    /// </summary>
    public async Task<Response.RegisterTeamResponse> ApproveRegistration(Guid registerTeamId)
    {
        var userId = GetCurrentUserId();
        var registerTeam = await _dbContext.RegisterTeams
            .Include(x => x.Team)
            .Include(x => x.Topic)
            .ThenInclude(x => x.Track)
            .ThenInclude(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == registerTeamId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        await EnsureStaffAssignedToEvent(registerTeam.Topic.Track.EventId, userId);
        if (registerTeam.Status != PendingStatus)
        {
            throw new ConflictException("REGISTER_TEAM_NOT_PENDING");
        }

        var leaderId = await GetTeamLeaderId(registerTeam.TeamId);
        var now = DateTimeOffset.UtcNow;
        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            registerTeam.Status = ApprovedStatus;
            registerTeam.RejectionReason = null;
            registerTeam.UpdatedAt = now;
            registerTeam.Team.CanEdit = false;
            registerTeam.Team.UpdatedAt = now;

            await _dbContext.Notifications.AddAsync(new Hackathon.Repository.Entity.Notifications
            {
                Id = Guid.NewGuid(),
                TeamId = registerTeam.TeamId,
                UserId = leaderId,
                Title = "REGISTER_TEAM_APPROVED",
                Status = UnreadNotificationStatus,
                Description = $"Đơn đăng ký tham gia event {registerTeam.Topic.Track.Event.Name} của team {registerTeam.Team.Name} đã được chấp nhận.",
                CreatedAt = now,
                UpdatedAt = now,
            });

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return ToRegisterTeamResponse(registerTeam, "REGISTER_TEAM_APPROVED_SUCCESSFULLY");
    }

    /// <summary>
    /// Staff từ chối đơn đăng ký Pending, bắt buộc ghi lý do, mở khóa team để chỉnh sửa/gửi lại và gửi thông báo cho leader.
    /// Các lỗi có thể xảy ra: thiếu access token -> MISSING_ACCESS_TOKEN; token không hợp lệ -> INVALID_ACCESS_TOKEN;
    /// đơn không tồn tại -> REGISTER_TEAM_NOT_FOUND; staff không được phân công event -> STAFF_NOT_ASSIGNED_TO_EVENT;
    /// đơn không còn Pending -> REGISTER_TEAM_NOT_PENDING; thiếu lý do từ chối -> REJECTION_REASON_REQUIRED; không tìm thấy leader -> TEAM_LEADER_NOT_FOUND.
    /// </summary>
    public async Task<Response.RegisterTeamResponse> RejectRegistration(Guid registerTeamId, Request.RejectRegistrationRequest request)
    {
        var userId = GetCurrentUserId();
        var reason = request.Reason?.Trim();
        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new BadRequestException("REJECTION_REASON_REQUIRED");
        }

        var registerTeam = await _dbContext.RegisterTeams
            .Include(x => x.Team)
            .Include(x => x.Topic)
            .ThenInclude(x => x.Track)
            .ThenInclude(x => x.Event)
            .FirstOrDefaultAsync(x => x.Id == registerTeamId && !x.IsDisable);

        if (registerTeam == null)
        {
            throw new NotFoundException("REGISTER_TEAM_NOT_FOUND");
        }

        await EnsureStaffAssignedToEvent(registerTeam.Topic.Track.EventId, userId);
        if (registerTeam.Status != PendingStatus)
        {
            throw new ConflictException("REGISTER_TEAM_NOT_PENDING");
        }

        var leaderId = await GetTeamLeaderId(registerTeam.TeamId);
        var now = DateTimeOffset.UtcNow;
        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            registerTeam.Status = RejectedStatus;
            registerTeam.RejectionReason = reason;
            registerTeam.UpdatedAt = now;
            registerTeam.Team.CanEdit = true;
            registerTeam.Team.UpdatedAt = now;

            await _dbContext.Notifications.AddAsync(new Hackathon.Repository.Entity.Notifications
            {
                Id = Guid.NewGuid(),
                TeamId = registerTeam.TeamId,
                UserId = leaderId,
                Title = "REGISTER_TEAM_REJECTED",
                Status = UnreadNotificationStatus,
                Description = $"Đơn đăng ký tham gia event {registerTeam.Topic.Track.Event.Name} của team {registerTeam.Team.Name} đã bị từ chối. Lý do: {reason}",
                CreatedAt = now,
                UpdatedAt = now,
            });

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return ToRegisterTeamResponse(registerTeam, "REGISTER_TEAM_REJECTED_SUCCESSFULLY");
    }
}
