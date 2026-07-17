using System;
using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class RoundDetailSeed
{
    // Round Detail IDs
    public static readonly Guid Rd1 = Guid.Parse("32000000-0000-0000-0000-000000000001");
    public static readonly Guid Rd2 = Guid.Parse("32000000-0000-0000-0000-000000000002");
    public static readonly Guid Rd3 = Guid.Parse("32000000-0000-0000-0000-000000000003");
    public static readonly Guid Rd4 = Guid.Parse("32000000-0000-0000-0000-000000000004");
    public static readonly Guid Rd5 = Guid.Parse("32000000-0000-0000-0000-000000000005");
    public static readonly Guid Rd6 = Guid.Parse("32000000-0000-0000-0000-000000000006");
    public static readonly Guid Rd7 = Guid.Parse("32000000-0000-0000-0000-000000000007");
    public static readonly Guid Rd8 = Guid.Parse("32000000-0000-0000-0000-000000000008");
    public static readonly Guid Rd9 = Guid.Parse("32000000-0000-0000-0000-000000000009");
    public static readonly Guid Rd10 = Guid.Parse("32000000-0000-0000-0000-000000000010");
    public static readonly Guid Rd11 = Guid.Parse("32000000-0000-0000-0000-000000000011");
    public static readonly Guid Rd12 = Guid.Parse("32000000-0000-0000-0000-000000000012");

    public static void SeedRoundDetails(this ModelBuilder modelBuilder)
    {
        // 12 Round Details linking Team registrations to Rounds
        modelBuilder.Entity<RoundDetails>().HasData(
            new RoundDetails { Id = Rd1, RoundId = RoundSeed.RoundEvent2R1, RegisterTeamId = TeamSeed.RegTeam1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RoundDetails { Id = Rd2, RoundId = RoundSeed.RoundEvent2R1, RegisterTeamId = TeamSeed.RegTeam2, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RoundDetails { Id = Rd3, RoundId = RoundSeed.RoundEvent2R1, RegisterTeamId = TeamSeed.RegTeam3, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RoundDetails { Id = Rd4, RoundId = RoundSeed.RoundEvent2R1, RegisterTeamId = TeamSeed.RegTeam4, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RoundDetails { Id = Rd5, RoundId = RoundSeed.RoundEvent2R1, RegisterTeamId = TeamSeed.RegTeam5, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RoundDetails { Id = Rd6, RoundId = RoundSeed.RoundEvent2R2, RegisterTeamId = TeamSeed.RegTeam1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RoundDetails { Id = Rd7, RoundId = RoundSeed.RoundEvent2R2, RegisterTeamId = TeamSeed.RegTeam2, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RoundDetails { Id = Rd8, RoundId = RoundSeed.RoundEvent3R1, RegisterTeamId = TeamSeed.RegTeam1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RoundDetails { Id = Rd9, RoundId = RoundSeed.RoundEvent4R1, RegisterTeamId = TeamSeed.RegTeam1, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RoundDetails { Id = Rd10, RoundId = RoundSeed.RoundEvent7R1, RegisterTeamId = TeamSeed.RegTeam11, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RoundDetails { Id = Rd11, RoundId = RoundSeed.RoundEvent7R2, RegisterTeamId = TeamSeed.RegTeam12, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RoundDetails { Id = Rd12, RoundId = RoundSeed.RoundEvent10R1, RegisterTeamId = TeamSeed.RegTeam15, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );
    }
}
