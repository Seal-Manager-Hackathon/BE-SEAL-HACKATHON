using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hackathon.Repository.Migrations
{
    /// <inheritdoc />
    public partial class seed_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CreatedAt", "Description", "EndTime", "IsDisable", "LimitTeam", "MaxMember", "MinMember", "Name", "NumberRound", "RegisterLimitTime", "Season", "StartTime", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Detailed description for Tech Startup Challenge 2024", new DateTimeOffset(new DateTime(2024, 6, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 30, 5, 2, "Tech Startup Challenge 2024", 3, new DateTimeOffset(new DateTime(2024, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "2024", new DateTimeOffset(new DateTime(2024, 6, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Published", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("20000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Detailed description for Green Innovators Cup 2024", new DateTimeOffset(new DateTime(2024, 6, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 30, 5, 2, "Green Innovators Cup 2024", 3, new DateTimeOffset(new DateTime(2024, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "2024", new DateTimeOffset(new DateTime(2024, 6, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Published", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("20000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Detailed description for Cyber Security Arena 2025", new DateTimeOffset(new DateTime(2025, 6, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 30, 5, 2, "Cyber Security Arena 2025", 3, new DateTimeOffset(new DateTime(2025, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "2025", new DateTimeOffset(new DateTime(2025, 6, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Published", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("20000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Detailed description for AI HackFest 2025", new DateTimeOffset(new DateTime(2025, 6, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 30, 5, 2, "AI HackFest 2025", 3, new DateTimeOffset(new DateTime(2025, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "2025", new DateTimeOffset(new DateTime(2025, 6, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Published", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("20000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Detailed description for Web3 DevCon 2025", new DateTimeOffset(new DateTime(2025, 6, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, 30, 5, 2, "Web3 DevCon 2025", 3, new DateTimeOffset(new DateTime(2025, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "2025", new DateTimeOffset(new DateTime(2025, 6, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Published", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("20000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Detailed description for SEAL Coding Battle 2026", new DateTimeOffset(new DateTime(2026, 6, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 30, 5, 2, "SEAL Coding Battle 2026", 3, new DateTimeOffset(new DateTime(2026, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "2026", new DateTimeOffset(new DateTime(2026, 6, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Published", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("20000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Detailed description for Cloud Builders Cup 2026", new DateTimeOffset(new DateTime(2026, 6, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 30, 5, 2, "Cloud Builders Cup 2026", 3, new DateTimeOffset(new DateTime(2026, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "2026", new DateTimeOffset(new DateTime(2026, 6, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Published", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("20000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Detailed description for Data Science Hackathon 2026", new DateTimeOffset(new DateTime(2026, 6, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, 30, 5, 2, "Data Science Hackathon 2026", 3, new DateTimeOffset(new DateTime(2026, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "2026", new DateTimeOffset(new DateTime(2026, 6, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Published", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("20000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Detailed description for IoT Smart City 2027", new DateTimeOffset(new DateTime(2027, 6, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 30, 5, 2, "IoT Smart City 2027", 3, new DateTimeOffset(new DateTime(2027, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "2027", new DateTimeOffset(new DateTime(2027, 6, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Published", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("20000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Detailed description for Robotics Summit 2027", new DateTimeOffset(new DateTime(2027, 6, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 30, 5, 2, "Robotics Summit 2027", 3, new DateTimeOffset(new DateTime(2027, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "2027", new DateTimeOffset(new DateTime(2027, 6, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Published", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CanEdit", "CreatedAt", "IsDisable", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000010"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Nexus Startups", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("30000000-0000-0000-0000-000000000011"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Solar Energy Coders", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("30000000-0000-0000-0000-000000000012"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Cyber Defenders", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("30000000-0000-0000-0000-000000000013"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "AI Model Makers", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("30000000-0000-0000-0000-000000000014"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Web3 Validators", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("30000000-0000-0000-0000-000000000015"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "SEAL Coders Elite", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("30000000-0000-0000-0000-000000000016"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Cloud Deployers", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("30000000-0000-0000-0000-000000000017"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Data Analytics Hackers", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("30000000-0000-0000-0000-000000000018"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Smart Grids builders", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("30000000-0000-0000-0000-000000000019"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Robotics Pathfinder Team", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AvatarUrl", "BanReason", "BannedAt", "Bio", "College", "CreatedAt", "DateOfBirth", "Email", "FirstName", "HashPassword", "ImgUrl", "IsDisable", "IsVerified", "LastName", "LinkUrl", "PhoneNumber", "Role", "Status", "StudentId", "UpdatedAt", "VerifyEmailAt" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000010"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "alex.jones@student.local", "Alex", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Jones", "https://seed.local/users", "0900000000", 2, "Active", "SEAL010", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000011"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "emma.watson@student.local", "Emma", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Watson", "https://seed.local/users", "0900000000", 2, "Active", "SEAL011", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000012"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "liam.smith@student.local", "Liam", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Smith", "https://seed.local/users", "0900000000", 2, "Active", "SEAL012", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000013"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "sophia.brown@student.local", "Sophia", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Brown", "https://seed.local/users", "0900000000", 2, "Active", "SEAL013", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000014"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "mason.davis@student.local", "Mason", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Davis", "https://seed.local/users", "0900000000", 2, "Active", "SEAL014", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000015"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "olivia.miller@student.local", "Olivia", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Miller", "https://seed.local/users", "0900000000", 2, "Active", "SEAL015", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000016"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ethan.wilson@student.local", "Ethan", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Wilson", "https://seed.local/users", "0900000000", 2, "Active", "SEAL016", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000017"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ava.taylor@student.local", "Ava", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Taylor", "https://seed.local/users", "0900000000", 2, "Active", "SEAL017", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000018"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "lucas.thomas@student.local", "Lucas", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Thomas", "https://seed.local/users", "0900000000", 2, "Active", "SEAL018", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000019"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "mia.white@student.local", "Mia", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "White", "https://seed.local/users", "0900000000", 2, "Active", "SEAL019", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000020"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "robert.martin@lecturer.local", "Robert", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Martin", "https://seed.local/users", "0900000000", 3, "Active", "LECT001", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000021"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "linda.clark@lecturer.local", "Linda", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Clark", "https://seed.local/users", "0900000000", 3, "Active", "LECT002", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000022"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "david.lewis@lecturer.local", "David", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Lewis", "https://seed.local/users", "0900000000", 3, "Active", "LECT003", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000023"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "barbara.hall@lecturer.local", "Barbara", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Hall", "https://seed.local/users", "0900000000", 3, "Active", "LECT004", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000024"), "Seed address", "https://seed.local/avatar.png", null, null, "Seed user", "Seed University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "james.allen@lecturer.local", "James", "seed-password-hash-not-for-login", "https://seed.local/profile.png", false, true, "Allen", "https://seed.local/users", "0900000000", 3, "Active", "LECT005", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "AssignEvents",
                columns: new[] { "Id", "CreatedAt", "EventId", "EventRoleId", "IsDisable", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000010"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000020") },
                    { new Guid("40000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000011"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000021") },
                    { new Guid("40000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000012"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000022") },
                    { new Guid("40000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000013"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000023") },
                    { new Guid("40000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000014"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000024") },
                    { new Guid("40000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000015"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000020") },
                    { new Guid("40000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000016"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000021") },
                    { new Guid("40000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000017"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000022") },
                    { new Guid("40000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000018"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000023") },
                    { new Guid("40000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000019"), new Guid("55555555-5555-5555-5555-555555555555"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000024") }
                });

            migrationBuilder.InsertData(
                table: "Awards",
                columns: new[] { "Id", "CreatedAt", "Description", "EventId", "IsDisable", "LevelAward", "Name", "NumberOfAward", "Prize", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("26000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "First place award", new Guid("20000000-0000-0000-0000-000000000010"), false, "First", "First Prize", 1, 1000m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("26000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "First place award", new Guid("20000000-0000-0000-0000-000000000011"), false, "First", "First Prize", 1, 1200m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("26000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "First place award", new Guid("20000000-0000-0000-0000-000000000012"), false, "First", "First Prize", 1, 1500m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("26000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "First place award", new Guid("20000000-0000-0000-0000-000000000013"), false, "First", "First Prize", 1, 1000m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("26000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "First place award", new Guid("20000000-0000-0000-0000-000000000014"), false, "First", "First Prize", 1, 1100m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("26000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "First place award", new Guid("20000000-0000-0000-0000-000000000015"), false, "First", "First Prize", 1, 2000m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("26000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "First place award", new Guid("20000000-0000-0000-0000-000000000016"), false, "First", "First Prize", 1, 1500m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("26000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "First place award", new Guid("20000000-0000-0000-0000-000000000017"), false, "First", "First Prize", 1, 1000m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("26000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "First place award", new Guid("20000000-0000-0000-0000-000000000018"), false, "First", "First Prize", 1, 1300m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("26000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "First place award", new Guid("20000000-0000-0000-0000-000000000019"), false, "First", "First Prize", 1, 2500m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "EmailVerifications",
                columns: new[] { "Id", "CreatedAt", "ExpiredAt", "IsDisable", "Status", "TokenHash", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("14000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Verified", "verify-14000000-0000-0000-0000-000000000010", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000010") },
                    { new Guid("14000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Verified", "verify-14000000-0000-0000-0000-000000000011", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000011") },
                    { new Guid("14000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Verified", "verify-14000000-0000-0000-0000-000000000012", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000012") },
                    { new Guid("14000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Verified", "verify-14000000-0000-0000-0000-000000000013", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000013") },
                    { new Guid("14000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Verified", "verify-14000000-0000-0000-0000-000000000014", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000014") },
                    { new Guid("14000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Verified", "verify-14000000-0000-0000-0000-000000000015", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000015") },
                    { new Guid("14000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Verified", "verify-14000000-0000-0000-0000-000000000016", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000016") },
                    { new Guid("14000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Verified", "verify-14000000-0000-0000-0000-000000000017", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000017") },
                    { new Guid("14000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Verified", "verify-14000000-0000-0000-0000-000000000018", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000018") },
                    { new Guid("14000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Verified", "verify-14000000-0000-0000-0000-000000000019", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000019") }
                });

            migrationBuilder.InsertData(
                table: "Invitations",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDisable", "LimitTime", "Status", "TeamId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("70000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed invitation detail", false, new DateTimeOffset(new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Pending", new Guid("30000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000010") },
                    { new Guid("70000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed invitation detail", false, new DateTimeOffset(new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Pending", new Guid("30000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000011") },
                    { new Guid("70000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed invitation detail", false, new DateTimeOffset(new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Pending", new Guid("30000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000012") },
                    { new Guid("70000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed invitation detail", false, new DateTimeOffset(new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Pending", new Guid("30000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000013") },
                    { new Guid("70000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed invitation detail", false, new DateTimeOffset(new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Pending", new Guid("30000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000014") },
                    { new Guid("70000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed invitation detail", false, new DateTimeOffset(new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Pending", new Guid("30000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000015") },
                    { new Guid("70000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed invitation detail", false, new DateTimeOffset(new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Pending", new Guid("30000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000016") },
                    { new Guid("70000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed invitation detail", false, new DateTimeOffset(new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Pending", new Guid("30000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000017") },
                    { new Guid("70000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed invitation detail", false, new DateTimeOffset(new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Pending", new Guid("30000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000018") },
                    { new Guid("70000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed invitation detail", false, new DateTimeOffset(new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Pending", new Guid("30000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000019") }
                });

            migrationBuilder.InsertData(
                table: "LeaderBoards",
                columns: new[] { "Id", "CreatedAt", "EventId", "IsDisable", "UpdatedAt", "Year" },
                values: new object[,]
                {
                    { new Guid("60000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000010"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2024 },
                    { new Guid("60000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000011"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2024 },
                    { new Guid("60000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000012"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2025 },
                    { new Guid("60000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000013"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2025 },
                    { new Guid("60000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000014"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2025 },
                    { new Guid("60000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000015"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2026 },
                    { new Guid("60000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000016"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2026 },
                    { new Guid("60000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000017"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2026 },
                    { new Guid("60000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000018"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2027 },
                    { new Guid("60000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000019"), false, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2027 }
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDisable", "Status", "TeamId", "Title", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("71000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Notification for team activity in New Team Created", false, "Unread", new Guid("30000000-0000-0000-0000-000000000010"), "New Team Created", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000010") },
                    { new Guid("71000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Notification for team activity in New Team Created", false, "Unread", new Guid("30000000-0000-0000-0000-000000000011"), "New Team Created", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000011") },
                    { new Guid("71000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Notification for team activity in New Team Created", false, "Unread", new Guid("30000000-0000-0000-0000-000000000012"), "New Team Created", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000012") },
                    { new Guid("71000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Notification for team activity in New Team Created", false, "Unread", new Guid("30000000-0000-0000-0000-000000000013"), "New Team Created", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000013") },
                    { new Guid("71000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Notification for team activity in New Team Created", false, "Unread", new Guid("30000000-0000-0000-0000-000000000014"), "New Team Created", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000014") },
                    { new Guid("71000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Notification for team activity in New Team Created", false, "Unread", new Guid("30000000-0000-0000-0000-000000000015"), "New Team Created", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000015") },
                    { new Guid("71000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Notification for team activity in New Team Created", false, "Unread", new Guid("30000000-0000-0000-0000-000000000016"), "New Team Created", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000016") },
                    { new Guid("71000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Notification for team activity in New Team Created", false, "Unread", new Guid("30000000-0000-0000-0000-000000000017"), "New Team Created", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000017") },
                    { new Guid("71000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Notification for team activity in New Team Created", false, "Unread", new Guid("30000000-0000-0000-0000-000000000018"), "New Team Created", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000018") },
                    { new Guid("71000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Notification for team activity in New Team Created", false, "Unread", new Guid("30000000-0000-0000-0000-000000000019"), "New Team Created", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000019") }
                });

            migrationBuilder.InsertData(
                table: "RefreshTokens",
                columns: new[] { "Id", "CreatedAt", "DeviceLabel", "ExpiredAt", "IpAddress", "IsDisable", "RefreshTokenHash", "RevokedAt", "UpdatedAt", "UserAgent", "UserId" },
                values: new object[,]
                {
                    { new Guid("12000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Device", new DateTimeOffset(new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "127.0.0.1", false, "token-12000000-0000-0000-0000-000000000010", null, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Agent", new Guid("10000000-0000-0000-0000-000000000010") },
                    { new Guid("12000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Device", new DateTimeOffset(new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "127.0.0.1", false, "token-12000000-0000-0000-0000-000000000011", null, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Agent", new Guid("10000000-0000-0000-0000-000000000011") },
                    { new Guid("12000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Device", new DateTimeOffset(new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "127.0.0.1", false, "token-12000000-0000-0000-0000-000000000012", null, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Agent", new Guid("10000000-0000-0000-0000-000000000012") },
                    { new Guid("12000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Device", new DateTimeOffset(new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "127.0.0.1", false, "token-12000000-0000-0000-0000-000000000013", null, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Agent", new Guid("10000000-0000-0000-0000-000000000013") },
                    { new Guid("12000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Device", new DateTimeOffset(new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "127.0.0.1", false, "token-12000000-0000-0000-0000-000000000014", null, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Agent", new Guid("10000000-0000-0000-0000-000000000014") },
                    { new Guid("12000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Device", new DateTimeOffset(new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "127.0.0.1", false, "token-12000000-0000-0000-0000-000000000015", null, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Agent", new Guid("10000000-0000-0000-0000-000000000015") },
                    { new Guid("12000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Device", new DateTimeOffset(new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "127.0.0.1", false, "token-12000000-0000-0000-0000-000000000016", null, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Agent", new Guid("10000000-0000-0000-0000-000000000016") },
                    { new Guid("12000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Device", new DateTimeOffset(new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "127.0.0.1", false, "token-12000000-0000-0000-0000-000000000017", null, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Agent", new Guid("10000000-0000-0000-0000-000000000017") },
                    { new Guid("12000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Device", new DateTimeOffset(new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "127.0.0.1", false, "token-12000000-0000-0000-0000-000000000018", null, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Agent", new Guid("10000000-0000-0000-0000-000000000018") },
                    { new Guid("12000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Device", new DateTimeOffset(new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "127.0.0.1", false, "token-12000000-0000-0000-0000-000000000019", null, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed Agent", new Guid("10000000-0000-0000-0000-000000000019") }
                });

            migrationBuilder.InsertData(
                table: "RegisterTeams",
                columns: new[] { "Id", "CreatedAt", "Description", "EventId", "IsBanned", "IsDisable", "RejectionReason", "Status", "TeamId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("31000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Paging team registration", new Guid("20000000-0000-0000-0000-000000000010"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("31000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Paging team registration", new Guid("20000000-0000-0000-0000-000000000011"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("31000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Paging team registration", new Guid("20000000-0000-0000-0000-000000000012"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("31000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Paging team registration", new Guid("20000000-0000-0000-0000-000000000013"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("31000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Paging team registration", new Guid("20000000-0000-0000-0000-000000000014"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("31000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Paging team registration", new Guid("20000000-0000-0000-0000-000000000015"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("31000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Paging team registration", new Guid("20000000-0000-0000-0000-000000000016"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("31000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Paging team registration", new Guid("20000000-0000-0000-0000-000000000017"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("31000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Paging team registration", new Guid("20000000-0000-0000-0000-000000000018"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("31000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Paging team registration", new Guid("20000000-0000-0000-0000-000000000019"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "ResetPasswords",
                columns: new[] { "Id", "CreatedAt", "ExpiresAt", "IsDisable", "IsUsed", "TokenHash", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("13000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "reset-13000000-0000-0000-0000-000000000010", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000010") },
                    { new Guid("13000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "reset-13000000-0000-0000-0000-000000000011", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000011") },
                    { new Guid("13000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "reset-13000000-0000-0000-0000-000000000012", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000012") },
                    { new Guid("13000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "reset-13000000-0000-0000-0000-000000000013", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000013") },
                    { new Guid("13000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "reset-13000000-0000-0000-0000-000000000014", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000014") },
                    { new Guid("13000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "reset-13000000-0000-0000-0000-000000000015", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000015") },
                    { new Guid("13000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "reset-13000000-0000-0000-0000-000000000016", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000016") },
                    { new Guid("13000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "reset-13000000-0000-0000-0000-000000000017", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000017") },
                    { new Guid("13000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "reset-13000000-0000-0000-0000-000000000018", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000018") },
                    { new Guid("13000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "reset-13000000-0000-0000-0000-000000000019", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000019") }
                });

            migrationBuilder.InsertData(
                table: "Rounds",
                columns: new[] { "Id", "CreatedAt", "Description", "EndSubmission", "EndTime", "EventId", "IsDisable", "LimitTeam", "Name", "StartSubmission", "StartTime", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("21000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Round description for Idea Submission 2024", new DateTimeOffset(new DateTime(2024, 6, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000010"), false, 25, "Idea Submission 2024", new DateTimeOffset(new DateTime(2024, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("21000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Round description for Concept Selection 2024", new DateTimeOffset(new DateTime(2024, 6, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000011"), false, 25, "Concept Selection 2024", new DateTimeOffset(new DateTime(2024, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("21000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Round description for Security Design 2025", new DateTimeOffset(new DateTime(2025, 6, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000012"), false, 25, "Security Design 2025", new DateTimeOffset(new DateTime(2025, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("21000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Round description for AI Model Pitch 2025", new DateTimeOffset(new DateTime(2025, 6, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000013"), false, 25, "AI Model Pitch 2025", new DateTimeOffset(new DateTime(2025, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("21000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Round description for Web3 Auditing 2025", new DateTimeOffset(new DateTime(2025, 6, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000014"), false, 25, "Web3 Auditing 2025", new DateTimeOffset(new DateTime(2025, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("21000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Round description for Rapid Prototyping 2026", new DateTimeOffset(new DateTime(2026, 6, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000015"), false, 25, "Rapid Prototyping 2026", new DateTimeOffset(new DateTime(2026, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("21000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Round description for Architecture Demo 2026", new DateTimeOffset(new DateTime(2026, 6, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000016"), false, 25, "Architecture Demo 2026", new DateTimeOffset(new DateTime(2026, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("21000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Round description for Model Training Pitch 2026", new DateTimeOffset(new DateTime(2026, 6, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000017"), false, 25, "Model Training Pitch 2026", new DateTimeOffset(new DateTime(2026, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("21000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Round description for Smart Grids Pitch 2027", new DateTimeOffset(new DateTime(2027, 6, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2027, 6, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000018"), false, 25, "Smart Grids Pitch 2027", new DateTimeOffset(new DateTime(2027, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2027, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("21000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Round description for Robotics Control Pitch 2027", new DateTimeOffset(new DateTime(2027, 6, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2027, 6, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000019"), false, 25, "Robotics Control Pitch 2027", new DateTimeOffset(new DateTime(2027, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2027, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "TeamDetails",
                columns: new[] { "Id", "CreatedAt", "IsDisable", "IsLeader", "Status", "TeamId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("30100000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000010") },
                    { new Guid("30100000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000011") },
                    { new Guid("30100000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000012") },
                    { new Guid("30100000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000013") },
                    { new Guid("30100000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000014") },
                    { new Guid("30100000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000015") },
                    { new Guid("30100000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000016") },
                    { new Guid("30100000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000017") },
                    { new Guid("30100000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000018") },
                    { new Guid("30100000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000019") }
                });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "CreatedAt", "Description", "EventId", "IsDisable", "MaxTeam", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("24000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Tech Startup Challenge 2024 Track", new Guid("20000000-0000-0000-0000-000000000010"), false, 15, "E-Commerce Innovation", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("24000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Green Innovators Cup 2024 Track", new Guid("20000000-0000-0000-0000-000000000011"), false, 15, "Renewable Energy Solutions", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("24000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Cyber Security Arena 2025 Track", new Guid("20000000-0000-0000-0000-000000000012"), false, 15, "Intrusion Detection Systems", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("24000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "AI HackFest 2025 Track", new Guid("20000000-0000-0000-0000-000000000013"), false, 15, "Natural Language Generation", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("24000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Web3 DevCon 2025 Track", new Guid("20000000-0000-0000-0000-000000000014"), false, 15, "Smart Contract Audit", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("24000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "SEAL Coding Battle 2026 Track", new Guid("20000000-0000-0000-0000-000000000015"), false, 15, "Algorithm Optimization", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("24000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Cloud Builders Cup 2026 Track", new Guid("20000000-0000-0000-0000-000000000016"), false, 15, "Serverless Deployment", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("24000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Data Science Hackathon 2026 Track", new Guid("20000000-0000-0000-0000-000000000017"), false, 15, "Predictive Analytics", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("24000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "IoT Smart City 2027 Track", new Guid("20000000-0000-0000-0000-000000000018"), false, 15, "Smart Grids Infrastructure", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("24000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Robotics Summit 2027 Track", new Guid("20000000-0000-0000-0000-000000000019"), false, 15, "Robotic Pathfinding", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "AssignTracks",
                columns: new[] { "Id", "AssignEventId", "CreatedAt", "IsDisable", "TrackId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("41000000-0000-0000-0000-000000000010"), new Guid("40000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("24000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("41000000-0000-0000-0000-000000000011"), new Guid("40000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("24000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("41000000-0000-0000-0000-000000000012"), new Guid("40000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("24000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("41000000-0000-0000-0000-000000000013"), new Guid("40000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("24000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("41000000-0000-0000-0000-000000000014"), new Guid("40000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("24000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("41000000-0000-0000-0000-000000000015"), new Guid("40000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("24000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("41000000-0000-0000-0000-000000000016"), new Guid("40000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("24000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("41000000-0000-0000-0000-000000000017"), new Guid("40000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("24000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("41000000-0000-0000-0000-000000000018"), new Guid("40000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("24000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("41000000-0000-0000-0000-000000000019"), new Guid("40000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("24000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "CriteriaTemplates",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDisable", "RoundId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("22000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Evaluation template details for Startup Pitch Template", false, new Guid("21000000-0000-0000-0000-000000000010"), "Startup Pitch Template", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("22000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Evaluation template details for Green Tech Template", false, new Guid("21000000-0000-0000-0000-000000000011"), "Green Tech Template", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("22000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Evaluation template details for SecOps Template", false, new Guid("21000000-0000-0000-0000-000000000012"), "SecOps Template", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("22000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Evaluation template details for AI Evaluation Template", false, new Guid("21000000-0000-0000-0000-000000000013"), "AI Evaluation Template", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("22000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Evaluation template details for Web3 Template", false, new Guid("21000000-0000-0000-0000-000000000014"), "Web3 Template", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("22000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Evaluation template details for Algorithm Performance Template", false, new Guid("21000000-0000-0000-0000-000000000015"), "Algorithm Performance Template", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("22000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Evaluation template details for Cloud Scaling Template", false, new Guid("21000000-0000-0000-0000-000000000016"), "Cloud Scaling Template", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("22000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Evaluation template details for Data Science Evaluation Template", false, new Guid("21000000-0000-0000-0000-000000000017"), "Data Science Evaluation Template", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("22000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Evaluation template details for Smart Grids Template", false, new Guid("21000000-0000-0000-0000-000000000018"), "Smart Grids Template", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("22000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Evaluation template details for Robotics Control Template", false, new Guid("21000000-0000-0000-0000-000000000019"), "Robotics Control Template", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "LeaderBoardDetails",
                columns: new[] { "Id", "CreatedAt", "IsDisable", "LeaderBoardId", "LevelAward", "Score", "TeamId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("61000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("60000000-0000-0000-0000-000000000010"), "First", 85m, new Guid("30000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("61000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("60000000-0000-0000-0000-000000000011"), "First", 89m, new Guid("30000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("61000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("60000000-0000-0000-0000-000000000012"), "First", 91m, new Guid("30000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("61000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("60000000-0000-0000-0000-000000000013"), "Second", 78m, new Guid("30000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("61000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("60000000-0000-0000-0000-000000000014"), "First", 88m, new Guid("30000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("61000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("60000000-0000-0000-0000-000000000015"), "First", 92m, new Guid("30000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("61000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("60000000-0000-0000-0000-000000000016"), "First", 87m, new Guid("30000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("61000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("60000000-0000-0000-0000-000000000017"), "Second", 83m, new Guid("30000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("61000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("60000000-0000-0000-0000-000000000018"), "First", 95m, new Guid("30000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("61000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("60000000-0000-0000-0000-000000000019"), "First", 94m, new Guid("30000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "RoundDetails",
                columns: new[] { "Id", "CreatedAt", "IsDisable", "RegisterTeamId", "RoundId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("32000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000010"), new Guid("21000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000011"), new Guid("21000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000012"), new Guid("21000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000013"), new Guid("21000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000014"), new Guid("21000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000015"), new Guid("21000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000016"), new Guid("21000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000017"), new Guid("21000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000018"), new Guid("21000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000019"), new Guid("21000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDisable", "Title", "TrackId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("25000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Automate customer support", false, "Retail AI Chatbot", new Guid("24000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("25000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Improve efficiency", false, "Solar Panel Optimization", new Guid("24000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("25000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Network security agent", false, "Malware Classifier AI", new Guid("24000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("25000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Generate code snippets", false, "LLM Code Assistant", new Guid("24000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("25000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Smart contract platform", false, "DeFi Yield Optimizer", new Guid("24000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("25000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Optimize route calculations", false, "Dynamic Pathfinding", new Guid("24000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("25000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Automated scaling agent", false, "SaaS Scale Engine", new Guid("24000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("25000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Identify customer churn risks", false, "Churn Predictor Model", new Guid("24000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("25000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Smart sensor nodes", false, "Grid Monitoring Agent", new Guid("24000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("25000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Autonomous LiDAR paths", false, "LiDAR Mapping Agent", new Guid("24000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "CriteriaItems",
                columns: new[] { "Id", "CreatedAt", "CriteriaTemplateId", "Description", "IsDisable", "Name", "Score", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("23000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22000000-0000-0000-0000-000000000010"), "Criteria item for Business Value", false, "Business Value", 100m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("23000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22000000-0000-0000-0000-000000000011"), "Criteria item for Sustainability Impact", false, "Sustainability Impact", 100m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("23000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22000000-0000-0000-0000-000000000012"), "Criteria item for Threat Modelling", false, "Threat Modelling", 100m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("23000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22000000-0000-0000-0000-000000000013"), "Criteria item for Model Performance", false, "Model Performance", 100m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("23000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22000000-0000-0000-0000-000000000014"), "Criteria item for Web3 Security Audit", false, "Web3 Security Audit", 100m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("23000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22000000-0000-0000-0000-000000000015"), "Criteria item for Code Optimization", false, "Code Optimization", 100m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("23000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22000000-0000-0000-0000-000000000016"), "Criteria item for Deployment Latency", false, "Deployment Latency", 100m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("23000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22000000-0000-0000-0000-000000000017"), "Criteria item for Accuracy Metric", false, "Accuracy Metric", 100m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("23000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22000000-0000-0000-0000-000000000018"), "Criteria item for Grid Latency", false, "Grid Latency", 100m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("23000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22000000-0000-0000-0000-000000000019"), "Criteria item for Control Loop Latency", false, "Control Loop Latency", 100m, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "MentorNotifications",
                columns: new[] { "Id", "AssignTrackId", "CreatedAt", "Description", "IsDisable", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("72000000-0000-0000-0000-000000000010"), new Guid("41000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "A new track status update has been broadcasted", false, "Event Update Notification", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("72000000-0000-0000-0000-000000000011"), new Guid("41000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "A new track status update has been broadcasted", false, "Event Update Notification", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("72000000-0000-0000-0000-000000000012"), new Guid("41000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "A new track status update has been broadcasted", false, "Event Update Notification", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("72000000-0000-0000-0000-000000000013"), new Guid("41000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "A new track status update has been broadcasted", false, "Event Update Notification", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("72000000-0000-0000-0000-000000000014"), new Guid("41000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "A new track status update has been broadcasted", false, "Event Update Notification", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("72000000-0000-0000-0000-000000000015"), new Guid("41000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "A new track status update has been broadcasted", false, "Event Update Notification", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("72000000-0000-0000-0000-000000000016"), new Guid("41000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "A new track status update has been broadcasted", false, "Event Update Notification", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("72000000-0000-0000-0000-000000000017"), new Guid("41000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "A new track status update has been broadcasted", false, "Event Update Notification", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("72000000-0000-0000-0000-000000000018"), new Guid("41000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "A new track status update has been broadcasted", false, "Event Update Notification", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("72000000-0000-0000-0000-000000000019"), new Guid("41000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "A new track status update has been broadcasted", false, "Event Update Notification", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDisable", "RoundDetailId", "Status", "SubmittedAt", "UpdatedAt", "Url" },
                values: new object[,]
                {
                    { new Guid("33000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed submission", false, new Guid("32000000-0000-0000-0000-000000000010"), "Submitted", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://seed.local/submissions/nexus-startups-idea" },
                    { new Guid("33000000-0000-0000-0000-000000000011"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed submission", false, new Guid("32000000-0000-0000-0000-000000000011"), "Submitted", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://seed.local/submissions/solar-energy-idea" },
                    { new Guid("33000000-0000-0000-0000-000000000012"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed submission", false, new Guid("32000000-0000-0000-0000-000000000012"), "Submitted", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://seed.local/submissions/cyber-defenders-idea" },
                    { new Guid("33000000-0000-0000-0000-000000000013"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed submission", false, new Guid("32000000-0000-0000-0000-000000000013"), "Submitted", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://seed.local/submissions/ai-makers-idea" },
                    { new Guid("33000000-0000-0000-0000-000000000014"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed submission", false, new Guid("32000000-0000-0000-0000-000000000014"), "Submitted", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://seed.local/submissions/web3-validators-idea" },
                    { new Guid("33000000-0000-0000-0000-000000000015"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed submission", false, new Guid("32000000-0000-0000-0000-000000000015"), "Submitted", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://seed.local/submissions/seal-elite-idea" },
                    { new Guid("33000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed submission", false, new Guid("32000000-0000-0000-0000-000000000016"), "Submitted", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://seed.local/submissions/cloud-deployers-idea" },
                    { new Guid("33000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed submission", false, new Guid("32000000-0000-0000-0000-000000000017"), "Submitted", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://seed.local/submissions/data-analytics-idea" },
                    { new Guid("33000000-0000-0000-0000-000000000018"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed submission", false, new Guid("32000000-0000-0000-0000-000000000018"), "Submitted", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://seed.local/submissions/smart-grids-idea" },
                    { new Guid("33000000-0000-0000-0000-000000000019"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Seed submission", false, new Guid("32000000-0000-0000-0000-000000000019"), "Submitted", new DateTimeOffset(new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://seed.local/submissions/robotics-pathfinder-idea" }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "AssignEventId", "CreatedAt", "Description", "FileUrl", "ImgUrl", "IsDisable", "Reason", "Status", "SubmissionId", "Title", "TypeReport", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("73000000-0000-0000-0000-000000000010"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Report detail comments", "https://seed.local/reports/file.pdf", "https://seed.local/reports/image.png", false, "Paging evaluation review comment", "Open", new Guid("33000000-0000-0000-0000-000000000010"), "Evaluation Report", "Submission", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000004") },
                    { new Guid("73000000-0000-0000-0000-000000000011"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Report detail comments", "https://seed.local/reports/file.pdf", "https://seed.local/reports/image.png", false, "Paging evaluation review comment", "Open", new Guid("33000000-0000-0000-0000-000000000011"), "Evaluation Report", "Submission", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000004") },
                    { new Guid("73000000-0000-0000-0000-000000000012"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Report detail comments", "https://seed.local/reports/file.pdf", "https://seed.local/reports/image.png", false, "Paging evaluation review comment", "Open", new Guid("33000000-0000-0000-0000-000000000012"), "Evaluation Report", "Submission", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000004") },
                    { new Guid("73000000-0000-0000-0000-000000000013"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Report detail comments", "https://seed.local/reports/file.pdf", "https://seed.local/reports/image.png", false, "Paging evaluation review comment", "Open", new Guid("33000000-0000-0000-0000-000000000013"), "Evaluation Report", "Submission", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000004") },
                    { new Guid("73000000-0000-0000-0000-000000000014"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Report detail comments", "https://seed.local/reports/file.pdf", "https://seed.local/reports/image.png", false, "Paging evaluation review comment", "Open", new Guid("33000000-0000-0000-0000-000000000014"), "Evaluation Report", "Submission", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000004") },
                    { new Guid("73000000-0000-0000-0000-000000000015"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Report detail comments", "https://seed.local/reports/file.pdf", "https://seed.local/reports/image.png", false, "Paging evaluation review comment", "Open", new Guid("33000000-0000-0000-0000-000000000015"), "Evaluation Report", "Submission", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000004") },
                    { new Guid("73000000-0000-0000-0000-000000000016"), new Guid("40000000-0000-0000-0000-000000000016"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Report detail comments", "https://seed.local/reports/file.pdf", "https://seed.local/reports/image.png", false, "Paging evaluation review comment", "Open", new Guid("33000000-0000-0000-0000-000000000016"), "Evaluation Report", "Submission", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000021") },
                    { new Guid("73000000-0000-0000-0000-000000000017"), new Guid("40000000-0000-0000-0000-000000000017"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Report detail comments", "https://seed.local/reports/file.pdf", "https://seed.local/reports/image.png", false, "Paging evaluation review comment", "Open", new Guid("33000000-0000-0000-0000-000000000017"), "Evaluation Report", "Submission", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000022") },
                    { new Guid("73000000-0000-0000-0000-000000000018"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Report detail comments", "https://seed.local/reports/file.pdf", "https://seed.local/reports/image.png", false, "Paging evaluation review comment", "Open", new Guid("33000000-0000-0000-0000-000000000018"), "Evaluation Report", "Submission", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000004") },
                    { new Guid("73000000-0000-0000-0000-000000000019"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Report detail comments", "https://seed.local/reports/file.pdf", "https://seed.local/reports/image.png", false, "Paging evaluation review comment", "Open", new Guid("33000000-0000-0000-0000-000000000019"), "Evaluation Report", "Submission", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000004") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "CriteriaItems",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "CriteriaItems",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "CriteriaItems",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "CriteriaItems",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "CriteriaItems",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "CriteriaItems",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "CriteriaItems",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "CriteriaItems",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "CriteriaItems",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "CriteriaItems",
                keyColumn: "Id",
                keyValue: new Guid("23000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "EmailVerifications",
                keyColumn: "Id",
                keyValue: new Guid("14000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "EmailVerifications",
                keyColumn: "Id",
                keyValue: new Guid("14000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "EmailVerifications",
                keyColumn: "Id",
                keyValue: new Guid("14000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "EmailVerifications",
                keyColumn: "Id",
                keyValue: new Guid("14000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "EmailVerifications",
                keyColumn: "Id",
                keyValue: new Guid("14000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "EmailVerifications",
                keyColumn: "Id",
                keyValue: new Guid("14000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "EmailVerifications",
                keyColumn: "Id",
                keyValue: new Guid("14000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "EmailVerifications",
                keyColumn: "Id",
                keyValue: new Guid("14000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "EmailVerifications",
                keyColumn: "Id",
                keyValue: new Guid("14000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "EmailVerifications",
                keyColumn: "Id",
                keyValue: new Guid("14000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Invitations",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Invitations",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Invitations",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Invitations",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Invitations",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Invitations",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Invitations",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Invitations",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Invitations",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Invitations",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "MentorNotifications",
                keyColumn: "Id",
                keyValue: new Guid("72000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "MentorNotifications",
                keyColumn: "Id",
                keyValue: new Guid("72000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "MentorNotifications",
                keyColumn: "Id",
                keyValue: new Guid("72000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "MentorNotifications",
                keyColumn: "Id",
                keyValue: new Guid("72000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "MentorNotifications",
                keyColumn: "Id",
                keyValue: new Guid("72000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "MentorNotifications",
                keyColumn: "Id",
                keyValue: new Guid("72000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "MentorNotifications",
                keyColumn: "Id",
                keyValue: new Guid("72000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "MentorNotifications",
                keyColumn: "Id",
                keyValue: new Guid("72000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "MentorNotifications",
                keyColumn: "Id",
                keyValue: new Guid("72000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "MentorNotifications",
                keyColumn: "Id",
                keyValue: new Guid("72000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: new Guid("71000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: new Guid("71000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: new Guid("71000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: new Guid("71000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: new Guid("71000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: new Guid("71000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: new Guid("71000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: new Guid("71000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: new Guid("71000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: new Guid("71000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "RefreshTokens",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "RefreshTokens",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "RefreshTokens",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "RefreshTokens",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "RefreshTokens",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "RefreshTokens",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "RefreshTokens",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "RefreshTokens",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "RefreshTokens",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "RefreshTokens",
                keyColumn: "Id",
                keyValue: new Guid("12000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("73000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("73000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("73000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("73000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("73000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("73000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("73000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("73000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("73000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("73000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "ResetPasswords",
                keyColumn: "Id",
                keyValue: new Guid("13000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "ResetPasswords",
                keyColumn: "Id",
                keyValue: new Guid("13000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "ResetPasswords",
                keyColumn: "Id",
                keyValue: new Guid("13000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "ResetPasswords",
                keyColumn: "Id",
                keyValue: new Guid("13000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "ResetPasswords",
                keyColumn: "Id",
                keyValue: new Guid("13000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "ResetPasswords",
                keyColumn: "Id",
                keyValue: new Guid("13000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "ResetPasswords",
                keyColumn: "Id",
                keyValue: new Guid("13000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "ResetPasswords",
                keyColumn: "Id",
                keyValue: new Guid("13000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "ResetPasswords",
                keyColumn: "Id",
                keyValue: new Guid("13000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "ResetPasswords",
                keyColumn: "Id",
                keyValue: new Guid("13000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("25000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("25000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("25000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("25000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("25000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("25000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("25000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("25000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("25000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("25000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "AssignTracks",
                keyColumn: "Id",
                keyValue: new Guid("41000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "AssignTracks",
                keyColumn: "Id",
                keyValue: new Guid("41000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "AssignTracks",
                keyColumn: "Id",
                keyValue: new Guid("41000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "AssignTracks",
                keyColumn: "Id",
                keyValue: new Guid("41000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "AssignTracks",
                keyColumn: "Id",
                keyValue: new Guid("41000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "AssignTracks",
                keyColumn: "Id",
                keyValue: new Guid("41000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "AssignTracks",
                keyColumn: "Id",
                keyValue: new Guid("41000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "AssignTracks",
                keyColumn: "Id",
                keyValue: new Guid("41000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "AssignTracks",
                keyColumn: "Id",
                keyValue: new Guid("41000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "AssignTracks",
                keyColumn: "Id",
                keyValue: new Guid("41000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "CriteriaTemplates",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "CriteriaTemplates",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "CriteriaTemplates",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "CriteriaTemplates",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "CriteriaTemplates",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "CriteriaTemplates",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "CriteriaTemplates",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "CriteriaTemplates",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "CriteriaTemplates",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "CriteriaTemplates",
                keyColumn: "Id",
                keyValue: new Guid("22000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "AssignEvents",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "AssignEvents",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "AssignEvents",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "AssignEvents",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "AssignEvents",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "AssignEvents",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "AssignEvents",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "AssignEvents",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "AssignEvents",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "AssignEvents",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: new Guid("24000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000019"));
        }
    }
}
