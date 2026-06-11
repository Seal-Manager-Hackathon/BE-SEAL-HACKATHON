using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hackathon.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RegisterTeamsUseEventId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterTeams_Topics_TopicId",
                table: "RegisterTeams");

            migrationBuilder.RenameColumn(
                name: "TopicId",
                table: "RegisterTeams",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_RegisterTeams_TopicId",
                table: "RegisterTeams",
                newName: "IX_RegisterTeams_EventId");

            migrationBuilder.Sql("""
                UPDATE "RegisterTeams" AS rt
                SET "EventId" = tr."EventId"
                FROM "Topics" AS tp
                INNER JOIN "Tracks" AS tr ON tr."Id" = tp."TrackId"
                WHERE rt."EventId" = tp."Id";
            """);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Users",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "RegisterTeams",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "EventRoles",
                columns: new[] { "Id", "CreatedAt", "IsDisable", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Mentor", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Judge", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CreatedAt", "Description", "EndTime", "IsDisable", "LimitTeam", "MaxMember", "MinMember", "Name", "NumberRound", "RegisterLimitTime", "Season", "StartTime", "Status", "UpdatedAt" },
                values: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed event for hackathon demo data", new DateTimeOffset(new DateTime(2026, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 20, 4, 2, "SEAL Hackathon 2026", 2, new DateTimeOffset(new DateTime(2026, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "2026", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Published", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "IsDisable", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Admin", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Staff", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Student", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Lecturer", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CanEdit", "CreatedAt", "IsDisable", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000001"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Seed Innovators", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("30000000-0000-0000-0000-000000000002"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Green Coders", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AvatarUrl", "BanReason", "BannedAt", "Bio", "College", "CreatedAt", "DateOfBirth", "Email", "FirstName", "HashPassword", "ImgUrl", "IsDisable", "IsVerified", "LastName", "LinkUrl", "PhoneNumber", "Status", "StudentId", "UpdatedAt", "VerifyEmailAt" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "admin@seed.local", "Admin", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Seed", "https://seed.local/users", "0900000000", "Active", "System Administrator", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "staff@seed.local", "Staff", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Seed", "https://seed.local/users", "0900000000", "Active", "Event Staff", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "mentor@seed.local", "Mentor", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Lecturer", "https://seed.local/users", "0900000000", "Active", "Seed Mentor", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "judge@seed.local", "Judge", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Lecturer", "https://seed.local/users", "0900000000", "Active", "Seed Judge", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "leader@seed.local", "Student", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Leader", "https://seed.local/users", "0900000000", "Active", "SEAL001", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "member@seed.local", "Student", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Member", "https://seed.local/users", "0900000000", "Active", "SEAL002", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000007"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "green.leader@seed.local", "Green", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Leader", "https://seed.local/users", "0900000000", "Active", "SEAL003", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "AssignEvents",
                columns: new[] { "Id", "CreatedAt", "EventId", "EventRoleId", "IsDisable", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000003") },
                    { new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("66666666-6666-6666-6666-666666666666"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000004") }
                });

            migrationBuilder.InsertData(
                table: "Awards",
                columns: new[] { "Id", "CreatedAt", "Description", "EventId", "IsDisable", "LevelAward", "Name", "NumberOfAward", "Prize", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("26000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "First place award", new Guid("20000000-0000-0000-0000-000000000001"), false, "First", "Champion", 1, 1000m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("26000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Second place award", new Guid("20000000-0000-0000-0000-000000000001"), false, "Second", "Runner Up", 1, 500m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "EmailVerifications",
                columns: new[] { "Id", "CreatedAt", "ExpiredAt", "IsDisable", "Status", "TokenHash", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("14000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Verified", "seed-email-verification-token-hash", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000005") });

            migrationBuilder.InsertData(
                table: "Invitations",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDisable", "LimitTime", "Status", "TeamId", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("70000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed invitation", false, new DateTimeOffset(new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Pending", new Guid("30000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000006") });

            migrationBuilder.InsertData(
                table: "LeaderBoards",
                columns: new[] { "Id", "CreatedAt", "EventId", "IsDisable", "UpdatedAt", "Year" },
                values: new object[] { new Guid("60000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000001"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2026 });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDisable", "Status", "TeamId", "Title", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("71000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Your team registration has been approved", false, "Unread", new Guid("30000000-0000-0000-0000-000000000001"), "Registration approved", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000005") });

            migrationBuilder.InsertData(
                table: "RefreshTokens",
                columns: new[] { "Id", "CreatedAt", "DeviceLabel", "ExpiredAt", "IpAddress", "IsDisable", "RefreshTokenHash", "RevokedAt", "UpdatedAt", "UserAgent", "UserId" },
                values: new object[] { new Guid("12000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Device", new DateTimeOffset(new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "127.0.0.1", false, "seed-refresh-token-hash", null, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Agent", new Guid("10000000-0000-0000-0000-000000000001") });

            migrationBuilder.InsertData(
                table: "RegisterTeams",
                columns: new[] { "Id", "CreatedAt", "Description", "EventId", "IsBanned", "IsDisable", "RejectionReason", "Status", "TeamId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("31000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Innovators registration", new Guid("20000000-0000-0000-0000-000000000001"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("31000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Green Coders registration", new Guid("20000000-0000-0000-0000-000000000001"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "ResetPasswords",
                columns: new[] { "Id", "CreatedAt", "ExpiresAt", "IsDisable", "IsUsed", "TokenHash", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("13000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "seed-reset-password-token-hash", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000006") });

            migrationBuilder.InsertData(
                table: "Rounds",
                columns: new[] { "Id", "CreatedAt", "Description", "EndSubmission", "EndTime", "EventId", "IsDisable", "LimitTeam", "Name", "StartSubmission", "StartTime", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("21000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Submit and validate the idea", new DateTimeOffset(new DateTime(2026, 6, 21, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000001"), false, 20, "Idea Submission", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("21000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Present the final product", new DateTimeOffset(new DateTime(2026, 6, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000001"), false, 10, "Final Demo", new DateTimeOffset(new DateTime(2026, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "TeamDetails",
                columns: new[] { "Id", "CreatedAt", "IsDisable", "IsLeader", "Status", "TeamId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("30100000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000005") },
                    { new Guid("30100000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Active", new Guid("30000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000006") },
                    { new Guid("30100000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000007") }
                });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "CreatedAt", "Description", "EventId", "IsDisable", "MaxTeam", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("24000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "AI solutions for learning", new Guid("20000000-0000-0000-0000-000000000001"), false, 10, "AI for Education", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("24000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Sustainable technology solutions", new Guid("20000000-0000-0000-0000-000000000001"), false, 10, "Green Technology", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "IsDisable", "RoleId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("11000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000001") },
                    { new Guid("11000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("22222222-2222-2222-2222-222222222222"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000002") },
                    { new Guid("11000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("44444444-4444-4444-4444-444444444444"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000003") },
                    { new Guid("11000000-0000-0000-0000-000000000004"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("44444444-4444-4444-4444-444444444444"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000004") },
                    { new Guid("11000000-0000-0000-0000-000000000005"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("33333333-3333-3333-3333-333333333333"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000005") },
                    { new Guid("11000000-0000-0000-0000-000000000006"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("33333333-3333-3333-3333-333333333333"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000006") },
                    { new Guid("11000000-0000-0000-0000-000000000007"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("33333333-3333-3333-3333-333333333333"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000007") }
                });

            migrationBuilder.InsertData(
                table: "AssignTracks",
                columns: new[] { "Id", "AssignEventId", "CreatedAt", "IsDisable", "TrackId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("41000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("24000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("41000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("24000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("41000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("24000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "CriteriaTemplates",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDisable", "RoundId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("22000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Criteria for idea validation", false, new Guid("21000000-0000-0000-0000-000000000001"), "Idea Evaluation Template", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("22000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Criteria for final demo", false, new Guid("21000000-0000-0000-0000-000000000002"), "Final Demo Evaluation Template", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "LeaderBoardDetails",
                columns: new[] { "Id", "CreatedAt", "IsDisable", "LeaderBoardId", "LevelAward", "Score", "TeamId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("61000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("60000000-0000-0000-0000-000000000001"), "First", 90m, new Guid("30000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("61000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("60000000-0000-0000-0000-000000000001"), "Second", 82m, new Guid("30000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "RoundDetails",
                columns: new[] { "Id", "CreatedAt", "IsDisable", "RegisterTeamId", "RoundId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("32000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000001"), new Guid("21000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000001"), new Guid("21000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000002"), new Guid("21000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000004"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000002"), new Guid("21000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDisable", "Title", "TrackId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("25000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Learning assistant powered by AI", false, "Personalized Learning Assistant", new Guid("24000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("25000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Track and reduce carbon footprint", false, "Carbon Footprint Tracker", new Guid("24000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "CriteriaItems",
                columns: new[] { "Id", "CreatedAt", "CriteriaTemplateId", "Description", "IsDisable", "Name", "Score", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("23000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22000000-0000-0000-0000-000000000001"), "Novelty of the idea", false, "Innovation", 40m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("23000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22000000-0000-0000-0000-000000000001"), "Feasibility of execution", false, "Feasibility", 60m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("23000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22000000-0000-0000-0000-000000000002"), "Quality of technical implementation", false, "Technical Execution", 50m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("23000000-0000-0000-0000-000000000004"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22000000-0000-0000-0000-000000000002"), "Clarity of presentation", false, "Presentation", 50m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "MentorNotifications",
                columns: new[] { "Id", "AssignTrackId", "CreatedAt", "Description", "IsDisable", "Title", "UpdatedAt" },
                values: new object[] { new Guid("72000000-0000-0000-0000-000000000001"), new Guid("41000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "A new team joined your track", false, "New team registered", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDisable", "RoundDetailId", "Status", "SubmittedAt", "UpdatedAt", "Url" },
                values: new object[,]
                {
                    { new Guid("33000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed submission", false, new Guid("32000000-0000-0000-0000-000000000001"), "Submitted", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://seed.local/submissions/seed-innovators-idea" },
                    { new Guid("33000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed submission", false, new Guid("32000000-0000-0000-0000-000000000002"), "Submitted", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://seed.local/submissions/seed-innovators-final" },
                    { new Guid("33000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed submission", false, new Guid("32000000-0000-0000-0000-000000000003"), "Submitted", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://seed.local/submissions/green-coders-idea" },
                    { new Guid("33000000-0000-0000-0000-000000000004"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed submission", false, new Guid("32000000-0000-0000-0000-000000000004"), "Submitted", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://seed.local/submissions/green-coders-final" }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "AssignEventId", "CreatedAt", "Description", "FileUrl", "ImgUrl", "IsDisable", "Reason", "Status", "SubmissionId", "Title", "TypeReport", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("73000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed report for final submission", "https://seed.local/reports/file.pdf", "https://seed.local/reports/image.png", false, "Seed review reason", "Open", new Guid("33000000-0000-0000-0000-000000000004"), "Seed submission report", "Submission", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000004") });

            migrationBuilder.InsertData(
                table: "Scores",
                columns: new[] { "Id", "AssignTrackId", "CreatedAt", "IsDisable", "IsMock", "IsRetake", "SubmissionId", "TotalScore", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), new Guid("41000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, false, new Guid("33000000-0000-0000-0000-000000000001"), 85m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("50000000-0000-0000-0000-000000000002"), new Guid("41000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, false, new Guid("33000000-0000-0000-0000-000000000002"), 90m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new Guid("41000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, false, new Guid("33000000-0000-0000-0000-000000000003"), 78m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("41000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, false, new Guid("33000000-0000-0000-0000-000000000004"), 82m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "ScoreItems",
                columns: new[] { "Id", "AssignTrackId", "Comment", "CreatedAt", "CriteriaItemId", "IsDisable", "Score", "ScoreId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("51000000-0000-0000-0000-000000000001"), new Guid("41000000-0000-0000-0000-000000000002"), "Strong innovation", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("23000000-0000-0000-0000-000000000001"), false, 35m, new Guid("50000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("51000000-0000-0000-0000-000000000002"), new Guid("41000000-0000-0000-0000-000000000002"), "Feasible plan", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("23000000-0000-0000-0000-000000000002"), false, 50m, new Guid("50000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("51000000-0000-0000-0000-000000000003"), new Guid("41000000-0000-0000-0000-000000000002"), "Solid implementation", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("23000000-0000-0000-0000-000000000003"), false, 45m, new Guid("50000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("51000000-0000-0000-0000-000000000004"), new Guid("41000000-0000-0000-0000-000000000002"), "Clear presentation", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("23000000-0000-0000-0000-000000000004"), false, 45m, new Guid("50000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("51000000-0000-0000-0000-000000000005"), new Guid("41000000-0000-0000-0000-000000000003"), "Useful concept", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("23000000-0000-0000-0000-000000000001"), false, 32m, new Guid("50000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("51000000-0000-0000-0000-000000000006"), new Guid("41000000-0000-0000-0000-000000000003"), "Good execution path", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("23000000-0000-0000-0000-000000000002"), false, 46m, new Guid("50000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("51000000-0000-0000-0000-000000000007"), new Guid("41000000-0000-0000-0000-000000000003"), "Working prototype", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("23000000-0000-0000-0000-000000000003"), false, 40m, new Guid("50000000-0000-0000-0000-000000000004"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("51000000-0000-0000-0000-000000000008"), new Guid("41000000-0000-0000-0000-000000000003"), "Good demo", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("23000000-0000-0000-0000-000000000004"), false, 42m, new Guid("50000000-0000-0000-0000-000000000004"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterTeams_Events_EventId",
                table: "RegisterTeams",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterTeams_Events_EventId",
                table: "RegisterTeams");

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "EmailVerifications",
                keyColumn: "Id",
                keyValue: new Guid("14000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Invitations",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "MentorNotifications",
                keyColumn: "Id",
                keyValue: new Guid("72000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: new Guid("71000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "RefreshTokens",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("73000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ResetPasswords",
                keyColumn: "Id",
                keyValue: new Guid("13000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ScoreItems",
                keyColumn: "Id",
                keyValue: new Guid("51000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ScoreItems",
                keyColumn: "Id",
                keyValue: new Guid("51000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "ScoreItems",
                keyColumn: "Id",
                keyValue: new Guid("51000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "ScoreItems",
                keyColumn: "Id",
                keyValue: new Guid("51000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "ScoreItems",
                keyColumn: "Id",
                keyValue: new Guid("51000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "ScoreItems",
                keyColumn: "Id",
                keyValue: new Guid("51000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "ScoreItems",
                keyColumn: "Id",
                keyValue: new Guid("51000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "ScoreItems",
                keyColumn: "Id",
                keyValue: new Guid("51000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("25000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("25000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("11000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("11000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("11000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("11000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("11000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("11000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("11000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "AssignTracks",
                keyColumn: "Id",
                keyValue: new Guid("41000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "CriteriaItems",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "CriteriaItems",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "CriteriaItems",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "CriteriaItems",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "AssignEvents",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "AssignTracks",
                keyColumn: "Id",
                keyValue: new Guid("41000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "AssignTracks",
                keyColumn: "Id",
                keyValue: new Guid("41000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "CriteriaTemplates",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "CriteriaTemplates",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "AssignEvents",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "EventRoles",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "EventRoles",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"));

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "RegisterTeams");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "RegisterTeams",
                newName: "TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_RegisterTeams_EventId",
                table: "RegisterTeams",
                newName: "IX_RegisterTeams_TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterTeams_Topics_TopicId",
                table: "RegisterTeams",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
