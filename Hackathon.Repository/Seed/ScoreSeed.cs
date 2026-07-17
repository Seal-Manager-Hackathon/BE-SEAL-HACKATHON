using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class ScoreSeed
{
    // 22 Score IDs
    public static readonly Guid Score1  = Guid.Parse("50000000-0000-0000-0000-000000000001");
    public static readonly Guid Score2  = Guid.Parse("50000000-0000-0000-0000-000000000002");
    public static readonly Guid Score3  = Guid.Parse("50000000-0000-0000-0000-000000000003");
    public static readonly Guid Score4  = Guid.Parse("50000000-0000-0000-0000-000000000004");
    public static readonly Guid Score5  = Guid.Parse("50000000-0000-0000-0000-000000000005");
    public static readonly Guid Score6  = Guid.Parse("50000000-0000-0000-0000-000000000006");
    public static readonly Guid Score7  = Guid.Parse("50000000-0000-0000-0000-000000000007");
    public static readonly Guid Score8  = Guid.Parse("50000000-0000-0000-0000-000000000008");
    public static readonly Guid Score9  = Guid.Parse("50000000-0000-0000-0000-000000000009");
    public static readonly Guid Score10 = Guid.Parse("50000000-0000-0000-0000-000000000010");
    public static readonly Guid Score11 = Guid.Parse("50000000-0000-0000-0000-000000000011");
    public static readonly Guid Score12 = Guid.Parse("50000000-0000-0000-0000-000000000012");
    public static readonly Guid Score13 = Guid.Parse("50000000-0000-0000-0000-000000000013");
    public static readonly Guid Score14 = Guid.Parse("50000000-0000-0000-0000-000000000014");
    public static readonly Guid Score15 = Guid.Parse("50000000-0000-0000-0000-000000000015");
    public static readonly Guid Score16 = Guid.Parse("50000000-0000-0000-0000-000000000016");
    public static readonly Guid Score17 = Guid.Parse("50000000-0000-0000-0000-000000000017");
    public static readonly Guid Score18 = Guid.Parse("50000000-0000-0000-0000-000000000018");
    public static readonly Guid Score19 = Guid.Parse("50000000-0000-0000-0000-000000000019");
    public static readonly Guid Score20 = Guid.Parse("50000000-0000-0000-0000-000000000020");
    public static readonly Guid Score21 = Guid.Parse("50000000-0000-0000-0000-000000000021");
    public static readonly Guid Score22 = Guid.Parse("50000000-0000-0000-0000-000000000022");

    // Score Items IDs (50+)
    public static readonly Guid Si1  = Guid.Parse("51000000-0000-0000-0000-000000000001");
    public static readonly Guid Si2  = Guid.Parse("51000000-0000-0000-0000-000000000002");
    public static readonly Guid Si3  = Guid.Parse("51000000-0000-0000-0000-000000000003");
    public static readonly Guid Si4  = Guid.Parse("51000000-0000-0000-0000-000000000004");
    public static readonly Guid Si5  = Guid.Parse("51000000-0000-0000-0000-000000000005");
    public static readonly Guid Si6  = Guid.Parse("51000000-0000-0000-0000-000000000006");
    public static readonly Guid Si7  = Guid.Parse("51000000-0000-0000-0000-000000000007");
    public static readonly Guid Si8  = Guid.Parse("51000000-0000-0000-0000-000000000008");
    public static readonly Guid Si9  = Guid.Parse("51000000-0000-0000-0000-000000000009");
    public static readonly Guid Si10 = Guid.Parse("51000000-0000-0000-0000-000000000010");
    public static readonly Guid Si11 = Guid.Parse("51000000-0000-0000-0000-000000000011");
    public static readonly Guid Si12 = Guid.Parse("51000000-0000-0000-0000-000000000012");
    public static readonly Guid Si13 = Guid.Parse("51000000-0000-0000-0000-000000000013");
    public static readonly Guid Si14 = Guid.Parse("51000000-0000-0000-0000-000000000014");
    public static readonly Guid Si15 = Guid.Parse("51000000-0000-0000-0000-000000000015");
    public static readonly Guid Si16 = Guid.Parse("51000000-0000-0000-0000-000000000016");
    public static readonly Guid Si17 = Guid.Parse("51000000-0000-0000-0000-000000000017");
    public static readonly Guid Si18 = Guid.Parse("51000000-0000-0000-0000-000000000018");
    public static readonly Guid Si19 = Guid.Parse("51000000-0000-0000-0000-000000000019");
    public static readonly Guid Si20 = Guid.Parse("51000000-0000-0000-0000-000000000020");
    public static readonly Guid Si21 = Guid.Parse("51000000-0000-0000-0000-000000000021");
    public static readonly Guid Si22 = Guid.Parse("51000000-0000-0000-0000-000000000022");
    public static readonly Guid Si23 = Guid.Parse("51000000-0000-0000-0000-000000000023");
    public static readonly Guid Si24 = Guid.Parse("51000000-0000-0000-0000-000000000024");
    public static readonly Guid Si25 = Guid.Parse("51000000-0000-0000-0000-000000000025");
    public static readonly Guid Si26 = Guid.Parse("51000000-0000-0000-0000-000000000026");
    public static readonly Guid Si27 = Guid.Parse("51000000-0000-0000-0000-000000000027");
    public static readonly Guid Si28 = Guid.Parse("51000000-0000-0000-0000-000000000028");
    public static readonly Guid Si29 = Guid.Parse("51000000-0000-0000-0000-000000000029");
    public static readonly Guid Si30 = Guid.Parse("51000000-0000-0000-0000-000000000030");
    public static readonly Guid Si31 = Guid.Parse("51000000-0000-0000-0000-000000000031");
    public static readonly Guid Si32 = Guid.Parse("51000000-0000-0000-0000-000000000032");
    public static readonly Guid Si33 = Guid.Parse("51000000-0000-0000-0000-000000000033");
    public static readonly Guid Si34 = Guid.Parse("51000000-0000-0000-0000-000000000034");
    public static readonly Guid Si35 = Guid.Parse("51000000-0000-0000-0000-000000000035");
    public static readonly Guid Si36 = Guid.Parse("51000000-0000-0000-0000-000000000036");
    public static readonly Guid Si37 = Guid.Parse("51000000-0000-0000-0000-000000000037");
    public static readonly Guid Si38 = Guid.Parse("51000000-0000-0000-0000-000000000038");
    public static readonly Guid Si39 = Guid.Parse("51000000-0000-0000-0000-000000000039");
    public static readonly Guid Si40 = Guid.Parse("51000000-0000-0000-0000-000000000040");
    public static readonly Guid Si41 = Guid.Parse("51000000-0000-0000-0000-000000000041");
    public static readonly Guid Si42 = Guid.Parse("51000000-0000-0000-0000-000000000042");
    public static readonly Guid Si43 = Guid.Parse("51000000-0000-0000-0000-000000000043");
    public static readonly Guid Si44 = Guid.Parse("51000000-0000-0000-0000-000000000044");
    public static readonly Guid Si45 = Guid.Parse("51000000-0000-0000-0000-000000000045");
    public static readonly Guid Si46 = Guid.Parse("51000000-0000-0000-0000-000000000046");
    public static readonly Guid Si47 = Guid.Parse("51000000-0000-0000-0000-000000000047");
    public static readonly Guid Si48 = Guid.Parse("51000000-0000-0000-0000-000000000048");
    public static readonly Guid Si49 = Guid.Parse("51000000-0000-0000-0000-000000000049");
    public static readonly Guid Si50 = Guid.Parse("51000000-0000-0000-0000-000000000050");

    public static void SeedScores(this ModelBuilder modelBuilder)
    {
        // Scores
        modelBuilder.Entity<Scores>().HasData(
            // Event 2 R1 (judge At1 = Event2/Track1Ai)
            Create(Score1,  SubmissionSeed.Sub1,  AssignmentSeed.At1, 85.0m,  false, null, false),
            Create(Score2,  SubmissionSeed.Sub2,  AssignmentSeed.At2, 90.0m,  false, null, false),
            Create(Score3,  SubmissionSeed.Sub3,  AssignmentSeed.At7, 75.0m,  false, null, false),
            Create(Score4,  SubmissionSeed.Sub4,  AssignmentSeed.At20, 60.0m, false, null, false),
            // Score5 = retake of Score4 (Submission Sub4 has IsRegrade=true)
            Create(Score5,  SubmissionSeed.Sub4,  AssignmentSeed.At20, 80.0m, true,  Score4,  false),
            // Event 2 R2
            Create(Score6,  SubmissionSeed.Sub6,  AssignmentSeed.At1,  95.0m,  false, null, false),
            Create(Score7,  SubmissionSeed.Sub7,  AssignmentSeed.At2,  88.0m,  false, null, false),
            Create(Score8,  SubmissionSeed.Sub8,  AssignmentSeed.At7,  82.0m,  false, null, false),
            // Event 3 R1
            Create(Score9,  SubmissionSeed.Sub9,  AssignmentSeed.At6,  70.0m,  false, null, false),
            Create(Score10, SubmissionSeed.Sub10, AssignmentSeed.At8,  45.0m,  false, null, false),
            // Event 4 R1
            Create(Score11, SubmissionSeed.Sub11, AssignmentSeed.At3,  78.0m,  false, null, false),
            Create(Score12, SubmissionSeed.Sub12, AssignmentSeed.At10, 65.0m,  false, null, false),
            Create(Score13, SubmissionSeed.Sub15, AssignmentSeed.At3,  55.0m,  false, null, false),
            // Event 7 R1
            Create(Score14, SubmissionSeed.Sub16, AssignmentSeed.At4,  88.0m,  false, null, false),
            Create(Score15, SubmissionSeed.Sub17, AssignmentSeed.At22, 72.0m,  false, null, false),
            Create(Score16, SubmissionSeed.Sub18, AssignmentSeed.At25, 50.0m,  false, null, false),
            // Event 7 R2
            Create(Score17, SubmissionSeed.Sub20, AssignmentSeed.At4,  92.0m,  false, null, false),
            Create(Score18, SubmissionSeed.Sub21, AssignmentSeed.At22, 76.0m,  false, null, false),
            // Event 10 R1
            Create(Score19, SubmissionSeed.Sub22, AssignmentSeed.At5,  84.0m,  false, null, false),
            Create(Score20, SubmissionSeed.Sub23, AssignmentSeed.At23, 68.0m,  false, null, false),
            // Edge cases: Mock score + Extra
            Create(Score21, SubmissionSeed.Sub1,  AssignmentSeed.At1,  85.0m,  false, null, true),
            Create(Score22, SubmissionSeed.Sub27, AssignmentSeed.At1,  91.0m,  false, null, false)
        );

        // Score Items (2 per score = 44 items for active scores)
        modelBuilder.Entity<ScoreItems>().HasData(
            // Score1 (Total: 85) - using CriteriaSeed Item1, Item2
            Create(Si1,  Score1,  CriteriaSeed.Item1,  AssignmentSeed.At1, 45m, "Sáng tạo tốt"),
            Create(Si2,  Score1,  CriteriaSeed.Item2,  AssignmentSeed.At1, 40m, "Khả thi cao"),
            // Score2 (Total: 90)
            Create(Si3,  Score2,  CriteriaSeed.Item1,  AssignmentSeed.At2, 48m, "Tuyệt vời"),
            Create(Si4,  Score2,  CriteriaSeed.Item2,  AssignmentSeed.At2, 42m, "Khả thi"),
            // Score3 (Total: 75)
            Create(Si5,  Score3,  CriteriaSeed.Item1,  AssignmentSeed.At7, 38m, "Khá"),
            Create(Si6,  Score3,  CriteriaSeed.Item2,  AssignmentSeed.At7, 37m, "Trung bình"),
            // Score4 (Total: 60)
            Create(Si7,  Score4,  CriteriaSeed.Item1,  AssignmentSeed.At20, 30m, "Cần cải thiện"),
            Create(Si8,  Score4,  CriteriaSeed.Item2,  AssignmentSeed.At20, 30m, "Sơ sài"),
            // Score5 retake (Total: 80)
            Create(Si9,  Score5,  CriteriaSeed.Item1,  AssignmentSeed.At20, 42m, "Cải thiện tốt"),
            Create(Si10, Score5,  CriteriaSeed.Item2,  AssignmentSeed.At20, 38m, "Khả thi hơn"),
            // Score6 (Total: 95)
            Create(Si11, Score6,  CriteriaSeed.Item1,  AssignmentSeed.At1,  48m, "Xuất sắc"),
            Create(Si12, Score6,  CriteriaSeed.Item2,  AssignmentSeed.At1,  47m, "Hoàn hảo"),
            // Score7 (Total: 88)
            Create(Si13, Score7,  CriteriaSeed.Item1,  AssignmentSeed.At2,  45m, "Rất tốt"),
            Create(Si14, Score7,  CriteriaSeed.Item2,  AssignmentSeed.At2,  43m, "Tốt"),
            // Score8 (Total: 82)
            Create(Si15, Score8,  CriteriaSeed.Item1,  AssignmentSeed.At7,  42m, "Khá tốt"),
            Create(Si16, Score8,  CriteriaSeed.Item2,  AssignmentSeed.At7,  40m, "Đạt"),
            // Score9 (Total: 70)
            Create(Si17, Score9,  CriteriaSeed.Item1,  AssignmentSeed.At6,  35m, "Bình thường"),
            Create(Si18, Score9,  CriteriaSeed.Item2,  AssignmentSeed.At6,  35m, "Ổn"),
            // Score10 (Total: 45)
            Create(Si19, Score10, CriteriaSeed.Item1,  AssignmentSeed.At8,  25m, "Yếu"),
            Create(Si20, Score10, CriteriaSeed.Item2,  AssignmentSeed.At8,  20m, "Không đạt"),
            // Score11 (Total: 78)
            Create(Si21, Score11, CriteriaSeed.Item7,  AssignmentSeed.At3,  38m, "Ý tưởng hay"),
            Create(Si22, Score11, CriteriaSeed.Item8,  AssignmentSeed.At3,  40m, "Tác động tốt"),
            // Score12 (Total: 65)
            Create(Si23, Score12, CriteriaSeed.Item7,  AssignmentSeed.At10, 30m, "Trung bình"),
            Create(Si24, Score12, CriteriaSeed.Item8,  AssignmentSeed.At10, 35m, "Ổn"),
            // Score13 (Total: 55)
            Create(Si25, Score13, CriteriaSeed.Item7,  AssignmentSeed.At3,  25m, "Kém"),
            Create(Si26, Score13, CriteriaSeed.Item8,  AssignmentSeed.At3,  30m, "Yếu"),
            // Score14 (Total: 88)
            Create(Si27, Score14, CriteriaSeed.Item15, AssignmentSeed.At4,  45m, "Video chất lượng"),
            Create(Si28, Score14, CriteriaSeed.Item16, AssignmentSeed.At4,  43m, "Thuyết phục"),
            // Score15 (Total: 72)
            Create(Si29, Score15, CriteriaSeed.Item15, AssignmentSeed.At22, 37m, "Bình thường"),
            Create(Si30, Score15, CriteriaSeed.Item16, AssignmentSeed.At22, 35m, "Tạm được"),
            // Score16 (Total: 50)
            Create(Si31, Score16, CriteriaSeed.Item15, AssignmentSeed.At25, 25m, "Kém chất lượng"),
            Create(Si32, Score16, CriteriaSeed.Item16, AssignmentSeed.At25, 25m, "Không thuyết phục"),
            // Score17 (Total: 92)
            Create(Si33, Score17, CriteriaSeed.Item17, AssignmentSeed.At4,  64m, "Sản phẩm tốt"),
            Create(Si34, Score17, CriteriaSeed.Item18, AssignmentSeed.At4,  28m, "Teamwork tốt"),
            // Score18 (Total: 76)
            Create(Si35, Score18, CriteriaSeed.Item17, AssignmentSeed.At22, 50m, "Trung bình"),
            Create(Si36, Score18, CriteriaSeed.Item18, AssignmentSeed.At22, 26m, "Khá"),
            // Score19 (Total: 84)
            Create(Si37, Score19, CriteriaSeed.Item19, AssignmentSeed.At5,  42m, "Hồ sơ tốt"),
            Create(Si38, Score19, CriteriaSeed.Item20, AssignmentSeed.At5,  42m, "Kinh nghiệm"),
            // Score20 (Total: 68)
            Create(Si39, Score20, CriteriaSeed.Item19, AssignmentSeed.At23, 34m, "Trung bình"),
            Create(Si40, Score20, CriteriaSeed.Item20, AssignmentSeed.At23, 34m, "Cần thêm"),
            // Score21 Mock
            Create(Si41, Score21, CriteriaSeed.Item1,  AssignmentSeed.At1,  43m, "Mock score item 1"),
            Create(Si42, Score21, CriteriaSeed.Item2,  AssignmentSeed.At1,  42m, "Mock score item 2"),
            // Score22
            Create(Si43, Score22, CriteriaSeed.Item1,  AssignmentSeed.At1,  46m, "Extra score"),
            Create(Si44, Score22, CriteriaSeed.Item2,  AssignmentSeed.At1,  45m, "Extra score 2"),
            // Disabled score items
            Create(Si45, Score1,  CriteriaSeed.Item1,  AssignmentSeed.At1,  0m,  "Disabled", true),
            Create(Si46, Score2,  CriteriaSeed.Item2,  AssignmentSeed.At2,  0m,  "Disabled", true),
            Create(Si47, Score6,  CriteriaSeed.Item1,  AssignmentSeed.At1,  0m,  "Disabled", true),
            Create(Si48, Score11, CriteriaSeed.Item7,  AssignmentSeed.At3,  0m,  "Disabled", true),
            Create(Si49, Score14, CriteriaSeed.Item15, AssignmentSeed.At4,  0m,  "Disabled", true),
            Create(Si50, Score19, CriteriaSeed.Item19, AssignmentSeed.At5,  0m,  "Disabled", true)
        );
    }

    private static Scores Create(Guid id, Guid submissionId, Guid assignTrackId, decimal total, bool isRetake, Guid? retakeFrom, bool isMock)
        => new() { Id = id, SubmissionId = submissionId, AssignTrackId = assignTrackId, IsRetake = isRetake, RetakeFromScoreId = retakeFrom, TotalScore = total, IsMock = isMock, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };

    private static ScoreItems Create(Guid id, Guid scoreId, Guid criteriaItemId, Guid assignTrackId, decimal score, string comment, bool isDisable = false)
        => new() { Id = id, ScoreId = scoreId, CriteriaItemId = criteriaItemId, AssignTrackId = assignTrackId, Score = score, Comment = comment, IsDisable = isDisable, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };
}
