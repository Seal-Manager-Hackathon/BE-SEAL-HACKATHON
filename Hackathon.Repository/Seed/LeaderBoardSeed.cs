using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class LeaderBoardSeed
{
    public static void SeedLeaderBoards(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LeaderBoards>().HasData(
            new LeaderBoards
            {
                Id = SeedConstants.LeaderBoardId,
                EventId = SeedConstants.SealHackathonEventId,
                Year = 2026,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },

            // 10 New LeaderBoards for 10 new Events
            CreateLeaderBoard(Guid.Parse("60000000-0000-0000-0000-000000000010"), Guid.Parse("20000000-0000-0000-0000-000000000010"), 2024),
            CreateLeaderBoard(Guid.Parse("60000000-0000-0000-0000-000000000011"), Guid.Parse("20000000-0000-0000-0000-000000000011"), 2024),
            CreateLeaderBoard(Guid.Parse("60000000-0000-0000-0000-000000000012"), Guid.Parse("20000000-0000-0000-0000-000000000012"), 2025),
            CreateLeaderBoard(Guid.Parse("60000000-0000-0000-0000-000000000013"), Guid.Parse("20000000-0000-0000-0000-000000000013"), 2025),
            CreateLeaderBoard(Guid.Parse("60000000-0000-0000-0000-000000000014"), Guid.Parse("20000000-0000-0000-0000-000000000014"), 2025),
            CreateLeaderBoard(Guid.Parse("60000000-0000-0000-0000-000000000015"), Guid.Parse("20000000-0000-0000-0000-000000000015"), 2026),
            CreateLeaderBoard(Guid.Parse("60000000-0000-0000-0000-000000000016"), Guid.Parse("20000000-0000-0000-0000-000000000016"), 2026),
            CreateLeaderBoard(Guid.Parse("60000000-0000-0000-0000-000000000017"), Guid.Parse("20000000-0000-0000-0000-000000000017"), 2026),
            CreateLeaderBoard(Guid.Parse("60000000-0000-0000-0000-000000000018"), Guid.Parse("20000000-0000-0000-0000-000000000018"), 2027),
            CreateLeaderBoard(Guid.Parse("60000000-0000-0000-0000-000000000019"), Guid.Parse("20000000-0000-0000-0000-000000000019"), 2027)
        );

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
            },

            // 10 New LeaderBoardDetails connecting new teams to new leaderboards
            CreateLeaderBoardDetail(Guid.Parse("61000000-0000-0000-0000-000000000010"), Guid.Parse("60000000-0000-0000-0000-000000000010"), Guid.Parse("30000000-0000-0000-0000-000000000010"), 85m, "First"),
            CreateLeaderBoardDetail(Guid.Parse("61000000-0000-0000-0000-000000000011"), Guid.Parse("60000000-0000-0000-0000-000000000011"), Guid.Parse("30000000-0000-0000-0000-000000000011"), 89m, "First"),
            CreateLeaderBoardDetail(Guid.Parse("61000000-0000-0000-0000-000000000012"), Guid.Parse("60000000-0000-0000-0000-000000000012"), Guid.Parse("30000000-0000-0000-0000-000000000012"), 91m, "First"),
            CreateLeaderBoardDetail(Guid.Parse("61000000-0000-0000-0000-000000000013"), Guid.Parse("60000000-0000-0000-0000-000000000013"), Guid.Parse("30000000-0000-0000-0000-000000000013"), 78m, "Second"),
            CreateLeaderBoardDetail(Guid.Parse("61000000-0000-0000-0000-000000000014"), Guid.Parse("60000000-0000-0000-0000-000000000014"), Guid.Parse("30000000-0000-0000-0000-000000000014"), 88m, "First"),
            CreateLeaderBoardDetail(Guid.Parse("61000000-0000-0000-0000-000000000015"), Guid.Parse("60000000-0000-0000-0000-000000000015"), Guid.Parse("30000000-0000-0000-0000-000000000015"), 92m, "First"),
            CreateLeaderBoardDetail(Guid.Parse("61000000-0000-0000-0000-000000000016"), Guid.Parse("60000000-0000-0000-0000-000000000016"), Guid.Parse("30000000-0000-0000-0000-000000000016"), 87m, "First"),
            CreateLeaderBoardDetail(Guid.Parse("61000000-0000-0000-0000-000000000017"), Guid.Parse("60000000-0000-0000-0000-000000000017"), Guid.Parse("30000000-0000-0000-0000-000000000017"), 83m, "Second"),
            CreateLeaderBoardDetail(Guid.Parse("61000000-0000-0000-0000-000000000018"), Guid.Parse("60000000-0000-0000-0000-000000000018"), Guid.Parse("30000000-0000-0000-0000-000000000018"), 95m, "First"),
            CreateLeaderBoardDetail(Guid.Parse("61000000-0000-0000-0000-000000000019"), Guid.Parse("60000000-0000-0000-0000-000000000019"), Guid.Parse("30000000-0000-0000-0000-000000000019"), 94m, "First")
        );
    }

    private static LeaderBoards CreateLeaderBoard(Guid id, Guid eventId, int year)
    {
        return new LeaderBoards
        {
            Id = id,
            EventId = eventId,
            Year = year,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }

    private static LeaderBoardDetails CreateLeaderBoardDetail(Guid id, Guid leaderBoardId, Guid teamId, decimal score, string levelAward)
    {
        return new LeaderBoardDetails
        {
            Id = id,
            LeaderBoardId = leaderBoardId,
            TeamId = teamId,
            Score = score,
            LevelAward = levelAward,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }
}
