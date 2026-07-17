using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

// RoundDetail IDs: 31000000-xxxx
public static class RoundDetailSeed
{
    public static readonly Guid Rd1 = Guid.Parse("31000000-0000-0000-0000-000000000001");
    public static readonly Guid Rd2 = Guid.Parse("31000000-0000-0000-0000-000000000002");
    public static readonly Guid Rd3 = Guid.Parse("31000000-0000-0000-0000-000000000003");
    public static readonly Guid Rd4 = Guid.Parse("31000000-0000-0000-0000-000000000004");
    public static readonly Guid Rd5 = Guid.Parse("31000000-0000-0000-0000-000000000005");
    public static readonly Guid Rd6 = Guid.Parse("31000000-0000-0000-0000-000000000006");
    public static readonly Guid Rd7 = Guid.Parse("31000000-0000-0000-0000-000000000007");
    public static readonly Guid Rd8 = Guid.Parse("31000000-0000-0000-0000-000000000008");
    public static readonly Guid Rd9 = Guid.Parse("31000000-0000-0000-0000-000000000009");
    public static readonly Guid Rd10 = Guid.Parse("31000000-0000-0000-0000-000000000010");
    public static readonly Guid Rd11 = Guid.Parse("31000000-0000-0000-0000-000000000011");
    public static readonly Guid Rd12 = Guid.Parse("31000000-0000-0000-0000-000000000012");
    public static readonly Guid Rd13 = Guid.Parse("31000000-0000-0000-0000-000000000013");
    public static readonly Guid Rd14 = Guid.Parse("31000000-0000-0000-0000-000000000014");
    public static readonly Guid Rd15 = Guid.Parse("31000000-0000-0000-0000-000000000015");
    public static readonly Guid Rd16 = Guid.Parse("31000000-0000-0000-0000-000000000016");
    public static readonly Guid Rd17 = Guid.Parse("31000000-0000-0000-0000-000000000017");
    public static readonly Guid Rd18 = Guid.Parse("31000000-0000-0000-0000-000000000018");
    public static readonly Guid Rd19 = Guid.Parse("31000000-0000-0000-0000-000000000019");
    public static readonly Guid Rd20 = Guid.Parse("31000000-0000-0000-0000-000000000020");
    public static readonly Guid Rd21 = Guid.Parse("31000000-0000-0000-0000-000000000021");
    public static readonly Guid Rd22 = Guid.Parse("31000000-0000-0000-0000-000000000022");
    public static readonly Guid Rd23 = Guid.Parse("31000000-0000-0000-0000-000000000023");
    public static readonly Guid Rd24 = Guid.Parse("31000000-0000-0000-0000-000000000024");
    public static readonly Guid Rd25 = Guid.Parse("31000000-0000-0000-0000-000000000025");
    public static readonly Guid Rd26 = Guid.Parse("31000000-0000-0000-0000-000000000026");
    public static readonly Guid Rd27 = Guid.Parse("31000000-0000-0000-0000-000000000027");
    public static readonly Guid Rd28 = Guid.Parse("31000000-0000-0000-0000-000000000028");
    public static readonly Guid Rd29 = Guid.Parse("31000000-0000-0000-0000-000000000029");
    public static readonly Guid Rd30 = Guid.Parse("31000000-0000-0000-0000-000000000030");
    public static readonly Guid Rd31 = Guid.Parse("31000000-0000-0000-0000-000000000031");
    public static readonly Guid Rd32 = Guid.Parse("31000000-0000-0000-0000-000000000032");
    public static readonly Guid Rd33 = Guid.Parse("31000000-0000-0000-0000-000000000033");
    public static readonly Guid Rd34 = Guid.Parse("31000000-0000-0000-0000-000000000034");
    public static readonly Guid Rd35 = Guid.Parse("31000000-0000-0000-0000-000000000035");

