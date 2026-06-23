using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon.Repository.Migrations
{
    /// <inheritdoc />
    public partial class make_event_role_id_nullable_in_assign_events : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignEvents_EventRoles_EventRoleId",
                table: "AssignEvents");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventRoleId",
                table: "AssignEvents",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignEvents_EventRoles_EventRoleId",
                table: "AssignEvents",
                column: "EventRoleId",
                principalTable: "EventRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignEvents_EventRoles_EventRoleId",
                table: "AssignEvents");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventRoleId",
                table: "AssignEvents",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignEvents_EventRoles_EventRoleId",
                table: "AssignEvents",
                column: "EventRoleId",
                principalTable: "EventRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
