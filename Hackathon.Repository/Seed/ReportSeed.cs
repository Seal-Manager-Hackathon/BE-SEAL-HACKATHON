using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class ReportSeed
{
    public static void SeedReports(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reports>().HasData(new Reports
        {
            Id = Guid.Parse("73000000-0000-0000-0000-000000000001"),
            UserId = SeedConstants.JudgeUserId,
            AssignEventId = SeedConstants.JudgeAssignEventId,
            SubmissionId = SeedConstants.GreenCodersFinalSubmissionId,
            Title = "Seed submission report",
            Description = "Seed report for final submission",
            ImgUrl = "https://seed.local/reports/image.png",
            FileUrl = "https://seed.local/reports/file.pdf",
            Status = "Open",
            Reason = "Seed review reason",
            TypeReport = "Submission",
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        });
    }
}
