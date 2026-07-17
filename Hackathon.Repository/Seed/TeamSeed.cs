using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class TeamSeed
{
    // Team IDs: 27000000-xxxx
    public static readonly Guid Team1 = Guid.Parse("27000000-0000-0000-0000-000000000001");
    public static readonly Guid Team2 = Guid.Parse("27000000-0000-0000-0000-000000000002");
    public static readonly Guid Team3 = Guid.Parse("27000000-0000-0000-0000-000000000003");
    public static readonly Guid Team4 = Guid.Parse("27000000-0000-0000-0000-000000000004");
    public static readonly Guid Team5 = Guid.Parse("27000000-0000-0000-0000-000000000005");
    public static readonly Guid Team6 = Guid.Parse("27000000-0000-0000-0000-000000000006");
    public static readonly Guid Team7 = Guid.Parse("27000000-0000-0000-0000-000000000007");
    public static readonly Guid Team8 = Guid.Parse("27000000-0000-0000-0000-000000000008");
    public static readonly Guid Team9 = Guid.Parse("27000000-0000-0000-0000-000000000009");
    public static readonly Guid Team10 = Guid.Parse("27000000-0000-0000-0000-000000000010");
    public static readonly Guid Team11 = Guid.Parse("27000000-0000-0000-0000-000000000011");
    public static readonly Guid Team12 = Guid.Parse("27000000-0000-0000-0000-000000000012");
    public static readonly Guid Team13 = Guid.Parse("27000000-0000-0000-0000-000000000013");
    public static readonly Guid Team14 = Guid.Parse("27000000-0000-0000-0000-000000000014");
    public static readonly Guid Team15 = Guid.Parse("27000000-0000-0000-0000-000000000015");
    public static readonly Guid Team16 = Guid.Parse("27000000-0000-0000-0000-000000000016");
    public static readonly Guid Team17 = Guid.Parse("27000000-0000-0000-0000-000000000017");
    public static readonly Guid Team18 = Guid.Parse("27000000-0000-0000-0000-000000000018");
    public static readonly Guid Team19 = Guid.Parse("27000000-0000-0000-0000-000000000019");
    public static readonly Guid Team20 = Guid.Parse("27000000-0000-0000-0000-000000000020");
    public static readonly Guid Team21 = Guid.Parse("27000000-0000-0000-0000-000000000021");
    public static readonly Guid Team22 = Guid.Parse("27000000-0000-0000-0000-000000000022");
    public static readonly Guid Team23 = Guid.Parse("27000000-0000-0000-0000-000000000023");
    public static readonly Guid Team24 = Guid.Parse("27000000-0000-0000-0000-000000000024");
    public static readonly Guid Team25 = Guid.Parse("27000000-0000-0000-0000-000000000025");
    public static readonly Guid Team26 = Guid.Parse("27000000-0000-0000-0000-000000000026");
    public static readonly Guid Team27 = Guid.Parse("27000000-0000-0000-0000-000000000027");
    public static readonly Guid Team28 = Guid.Parse("27000000-0000-0000-0000-000000000028");
    public static readonly Guid Team29 = Guid.Parse("27000000-0000-0000-0000-000000000029");
    public static readonly Guid Team30 = Guid.Parse("27000000-0000-0000-0000-000000000030");

    // RegisterTeam IDs: 29000000-xxxx
    public static readonly Guid RegisterTeam1 = Guid.Parse("29000000-0000-0000-0000-000000000001");
    public static readonly Guid RegisterTeam2 = Guid.Parse("29000000-0000-0000-0000-000000000002");
    public static readonly Guid RegisterTeam3 = Guid.Parse("29000000-0000-0000-0000-000000000003");
    public static readonly Guid RegisterTeam4 = Guid.Parse("29000000-0000-0000-0000-000000000004");
    public static readonly Guid RegisterTeam5 = Guid.Parse("29000000-0000-0000-0000-000000000005");
    public static readonly Guid RegisterTeam6 = Guid.Parse("29000000-0000-0000-0000-000000000006");
    public static readonly Guid RegisterTeam7 = Guid.Parse("29000000-0000-0000-0000-000000000007");
    public static readonly Guid RegisterTeam8 = Guid.Parse("29000000-0000-0000-0000-000000000008");
    public static readonly Guid RegisterTeam9 = Guid.Parse("29000000-0000-0000-0000-000000000009");
    public static readonly Guid RegisterTeam10 = Guid.Parse("29000000-0000-0000-0000-000000000010");
    public static readonly Guid RegisterTeam11 = Guid.Parse("29000000-0000-0000-0000-000000000011");
    public static readonly Guid RegisterTeam12 = Guid.Parse("29000000-0000-0000-0000-000000000012");
    public static readonly Guid RegisterTeam13 = Guid.Parse("29000000-0000-0000-0000-000000000013");
    public static readonly Guid RegisterTeam14 = Guid.Parse("29000000-0000-0000-0000-000000000014");
    public static readonly Guid RegisterTeam15 = Guid.Parse("29000000-0000-0000-0000-000000000015");
    public static readonly Guid RegisterTeam16 = Guid.Parse("29000000-0000-0000-0000-000000000016");
    public static readonly Guid RegisterTeam17 = Guid.Parse("29000000-0000-0000-0000-000000000017");
    public static readonly Guid RegisterTeam18 = Guid.Parse("29000000-0000-0000-0000-000000000018");
    public static readonly Guid RegisterTeam19 = Guid.Parse("29000000-0000-0000-0000-000000000019");
    public static readonly Guid RegisterTeam20 = Guid.Parse("29000000-0000-0000-0000-000000000020");
    public static readonly Guid RegisterTeam21 = Guid.Parse("29000000-0000-0000-0000-000000000021");
    public static readonly Guid RegisterTeam22 = Guid.Parse("29000000-0000-0000-0000-000000000022");
    public static readonly Guid RegisterTeam23 = Guid.Parse("29000000-0000-0000-0000-000000000023");
    public static readonly Guid RegisterTeam24 = Guid.Parse("29000000-0000-0000-0000-000000000024");
    public static readonly Guid RegisterTeam25 = Guid.Parse("29000000-0000-0000-0000-000000000025");
    public static readonly Guid RegisterTeam26 = Guid.Parse("29000000-0000-0000-0000-000000000026");
    public static readonly Guid RegisterTeam27 = Guid.Parse("29000000-0000-0000-0000-000000000027");
    public static readonly Guid RegisterTeam28 = Guid.Parse("29000000-0000-0000-0000-000000000028");
    public static readonly Guid RegisterTeam29 = Guid.Parse("29000000-0000-0000-0000-000000000029");
    public static readonly Guid RegisterTeam30 = Guid.Parse("29000000-0000-0000-0000-000000000030");

    public static void SeedTeams(this ModelBuilder modelBuilder)
    {
        var c = SeedConstants.CreatedAt;

        // ── 30 Teams ────────────────────────────────────────────────
        modelBuilder.Entity<Teams>().HasData(
            // E2 Published teams
            CreateTeam(Team1, "Alpha Coders"),
            CreateTeam(Team2, "Binary Stars"),
            CreateTeam(Team3, "Cyber Wolves"),
            CreateTeam(Team4, "Data Pirates"),
            CreateTeam(Team5, "Error 404"),
            // E3 Closed teams
            CreateTeam(Team6, "Legacy Force"),
            CreateTeam(Team7, "Past Masters"),
            CreateTeam(Team8, "Heritage Dev"),
            CreateTeam(Team9, "Classic Team"),
            // E4 Published teams
            CreateTeam(Team10, "Green Warriors"),
            CreateTeam(Team11, "Finance Gurus"),
            CreateTeam(Team12, "Edu Innovators"),
            CreateTeam(Team13, "Health Pioneers"),
            CreateTeam(Team14, "City Builders"),
            // E6 Closed teams
            CreateTeam(Team15, "Summer Heat"),
            CreateTeam(Team16, "Sunshine Dev"),
            CreateTeam(Team17, "Hot Code"),
            // E7 Published teams
            CreateTeam(Team18, "Cyber Sentinels"),
            CreateTeam(Team19, "Data Wizards"),
            CreateTeam(Team20, "Cloud Nine"),
            CreateTeam(Team21, "Pipeline Pro"),
            // E9 Closed teams
            CreateTeam(Team22, "Winter Soldiers"),
            CreateTeam(Team23, "Frost Byte"),
            // E10 Published teams
            CreateTeam(Team24, "Game Changers"),
            CreateTeam(Team25, "Visionaries"),
            // Extra teams (no register / disbanded)
            CreateTeam(Team26, "Disbanded Team"),
            CreateTeam(Team27, "Never Registered"),
            CreateTeam(Team28, "Inactive Squad"),
            CreateTeam(Team29, "Ghost Team"),
            CreateTeam(Team30, "Zombie Devs")
        );

        // ── 75 Team Details ─────────────────────────────────────────
        modelBuilder.Entity<TeamDetails>().HasData(
            // E2 Teams (15)
            CreateTd(SeedConstants.Td1, SeedConstants.UserStudentLeader1, Team1, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td2, SeedConstants.UserStudentMember1, Team1, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td3, SeedConstants.UserStudentMember2, Team1, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td4, SeedConstants.UserStudentLeader2, Team2, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td5, SeedConstants.UserStudentMember3, Team2, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td6, SeedConstants.UserStudentMember4, Team2, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td7, SeedConstants.UserStudentMember5, Team2, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td8, SeedConstants.UserStudentLeader3, Team3, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td9, SeedConstants.UserStudentMember6, Team3, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td10, SeedConstants.UserStudentMember7, Team3, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td11, SeedConstants.UserStudentLeader4, Team4, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td12, SeedConstants.UserStudentMember8, Team4, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td13, SeedConstants.UserStudentMember9, Team4, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td14, SeedConstants.UserStudentLeader5, Team5, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td15, SeedConstants.UserStudentMember10, Team5, false, TeamDetailStatusEnum.Active),
            // E3 Teams (11)
            CreateTd(SeedConstants.Td16, SeedConstants.UserStudentLeader6, Team6, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td17, SeedConstants.UserStudentMember11, Team6, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td18, SeedConstants.UserStudentMember12, Team6, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td19, SeedConstants.UserStudentLeader7, Team7, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td20, SeedConstants.UserStudentMember13, Team7, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td21, SeedConstants.UserStudentMember14, Team7, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td22, SeedConstants.UserStudentMember15, Team7, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td23, SeedConstants.UserStudentLeader8, Team8, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td24, SeedConstants.UserStudentMember16, Team8, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td25, SeedConstants.UserStudentLeader9, Team9, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td26, SeedConstants.UserStudentMember17, Team9, false, TeamDetailStatusEnum.Active),
            // E4 Teams (15)
            CreateTd(SeedConstants.Td27, SeedConstants.UserStudentLeader10, Team10, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td28, SeedConstants.UserStudentMember18, Team10, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td29, SeedConstants.UserStudentMember19, Team10, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td30, SeedConstants.UserStudentLeader1, Team11, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td31, SeedConstants.UserStudentMember20, Team11, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td32, SeedConstants.UserStudentMember1, Team11, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td33, SeedConstants.UserStudentMember2, Team11, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td34, SeedConstants.UserStudentLeader2, Team12, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td35, SeedConstants.UserStudentMember3, Team12, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td36, SeedConstants.UserStudentLeader3, Team13, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td37, SeedConstants.UserStudentMember4, Team13, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td38, SeedConstants.UserStudentMember5, Team13, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td39, SeedConstants.UserStudentLeader4, Team14, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td40, SeedConstants.UserStudentMember6, Team14, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td41, SeedConstants.UserStudentMember7, Team14, false, TeamDetailStatusEnum.Active),
            // E6 Teams (7)
            CreateTd(SeedConstants.Td42, SeedConstants.UserStudentLeader5, Team15, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td43, SeedConstants.UserStudentMember8, Team15, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td44, SeedConstants.UserStudentLeader6, Team16, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td45, SeedConstants.UserStudentMember9, Team16, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td46, SeedConstants.UserStudentMember10, Team16, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td47, SeedConstants.UserStudentLeader7, Team17, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td48, SeedConstants.UserStudentMember11, Team17, false, TeamDetailStatusEnum.Active),
            // E7 Teams (11)
            CreateTd(SeedConstants.Td49, SeedConstants.UserStudentLeader8, Team18, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td50, SeedConstants.UserStudentMember12, Team18, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td51, SeedConstants.UserStudentMember13, Team18, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td52, SeedConstants.UserStudentMember14, Team18, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td53, SeedConstants.UserStudentLeader9, Team19, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td54, SeedConstants.UserStudentMember15, Team19, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td55, SeedConstants.UserStudentMember16, Team19, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td56, SeedConstants.UserStudentLeader10, Team20, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td57, SeedConstants.UserStudentMember17, Team20, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td58, SeedConstants.UserStudentLeader1, Team21, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td59, SeedConstants.UserStudentMember18, Team21, false, TeamDetailStatusEnum.Active),
            // E9 Teams (5)
            CreateTd(SeedConstants.Td60, SeedConstants.UserStudentLeader2, Team22, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td61, SeedConstants.UserStudentMember19, Team22, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td62, SeedConstants.UserStudentMember20, Team22, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td63, SeedConstants.UserStudentLeader3, Team23, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td64, SeedConstants.UserStudentMember1, Team23, false, TeamDetailStatusEnum.Active),
            // E10 Teams (6)
            CreateTd(SeedConstants.Td65, SeedConstants.UserStudentLeader4, Team24, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td66, SeedConstants.UserStudentMember2, Team24, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td67, SeedConstants.UserStudentMember3, Team24, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td68, SeedConstants.UserStudentMember4, Team24, false, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td69, SeedConstants.UserStudentLeader5, Team25, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td70, SeedConstants.UserStudentMember5, Team25, false, TeamDetailStatusEnum.Active),
            // Extra teams (5)
            CreateTd(SeedConstants.Td71, SeedConstants.UserStudentLeader6, Team26, true, TeamDetailStatusEnum.Inactive),
            CreateTd(SeedConstants.Td72, SeedConstants.UserStudentMember6, Team26, false, TeamDetailStatusEnum.Inactive),
            CreateTd(SeedConstants.Td73, SeedConstants.UserStudentLeader7, Team27, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td74, SeedConstants.UserStudentLeader8, Team28, true, TeamDetailStatusEnum.Inactive),
            CreateTd(SeedConstants.Td75, SeedConstants.UserStudentLeader9, Team29, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td76, SeedConstants.UserStudentLeader10, Team30, true, TeamDetailStatusEnum.Active),
            CreateTd(SeedConstants.Td77, SeedConstants.UserStudentMember7, Team30, false, TeamDetailStatusEnum.Active)
        );

        // ── 30 RegisterTeams ─────────────────────────────────────────
        modelBuilder.Entity<RegisterTeams>().HasData(
            // E2 — 5 registrations
            CreateReg(RegisterTeam1, Team1, SeedConstants.Event2Published, TrackSeed.Track1, TrackSeed.Topic1, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam2, Team2, SeedConstants.Event2Published, TrackSeed.Track2, TrackSeed.Topic2, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam3, Team3, SeedConstants.Event2Published, TrackSeed.Track3, TrackSeed.Topic3, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam4, Team4, SeedConstants.Event2Published, TrackSeed.Track4, TrackSeed.Topic4, RegisterTeamStatusEnum.Pending),
            CreateReg(RegisterTeam5, Team5, SeedConstants.Event2Published, TrackSeed.Track5, TrackSeed.Topic5, RegisterTeamStatusEnum.Rejected, "Incomplete proposal"),
            // E3 — 4 registrations
            CreateReg(RegisterTeam6, Team6, SeedConstants.Event3Closed, TrackSeed.Track19, TrackSeed.Topic19, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam7, Team7, SeedConstants.Event3Closed, TrackSeed.Track20, TrackSeed.Topic20, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam8, Team8, SeedConstants.Event3Closed, TrackSeed.Track21, TrackSeed.Topic21, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam9, Team9, SeedConstants.Event3Closed, TrackSeed.Track19, TrackSeed.Topic19, RegisterTeamStatusEnum.Rejected, "Duplicate submission"),
            // E4 — 5 registrations
            CreateReg(RegisterTeam10, Team10, SeedConstants.Event4Published, TrackSeed.Track6, TrackSeed.Topic6, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam11, Team11, SeedConstants.Event4Published, TrackSeed.Track7, TrackSeed.Topic7, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam12, Team12, SeedConstants.Event4Published, TrackSeed.Track8, TrackSeed.Topic8, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam13, Team13, SeedConstants.Event4Published, TrackSeed.Track9, TrackSeed.Topic9, RegisterTeamStatusEnum.Pending),
            CreateReg(RegisterTeam14, Team14, SeedConstants.Event4Published, TrackSeed.Track10, TrackSeed.Topic10, RegisterTeamStatusEnum.Rejected, "Team capacity exceeded"),
            // E6 — 3 registrations
            CreateReg(RegisterTeam15, Team15, SeedConstants.Event6Closed, TrackSeed.Track22, TrackSeed.Topic22, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam16, Team16, SeedConstants.Event6Closed, TrackSeed.Track23, TrackSeed.Topic23, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam17, Team17, SeedConstants.Event6Closed, TrackSeed.Track22, TrackSeed.Topic22, RegisterTeamStatusEnum.Approved),
            // E7 — 4 registrations
            CreateReg(RegisterTeam18, Team18, SeedConstants.Event7Published, TrackSeed.Track11, TrackSeed.Topic11, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam19, Team19, SeedConstants.Event7Published, TrackSeed.Track12, TrackSeed.Topic12, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam20, Team20, SeedConstants.Event7Published, TrackSeed.Track13, TrackSeed.Topic13, RegisterTeamStatusEnum.Pending),
            CreateReg(RegisterTeam21, Team21, SeedConstants.Event7Published, TrackSeed.Track14, TrackSeed.Topic14, RegisterTeamStatusEnum.Rejected, "Late registration"),
            // E9 — 2 registrations
            CreateReg(RegisterTeam22, Team22, SeedConstants.Event9Closed, TrackSeed.Track24, TrackSeed.Topic24, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam23, Team23, SeedConstants.Event9Closed, TrackSeed.Track25, TrackSeed.Topic25, RegisterTeamStatusEnum.Approved),
            // E10 — 2 registrations
            CreateReg(RegisterTeam24, Team24, SeedConstants.Event10Published, TrackSeed.Track15, TrackSeed.Topic15, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam25, Team25, SeedConstants.Event10Published, TrackSeed.Track16, TrackSeed.Topic16, RegisterTeamStatusEnum.Approved),
            // Banned registration
            CreateReg(RegisterTeam26, Team9, SeedConstants.Event2Published, TrackSeed.Track1, TrackSeed.Topic1, RegisterTeamStatusEnum.Banned),
            // Extra registrations for edge cases
            CreateReg(RegisterTeam27, Team26, SeedConstants.Event4Published, TrackSeed.Track6, TrackSeed.Topic6, RegisterTeamStatusEnum.Rejected, "Disbanded team"),
            CreateReg(RegisterTeam28, Team28, SeedConstants.Event7Published, TrackSeed.Track11, TrackSeed.Topic11, RegisterTeamStatusEnum.Pending),
            CreateReg(RegisterTeam29, Team29, SeedConstants.Event10Published, TrackSeed.Track17, TrackSeed.Topic17, RegisterTeamStatusEnum.Approved),
            CreateReg(RegisterTeam30, Team30, SeedConstants.Event2Published, TrackSeed.Track3, TrackSeed.Topic3, RegisterTeamStatusEnum.Pending)
        );
    }

    private static Teams CreateTeam(Guid id, string name) => new()
    {
        Id = id, Name = name, CanEdit = true,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };

    private static TeamDetails CreateTd(Guid id, Guid userId, Guid teamId, bool isLeader, TeamDetailStatusEnum status)
        => new()
        {
            Id = id, UserId = userId, TeamId = teamId, IsLeader = isLeader, Status = status,
            IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
        };

    private static RegisterTeams CreateReg(Guid id, Guid teamId, Guid eventId, Guid trackId, Guid topicId, RegisterTeamStatusEnum status, string? rejectionReason = null) => new()
    {
        Id = id, TeamId = teamId, EventId = eventId, TrackId = trackId, TopicId = topicId,
        Description = $"Registration for event {eventId}", RejectionReason = rejectionReason,
        Status = status, IsBanned = status == RegisterTeamStatusEnum.Banned,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };
}
