using System;
using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class LeaderBoardSeed
{
    // Leaderboard IDs
    public static readonly Guid Lb1 = Guid.Parse("60000000-0000-0000-0000-000000000001");
    public static readonly Guid Lb2 = Guid.Parse("60000000-0000-0000-0000-000000000002");
    public static readonly Guid Lb3 = Guid.Parse("60000000-0000-0000-0000-000000000003");
    public static readonly Guid Lb4 = Guid.Parse("60000000-0000-0000-0000-000000000004");
    public static readonly Guid Lb5 = Guid.Parse("60000000-0000-0000-0000-000000000005");
    public static readonly Guid Lb6 = Guid.Parse("60000000-0000-0000-0000-000000000006");
    public static readonly Guid Lb7 = Guid.Parse("60000000-0000-0000-0000-000000000007");
    public static readonly Guid Lb8 = Guid.Parse("60000000-0000-0000-0000-000000000008");
    public static readonly Guid Lb9 = Guid.Parse("60000000-0000-0000-0000-000000000009");
    public static readonly Guid Lb10 = Guid.Parse("60000000-0000-0000-0000-000000000010");

    public static void SeedLeaderBoards(this ModelBuilder modelBuilder)
    {
        // 10 Leaderboards representing different events
        modelBuilder.Entity<LeaderBoards>().HasData(
            new LeaderBoards { Id = Lb1, EventId = SeedConstants.Event1Draft, Year = 2026, IsLocked = false, IsPublished = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb2, EventId = SeedConstants.Event2Published, Year = 2026, IsLocked = true, IsPublished = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb3, EventId = SeedConstants.Event3Closed, Year = 2026, IsLocked = true, IsPublished = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb4, EventId = SeedConstants.Event4Published, Year = 2026, IsLocked = false, IsPublished = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb5, EventId = SeedConstants.Event5Draft, Year = 2026, IsLocked = false, IsPublished = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb6, EventId = SeedConstants.Event6Closed, Year = 2026, IsLocked = true, IsPublished = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb7, EventId = SeedConstants.Event7Published, Year = 2026, IsLocked = false, IsPublished = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb8, EventId = SeedConstants.Event8Draft, Year = 2026, IsLocked = false, IsPublished = false, IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }, // disabled leaderboard
            new LeaderBoards { Id = Lb9, EventId = SeedConstants.Event9Closed, Year = 2025, IsLocked = true, IsPublished = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoards { Id = Lb10, EventId = SeedConstants.Event10Published, Year = 2026, IsLocked = false, IsPublished = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );

        // 15 Leaderboard Details linking teams and displaying scores and award levels
        modelBuilder.Entity<LeaderBoardDetails>().HasData(
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000001"), LeaderBoardId = Lb2, TeamId = TeamSeed.Team1, Score = 85.0m, LevelAward = 1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000002"), LeaderBoardId = Lb2, TeamId = TeamSeed.Team2, Score = 90.0m, LevelAward = 2, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000003"), LeaderBoardId = Lb2, TeamId = TeamSeed.Team3, Score = 75.0m, LevelAward = 3, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000004"), LeaderBoardId = Lb2, TeamId = TeamSeed.Team4, Score = 80.0m, LevelAward = null, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000005"), LeaderBoardId = Lb2, TeamId = TeamSeed.Team5, Score = 55.0m, LevelAward = null, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000006"), LeaderBoardId = Lb3, TeamId = TeamSeed.Team1, Score = 95.0m, LevelAward = 1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000007"), LeaderBoardId = Lb3, TeamId = TeamSeed.Team2, Score = 88.0m, LevelAward = 2, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000008"), LeaderBoardId = Lb4, TeamId = TeamSeed.Team6, Score = 70.0m, LevelAward = null, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000009"), LeaderBoardId = Lb4, TeamId = TeamSeed.Team7, Score = 78.0m, LevelAward = null, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000010"), LeaderBoardId = Lb4, TeamId = TeamSeed.Team8, Score = 60.0m, LevelAward = null, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000011"), LeaderBoardId = Lb6, TeamId = TeamSeed.Team1, Score = 82.0m, LevelAward = 1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000012"), LeaderBoardId = Lb7, TeamId = TeamSeed.Team11, Score = 90.0m, LevelAward = 1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000013"), LeaderBoardId = Lb7, TeamId = TeamSeed.Team12, Score = 87.0m, LevelAward = null, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000014"), LeaderBoardId = Lb2, TeamId = TeamSeed.Team10, Score = 40.0m, LevelAward = null, IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }, // disabled detail
            new LeaderBoardDetails { Id = Guid.Parse("61000000-0000-0000-0000-000000000015"), LeaderBoardId = Lb10, TeamId = TeamSeed.Team15, Score = 85.0m, LevelAward = 1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );
    }
}
