using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

// Submission IDs: 32000000-xxxx
public static class SubmissionSeed
{
    public static readonly Guid Sub1 = Guid.Parse("32000000-0000-0000-0000-000000000001");
    public static readonly Guid Sub2 = Guid.Parse("32000000-0000-0000-0000-000000000002");
    public static readonly Guid Sub3 = Guid.Parse("32000000-0000-0000-0000-000000000003");
    public static readonly Guid Sub4 = Guid.Parse("32000000-0000-0000-0000-000000000004");
    public static readonly Guid Sub5 = Guid.Parse("32000000-0000-0000-0000-000000000005");
    public static readonly Guid Sub6 = Guid.Parse("32000000-0000-0000-0000-000000000006");
    public static readonly Guid Sub7 = Guid.Parse("32000000-0000-0000-0000-000000000007");
    public static readonly Guid Sub8 = Guid.Parse("32000000-0000-0000-0000-000000000008");
    public static readonly Guid Sub9 = Guid.Parse("32000000-0000-0000-0000-000000000009");
    public static readonly Guid Sub10 = Guid.Parse("32000000-0000-0000-0000-000000000010");
    public static readonly Guid Sub11 = Guid.Parse("32000000-0000-0000-0000-000000000011");
    public static readonly Guid Sub12 = Guid.Parse("32000000-0000-0000-0000-000000000012");
    public static readonly Guid Sub13 = Guid.Parse("32000000-0000-0000-0000-000000000013");
    public static readonly Guid Sub14 = Guid.Parse("32000000-0000-0000-0000-000000000014");
    public static readonly Guid Sub15 = Guid.Parse("32000000-0000-0000-0000-000000000015");
    public static readonly Guid Sub16 = Guid.Parse("32000000-0000-0000-0000-000000000016");
    public static readonly Guid Sub17 = Guid.Parse("32000000-0000-0000-0000-000000000017");
    public static readonly Guid Sub18 = Guid.Parse("32000000-0000-0000-0000-000000000018");
    public static readonly Guid Sub19 = Guid.Parse("32000000-0000-0000-0000-000000000019");
    public static readonly Guid Sub20 = Guid.Parse("32000000-0000-0000-0000-000000000020");
    public static readonly Guid Sub21 = Guid.Parse("32000000-0000-0000-0000-000000000021");
    public static readonly Guid Sub22 = Guid.Parse("32000000-0000-0000-0000-000000000022");
    public static readonly Guid Sub23 = Guid.Parse("32000000-0000-0000-0000-000000000023");
    public static readonly Guid Sub24 = Guid.Parse("32000000-0000-0000-0000-000000000024");
    public static readonly Guid Sub25 = Guid.Parse("32000000-0000-0000-0000-000000000025");
    public static readonly Guid Sub26 = Guid.Parse("32000000-0000-0000-0000-000000000026");
    public static readonly Guid Sub27 = Guid.Parse("32000000-0000-0000-0000-000000000027");
    public static readonly Guid Sub28 = Guid.Parse("32000000-0000-0000-0000-000000000028");
    public static readonly Guid Sub29 = Guid.Parse("32000000-0000-0000-0000-000000000029");
    public static readonly Guid Sub30 = Guid.Parse("32000000-0000-0000-0000-000000000030");

