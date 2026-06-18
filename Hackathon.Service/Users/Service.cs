
using Hackathon.Repository;
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

    private Guid GetUserId()
    {
        var userId = _IhttpContex?.HttpContext.User.FindFirst("UserId")?.Value;
        if(userId != null)
        {
            return Guid.Parse(userId);
        }
        return Guid.Empty;
    }
}