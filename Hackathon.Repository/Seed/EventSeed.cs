using System;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class EventSeed
{
    public static void SeedEvents(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Events>().HasData(
            CreateEvent(SeedConstants.Event1Draft, "Seal Hackathon - Spring Draft", "Draft event for testing Spring season.", EventStatusEnum.Draft, SeasonEnum.Spring, 2, 4, 10),
            CreateEvent(SeedConstants.Event2Published, "Seal Hackathon - Spring Published", "Main active event for Spring season.", EventStatusEnum.Published, SeasonEnum.Spring, 1, 3, 10),
            CreateEvent(SeedConstants.Event3Closed, "Seal Hackathon - Spring Closed", "Completed event for Spring season.", EventStatusEnum.Closed, SeasonEnum.Spring, 2, 5, 8),
            CreateEvent(SeedConstants.Event4Published, "Seal Hackathon - Summer Published", "Main active event for Summer season.", EventStatusEnum.Published, SeasonEnum.Summer, 2, 4, 15),
            CreateEvent(SeedConstants.Event5Draft, "Seal Hackathon - Summer Draft", "Draft event for Summer season.", EventStatusEnum.Draft, SeasonEnum.Summer, 3, 5, 12),
            CreateEvent(SeedConstants.Event6Closed, "Seal Hackathon - Summer Closed", "Completed event for Summer season.", EventStatusEnum.Closed, SeasonEnum.Summer, 2, 4, 10),
            CreateEvent(SeedConstants.Event7Published, "Seal Hackathon - Autumn Published", "Main active event for Autumn season.", EventStatusEnum.Published, SeasonEnum.Autumn, 2, 4, 10),
            CreateEvent(SeedConstants.Event8Draft, "Seal Hackathon - Autumn Draft", "Draft event for Autumn season.", EventStatusEnum.Draft, SeasonEnum.Autumn, 2, 5, 10),
            CreateEvent(SeedConstants.Event9Closed, "Seal Hackathon - Winter Closed", "Completed event for Winter season.", EventStatusEnum.Closed, SeasonEnum.Winter, 2, 4, 10),
            CreateEvent(SeedConstants.Event10Published, "Seal Hackathon - Winter Published", "Main active event for Winter season.", EventStatusEnum.Published, SeasonEnum.Winter, 1, 5, 20)
        );
    }

    private static Events CreateEvent(Guid id, string name, string description, EventStatusEnum status, SeasonEnum season, int minMember, int maxMember, int limitTeam)
    {
        return new Events
        {
            Id = id,
            Name = name,
            Description = description,
            StartTime = SeedConstants.CreatedAt.AddDays(10),
            EndTime = SeedConstants.CreatedAt.AddDays(20),
            RegisterLimitTime = SeedConstants.CreatedAt.AddDays(9),
            LimitTeam = limitTeam,
            MinMember = minMember,
            MaxMember = maxMember,
            Status = status,
            NumberRound = 2,
            Season = season,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }
}
