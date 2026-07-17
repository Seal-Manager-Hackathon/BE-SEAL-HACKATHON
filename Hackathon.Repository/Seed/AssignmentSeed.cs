using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

// AssignEvent IDs: 33000000-xxxx, AssignTrack IDs: 34000000-xxxx
public static class AssignmentSeed
{
    public static readonly Guid Ae1 = Guid.Parse("33000000-0000-0000-0000-000000000001");
    public static readonly Guid Ae2 = Guid.Parse("33000000-0000-0000-0000-000000000002");
    public static readonly Guid Ae3 = Guid.Parse("33000000-0000-0000-0000-000000000003");
    public static readonly Guid Ae4 = Guid.Parse("33000000-0000-0000-0000-000000000004");
    public static readonly Guid Ae5 = Guid.Parse("33000000-0000-0000-0000-000000000005");
    public static readonly Guid Ae6 = Guid.Parse("33000000-0000-0000-0000-000000000006");
    public static readonly Guid Ae7 = Guid.Parse("33000000-0000-0000-0000-000000000007");
    public static readonly Guid Ae8 = Guid.Parse("33000000-0000-0000-0000-000000000008");
    public static readonly Guid Ae9 = Guid.Parse("33000000-0000-0000-0000-000000000009");
    public static readonly Guid Ae10 = Guid.Parse("33000000-0000-0000-0000-000000000010");
    public static readonly Guid Ae11 = Guid.Parse("33000000-0000-0000-0000-000000000011");
    public static readonly Guid Ae12 = Guid.Parse("33000000-0000-0000-0000-000000000012");
    public static readonly Guid Ae13 = Guid.Parse("33000000-0000-0000-0000-000000000013");
    public static readonly Guid Ae14 = Guid.Parse("33000000-0000-0000-0000-000000000014");
    public static readonly Guid Ae15 = Guid.Parse("33000000-0000-0000-0000-000000000015");
    public static readonly Guid Ae16 = Guid.Parse("33000000-0000-0000-0000-000000000016");
    public static readonly Guid Ae17 = Guid.Parse("33000000-0000-0000-0000-000000000017");
    public static readonly Guid Ae18 = Guid.Parse("33000000-0000-0000-0000-000000000018");
    public static readonly Guid Ae19 = Guid.Parse("33000000-0000-0000-0000-000000000019");
    public static readonly Guid Ae20 = Guid.Parse("33000000-0000-0000-0000-000000000020");

    public static readonly Guid At1 = Guid.Parse("34000000-0000-0000-0000-000000000001");
    public static readonly Guid At2 = Guid.Parse("34000000-0000-0000-0000-000000000002");
    public static readonly Guid At3 = Guid.Parse("34000000-0000-0000-0000-000000000003");
    public static readonly Guid At4 = Guid.Parse("34000000-0000-0000-0000-000000000004");
    public static readonly Guid At5 = Guid.Parse("34000000-0000-0000-0000-000000000005");
    public static readonly Guid At6 = Guid.Parse("34000000-0000-0000-0000-000000000006");
    public static readonly Guid At7 = Guid.Parse("34000000-0000-0000-0000-000000000007");
    public static readonly Guid At8 = Guid.Parse("34000000-0000-0000-0000-000000000008");
    public static readonly Guid At9 = Guid.Parse("34000000-0000-0000-0000-000000000009");
    public static readonly Guid At10 = Guid.Parse("34000000-0000-0000-0000-000000000010");
    public static readonly Guid At11 = Guid.Parse("34000000-0000-0000-0000-000000000011");
    public static readonly Guid At12 = Guid.Parse("34000000-0000-0000-0000-000000000012");
    public static readonly Guid At13 = Guid.Parse("34000000-0000-0000-0000-000000000013");
    public static readonly Guid At14 = Guid.Parse("34000000-0000-0000-0000-000000000014");
    public static readonly Guid At15 = Guid.Parse("34000000-0000-0000-0000-000000000015");
    public static readonly Guid At16 = Guid.Parse("34000000-0000-0000-0000-000000000016");
    public static readonly Guid At17 = Guid.Parse("34000000-0000-0000-0000-000000000017");
    public static readonly Guid At18 = Guid.Parse("34000000-0000-0000-0000-000000000018");
    public static readonly Guid At19 = Guid.Parse("34000000-0000-0000-0000-000000000019");
    public static readonly Guid At20 = Guid.Parse("34000000-0000-0000-0000-000000000020");
    public static readonly Guid At21 = Guid.Parse("34000000-0000-0000-0000-000000000021");
    public static readonly Guid At22 = Guid.Parse("34000000-0000-0000-0000-000000000022");
    public static readonly Guid At23 = Guid.Parse("34000000-0000-0000-0000-000000000023");
    public static readonly Guid At24 = Guid.Parse("34000000-0000-0000-0000-000000000024");
    public static readonly Guid At25 = Guid.Parse("34000000-0000-0000-0000-000000000025");
    public static readonly Guid At26 = Guid.Parse("34000000-0000-0000-0000-000000000026");
    public static readonly Guid At27 = Guid.Parse("34000000-0000-0000-0000-000000000027");
    public static readonly Guid At28 = Guid.Parse("34000000-0000-0000-0000-000000000028");
    public static readonly Guid At29 = Guid.Parse("34000000-0000-0000-0000-000000000029");
    public static readonly Guid At30 = Guid.Parse("34000000-0000-0000-0000-000000000030");