    public static void SeedRoundDetails(this ModelBuilder modelBuilder)
    {
        var c = SeedConstants.CreatedAt;

        modelBuilder.Entity<RoundDetails>().HasData(
            // E2 — R1 (5 approved regs)
            Create(Rd1, SeedConstants.RoundE2R1, TeamSeed.RegisterTeam1),
            Create(Rd2, SeedConstants.RoundE2R1, TeamSeed.RegisterTeam2),
            Create(Rd3, SeedConstants.RoundE2R1, TeamSeed.RegisterTeam3),
            Create(Rd4, SeedConstants.RoundE2R2, TeamSeed.RegisterTeam1),
            Create(Rd5, SeedConstants.RoundE2R2, TeamSeed.RegisterTeam2),
            Create(Rd6, SeedConstants.RoundE2R3, TeamSeed.RegisterTeam1),
            Create(Rd7, SeedConstants.RoundE2R3, TeamSeed.RegisterTeam2),
            // E3 — R1 (3 approved regs)
            Create(Rd8, SeedConstants.RoundE3R1, TeamSeed.RegisterTeam6),
            Create(Rd9, SeedConstants.RoundE3R1, TeamSeed.RegisterTeam7),
            Create(Rd10, SeedConstants.RoundE3R1, TeamSeed.RegisterTeam8),
            Create(Rd11, SeedConstants.RoundE3R2, TeamSeed.RegisterTeam6),
            Create(Rd12, SeedConstants.RoundE3R2, TeamSeed.RegisterTeam7),
            Create(Rd13, SeedConstants.RoundE3R3, TeamSeed.RegisterTeam6),
            // E4 — R1 (3 approved regs)
            Create(Rd14, SeedConstants.RoundE4R1, TeamSeed.RegisterTeam10),
            Create(Rd15, SeedConstants.RoundE4R1, TeamSeed.RegisterTeam11),
            Create(Rd16, SeedConstants.RoundE4R1, TeamSeed.RegisterTeam12),
            Create(Rd17, SeedConstants.RoundE4R2, TeamSeed.RegisterTeam10),
            Create(Rd18, SeedConstants.RoundE4R2, TeamSeed.RegisterTeam11),
            Create(Rd19, SeedConstants.RoundE4R3, TeamSeed.RegisterTeam10),
            // E6 — R1 (3 approved regs)
            Create(Rd20, SeedConstants.RoundE6R1, TeamSeed.RegisterTeam15),
            Create(Rd21, SeedConstants.RoundE6R1, TeamSeed.RegisterTeam16),
            Create(Rd22, SeedConstants.RoundE6R1, TeamSeed.RegisterTeam17),
            Create(Rd23, SeedConstants.RoundE6R2, TeamSeed.RegisterTeam15),
            Create(Rd24, SeedConstants.RoundE6R2, TeamSeed.RegisterTeam16),
            // E7 — R1 (2 approved regs)
            Create(Rd25, SeedConstants.RoundE7R1, TeamSeed.RegisterTeam18),
            Create(Rd26, SeedConstants.RoundE7R1, TeamSeed.RegisterTeam19),
            Create(Rd27, SeedConstants.RoundE7R2, TeamSeed.RegisterTeam18),
            Create(Rd28, SeedConstants.RoundE7R3, TeamSeed.RegisterTeam18),
            // E9 — R1 (2 approved regs)
            Create(Rd29, SeedConstants.RoundE9R1, TeamSeed.RegisterTeam22),
            Create(Rd30, SeedConstants.RoundE9R1, TeamSeed.RegisterTeam23),
            Create(Rd31, SeedConstants.RoundE9R2, TeamSeed.RegisterTeam22),
            // E10 — R1 (2 approved regs + 1 extra)
            Create(Rd32, SeedConstants.RoundE10R1, TeamSeed.RegisterTeam24),
            Create(Rd33, SeedConstants.RoundE10R1, TeamSeed.RegisterTeam25),
            Create(Rd34, SeedConstants.RoundE10R1, TeamSeed.RegisterTeam29),
            Create(Rd35, SeedConstants.RoundE10R2, TeamSeed.RegisterTeam24)
        );
    }

    private static RoundDetails Create(Guid id, Guid roundId, Guid registerTeamId) => new()
    {
        Id = id, RoundId = roundId, RegisterTeamId = registerTeamId,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };
}
