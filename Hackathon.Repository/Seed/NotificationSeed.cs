using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

// Notification IDs: 40000000-xxxx, MentorNotif IDs: 41000000-xxxx
public static class NotificationSeed
{
    public static readonly Guid Notif1 = Guid.Parse("40000000-0000-0000-0000-000000000001");
    public static readonly Guid Notif2 = Guid.Parse("40000000-0000-0000-0000-000000000002");
    public static readonly Guid Notif3 = Guid.Parse("40000000-0000-0000-0000-000000000003");
    public static readonly Guid Notif4 = Guid.Parse("40000000-0000-0000-0000-000000000004");
    public static readonly Guid Notif5 = Guid.Parse("40000000-0000-0000-0000-000000000005");
    public static readonly Guid Notif6 = Guid.Parse("40000000-0000-0000-0000-000000000006");
    public static readonly Guid Notif7 = Guid.Parse("40000000-0000-0000-0000-000000000007");
    public static readonly Guid Notif8 = Guid.Parse("40000000-0000-0000-0000-000000000008");
    public static readonly Guid Notif9 = Guid.Parse("40000000-0000-0000-0000-000000000009");
    public static readonly Guid Notif10 = Guid.Parse("40000000-0000-0000-0000-000000000010");
    public static readonly Guid Notif11 = Guid.Parse("40000000-0000-0000-0000-000000000011");
    public static readonly Guid Notif12 = Guid.Parse("40000000-0000-0000-0000-000000000012");
    public static readonly Guid Notif13 = Guid.Parse("40000000-0000-0000-0000-000000000013");
    public static readonly Guid Notif14 = Guid.Parse("40000000-0000-0000-0000-000000000014");
    public static readonly Guid Notif15 = Guid.Parse("40000000-0000-0000-0000-000000000015");
    public static readonly Guid Notif16 = Guid.Parse("40000000-0000-0000-0000-000000000016");
    public static readonly Guid Notif17 = Guid.Parse("40000000-0000-0000-0000-000000000017");
    public static readonly Guid Notif18 = Guid.Parse("40000000-0000-0000-0000-000000000018");
    public static readonly Guid Notif19 = Guid.Parse("40000000-0000-0000-0000-000000000019");
    public static readonly Guid Notif20 = Guid.Parse("40000000-0000-0000-0000-000000000020");
    public static readonly Guid Notif21 = Guid.Parse("40000000-0000-0000-0000-000000000021");
    public static readonly Guid Notif22 = Guid.Parse("40000000-0000-0000-0000-000000000022");
    public static readonly Guid Notif23 = Guid.Parse("40000000-0000-0000-0000-000000000023");
    public static readonly Guid Notif24 = Guid.Parse("40000000-0000-0000-0000-000000000024");
    public static readonly Guid Notif25 = Guid.Parse("40000000-0000-0000-0000-000000000025");
    public static readonly Guid Notif26 = Guid.Parse("40000000-0000-0000-0000-000000000026");
    public static readonly Guid Notif27 = Guid.Parse("40000000-0000-0000-0000-000000000027");
    public static readonly Guid Notif28 = Guid.Parse("40000000-0000-0000-0000-000000000028");
    public static readonly Guid Notif29 = Guid.Parse("40000000-0000-0000-0000-000000000029");
    public static readonly Guid Notif30 = Guid.Parse("40000000-0000-0000-0000-000000000030");
    public static readonly Guid Notif31 = Guid.Parse("40000000-0000-0000-0000-000000000031");
    public static readonly Guid Notif32 = Guid.Parse("40000000-0000-0000-0000-000000000032");
    public static readonly Guid Notif33 = Guid.Parse("40000000-0000-0000-0000-000000000033");
    public static readonly Guid Notif34 = Guid.Parse("40000000-0000-0000-0000-000000000034");
    public static readonly Guid Notif35 = Guid.Parse("40000000-0000-0000-0000-000000000035");

    public static readonly Guid Mn1 = Guid.Parse("41000000-0000-0000-0000-000000000001");
    public static readonly Guid Mn2 = Guid.Parse("41000000-0000-0000-0000-000000000002");
    public static readonly Guid Mn3 = Guid.Parse("41000000-0000-0000-0000-000000000003");
    public static readonly Guid Mn4 = Guid.Parse("41000000-0000-0000-0000-000000000004");
    public static readonly Guid Mn5 = Guid.Parse("41000000-0000-0000-0000-000000000005");
    public static readonly Guid Mn6 = Guid.Parse("41000000-0000-0000-0000-000000000006");
    public static readonly Guid Mn7 = Guid.Parse("41000000-0000-0000-0000-000000000007");
    public static readonly Guid Mn8 = Guid.Parse("41000000-0000-0000-0000-000000000008");
    public static readonly Guid Mn9 = Guid.Parse("41000000-0000-0000-0000-000000000009");
    public static readonly Guid Mn10 = Guid.Parse("41000000-0000-0000-0000-000000000010");
    public static readonly Guid Mn11 = Guid.Parse("41000000-0000-0000-0000-000000000011");
    public static readonly Guid Mn12 = Guid.Parse("41000000-0000-0000-0000-000000000012");
    public static readonly Guid Mn13 = Guid.Parse("41000000-0000-0000-0000-000000000013");
    public static readonly Guid Mn14 = Guid.Parse("41000000-0000-0000-0000-000000000014");
    public static readonly Guid Mn15 = Guid.Parse("41000000-0000-0000-0000-000000000015");

