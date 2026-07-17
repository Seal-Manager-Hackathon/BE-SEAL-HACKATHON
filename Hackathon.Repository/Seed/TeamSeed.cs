using System;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class TeamSeed
{
    // Team IDs
    public static readonly Guid Team1 = Guid.Parse("30000000-0000-0000-0000-000000000001");
    public static readonly Guid Team2 = Guid.Parse("30000000-0000-0000-0000-000000000002");
    public static readonly Guid Team3 = Guid.Parse("30000000-0000-0000-0000-000000000003");
    public static readonly Guid Team4 = Guid.Parse("30000000-0000-0000-0000-000000000004");
    public static readonly Guid Team5 = Guid.Parse("30000000-0000-0000-0000-000000000005");
    public static readonly Guid Team6 = Guid.Parse("30000000-0000-0000-0000-000000000006");
    public static readonly Guid Team7 = Guid.Parse("30000000-0000-0000-0000-000000000007");
    public static readonly Guid Team8 = Guid.Parse("30000000-0000-0000-0000-000000000008");
    public static readonly Guid Team9 = Guid.Parse("30000000-0000-0000-0000-000000000009");
    public static readonly Guid Team10 = Guid.Parse("30000000-0000-0000-0000-000000000010");
    public static readonly Guid Team11 = Guid.Parse("30000000-0000-0000-0000-000000000011");
    public static readonly Guid Team12 = Guid.Parse("30000000-0000-0000-0000-000000000012");
    public static readonly Guid Team13 = Guid.Parse("30000000-0000-0000-0000-000000000013");
    public static readonly Guid Team14 = Guid.Parse("30000000-0000-0000-0000-000000000014");
    public static readonly Guid Team15 = Guid.Parse("30000000-0000-0000-0000-000000000015");

    // Register Team IDs
    public static readonly Guid RegTeam1 = Guid.Parse("31000000-0000-0000-0000-000000000001");
    public static readonly Guid RegTeam2 = Guid.Parse("31000000-0000-0000-0000-000000000002");
    public static readonly Guid RegTeam3 = Guid.Parse("31000000-0000-0000-0000-000000000003");
    public static readonly Guid RegTeam4 = Guid.Parse("31000000-0000-0000-0000-000000000004");
    public static readonly Guid RegTeam5 = Guid.Parse("31000000-0000-0000-0000-000000000005");
    public static readonly Guid RegTeam6 = Guid.Parse("31000000-0000-0000-0000-000000000006");
    public static readonly Guid RegTeam7 = Guid.Parse("31000000-0000-0000-0000-000000000007");
    public static readonly Guid RegTeam8 = Guid.Parse("31000000-0000-0000-0000-000000000008");
    public static readonly Guid RegTeam9 = Guid.Parse("31000000-0000-0000-0000-000000000009");
    public static readonly Guid RegTeam10 = Guid.Parse("31000000-0000-0000-0000-000000000010");
    public static readonly Guid RegTeam11 = Guid.Parse("31000000-0000-0000-0000-000000000011");
    public static readonly Guid RegTeam12 = Guid.Parse("31000000-0000-0000-0000-000000000012");
    public static readonly Guid RegTeam13 = Guid.Parse("31000000-0000-0000-0000-000000000013");
    public static readonly Guid RegTeam14 = Guid.Parse("31000000-0000-0000-0000-000000000014");
    public static readonly Guid RegTeam15 = Guid.Parse("31000000-0000-0000-0000-000000000015");

    public static void SeedTeams(this ModelBuilder modelBuilder)
    {
        // 15 Teams
        modelBuilder.Entity<Teams>().HasData(
            new Teams { Id = Team1, Name = "Alpha Tech", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team2, Name = "Beta Coders", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team3, Name = "Gamma Devs", CanEdit = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team4, Name = "Delta Force", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team5, Name = "Epsilon AI", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team6, Name = "Zeta Hackers", CanEdit = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team7, Name = "Eta Innovators", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team8, Name = "Theta System", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team9, Name = "Iota Solutions", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team10, Name = "Kappa Web", CanEdit = true, IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }, // disabled team
            new Teams { Id = Team11, Name = "Lambda Cyber", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team12, Name = "Mu Mobile", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team13, Name = "Nu Blockchain", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team14, Name = "Xi IoT", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Teams { Id = Team15, Name = "Omicron Cloud", CanEdit = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );

        // 15 TeamDetails (connecting users to teams)
        modelBuilder.Entity<TeamDetails>().HasData(
            new TeamDetails { Id = Guid.Parse("30100000-0000-0000-0000-000000000001"), TeamId = Team1, UserId = SeedConstants.UserStudentLeaderActive1, IsLeader = true, Status = TeamDetailStatusEnum.Active, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new TeamDetails { Id = Guid.Parse("30100000-0000-0000-0000-000000000002"), TeamId = Team1, UserId = SeedConstants.UserStudentMemberActive1, IsLeader = false, Status = TeamDetailStatusEnum.Active, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new TeamDetails { Id = Guid.Parse("30100000-0000-0000-0000-000000000003"), TeamId = Team1, UserId = SeedConstants.UserStudentMemberInactive1, IsLeader = false, Status = TeamDetailStatusEnum.Inactive, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new TeamDetails { Id = Guid.Parse("30100000-0000-0000-0000-000000000004"), TeamId = Team2, UserId = SeedConstants.UserStudentLeaderActive2, IsLeader = true, Status = TeamDetailStatusEnum.Active, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new TeamDetails { Id = Guid.Parse("30100000-0000-0000-0000-000000000005"), TeamId = Team2, UserId = SeedConstants.UserStudentMemberActive2, IsLeader = false, Status = TeamDetailStatusEnum.Active, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new TeamDetails { Id = Guid.Parse("30100000-0000-0000-0000-000000000006"), TeamId = Team3, UserId = SeedConstants.UserStudentLeaderActive3, IsLeader = true, Status = TeamDetailStatusEnum.Active, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new TeamDetails { Id = Guid.Parse("30100000-0000-0000-0000-000000000007"), TeamId = Team3, UserId = SeedConstants.UserStudentMemberBanned3, IsLeader = false, Status = TeamDetailStatusEnum.Active, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new TeamDetails { Id = Guid.Parse("30100000-0000-0000-0000-000000000008"), TeamId = Team4, UserId = SeedConstants.UserStudentLeaderActive1, IsLeader = true, Status = TeamDetailStatusEnum.Active, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new TeamDetails { Id = Guid.Parse("30100000-0000-0000-0000-000000000009"), TeamId = Team5, UserId = SeedConstants.UserStudentLeaderActive2, IsLeader = true, Status = TeamDetailStatusEnum.Active, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new TeamDetails { Id = Guid.Parse("30100000-0000-0000-0000-000000000010"), TeamId = Team6, UserId = SeedConstants.UserStudentLeaderActive3, IsLeader = true, Status = TeamDetailStatusEnum.Active, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new TeamDetails { Id = Guid.Parse("30100000-0000-0000-0000-000000000011"), TeamId = Team7, UserId = SeedConstants.UserStudentLeaderActive1, IsLeader = true, Status = TeamDetailStatusEnum.Active, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new TeamDetails { Id = Guid.Parse("30100000-0000-0000-0000-000000000012"), TeamId = Team8, UserId = SeedConstants.UserStudentLeaderActive2, IsLeader = true, Status = TeamDetailStatusEnum.Active, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new TeamDetails { Id = Guid.Parse("30100000-0000-0000-0000-000000000013"), TeamId = Team9, UserId = SeedConstants.UserStudentLeaderActive3, IsLeader = true, Status = TeamDetailStatusEnum.Active, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new TeamDetails { Id = Guid.Parse("30100000-0000-0000-0000-000000000014"), TeamId = Team11, UserId = SeedConstants.UserStudentLeaderActive1, IsLeader = true, Status = TeamDetailStatusEnum.Active, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new TeamDetails { Id = Guid.Parse("30100000-0000-0000-0000-000000000015"), TeamId = Team12, UserId = SeedConstants.UserStudentLeaderActive2, IsLeader = true, Status = TeamDetailStatusEnum.Active, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );

        // 15 RegisterTeams (all statuses and options)
        modelBuilder.Entity<RegisterTeams>().HasData(
            new RegisterTeams { Id = RegTeam1, TeamId = Team1, EventId = SeedConstants.Event2Published, TrackId = TrackSeed.Track1Ai, TopicId = TrackSeed.Topic1, Description = "AI Project Registration", Status = RegisterTeamStatusEnum.Approved, IsBanned = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RegisterTeams { Id = RegTeam2, TeamId = Team2, EventId = SeedConstants.Event2Published, TrackId = TrackSeed.Track2Web, TopicId = TrackSeed.Topic2, Description = "Web Project Registration", Status = RegisterTeamStatusEnum.Approved, IsBanned = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RegisterTeams { Id = RegTeam3, TeamId = Team3, EventId = SeedConstants.Event2Published, TrackId = TrackSeed.Track3Mobile, TopicId = TrackSeed.Topic3, Description = "Mobile Project Registration", Status = RegisterTeamStatusEnum.Approved, IsBanned = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RegisterTeams { Id = RegTeam4, TeamId = Team4, EventId = SeedConstants.Event2Published, TrackId = TrackSeed.Track4Iot, TopicId = TrackSeed.Topic4, Description = "IoT Project Registration", Status = RegisterTeamStatusEnum.Approved, IsBanned = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RegisterTeams { Id = RegTeam5, TeamId = Team5, EventId = SeedConstants.Event2Published, TrackId = TrackSeed.Track5Cloud, TopicId = TrackSeed.Topic5, Description = "Cloud Project Registration", Status = RegisterTeamStatusEnum.Approved, IsBanned = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RegisterTeams { Id = RegTeam6, TeamId = Team6, EventId = SeedConstants.Event4Published, TrackId = TrackSeed.Track6Security, TopicId = TrackSeed.Topic6, Description = "Security Project Registration", Status = RegisterTeamStatusEnum.Pending, IsBanned = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RegisterTeams { Id = RegTeam7, TeamId = Team7, EventId = SeedConstants.Event4Published, TrackId = TrackSeed.Track7Blockchain, TopicId = TrackSeed.Topic7, Description = "Blockchain Project Registration", Status = RegisterTeamStatusEnum.Pending, IsBanned = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RegisterTeams { Id = RegTeam8, TeamId = Team8, EventId = SeedConstants.Event4Published, TrackId = TrackSeed.Track8Game, TopicId = TrackSeed.Topic8, Description = "Game Project Registration", Status = RegisterTeamStatusEnum.Pending, IsBanned = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RegisterTeams { Id = RegTeam9, TeamId = Team9, EventId = SeedConstants.Event4Published, TrackId = TrackSeed.Track9Data, TopicId = TrackSeed.Topic9, Description = "Data Project Registration", Status = RegisterTeamStatusEnum.Rejected, RejectionReason = "Out of scope", IsBanned = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RegisterTeams { Id = RegTeam10, TeamId = Team10, EventId = SeedConstants.Event4Published, TrackId = TrackSeed.Track10Devops, TopicId = TrackSeed.Topic10, Description = "DevOps Project Registration", Status = RegisterTeamStatusEnum.Rejected, RejectionReason = "Incomplete profile", IsBanned = false, IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RegisterTeams { Id = RegTeam11, TeamId = Team11, EventId = SeedConstants.Event7Published, TrackId = TrackSeed.Track6Security, TopicId = TrackSeed.Topic6, Description = "Approved security registration", Status = RegisterTeamStatusEnum.Approved, IsBanned = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RegisterTeams { Id = RegTeam12, TeamId = Team12, EventId = SeedConstants.Event7Published, TrackId = TrackSeed.Track7Blockchain, TopicId = TrackSeed.Topic7, Description = "Approved blockchain registration", Status = RegisterTeamStatusEnum.Approved, IsBanned = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RegisterTeams { Id = RegTeam13, TeamId = Team13, EventId = SeedConstants.Event7Published, TrackId = TrackSeed.Track8Game, TopicId = TrackSeed.Topic8, Description = "Banned team registration", Status = RegisterTeamStatusEnum.Approved, IsBanned = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RegisterTeams { Id = RegTeam14, TeamId = Team14, EventId = SeedConstants.Event10Published, TrackId = TrackSeed.Track9Data, TopicId = TrackSeed.Topic9, Description = "Pending winter registration", Status = RegisterTeamStatusEnum.Pending, IsBanned = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new RegisterTeams { Id = RegTeam15, TeamId = Team15, EventId = SeedConstants.Event10Published, TrackId = TrackSeed.Track10Devops, TopicId = TrackSeed.Topic10, Description = "Approved winter registration", Status = RegisterTeamStatusEnum.Approved, IsBanned = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );
    }
}
