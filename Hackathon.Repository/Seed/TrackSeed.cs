using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class TrackSeed
{
    public static void SeedTracks(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tracks>().HasData(
            new Tracks
            {
                Id = SeedConstants.AiTrackId,
                EventId = SeedConstants.SealHackathonEventId,
                Title = "AI for Education",
                Description = "AI solutions for learning",
                MaxTeam = 10,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new Tracks
            {
                Id = SeedConstants.GreenTrackId,
                EventId = SeedConstants.SealHackathonEventId,
                Title = "Green Technology",
                Description = "Sustainable technology solutions",
                MaxTeam = 10,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            }
        );

        modelBuilder.Entity<Topics>().HasData(
            new Topics
            {
                Id = SeedConstants.AiTopicId,
                TrackId = SeedConstants.AiTrackId,
                Title = "Personalized Learning Assistant",
                Description = "Learning assistant powered by AI",
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new Topics
            {
                Id = SeedConstants.GreenTopicId,
                TrackId = SeedConstants.GreenTrackId,
                Title = "Carbon Footprint Tracker",
                Description = "Track and reduce carbon footprint",
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            }
        );
    }
}
