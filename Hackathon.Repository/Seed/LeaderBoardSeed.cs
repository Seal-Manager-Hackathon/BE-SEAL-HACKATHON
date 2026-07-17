using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class LeaderBoardSeed
{
    // 10 Leaderboard IDs (one per event — one-to-one)
    public static readonly Guid Lb1  = Guid.Parse("60000000-0000-0000-0000-000000000001");
    public static readonly Guid Lb2  = Guid.Parse("60000000-0000-0000-0000-000000000002");
    public static readonly Guid Lb3  = Guid.Parse("60000000-0000-0000-0000-000000000003");
    public static readonly Guid Lb4  = Guid.Parse("60000000-0000-0000-0000-000000000004");
    public static readonly Guid Lb5  = Guid.Parse("60000000-0000-0000-0000-000000000005");
    public static readonly Guid Lb6  = Guid.Parse("60000000-0000-0000-0000-000000000006");
    public static readonly Guid Lb7  = Guid.Parse("60000000-0000-0000-0000-000000000007");
    public static readonly Guid Lb8  = Guid.Parse("60000000-0000-0000-0000-000000000008");
    public static readonly Guid Lb9  = Guid.Parse("60000000-0000-0000-0000-000000000009");
    public static readonly Guid Lb10 = Guid.Parse("60000000-0000-0000-0000-000000000010");

    public static void SeedLeaderBoards(this ModelBuilder modelBuilder)
    {
        // 10 Leaderboards — one per event
        modelBuilder.Entity<LeaderBoards>().HasData(
            new LeaderBoards { Id = Lb1,  EventId = SeedConstants.Event1Draft,      Year = 2026, IsLocked = false, IsPublished = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb2,  EventId = SeedConstants.Event2Published,   Year = 2026, IsLocked = true,  IsPublished = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb3,  EventId = SeedConstants.Event3Closed,      Year = 2026, IsLocked = true,  IsPublished = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb4,  EventId = SeedConstants.Event4Published,   Year = 2026, IsLocked = false, IsPublished = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb5,  EventId = SeedConstants.Event5Draft,       Year = 2026, IsLocked = false, IsPublished = false, IsDisable = true,  CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb6,  EventId = SeedConstants.Event6Closed,      Year = 2026, IsLocked = true,  IsPublished = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb7,  EventId = SeedConstants.Event7Published,   Year = 2026, IsLocked = false, IsPublished = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb8,  EventId = SeedConstants.Event8Draft,       Year = 2026, IsLocked = false, IsPublished = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb9,  EventId = SeedConstants.Event9Closed,      Year = 2026, IsLocked = true,  IsPublished = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb10, EventId = SeedConstants.Event10Published,  Year = 2026, IsLocked = false, IsPublished = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );

        // 22 LeaderBoardDetails
        modelBuilder.Entity<LeaderBoardDetails>().HasData(
            // LB2 (Event2) — 5 teams
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000001"), LeaderBoardId = Lb2, TeamId = TeamSeed.Team1, Score = 85.0m,  LevelAward = 1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000002"), LeaderBoardId = Lb2, TeamId = TeamSeed.Team2, Score = 90.0m,  LevelAward = 2, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000003"), LeaderBoardId = Lb2, TeamId = TeamSeed.Team3, Score = 75.0m,  LevelAward = 3, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000004"), LeaderBoardId = Lb2, TeamId = TeamSeed.Team4, Score = 80.0m,  LevelAward = null, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000005"), LeaderBoardId = Lb2, TeamId = TeamSeed.Team5, Score = 60.0m,  LevelAward = null, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // LB3 (Event3) — 2 teams
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000006"), LeaderBoardId = Lb3, TeamId = TeamSeed.Team21, Score = 70.0m, LevelAward = 1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000007"), LeaderBoardId = Lb3, TeamId = TeamSeed.Team22, Score = 45.0m, LevelAward = 2, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // LB4 (Event4) — 3 teams
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000008"), LeaderBoardId = Lb4, TeamId = TeamSeed.Team6, Score = 78.0m,  LevelAward = null, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000009"), LeaderBoardId = Lb4, TeamId = TeamSeed.Team7, Score = 65.0m,  LevelAward = null, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000010"), LeaderBoardId = Lb4, TeamId = TeamSeed.Team8, Score = 55.0m,  LevelAward = null, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // LB6 (Event6) — 1 team
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000011"), LeaderBoardId = Lb6, TeamId = TeamSeed.Team1, Score = 85.0m,  LevelAward = 1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // LB7 (Event7) — 3 teams
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000012"), LeaderBoardId = Lb7, TeamId = TeamSeed.Team11, Score = 88.0m, LevelAward = 1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000013"), LeaderBoardId = Lb7, TeamId = TeamSeed.Team12, Score = 72.0m, LevelAward = null, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000014"), LeaderBoardId = Lb7, TeamId = TeamSeed.Team13, Score = 50.0m, LevelAward = null, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // LB9 (Event9) — 1 team
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000015"), LeaderBoardId = Lb9, TeamId = TeamSeed.Team1, Score = 90.0m,  LevelAward = 1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // LB10 (Event10) — 2 teams
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000016"), LeaderBoardId = Lb10, TeamId = TeamSeed.Team16, Score = 84.0m, LevelAward = 1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000017"), LeaderBoardId = Lb10, TeamId = TeamSeed.Team17, Score = 68.0m, LevelAward = 2, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // LB8 (Event8 Draft) — 1 team
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000018"), LeaderBoardId = Lb8, TeamId = TeamSeed.Team1, Score = 95.0m, LevelAward = 1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // LB1 (Event1 Draft) — 2 teams
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000019"), LeaderBoardId = Lb1, TeamId = TeamSeed.Team11, Score = 80.0m, LevelAward = null, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000020"), LeaderBoardId = Lb1, TeamId = TeamSeed.Team12, Score = 65.0m, LevelAward = null, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // Disabled details
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000022"), LeaderBoardId = Lb2, TeamId = TeamSeed.Team10, Score = 40.0m, LevelAward = null, IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000023"), LeaderBoardId = Lb4, TeamId = TeamSeed.Team10, Score = 30.0m, LevelAward = null, IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );
    }
}
