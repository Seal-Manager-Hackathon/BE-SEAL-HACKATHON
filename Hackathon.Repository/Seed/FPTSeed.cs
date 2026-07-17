using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Hackathon.Repository.Seed;

public static class FPTSeed
{
    private static readonly DateTimeOffset Now = new(2026, 7, 3, 0, 0, 0, TimeSpan.Zero);
    private static readonly string PasswordHash = SeedHelper.HashDefaultPassword();

    // ── Event 1 (Spring 2026 - ongoing, linked to Event4Published pattern) ──
    private static readonly Guid Ev1Id = Guid.Parse("20000000-0000-0000-0000-000000000100");
    private static readonly Guid Ev1R1Id = Guid.Parse("21000000-0000-0000-0000-000000000100");
    private static readonly Guid Ev1R2Id = Guid.Parse("21000000-0000-0000-0000-000000000101");
    private static readonly Guid Ev1LbId = Guid.Parse("60000000-0000-0000-0000-000000000100");

    // ── Event 2 (Summer 2026 - open for registration) ──
    private static readonly Guid Ev2Id = Guid.Parse("20000000-0000-0000-0000-000000000200");
    private static readonly Guid Ev2R1Id = Guid.Parse("21000000-0000-0000-0000-000000000200");
    private static readonly Guid Ev2R2Id = Guid.Parse("21000000-0000-0000-0000-000000000201");
    private static readonly Guid Ev2LbId = Guid.Parse("60000000-0000-0000-0000-000000000200");

    // ── Tracks Event 1 ──
    private static readonly Guid Ev1TrAi = Guid.Parse("24000000-0000-0000-0000-000000000100");
    private static readonly Guid Ev1TrMobile = Guid.Parse("24000000-0000-0000-0000-000000000101");
    private static readonly Guid Ev1TrWeb = Guid.Parse("24000000-0000-0000-0000-000000000102");
    private static readonly Guid Ev1TrData = Guid.Parse("24000000-0000-0000-0000-000000000103");
    private static readonly Guid Ev1TrCloud = Guid.Parse("24000000-0000-0000-0000-000000000104");
    private static readonly Guid[] Ev1Tracks = [Ev1TrAi, Ev1TrMobile, Ev1TrWeb, Ev1TrData, Ev1TrCloud];

    // ── Tracks Event 2 ──
    private static readonly Guid Ev2TrAi = Guid.Parse("24000000-0000-0000-0000-000000000200");
    private static readonly Guid Ev2TrMobile = Guid.Parse("24000000-0000-0000-0000-000000000201");
    private static readonly Guid Ev2TrWeb = Guid.Parse("24000000-0000-0000-0000-000000000202");
    private static readonly Guid Ev2TrData = Guid.Parse("24000000-0000-0000-0000-000000000203");
    private static readonly Guid Ev2TrCloud = Guid.Parse("24000000-0000-0000-0000-000000000204");
    private static readonly Guid[] Ev2Tracks = [Ev2TrAi, Ev2TrMobile, Ev2TrWeb, Ev2TrData, Ev2TrCloud];

    // ── Topics per Track ──
    private static readonly Guid[] Ev1Topics = [
        Guid.Parse("25000000-0000-0000-0000-000000000100"),
        Guid.Parse("25000000-0000-0000-0000-000000000101"),
        Guid.Parse("25000000-0000-0000-0000-000000000102"),
        Guid.Parse("25000000-0000-0000-0000-000000000103"),
        Guid.Parse("25000000-0000-0000-0000-000000000104"),
    ];
    private static readonly Guid[] Ev2Topics = [
        Guid.Parse("25000000-0000-0000-0000-000000000200"),
        Guid.Parse("25000000-0000-0000-0000-000000000201"),
        Guid.Parse("25000000-0000-0000-0000-000000000202"),
        Guid.Parse("25000000-0000-0000-0000-000000000203"),
        Guid.Parse("25000000-0000-0000-0000-000000000204"),
    ];

    // ── Users: 5 Lecturers (shared between events) ──
    private static readonly Guid[] JudgeIds = [
        Guid.Parse("10000000-0000-0000-0000-000000000280"),
        Guid.Parse("10000000-0000-0000-0000-000000000281"),
        Guid.Parse("10000000-0000-0000-0000-000000000282"),
        Guid.Parse("10000000-0000-0000-0000-000000000283"),
        Guid.Parse("10000000-0000-0000-0000-000000000284"),
    ];
    private static readonly Guid[] MentorIds = [
        Guid.Parse("10000000-0000-0000-0000-000000000285"),
        Guid.Parse("10000000-0000-0000-0000-000000000286"),
        Guid.Parse("10000000-0000-0000-0000-000000000287"),
        Guid.Parse("10000000-0000-0000-0000-000000000288"),
        Guid.Parse("10000000-0000-0000-0000-000000000289"),
    ];
    private static readonly Guid[] StaffIds = [
        Guid.Parse("10000000-0000-0000-0000-000000000290"),
        Guid.Parse("10000000-0000-0000-0000-000000000291"),
        Guid.Parse("10000000-0000-0000-0000-000000000292"),
        Guid.Parse("10000000-0000-0000-0000-000000000293"),
        Guid.Parse("10000000-0000-0000-0000-000000000294"),
    ];

