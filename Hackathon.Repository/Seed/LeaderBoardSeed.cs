using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

// Lb IDs: 37000000-xxxx, LbDetail IDs: 38000000-xxxx
public static class LeaderBoardSeed
{
    public static readonly Guid Lb1 = Guid.Parse("37000000-0000-0000-0000-000000000001");
    public static readonly Guid Lb2 = Guid.Parse("37000000-0000-0000-0000-000000000002");
    public static readonly Guid Lb3 = Guid.Parse("37000000-0000-0000-0000-000000000003");
    public static readonly Guid Lb4 = Guid.Parse("37000000-0000-0000-0000-000000000004");
    public static readonly Guid Lb5 = Guid.Parse("37000000-0000-0000-0000-000000000005");
    public static readonly Guid Lb6 = Guid.Parse("37000000-0000-0000-0000-000000000006");
    public static readonly Guid Lb7 = Guid.Parse("37000000-0000-0000-0000-000000000007");
    public static readonly Guid Lb8 = Guid.Parse("37000000-0000-0000-0000-000000000008");
    public static readonly Guid Lb9 = Guid.Parse("37000000-0000-0000-0000-000000000009");
    public static readonly Guid Lb10 = Guid.Parse("37000000-0000-0000-0000-000000000010");

    public static readonly Guid Lbd1 = Guid.Parse("38000000-0000-0000-0000-000000000001");
    public static readonly Guid Lbd2 = Guid.Parse("38000000-0000-0000-0000-000000000002");
    public static readonly Guid Lbd3 = Guid.Parse("38000000-0000-0000-0000-000000000003");
    public static readonly Guid Lbd4 = Guid.Parse("38000000-0000-0000-0000-000000000004");
    public static readonly Guid Lbd5 = Guid.Parse("38000000-0000-0000-0000-000000000005");
    public static readonly Guid Lbd6 = Guid.Parse("38000000-0000-0000-0000-000000000006");
    public static readonly Guid Lbd7 = Guid.Parse("38000000-0000-0000-0000-000000000007");
    public static readonly Guid Lbd8 = Guid.Parse("38000000-0000-0000-0000-000000000008");
    public static readonly Guid Lbd9 = Guid.Parse("38000000-0000-0000-0000-000000000009");
    public static readonly Guid Lbd10 = Guid.Parse("38000000-0000-0000-0000-000000000010");
    public static readonly Guid Lbd11 = Guid.Parse("38000000-0000-0000-0000-000000000011");
    public static readonly Guid Lbd12 = Guid.Parse("38000000-0000-0000-0000-000000000012");
    public static readonly Guid Lbd13 = Guid.Parse("38000000-0000-0000-0000-000000000013");
    public static readonly Guid Lbd14 = Guid.Parse("38000000-0000-0000-0000-000000000014");
    public static readonly Guid Lbd15 = Guid.Parse("38000000-0000-0000-0000-000000000015");
    public static readonly Guid Lbd16 = Guid.Parse("38000000-0000-0000-0000-000000000016");
    public static readonly Guid Lbd17 = Guid.Parse("38000000-0000-0000-0000-000000000017");
    public static readonly Guid Lbd18 = Guid.Parse("38000000-0000-0000-0000-000000000018");
    public static readonly Guid Lbd19 = Guid.Parse("38000000-0000-0000-0000-000000000019");
    public static readonly Guid Lbd20 = Guid.Parse("38000000-0000-0000-0000-000000000020");
    public static readonly Guid Lbd21 = Guid.Parse("38000000-0000-0000-0000-000000000021");
    public static readonly Guid Lbd22 = Guid.Parse("38000000-0000-0000-0000-000000000022");
    public static readonly Guid Lbd23 = Guid.Parse("38000000-0000-0000-0000-000000000023");
    public static readonly Guid Lbd24 = Guid.Parse("38000000-0000-0000-0000-000000000024");
    public static readonly Guid Lbd25 = Guid.Parse("38000000-0000-0000-0000-000000000025");
    public static readonly Guid Lbd26 = Guid.Parse("38000000-0000-0000-0000-000000000026");
    public static readonly Guid Lbd27 = Guid.Parse("38000000-0000-0000-0000-000000000027");
    public static readonly Guid Lbd28 = Guid.Parse("38000000-0000-0000-0000-000000000028");
    public static readonly Guid Lbd29 = Guid.Parse("38000000-0000-0000-0000-000000000029");
    public static readonly Guid Lbd30 = Guid.Parse("38000000-0000-0000-0000-000000000030");

