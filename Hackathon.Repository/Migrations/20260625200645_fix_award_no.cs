using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon.Repository.Migrations
{
    /// <inheritdoc />
    public partial class fix_award_no : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Chuyển các giá trị text cũ thành chuỗi số trước khi ép kiểu
            migrationBuilder.Sql("UPDATE \"LeaderBoardDetails\" SET \"LevelAward\" = '1' WHERE \"LevelAward\" = 'First';");
            migrationBuilder.Sql("UPDATE \"LeaderBoardDetails\" SET \"LevelAward\" = '2' WHERE \"LevelAward\" = 'Second';");

            migrationBuilder.Sql("UPDATE \"Awards\" SET \"LevelAward\" = '1' WHERE \"LevelAward\" = 'First';");
            migrationBuilder.Sql("UPDATE \"Awards\" SET \"LevelAward\" = '2' WHERE \"LevelAward\" = 'Second';");

            // 2. Ép kiểu cột bằng cách dùng USING trong PostgreSQL
            migrationBuilder.Sql("ALTER TABLE \"LeaderBoardDetails\" ALTER COLUMN \"LevelAward\" TYPE integer USING \"LevelAward\"::integer;");
            migrationBuilder.Sql("ALTER TABLE \"Awards\" ALTER COLUMN \"LevelAward\" TYPE integer USING \"LevelAward\"::integer;");

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000001"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000002"),
                column: "LevelAward",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000010"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000011"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000012"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000013"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000014"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000015"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000016"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000017"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000018"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000019"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000001"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000002"),
                column: "LevelAward",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000010"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000011"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000012"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000013"),
                column: "LevelAward",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000014"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000015"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000016"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000017"),
                column: "LevelAward",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000018"),
                column: "LevelAward",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000019"),
                column: "LevelAward",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LevelAward",
                table: "LeaderBoardDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LevelAward",
                table: "Awards",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000001"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000002"),
                column: "LevelAward",
                value: "Second");

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000010"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000011"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000012"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000013"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000014"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000015"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000016"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000017"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000018"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: new Guid("26000000-0000-0000-0000-000000000019"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000001"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000002"),
                column: "LevelAward",
                value: "Second");

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000010"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000011"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000012"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000013"),
                column: "LevelAward",
                value: "Second");

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000014"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000015"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000016"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000017"),
                column: "LevelAward",
                value: "Second");

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000018"),
                column: "LevelAward",
                value: "First");

            migrationBuilder.UpdateData(
                table: "LeaderBoardDetails",
                keyColumn: "Id",
                keyValue: new Guid("61000000-0000-0000-0000-000000000019"),
                column: "LevelAward",
                value: "First");
        }
    }
}
