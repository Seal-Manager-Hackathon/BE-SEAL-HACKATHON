using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class SubmissionSeed
{
    // 30 Submission IDs
    public static readonly Guid Sub1  = Guid.Parse("33000000-0000-0000-0000-000000000001");
    public static readonly Guid Sub2  = Guid.Parse("33000000-0000-0000-0000-000000000002");
    public static readonly Guid Sub3  = Guid.Parse("33000000-0000-0000-0000-000000000003");
    public static readonly Guid Sub4  = Guid.Parse("33000000-0000-0000-0000-000000000004");
    public static readonly Guid Sub5  = Guid.Parse("33000000-0000-0000-0000-000000000005");
    public static readonly Guid Sub6  = Guid.Parse("33000000-0000-0000-0000-000000000006");
    public static readonly Guid Sub7  = Guid.Parse("33000000-0000-0000-0000-000000000007");
    public static readonly Guid Sub8  = Guid.Parse("33000000-0000-0000-0000-000000000008");
    public static readonly Guid Sub9  = Guid.Parse("33000000-0000-0000-0000-000000000009");
    public static readonly Guid Sub10 = Guid.Parse("33000000-0000-0000-0000-000000000010");
    public static readonly Guid Sub11 = Guid.Parse("33000000-0000-0000-0000-000000000011");
    public static readonly Guid Sub12 = Guid.Parse("33000000-0000-0000-0000-000000000012");
    public static readonly Guid Sub13 = Guid.Parse("33000000-0000-0000-0000-000000000013");
    public static readonly Guid Sub14 = Guid.Parse("33000000-0000-0000-0000-000000000014");
    public static readonly Guid Sub15 = Guid.Parse("33000000-0000-0000-0000-000000000015");
    public static readonly Guid Sub16 = Guid.Parse("33000000-0000-0000-0000-000000000016");
    public static readonly Guid Sub17 = Guid.Parse("33000000-0000-0000-0000-000000000017");
    public static readonly Guid Sub18 = Guid.Parse("33000000-0000-0000-0000-000000000018");
    public static readonly Guid Sub19 = Guid.Parse("33000000-0000-0000-0000-000000000019");
    public static readonly Guid Sub20 = Guid.Parse("33000000-0000-0000-0000-000000000020");
    public static readonly Guid Sub21 = Guid.Parse("33000000-0000-0000-0000-000000000021");
    public static readonly Guid Sub22 = Guid.Parse("33000000-0000-0000-0000-000000000022");
    public static readonly Guid Sub23 = Guid.Parse("33000000-0000-0000-0000-000000000023");
    public static readonly Guid Sub24 = Guid.Parse("33000000-0000-0000-0000-000000000024");
    public static readonly Guid Sub25 = Guid.Parse("33000000-0000-0000-0000-000000000025");
    public static readonly Guid Sub26 = Guid.Parse("33000000-0000-0000-0000-000000000026");
    public static readonly Guid Sub27 = Guid.Parse("33000000-0000-0000-0000-000000000027");
    public static readonly Guid Sub28 = Guid.Parse("33000000-0000-0000-0000-000000000028");
    public static readonly Guid Sub29 = Guid.Parse("33000000-0000-0000-0000-000000000029");
    public static readonly Guid Sub30 = Guid.Parse("33000000-0000-0000-0000-000000000030");

    public static void SeedSubmissions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Submissions>().HasData(
            // Event 2 R1 - 5 teams (3 graded, 1 submitted, 1 regrade)
            Create(Sub1,  RoundDetailSeed.Rd1, "https://github.com/team1-e2r1", "E2R1 - AI Proposal",       SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(12), false),
            Create(Sub2,  RoundDetailSeed.Rd2, "https://github.com/team2-e2r1", "E2R1 - Web Architecture",  SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(12), false),
            Create(Sub3,  RoundDetailSeed.Rd3, "https://github.com/team3-e2r1", "E2R1 - Mobile Design",     SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(12), false),
            Create(Sub4,  RoundDetailSeed.Rd4, "https://github.com/team4-e2r1", "E2R1 - IoT Blueprint",     SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(12), true),
            Create(Sub5,  RoundDetailSeed.Rd5, null,                             "E2R1 - Cloud Draft",       SubmissionStatusEnum.Submitted, null, false),
            // Event 2 R2 - 3 teams (all graded)
            Create(Sub6,  RoundDetailSeed.Rd6, "https://github.com/team1-e2r2", "E2R2 - Final AI",          SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(14), false),
            Create(Sub7,  RoundDetailSeed.Rd7, "https://github.com/team2-e2r2", "E2R2 - Final Web",         SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(14), false),
            Create(Sub8,  RoundDetailSeed.Rd8, "https://github.com/team3-e2r2", "E2R2 - Final Mobile",      SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(14), false),
            // Event 3 R1 - 2 teams (graded + failed)
            Create(Sub9,  RoundDetailSeed.Rd9, "https://github.com/team21-e3r1","E3R1 - AI Legacy",         SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(12), false),
            Create(Sub10, RoundDetailSeed.Rd10, "https://github.com/team22-e3r1","E3R1 - Web Legacy",       SubmissionStatusEnum.Failed,   SeedConstants.CreatedAt.AddDays(12), false),
            // Event 4 R1 - 5 teams (mix)
            Create(Sub11, RoundDetailSeed.Rd11, "https://github.com/team6-e4r1", "E4R1 - Security Tool",    SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(12), false),
            Create(Sub12, RoundDetailSeed.Rd12, "https://github.com/team7-e4r1", "E4R1 - Blockchain",       SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(12), false),
            Create(Sub13, RoundDetailSeed.Rd13, "https://github.com/team8-e4r1", "E4R1 - Game",             SubmissionStatusEnum.Submitted, null, false),
            Create(Sub14, RoundDetailSeed.Rd14, null,                             "E4R1 - Data (rejected)",  SubmissionStatusEnum.Submitted, null, false),
            Create(Sub15, RoundDetailSeed.Rd15, "https://github.com/team23-e4r1","E4R1 - Disabled team",     SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(12), false),
            // Event 7 R1 - 4 teams
            Create(Sub16, RoundDetailSeed.Rd16, "https://github.com/team11-e7r1","E7R1 - Security AI",      SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(12), false),
            Create(Sub17, RoundDetailSeed.Rd17, "https://github.com/team12-e7r1","E7R1 - Web Auction",      SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(12), false),
            Create(Sub18, RoundDetailSeed.Rd18, "https://github.com/team13-e7r1","E7R1 - Game (banned)",    SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(12), false),
            Create(Sub19, RoundDetailSeed.Rd19, "https://github.com/team15-e7r1","E7R1 - Cloud Monitor",     SubmissionStatusEnum.Submitted, null, false),
            // Event 7 R2 - 2 teams
            Create(Sub20, RoundDetailSeed.Rd20, "https://github.com/team11-e7r2","E7R2 - Security Final",    SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(14), false),
            Create(Sub21, RoundDetailSeed.Rd21, "https://github.com/team12-e7r2","E7R2 - Auction Final",     SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(14), false),
            // Event 10 R1 - 4 teams
            Create(Sub22, RoundDetailSeed.Rd22, "https://github.com/team16-e10r1","E10R1 - Cyber",           SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(12), false),
            Create(Sub23, RoundDetailSeed.Rd23, "https://github.com/team17-e10r1","E10R1 - Wallet",          SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(12), false),
            Create(Sub24, RoundDetailSeed.Rd24, null,                             "E10R1 - K8s (pending)",   SubmissionStatusEnum.Submitted, null, false),
            Create(Sub25, RoundDetailSeed.Rd25, "https://github.com/team25-e10r1","E10R1 - Extra (banned)",  SubmissionStatusEnum.Submitted, null, false),
            // Edge cases
            Create(Sub26, RoundDetailSeed.Rd26, "https://github.com/team1-e6r1", "E6R1 - Recycled team",    SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(12), false),
            Create(Sub27, RoundDetailSeed.Rd27, "https://github.com/team24-e2r2","E2R2 - Extra team",        SubmissionStatusEnum.Graded,   SeedConstants.CreatedAt.AddDays(14), false),
            Create(Sub28, RoundDetailSeed.Rd28, null,                             "E2R1 - Inactive team",    SubmissionStatusEnum.Submitted, null, false),
            // Disabled submissions
            new Submissions { Id = Sub29, RoundDetailId = RoundDetailSeed.Rd1, Url = null, Description = "Draft submission", Status = SubmissionStatusEnum.Submitted, SubmittedAt = null, IsRegrade = false, IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Submissions { Id = Sub30, RoundDetailId = RoundDetailSeed.Rd22, Url = "https://github.com/old", Description = "Old draft", Status = SubmissionStatusEnum.Failed, SubmittedAt = SeedConstants.CreatedAt.AddDays(10), IsRegrade = false, IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );
    }

    private static Submissions Create(Guid id, Guid roundDetailId, string? url, string desc, SubmissionStatusEnum status, DateTimeOffset? submittedAt, bool isRegrade)
        => new() { Id = id, RoundDetailId = roundDetailId, Url = url, Description = desc, Status = status, SubmittedAt = submittedAt, IsRegrade = isRegrade, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };
}
