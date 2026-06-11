using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class EventSeed
{
    public static void SeedEvents(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Events>().HasData(new Events
        {
            Id = SeedConstants.SealHackathonEventId,
            Name = "SEAL Hackathon 2026",
            Description = "Seed event for hackathon demo data",
            StartTime = SeedConstants.CreatedAt.AddDays(10),
            EndTime = SeedConstants.CreatedAt.AddDays(12),
            RegisterLimitTime = SeedConstants.CreatedAt.AddDays(5),
            LimitTeam = 20,
            MinMember = 2,
            MaxMember = 4,
            Status = "Published",
            NumberRound = 2,
            Season = "2026",
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        });
    }
}