    public static void SeedAssignments(this ModelBuilder modelBuilder)
    {
        var c = SeedConstants.CreatedAt;

        // ── 20 AssignEvents ──────────────────────────────────────────
        modelBuilder.Entity<AssignEvents>().HasData(
            // E2 staff
            CreateAe(Ae1, SeedConstants.UserStaff1, SeedConstants.StaffEventRoleId, SeedConstants.Event2Published),
            CreateAe(Ae2, SeedConstants.UserStaff2, SeedConstants.StaffEventRoleId, SeedConstants.Event2Published),
            // E2 judges
            CreateAe(Ae3, SeedConstants.UserJudge1, SeedConstants.JudgeEventRoleId, SeedConstants.Event2Published),
            CreateAe(Ae4, SeedConstants.UserJudge2, SeedConstants.JudgeEventRoleId, SeedConstants.Event2Published),
            // E2 mentors
            CreateAe(Ae5, SeedConstants.UserMentor1, SeedConstants.MentorEventRoleId, SeedConstants.Event2Published),
            CreateAe(Ae6, SeedConstants.UserMentor2, SeedConstants.MentorEventRoleId, SeedConstants.Event2Published),
            // E3 staff + judges
            CreateAe(Ae7, SeedConstants.UserStaff1, SeedConstants.StaffEventRoleId, SeedConstants.Event3Closed),
            CreateAe(Ae8, SeedConstants.UserJudge3, SeedConstants.JudgeEventRoleId, SeedConstants.Event3Closed),
            CreateAe(Ae9, SeedConstants.UserJudge4, SeedConstants.JudgeEventRoleId, SeedConstants.Event3Closed),
            // E4 staff + judges + mentors
            CreateAe(Ae10, SeedConstants.UserStaff3, SeedConstants.StaffEventRoleId, SeedConstants.Event4Published),
            CreateAe(Ae11, SeedConstants.UserJudge1, SeedConstants.JudgeEventRoleId, SeedConstants.Event4Published),
            CreateAe(Ae12, SeedConstants.UserJudge2, SeedConstants.JudgeEventRoleId, SeedConstants.Event4Published),
            CreateAe(Ae13, SeedConstants.UserMentor3, SeedConstants.MentorEventRoleId, SeedConstants.Event4Published),
            // E6 staff + judge
            CreateAe(Ae14, SeedConstants.UserStaff2, SeedConstants.StaffEventRoleId, SeedConstants.Event6Closed),
            CreateAe(Ae15, SeedConstants.UserJudge3, SeedConstants.JudgeEventRoleId, SeedConstants.Event6Closed),
            // E7 staff + judge + mentor
            CreateAe(Ae16, SeedConstants.UserStaff1, SeedConstants.StaffEventRoleId, SeedConstants.Event7Published),
            CreateAe(Ae17, SeedConstants.UserJudge4, SeedConstants.JudgeEventRoleId, SeedConstants.Event7Published),
            CreateAe(Ae18, SeedConstants.UserMentor4, SeedConstants.MentorEventRoleId, SeedConstants.Event7Published),
            // E9 staff + judge
            CreateAe(Ae19, SeedConstants.UserStaff3, SeedConstants.StaffEventRoleId, SeedConstants.Event9Closed),
            CreateAe(Ae20, SeedConstants.UserJudge1, SeedConstants.JudgeEventRoleId, SeedConstants.Event9Closed)
        );

        // ── 30 AssignTracks ──────────────────────────────────────────
        modelBuilder.Entity<AssignTracks>().HasData(
            // E2 — Judge1 → Track1-2, Judge2 → Track3-5
            CreateAt(At1, Ae3, TrackSeed.Track1),
            CreateAt(At2, Ae3, TrackSeed.Track2),
            CreateAt(At3, Ae4, TrackSeed.Track3),
            CreateAt(At4, Ae4, TrackSeed.Track4),
            CreateAt(At5, Ae4, TrackSeed.Track5),
            // E2 — Mentor1 → Track1, Mentor2 → Track2
            CreateAt(At6, Ae5, TrackSeed.Track1),
            CreateAt(At7, Ae6, TrackSeed.Track2),
            // E3 — Judge3 → Track19-20, Judge4 → Track21
            CreateAt(At8, Ae8, TrackSeed.Track19),
            CreateAt(At9, Ae8, TrackSeed.Track20),
            CreateAt(At10, Ae9, TrackSeed.Track21),
            // E4 — Judge1 → Track6-7, Judge2 → Track8-10
            CreateAt(At11, Ae11, TrackSeed.Track6),
            CreateAt(At12, Ae11, TrackSeed.Track7),
            CreateAt(At13, Ae12, TrackSeed.Track8),
            CreateAt(At14, Ae12, TrackSeed.Track9),
            CreateAt(At15, Ae12, TrackSeed.Track10),
            // E4 — Mentor3 → Track6
            CreateAt(At16, Ae13, TrackSeed.Track6),
            // E6 — Judge3 → Track22-23
            CreateAt(At17, Ae15, TrackSeed.Track22),
            CreateAt(At18, Ae15, TrackSeed.Track23),
            // E7 — Judge4 → Track11-14
            CreateAt(At19, Ae17, TrackSeed.Track11),
            CreateAt(At20, Ae17, TrackSeed.Track12),
            CreateAt(At21, Ae17, TrackSeed.Track13),
            CreateAt(At22, Ae17, TrackSeed.Track14),
            // E7 — Mentor4 → Track11
            CreateAt(At23, Ae18, TrackSeed.Track11),
            // E9 — Judge1 → Track24-25
            CreateAt(At24, Ae20, TrackSeed.Track24),
            CreateAt(At25, Ae20, TrackSeed.Track25),
            // Extra assignments (inactive/banned judge assignments for edge cases)
            CreateAt(At26, Ae3, TrackSeed.Track26),
            CreateAt(At27, Ae11, TrackSeed.Track27),
            CreateAt(At28, Ae17, TrackSeed.Track28),
            CreateAt(At29, Ae20, TrackSeed.Track29),
            CreateAt(At30, Ae17, TrackSeed.Track30)
        );
    }

    private static AssignEvents CreateAe(Guid id, Guid userId, Guid eventRoleId, Guid eventId) => new()
    {
        Id = id, UserId = userId, EventRoleId = eventRoleId, EventId = eventId,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };

    private static AssignTracks CreateAt(Guid id, Guid assignEventId, Guid trackId) => new()
    {
        Id = id, AssignEventId = assignEventId, TrackId = trackId,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };
}
