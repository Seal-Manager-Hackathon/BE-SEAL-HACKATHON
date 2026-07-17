using System;
using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class AwardSeed
{
    // Awards
    public static readonly Guid AwardEvent2Champ = Guid.Parse("26000000-0000-0000-0000-000000000001");
    public static readonly Guid AwardEvent2Runner = Guid.Parse("26000000-0000-0000-0000-000000000002");
    public static readonly Guid AwardEvent2Third = Guid.Parse("26000000-0000-0000-0000-000000000003");
    public static readonly Guid AwardEvent3Champ = Guid.Parse("26000000-0000-0000-0000-000000000004");
    public static readonly Guid AwardEvent3Runner = Guid.Parse("26000000-0000-0000-0000-000000000005");
    public static readonly Guid AwardEvent4Champ = Guid.Parse("26000000-0000-0000-0000-000000000006");
    public static readonly Guid AwardEvent4Runner = Guid.Parse("26000000-0000-0000-0000-000000000007");
    public static readonly Guid AwardEvent7Champ = Guid.Parse("26000000-0000-0000-0000-000000000008");
    public static readonly Guid AwardEvent9Champ = Guid.Parse("26000000-0000-0000-0000-000000000009");
    public static readonly Guid AwardEvent10Champ = Guid.Parse("26000000-0000-0000-0000-000000000010");

    public static void SeedAwards(this ModelBuilder modelBuilder)
    {
        // 10 Awards
        modelBuilder.Entity<Awards>().HasData(
            new Awards { Id = AwardEvent2Champ, EventId = SeedConstants.Event2Published, Name = "Vô địch", Description = "Giải nhất Event 2", LevelAward = 1, NumberOfAward = 1, Prize = 10000000m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Awards { Id = AwardEvent2Runner, EventId = SeedConstants.Event2Published, Name = "Á quân", Description = "Giải nhì Event 2", LevelAward = 2, NumberOfAward = 1, Prize = 5000000m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Awards { Id = AwardEvent2Third, EventId = SeedConstants.Event2Published, Name = "Giải ba", Description = "Giải ba Event 2", LevelAward = 3, NumberOfAward = 2, Prize = 3000000m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Awards { Id = AwardEvent3Champ, EventId = SeedConstants.Event3Closed, Name = "Giải nhất", Description = "Giải nhất Event 3", LevelAward = 1, NumberOfAward = 1, Prize = 8000000m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Awards { Id = AwardEvent3Runner, EventId = SeedConstants.Event3Closed, Name = "Giải nhì", Description = "Giải nhì Event 3", LevelAward = 2, NumberOfAward = 1, Prize = 4000000m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Awards { Id = AwardEvent4Champ, EventId = SeedConstants.Event4Published, Name = "Champion", Description = "Grand Champion Event 4", LevelAward = 1, NumberOfAward = 1, Prize = 12000000m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Awards { Id = AwardEvent4Runner, EventId = SeedConstants.Event4Published, Name = "First Runner Up", Description = "Second Place Event 4", LevelAward = 2, NumberOfAward = 1, Prize = 6000000m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Awards { Id = AwardEvent7Champ, EventId = SeedConstants.Event7Published, Name = "Best Hack", Description = "Giải công nghệ xuất sắc", LevelAward = 1, NumberOfAward = 1, Prize = 5000000m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Awards { Id = AwardEvent9Champ, EventId = SeedConstants.Event9Closed, Name = "Giải nhất", Description = "Giải nhất Event 9", LevelAward = 1, NumberOfAward = 1, Prize = 7000000m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Awards { Id = AwardEvent10Champ, EventId = SeedConstants.Event10Published, Name = "Vô địch", Description = "Giải nhất Event 10", LevelAward = 1, NumberOfAward = 1, Prize = 15000000m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );
    }
}
