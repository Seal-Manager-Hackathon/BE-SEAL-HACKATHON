using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class NotificationSeed
{
    public static void SeedNotifications(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invitations>().HasData(new Invitations
        {
            Id = Guid.Parse("70000000-0000-0000-0000-000000000001"),
            TeamId = SeedConstants.GreenCodersTeamId,
            UserId = SeedConstants.StudentMemberUserId,
            LimitTime = SeedConstants.CreatedAt.AddDays(3),
            Status = "Pending",
            Description = "Seed invitation",
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        });

        modelBuilder.Entity<Notifications>().HasData(new Notifications
        {
            Id = Guid.Parse("71000000-0000-0000-0000-000000000001"),
            UserId = SeedConstants.StudentLeaderUserId,
            TeamId = SeedConstants.SeedInnovatorsTeamId,
            Title = "Registration approved",
            Status = "Unread",
            Description = "Your team registration has been approved",
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        });

        modelBuilder.Entity<MentorNotifications>().HasData(new MentorNotifications
        {
            Id = Guid.Parse("72000000-0000-0000-0000-000000000001"),
            AssignTrackId = SeedConstants.MentorAiAssignTrackId,
            Title = "New team registered",
            Description = "A new team joined your track",
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        });
    }
}
