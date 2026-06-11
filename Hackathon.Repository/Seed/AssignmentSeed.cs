using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class AssignmentSeed
{
    public static void SeedAssignments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssignEvents>().HasData(
            new AssignEvents
            {
                Id = SeedConstants.MentorAssignEventId,
                UserId = SeedConstants.MentorUserId,
                EventRoleId = SeedConstants.MentorEventRoleId,
                EventId = SeedConstants.SealHackathonEventId,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new AssignEvents
            {
                Id = SeedConstants.JudgeAssignEventId,
                UserId = SeedConstants.JudgeUserId,
                EventRoleId = SeedConstants.JudgeEventRoleId,
                EventId = SeedConstants.SealHackathonEventId,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            }
        );

        modelBuilder.Entity<AssignTracks>().HasData(
            CreateAssignTrack(SeedConstants.MentorAiAssignTrackId, SeedConstants.MentorAssignEventId, SeedConstants.AiTrackId),
            CreateAssignTrack(SeedConstants.JudgeAiAssignTrackId, SeedConstants.JudgeAssignEventId, SeedConstants.AiTrackId),
            CreateAssignTrack(SeedConstants.JudgeGreenAssignTrackId, SeedConstants.JudgeAssignEventId, SeedConstants.GreenTrackId)
        );
    }

    private static AssignTracks CreateAssignTrack(Guid id, Guid assignEventId, Guid trackId)
    {
        return new AssignTracks
        {
            Id = id,
            AssignEventId = assignEventId,
            TrackId = trackId,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }
}
