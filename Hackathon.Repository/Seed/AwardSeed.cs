using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class AwardSeed
{
    // 15 Awards across events
    public static readonly Guid AwardE2Champ   = Guid.Parse("26000000-0000-0000-0000-000000000001");
    public static readonly Guid AwardE2Runner  = Guid.Parse("26000000-0000-0000-0000-000000000002");
    public static readonly Guid AwardE2Third   = Guid.Parse("26000000-0000-0000-0000-000000000003");
    public static readonly Guid AwardE3Champ   = Guid.Parse("26000000-0000-0000-0000-000000000004");
    public static readonly Guid AwardE3Runner  = Guid.Parse("26000000-0000-0000-0000-000000000005");
    public static readonly Guid AwardE4Champ   = Guid.Parse("26000000-0000-0000-0000-000000000006");
    public static readonly Guid AwardE4Runner  = Guid.Parse("26000000-0000-0000-0000-000000000007");
    public static readonly Guid AwardE4Third   = Guid.Parse("26000000-0000-0000-0000-000000000008");
    public static readonly Guid AwardE6Champ   = Guid.Parse("26000000-0000-0000-0000-000000000009");
    public static readonly Guid AwardE7Champ   = Guid.Parse("26000000-0000-0000-0000-000000000010");
    public static readonly Guid AwardE7Runner  = Guid.Parse("26000000-0000-0000-0000-000000000011");
    public static readonly Guid AwardE9Champ   = Guid.Parse("26000000-0000-0000-0000-000000000012");
    public static readonly Guid AwardE10Champ  = Guid.Parse("26000000-0000-0000-0000-000000000013");
    public static readonly Guid AwardE10Runner = Guid.Parse("26000000-0000-0000-0000-000000000014");
    public static readonly Guid AwardE10Third  = Guid.Parse("26000000-0000-0000-0000-000000000015");

    public static void SeedAwards(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Awards>().HasData(
            // Event 2 (Published)
            Create(AwardE2Champ,  SeedConstants.Event2Published, "Vô địch",  "Giải nhất Event 2", 1, 1, 10000000m),
            Create(AwardE2Runner, SeedConstants.Event2Published, "Á quân",   "Giải nhì Event 2", 2, 1, 5000000m),
            Create(AwardE2Third,  SeedConstants.Event2Published, "Giải ba",  "Giải ba Event 2",  3, 2, 3000000m),
            // Event 3 (Closed)
            Create(AwardE3Champ,  SeedConstants.Event3Closed,    "Giải nhất", "Giải nhất Event 3", 1, 1, 8000000m),
            Create(AwardE3Runner, SeedConstants.Event3Closed,    "Giải nhì",  "Giải nhì Event 3", 2, 1, 4000000m),
            // Event 4 (Published)
            Create(AwardE4Champ,  SeedConstants.Event4Published, "Champion",      "Grand Champion", 1, 1, 12000000m),
            Create(AwardE4Runner, SeedConstants.Event4Published, "First Runner Up","Second Place",  2, 1, 6000000m),
            Create(AwardE4Third,  SeedConstants.Event4Published, "Second Runner Up","Third Place",  3, 1, 3000000m),
            // Event 6 (Closed)
            Create(AwardE6Champ,  SeedConstants.Event6Closed,    "Vô địch",  "Giải nhất Event 6", 1, 1, 6000000m),
            // Event 7 (Published)
            Create(AwardE7Champ,  SeedConstants.Event7Published, "Best Hack",    "Giải công nghệ",   1, 1, 5000000m),
            Create(AwardE7Runner, SeedConstants.Event7Published, "Best Design",  "Giải thiết kế",   2, 1, 3000000m),
            // Event 9 (Closed)
            Create(AwardE9Champ,  SeedConstants.Event9Closed,    "Giải nhất", "Giải nhất Event 9", 1, 1, 7000000m),
            // Event 10 (Published)
            Create(AwardE10Champ,  SeedConstants.Event10Published, "Vô địch", "Giải nhất Event 10", 1, 1, 15000000m),
            Create(AwardE10Runner, SeedConstants.Event10Published, "Á quân",  "Giải nhì Event 10", 2, 1, 8000000m),
            Create(AwardE10Third,  SeedConstants.Event10Published, "Giải ba", "Giải ba Event 10",  3, 2, 4000000m)
        );
    }

    private static Awards Create(Guid id, Guid eventId, string name, string desc, int level, int count, decimal prize)
        => new() { Id = id, EventId = eventId, Name = name, Description = desc, LevelAward = level, NumberOfAward = count, Prize = prize, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };
}