    public static void SeedLeaderBoards(this ModelBuilder modelBuilder)
    {
        var c = SeedConstants.CreatedAt;
        var year = 2026;

        // ── 10 LeaderBoards (ONE per event due to unique index) ───────
        modelBuilder.Entity<LeaderBoards>().HasData(
            Create(Lb1, SeedConstants.Event1Draft, year, false, false),
            Create(Lb2, SeedConstants.Event2Published, year, false, true),
            Create(Lb3, SeedConstants.Event3Closed, year, true, true),
            Create(Lb4, SeedConstants.Event4Published, year, false, false),
            Create(Lb5, SeedConstants.Event5Draft, year, false, false),
            Create(Lb6, SeedConstants.Event6Closed, year, true, true),
            Create(Lb7, SeedConstants.Event7Published, year, false, true),
            Create(Lb8, SeedConstants.Event8Draft, year, false, false),
            Create(Lb9, SeedConstants.Event9Closed, year, true, true),
            Create(Lb10, SeedConstants.Event10Published, year, false, false)
        );

        // ── 30 LeaderBoardDetails ─────────────────────────────────────
        modelBuilder.Entity<LeaderBoardDetails>().HasData(
            // E2 (published, published=true) — top teams
            Create(Lbd1, Lb2, TeamSeed.Team1, 170m, 1),
            Create(Lbd2, Lb2, TeamSeed.Team2, 165m, 2),
            Create(Lbd3, Lb2, TeamSeed.Team3, 38m, null),
            // E3 (closed, locked, published) — final results
            Create(Lbd4, Lb3, TeamSeed.Team6, 175m, 1),
            Create(Lbd5, Lb3, TeamSeed.Team7, 90m, 2),
            Create(Lbd6, Lb3, TeamSeed.Team8, 42m, null),
            // E4 (published, not locked/published)
            Create(Lbd7, Lb4, TeamSeed.Team10, 180m, 1),
            Create(Lbd8, Lb4, TeamSeed.Team11, 45m, null),
            Create(Lbd9, Lb4, TeamSeed.Team12, 43m, null),
            // E6 (closed, locked, published)
            Create(Lbd10, Lb6, TeamSeed.Team15, 82m, 1),
            Create(Lbd11, Lb6, TeamSeed.Team16, 78m, 2),
            Create(Lbd12, Lb6, TeamSeed.Team17, 37m, null),
            // E7 (published, published=true) — intermediate
            Create(Lbd13, Lb7, TeamSeed.Team18, 88m, 1),
            Create(Lbd14, Lb7, TeamSeed.Team19, 40m, null),
            // E9 (closed, locked, published)
            Create(Lbd15, Lb9, TeamSeed.Team22, 44m, 1),
            Create(Lbd16, Lb9, TeamSeed.Team23, 0m, null),
            // Extra details for various scenarios
            Create(Lbd17, Lb1, TeamSeed.Team27, null, null),
            Create(Lbd18, Lb1, TeamSeed.Team28, null, null),
            Create(Lbd19, Lb2, TeamSeed.Team26, null, null),
            Create(Lbd20, Lb3, TeamSeed.Team9, null, null),
            Create(Lbd21, Lb4, TeamSeed.Team13, null, null),
            Create(Lbd22, Lb4, TeamSeed.Team14, null, null),
            Create(Lbd23, Lb5, TeamSeed.Team15, null, null),
            Create(Lbd24, Lb5, TeamSeed.Team20, null, null),
            Create(Lbd25, Lb6, TeamSeed.Team5, null, null),
            Create(Lbd26, Lb7, TeamSeed.Team21, null, null),
            Create(Lbd27, Lb8, TeamSeed.Team24, null, null),
            Create(Lbd28, Lb9, TeamSeed.Team4, null, null),
            Create(Lbd29, Lb10, TeamSeed.Team25, null, null),
            Create(Lbd30, Lb10, TeamSeed.Team29, null, null)
        );
    }

    private static LeaderBoards Create(Guid id, Guid eventId, int year, bool isLocked, bool isPublished) => new()
    {
        Id = id, EventId = eventId, Year = year, IsLocked = isLocked, IsPublished = isPublished,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };

    private static LeaderBoardDetails Create(Guid id, Guid leaderBoardId, Guid teamId, decimal? score, int? levelAward) => new()
    {
        Id = id, LeaderBoardId = leaderBoardId, TeamId = teamId, Score = score, LevelAward = levelAward,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };
}
