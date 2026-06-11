using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class AwardSeed
{
    public static void SeedAwards(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Awards>().HasData(
            new Awards
            {
                Id = SeedConstants.ChampionAwardId,
                EventId = SeedConstants.SealHackathonEventId,
                Name = "Champion",
                Description = "First place award",
                LevelAward = "First",
                NumberOfAward = 1,
                Prize = 1000m,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new Awards
            {
                Id = SeedConstants.RunnerUpAwardId,
                EventId = SeedConstants.SealHackathonEventId,
                Name = "Runner Up",
                Description = "Second place award",
                LevelAward = "Second",
                NumberOfAward = 1,
                Prize = 500m,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            }
        );
    }
}
