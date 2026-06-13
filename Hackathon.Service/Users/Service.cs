using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Users;

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

    private static Response.ProfileResponse ToResponse(Repository.Entity.Users user)
    {
        return new Response.ProfileResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            StudentId = user.StudentId,
            College = user.College,
            AvatarUrl = user.AvatarUrl,
            Bio = user.Bio,
            Status = user.Status?.ToString(),
            IsVerified = user.IsVerified,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
        };
    }

    public async Task<Response.ProfileResponse> GetProfile()
    {
        var userId = GetCurrentUserId();
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        return ToResponse(user);
    }

    public async Task<Response.ProfileResponse> UpdateProfile(Request.UpdateProfileRequest request)
    {
        var userId = GetCurrentUserId();
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        if (request.FirstName != null && string.IsNullOrWhiteSpace(request.FirstName)
            || request.LastName != null && string.IsNullOrWhiteSpace(request.LastName)
            || request.PhoneNumber != null && string.IsNullOrWhiteSpace(request.PhoneNumber)
            || request.StudentId != null && string.IsNullOrWhiteSpace(request.StudentId)
            || request.College != null && string.IsNullOrWhiteSpace(request.College))
        {
            throw new BadRequestException("INVALID_PROFILE_DATA");
        }

        user.FirstName = request.FirstName?.Trim() ?? user.FirstName;
        user.LastName = request.LastName?.Trim() ?? user.LastName;
        user.PhoneNumber = request.PhoneNumber?.Trim() ?? user.PhoneNumber;
        user.StudentId = request.StudentId?.Trim() ?? user.StudentId;
        user.College = request.College?.Trim() ?? user.College;
        user.AvatarUrl = request.AvatarUrl?.Trim() ?? user.AvatarUrl;
        user.Bio = request.Bio?.Trim() ?? user.Bio;
        user.UpdatedAt = DateTimeOffset.UtcNow;

        await _dbContext.SaveChangesAsync();
        return ToResponse(user);
    }
}
