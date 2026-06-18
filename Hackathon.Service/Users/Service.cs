using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Users;

public class Service : IService
{
    public readonly AppDbContext _dbContext;
    public readonly IHttpContextAccessor _IhttpContex;
    public Service(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _IhttpContex = httpContextAccessor;
    }

    
    public async Task<Reponse.UserProfileDetailResponse> GetProfileUser()
    {
        var userId = GetUserId();
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
        if(user == null) throw new NotFoundException("USER_NOT_FOUND");

        return new Reponse.UserProfileDetailResponse
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            AvatarUrl = user.AvatarUrl,
            Bio = user.Bio,
            Address = user.Address,
            DateOfBirth = user.DateOfBirth,
            StudentId = user.StudentId,
            College = user.College,
            ImgUrl = user.ImgUrl,
            LinkUrl = user.LinkUrl,
            Status = user.Status,
            BanReason = user.BanReason
        };
    }

    public async Task<string> UpdateProfile(Request.UpdateProfileRequest request)
    {
        var userId = GetUserId();
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
            throw new NotFoundException("USER_NOT_FOUND");

        if (request.FirstName != null) user.FirstName = request.FirstName;
        if (request.LastName != null) user.LastName = request.LastName;
        if (request.PhoneNumber != null) user.PhoneNumber = request.PhoneNumber;
        if (request.AvatarUrl != null) user.AvatarUrl = request.AvatarUrl;
        if (request.Bio != null) user.Bio = request.Bio;
        if (request.Address != null) user.Address = request.Address;
        if (request.StudentId != null) user.StudentId = request.StudentId;
        if (request.College != null) user.College = request.College;

        
        user.UpdatedAt = DateTimeOffset.UtcNow;

        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();

        return "Cập nhật profile thành công";
    }

    public async Task<string> CreateSystemReport(Request.CreateSystemReportRequest request)
    {
        var userId = GetUserId();

        var report = new Reports
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            AssignEventId = request.AssignEventId,
            SubmissionId = request.SubmissionId,
            Title = request.Title,
            Description = request.Description,
            ImgUrl = request.ImgUrl,
            FileUrl = request.FileUrl,
            TypeReport = request.TypeReport,
            Status = ReportStatusEnum.Open,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _dbContext.Reports.Add(report);
        await _dbContext.SaveChangesAsync();

        return "Gửi báo cáo thành công";
    }

    private Guid GetUserId()
    {
        var userId = _IhttpContex?.HttpContext?.User.FindFirst("UserId")?.Value;
        if(userId != null)
        {
            return Guid.Parse(userId);
        }
        return Guid.Empty;
    }
}
