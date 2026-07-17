using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class AwardSeed
{
    // Award IDs: 26000000-xxxx
    public static readonly Guid Award1 = Guid.Parse("26000000-0000-0000-0000-000000000001");
    public static readonly Guid Award2 = Guid.Parse("26000000-0000-0000-0000-000000000002");
    public static readonly Guid Award3 = Guid.Parse("26000000-0000-0000-0000-000000000003");
    public static readonly Guid Award4 = Guid.Parse("26000000-0000-0000-0000-000000000004");
    public static readonly Guid Award5 = Guid.Parse("26000000-0000-0000-0000-000000000005");
    public static readonly Guid Award6 = Guid.Parse("26000000-0000-0000-0000-000000000006");
    public static readonly Guid Award7 = Guid.Parse("26000000-0000-0000-0000-000000000007");
    public static readonly Guid Award8 = Guid.Parse("26000000-0000-0000-0000-000000000008");
    public static readonly Guid Award9 = Guid.Parse("26000000-0000-0000-0000-000000000009");
    public static readonly Guid Award10 = Guid.Parse("26000000-0000-0000-0000-000000000010");
    public static readonly Guid Award11 = Guid.Parse("26000000-0000-0000-0000-000000000011");
    public static readonly Guid Award12 = Guid.Parse("26000000-0000-0000-0000-000000000012");
    public static readonly Guid Award13 = Guid.Parse("26000000-0000-0000-0000-000000000013");
    public static readonly Guid Award14 = Guid.Parse("26000000-0000-0000-0000-000000000014");
    public static readonly Guid Award15 = Guid.Parse("26000000-0000-0000-0000-000000000015");
    public static readonly Guid Award16 = Guid.Parse("26000000-0000-0000-0000-000000000016");
    public static readonly Guid Award17 = Guid.Parse("26000000-0000-0000-0000-000000000017");
    public static readonly Guid Award18 = Guid.Parse("26000000-0000-0000-0000-000000000018");
    public static readonly Guid Award19 = Guid.Parse("26000000-0000-0000-0000-000000000019");
    public static readonly Guid Award20 = Guid.Parse("26000000-0000-0000-0000-000000000020");
    public static readonly Guid Award21 = Guid.Parse("26000000-0000-0000-0000-000000000021");
    public static readonly Guid Award22 = Guid.Parse("26000000-0000-0000-0000-000000000022");
    public static readonly Guid Award23 = Guid.Parse("26000000-0000-0000-0000-000000000023");
    public static readonly Guid Award24 = Guid.Parse("26000000-0000-0000-0000-000000000024");
    public static readonly Guid Award25 = Guid.Parse("26000000-0000-0000-0000-000000000025");
    public static readonly Guid Award26 = Guid.Parse("26000000-0000-0000-0000-000000000026");
    public static readonly Guid Award27 = Guid.Parse("26000000-0000-0000-0000-000000000027");
    public static readonly Guid Award28 = Guid.Parse("26000000-0000-0000-0000-000000000028");
    public static readonly Guid Award29 = Guid.Parse("26000000-0000-0000-0000-000000000029");
    public static readonly Guid Award30 = Guid.Parse("26000000-0000-0000-0000-000000000030");

    public static void SeedAwards(this ModelBuilder modelBuilder)
    {
        var c = SeedConstants.CreatedAt;
        modelBuilder.Entity<Awards>().HasData(
            // E2 (Published) — 5 awards
            Create(Award1, SeedConstants.Event2Published, "Giải Nhất", 1, 1, 5000000m),
            Create(Award2, SeedConstants.Event2Published, "Giải Nhì", 2, 2, 3000000m),
            Create(Award3, SeedConstants.Event2Published, "Giải Ba", 3, 3, 1000000m),
            Create(Award4, SeedConstants.Event2Published, "Giải Khuyến Khích", 4, 5, 500000m),
            Create(Award5, SeedConstants.Event2Published, "Giải Đặc Biệt", 5, 1, 2000000m),
            // E4 (Published) — 5 awards
            Create(Award6, SeedConstants.Event4Published, "Champion", 1, 1, 10000000m),
            Create(Award7, SeedConstants.Event4Published, "First Runner-up", 2, 1, 5000000m),
            Create(Award8, SeedConstants.Event4Published, "Second Runner-up", 3, 1, 3000000m),
            Create(Award9, SeedConstants.Event4Published, "Consolation", 4, 5, 1000000m),
            Create(Award10, SeedConstants.Event4Published, "People's Choice", 5, 1, 2000000m),
            // E7 (Published) — 4 awards
            Create(Award11, SeedConstants.Event7Published, "Gold", 1, 1, 8000000m),
            Create(Award12, SeedConstants.Event7Published, "Silver", 2, 2, 4000000m),
            Create(Award13, SeedConstants.Event7Published, "Bronze", 3, 3, 2000000m),
            Create(Award14, SeedConstants.Event7Published, "Special", 4, 1, 1500000m),
            // E10 (Published) — 4 awards
            Create(Award15, SeedConstants.Event10Published, "First Place", 1, 1, 7000000m),
            Create(Award16, SeedConstants.Event10Published, "Second Place", 2, 1, 4000000m),
            Create(Award17, SeedConstants.Event10Published, "Third Place", 3, 2, 2000000m),
            Create(Award18, SeedConstants.Event10Published, "Innovation Award", 4, 1, 1000000m),
            // E3 (Closed) — 4 awards
            Create(Award19, SeedConstants.Event3Closed, "Winner", 1, 1, 5000000m),
            Create(Award20, SeedConstants.Event3Closed, "Runner-up", 2, 1, 3000000m),
            Create(Award21, SeedConstants.Event3Closed, "Best Presenter", 3, 1, 1000000m),
            Create(Award22, SeedConstants.Event3Closed, "Best Technical", 4, 1, 1000000m),
            // E6 (Closed) — 3 awards
            Create(Award23, SeedConstants.Event6Closed, "Champion Summer", 1, 1, 4000000m),
            Create(Award24, SeedConstants.Event6Closed, "1st Runner-up Summer", 2, 1, 2000000m),
            Create(Award25, SeedConstants.Event6Closed, "Best Demo Summer", 3, 1, 1000000m),
            // E9 (Closed) — 3 awards
            Create(Award26, SeedConstants.Event9Closed, "Winter Champion", 1, 1, 6000000m),
            Create(Award27, SeedConstants.Event9Closed, "Winter Runner-up", 2, 2, 3000000m),
            Create(Award28, SeedConstants.Event9Closed, "Winter Best UI", 3, 1, 1500000m),
            // Disabled awards
            Create(Award29, SeedConstants.Event2Published, "Old Award (disabled)", 99, 1, 0m, true),
            Create(Award30, SeedConstants.Event4Published, "Old Award E4 (disabled)", 99, 1, 0m, true)
        );
    }

    private static Awards Create(Guid id, Guid eventId, string name, int level, int num, decimal? prize, bool isDisable = false) => new()
    {
        Id = id, EventId = eventId, Name = name, LevelAward = level, NumberOfAward = num, Prize = prize,
        IsDisable = isDisable, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };
}
