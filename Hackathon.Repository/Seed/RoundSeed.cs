using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class RoundSeed
{
    public static void SeedRounds(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rounds>().HasData(
            new Rounds
            {
                Id = SeedConstants.IdeaRoundId,
                EventId = SeedConstants.SealHackathonEventId,
                Name = "Idea Submission",
                Description = "Submit and validate the idea",
                StartTime = SeedConstants.CreatedAt.AddDays(10),
                EndTime = SeedConstants.CreatedAt.AddDays(11),
                StartSubmission = SeedConstants.CreatedAt.AddDays(10),
                EndSubmission = SeedConstants.CreatedAt.AddDays(10).AddHours(12),
                LimitTeam = 20,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new Rounds
            {
                Id = SeedConstants.FinalRoundId,
                EventId = SeedConstants.SealHackathonEventId,
                Name = "Final Demo",
                Description = "Present the final product",
                StartTime = SeedConstants.CreatedAt.AddDays(11),
                EndTime = SeedConstants.CreatedAt.AddDays(12),
                StartSubmission = SeedConstants.CreatedAt.AddDays(11),
                EndSubmission = SeedConstants.CreatedAt.AddDays(11).AddHours(12),
                LimitTeam = 10,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            }
        );
    }
}
