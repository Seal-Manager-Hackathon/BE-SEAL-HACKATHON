using System;
using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class AssignmentSeed
{
    // AssignEvents IDs
    public static readonly Guid Ae1 = Guid.Parse("40000000-0000-0000-0000-000000000001");
    public static readonly Guid Ae2 = Guid.Parse("40000000-0000-0000-0000-000000000002");
    public static readonly Guid Ae3 = Guid.Parse("40000000-0000-0000-0000-000000000003");
    public static readonly Guid Ae4 = Guid.Parse("40000000-0000-0000-0000-000000000004");
    public static readonly Guid Ae5 = Guid.Parse("40000000-0000-0000-0000-000000000005");
    public static readonly Guid Ae6 = Guid.Parse("40000000-0000-0000-0000-000000000006");
    public static readonly Guid Ae7 = Guid.Parse("40000000-0000-0000-0000-000000000007");
    public static readonly Guid Ae8 = Guid.Parse("40000000-0000-0000-0000-000000000008");
    public static readonly Guid Ae9 = Guid.Parse("40000000-0000-0000-0000-000000000009");
    public static readonly Guid Ae10 = Guid.Parse("40000000-0000-0000-0000-000000000010");
    public static readonly Guid Ae11 = Guid.Parse("40000000-0000-0000-0000-000000000011");
    public static readonly Guid Ae12 = Guid.Parse("40000000-0000-0000-0000-000000000012");
    public static readonly Guid Ae13 = Guid.Parse("40000000-0000-0000-0000-000000000013");
    public static readonly Guid Ae14 = Guid.Parse("40000000-0000-0000-0000-000000000014");
    public static readonly Guid Ae15 = Guid.Parse("40000000-0000-0000-0000-000000000015");

    // AssignTracks IDs
    public static readonly Guid At1 = Guid.Parse("41000000-0000-0000-0000-000000000001");
    public static readonly Guid At2 = Guid.Parse("41000000-0000-0000-0000-000000000002");
    public static readonly Guid At3 = Guid.Parse("41000000-0000-0000-0000-000000000003");
    public static readonly Guid At4 = Guid.Parse("41000000-0000-0000-0000-000000000004");
    public static readonly Guid At5 = Guid.Parse("41000000-0000-0000-0000-000000000005");
    public static readonly Guid At6 = Guid.Parse("41000000-0000-0000-0000-000000000006");
    public static readonly Guid At7 = Guid.Parse("41000000-0000-0000-0000-000000000007");
    public static readonly Guid At8 = Guid.Parse("41000000-0000-0000-0000-000000000008");
    public static readonly Guid At9 = Guid.Parse("41000000-0000-0000-0000-000000000009");
    public static readonly Guid At10 = Guid.Parse("41000000-0000-0000-0000-000000000010");
    public static readonly Guid At11 = Guid.Parse("41000000-0000-0000-0000-000000000011");
    public static readonly Guid At12 = Guid.Parse("41000000-0000-0000-0000-000000000012");
    public static readonly Guid At13 = Guid.Parse("41000000-0000-0000-0000-000000000013");
    public static readonly Guid At14 = Guid.Parse("41000000-0000-0000-0000-000000000014");
    public static readonly Guid At15 = Guid.Parse("41000000-0000-0000-0000-000000000015");

    public static void SeedAssignments(this ModelBuilder modelBuilder)
    {
        // 15 AssignEvents
        modelBuilder.Entity<AssignEvents>().HasData(
            new AssignEvents { Id = Ae1, UserId = SeedConstants.UserJudgeActive, EventRoleId = SeedConstants.JudgeEventRoleId, EventId = SeedConstants.Event2Published, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignEvents { Id = Ae2, UserId = SeedConstants.UserMentorActive, EventRoleId = SeedConstants.MentorEventRoleId, EventId = SeedConstants.Event2Published, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignEvents { Id = Ae3, UserId = SeedConstants.UserStaffActive, EventRoleId = SeedConstants.StaffEventRoleId, EventId = SeedConstants.Event2Published, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignEvents { Id = Ae4, UserId = SeedConstants.UserJudgeActive, EventRoleId = SeedConstants.JudgeEventRoleId, EventId = SeedConstants.Event4Published, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignEvents { Id = Ae5, UserId = SeedConstants.UserMentorActive, EventRoleId = SeedConstants.MentorEventRoleId, EventId = SeedConstants.Event4Published, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignEvents { Id = Ae6, UserId = SeedConstants.UserStaffActive, EventRoleId = SeedConstants.StaffEventRoleId, EventId = SeedConstants.Event4Published, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignEvents { Id = Ae7, UserId = SeedConstants.UserJudgeActive, EventRoleId = SeedConstants.JudgeEventRoleId, EventId = SeedConstants.Event7Published, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignEvents { Id = Ae8, UserId = SeedConstants.UserMentorActive, EventRoleId = SeedConstants.MentorEventRoleId, EventId = SeedConstants.Event7Published, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignEvents { Id = Ae9, UserId = SeedConstants.UserStaffActive, EventRoleId = SeedConstants.StaffEventRoleId, EventId = SeedConstants.Event7Published, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignEvents { Id = Ae10, UserId = SeedConstants.UserJudgeInactive, EventRoleId = SeedConstants.JudgeEventRoleId, EventId = SeedConstants.Event2Published, IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }, // disabled assignment
            new AssignEvents { Id = Ae11, UserId = SeedConstants.UserJudgeActive, EventRoleId = SeedConstants.JudgeEventRoleId, EventId = SeedConstants.Event10Published, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignEvents { Id = Ae12, UserId = SeedConstants.UserMentorActive, EventRoleId = SeedConstants.MentorEventRoleId, EventId = SeedConstants.Event10Published, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignEvents { Id = Ae13, UserId = SeedConstants.UserStaffActive, EventRoleId = SeedConstants.StaffEventRoleId, EventId = SeedConstants.Event10Published, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignEvents { Id = Ae14, UserId = SeedConstants.UserJudgeActive, EventRoleId = SeedConstants.JudgeEventRoleId, EventId = SeedConstants.Event3Closed, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignEvents { Id = Ae15, UserId = SeedConstants.UserMentorActive, EventRoleId = SeedConstants.MentorEventRoleId, EventId = SeedConstants.Event3Closed, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );

        // 15 AssignTracks
        modelBuilder.Entity<AssignTracks>().HasData(
            new AssignTracks { Id = At1, AssignEventId = Ae1, TrackId = TrackSeed.Track1Ai, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignTracks { Id = At2, AssignEventId = Ae1, TrackId = TrackSeed.Track2Web, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignTracks { Id = At3, AssignEventId = Ae1, TrackId = TrackSeed.Track3Mobile, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignTracks { Id = At4, AssignEventId = Ae1, TrackId = TrackSeed.Track4Iot, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignTracks { Id = At5, AssignEventId = Ae1, TrackId = TrackSeed.Track5Cloud, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignTracks { Id = At6, AssignEventId = Ae2, TrackId = TrackSeed.Track1Ai, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignTracks { Id = At7, AssignEventId = Ae2, TrackId = TrackSeed.Track2Web, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignTracks { Id = At8, AssignEventId = Ae4, TrackId = TrackSeed.Track6Security, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignTracks { Id = At9, AssignEventId = Ae4, TrackId = TrackSeed.Track7Blockchain, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignTracks { Id = At10, AssignEventId = Ae4, TrackId = TrackSeed.Track8Game, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignTracks { Id = At11, AssignEventId = Ae5, TrackId = TrackSeed.Track6Security, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignTracks { Id = At12, AssignEventId = Ae5, TrackId = TrackSeed.Track7Blockchain, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignTracks { Id = At13, AssignEventId = Ae7, TrackId = TrackSeed.Track6Security, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignTracks { Id = At14, AssignEventId = Ae8, TrackId = TrackSeed.Track6Security, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new AssignTracks { Id = At15, AssignEventId = Ae10, TrackId = TrackSeed.Track1Ai, IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt } // disabled
        );
    }
}