    public static void SeedSubmissions(this ModelBuilder modelBuilder)
    {
        var c = SeedConstants.CreatedAt;

        modelBuilder.Entity<Submissions>().HasData(
            // E2 Submissions
            Create(Sub1, RoundDetailSeed.Rd1, SubmissionStatusEnum.Graded, c.AddDays(11)),
            Create(Sub2, RoundDetailSeed.Rd2, SubmissionStatusEnum.Graded, c.AddDays(11)),
            Create(Sub3, RoundDetailSeed.Rd3, SubmissionStatusEnum.Graded, c.AddDays(12)),
            Create(Sub4, RoundDetailSeed.Rd4, SubmissionStatusEnum.Graded, c.AddDays(19)),
            Create(Sub5, RoundDetailSeed.Rd5, SubmissionStatusEnum.Graded, c.AddDays(19)),
            Create(Sub6, RoundDetailSeed.Rd6, SubmissionStatusEnum.Graded, c.AddDays(26)),
            Create(Sub7, RoundDetailSeed.Rd7, SubmissionStatusEnum.Graded, c.AddDays(26)),
            // E3 Submissions (closed event)
            Create(Sub8, RoundDetailSeed.Rd8, SubmissionStatusEnum.Graded, c.AddDays(-29)),
            Create(Sub9, RoundDetailSeed.Rd9, SubmissionStatusEnum.Graded, c.AddDays(-29)),
            Create(Sub10, RoundDetailSeed.Rd10, SubmissionStatusEnum.Graded, c.AddDays(-28)),
            Create(Sub11, RoundDetailSeed.Rd11, SubmissionStatusEnum.Graded, c.AddDays(-21)),
            Create(Sub12, RoundDetailSeed.Rd12, SubmissionStatusEnum.Graded, c.AddDays(-21)),
            Create(Sub13, RoundDetailSeed.Rd13, SubmissionStatusEnum.Graded, c.AddDays(-14)),
            // E4 Submissions
            Create(Sub14, RoundDetailSeed.Rd14, SubmissionStatusEnum.Graded, c.AddDays(11)),
            Create(Sub15, RoundDetailSeed.Rd15, SubmissionStatusEnum.Graded, c.AddDays(11)),
            Create(Sub16, RoundDetailSeed.Rd16, SubmissionStatusEnum.Graded, c.AddDays(12)),
            Create(Sub17, RoundDetailSeed.Rd17, SubmissionStatusEnum.Submitted, c.AddDays(19)),
            Create(Sub18, RoundDetailSeed.Rd18, SubmissionStatusEnum.Submitted, c.AddDays(19)),
            Create(Sub19, RoundDetailSeed.Rd19, SubmissionStatusEnum.Graded, c.AddDays(26)),
            // E6 Submissions (closed)
            Create(Sub20, RoundDetailSeed.Rd20, SubmissionStatusEnum.Graded, c.AddDays(-59)),
            Create(Sub21, RoundDetailSeed.Rd21, SubmissionStatusEnum.Graded, c.AddDays(-59)),
            Create(Sub22, RoundDetailSeed.Rd22, SubmissionStatusEnum.Graded, c.AddDays(-58)),
            Create(Sub23, RoundDetailSeed.Rd23, SubmissionStatusEnum.Graded, c.AddDays(-51)),
            Create(Sub24, RoundDetailSeed.Rd24, SubmissionStatusEnum.Graded, c.AddDays(-51)),
            // E7 Submissions
            Create(Sub25, RoundDetailSeed.Rd25, SubmissionStatusEnum.Graded, c.AddDays(11)),
            Create(Sub26, RoundDetailSeed.Rd26, SubmissionStatusEnum.Graded, c.AddDays(12)),
            Create(Sub27, RoundDetailSeed.Rd27, SubmissionStatusEnum.Graded, c.AddDays(19)),
            Create(Sub28, RoundDetailSeed.Rd28, SubmissionStatusEnum.Submitted, c.AddDays(26)),
            // E9 Submissions (closed)
            Create(Sub29, RoundDetailSeed.Rd29, SubmissionStatusEnum.Graded, c.AddDays(-89)),
            Create(Sub30, RoundDetailSeed.Rd30, SubmissionStatusEnum.Failed, c.AddDays(-89))
        );
    }

    private static Submissions Create(Guid id, Guid roundDetailId, SubmissionStatusEnum status, DateTimeOffset submittedAt) => new()
    {
        Id = id, RoundDetailId = roundDetailId,
        Url = $"https://drive.google.com/hackathon/submission/{id}",
        Description = $"Submission for round detail {roundDetailId}",
        Status = status, IsRegrade = false,
        SubmittedAt = status != SubmissionStatusEnum.Failed ? submittedAt : null,
        IsDisable = false, CreatedAt = submittedAt, UpdatedAt = submittedAt
    };
}
