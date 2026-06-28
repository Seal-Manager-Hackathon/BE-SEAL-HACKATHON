using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddIsRegradeAndApprovedStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRegrade",
                table: "Submissions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000001"),
                column: "IsRegrade",
                value: false);

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000002"),
                column: "IsRegrade",
                value: false);

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000003"),
                column: "IsRegrade",
                value: false);

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000004"),
                column: "IsRegrade",
                value: false);

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000010"),
                column: "IsRegrade",
                value: false);

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000011"),
                column: "IsRegrade",
                value: false);

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000012"),
                column: "IsRegrade",
                value: false);

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000013"),
                column: "IsRegrade",
                value: false);

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000014"),
                column: "IsRegrade",
                value: false);

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000015"),
                column: "IsRegrade",
                value: false);

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000016"),
                column: "IsRegrade",
                value: false);

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000017"),
                column: "IsRegrade",
                value: false);

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000018"),
                column: "IsRegrade",
                value: false);

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000019"),
                column: "IsRegrade",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRegrade",
                table: "Submissions");
        }
    }
}
