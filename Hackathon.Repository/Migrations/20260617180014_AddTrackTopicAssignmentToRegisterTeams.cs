using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddTrackTopicAssignmentToRegisterTeams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
