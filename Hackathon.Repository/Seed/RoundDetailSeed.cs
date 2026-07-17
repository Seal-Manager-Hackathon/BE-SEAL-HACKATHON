using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class RoundDetailSeed
{
    // 30 RoundDetail IDs
    public static readonly Guid Rd1  = Guid.Parse("32000000-0000-0000-0000-000000000001");
    public static readonly Guid Rd2  = Guid.Parse("32000000-0000-0000-0000-000000000002");
    public static readonly Guid Rd3  = Guid.Parse("32000000-0000-0000-0000-000000000003");
    public static readonly Guid Rd4  = Guid.Parse("32000000-0000-0000-0000-000000000004");
    public static readonly Guid Rd5  = Guid.Parse("32000000-0000-0000-0000-000000000005");
    public static readonly Guid Rd6  = Guid.Parse("32000000-0000-0000-0000-000000000006");
    public static readonly Guid Rd7  = Guid.Parse("32000000-0000-0000-0000-000000000007");
    public static readonly Guid Rd8  = Guid.Parse("32000000-0000-0000-0000-000000000008");
    public static readonly Guid Rd9  = Guid.Parse("32000000-0000-0000-0000-000000000009");
    public static readonly Guid Rd10 = Guid.Parse("32000000-0000-0000-0000-000000000010");
    public static readonly Guid Rd11 = Guid.Parse("32000000-0000-0000-0000-000000000011");
    public static readonly Guid Rd12 = Guid.Parse("32000000-0000-0000-0000-000000000012");
    public static readonly Guid Rd13 = Guid.Parse("32000000-0000-0000-0000-000000000013");
    public static readonly Guid Rd14 = Guid.Parse("32000000-0000-0000-0000-000000000014");
    public static readonly Guid Rd15 = Guid.Parse("32000000-0000-0000-0000-000000000015");
    public static readonly Guid Rd16 = Guid.Parse("32000000-0000-0000-0000-000000000016");
    public static readonly Guid Rd17 = Guid.Parse("32000000-0000-0000-0000-000000000017");
    public static readonly Guid Rd18 = Guid.Parse("32000000-0000-0000-0000-000000000018");
    public static readonly Guid Rd19 = Guid.Parse("32000000-0000-0000-0000-000000000019");
    public static readonly Guid Rd20 = Guid.Parse("32000000-0000-0000-0000-000000000020");
    public static readonly Guid Rd21 = Guid.Parse("32000000-0000-0000-0000-000000000021");
    public static readonly Guid Rd22 = Guid.Parse("32000000-0000-0000-0000-000000000022");
    public static readonly Guid Rd23 = Guid.Parse("32000000-0000-0000-0000-000000000023");
    public static readonly Guid Rd24 = Guid.Parse("32000000-0000-0000-0000-000000000024");
    public static readonly Guid Rd25 = Guid.Parse("32000000-0000-0000-0000-000000000025");
    public static readonly Guid Rd26 = Guid.Parse("32000000-0000-0000-0000-000000000026");
    public static readonly Guid Rd27 = Guid.Parse("32000000-0000-0000-0000-000000000027");
    public static readonly Guid Rd28 = Guid.Parse("32000000-0000-0000-0000-000000000028");
    public static readonly Guid Rd29 = Guid.Parse("32000000-0000-0000-0000-000000000029");
    public static readonly Guid Rd30 = Guid.Parse("32000000-0000-0000-0000-000000000030");

    public static void SeedRoundDetails(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoundDetails>().HasData(
            // Event 2 (Published) Round 1 - 5 teams
            Create(Rd1,  RoundSeed.R1E2, TeamSeed.RegTeam1),
            Create(Rd2,  RoundSeed.R1E2, TeamSeed.RegTeam2),
            Create(Rd3,  RoundSeed.R1E2, TeamSeed.RegTeam3),
            Create(Rd4,  RoundSeed.R1E2, TeamSeed.RegTeam4),
            Create(Rd5,  RoundSeed.R1E2, TeamSeed.RegTeam5),
            // Event 2 (Published) Round 2 - top 3 teams
            Create(Rd6,  RoundSeed.R2E2, TeamSeed.RegTeam1),
            Create(Rd7,  RoundSeed.R2E2, TeamSeed.RegTeam2),
            Create(Rd8,  RoundSeed.R2E2, TeamSeed.RegTeam3),
            // Event 3 (Closed) Round 1 - 2 teams
            Create(Rd9,  RoundSeed.R1E3, TeamSeed.RegTeam21),
            Create(Rd10, RoundSeed.R1E3, TeamSeed.RegTeam22),
            // Event 4 (Published) Round 1 - 5 teams
            Create(Rd11, RoundSeed.R1E4, TeamSeed.RegTeam6),
            Create(Rd12, RoundSeed.R1E4, TeamSeed.RegTeam7),
            Create(Rd13, RoundSeed.R1E4, TeamSeed.RegTeam8),
            Create(Rd14, RoundSeed.R1E4, TeamSeed.RegTeam9),
            Create(Rd15, RoundSeed.R1E4, TeamSeed.RegTeam23),
            // Event 7 (Published) Round 1 - 4 teams
            Create(Rd16, RoundSeed.R1E7, TeamSeed.RegTeam11),
            Create(Rd17, RoundSeed.R1E7, TeamSeed.RegTeam12),
            Create(Rd18, RoundSeed.R1E7, TeamSeed.RegTeam13),
            Create(Rd19, RoundSeed.R1E7, TeamSeed.RegTeam15),
            // Event 7 (Published) Round 2 - 2 teams
            Create(Rd20, RoundSeed.R2E7, TeamSeed.RegTeam11),
            Create(Rd21, RoundSeed.R2E7, TeamSeed.RegTeam12),
            // Event 10 (Published) Round 1 - 4 teams
            Create(Rd22, RoundSeed.R1E10, TeamSeed.RegTeam16),
            Create(Rd23, RoundSeed.R1E10, TeamSeed.RegTeam17),
            Create(Rd24, RoundSeed.R1E10, TeamSeed.RegTeam20),
            Create(Rd25, RoundSeed.R1E10, TeamSeed.RegTeam25),
            // Extra edge cases
            Create(Rd26, RoundSeed.R1E6, TeamSeed.RegTeam1),  // recycled in closed E6
            Create(Rd27, RoundSeed.R2E2, TeamSeed.RegTeam24), // R2 E2 with Team24
            Create(Rd28, RoundSeed.R1E2, TeamSeed.RegTeam24), // R1 E2 same team
            // Disabled round details
            new RoundDetails { Id = Rd29, RoundId = RoundSeed.R1E4B, RegisterTeamId = TeamSeed.RegTeam6, IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RoundDetails { Id = Rd30, RoundId = RoundSeed.R2E4B, RegisterTeamId = TeamSeed.RegTeam7, IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );
    }

    private static RoundDetails Create(Guid id, Guid roundId, Guid regTeamId)
        => new() { Id = id, RoundId = roundId, RegisterTeamId = regTeamId, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };
}
