using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class DemoSeed
{
    // Demo users: 10000000-...-000000000030 to 000000000041 (12 users, matching reference convention)
    private static readonly Guid DemoUser1 = Guid.Parse("10000000-0000-0000-0000-000000000030");
    private static readonly Guid DemoUser2 = Guid.Parse("10000000-0000-0000-0000-000000000031");
    private static readonly Guid DemoUser3 = Guid.Parse("10000000-0000-0000-0000-000000000032");
    private static readonly Guid DemoUser4 = Guid.Parse("10000000-0000-0000-0000-000000000033");
    private static readonly Guid DemoUser5 = Guid.Parse("10000000-0000-0000-0000-000000000034");
    private static readonly Guid DemoUser6 = Guid.Parse("10000000-0000-0000-0000-000000000035");
    private static readonly Guid DemoUser7 = Guid.Parse("10000000-0000-0000-0000-000000000036");
    private static readonly Guid DemoUser8 = Guid.Parse("10000000-0000-0000-0000-000000000037");
    private static readonly Guid DemoUser9 = Guid.Parse("10000000-0000-0000-0000-000000000038");
    private static readonly Guid DemoUser10 = Guid.Parse("10000000-0000-0000-0000-000000000039");
    private static readonly Guid DemoUser11 = Guid.Parse("10000000-0000-0000-0000-000000000040");
    private static readonly Guid DemoUser12 = Guid.Parse("10000000-0000-0000-0000-000000000041");

    // Teams: 30000000-...-000000000020 to 000000000024
    private static readonly Guid DemoTeam1 = Guid.Parse("30000000-0000-0000-0000-000000000020");
    private static readonly Guid DemoTeam2 = Guid.Parse("30000000-0000-0000-0000-000000000021");
    private static readonly Guid DemoTeam3 = Guid.Parse("30000000-0000-0000-0000-000000000022");
    private static readonly Guid DemoTeam4 = Guid.Parse("30000000-0000-0000-0000-000000000023");
    private static readonly Guid DemoTeam5 = Guid.Parse("30000000-0000-0000-0000-000000000024");

    // Round (use existing Event2Published Round 1)
    // RoundEvent2R1 = 21000000-...-000000000003

    public static void SeedDemoData(this ModelBuilder modelBuilder)
    {
        var passwordHash = SeedHelper.HashDefaultPassword();

        // ── Users ─────────────────────────────────────────────
        modelBuilder.Entity<Users>().HasData(
            CreateDemoUser(DemoUser1, "thanh.nguyen@demo.local", "Thanh", "Nguyen", "DEMO001", passwordHash),
            CreateDemoUser(DemoUser2, "anh.pham@demo.local", "Anh", "Pham", "DEMO002", passwordHash),
            CreateDemoUser(DemoUser3, "minh.tran@demo.local", "Minh", "Tran", "DEMO003", passwordHash),
            CreateDemoUser(DemoUser4, "hoa.le@demo.local", "Hoa", "Le", "DEMO004", passwordHash),
            CreateDemoUser(DemoUser5, "binh.hoang@demo.local", "Binh", "Hoang", "DEMO005", passwordHash),
            CreateDemoUser(DemoUser6, "lan.vu@demo.local", "Lan", "Vu", "DEMO006", passwordHash),
            CreateDemoUser(DemoUser7, "tuan.do@demo.local", "Tuan", "Do", "DEMO007", passwordHash),
            CreateDemoUser(DemoUser8, "hieu.nguyen@demo.local", "Hieu", "Nguyen", "DEMO008", passwordHash),
            CreateDemoUser(DemoUser9, "quynh.pham@demo.local", "Quynh", "Pham", "DEMO009", passwordHash),
            CreateDemoUser(DemoUser10, "nam.hoang@demo.local", "Nam", "Hoang", "DEMO010", passwordHash),
            CreateDemoUser(DemoUser11, "khoa.le@demo.local", "Khoa", "Le", "DEMO011", passwordHash),
            CreateDemoUser(DemoUser12, "thu.tran@demo.local", "Thu", "Tran", "DEMO012", passwordHash)
        );

        // ── Teams ─────────────────────────────────────────────
        modelBuilder.Entity<Teams>().HasData(
            new Teams { Id = DemoTeam1, Name = "AI Mavericks", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = DemoTeam2, Name = "Eco Guardians", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = DemoTeam3, Name = "Code Visionaries", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = DemoTeam4, Name = "GreenTech Solutions", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = DemoTeam5, Name = "AI Builders", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );

        // ── TeamDetails ───────────────────────────────────────
        modelBuilder.Entity<TeamDetails>().HasData(
            // Team 1: AI Mavericks (leader: Thanh, member: Anh)
            CreateDemoTeamDetail(Guid.Parse("30100000-0000-0000-0000-000000000020"), DemoTeam1, DemoUser1, true),
            CreateDemoTeamDetail(Guid.Parse("30100000-0000-0000-0000-000000000021"), DemoTeam1, DemoUser2, false),
            // Team 2: Eco Guardians (leader: Minh, members: Hoa, Binh)
            CreateDemoTeamDetail(Guid.Parse("30100000-0000-0000-0000-000000000022"), DemoTeam2, DemoUser3, true),
            CreateDemoTeamDetail(Guid.Parse("30100000-0000-0000-0000-000000000023"), DemoTeam2, DemoUser4, false),
            CreateDemoTeamDetail(Guid.Parse("30100000-0000-0000-0000-000000000024"), DemoTeam2, DemoUser5, false),
            // Team 3: Code Visionaries (leader: Lan, member: Tuan)
            CreateDemoTeamDetail(Guid.Parse("30100000-0000-0000-0000-000000000025"), DemoTeam3, DemoUser6, true),
            CreateDemoTeamDetail(Guid.Parse("30100000-0000-0000-0000-000000000026"), DemoTeam3, DemoUser7, false),
            // Team 4: GreenTech Solutions (leader: Hieu, members: Quynh, Nam)
            CreateDemoTeamDetail(Guid.Parse("30100000-0000-0000-0000-000000000027"), DemoTeam4, DemoUser8, true),
            CreateDemoTeamDetail(Guid.Parse("30100000-0000-0000-0000-000000000028"), DemoTeam4, DemoUser9, false),
            CreateDemoTeamDetail(Guid.Parse("30100000-0000-0000-0000-000000000029"), DemoTeam4, DemoUser10, false),
            // Team 5: AI Builders (leader: Khoa, member: Thu)
            CreateDemoTeamDetail(Guid.Parse("30100000-0000-0000-0000-000000000030"), DemoTeam5, DemoUser11, true),
            CreateDemoTeamDetail(Guid.Parse("30100000-0000-0000-0000-000000000031"), DemoTeam5, DemoUser12, false)
        );

        // ── RegisterTeams ─────────────────────────────────────
        // All demo teams register for Event2Published with Track1Ai and Topic1
        modelBuilder.Entity<RegisterTeams>().HasData(
            CreateDemoRegisterTeam(Guid.Parse("31000000-0000-0000-0000-000000000020"), DemoTeam1, SeedConstants.Event2Published, TrackSeed.Track1Ai, TrackSeed.Topic1, "AI Mavericks registration - Spring Published"),
            CreateDemoRegisterTeam(Guid.Parse("31000000-0000-0000-0000-000000000021"), DemoTeam2, SeedConstants.Event2Published, TrackSeed.Track2Web, TrackSeed.Topic2, "Eco Guardians registration - Spring Published"),
            CreateDemoRegisterTeam(Guid.Parse("31000000-0000-0000-0000-000000000022"), DemoTeam3, SeedConstants.Event2Published, TrackSeed.Track3Mobile, TrackSeed.Topic3, "Code Visionaries registration - Spring Published"),
            CreateDemoRegisterTeam(Guid.Parse("31000000-0000-0000-0000-000000000023"), DemoTeam4, SeedConstants.Event2Published, TrackSeed.Track4Iot, TrackSeed.Topic4, "GreenTech Solutions registration - Spring Published"),
            CreateDemoRegisterTeam(Guid.Parse("31000000-0000-0000-0000-000000000024"), DemoTeam5, SeedConstants.Event2Published, TrackSeed.Track5Cloud, TrackSeed.Topic5, "AI Builders registration - Spring Published")
        );

        // ── RoundDetails + Submissions (RoundEvent2R1 = 21000000-...-000000000003) ──
        var demoRoundId = Guid.Parse("21000000-0000-0000-0000-000000000003"); // RoundEvent2R1

        modelBuilder.Entity<RoundDetails>().HasData(
            CreateDemoRoundDetail(Guid.Parse("32000000-0000-0000-0000-000000000020"), demoRoundId, Guid.Parse("31000000-0000-0000-0000-000000000020")),
            CreateDemoRoundDetail(Guid.Parse("32000000-0000-0000-0000-000000000021"), demoRoundId, Guid.Parse("31000000-0000-0000-0000-000000000021")),
            CreateDemoRoundDetail(Guid.Parse("32000000-0000-0000-0000-000000000022"), demoRoundId, Guid.Parse("31000000-0000-0000-0000-000000000022")),
            CreateDemoRoundDetail(Guid.Parse("32000000-0000-0000-0000-000000000023"), demoRoundId, Guid.Parse("31000000-0000-0000-0000-000000000023")),
            CreateDemoRoundDetail(Guid.Parse("32000000-0000-0000-0000-000000000024"), demoRoundId, Guid.Parse("31000000-0000-0000-0000-000000000024"))
        );

        // Submissions for all 5 teams (graded for first 3, submitted for last 2)
        modelBuilder.Entity<Submissions>().HasData(
            new Submissions { Id = Guid.Parse("33000000-0000-0000-0000-000000000020"), RoundDetailId = Guid.Parse("32000000-0000-0000-0000-000000000020"), Url = "https://github.com/demo/ai-mavericks", Description = "Round 1 - AI Mavericks submission", Status = SubmissionStatusEnum.Graded, SubmittedAt = SeedConstants.CreatedAt.AddDays(12), IsRegrade = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Submissions { Id = Guid.Parse("33000000-0000-0000-0000-000000000021"), RoundDetailId = Guid.Parse("32000000-0000-0000-0000-000000000021"), Url = "https://github.com/demo/eco-guardians", Description = "Round 1 - Eco Guardians submission", Status = SubmissionStatusEnum.Graded, SubmittedAt = SeedConstants.CreatedAt.AddDays(12), IsRegrade = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Submissions { Id = Guid.Parse("33000000-0000-0000-0000-000000000022"), RoundDetailId = Guid.Parse("32000000-0000-0000-0000-000000000022"), Url = "https://github.com/demo/code-visionaries", Description = "Round 1 - Code Visionaries submission", Status = SubmissionStatusEnum.Graded, SubmittedAt = SeedConstants.CreatedAt.AddDays(12), IsRegrade = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Submissions { Id = Guid.Parse("33000000-0000-0000-0000-000000000023"), RoundDetailId = Guid.Parse("32000000-0000-0000-0000-000000000023"), Url = null, Description = "Round 1 - GreenTech Solutions submission", Status = SubmissionStatusEnum.Submitted, SubmittedAt = null, IsRegrade = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Submissions { Id = Guid.Parse("33000000-0000-0000-0000-000000000024"), RoundDetailId = Guid.Parse("32000000-0000-0000-0000-000000000024"), Url = null, Description = "Round 1 - AI Builders submission", Status = SubmissionStatusEnum.Submitted, SubmittedAt = null, IsRegrade = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );
    }

    private static Users CreateDemoUser(Guid id, string email, string firstName, string lastName, string studentId, string passwordHash)
    {
        return new Users
        {
            Id = id,
            Email = email,
            HashPassword = passwordHash,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = "0900000000",
            AvatarUrl = "https://robohash.org/" + email,
            Bio = "Demo user",
            Address = "FPT Campus Ho Chi Minh City",
            DateOfBirth = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero),
            StudentId = studentId,
            Role = RoleEnum.Student,
            College = "FPT University",
            ImgUrl = "https://robohash.org/" + email,
            LinkUrl = "",
            VerifyEmailAt = SeedConstants.CreatedAt,
            Status = UserStatusEnum.Active,
            IsVerified = true,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }

    private static TeamDetails CreateDemoTeamDetail(Guid id, Guid teamId, Guid userId, bool isLeader)
    {
        return new TeamDetails
        {
            Id = id,
            TeamId = teamId,
            UserId = userId,
            IsLeader = isLeader,
            Status = TeamDetailStatusEnum.Active,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }

    private static RegisterTeams CreateDemoRegisterTeam(Guid id, Guid teamId, Guid eventId, Guid trackId, Guid topicId, string description)
    {
        return new RegisterTeams
        {
            Id = id,
            TeamId = teamId,
            EventId = eventId,
            TrackId = trackId,
            TopicId = topicId,
            Description = description,
            Status = RegisterTeamStatusEnum.Approved,
            IsBanned = false,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }

    private static RoundDetails CreateDemoRoundDetail(Guid id, Guid roundId, Guid registerTeamId)
    {
        return new RoundDetails
        {
            Id = id,
            RoundId = roundId,
            RegisterTeamId = registerTeamId,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }
}
