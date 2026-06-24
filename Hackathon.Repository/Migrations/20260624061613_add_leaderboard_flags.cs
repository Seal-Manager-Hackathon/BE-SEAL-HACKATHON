using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon.Repository.Migrations
{
    /// <inheritdoc />
    public partial class add_leaderboard_flags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "LeaderBoards",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "LeaderBoards",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000001"),
                columns: new[] { "IsLocked", "IsPublished" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000010"),
                columns: new[] { "IsLocked", "IsPublished" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000011"),
                columns: new[] { "IsLocked", "IsPublished" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000012"),
                columns: new[] { "IsLocked", "IsPublished" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000013"),
                columns: new[] { "IsLocked", "IsPublished" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000014"),
                columns: new[] { "IsLocked", "IsPublished" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000015"),
                columns: new[] { "IsLocked", "IsPublished" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000016"),
                columns: new[] { "IsLocked", "IsPublished" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000017"),
                columns: new[] { "IsLocked", "IsPublished" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000018"),
                columns: new[] { "IsLocked", "IsPublished" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "LeaderBoards",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000019"),
                columns: new[] { "IsLocked", "IsPublished" },
                values: new object[] { false, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "LeaderBoards");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "LeaderBoards");
        }
    }
}
