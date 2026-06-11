using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class SubmissionSeed
{
    public static void SeedSubmissions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Submissions>().HasData(
            CreateSubmission(SeedConstants.SeedInnovatorsIdeaSubmissionId, SeedConstants.SeedInnovatorsIdeaRoundDetailId, "https://seed.local/submissions/seed-innovators-idea"),
            CreateSubmission(SeedConstants.SeedInnovatorsFinalSubmissionId, SeedConstants.SeedInnovatorsFinalRoundDetailId, "https://seed.local/submissions/seed-innovators-final"),
            CreateSubmission(SeedConstants.GreenCodersIdeaSubmissionId, SeedConstants.GreenCodersIdeaRoundDetailId, "https://seed.local/submissions/green-coders-idea"),
            CreateSubmission(SeedConstants.GreenCodersFinalSubmissionId, SeedConstants.GreenCodersFinalRoundDetailId, "https://seed.local/submissions/green-coders-final")
        );
    }

    private static Submissions CreateSubmission(Guid id, Guid roundDetailId, string url)
    {
        return new Submissions
        {
            Id = id,
            RoundDetailId = roundDetailId,
            Url = url,
            Description = "Seed submission",
            Status = "Submitted",
            SubmittedAt = SeedConstants.CreatedAt.AddDays(10),
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }
}
