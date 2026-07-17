using System;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class SubmissionSeed
{
    // Submission IDs
    public static readonly Guid Sub1 = Guid.Parse("33000000-0000-0000-0000-000000000001");
    public static readonly Guid Sub2 = Guid.Parse("33000000-0000-0000-0000-000000000002");
    public static readonly Guid Sub3 = Guid.Parse("33000000-0000-0000-0000-000000000003");
    public static readonly Guid Sub4 = Guid.Parse("33000000-0000-0000-0000-000000000004");
    public static readonly Guid Sub5 = Guid.Parse("33000000-0000-0000-0000-000000000005");
    public static readonly Guid Sub6 = Guid.Parse("33000000-0000-0000-0000-000000000006");
    public static readonly Guid Sub7 = Guid.Parse("33000000-0000-0000-0000-000000000007");
    public static readonly Guid Sub8 = Guid.Parse("33000000-0000-0000-0000-000000000008");
    public static readonly Guid Sub9 = Guid.Parse("33000000-0000-0000-0000-000000000009");
    public static readonly Guid Sub10 = Guid.Parse("33000000-0000-0000-0000-000000000010");
    public static readonly Guid Sub11 = Guid.Parse("33000000-0000-0000-0000-000000000011");
    public static readonly Guid Sub12 = Guid.Parse("33000000-0000-0000-0000-000000000012");

    public static void SeedSubmissions(this ModelBuilder modelBuilder)
    {
        // 12 Submissions with different statuses and options
        modelBuilder.Entity<Submissions>().HasData(
            new Submissions { Id = Sub1, RoundDetailId = RoundDetailSeed.Rd1, Url = "https://github.com/test/project1", Description = "Idea Proposal Doc", Status = SubmissionStatusEnum.Submitted, SubmittedAt = SeedConstants.CreatedAt.AddDays(12), IsRegrade = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Submissions { Id = Sub2, RoundDetailId = RoundDetailSeed.Rd2, Url = "https://github.com/test/project2", Description = "Web App Architecture", Status = SubmissionStatusEnum.Submitted, SubmittedAt = SeedConstants.CreatedAt.AddDays(12), IsRegrade = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Submissions { Id = Sub3, RoundDetailId = RoundDetailSeed.Rd3, Url = "https://github.com/test/project3", Description = "Mobile App Design", Status = SubmissionStatusEnum.Submitted, SubmittedAt = SeedConstants.CreatedAt.AddDays(12), IsRegrade = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Submissions { Id = Sub4, RoundDetailId = RoundDetailSeed.Rd4, Url = "https://github.com/test/project4", Description = "IoT Prototype Blueprint", Status = SubmissionStatusEnum.Submitted, SubmittedAt = SeedConstants.CreatedAt.AddDays(12), IsRegrade = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }, // regrade requested
            new Submissions { Id = Sub5, RoundDetailId = RoundDetailSeed.Rd5, Url = null, Description = "Cloud Deployment Draft", Status = SubmissionStatusEnum.Submitted, SubmittedAt = null, IsRegrade = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }, // not yet submitted
            new Submissions { Id = Sub6, RoundDetailId = RoundDetailSeed.Rd6, Url = "https://github.com/test/project1-v2", Description = "Beta Prototype Code", Status = SubmissionStatusEnum.Submitted, SubmittedAt = SeedConstants.CreatedAt.AddDays(13), IsRegrade = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Submissions { Id = Sub7, RoundDetailId = RoundDetailSeed.Rd7, Url = "https://github.com/test/project2-v2", Description = "Beta Web Code", Status = SubmissionStatusEnum.Submitted, SubmittedAt = SeedConstants.CreatedAt.AddDays(13), IsRegrade = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Submissions { Id = Sub8, RoundDetailId = RoundDetailSeed.Rd8, Url = "https://github.com/test/project-single", Description = "Final submission for single round", Status = SubmissionStatusEnum.Submitted, SubmittedAt = SeedConstants.CreatedAt.AddDays(12), IsRegrade = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Submissions { Id = Sub9, RoundDetailId = RoundDetailSeed.Rd9, Url = "https://github.com/test/project-summer-1", Description = "Summer Round 1 doc", Status = SubmissionStatusEnum.Submitted, SubmittedAt = SeedConstants.CreatedAt.AddDays(12), IsRegrade = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Submissions { Id = Sub10, RoundDetailId = RoundDetailSeed.Rd10, Url = "https://github.com/test/project-autumn-1", Description = "Autumn Round 1 doc", Status = SubmissionStatusEnum.Submitted, SubmittedAt = SeedConstants.CreatedAt.AddDays(12), IsRegrade = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Submissions { Id = Sub11, RoundDetailId = RoundDetailSeed.Rd11, Url = "https://github.com/test/project-autumn-2", Description = "Autumn Round 2 code", Status = SubmissionStatusEnum.Failed, SubmittedAt = SeedConstants.CreatedAt.AddDays(12), IsRegrade = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }, // failed submission
            new Submissions { Id = Sub12, RoundDetailId = RoundDetailSeed.Rd12, Url = null, Description = "Winter Round 1 blank", Status = SubmissionStatusEnum.Submitted, SubmittedAt = null, IsRegrade = false, IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt } // disabled submission
        );
    }
}
