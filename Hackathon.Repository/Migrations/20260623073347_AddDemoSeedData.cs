using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hackathon.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddDemoSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000001"),
                column: "EndSubmission",
                value: new DateTimeOffset(new DateTime(2026, 6, 23, 23, 59, 59, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CanEdit", "CreatedAt", "IsDisable", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000020"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "AI Mavericks", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("30000000-0000-0000-0000-000000000021"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Eco Guardians", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("30000000-0000-0000-0000-000000000022"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Code Visionaries", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("30000000-0000-0000-0000-000000000023"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "GreenTech Solutions", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("30000000-0000-0000-0000-000000000024"), true, new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "AI Builders", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AvatarUrl", "BanReason", "BannedAt", "Bio", "College", "CreatedAt", "DateOfBirth", "Email", "FirstName", "HashPassword", "ImgUrl", "IsDisable", "IsVerified", "LastName", "LinkUrl", "PhoneNumber", "Role", "Status", "StudentId", "UpdatedAt", "VerifyEmailAt" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000030"), "Demo address", "https://robohash.org/thanh.nguyen@demo.local", null, null, "Demo user", "SEAL University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "thanh.nguyen@demo.local", "Thanh", "$2a$11$ELUlXu.C3Yh0miS3.dAZaO17ER/stLENq.EWnMYmPBiwZ14X8g1i6", "https://robohash.org/thanh.nguyen@demo.local", false, true, "Nguyen", "", "0900000000", 2, "Active", "SEAL030", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000031"), "Demo address", "https://robohash.org/anh.pham@demo.local", null, null, "Demo user", "SEAL University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "anh.pham@demo.local", "Anh", "$2a$11$ELUlXu.C3Yh0miS3.dAZaO17ER/stLENq.EWnMYmPBiwZ14X8g1i6", "https://robohash.org/anh.pham@demo.local", false, true, "Pham", "", "0900000000", 2, "Active", "SEAL031", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000032"), "Demo address", "https://robohash.org/minh.tran@demo.local", null, null, "Demo user", "SEAL University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "minh.tran@demo.local", "Minh", "$2a$11$ELUlXu.C3Yh0miS3.dAZaO17ER/stLENq.EWnMYmPBiwZ14X8g1i6", "https://robohash.org/minh.tran@demo.local", false, true, "Tran", "", "0900000000", 2, "Active", "SEAL032", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000033"), "Demo address", "https://robohash.org/hoa.le@demo.local", null, null, "Demo user", "SEAL University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "hoa.le@demo.local", "Hoa", "$2a$11$ELUlXu.C3Yh0miS3.dAZaO17ER/stLENq.EWnMYmPBiwZ14X8g1i6", "https://robohash.org/hoa.le@demo.local", false, true, "Le", "", "0900000000", 2, "Active", "SEAL033", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000034"), "Demo address", "https://robohash.org/binh.hoang@demo.local", null, null, "Demo user", "SEAL University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "binh.hoang@demo.local", "Binh", "$2a$11$ELUlXu.C3Yh0miS3.dAZaO17ER/stLENq.EWnMYmPBiwZ14X8g1i6", "https://robohash.org/binh.hoang@demo.local", false, true, "Hoang", "", "0900000000", 2, "Active", "SEAL034", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000035"), "Demo address", "https://robohash.org/lan.vu@demo.local", null, null, "Demo user", "SEAL University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "lan.vu@demo.local", "Lan", "$2a$11$ELUlXu.C3Yh0miS3.dAZaO17ER/stLENq.EWnMYmPBiwZ14X8g1i6", "https://robohash.org/lan.vu@demo.local", false, true, "Vu", "", "0900000000", 2, "Active", "SEAL035", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000036"), "Demo address", "https://robohash.org/tuan.do@demo.local", null, null, "Demo user", "SEAL University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "tuan.do@demo.local", "Tuan", "$2a$11$ELUlXu.C3Yh0miS3.dAZaO17ER/stLENq.EWnMYmPBiwZ14X8g1i6", "https://robohash.org/tuan.do@demo.local", false, true, "Do", "", "0900000000", 2, "Active", "SEAL036", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000037"), "Demo address", "https://robohash.org/hieu.nguyen@demo.local", null, null, "Demo user", "SEAL University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "hieu.nguyen@demo.local", "Hieu", "$2a$11$ELUlXu.C3Yh0miS3.dAZaO17ER/stLENq.EWnMYmPBiwZ14X8g1i6", "https://robohash.org/hieu.nguyen@demo.local", false, true, "Nguyen", "", "0900000000", 2, "Active", "SEAL037", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000038"), "Demo address", "https://robohash.org/quynh.pham@demo.local", null, null, "Demo user", "SEAL University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "quynh.pham@demo.local", "Quynh", "$2a$11$ELUlXu.C3Yh0miS3.dAZaO17ER/stLENq.EWnMYmPBiwZ14X8g1i6", "https://robohash.org/quynh.pham@demo.local", false, true, "Pham", "", "0900000000", 2, "Active", "SEAL038", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000039"), "Demo address", "https://robohash.org/nam.hoang@demo.local", null, null, "Demo user", "SEAL University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "nam.hoang@demo.local", "Nam", "$2a$11$ELUlXu.C3Yh0miS3.dAZaO17ER/stLENq.EWnMYmPBiwZ14X8g1i6", "https://robohash.org/nam.hoang@demo.local", false, true, "Hoang", "", "0900000000", 2, "Active", "SEAL039", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000040"), "Demo address", "https://robohash.org/khoa.le@demo.local", null, null, "Demo user", "SEAL University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "khoa.le@demo.local", "Khoa", "$2a$11$ELUlXu.C3Yh0miS3.dAZaO17ER/stLENq.EWnMYmPBiwZ14X8g1i6", "https://robohash.org/khoa.le@demo.local", false, true, "Le", "", "0900000000", 2, "Active", "SEAL040", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-0000-0000-000000000041"), "Demo address", "https://robohash.org/thu.tran@demo.local", null, null, "Demo user", "SEAL University", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "thu.tran@demo.local", "Thu", "$2a$11$ELUlXu.C3Yh0miS3.dAZaO17ER/stLENq.EWnMYmPBiwZ14X8g1i6", "https://robohash.org/thu.tran@demo.local", false, true, "Tran", "", "0900000000", 2, "Active", "SEAL041", new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "RegisterTeams",
                columns: new[] { "Id", "CreatedAt", "Description", "EventId", "IsBanned", "IsDisable", "RejectionReason", "Status", "TeamId", "TopicId", "TrackId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("31000000-0000-0000-0000-000000000020"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "AI Mavericks registration for SEAL Hackathon 2026", new Guid("20000000-0000-0000-0000-000000000001"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000020"), new Guid("25000000-0000-0000-0000-000000000001"), new Guid("24000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("31000000-0000-0000-0000-000000000021"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Eco Guardians registration for SEAL Hackathon 2026", new Guid("20000000-0000-0000-0000-000000000001"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000021"), new Guid("25000000-0000-0000-0000-000000000002"), new Guid("24000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("31000000-0000-0000-0000-000000000022"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Code Visionaries registration for SEAL Hackathon 2026", new Guid("20000000-0000-0000-0000-000000000001"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000022"), new Guid("25000000-0000-0000-0000-000000000001"), new Guid("24000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("31000000-0000-0000-0000-000000000023"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "GreenTech Solutions registration for SEAL Hackathon 2026", new Guid("20000000-0000-0000-0000-000000000001"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000023"), new Guid("25000000-0000-0000-0000-000000000002"), new Guid("24000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("31000000-0000-0000-0000-000000000024"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "AI Builders registration for SEAL Hackathon 2026", new Guid("20000000-0000-0000-0000-000000000001"), false, false, null, "Approved", new Guid("30000000-0000-0000-0000-000000000024"), new Guid("25000000-0000-0000-0000-000000000001"), new Guid("24000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "TeamDetails",
                columns: new[] { "Id", "CreatedAt", "IsDisable", "IsLeader", "Status", "TeamId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("30100000-0000-0000-0000-000000000020"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000020"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000030") },
                    { new Guid("30100000-0000-0000-0000-000000000021"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Active", new Guid("30000000-0000-0000-0000-000000000020"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000031") },
                    { new Guid("30100000-0000-0000-0000-000000000022"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000021"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000032") },
                    { new Guid("30100000-0000-0000-0000-000000000023"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Active", new Guid("30000000-0000-0000-0000-000000000021"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000033") },
                    { new Guid("30100000-0000-0000-0000-000000000024"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Active", new Guid("30000000-0000-0000-0000-000000000021"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000034") },
                    { new Guid("30100000-0000-0000-0000-000000000025"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000022"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000035") },
                    { new Guid("30100000-0000-0000-0000-000000000026"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Active", new Guid("30000000-0000-0000-0000-000000000022"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000036") },
                    { new Guid("30100000-0000-0000-0000-000000000027"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000023"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000037") },
                    { new Guid("30100000-0000-0000-0000-000000000028"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Active", new Guid("30000000-0000-0000-0000-000000000023"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000038") },
                    { new Guid("30100000-0000-0000-0000-000000000029"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Active", new Guid("30000000-0000-0000-0000-000000000023"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000039") },
                    { new Guid("30100000-0000-0000-0000-000000000030"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, true, "Active", new Guid("30000000-0000-0000-0000-000000000024"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000040") },
                    { new Guid("30100000-0000-0000-0000-000000000031"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "Active", new Guid("30000000-0000-0000-0000-000000000024"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("10000000-0000-0000-0000-000000000041") }
                });

            migrationBuilder.InsertData(
                table: "RoundDetails",
                columns: new[] { "Id", "CreatedAt", "IsDisable", "RegisterTeamId", "RoundId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("32000000-0000-0000-0000-000000000020"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000020"), new Guid("21000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000021"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000021"), new Guid("21000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000022"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000022"), new Guid("21000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000023"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000023"), new Guid("21000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("32000000-0000-0000-0000-000000000024"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, new Guid("31000000-0000-0000-0000-000000000024"), new Guid("21000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "RoundDetails",
                keyColumn: "Id",
                keyValue: new Guid("32000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "TeamDetails",
                keyColumn: "Id",
                keyValue: new Guid("30100000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000036"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000038"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000040"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000041"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000024"));

            migrationBuilder.UpdateData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000001"),
                column: "EndSubmission",
                value: new DateTimeOffset(new DateTime(2026, 6, 21, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
