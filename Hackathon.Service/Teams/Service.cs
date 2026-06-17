using System.Security.Claims;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Teams;

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

    private static bool IsProfileCompleted(Hackathon.Repository.Entity.Users user)
    {
        return !string.IsNullOrWhiteSpace(user.FirstName)
               && !string.IsNullOrWhiteSpace(user.LastName)
               && !string.IsNullOrWhiteSpace(user.PhoneNumber)
               && !string.IsNullOrWhiteSpace(user.AvatarUrl)
               && !string.IsNullOrWhiteSpace(user.Address)
               && user.DateOfBirth != DateTimeOffset.MinValue
               && !string.IsNullOrWhiteSpace(user.StudentId)
               && !string.IsNullOrWhiteSpace(user.College);
    }

    public async Task<Response.CreateTeamResponse> CreateTeam(Request.CreateTeamRequest request)
    {
        var userId = GetCurrentUserId();
        var teamName = request.TeamName?.Trim();

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && !x.IsDisable);
        if (user == null)
        {
            throw new NotFoundException("USER_NOT_FOUND");
        }

        if (user.IsVerified != true)
        {
            throw new ForbiddenException("USER_NOT_VERIFIED");
        }

        if (!IsProfileCompleted(user))
        {
            throw new BadRequestException("USER_PROFILE_NOT_COMPLETED");
        }

        var isDuplicatedName = await _dbContext.Teams.AnyAsync(x => x.Name.ToLower() == teamName.ToLower());
        if (isDuplicatedName)
        {
            throw new ConflictException("TEAM_NAME_ALREADY_EXISTS");
        }

        var now = DateTimeOffset.UtcNow;
        var team = new Hackathon.Repository.Entity.Teams
        {
            Id = Guid.NewGuid(),
            Name = teamName,
            CanEdit = true,
            CreatedAt = now,
            UpdatedAt = now,
        };

        var leader = new Hackathon.Repository.Entity.TeamDetails
        {
            Id = Guid.NewGuid(),
            TeamId = team.Id,
            UserId = user.Id,
            IsLeader = true,
            Status = TeamDetailStatusEnum.Active,
            CreatedAt = now,
            UpdatedAt = now,
        };

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _dbContext.Teams.AddAsync(team);
            await _dbContext.TeamDetails.AddAsync(leader);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return new Response.CreateTeamResponse
        {
            Id = team.Id,
            Name = team.Name,
            CanEdit = team.CanEdit,
            CreatedAt = team.CreatedAt,
            Message = "TEAM_CREATED_SUCCESSFULLY",
            Members = new List<Response.TeamMemberResponse>
            {
                new()
                {
                    UserId = leader.UserId,
                    IsLeader = leader.IsLeader,
                    Status = leader.Status?.ToString(),
                }
            }
        };
    }
}
