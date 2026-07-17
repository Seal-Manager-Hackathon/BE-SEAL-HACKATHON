using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToReferenceLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AssignEvents_AssignEventId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Submissions_SubmissionId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_AssignEventId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_SubmissionId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "AssignEventId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "SubmissionId",
                table: "Reports",
                newName: "AssignEventsId");

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                columns: new[] { "AssignEventsId", "Status" },
                values: new object[] { null, "Pending" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                columns: new[] { "AssignEventsId", "Status" },
                values: new object[] { null, "Resolved" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                columns: new[] { "AssignEventsId", "Status" },
                values: new object[] { null, "Resolved" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                columns: new[] { "AssignEventsId", "Status" },
                values: new object[] { null, "Pending" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                columns: new[] { "AssignEventsId", "Status" },
                values: new object[] { null, "Pending" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                columns: new[] { "AssignEventsId", "Status" },
                values: new object[] { null, "Reject" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                columns: new[] { "AssignEventsId", "Status" },
                values: new object[] { null, "Pending" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                columns: new[] { "AssignEventsId", "Status" },
                values: new object[] { null, "Resolved" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                columns: new[] { "AssignEventsId", "Status" },
                values: new object[] { null, "Pending" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                columns: new[] { "AssignEventsId", "Status" },
                values: new object[] { null, "Pending" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                columns: new[] { "AssignEventsId", "Status" },
                values: new object[] { null, "Pending" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000012"),
                column: "Status",
                value: "Pending");

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000005"),
                column: "Status",
                value: "Submitted");

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000012"),
                column: "Status",
                value: "Submitted");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AssignEventsId",
                table: "Reports",
                column: "AssignEventsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AssignEvents_AssignEventsId",
                table: "Reports",
                column: "AssignEventsId",
                principalTable: "AssignEvents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AssignEvents_AssignEventsId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_AssignEventsId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "AssignEventsId",
                table: "Reports",
                newName: "SubmissionId");

            migrationBuilder.AddColumn<Guid>(
                name: "AssignEventId",
                table: "Reports",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "Reports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Reports",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                columns: new[] { "AssignEventId", "FileUrl", "ImgUrl", "Status", "SubmissionId" },
                values: new object[] { new Guid("40000000-0000-0000-0000-000000000003"), "https://test/err.log", "https://test/err.png", "Open", new Guid("33000000-0000-0000-0000-000000000001") });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                columns: new[] { "AssignEventId", "FileUrl", "ImgUrl", "Status", "SubmissionId" },
                values: new object[] { new Guid("40000000-0000-0000-0000-000000000003"), null, null, "Closed", new Guid("33000000-0000-0000-0000-000000000002") });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                columns: new[] { "AssignEventId", "FileUrl", "ImgUrl", "Status", "SubmissionId" },
                values: new object[] { new Guid("40000000-0000-0000-0000-000000000001"), null, "https://test/plagiarism.png", "Approved", new Guid("33000000-0000-0000-0000-000000000003") });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                columns: new[] { "AssignEventId", "FileUrl", "ImgUrl", "Status", "SubmissionId" },
                values: new object[] { new Guid("40000000-0000-0000-0000-000000000002"), null, null, "Open", new Guid("33000000-0000-0000-0000-000000000004") });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                columns: new[] { "AssignEventId", "FileUrl", "ImgUrl", "Status", "SubmissionId" },
                values: new object[] { new Guid("40000000-0000-0000-0000-000000000006"), null, null, "Open", new Guid("33000000-0000-0000-0000-000000000005") });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                columns: new[] { "AssignEventId", "FileUrl", "ImgUrl", "Status", "SubmissionId" },
                values: new object[] { new Guid("40000000-0000-0000-0000-000000000010"), null, null, "Closed", new Guid("33000000-0000-0000-0000-000000000006") });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                columns: new[] { "AssignEventId", "FileUrl", "ImgUrl", "Status", "SubmissionId" },
                values: new object[] { new Guid("40000000-0000-0000-0000-000000000003"), null, null, "Open", new Guid("33000000-0000-0000-0000-000000000007") });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                columns: new[] { "AssignEventId", "FileUrl", "ImgUrl", "Status", "SubmissionId" },
                values: new object[] { new Guid("40000000-0000-0000-0000-000000000003"), null, null, "Approved", new Guid("33000000-0000-0000-0000-000000000008") });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                columns: new[] { "AssignEventId", "FileUrl", "ImgUrl", "Status", "SubmissionId" },
                values: new object[] { new Guid("40000000-0000-0000-0000-000000000004"), null, null, "Open", new Guid("33000000-0000-0000-0000-000000000009") });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                columns: new[] { "AssignEventId", "FileUrl", "ImgUrl", "Status", "SubmissionId" },
                values: new object[] { new Guid("40000000-0000-0000-0000-000000000005"), null, null, "Open", new Guid("33000000-0000-0000-0000-000000000010") });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                columns: new[] { "AssignEventId", "FileUrl", "ImgUrl", "Status", "SubmissionId" },
                values: new object[] { new Guid("40000000-0000-0000-0000-000000000003"), null, null, "Open", new Guid("33000000-0000-0000-0000-000000000011") });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000012"),
                columns: new[] { "AssignEventId", "FileUrl", "ImgUrl", "Status" },
                values: new object[] { null, null, null, "Open" });

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000005"),
                column: "Status",
                value: "Unsubmitted");

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "Id",
                keyValue: new Guid("33000000-0000-0000-0000-000000000012"),
                column: "Status",
                value: "Unsubmitted");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AssignEventId",
                table: "Reports",
                column: "AssignEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_SubmissionId",
                table: "Reports",
                column: "SubmissionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AssignEvents_AssignEventId",
                table: "Reports",
                column: "AssignEventId",
                principalTable: "AssignEvents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Submissions_SubmissionId",
                table: "Reports",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id");
        }
    }
}
