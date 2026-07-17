using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class AssignmentSeed
{
    // 25 AssignEvents IDs
    public static readonly Guid Ae1  = Guid.Parse("40000000-0000-0000-0000-000000000001");
    public static readonly Guid Ae2  = Guid.Parse("40000000-0000-0000-0000-000000000002");
    public static readonly Guid Ae3  = Guid.Parse("40000000-0000-0000-0000-000000000003");
    public static readonly Guid Ae4  = Guid.Parse("40000000-0000-0000-0000-000000000004");
    public static readonly Guid Ae5  = Guid.Parse("40000000-0000-0000-0000-000000000005");
    public static readonly Guid Ae6  = Guid.Parse("40000000-0000-0000-0000-000000000006");
    public static readonly Guid Ae7  = Guid.Parse("40000000-0000-0000-0000-000000000007");
    public static readonly Guid Ae8  = Guid.Parse("40000000-0000-0000-0000-000000000008");
    public static readonly Guid Ae9  = Guid.Parse("40000000-0000-0000-0000-000000000009");
    public static readonly Guid Ae10 = Guid.Parse("40000000-0000-0000-0000-000000000010");
    public static readonly Guid Ae11 = Guid.Parse("40000000-0000-0000-0000-000000000011");
    public static readonly Guid Ae12 = Guid.Parse("40000000-0000-0000-0000-000000000012");
    public static readonly Guid Ae13 = Guid.Parse("40000000-0000-0000-0000-000000000013");
    public static readonly Guid Ae14 = Guid.Parse("40000000-0000-0000-0000-000000000014");
    public static readonly Guid Ae15 = Guid.Parse("40000000-0000-0000-0000-000000000015");
    public static readonly Guid Ae16 = Guid.Parse("40000000-0000-0000-0000-000000000016");
    public static readonly Guid Ae17 = Guid.Parse("40000000-0000-0000-0000-000000000017");
    public static readonly Guid Ae18 = Guid.Parse("40000000-0000-0000-0000-000000000018");
    public static readonly Guid Ae19 = Guid.Parse("40000000-0000-0000-0000-000000000019");
    public static readonly Guid Ae20 = Guid.Parse("40000000-0000-0000-0000-000000000020");
    public static readonly Guid Ae21 = Guid.Parse("40000000-0000-0000-0000-000000000021");
    public static readonly Guid Ae22 = Guid.Parse("40000000-0000-0000-0000-000000000022");
    public static readonly Guid Ae23 = Guid.Parse("40000000-0000-0000-0000-000000000023");
    public static readonly Guid Ae24 = Guid.Parse("40000000-0000-0000-0000-000000000024");
    public static readonly Guid Ae25 = Guid.Parse("40000000-0000-0000-0000-000000000025");

    // 25 AssignTracks IDs
    public static readonly Guid At1  = Guid.Parse("41000000-0000-0000-0000-000000000001");
    public static readonly Guid At2  = Guid.Parse("41000000-0000-0000-0000-000000000002");
    public static readonly Guid At3  = Guid.Parse("41000000-0000-0000-0000-000000000003");
    public static readonly Guid At4  = Guid.Parse("41000000-0000-0000-0000-000000000004");
    public static readonly Guid At5  = Guid.Parse("41000000-0000-0000-0000-000000000005");
    public static readonly Guid At6  = Guid.Parse("41000000-0000-0000-0000-000000000006");
    public static readonly Guid At7  = Guid.Parse("41000000-0000-0000-0000-000000000007");
    public static readonly Guid At8  = Guid.Parse("41000000-0000-0000-0000-000000000008");
    public static readonly Guid At9  = Guid.Parse("41000000-0000-0000-0000-000000000009");
    public static readonly Guid At10 = Guid.Parse("41000000-0000-0000-0000-000000000010");
    public static readonly Guid At11 = Guid.Parse("41000000-0000-0000-0000-000000000011");
    public static readonly Guid At12 = Guid.Parse("41000000-0000-0000-0000-000000000012");
    public static readonly Guid At13 = Guid.Parse("41000000-0000-0000-0000-000000000013");
    public static readonly Guid At14 = Guid.Parse("41000000-0000-0000-0000-000000000014");
    public static readonly Guid At15 = Guid.Parse("41000000-0000-0000-0000-000000000015");
    public static readonly Guid At16 = Guid.Parse("41000000-0000-0000-0000-000000000016");
    public static readonly Guid At17 = Guid.Parse("41000000-0000-0000-0000-000000000017");
    public static readonly Guid At18 = Guid.Parse("41000000-0000-0000-0000-000000000018");
    public static readonly Guid At19 = Guid.Parse("41000000-0000-0000-0000-000000000019");
    public static readonly Guid At20 = Guid.Parse("41000000-0000-0000-0000-000000000020");
    public static readonly Guid At21 = Guid.Parse("41000000-0000-0000-0000-000000000021");
    public static readonly Guid At22 = Guid.Parse("41000000-0000-0000-0000-000000000022");
    public static readonly Guid At23 = Guid.Parse("41000000-0000-0000-0000-000000000023");
    public static readonly Guid At24 = Guid.Parse("41000000-0000-0000-0000-000000000024");
    public static readonly Guid At25 = Guid.Parse("41000000-0000-0000-0000-000000000025");

    public static void SeedAssignments(this ModelBuilder modelBuilder)
    {
        // AssignEvents
        modelBuilder.Entity<AssignEvents>().HasData(
            // Event 2: Judge + Mentor + Staff
            CreateAe(Ae1,  SeedConstants.UserJudgeActive,   SeedConstants.JudgeEventRoleId,  SeedConstants.Event2Published, false),
            CreateAe(Ae2,  SeedConstants.UserMentorActive,  SeedConstants.MentorEventRoleId, SeedConstants.Event2Published, false),
            CreateAe(Ae3,  SeedConstants.UserStaffActive,   SeedConstants.StaffEventRoleId,  SeedConstants.Event2Published, false),
            // Event 4: Judge + Mentor + Staff
            CreateAe(Ae4,  SeedConstants.UserJudgeActive,   SeedConstants.JudgeEventRoleId,  SeedConstants.Event4Published, false),
            CreateAe(Ae5,  SeedConstants.UserMentorActive,  SeedConstants.MentorEventRoleId, SeedConstants.Event4Published, false),
            CreateAe(Ae6,  SeedConstants.UserStaffActive,   SeedConstants.StaffEventRoleId,  SeedConstants.Event4Published, false),
            // Event 7: Judge + Mentor + Staff
            CreateAe(Ae7,  SeedConstants.UserJudgeActive,   SeedConstants.JudgeEventRoleId,  SeedConstants.Event7Published, false),
            CreateAe(Ae8,  SeedConstants.UserMentorActive,  SeedConstants.MentorEventRoleId, SeedConstants.Event7Published, false),
            CreateAe(Ae9,  SeedConstants.UserStaffActive,   SeedConstants.StaffEventRoleId,  SeedConstants.Event7Published, false),
            // Event 10: Judge + Mentor + Staff
            CreateAe(Ae10, SeedConstants.UserJudgeActive,   SeedConstants.JudgeEventRoleId,  SeedConstants.Event10Published, false),
            CreateAe(Ae11, SeedConstants.UserMentorActive,  SeedConstants.MentorEventRoleId, SeedConstants.Event10Published, false),
            CreateAe(Ae12, SeedConstants.UserStaffActive,   SeedConstants.StaffEventRoleId,  SeedConstants.Event10Published, false),
            // Event 3 (Closed): Judge + Mentor
            CreateAe(Ae13, SeedConstants.UserJudgeActive,   SeedConstants.JudgeEventRoleId,  SeedConstants.Event3Closed, false),
            CreateAe(Ae14, SeedConstants.UserMentorActive,  SeedConstants.MentorEventRoleId, SeedConstants.Event3Closed, false),
            // Extra: Inactive judge assigned
            CreateAe(Ae15, SeedConstants.UserJudgeInactive, SeedConstants.JudgeEventRoleId,  SeedConstants.Event2Published, false),
            // Extra: Banned judge assigned
            CreateAe(Ae16, SeedConstants.UserJudgeBanned,   SeedConstants.JudgeEventRoleId,  SeedConstants.Event4Published, false),
            // Disabled assignments
            CreateAe(Ae17, SeedConstants.UserJudgeActive,   SeedConstants.JudgeEventRoleId,  SeedConstants.Event2Published, true),
            CreateAe(Ae18, SeedConstants.UserMentorActive,  SeedConstants.MentorEventRoleId, SeedConstants.Event4Published, true),
            // Additional mentor coverage
            CreateAe(Ae19, SeedConstants.UserMentorInactive,SeedConstants.MentorEventRoleId, SeedConstants.Event7Published, false),
            CreateAe(Ae20, SeedConstants.UserMentorBanned,  SeedConstants.MentorEventRoleId, SeedConstants.Event10Published, false),
            // Staff across events
            CreateAe(Ae21, SeedConstants.UserStaffInactive, SeedConstants.StaffEventRoleId,  SeedConstants.Event7Published, false),
            CreateAe(Ae22, SeedConstants.UserStaffBanned,   SeedConstants.StaffEventRoleId,  SeedConstants.Event10Published, false),
            // Additional judges
            CreateAe(Ae23, SeedConstants.UserJudgeActive,   SeedConstants.JudgeEventRoleId,  SeedConstants.Event3Closed, false),
            CreateAe(Ae24, SeedConstants.UserJudgeInactive, SeedConstants.JudgeEventRoleId,  SeedConstants.Event7Published, false),
            CreateAe(Ae25, SeedConstants.UserJudgeActive,   SeedConstants.JudgeEventRoleId,  SeedConstants.Event6Closed, false)
        );

        // AssignTracks (linking judge/mentor to specific tracks)
        modelBuilder.Entity<AssignTracks>().HasData(
            // Judge events -> tracks
            CreateAt(At1,  Ae1,  TrackSeed.Track1Ai,  false),
            CreateAt(At2,  Ae1,  TrackSeed.Track2Web, false),
            CreateAt(At3,  Ae4,  TrackSeed.Track6Security, false),
            CreateAt(At4,  Ae7,  TrackSeed.Track11Ai, false),
            CreateAt(At5,  Ae10, TrackSeed.Track16Security, false),
            CreateAt(At6,  Ae13, TrackSeed.Track1Ai,  false),
            CreateAt(At7,  Ae15, TrackSeed.Track3Mobile, false),
            CreateAt(At8,  Ae23, TrackSeed.Track2Web, false),
            CreateAt(At9,  Ae25, TrackSeed.Track1Ai,  false),
            CreateAt(At10, Ae4,  TrackSeed.Track7Blockchain, false),
            // Mentor events -> tracks
            CreateAt(At11, Ae2,  TrackSeed.Track1Ai,  false),
            CreateAt(At12, Ae5,  TrackSeed.Track6Security, false),
            CreateAt(At13, Ae8,  TrackSeed.Track11Ai, false),
            CreateAt(At14, Ae11, TrackSeed.Track16Security, false),
            CreateAt(At15, Ae14, TrackSeed.Track1Ai,  false),
            CreateAt(At16, Ae19, TrackSeed.Track12Web, false),
            CreateAt(At17, Ae20, TrackSeed.Track17Blockchain, false),
            // Disabled track assignments
            CreateAt(At18, Ae16, TrackSeed.Track6Security, true),
            CreateAt(At19, Ae17, TrackSeed.Track4Iot, true),
            // Extra judge-track assignments
            CreateAt(At20, Ae1,  TrackSeed.Track4Iot, false),
            CreateAt(At21, Ae4,  TrackSeed.Track8Game, false),
            CreateAt(At22, Ae7,  TrackSeed.Track12Web, false),
            CreateAt(At23, Ae10, TrackSeed.Track18Game, false),
            CreateAt(At24, Ae5,  TrackSeed.Track7Blockchain, false),
            CreateAt(At25, Ae8,  TrackSeed.Track13Mobile, false)
        );
    }

    private static AssignEvents CreateAe(Guid id, Guid userId, Guid roleId, Guid eventId, bool isDisable)
        => new() { Id = id, UserId = userId, EventRoleId = roleId, EventId = eventId, IsDisable = isDisable, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };

    private static AssignTracks CreateAt(Guid id, Guid assignEventId, Guid trackId, bool isDisable)
        => new() { Id = id, AssignEventId = assignEventId, TrackId = trackId, IsDisable = isDisable, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };
}
