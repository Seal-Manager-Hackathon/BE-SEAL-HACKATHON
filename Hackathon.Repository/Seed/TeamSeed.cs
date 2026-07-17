using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class TeamSeed
{
    // 25 Team IDs
    public static readonly Guid Team1  = Guid.Parse("30000000-0000-0000-0000-000000000001");
    public static readonly Guid Team2  = Guid.Parse("30000000-0000-0000-0000-000000000002");
    public static readonly Guid Team3  = Guid.Parse("30000000-0000-0000-0000-000000000003");
    public static readonly Guid Team4  = Guid.Parse("30000000-0000-0000-0000-000000000004");
    public static readonly Guid Team5  = Guid.Parse("30000000-0000-0000-0000-000000000005");
    public static readonly Guid Team6  = Guid.Parse("30000000-0000-0000-0000-000000000006");
    public static readonly Guid Team7  = Guid.Parse("30000000-0000-0000-0000-000000000007");
    public static readonly Guid Team8  = Guid.Parse("30000000-0000-0000-0000-000000000008");
    public static readonly Guid Team9  = Guid.Parse("30000000-0000-0000-0000-000000000009");
    public static readonly Guid Team10 = Guid.Parse("30000000-0000-0000-0000-000000000010");
    public static readonly Guid Team11 = Guid.Parse("30000000-0000-0000-0000-000000000011");
    public static readonly Guid Team12 = Guid.Parse("30000000-0000-0000-0000-000000000012");
    public static readonly Guid Team13 = Guid.Parse("30000000-0000-0000-0000-000000000013");
    public static readonly Guid Team14 = Guid.Parse("30000000-0000-0000-0000-000000000014");
    public static readonly Guid Team15 = Guid.Parse("30000000-0000-0000-0000-000000000015");
    public static readonly Guid Team16 = Guid.Parse("30000000-0000-0000-0000-000000000016");
    public static readonly Guid Team17 = Guid.Parse("30000000-0000-0000-0000-000000000017");
    public static readonly Guid Team18 = Guid.Parse("30000000-0000-0000-0000-000000000018");
    public static readonly Guid Team19 = Guid.Parse("30000000-0000-0000-0000-000000000019");
    public static readonly Guid Team20 = Guid.Parse("30000000-0000-0000-0000-000000000020");
    public static readonly Guid Team21 = Guid.Parse("30000000-0000-0000-0000-000000000021");
    public static readonly Guid Team22 = Guid.Parse("30000000-0000-0000-0000-000000000022");
    public static readonly Guid Team23 = Guid.Parse("30000000-0000-0000-0000-000000000023");
    public static readonly Guid Team24 = Guid.Parse("30000000-0000-0000-0000-000000000024");
    public static readonly Guid Team25 = Guid.Parse("30000000-0000-0000-0000-000000000025");

    // 25 RegisterTeam IDs
    public static readonly Guid RegTeam1  = Guid.Parse("31000000-0000-0000-0000-000000000001");
    public static readonly Guid RegTeam2  = Guid.Parse("31000000-0000-0000-0000-000000000002");
    public static readonly Guid RegTeam3  = Guid.Parse("31000000-0000-0000-0000-000000000003");
    public static readonly Guid RegTeam4  = Guid.Parse("31000000-0000-0000-0000-000000000004");
    public static readonly Guid RegTeam5  = Guid.Parse("31000000-0000-0000-0000-000000000005");
    public static readonly Guid RegTeam6  = Guid.Parse("31000000-0000-0000-0000-000000000006");
    public static readonly Guid RegTeam7  = Guid.Parse("31000000-0000-0000-0000-000000000007");
    public static readonly Guid RegTeam8  = Guid.Parse("31000000-0000-0000-0000-000000000008");
    public static readonly Guid RegTeam9  = Guid.Parse("31000000-0000-0000-0000-000000000009");
    public static readonly Guid RegTeam10 = Guid.Parse("31000000-0000-0000-0000-000000000010");
    public static readonly Guid RegTeam11 = Guid.Parse("31000000-0000-0000-0000-000000000011");
    public static readonly Guid RegTeam12 = Guid.Parse("31000000-0000-0000-0000-000000000012");
    public static readonly Guid RegTeam13 = Guid.Parse("31000000-0000-0000-0000-000000000013");
    public static readonly Guid RegTeam14 = Guid.Parse("31000000-0000-0000-0000-000000000014");
    public static readonly Guid RegTeam15 = Guid.Parse("31000000-0000-0000-0000-000000000015");
    public static readonly Guid RegTeam16 = Guid.Parse("31000000-0000-0000-0000-000000000016");
    public static readonly Guid RegTeam17 = Guid.Parse("31000000-0000-0000-0000-000000000017");
    public static readonly Guid RegTeam18 = Guid.Parse("31000000-0000-0000-0000-000000000018");
    public static readonly Guid RegTeam19 = Guid.Parse("31000000-0000-0000-0000-000000000019");
    public static readonly Guid RegTeam20 = Guid.Parse("31000000-0000-0000-0000-000000000020");
    public static readonly Guid RegTeam21 = Guid.Parse("31000000-0000-0000-0000-000000000021");
    public static readonly Guid RegTeam22 = Guid.Parse("31000000-0000-0000-0000-000000000022");
    public static readonly Guid RegTeam23 = Guid.Parse("31000000-0000-0000-0000-000000000023");
    public static readonly Guid RegTeam24 = Guid.Parse("31000000-0000-0000-0000-000000000024");
    public static readonly Guid RegTeam25 = Guid.Parse("31000000-0000-0000-0000-000000000025");

    public static void SeedTeams(this ModelBuilder modelBuilder)
    {
        // 25 Teams
        modelBuilder.Entity<Teams>().HasData(
            new Teams { Id = Team1,  Name = "Alpha Tech",        CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team2,  Name = "Beta Coders",       CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team3,  Name = "Gamma Devs",        CanEdit = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team4,  Name = "Delta Force",       CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team5,  Name = "Epsilon AI",        CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team6,  Name = "Zeta Hackers",      CanEdit = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team7,  Name = "Eta Innovators",    CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team8,  Name = "Theta System",      CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team9,  Name = "Iota Solutions",    CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team10, Name = "Kappa Web",         CanEdit = true,  IsDisable = true,  CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team11, Name = "Lambda Cyber",      CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team12, Name = "Mu Mobile",         CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team13, Name = "Nu Blockchain",     CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team14, Name = "Xi IoT",            CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team15, Name = "Omicron Cloud",     CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team16, Name = "Pi Data",           CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team17, Name = "Rho Security",       CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team18, Name = "Sigma Blockchain",  CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team19, Name = "Tau Game",          CanEdit = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team20, Name = "Upsilon Mobile",    CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team21, Name = "Phi AI",            CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team22, Name = "Chi Cloud",         CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team23, Name = "Psi Web",           CanEdit = true,  IsDisable = true,  CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team24, Name = "Omega IoT",         CanEdit = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team25, Name = "Nova Cloud",        CanEdit = true,  IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );

        // 40 TeamDetails connecting users to teams
        modelBuilder.Entity<TeamDetails>().HasData(
            // Team 1 (Leader1 + Member1 + Member2)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000001"), Team1,  SeedConstants.UserStudentLeader1, true),
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000002"), Team1,  SeedConstants.UserStudentMember1,  false),
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000003"), Team1,  SeedConstants.UserStudentMember2,  false),
            // Team 2 (Leader2 + Member3)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000004"), Team2,  SeedConstants.UserStudentLeader2, true),
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000005"), Team2,  SeedConstants.UserStudentMember3,  false),
            // Team 3 (Leader3 + BannedMember)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000006"), Team3,  SeedConstants.UserStudentLeader3, true),
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000007"), Team3,  SeedConstants.UserStudentBanned,  false),
            // Team 4 (Leader4 + Member4)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000008"), Team4,  SeedConstants.UserStudentLeader4, true),
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000009"), Team4,  SeedConstants.UserStudentMember4,  false),
            // Team 5 (Leader5 + Member5)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000010"), Team5,  SeedConstants.UserStudentLeader5, true),
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000011"), Team5,  SeedConstants.UserStudentMember5,  false),
            // Team 6 (Leader6)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000012"), Team6,  SeedConstants.UserStudentLeader6, true),
            // Team 7 (Leader7 + Member6)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000013"), Team7,  SeedConstants.UserStudentLeader7, true),
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000014"), Team7,  SeedConstants.UserStudentMember6,  false),
            // Team 8 (Leader1 + Member7)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000015"), Team8,  SeedConstants.UserStudentLeader1, true),
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000016"), Team8,  SeedConstants.UserStudentMember7,  false),
            // Team 9 (Leader2 + Member8)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000017"), Team9,  SeedConstants.UserStudentLeader2, true),
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000018"), Team9,  SeedConstants.UserStudentMember8,  false),
            // Team 10 (Leader3 - disabled team)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000019"), Team10, SeedConstants.UserStudentLeader3, true),
            // Team 11 (Leader4 + Member9)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000020"), Team11, SeedConstants.UserStudentLeader4, true),
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000021"), Team11, SeedConstants.UserStudentMember9,  false),
            // Team 12 (Leader5 + Member10)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000022"), Team12, SeedConstants.UserStudentLeader5, true),
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000023"), Team12, SeedConstants.UserStudentMember10, false),
            // Team 13 (Leader6)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000024"), Team13, SeedConstants.UserStudentLeader6, true),
            // Team 14 (Leader7)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000025"), Team14, SeedConstants.UserStudentLeader7, true),
            // Team 15 (Leader1 - 3rd team for Leader1)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000026"), Team15, SeedConstants.UserStudentLeader1, true),
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000027"), Team15, SeedConstants.UserStudentMember1,  false),
            // Team 16 (Leader2)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000028"), Team16, SeedConstants.UserStudentLeader2, true),
            // Team 17 (Leader3)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000029"), Team17, SeedConstants.UserStudentLeader3, true),
            // Team 18 (Leader4)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000030"), Team18, SeedConstants.UserStudentLeader4, true),
            // Team 19 (Leader5 - CanEdit=false)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000031"), Team19, SeedConstants.UserStudentLeader5, true),
            // Team 20 (Leader6)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000032"), Team20, SeedConstants.UserStudentLeader6, true),
            // Team 21 (Leader7)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000033"), Team21, SeedConstants.UserStudentLeader7, true),
            // Team 22 (Leader1)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000034"), Team22, SeedConstants.UserStudentLeader1, true),
            // Team 23 (Leader2 - disabled team)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000035"), Team23, SeedConstants.UserStudentLeader2, true),
            // Team 24 (Leader3 - CanEdit=false)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000036"), Team24, SeedConstants.UserStudentLeader3, true),
            // Team 25 (Leader4)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000037"), Team25, SeedConstants.UserStudentLeader4, true),
            // Inactive member (InactiveStudent in Team1)
            CreateTd(Guid.Parse("30100000-0000-0000-0000-000000000038"), Team1,  SeedConstants.UserStudentInactive, false, TeamDetailStatusEnum.Inactive)
        );

        // 25 RegisterTeams (various statuses)
        modelBuilder.Entity<RegisterTeams>().HasData(
            // Event 2 (Published) - 5 approved teams
            CreateReg(RegTeam1,  Team1,  SeedConstants.Event2Published, TrackSeed.Track1Ai,  TrackSeed.Topic1,  "AI Project",   RegisterTeamStatusEnum.Approved, false, null),
            CreateReg(RegTeam2,  Team2,  SeedConstants.Event2Published, TrackSeed.Track2Web, TrackSeed.Topic2,  "Web App",      RegisterTeamStatusEnum.Approved, false, null),
            CreateReg(RegTeam3,  Team3,  SeedConstants.Event2Published, TrackSeed.Track3Mobile, TrackSeed.Topic3, "Mobile App",  RegisterTeamStatusEnum.Approved, false, null),
            CreateReg(RegTeam4,  Team4,  SeedConstants.Event2Published, TrackSeed.Track4Iot,  TrackSeed.Topic4,  "IoT Solution", RegisterTeamStatusEnum.Approved, false, null),
            CreateReg(RegTeam5,  Team5,  SeedConstants.Event2Published, TrackSeed.Track5Cloud, TrackSeed.Topic5, "Cloud Native",RegisterTeamStatusEnum.Approved, false, null),
            // Event 4 (Published) - mix of statuses
            CreateReg(RegTeam6,  Team6,  SeedConstants.Event4Published, TrackSeed.Track6Security,  TrackSeed.Topic6,  "Security Tool",  RegisterTeamStatusEnum.Pending,  false, null),
            CreateReg(RegTeam7,  Team7,  SeedConstants.Event4Published, TrackSeed.Track7Blockchain, TrackSeed.Topic7,  "Blockchain App", RegisterTeamStatusEnum.Pending,  false, null),
            CreateReg(RegTeam8,  Team8,  SeedConstants.Event4Published, TrackSeed.Track8Game,      TrackSeed.Topic8,  "Game Project",   RegisterTeamStatusEnum.Pending,  false, null),
            CreateReg(RegTeam9,  Team9,  SeedConstants.Event4Published, TrackSeed.Track9Data,      TrackSeed.Topic9,  "Data Pipeline",  RegisterTeamStatusEnum.Rejected, false, "Out of scope"),
            CreateReg(RegTeam10, Team10, SeedConstants.Event4Published, TrackSeed.Track10Devops,   TrackSeed.Topic10, "DevOps Tool",    RegisterTeamStatusEnum.Rejected, false, "Incomplete profile"),
            // Event 7 (Published) - approved & banned
            CreateReg(RegTeam11, Team11, SeedConstants.Event7Published, TrackSeed.Track11Ai,  TrackSeed.Topic11, "AI Security",    RegisterTeamStatusEnum.Approved, false, null),
            CreateReg(RegTeam12, Team12, SeedConstants.Event7Published, TrackSeed.Track12Web, TrackSeed.Topic12, "Web Auction",    RegisterTeamStatusEnum.Approved, false, null),
            CreateReg(RegTeam13, Team13, SeedConstants.Event7Published, TrackSeed.Track13Mobile, TrackSeed.Topic13, "Mobile Game",  RegisterTeamStatusEnum.Approved, true,  null),
            CreateReg(RegTeam14, Team14, SeedConstants.Event7Published, TrackSeed.Track14Iot,  TrackSeed.Topic14, "Smart Farm",    RegisterTeamStatusEnum.Pending,  false, null),
            CreateReg(RegTeam15, Team15, SeedConstants.Event7Published, TrackSeed.Track15Cloud, TrackSeed.Topic15, "Cloud Monitor", RegisterTeamStatusEnum.Approved, false, null),
            // Event 10 (Published) - mixed
            CreateReg(RegTeam16, Team16, SeedConstants.Event10Published, TrackSeed.Track16Security,  TrackSeed.Topic16, "Cyber Security",  RegisterTeamStatusEnum.Approved, false, null),
            CreateReg(RegTeam17, Team17, SeedConstants.Event10Published, TrackSeed.Track17Blockchain, TrackSeed.Topic17, "Crypto Wallet",  RegisterTeamStatusEnum.Approved, false, null),
            CreateReg(RegTeam18, Team18, SeedConstants.Event10Published, TrackSeed.Track18Game,      TrackSeed.Topic18, "VR Game",        RegisterTeamStatusEnum.Pending,  false, null),
            CreateReg(RegTeam19, Team19, SeedConstants.Event10Published, TrackSeed.Track19Data,      TrackSeed.Topic19, "Recommendation", RegisterTeamStatusEnum.Pending,  false, null),
            CreateReg(RegTeam20, Team20, SeedConstants.Event10Published, TrackSeed.Track20Devops,    TrackSeed.Topic20, "K8s Cluster",    RegisterTeamStatusEnum.Approved, false, null),
            // Event 3 (Closed) - historical
            CreateReg(RegTeam21, Team21, SeedConstants.Event3Closed, TrackSeed.Track1Ai,  TrackSeed.Topic1,  "AI Legacy",  RegisterTeamStatusEnum.Approved, false, null),
            CreateReg(RegTeam22, Team22, SeedConstants.Event3Closed, TrackSeed.Track2Web, TrackSeed.Topic2,  "Web Legacy", RegisterTeamStatusEnum.Approved, false, null),
            // Disabled/edge-case registrations
            CreateReg(RegTeam23, Team23, SeedConstants.Event4Published, TrackSeed.Track6Security, TrackSeed.Topic6,  "Disabled reg",RegisterTeamStatusEnum.Approved, false, null),
            CreateReg(RegTeam24, Team24, SeedConstants.Event2Published, TrackSeed.Track1Ai,       TrackSeed.Topic1,  "Inactive team",RegisterTeamStatusEnum.Approved, false, null),
            CreateReg(RegTeam25, Team25, SeedConstants.Event10Published, TrackSeed.Track20Devops, TrackSeed.Topic20, "Extra reg",    RegisterTeamStatusEnum.Banned,  false, "Duplicate registration")
        );
    }

    private static TeamDetails CreateTd(Guid id, Guid teamId, Guid userId, bool isLeader, TeamDetailStatusEnum status = TeamDetailStatusEnum.Active)
        => new() { Id = id, TeamId = teamId, UserId = userId, IsLeader = isLeader, Status = status, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };

    private static RegisterTeams CreateReg(Guid id, Guid teamId, Guid eventId, Guid trackId, Guid topicId, string desc, RegisterTeamStatusEnum status, bool isBanned, string? rejectionReason)
        => new() { Id = id, TeamId = teamId, EventId = eventId, TrackId = trackId, TopicId = topicId, Description = desc, Status = status, IsBanned = isBanned, RejectionReason = rejectionReason, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };
}