    public static void SeedFPTData(this ModelBuilder modelBuilder)
    {
        // ══════════════════════════════════════════════════════════
        //  USERS
        // ══════════════════════════════════════════════════════════
        var fptStudents = new List<Users>();
        // 30 students for Event 1
        var e1Names = new (string first, string last)[]
        {
            ("Nguyen Van", "An"), ("Tran Thi", "Bich"), ("Le Hoang", "Cuong"),
            ("Pham Minh", "Dung"), ("Vo Thi", "Em"), ("Dang Van", "Phuoc"),
            ("Bui Thi", "Giang"), ("Do Quoc", "Huy"), ("Ho Van", "Hung"),
            ("Ngo Thi", "Hong"), ("Duong Van", "Kien"), ("Ly Thi", "Lan"),
            ("Mai Thanh", "Long"), ("Luong Thi", "Mai"), ("Chu Van", "Manh"),
            ("Cao Thi", "Ngoc"), ("Phan Van", "Nhan"), ("Ta Thi", "Oanh"),
            ("Quach Van", "Phat"), ("La Thi", "Quynh"), ("Su Van", "Son"),
            ("Lam Thi", "Thanh"), ("Kieu Van", "Tien"), ("Dinh Thi", "Tuyet"),
            ("Vuong Van", "Trong"), ("Ha Thi", "Van"), ("Khuc Van", "Xuyen"),
            ("Dao Thi", "Yen"), ("Vu Van", "Binh"), ("Luc Thi", "Nhung"),
        };
        for (int i = 0; i < 30; i++)
        {
            var id = Guid.Parse($"10000000-0000-0000-0000-00000000{200 + i:X4}");
            fptStudents.Add(MakeFptUser(id, $"{ToEmailSlug(e1Names[i].first)}.{ToEmailSlug(e1Names[i].last)}@fpt.edu.vn", e1Names[i].first, e1Names[i].last, $"SE{2018001 + i}"));
        }

        // 30 students for Event 2
        var e2Names = new (string first, string last)[]
        {
            ("Nguyen Thi", "Anh"), ("Tran Van", "Bao"), ("Le Thi", "Chi"),
            ("Pham Van", "Dat"), ("Vo Thi", "Dung"), ("Dang Van", "Duy"),
            ("Bui Thi", "Ha"), ("Do Van", "Hieu"), ("Ho Thi", "Hue"),
            ("Ngo Van", "Khoa"), ("Duong Thi", "Lai"), ("Ly Van", "Loc"),
            ("Mai Thi", "My"), ("Luong Van", "Nam"), ("Chu Thi", "Nga"),
            ("Cao Van", "Phong"), ("Phan Thi", "Phuong"), ("Ta Van", "Quan"),
            ("Quach Thi", "Thao"), ("La Van", "Thang"), ("Su Thi", "Thuy"),
            ("Lam Van", "Tung"), ("Kieu Thi", "Tuoi"), ("Dinh Van", "Vinh"),
            ("Vuong Thi", "Xuan"), ("Ha Van", "Y"), ("Khuc Thi", "Hanh"),
            ("Dao Van", "Loi"), ("Vu Thi", "Lien"), ("Luc Van", "Hai"),
        };
        for (int i = 0; i < 30; i++)
        {
            var id = Guid.Parse($"10000000-0000-0000-0000-00000000{240 + i:X4}");
            fptStudents.Add(MakeFptUser(id, $"{ToEmailSlug(e2Names[i].first)}.{ToEmailSlug(e2Names[i].last)}@fpt.edu.vn", e2Names[i].first, e2Names[i].last, $"SE{2018031 + i}"));
        }

        // 10 Lecturers + 5 Staff
        var lecturers = new List<Users>
        {
            MakeFptUser(JudgeIds[0], "nguyen.thanh.tung@fpt.edu.vn", "Nguyen Thanh", "Tung", "GV001", RoleEnum.Lecturer),
            MakeFptUser(JudgeIds[1], "tran.le.hong@fpt.edu.vn", "Tran Le", "Hong", "GV002", RoleEnum.Lecturer),
            MakeFptUser(JudgeIds[2], "le.quoc.bao@fpt.edu.vn", "Le Quoc", "Bao", "GV003", RoleEnum.Lecturer),
            MakeFptUser(JudgeIds[3], "pham.duc.minh@fpt.edu.vn", "Pham Duc", "Minh", "GV004", RoleEnum.Lecturer),
            MakeFptUser(JudgeIds[4], "hoang.ngoc.son@fpt.edu.vn", "Hoang Ngoc", "Son", "GV005", RoleEnum.Lecturer),
            MakeFptUser(MentorIds[0], "vo.tuan.kiet@fpt.edu.vn", "Vo Tuan", "Kiet", "GV006", RoleEnum.Lecturer),
            MakeFptUser(MentorIds[1], "dang.thuy.linh@fpt.edu.vn", "Dang Thuy", "Linh", "GV007", RoleEnum.Lecturer),
            MakeFptUser(MentorIds[2], "bui.cong.thanh@fpt.edu.vn", "Bui Cong", "Thanh", "GV008", RoleEnum.Lecturer),
            MakeFptUser(MentorIds[3], "do.hoang.yen@fpt.edu.vn", "Do Hoang", "Yen", "GV009", RoleEnum.Lecturer),
            MakeFptUser(MentorIds[4], "ngo.quang.trung@fpt.edu.vn", "Ngo Quang", "Trung", "GV010", RoleEnum.Lecturer),
            MakeFptUser(StaffIds[0], "hoang.mai.anh@fpt.edu.vn", "Hoang Mai", "Anh", "STF006", RoleEnum.Staff),
            MakeFptUser(StaffIds[1], "tran.minh.duc@fpt.edu.vn", "Tran Minh", "Duc", "STF007", RoleEnum.Staff),
            MakeFptUser(StaffIds[2], "le.phuong.thao@fpt.edu.vn", "Le Phuong", "Thao", "STF008", RoleEnum.Staff),
            MakeFptUser(StaffIds[3], "pham.quoc.huy@fpt.edu.vn", "Pham Quoc", "Huy", "STF009", RoleEnum.Staff),
            MakeFptUser(StaffIds[4], "dang.thi.thu@fpt.edu.vn", "Dang Thi", "Thu", "STF010", RoleEnum.Staff),
        };

        modelBuilder.Entity<Users>().HasData(fptStudents.Concat(lecturers));

        // ══════════════════════════════════════════════════════════
        //  EVENTS
        // ══════════════════════════════════════════════════════════
        modelBuilder.Entity<Events>().HasData(
            new Events
            {
                Id = Ev1Id, Name = "SEAL Hackathon 2026 - Spring FPT", Description = "Spring Hackathon for FPT students.",
                StartTime = new DateTimeOffset(2026, 6, 15, 8, 0, 0, TimeSpan.FromHours(7)),
                EndTime = new DateTimeOffset(2026, 7, 30, 17, 0, 0, TimeSpan.FromHours(7)),
                RegisterLimitTime = new DateTimeOffset(2026, 6, 25, 23, 59, 0, TimeSpan.FromHours(7)),
                LimitTeam = 10, MinMember = 2, MaxMember = 4,
                Status = EventStatusEnum.Published, NumberRound = 2, Season = SeasonEnum.Spring,
                IsDisable = false, CreatedAt = Now, UpdatedAt = Now
            },
            new Events
            {
                Id = Ev2Id, Name = "SEAL Hackathon 2026 - Summer FPT", Description = "Summer Hackathon for FPT students.",
                StartTime = new DateTimeOffset(2026, 8, 1, 8, 0, 0, TimeSpan.FromHours(7)),
                EndTime = new DateTimeOffset(2026, 9, 15, 17, 0, 0, TimeSpan.FromHours(7)),
                RegisterLimitTime = new DateTimeOffset(2026, 7, 25, 23, 59, 0, TimeSpan.FromHours(7)),
                LimitTeam = 10, MinMember = 2, MaxMember = 4,
                Status = EventStatusEnum.Published, NumberRound = 2, Season = SeasonEnum.Summer,
                IsDisable = false, CreatedAt = Now, UpdatedAt = Now
            }
        );

        // ══════════════════════════════════════════════════════════
        //  ROUNDS
        // ══════════════════════════════════════════════════════════
        modelBuilder.Entity<Rounds>().HasData(
            new Rounds
            {
                Id = Ev1R1Id, EventId = Ev1Id, Name = "Round 1 - Idea", Description = "Present ideas and plans",
                RoundNo = 1,
                StartTime = new DateTimeOffset(2026, 6, 29, 8, 0, 0, TimeSpan.FromHours(7)),
                EndTime = new DateTimeOffset(2026, 7, 13, 17, 0, 0, TimeSpan.FromHours(7)),
                StartSubmission = new DateTimeOffset(2026, 6, 29, 8, 0, 0, TimeSpan.FromHours(7)),
                EndSubmission = new DateTimeOffset(2026, 7, 10, 23, 59, 0, TimeSpan.FromHours(7)),
                LimitTeam = 10, IsDisable = false, CreatedAt = Now, UpdatedAt = Now
            },
            new Rounds
            {
                Id = Ev1R2Id, EventId = Ev1Id, Name = "Round 2 - Final", Description = "Present the complete product",
                RoundNo = 2,
                StartTime = new DateTimeOffset(2026, 7, 21, 8, 0, 0, TimeSpan.FromHours(7)),
                EndTime = new DateTimeOffset(2026, 7, 30, 17, 0, 0, TimeSpan.FromHours(7)),
                StartSubmission = new DateTimeOffset(2026, 7, 21, 8, 0, 0, TimeSpan.FromHours(7)),
                EndSubmission = new DateTimeOffset(2026, 7, 28, 23, 59, 0, TimeSpan.FromHours(7)),
                LimitTeam = 6, IsDisable = false, CreatedAt = Now, UpdatedAt = Now
            },
            new Rounds
            {
                Id = Ev2R1Id, EventId = Ev2Id, Name = "Round 1 - Idea", Description = "Present ideas and plans",
                RoundNo = 1,
                StartTime = new DateTimeOffset(2026, 8, 4, 8, 0, 0, TimeSpan.FromHours(7)),
                EndTime = new DateTimeOffset(2026, 8, 18, 17, 0, 0, TimeSpan.FromHours(7)),
                StartSubmission = new DateTimeOffset(2026, 8, 4, 8, 0, 0, TimeSpan.FromHours(7)),
                EndSubmission = new DateTimeOffset(2026, 8, 15, 23, 59, 0, TimeSpan.FromHours(7)),
                LimitTeam = 10, IsDisable = false, CreatedAt = Now, UpdatedAt = Now
            },
            new Rounds
            {
                Id = Ev2R2Id, EventId = Ev2Id, Name = "Round 2 - Final", Description = "Present the complete product",
                RoundNo = 2,
                StartTime = new DateTimeOffset(2026, 8, 25, 8, 0, 0, TimeSpan.FromHours(7)),
                EndTime = new DateTimeOffset(2026, 9, 15, 17, 0, 0, TimeSpan.FromHours(7)),
                StartSubmission = new DateTimeOffset(2026, 8, 25, 8, 0, 0, TimeSpan.FromHours(7)),
                EndSubmission = new DateTimeOffset(2026, 9, 12, 23, 59, 0, TimeSpan.FromHours(7)),
                LimitTeam = 6, IsDisable = false, CreatedAt = Now, UpdatedAt = Now
            }
        );

        // ══════════════════════════════════════════════════════════
        //  AWARDS
        // ══════════════════════════════════════════════════════════
        modelBuilder.Entity<Awards>().HasData(
            new Awards { Id = Guid.Parse("26000000-0000-0000-0000-000000000100"), EventId = Ev1Id, Name = "Champion", Description = "First place overall", LevelAward = 1, NumberOfAward = 1, Prize = 5000000m, IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new Awards { Id = Guid.Parse("26000000-0000-0000-0000-000000000101"), EventId = Ev1Id, Name = "Runner-up", Description = "Second place overall", LevelAward = 2, NumberOfAward = 1, Prize = 3000000m, IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new Awards { Id = Guid.Parse("26000000-0000-0000-0000-000000000200"), EventId = Ev2Id, Name = "Champion", Description = "First place overall", LevelAward = 1, NumberOfAward = 1, Prize = 5000000m, IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new Awards { Id = Guid.Parse("26000000-0000-0000-0000-000000000201"), EventId = Ev2Id, Name = "Runner-up", Description = "Second place overall", LevelAward = 2, NumberOfAward = 1, Prize = 3000000m, IsDisable = false, CreatedAt = Now, UpdatedAt = Now }
        );

        // ══════════════════════════════════════════════════════════
        //  TRACKS + TOPICS
        // ══════════════════════════════════════════════════════════
        var tracks = new List<Tracks>
        {
            new() { Id = Ev1TrAi, EventId = Ev1Id, Title = "AI - Artificial Intelligence", Description = "AI-powered solutions", MaxTeam = 2, IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev1TrMobile, EventId = Ev1Id, Title = "Mobile - Mobile Apps", Description = "Mobile platform apps", MaxTeam = 2, IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev1TrWeb, EventId = Ev1Id, Title = "Web - Web Technology", Description = "Modern web apps", MaxTeam = 2, IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev1TrData, EventId = Ev1Id, Title = "Data - Data Science", Description = "Data analysis", MaxTeam = 2, IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev1TrCloud, EventId = Ev1Id, Title = "Cloud - Cloud Computing", Description = "Cloud deployment", MaxTeam = 2, IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev2TrAi, EventId = Ev2Id, Title = "AI - Image Processing", Description = "Intelligent image processing", MaxTeam = 2, IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev2TrMobile, EventId = Ev2Id, Title = "Mobile - Gaming", Description = "Mobile game development", MaxTeam = 2, IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev2TrWeb, EventId = Ev2Id, Title = "Web - E-Commerce", Description = "E-commerce platforms", MaxTeam = 2, IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev2TrData, EventId = Ev2Id, Title = "Data - Machine Learning", Description = "Applied ML", MaxTeam = 2, IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev2TrCloud, EventId = Ev2Id, Title = "Cloud - DevOps", Description = "Infrastructure automation", MaxTeam = 2, IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
        };
        modelBuilder.Entity<Tracks>().HasData(tracks);

        var topics = new List<Topics>
        {
            new() { Id = Ev1Topics[0], TrackId = Ev1TrAi, Title = "Learning Support Chatbot", Description = "AI chatbot for student learning", IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev1Topics[1], TrackId = Ev1TrMobile, Title = "Personal Finance Manager", Description = "Expense management mobile app", IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev1Topics[2], TrackId = Ev1TrWeb, Title = "Volunteer Connection Platform", Description = "Social network for volunteering", IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev1Topics[3], TrackId = Ev1TrData, Title = "Weather Forecasting System", Description = "Data analysis for weather prediction", IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev1Topics[4], TrackId = Ev1TrCloud, Title = "Automated CI/CD System", Description = "Cloud CI/CD pipeline", IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev2Topics[0], TrackId = Ev2TrAi, Title = "Facial Emotion Recognition", Description = "AI emotion recognition via camera", IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev2Topics[1], TrackId = Ev2TrMobile, Title = "Interactive Educational Game", Description = "Learning games for children", IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev2Topics[2], TrackId = Ev2TrWeb, Title = "Online Agricultural Marketplace", Description = "Connecting farmers and buyers", IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev2Topics[3], TrackId = Ev2TrData, Title = "Product Recommendation System", Description = "E-commerce recommendation engine", IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new() { Id = Ev2Topics[4], TrackId = Ev2TrCloud, Title = "System Monitoring Platform", Description = "Cloud infrastructure monitoring", IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
        };
        modelBuilder.Entity<Topics>().HasData(topics);

        // ══════════════════════════════════════════════════════════
        //  CRITERIA TEMPLATES + ITEMS (Event 1 Round 1 only)
        // ══════════════════════════════════════════════════════════
        var tpl1Id = Guid.Parse("22000000-0000-0000-0000-000000000100");
        var tpl2Id = Guid.Parse("22000000-0000-0000-0000-000000000101");
        var tpl3Id = Guid.Parse("22000000-0000-0000-0000-000000000102");

        var criteriaItems = new (Guid id, string name, decimal maxScore)[]
        {
            (Guid.Parse("23000000-0000-0000-0000-000000000100"), "Creativity", 25m),
            (Guid.Parse("23000000-0000-0000-0000-000000000101"), "Feasibility", 25m),
            (Guid.Parse("23000000-0000-0000-0000-000000000102"), "Social Impact", 20m),
            (Guid.Parse("23000000-0000-0000-0000-000000000103"), "Tech Usage", 20m),
            (Guid.Parse("23000000-0000-0000-0000-000000000104"), "Execution Plan", 10m),
        };

        modelBuilder.Entity<CriteriaTemplates>().HasData(
            new CriteriaTemplates { Id = tpl1Id, RoundId = Ev1R1Id, Title = "Idea Evaluation", Description = "Criteria for evaluating ideas in round 1", IsDisable = false, CreatedAt = Now, UpdatedAt = Now },
            new CriteriaTemplates { Id = tpl2Id, RoundId = Ev1R1Id, Title = "Technical Eval (backup)", Description = "Backup template", IsDisable = true, CreatedAt = Now, UpdatedAt = Now },
            new CriteriaTemplates { Id = tpl3Id, RoundId = Ev1R1Id, Title = "Presentation Eval (backup)", Description = "Backup template", IsDisable = true, CreatedAt = Now, UpdatedAt = Now }
        );
        modelBuilder.Entity<CriteriaItems>().HasData(
            criteriaItems.Select((x, i) => new CriteriaItems { Id = x.id, CriteriaTemplateId = tpl1Id, Name = x.name, Description = $"{x.name} criteria", Score = x.maxScore, IsDisable = false, CreatedAt = Now, UpdatedAt = Now }).Concat(
            criteriaItems.Select((x, i) => new CriteriaItems { Id = Guid.Parse($"23000000-0000-0000-0000-00000000010{i + 5:X1}"), CriteriaTemplateId = tpl2Id, Name = x.name, Description = $"Backup {x.name}", Score = x.maxScore, IsDisable = true, CreatedAt = Now, UpdatedAt = Now }).Concat(
            criteriaItems.Select((x, i) => new CriteriaItems { Id = Guid.Parse($"23000000-0000-0000-0000-00000000011{i:X1}"), CriteriaTemplateId = tpl3Id, Name = x.name, Description = $"Backup {x.name}", Score = x.maxScore, IsDisable = true, CreatedAt = Now, UpdatedAt = Now }))));

        // ══════════════════════════════════════════════════════════
        //  TEAMS + TEAMDETAILS + REGISTERTEAMS (Event 1)
        // ══════════════════════════════════════════════════════════
        var e1TeamNames = new[] { "FPT AI Pioneers", "FPT Code Breakers", "FPT Mobile Knights", "FPT Web Wizards", "FPT Data Miners", "FPT Cloud Ninjas", "FPT AI Avengers", "FPT Mobile Stars", "FPT Web Builders", "FPT Data Hawks" };
        var e1TeamTrack = new[] { Ev1TrAi, Ev1TrAi, Ev1TrMobile, Ev1TrMobile, Ev1TrWeb, Ev1TrWeb, Ev1TrData, Ev1TrData, Ev1TrCloud, Ev1TrCloud };
        var e1TeamTopic = new[] { Ev1Topics[0], Ev1Topics[0], Ev1Topics[1], Ev1Topics[1], Ev1Topics[2], Ev1Topics[2], Ev1Topics[3], Ev1Topics[3], Ev1Topics[4], Ev1Topics[4] };

        var e1Teams = new List<Teams>();
        var e1TeamDetails = new List<TeamDetails>();
        var e1RegisterTeams = new List<RegisterTeams>();

        for (int i = 0; i < 10; i++)
        {
            var teamId = Guid.Parse($"30000000-0000-0000-0000-0000000001{i:X1}0");
            e1Teams.Add(new Teams { Id = teamId, Name = e1TeamNames[i], CanEdit = false, IsDisable = false, CreatedAt = Now, UpdatedAt = Now });
            var m1 = Guid.Parse($"10000000-0000-0000-0000-00000000{200 + i * 3:X4}");
            var m2 = Guid.Parse($"10000000-0000-0000-0000-00000000{201 + i * 3:X4}");
            var m3 = Guid.Parse($"10000000-0000-0000-0000-00000000{202 + i * 3:X4}");
            e1TeamDetails.Add(MakeTeamDetail(Guid.Parse($"30100000-0000-0000-0000-0000000001{i:X1}0"), teamId, m1, true));
            e1TeamDetails.Add(MakeTeamDetail(Guid.Parse($"30100000-0000-0000-0000-0000000001{i:X1}1"), teamId, m2, false));
            e1TeamDetails.Add(MakeTeamDetail(Guid.Parse($"30100000-0000-0000-0000-0000000001{i:X1}2"), teamId, m3, false));
            e1RegisterTeams.Add(new RegisterTeams
            {
                Id = Guid.Parse($"31000000-0000-0000-0000-0000000001{i:X1}0"),
                TeamId = teamId, EventId = Ev1Id, TrackId = e1TeamTrack[i], TopicId = e1TeamTopic[i],
                Description = $"Registration - {e1TeamNames[i]}",
                Status = RegisterTeamStatusEnum.Approved, IsBanned = false, IsDisable = false,
                CreatedAt = Now, UpdatedAt = Now
            });
        }

        modelBuilder.Entity<Teams>().HasData(e1Teams);
        modelBuilder.Entity<TeamDetails>().HasData(e1TeamDetails);
        modelBuilder.Entity<RegisterTeams>().HasData(e1RegisterTeams);

        // ══════════════════════════════════════════════════════════
        //  ROUNDDETAILS + SUBMISSIONS (Event 1 Round 1)
        // ══════════════════════════════════════════════════════════
        var e1RoundDetails = new List<RoundDetails>();
        var e1Submissions = new List<Submissions>();
        for (int i = 0; i < 10; i++)
        {
            var rdId = Guid.Parse($"32000000-0000-0000-0000-0000000001{i:X1}0");
            e1RoundDetails.Add(new RoundDetails { Id = rdId, RoundId = Ev1R1Id, RegisterTeamId = Guid.Parse($"31000000-0000-0000-0000-0000000001{i:X1}0"), IsDisable = false, CreatedAt = Now, UpdatedAt = Now });
            e1Submissions.Add(new Submissions
            {
                Id = Guid.Parse($"33000000-0000-0000-0000-0000000001{i:X1}0"),
                RoundDetailId = rdId,
                Url = $"https://github.com/fpt-spring/{i + 1}",
                Description = $"Round 1 submission - {e1TeamNames[i]}",
                Status = i < 6 ? SubmissionStatusEnum.Graded : SubmissionStatusEnum.Submitted,
                SubmittedAt = i < 6 ? new DateTimeOffset(2026, 7, 1, 9 + i, 0, 0, TimeSpan.FromHours(7)) : null,
                IsRegrade = false, IsDisable = false, CreatedAt = Now, UpdatedAt = Now
            });
        }
        modelBuilder.Entity<RoundDetails>().HasData(e1RoundDetails);
        modelBuilder.Entity<Submissions>().HasData(e1Submissions);

        // ══════════════════════════════════════════════════════════
        //  ASSIGNMENTS (Event 1)
        // ══════════════════════════════════════════════════════════
        var assignEvents = new List<AssignEvents>();
        var assignTracks = new List<AssignTracks>();

        // Event 1: 5 judges (one per track)
        for (int i = 0; i < 5; i++)
        {
            var aeId = Guid.Parse($"40000000-0000-0000-0000-0000000001{i:X1}0");
            assignEvents.Add(new AssignEvents { Id = aeId, UserId = JudgeIds[i], EventRoleId = SeedConstants.JudgeEventRoleId, EventId = Ev1Id, IsDisable = false, CreatedAt = Now, UpdatedAt = Now });
            assignTracks.Add(new AssignTracks { Id = Guid.Parse($"41000000-0000-0000-0000-0000000001{i:X1}0"), AssignEventId = aeId, TrackId = Ev1Tracks[i], IsDisable = false, CreatedAt = Now, UpdatedAt = Now });
        }

        // Event 1: 5 mentors
        for (int i = 0; i < 5; i++)
        {
            var aeId = Guid.Parse($"40000000-0000-0000-0000-0000000001{5 + i:X1}0");
            assignEvents.Add(new AssignEvents { Id = aeId, UserId = MentorIds[i], EventRoleId = SeedConstants.MentorEventRoleId, EventId = Ev1Id, IsDisable = false, CreatedAt = Now, UpdatedAt = Now });
            assignTracks.Add(new AssignTracks { Id = Guid.Parse($"41000000-0000-0000-0000-0000000001{5 + i:X1}0"), AssignEventId = aeId, TrackId = Ev1Tracks[i], IsDisable = false, CreatedAt = Now, UpdatedAt = Now });
        }

        // Event 1: 5 staff
        for (int i = 0; i < 5; i++)
        {
            assignEvents.Add(new AssignEvents { Id = Guid.Parse($"40000000-0000-0000-0000-0000000001{10 + i:X1}0"), UserId = StaffIds[i], EventRoleId = SeedConstants.StaffEventRoleId, EventId = Ev1Id, IsDisable = false, CreatedAt = Now, UpdatedAt = Now });
        }

        // Event 2: 5 judges
        for (int i = 0; i < 5; i++)
        {
            var aeId = Guid.Parse($"40000000-0000-0000-0000-0000000002{i:X1}0");
            assignEvents.Add(new AssignEvents { Id = aeId, UserId = JudgeIds[i], EventRoleId = SeedConstants.JudgeEventRoleId, EventId = Ev2Id, IsDisable = false, CreatedAt = Now, UpdatedAt = Now });
            assignTracks.Add(new AssignTracks { Id = Guid.Parse($"41000000-0000-0000-0000-0000000002{i:X1}0"), AssignEventId = aeId, TrackId = Ev2Tracks[i], IsDisable = false, CreatedAt = Now, UpdatedAt = Now });
        }

        // Event 2: 5 mentors
        for (int i = 0; i < 5; i++)
        {
            var aeId = Guid.Parse($"40000000-0000-0000-0000-0000000002{5 + i:X1}0");
            assignEvents.Add(new AssignEvents { Id = aeId, UserId = MentorIds[i], EventRoleId = SeedConstants.MentorEventRoleId, EventId = Ev2Id, IsDisable = false, CreatedAt = Now, UpdatedAt = Now });
            assignTracks.Add(new AssignTracks { Id = Guid.Parse($"41000000-0000-0000-0000-0000000002{5 + i:X1}0"), AssignEventId = aeId, TrackId = Ev2Tracks[i], IsDisable = false, CreatedAt = Now, UpdatedAt = Now });
        }

        // Event 2: 5 staff
        for (int i = 0; i < 5; i++)
        {
            assignEvents.Add(new AssignEvents { Id = Guid.Parse($"40000000-0000-0000-0000-0000000002{10 + i:X1}0"), UserId = StaffIds[i], EventRoleId = SeedConstants.StaffEventRoleId, EventId = Ev2Id, IsDisable = false, CreatedAt = Now, UpdatedAt = Now });
        }

        modelBuilder.Entity<AssignEvents>().HasData(assignEvents);
        modelBuilder.Entity<AssignTracks>().HasData(assignTracks);

        // ══════════════════════════════════════════════════════════
        //  SCORES (Event 1 Round 1 - 6 graded teams)
        // ══════════════════════════════════════════════════════════
        var scores = new List<Scores>();
        var scoreItems = new List<ScoreItems>();

        for (int t = 0; t < 5; t++)
        {
            var judgeAtId = Guid.Parse($"41000000-0000-0000-0000-0000000001{t:X1}0");
            for (int teamIdx = 0; teamIdx < 2; teamIdx++)
            {
                var globalIdx = t * 2 + teamIdx;
                if (globalIdx >= 6) break; // only first 6 teams graded

                var subId = Guid.Parse($"33000000-0000-0000-0000-0000000001{globalIdx:X1}0");
                var scoreId = Guid.Parse($"50000000-0000-0000-0000-0000000001{globalIdx:X1}0");

                var seed = globalIdx * 7 + 50;
                var total = seed % 41 + 60m;
                var itemVals = new decimal[5];
                var remaining = total;
                for (int si = 0; si < 4; si++)
                {
                    var maxI = criteriaItems[si].maxScore;
                    var portion = remaining * (maxI / (total > 0 ? total : 100));
                    itemVals[si] = Math.Max(0, Math.Min(maxI, Math.Round(portion, 1)));
                    remaining -= itemVals[si];
                }
                itemVals[4] = Math.Max(0, Math.Min(criteriaItems[4].maxScore, Math.Round(remaining, 1)));

                scores.Add(new Scores { Id = scoreId, SubmissionId = subId, AssignTrackId = judgeAtId, IsRetake = false, TotalScore = itemVals.Sum(), IsMock = false, IsDisable = false, CreatedAt = Now, UpdatedAt = Now });

                for (int si = 0; si < 5; si++)
                {
                    var comment = itemVals[si] >= criteriaItems[si].maxScore * 0.8m ? "Excellent" :
                        itemVals[si] >= criteriaItems[si].maxScore * 0.6m ? "Good" : "Average";
                    scoreItems.Add(new ScoreItems
                    {
                        Id = Guid.Parse($"51000000-0000-0000-0000-0000000001{globalIdx:X1}{si:X1}"),
                        ScoreId = scoreId, CriteriaItemId = criteriaItems[si].id, AssignTrackId = judgeAtId,
                        Score = itemVals[si], Comment = comment, IsDisable = false, CreatedAt = Now, UpdatedAt = Now
                    });
                }
            }
        }

        modelBuilder.Entity<Scores>().HasData(scores);
        modelBuilder.Entity<ScoreItems>().HasData(scoreItems);

        // ══════════════════════════════════════════════════════════
        //  LEADERBOARD (Event 1)
        // ══════════════════════════════════════════════════════════
        modelBuilder.Entity<LeaderBoards>().HasData(
            new LeaderBoards { Id = Ev1LbId, EventId = Ev1Id, Year = 2026, IsLocked = false, IsPublished = false, IsDisable = false, CreatedAt = Now, UpdatedAt = Now }
        );

        var lbDetails = new List<LeaderBoardDetails>();
        for (int i = 0; i < 6; i++)
        {
            lbDetails.Add(new LeaderBoardDetails
            {
                Id = Guid.Parse($"61000000-0000-0000-0000-0000000001{i:X1}0"),
                LeaderBoardId = Ev1LbId,
                TeamId = Guid.Parse($"30000000-0000-0000-0000-0000000001{i:X1}0"),
                Score = scores.Where(s => s.SubmissionId == Guid.Parse($"33000000-0000-0000-0000-0000000001{i:X1}0")).Sum(s => s.TotalScore),
                LevelAward = i < 1 ? 1 : i < 2 ? 2 : null,
                IsDisable = false, CreatedAt = Now, UpdatedAt = Now
            });
        }
        modelBuilder.Entity<LeaderBoardDetails>().HasData(lbDetails);

        // ══════════════════════════════════════════════════════════
        //  NOTIFICATIONS
        // ══════════════════════════════════════════════════════════
        var notifications = new List<Notifications>();
        for (int i = 0; i < 10; i++)
        {
            var leaderUserId = Guid.Parse($"10000000-0000-0000-0000-00000000{200 + i * 3:X4}");
            notifications.Add(new Notifications
            {
                Id = Guid.Parse($"71000000-0000-0000-0000-0000000001{i:X1}0"),
                UserId = leaderUserId, TeamId = Guid.Parse($"30000000-0000-0000-0000-0000000001{i:X1}0"),
                Title = "Registration Approved", Status = NotificationStatusEnum.Read,
                Description = $"Team {e1TeamNames[i]} approved for SEAL Spring 2026.",
                TargetType = NotificationTargetTypeEnum.Personal,
                IsDisable = false, CreatedAt = Now, UpdatedAt = Now
            });
        }
        modelBuilder.Entity<Notifications>().HasData(notifications);
    }

    // ══════════════════════════════════════════════════════════════
    //  HELPERS
    // ══════════════════════════════════════════════════════════════

    private static Users MakeFptUser(Guid id, string email, string firstName, string lastName, string studentId, RoleEnum role = RoleEnum.Student)
    {
        return new Users
        {
            Id = id, Email = email, HashPassword = PasswordHash,
            FirstName = firstName, LastName = lastName, PhoneNumber = "0900000000",
            AvatarUrl = $"https://robohash.org/{email}",
            Bio = role == RoleEnum.Student ? "Student at FPT University" : "Lecturer at FPT University",
            Address = "Saigon Hi-Tech Park, District 9, Ho Chi Minh City",
            DateOfBirth = role == RoleEnum.Student ? new DateTimeOffset(2002, 1, 1, 0, 0, 0, TimeSpan.Zero) : new DateTimeOffset(1990, 1, 1, 0, 0, 0, TimeSpan.Zero),
            StudentId = studentId, Role = role, College = "FPT University",
            ImgUrl = $"https://robohash.org/{email}", LinkUrl = $"https://fpt.edu.vn/users/{studentId}",
            VerifyEmailAt = Now, Status = UserStatusEnum.Active, IsVerified = true, IsDisable = false,
            CreatedAt = Now, UpdatedAt = Now
        };
    }

    private static TeamDetails MakeTeamDetail(Guid id, Guid teamId, Guid userId, bool isLeader)
    {
        return new TeamDetails { Id = id, TeamId = teamId, UserId = userId, IsLeader = isLeader, Status = TeamDetailStatusEnum.Active, IsDisable = false, CreatedAt = Now, UpdatedAt = Now };
    }

    private static string ToEmailSlug(string name)
    {
        var normalized = name.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (char c in normalized)
        {
            var uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
            if (uc != System.Globalization.UnicodeCategory.NonSpacingMark && c != ' ')
                sb.Append(c);
        }
        return sb.ToString().Normalize(NormalizationForm.FormC).ToLower();
    }
}
