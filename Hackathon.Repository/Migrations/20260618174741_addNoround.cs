using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addNoround : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "RoundNo",
                table: "Rounds",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000001"),
                column: "RoundNo",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000002"),
                column: "RoundNo",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000010"),
                column: "RoundNo",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000011"),
                column: "RoundNo",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000012"),
                column: "RoundNo",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000013"),
                column: "RoundNo",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000014"),
                column: "RoundNo",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000015"),
                column: "RoundNo",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000016"),
                column: "RoundNo",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000017"),
                column: "RoundNo",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000018"),
                column: "RoundNo",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: new Guid("21000000-0000-0000-0000-000000000019"),
                column: "RoundNo",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoundNo",
                table: "Rounds");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
