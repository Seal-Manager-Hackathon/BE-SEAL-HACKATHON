using System;

namespace Hackathon.Repository.Seed;

public static class SeedConstants
{
    public static readonly DateTimeOffset CreatedAt = new(2026, 6, 11, 0, 0, 0, TimeSpan.Zero);

    // Roles (Event Roles)
    public static readonly Guid MentorEventRoleId = Guid.Parse("55555555-5555-5555-5555-555555555555");
    public static readonly Guid JudgeEventRoleId = Guid.Parse("66666666-6666-6666-6666-666666666666");
    public static readonly Guid StaffEventRoleId = Guid.Parse("77777777-7777-7777-7777-777777777777");

    // 15 Users
    public static readonly Guid UserAdminActive = Guid.Parse("10000000-0000-0000-0000-000000000001");
    public static readonly Guid UserAdminBanned = Guid.Parse("10000000-0000-0000-0000-000000000002");
    public static readonly Guid UserStaffActive = Guid.Parse("10000000-0000-0000-0000-000000000003");
    public static readonly Guid UserStaffInactive = Guid.Parse("10000000-0000-0000-0000-000000000004");
    public static readonly Guid UserJudgeActive = Guid.Parse("10000000-0000-0000-0000-000000000005");
    public static readonly Guid UserJudgeInactive = Guid.Parse("10000000-0000-0000-0000-000000000006");
    public static readonly Guid UserMentorActive = Guid.Parse("10000000-0000-0000-0000-000000000007");
    public static readonly Guid UserMentorBanned = Guid.Parse("10000000-0000-0000-0000-000000000008");
    public static readonly Guid UserStudentLeaderActive1 = Guid.Parse("10000000-0000-0000-0000-000000000009");
    public static readonly Guid UserStudentMemberActive1 = Guid.Parse("10000000-0000-0000-0000-000000000010");
    public static readonly Guid UserStudentMemberInactive1 = Guid.Parse("10000000-0000-0000-0000-000000000011");
    public static readonly Guid UserStudentLeaderActive2 = Guid.Parse("10000000-0000-0000-0000-000000000012");
    public static readonly Guid UserStudentMemberActive2 = Guid.Parse("10000000-0000-0000-0000-000000000013");
    public static readonly Guid UserStudentLeaderActive3 = Guid.Parse("10000000-0000-0000-0000-000000000014");
    public static readonly Guid UserStudentMemberBanned3 = Guid.Parse("10000000-0000-0000-0000-000000000015");

    // 10 Events
    public static readonly Guid Event1Draft = Guid.Parse("20000000-0000-0000-0000-000000000001");
    public static readonly Guid Event2Published = Guid.Parse("20000000-0000-0000-0000-000000000002");
    public static readonly Guid Event3Closed = Guid.Parse("20000000-0000-0000-0000-000000000003");
    public static readonly Guid Event4Published = Guid.Parse("20000000-0000-0000-0000-000000000004");
    public static readonly Guid Event5Draft = Guid.Parse("20000000-0000-0000-0000-000000000005");
    public static readonly Guid Event6Closed = Guid.Parse("20000000-0000-0000-0000-000000000006");
    public static readonly Guid Event7Published = Guid.Parse("20000000-0000-0000-0000-000000000007");
    public static readonly Guid Event8Draft = Guid.Parse("20000000-0000-0000-0000-000000000008");
    public static readonly Guid Event9Closed = Guid.Parse("20000000-0000-0000-0000-000000000009");
    public static readonly Guid Event10Published = Guid.Parse("20000000-0000-0000-0000-000000000010");
}
