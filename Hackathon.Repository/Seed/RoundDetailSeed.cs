using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class RoundDetailSeed
{
    public static void SeedRoundDetails(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoundDetails>().HasData(
            CreateRoundDetail(SeedConstants.SeedInnovatorsIdeaRoundDetailId, SeedConstants.IdeaRoundId, SeedConstants.SeedInnovatorsRegisterTeamId),
            CreateRoundDetail(SeedConstants.SeedInnovatorsFinalRoundDetailId, SeedConstants.FinalRoundId, SeedConstants.SeedInnovatorsRegisterTeamId),
            CreateRoundDetail(SeedConstants.GreenCodersIdeaRoundDetailId, SeedConstants.IdeaRoundId, SeedConstants.GreenCodersRegisterTeamId),
            CreateRoundDetail(SeedConstants.GreenCodersFinalRoundDetailId, SeedConstants.FinalRoundId, SeedConstants.GreenCodersRegisterTeamId)
        );
    }

    private static RoundDetails CreateRoundDetail(Guid id, Guid roundId, Guid registerTeamId)
    {
        return new RoundDetails
        {
            Id = id,
            RoundId = roundId,
            RegisterTeamId = registerTeamId,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }
}
