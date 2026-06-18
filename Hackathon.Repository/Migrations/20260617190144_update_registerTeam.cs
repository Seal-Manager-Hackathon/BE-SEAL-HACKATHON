using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon.Repository.Migrations
{
    /// <inheritdoc />
    public partial class update_registerTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateOfBirth",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "College",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TopicId",
                table: "RegisterTeams",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TrackId",
                table: "RegisterTeams",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000001"),
                columns: new[] { "TopicId", "TrackId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000002"),
                columns: new[] { "TopicId", "TrackId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000010"),
                columns: new[] { "TopicId", "TrackId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000011"),
                columns: new[] { "TopicId", "TrackId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000012"),
                columns: new[] { "TopicId", "TrackId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000013"),
                columns: new[] { "TopicId", "TrackId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000014"),
                columns: new[] { "TopicId", "TrackId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000015"),
                columns: new[] { "TopicId", "TrackId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000016"),
                columns: new[] { "TopicId", "TrackId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000017"),
                columns: new[] { "TopicId", "TrackId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000018"),
                columns: new[] { "TopicId", "TrackId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RegisterTeams",
                keyColumn: "Id",
                keyValue: new Guid("31000000-0000-0000-0000-000000000019"),
                columns: new[] { "TopicId", "TrackId" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_RegisterTeams_TopicId",
                table: "RegisterTeams",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterTeams_TrackId",
                table: "RegisterTeams",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterTeams_Topics_TopicId",
                table: "RegisterTeams",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterTeams_Tracks_TrackId",
                table: "RegisterTeams",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterTeams_Topics_TopicId",
                table: "RegisterTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_RegisterTeams_Tracks_TrackId",
                table: "RegisterTeams");

            migrationBuilder.DropIndex(
                name: "IX_RegisterTeams_TopicId",
                table: "RegisterTeams");

            migrationBuilder.DropIndex(
                name: "IX_RegisterTeams_TrackId",
                table: "RegisterTeams");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "RegisterTeams");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "RegisterTeams");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateOfBirth",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "College",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
