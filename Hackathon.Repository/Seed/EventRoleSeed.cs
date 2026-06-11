using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class EventRoleSeed
{
    public static void SeedEventRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventRoles>().HasData(
            new EventRoles
            {
                Id = SeedConstants.MentorEventRoleId,
                Name = EventRoleEnum.Mentor,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new EventRoles
            {
                Id = SeedConstants.JudgeEventRoleId,
                Name = EventRoleEnum.Judge,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            }
        );
    }
}
