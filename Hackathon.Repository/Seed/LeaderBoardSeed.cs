using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class LeaderBoardSeed
{
    public static void SeedLeaderBoards(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LeaderBoards>().HasData(new LeaderBoards
        {
            Id = SeedConstants.LeaderBoardId,
            EventId = SeedConstants.SealHackathonEventId,
            Year = 2026,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        });

        modelBuilder.Entity<LeaderBoardDetails>().HasData(
            new LeaderBoardDetails
            {
                Id = Guid.Parse("61000000-0000-0000-0000-000000000001"),
                LeaderBoardId = SeedConstants.LeaderBoardId,
                TeamId = SeedConstants.SeedInnovatorsTeamId,
                Score = 90m,
                LevelAward = "First",
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new LeaderBoardDetails
            {
                Id = Guid.Parse("61000000-0000-0000-0000-000000000002"),
                LeaderBoardId = SeedConstants.LeaderBoardId,
                TeamId = SeedConstants.GreenCodersTeamId,
                Score = 82m,
                LevelAward = "Second",
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            }
        );
    }
}
