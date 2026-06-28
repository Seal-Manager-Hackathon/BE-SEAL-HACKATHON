using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddRetakeFromScoreId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RetakeFromScoreId",
                table: "Scores",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "RetakeFromScoreId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "RetakeFromScoreId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "RetakeFromScoreId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "RetakeFromScoreId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_RetakeFromScoreId",
                table: "Scores",
                column: "RetakeFromScoreId",
                unique: true,
                filter: "\"IsDisable\" = false AND \"RetakeFromScoreId\" IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Scores_RetakeFromScoreId",
                table: "Scores",
                column: "RetakeFromScoreId",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Scores_RetakeFromScoreId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_RetakeFromScoreId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "RetakeFromScoreId",
                table: "Scores");
        }
    }
}
