using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class EventSeed
{
    public static void SeedEvents(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Events>().HasData(
            Create(SeedConstants.Event1Draft,      "SEAL Hackathon - Spring Draft",      "Spring draft event for testing.",     EventStatusEnum.Draft,     SeasonEnum.Spring,  2, 4, 10),
            Create(SeedConstants.Event2Published,   "SEAL Hackathon - Spring Published",  "Main active event for Spring season.",EventStatusEnum.Published, SeasonEnum.Spring,  2, 4, 12),
            Create(SeedConstants.Event3Closed,      "SEAL Hackathon - Spring Closed",     "Completed Spring event.",             EventStatusEnum.Closed,   SeasonEnum.Spring,  2, 5, 8),
            Create(SeedConstants.Event4Published,   "SEAL Hackathon - Summer Published",  "Main active event for Summer season.",EventStatusEnum.Published, SeasonEnum.Summer,  2, 4, 15),
            Create(SeedConstants.Event5Draft,       "SEAL Hackathon - Summer Draft",      "Summer draft event.",                 EventStatusEnum.Draft,     SeasonEnum.Summer,  3, 5, 12),
            Create(SeedConstants.Event6Closed,      "SEAL Hackathon - Summer Closed",     "Completed Summer event.",             EventStatusEnum.Closed,   SeasonEnum.Summer,  2, 4, 10),
            Create(SeedConstants.Event7Published,   "SEAL Hackathon - Autumn Published",  "Main active event for Autumn season.",EventStatusEnum.Published, SeasonEnum.Autumn,  2, 4, 10),
            Create(SeedConstants.Event8Draft,       "SEAL Hackathon - Autumn Draft",      "Autumn draft event.",                 EventStatusEnum.Draft,     SeasonEnum.Autumn,  2, 5, 10),
            Create(SeedConstants.Event9Closed,      "SEAL Hackathon - Winter Closed",     "Completed Winter event.",             EventStatusEnum.Closed,   SeasonEnum.Winter,  2, 4, 10),
            Create(SeedConstants.Event10Published,  "SEAL Hackathon - Winter Published",  "Main active event for Winter season.",EventStatusEnum.Published, SeasonEnum.Winter,  1, 5, 20)
        );
    }

    private static Events Create(Guid id, string name, string description, EventStatusEnum status, SeasonEnum season, int minMember, int maxMember, int limitTeam)
    {
        return new Events
        {
            Id = id, Name = name, Description = description,
            StartTime = SeedConstants.CreatedAt.AddDays(10),
            EndTime = SeedConstants.CreatedAt.AddDays(30),
            RegisterLimitTime = SeedConstants.CreatedAt.AddDays(9),
            LimitTeam = limitTeam, MinMember = minMember, MaxMember = maxMember,
            Status = status, NumberRound = 2, Season = season,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
        };
    }
}
