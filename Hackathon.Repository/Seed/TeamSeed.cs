using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class TeamSeed
{
    public static void SeedTeams(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Teams>().HasData(
            new Teams
            {
                Id = SeedConstants.SeedInnovatorsTeamId,
                Name = "Seed Innovators",
                CanEdit = true,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new Teams
            {
                Id = SeedConstants.GreenCodersTeamId,
                Name = "Green Coders",
                CanEdit = true,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            }
        );

        modelBuilder.Entity<TeamDetails>().HasData(
            CreateTeamDetail(Guid.Parse("30100000-0000-0000-0000-000000000001"), SeedConstants.SeedInnovatorsTeamId, SeedConstants.StudentLeaderUserId, true),
            CreateTeamDetail(Guid.Parse("30100000-0000-0000-0000-000000000002"), SeedConstants.SeedInnovatorsTeamId, SeedConstants.StudentMemberUserId, false),
            CreateTeamDetail(Guid.Parse("30100000-0000-0000-0000-000000000003"), SeedConstants.GreenCodersTeamId, SeedConstants.GreenLeaderUserId, true)
        );

        modelBuilder.Entity<RegisterTeams>().HasData(
            new RegisterTeams
            {
                Id = SeedConstants.SeedInnovatorsRegisterTeamId,
                TeamId = SeedConstants.SeedInnovatorsTeamId,
                TopicId = SeedConstants.AiTopicId,
                Description = "Seed Innovators registration",
                Status = RegisterTeamStatusEnum.Approved,
                IsBanned = false,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new RegisterTeams
            {
                Id = SeedConstants.GreenCodersRegisterTeamId,
                TeamId = SeedConstants.GreenCodersTeamId,
                TopicId = SeedConstants.GreenTopicId,
                Description = "Green Coders registration",
                Status = RegisterTeamStatusEnum.Approved,
                IsBanned = false,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            }
        );
    }

    private static TeamDetails CreateTeamDetail(Guid id, Guid teamId, Guid userId, bool isLeader)
    {
        return new TeamDetails
        {
            Id = id,
            TeamId = teamId,
            UserId = userId,
            IsLeader = isLeader,
            Status = TeamDetailStatusEnum.Active,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }
}