    public static void SeedNotifications(this ModelBuilder modelBuilder)
    {
        var c = SeedConstants.CreatedAt;

        // ── 35 Notifications ──────────────────────────────────────────
        modelBuilder.Entity<Notifications>().HasData(
            // Personal notifications
            Create(Notif1, SeedConstants.UserStudentLeader1, null, "Đăng ký thành công", "Bạn đã đăng ký event thành công", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Read),
            Create(Notif2, SeedConstants.UserStudentLeader2, null, "Kết quả vòng 1", "Đội của bạn đã qua vòng 1", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Read),
            Create(Notif3, SeedConstants.UserStudentLeader3, null, "Nhắc nhở nộp bài", "Còn 2 ngày để nộp bài vòng 2", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Unread),
            Create(Notif4, SeedConstants.UserStudentLeader4, null, "Kết quả chung cuộc", "Chúc mừng đội bạn đạt giải", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Read),
            Create(Notif5, SeedConstants.UserStudentMember1, null, "Thông báo từ hệ thống", "Lịch trình event đã được cập nhật", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Unread),
            Create(Notif6, SeedConstants.UserStudentBanned, null, "Tài khoản bị khóa", "Tài khoản của bạn đã bị khóa vì vi phạm", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Read),
            Create(Notif7, SeedConstants.UserStaff1, null, "Báo cáo mới", "Có báo cáo mới cần xử lý", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Unread),
            Create(Notif8, SeedConstants.UserJudge1, null, "Lịch chấm điểm", "Bạn được phân công chấm điểm event E2", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Read),
            Create(Notif9, SeedConstants.UserJudge2, null, "Nhắc nhở chấm điểm", "Còn bài chấm điểm chưa hoàn thành", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Unread),
            Create(Notif10, SeedConstants.UserMentor1, null, "Team cần mentor", "Team Alpha Coders cần sự hỗ trợ", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Read),
            // Team notifications
            Create(Notif11, null, TeamSeed.Team1, "Thông báo đội", "Đội bạn đã được duyệt tham gia event", NotificationTargetTypeEnum.Team, NotificationStatusEnum.Read),
            Create(Notif12, null, TeamSeed.Team2, "Kết quả vòng loại", "Đội bạn đã vào vòng trong", NotificationTargetTypeEnum.Team, NotificationStatusEnum.Read),
            Create(Notif13, null, TeamSeed.Team3, "Lịch trình cập nhật", "Lịch trình vòng 2 đã được thay đổi", NotificationTargetTypeEnum.Team, NotificationStatusEnum.Unread),
            Create(Notif14, null, TeamSeed.Team6, "Kết quả chung kết", "Đội bạn đã hoàn thành event", NotificationTargetTypeEnum.Team, NotificationStatusEnum.Read),
            Create(Notif15, null, TeamSeed.Team10, "Chúc mừng", "Đội bạn dẫn đầu bảng xếp hạng", NotificationTargetTypeEnum.Team, NotificationStatusEnum.Unread),
            Create(Notif16, null, TeamSeed.Team18, "Thông báo quan trọng", "Vòng chung kết sẽ diễn ra vào tuần sau", NotificationTargetTypeEnum.Team, NotificationStatusEnum.Unread),
            Create(Notif17, null, TeamSeed.Team24, "Nhắc nhở", "Vui lòng hoàn thành hồ sơ đội", NotificationTargetTypeEnum.Team, NotificationStatusEnum.Pending),
            // System notifications
            Create(Notif18, null, null, "Bảo trì hệ thống", "Hệ thống sẽ bảo trì vào 2h sáng CN", NotificationTargetTypeEnum.System, NotificationStatusEnum.Unread),
            Create(Notif19, null, null, "Mở đăng ký event mới", "Event mới đã mở đăng ký", NotificationTargetTypeEnum.System, NotificationStatusEnum.Read),
            Create(Notif20, null, null, "Thay đổi thể lệ", "Thể lệ event đã được cập nhật", NotificationTargetTypeEnum.System, NotificationStatusEnum.Unread),
            // More notifications to cover all scenarios
            Create(Notif21, SeedConstants.UserStudentLeader5, null, "Giải thưởng", "Đội bạn đã nhận được giải đặc biệt", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Unread),
            Create(Notif22, SeedConstants.UserStudentLeader6, null, "Mời vào đội", "Bạn được mời vào đội mới", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Pending),
            Create(Notif23, SeedConstants.UserStudentMember8, null, "Cập nhật hồ sơ", "Vui lòng cập nhật thông tin cá nhân", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Unread),
            Create(Notif24, SeedConstants.UserStudentMember9, null, "Xác nhận tham gia", "Xác nhận tham gia event E2", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Read),
            Create(Notif25, SeedConstants.UserStudentMember10, null, "Hết hạn đăng ký", "Đăng ký event sẽ đóng trong 24h", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Unread),
            Create(Notif26, SeedConstants.UserStudentMember11, null, "Phản hồi báo cáo", "Báo cáo của bạn đã được xử lý", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Read),
            Create(Notif27, null, TeamSeed.Team5, "Đội bị từ chối", "Đội bạn không được duyệt tham gia", NotificationTargetTypeEnum.Team, NotificationStatusEnum.Read),
            Create(Notif28, null, TeamSeed.Team15, "Xếp hạng E6", "Kết quả chung cuộc E6 đã có", NotificationTargetTypeEnum.Team, NotificationStatusEnum.Read),
            Create(Notif29, null, TeamSeed.Team22, "Winter results", "E9 final results published", NotificationTargetTypeEnum.Team, NotificationStatusEnum.Unread),
            Create(Notif30, null, null, "Sự kiện sắp diễn ra", "E2 sẽ bắt đầu trong 3 ngày tới", NotificationTargetTypeEnum.System, NotificationStatusEnum.Unread),
            Create(Notif31, null, null, "Cập nhật phiên bản", "Hệ thống đã được nâng cấp lên phiên bản mới", NotificationTargetTypeEnum.System, NotificationStatusEnum.Read),
            Create(Notif32, SeedConstants.UserJudge3, null, "Phân công mới", "Bạn được phân công chấm E3", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Read),
            Create(Notif33, SeedConstants.UserJudge4, null, "Lịch chấm E7", "Lịch chấm E7 đã được sắp xếp", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Unread),
            Create(Notif34, SeedConstants.UserMentor3, null, "Team cần mentor E4", "Green Warriors cần hướng dẫn", NotificationTargetTypeEnum.Personal, NotificationStatusEnum.Pending),
            Create(Notif35, null, TeamSeed.Team4, "Thông báo pending", "Đăng ký của bạn đang chờ xét duyệt", NotificationTargetTypeEnum.Team, NotificationStatusEnum.Unread)
        );

        // ── 15 MentorNotifications ────────────────────────────────────
        modelBuilder.Entity<MentorNotifications>().HasData(
            CreateMn(Mn1, AssignmentSeed.At6, "Mentor request", "Team1 (AI) needs mentor help"),
            CreateMn(Mn2, AssignmentSeed.At7, "Progress update", "Team2 (Web) updated their progress"),
            CreateMn(Mn3, AssignmentSeed.At6, "Urgent help", "Team1 blocked on deployment"),
            CreateMn(Mn4, AssignmentSeed.At16, "New team", "Team10 (Green Tech) assigned to you"),
            CreateMn(Mn5, AssignmentSeed.At16, "Question", "Team10 asks about tech stack"),
            CreateMn(Mn6, AssignmentSeed.At23, "Mentor request", "Team18 (Cyber) needs guidance"),
            CreateMn(Mn7, AssignmentSeed.At23, "Progress check", "Team18 weekly update"),
            CreateMn(Mn8, AssignmentSeed.At6, "Submission ready", "Team1 submitted R1"),
            CreateMn(Mn9, AssignmentSeed.At7, "Thank you", "Team2 thanks for mentoring"),
            CreateMn(Mn10, AssignmentSeed.At16, "Meeting request", "Team10 wants to schedule meeting"),
            CreateMn(Mn11, AssignmentSeed.At23, "Emergency", "Team18 has critical bug"),
            CreateMn(Mn12, AssignmentSeed.At6, "Final demo", "Team1 ready for final demo"),
            CreateMn(Mn13, AssignmentSeed.At7, "Code review", "Team2 needs code review"),
            CreateMn(Mn14, AssignmentSeed.At16, "Report", "Team10 monthly progress report"),
            CreateMn(Mn15, AssignmentSeed.At23, "Completed", "Team18 completed the program")
        );
    }

    private static Notifications Create(Guid id, Guid? userId, Guid? teamId, string title, string desc, NotificationTargetTypeEnum targetType, NotificationStatusEnum status) => new()
    {
        Id = id, UserId = userId, TeamId = teamId, Title = title, Description = desc,
        TargetType = targetType, Status = status,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };

    private static MentorNotifications CreateMn(Guid id, Guid assignTrackId, string title, string desc) => new()
    {
        Id = id, AssignTrackId = assignTrackId, Title = title, Description = desc,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };
}
