using System;
using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class ScoreSeed
{
    // Score IDs
    public static readonly Guid Score1 = Guid.Parse("50000000-0000-0000-0000-000000000001");
    public static readonly Guid Score2 = Guid.Parse("50000000-0000-0000-0000-000000000002");
    public static readonly Guid Score3 = Guid.Parse("50000000-0000-0000-0000-000000000003");
    public static readonly Guid Score4 = Guid.Parse("50000000-0000-0000-0000-000000000004");
    public static readonly Guid Score5Retake = Guid.Parse("50000000-0000-0000-0000-000000000005");
    public static readonly Guid Score6 = Guid.Parse("50000000-0000-0000-0000-000000000006");
    public static readonly Guid Score7 = Guid.Parse("50000000-0000-0000-0000-000000000007");
    public static readonly Guid Score8 = Guid.Parse("50000000-0000-0000-0000-000000000008");
    public static readonly Guid Score9 = Guid.Parse("50000000-0000-0000-0000-000000000009");
    public static readonly Guid Score10 = Guid.Parse("50000000-0000-0000-0000-000000000010");
    public static readonly Guid Score11 = Guid.Parse("50000000-0000-0000-0000-000000000011");
    public static readonly Guid Score12Mock = Guid.Parse("50000000-0000-0000-0000-000000000012");

    public static void SeedScores(this ModelBuilder modelBuilder)
    {
        // 12 Scores
        modelBuilder.Entity<Scores>().HasData(
            new Scores { Id = Score1, SubmissionId = SubmissionSeed.Sub1, AssignTrackId = AssignmentSeed.At1, IsRetake = false, RetakeFromScoreId = null, TotalScore = 85.0m, IsMock = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Scores { Id = Score2, SubmissionId = SubmissionSeed.Sub2, AssignTrackId = AssignmentSeed.At2, IsRetake = false, RetakeFromScoreId = null, TotalScore = 90.0m, IsMock = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Scores { Id = Score3, SubmissionId = SubmissionSeed.Sub3, AssignTrackId = AssignmentSeed.At3, IsRetake = false, RetakeFromScoreId = null, TotalScore = 75.0m, IsMock = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Scores { Id = Score4, SubmissionId = SubmissionSeed.Sub4, AssignTrackId = AssignmentSeed.At4, IsRetake = false, RetakeFromScoreId = null, TotalScore = 60.0m, IsMock = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Scores { Id = Score5Retake, SubmissionId = SubmissionSeed.Sub4, AssignTrackId = AssignmentSeed.At4, IsRetake = true, RetakeFromScoreId = Score4, TotalScore = 80.0m, IsMock = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }, // retake of Score4
            new Scores { Id = Score6, SubmissionId = SubmissionSeed.Sub6, AssignTrackId = AssignmentSeed.At1, IsRetake = false, RetakeFromScoreId = null, TotalScore = 95.0m, IsMock = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Scores { Id = Score7, SubmissionId = SubmissionSeed.Sub7, AssignTrackId = AssignmentSeed.At2, IsRetake = false, RetakeFromScoreId = null, TotalScore = 88.0m, IsMock = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Scores { Id = Score8, SubmissionId = SubmissionSeed.Sub8, AssignTrackId = AssignmentSeed.At1, IsRetake = false, RetakeFromScoreId = null, TotalScore = 70.0m, IsMock = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Scores { Id = Score9, SubmissionId = SubmissionSeed.Sub9, AssignTrackId = AssignmentSeed.At1, IsRetake = false, RetakeFromScoreId = null, TotalScore = 65.0m, IsMock = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Scores { Id = Score10, SubmissionId = SubmissionSeed.Sub10, AssignTrackId = AssignmentSeed.At8, IsRetake = false, RetakeFromScoreId = null, TotalScore = 78.0m, IsMock = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Scores { Id = Score11, SubmissionId = SubmissionSeed.Sub11, AssignTrackId = AssignmentSeed.At8, IsRetake = false, RetakeFromScoreId = null, TotalScore = 45.0m, IsMock = false, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Scores { Id = Score12Mock, SubmissionId = SubmissionSeed.Sub1, AssignTrackId = AssignmentSeed.At1, IsRetake = false, RetakeFromScoreId = null, TotalScore = 85.0m, IsMock = true, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt } // mock score
        );

        // Score Items (grading individual criteria items)
        modelBuilder.Entity<ScoreItems>().HasData(
            // Score 1 (Total: 85): Template 1 Items (Item1: 30, Item2: 30, Item3: 20, Item4: 20)
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000001"), ScoreId = Score1, CriteriaItemId = CriteriaSeed.Item1, AssignTrackId = AssignmentSeed.At1, Score = 25m, Comment = "Rất sáng tạo", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000002"), ScoreId = Score1, CriteriaItemId = CriteriaSeed.Item2, AssignTrackId = AssignmentSeed.At1, Score = 25m, Comment = "Khả thi cao", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000003"), ScoreId = Score1, CriteriaItemId = CriteriaSeed.Item3, AssignTrackId = AssignmentSeed.At1, Score = 18m, Comment = "Ý nghĩa", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000004"), ScoreId = Score1, CriteriaItemId = CriteriaSeed.Item4, AssignTrackId = AssignmentSeed.At1, Score = 17m, Comment = "Tài liệu tốt", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },

            // Score 2 (Total: 90)
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000005"), ScoreId = Score2, CriteriaItemId = CriteriaSeed.Item1, AssignTrackId = AssignmentSeed.At2, Score = 28m, Comment = "Tuyệt vời", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000006"), ScoreId = Score2, CriteriaItemId = CriteriaSeed.Item2, AssignTrackId = AssignmentSeed.At2, Score = 27m, Comment = "Khả thi", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000007"), ScoreId = Score2, CriteriaItemId = CriteriaSeed.Item3, AssignTrackId = AssignmentSeed.At2, Score = 18m, Comment = "Giá trị cao", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000008"), ScoreId = Score2, CriteriaItemId = CriteriaSeed.Item4, AssignTrackId = AssignmentSeed.At2, Score = 17m, Comment = "Đầy đủ", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },

            // Score 3 (Total: 75)
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000009"), ScoreId = Score3, CriteriaItemId = CriteriaSeed.Item1, AssignTrackId = AssignmentSeed.At3, Score = 22m, Comment = "Khá tốt", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000010"), ScoreId = Score3, CriteriaItemId = CriteriaSeed.Item2, AssignTrackId = AssignmentSeed.At3, Score = 23m, Comment = "Được", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000011"), ScoreId = Score3, CriteriaItemId = CriteriaSeed.Item3, AssignTrackId = AssignmentSeed.At3, Score = 15m, Comment = "Bình thường", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000012"), ScoreId = Score3, CriteriaItemId = CriteriaSeed.Item4, AssignTrackId = AssignmentSeed.At3, Score = 15m, Comment = "Cần thêm chi tiết", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },

            // Score 4 (Total: 60)
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000013"), ScoreId = Score4, CriteriaItemId = CriteriaSeed.Item1, AssignTrackId = AssignmentSeed.At4, Score = 18m, Comment = "Trung bình", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000014"), ScoreId = Score4, CriteriaItemId = CriteriaSeed.Item2, AssignTrackId = AssignmentSeed.At4, Score = 17m, Comment = "Hơi sơ sài", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000015"), ScoreId = Score4, CriteriaItemId = CriteriaSeed.Item3, AssignTrackId = AssignmentSeed.At4, Score = 13m, Comment = "Ít tác động", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000016"), ScoreId = Score4, CriteriaItemId = CriteriaSeed.Item4, AssignTrackId = AssignmentSeed.At4, Score = 12m, Comment = "Sơ sài", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },

            // Score 5 Retake (Total: 80)
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000017"), ScoreId = Score5Retake, CriteriaItemId = CriteriaSeed.Item1, AssignTrackId = AssignmentSeed.At4, Score = 24m, Comment = "Cải thiện tốt", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000018"), ScoreId = Score5Retake, CriteriaItemId = CriteriaSeed.Item2, AssignTrackId = AssignmentSeed.At4, Score = 24m, Comment = "Đã khả thi hơn", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000019"), ScoreId = Score5Retake, CriteriaItemId = CriteriaSeed.Item3, AssignTrackId = AssignmentSeed.At4, Score = 16m, Comment = "Tốt", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new ScoreItems { Id = Guid.Parse("51000000-0000-0000-0000-000000000020"), ScoreId = Score5Retake, CriteriaItemId = CriteriaSeed.Item4, AssignTrackId = AssignmentSeed.At4, Score = 16m, Comment = "Đã bổ sung đầy đủ", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );
    }
}
