using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

// Score IDs: 35000000-xxxx, ScoreItem IDs: 36000000-xxxx
public static class ScoreSeed
{
    public static readonly Guid Score1 = Guid.Parse("35000000-0000-0000-0000-000000000001");
    public static readonly Guid Score2 = Guid.Parse("35000000-0000-0000-0000-000000000002");
    public static readonly Guid Score3 = Guid.Parse("35000000-0000-0000-0000-000000000003");
    public static readonly Guid Score4 = Guid.Parse("35000000-0000-0000-0000-000000000004");
    public static readonly Guid Score5 = Guid.Parse("35000000-0000-0000-0000-000000000005");
    public static readonly Guid Score6 = Guid.Parse("35000000-0000-0000-0000-000000000006");
    public static readonly Guid Score7 = Guid.Parse("35000000-0000-0000-0000-000000000007");
    public static readonly Guid Score8 = Guid.Parse("35000000-0000-0000-0000-000000000008");
    public static readonly Guid Score9 = Guid.Parse("35000000-0000-0000-0000-000000000009");
    public static readonly Guid Score10 = Guid.Parse("35000000-0000-0000-0000-000000000010");
    public static readonly Guid Score11 = Guid.Parse("35000000-0000-0000-0000-000000000011");
    public static readonly Guid Score12 = Guid.Parse("35000000-0000-0000-0000-000000000012");
    public static readonly Guid Score13 = Guid.Parse("35000000-0000-0000-0000-000000000013");
    public static readonly Guid Score14 = Guid.Parse("35000000-0000-0000-0000-000000000014");
    public static readonly Guid Score15 = Guid.Parse("35000000-0000-0000-0000-000000000015");
    public static readonly Guid Score16 = Guid.Parse("35000000-0000-0000-0000-000000000016");
    public static readonly Guid Score17 = Guid.Parse("35000000-0000-0000-0000-000000000017");
    public static readonly Guid Score18 = Guid.Parse("35000000-0000-0000-0000-000000000018");
    public static readonly Guid Score19 = Guid.Parse("35000000-0000-0000-0000-000000000019");
    public static readonly Guid Score20 = Guid.Parse("35000000-0000-0000-0000-000000000020");
    public static readonly Guid Score21 = Guid.Parse("35000000-0000-0000-0000-000000000021");
    public static readonly Guid Score22 = Guid.Parse("35000000-0000-0000-0000-000000000022");
    public static readonly Guid Score23 = Guid.Parse("35000000-0000-0000-0000-000000000023");
    public static readonly Guid Score24 = Guid.Parse("35000000-0000-0000-0000-000000000024");
    public static readonly Guid Score25 = Guid.Parse("35000000-0000-0000-0000-000000000025");
    public static readonly Guid Score26 = Guid.Parse("35000000-0000-0000-0000-000000000026");
    public static readonly Guid Score27 = Guid.Parse("35000000-0000-0000-0000-000000000027");
    public static readonly Guid Score28 = Guid.Parse("35000000-0000-0000-0000-000000000028");
    public static readonly Guid Score29 = Guid.Parse("35000000-0000-0000-0000-000000000029");
    public static readonly Guid Score30 = Guid.Parse("35000000-0000-0000-0000-000000000030");

    public static readonly Guid Si1 = Guid.Parse("36000000-0000-0000-0000-000000000001");
    public static readonly Guid Si2 = Guid.Parse("36000000-0000-0000-0000-000000000002");
    public static readonly Guid Si3 = Guid.Parse("36000000-0000-0000-0000-000000000003");
    public static readonly Guid Si4 = Guid.Parse("36000000-0000-0000-0000-000000000004");
    public static readonly Guid Si5 = Guid.Parse("36000000-0000-0000-0000-000000000005");
    public static readonly Guid Si6 = Guid.Parse("36000000-0000-0000-0000-000000000006");
    public static readonly Guid Si7 = Guid.Parse("36000000-0000-0000-0000-000000000007");
    public static readonly Guid Si8 = Guid.Parse("36000000-0000-0000-0000-000000000008");
    public static readonly Guid Si9 = Guid.Parse("36000000-0000-0000-0000-000000000009");
    public static readonly Guid Si10 = Guid.Parse("36000000-0000-0000-0000-000000000010");
    public static readonly Guid Si11 = Guid.Parse("36000000-0000-0000-0000-000000000011");
    public static readonly Guid Si12 = Guid.Parse("36000000-0000-0000-0000-000000000012");
    public static readonly Guid Si13 = Guid.Parse("36000000-0000-0000-0000-000000000013");
    public static readonly Guid Si14 = Guid.Parse("36000000-0000-0000-0000-000000000014");
    public static readonly Guid Si15 = Guid.Parse("36000000-0000-0000-0000-000000000015");
    public static readonly Guid Si16 = Guid.Parse("36000000-0000-0000-0000-000000000016");
    public static readonly Guid Si17 = Guid.Parse("36000000-0000-0000-0000-000000000017");
    public static readonly Guid Si18 = Guid.Parse("36000000-0000-0000-0000-000000000018");
    public static readonly Guid Si19 = Guid.Parse("36000000-0000-0000-0000-000000000019");
    public static readonly Guid Si20 = Guid.Parse("36000000-0000-0000-0000-000000000020");
    public static readonly Guid Si21 = Guid.Parse("36000000-0000-0000-0000-000000000021");
    public static readonly Guid Si22 = Guid.Parse("36000000-0000-0000-0000-000000000022");
    public static readonly Guid Si23 = Guid.Parse("36000000-0000-0000-0000-000000000023");
    public static readonly Guid Si24 = Guid.Parse("36000000-0000-0000-0000-000000000024");
    public static readonly Guid Si25 = Guid.Parse("36000000-0000-0000-0000-000000000025");
    public static readonly Guid Si26 = Guid.Parse("36000000-0000-0000-0000-000000000026");
    public static readonly Guid Si27 = Guid.Parse("36000000-0000-0000-0000-000000000027");
    public static readonly Guid Si28 = Guid.Parse("36000000-0000-0000-0000-000000000028");
    public static readonly Guid Si29 = Guid.Parse("36000000-0000-0000-0000-000000000029");
    public static readonly Guid Si30 = Guid.Parse("36000000-0000-0000-0000-000000000030");
    public static readonly Guid Si31 = Guid.Parse("36000000-0000-0000-0000-000000000031");
    public static readonly Guid Si32 = Guid.Parse("36000000-0000-0000-0000-000000000032");
    public static readonly Guid Si33 = Guid.Parse("36000000-0000-0000-0000-000000000033");
    public static readonly Guid Si34 = Guid.Parse("36000000-0000-0000-0000-000000000034");
    public static readonly Guid Si35 = Guid.Parse("36000000-0000-0000-0000-000000000035");
    public static readonly Guid Si36 = Guid.Parse("36000000-0000-0000-0000-000000000036");
    public static readonly Guid Si37 = Guid.Parse("36000000-0000-0000-0000-000000000037");
    public static readonly Guid Si38 = Guid.Parse("36000000-0000-0000-0000-000000000038");
    public static readonly Guid Si39 = Guid.Parse("36000000-0000-0000-0000-000000000039");
    public static readonly Guid Si40 = Guid.Parse("36000000-0000-0000-0000-000000000040");
    public static readonly Guid Si41 = Guid.Parse("36000000-0000-0000-0000-000000000041");
    public static readonly Guid Si42 = Guid.Parse("36000000-0000-0000-0000-000000000042");
    public static readonly Guid Si43 = Guid.Parse("36000000-0000-0000-0000-000000000043");
    public static readonly Guid Si44 = Guid.Parse("36000000-0000-0000-0000-000000000044");
    public static readonly Guid Si45 = Guid.Parse("36000000-0000-0000-0000-000000000045");
    public static readonly Guid Si46 = Guid.Parse("36000000-0000-0000-0000-000000000046");
    public static readonly Guid Si47 = Guid.Parse("36000000-0000-0000-0000-000000000047");
    public static readonly Guid Si48 = Guid.Parse("36000000-0000-0000-0000-000000000048");
    public static readonly Guid Si49 = Guid.Parse("36000000-0000-0000-0000-000000000049");
    public static readonly Guid Si50 = Guid.Parse("36000000-0000-0000-0000-000000000050");
    public static readonly Guid Si51 = Guid.Parse("36000000-0000-0000-0000-000000000051");
    public static readonly Guid Si52 = Guid.Parse("36000000-0000-0000-0000-000000000052");
    public static readonly Guid Si53 = Guid.Parse("36000000-0000-0000-0000-000000000053");
    public static readonly Guid Si54 = Guid.Parse("36000000-0000-0000-0000-000000000054");
    public static readonly Guid Si55 = Guid.Parse("36000000-0000-0000-0000-000000000055");
    public static readonly Guid Si56 = Guid.Parse("36000000-0000-0000-0000-000000000056");
    public static readonly Guid Si57 = Guid.Parse("36000000-0000-0000-0000-000000000057");
    public static readonly Guid Si58 = Guid.Parse("36000000-0000-0000-0000-000000000058");
    public static readonly Guid Si59 = Guid.Parse("36000000-0000-0000-0000-000000000059");
    public static readonly Guid Si60 = Guid.Parse("36000000-0000-0000-0000-000000000060");

    public static void SeedScores(this ModelBuilder modelBuilder)
    {
        var c = SeedConstants.CreatedAt;

        // ── 30 Scores ────────────────────────────────────────────────
        modelBuilder.Entity<Scores>().HasData(
            // E2 R1 scores (Team1 → Track1, Judge1-at1)
            Create(Score1, SubmissionSeed.Sub1, AssignmentSeed.At1, 45m, false, false),
            Create(Score2, SubmissionSeed.Sub1, AssignmentSeed.At1, 48m, false, true),
            Create(Score3, SubmissionSeed.Sub2, AssignmentSeed.At2, 42m, false, false),
            Create(Score4, SubmissionSeed.Sub3, AssignmentSeed.At3, 38m, false, false),
            // E2 R2 scores
            Create(Score5, SubmissionSeed.Sub4, AssignmentSeed.At1, 88m, false, false),
            Create(Score6, SubmissionSeed.Sub5, AssignmentSeed.At2, 85m, false, false),
            // E2 R3 scores
            Create(Score7, SubmissionSeed.Sub6, AssignmentSeed.At1, 170m, false, false),
            Create(Score8, SubmissionSeed.Sub7, AssignmentSeed.At2, 165m, false, false),
            // E3 scores (closed)
            Create(Score9, SubmissionSeed.Sub8, AssignmentSeed.At8, 40m, false, false),
            Create(Score10, SubmissionSeed.Sub9, AssignmentSeed.At9, 44m, false, false),
            Create(Score11, SubmissionSeed.Sub10, AssignmentSeed.At10, 42m, false, false),
            Create(Score12, SubmissionSeed.Sub11, AssignmentSeed.At8, 85m, false, false),
            Create(Score13, SubmissionSeed.Sub12, AssignmentSeed.At9, 90m, false, false),
            Create(Score14, SubmissionSeed.Sub13, AssignmentSeed.At8, 175m, false, false),
            // E4 scores
            Create(Score15, SubmissionSeed.Sub14, AssignmentSeed.At11, 47m, false, false),
            Create(Score16, SubmissionSeed.Sub15, AssignmentSeed.At12, 45m, false, false),
            Create(Score17, SubmissionSeed.Sub16, AssignmentSeed.At13, 43m, false, false),
            Create(Score18, SubmissionSeed.Sub17, AssignmentSeed.At11, 92m, false, false),
            Create(Score19, SubmissionSeed.Sub19, AssignmentSeed.At11, 180m, false, false),
            // E6 scores (closed)
            Create(Score20, SubmissionSeed.Sub20, AssignmentSeed.At17, 39m, false, false),
            Create(Score21, SubmissionSeed.Sub21, AssignmentSeed.At18, 41m, false, false),
            Create(Score22, SubmissionSeed.Sub22, AssignmentSeed.At17, 37m, false, false),
            Create(Score23, SubmissionSeed.Sub23, AssignmentSeed.At17, 82m, false, false),
            Create(Score24, SubmissionSeed.Sub24, AssignmentSeed.At18, 78m, false, false),
            // E7 scores
            Create(Score25, SubmissionSeed.Sub25, AssignmentSeed.At19, 36m, false, false),
            Create(Score26, SubmissionSeed.Sub26, AssignmentSeed.At20, 40m, false, false),
            Create(Score27, SubmissionSeed.Sub27, AssignmentSeed.At19, 55m, false, false),
            Create(Score28, SubmissionSeed.Sub28, AssignmentSeed.At19, 88m, false, false),
            // E9 scores (closed)
            Create(Score29, SubmissionSeed.Sub29, AssignmentSeed.At24, 44m, false, false),
            Create(Score30, SubmissionSeed.Sub30, AssignmentSeed.At25, 0m, false, false) // Failed submission → 0 score
        );

        // ── 60 ScoreItems ────────────────────────────────────────────
        modelBuilder.Entity<ScoreItems>().HasData(
            // Score1 (E2 R1, Team1 → At1, Ct1: Item1+Item2)
            Create(Si1, Score1, SeedConstants.Item1, AssignmentSeed.At1, 22m, "Good idea"),
            Create(Si2, Score1, SeedConstants.Item2, AssignmentSeed.At1, 23m, "Feasible"),
            // Score2 (mock)
            Create(Si3, Score2, SeedConstants.Item1, AssignmentSeed.At1, 24m, "Mock creative"),
            Create(Si4, Score2, SeedConstants.Item2, AssignmentSeed.At1, 24m, "Mock feasible"),
            // Score3 (E2 R1, Team2 → At2)
            Create(Si5, Score3, SeedConstants.Item1, AssignmentSeed.At2, 20m, "Decent"),
            Create(Si6, Score3, SeedConstants.Item2, AssignmentSeed.At2, 22m, "Good"),
            // Score4 (E2 R1, Team3 → At3, Ct2: Item3+Item4)
            Create(Si7, Score4, SeedConstants.Item3, AssignmentSeed.At3, 19m, "OK tech"),
            Create(Si8, Score4, SeedConstants.Item4, AssignmentSeed.At3, 19m, "OK arch"),
            // Score5 (E2 R2, Team1 → At1)
            Create(Si9, Score5, SeedConstants.Item5, AssignmentSeed.At1, 44m, "Great UI"),
            Create(Si10, Score5, SeedConstants.Item6, AssignmentSeed.At1, 44m, "All features"),
            // Score6 (E2 R2, Team2 → At2)
            Create(Si11, Score6, SeedConstants.Item5, AssignmentSeed.At2, 42m, "Nice UX"),
            Create(Si12, Score6, SeedConstants.Item6, AssignmentSeed.At2, 43m, "Complete"),
            // Score7 (E2 R3, Team1 → At1)
            Create(Si13, Score7, SeedConstants.Item7, AssignmentSeed.At1, 85m, "Polished"),
            Create(Si14, Score7, SeedConstants.Item8, AssignmentSeed.At1, 85m, "Great presentation"),
            // Score8 (E2 R3, Team2 → At2)
            Create(Si15, Score8, SeedConstants.Item7, AssignmentSeed.At2, 82m, "Good"),
            Create(Si16, Score8, SeedConstants.Item8, AssignmentSeed.At2, 83m, "Well presented"),
            // Score9 (E3 R1, Team6 → At8, Ct5: Item9+Item10)
            Create(Si17, Score9, SeedConstants.Item9, AssignmentSeed.At8, 20m, "Quality OK"),
            Create(Si18, Score9, SeedConstants.Item10, AssignmentSeed.At8, 20m, "On time"),
            // Score10 (E3 R1, Team7 → At9)
            Create(Si19, Score10, SeedConstants.Item9, AssignmentSeed.At9, 22m, "Good quality"),
            Create(Si20, Score10, SeedConstants.Item10, AssignmentSeed.At9, 22m, "On time"),
            // Score11 (E3 R1, Team8 → At10)
            Create(Si21, Score11, SeedConstants.Item9, AssignmentSeed.At10, 21m, "Average"),
            Create(Si22, Score11, SeedConstants.Item10, AssignmentSeed.At10, 21m, "On time"),
            // Score12 (E3 R2, Team6 → At8, Ct7: Item13+Item14)
            Create(Si23, Score12, SeedConstants.Item13, AssignmentSeed.At8, 43m, "Good progress"),
            Create(Si24, Score12, SeedConstants.Item14, AssignmentSeed.At8, 42m, "Team works well"),
            // Score13 (E3 R2, Team7 → At9)
            Create(Si25, Score13, SeedConstants.Item13, AssignmentSeed.At9, 45m, "Great progress"),
            Create(Si26, Score13, SeedConstants.Item14, AssignmentSeed.At9, 45m, "Excellent team"),
            // Score14 (E3 R3, Team6 → At8, Ct8: Item15+Item16)
            Create(Si27, Score14, SeedConstants.Item15, AssignmentSeed.At8, 88m, "Strong result"),
            Create(Si28, Score14, SeedConstants.Item16, AssignmentSeed.At8, 87m, "Impressive"),
            // Score15 (E4 R1, Team10 → At11, Ct9: Item17+Item18)
            Create(Si29, Score15, SeedConstants.Item17, AssignmentSeed.At11, 24m, "Good business"),
            Create(Si30, Score15, SeedConstants.Item18, AssignmentSeed.At11, 23m, "Impactful"),
            // Score16 (E4 R1, Team11 → At12)
            Create(Si31, Score16, SeedConstants.Item17, AssignmentSeed.At12, 22m, "OK idea"),
            Create(Si32, Score16, SeedConstants.Item18, AssignmentSeed.At12, 23m, "Good impact"),
            // Score17 (E4 R1, Team12 → At13, Ct10: Item19+Item20)
            Create(Si33, Score17, SeedConstants.Item19, AssignmentSeed.At13, 21m, "Solid tech"),
            Create(Si34, Score17, SeedConstants.Item20, AssignmentSeed.At13, 22m, "Good data"),
            // Score18 (E4 R2, Team10 → At11, Ct11: Item21+Item22)
            Create(Si35, Score18, SeedConstants.Item21, AssignmentSeed.At11, 46m, "On track"),
            Create(Si36, Score18, SeedConstants.Item22, AssignmentSeed.At11, 46m, "Clean code"),
            // Score19 (E4 R3, Team10 → At11, Ct12: Item23+Item24)
            Create(Si37, Score19, SeedConstants.Item23, AssignmentSeed.At11, 90m, "Excellent product"),
            Create(Si38, Score19, SeedConstants.Item24, AssignmentSeed.At11, 90m, "Great demo"),
            // Score20 (E6 R1, Team15 → At17, Ct13: Item25+Item26)
            Create(Si39, Score20, SeedConstants.Item25, AssignmentSeed.At17, 20m, "Basic OK"),
            Create(Si40, Score20, SeedConstants.Item26, AssignmentSeed.At17, 19m, "Advanced OK"),
            // Score21 (E6 R1, Team16 → At18)
            Create(Si41, Score21, SeedConstants.Item25, AssignmentSeed.At18, 21m, "Good basic"),
            Create(Si42, Score21, SeedConstants.Item26, AssignmentSeed.At18, 20m, "Good advanced"),
            // Score22 (E6 R1, Team17 → At17)
            Create(Si43, Score22, SeedConstants.Item25, AssignmentSeed.At17, 18m, "Weak basic"),
            Create(Si44, Score22, SeedConstants.Item26, AssignmentSeed.At17, 19m, "OK advanced"),
            // Score23 (E6 R2, Team15 → At17, Ct14: Item27+Item28)
            Create(Si45, Score23, SeedConstants.Item27, AssignmentSeed.At17, 41m, "Final OK"),
            Create(Si46, Score23, SeedConstants.Item28, AssignmentSeed.At17, 41m, "Final good"),
            // Score24 (E6 R2, Team16 → At18)
            Create(Si47, Score24, SeedConstants.Item27, AssignmentSeed.At18, 39m, "Decent final"),
            Create(Si48, Score24, SeedConstants.Item28, AssignmentSeed.At18, 39m, "OK final"),
            // Score25 (E7 R1, Team18 → At19, Ct15: Item29+Item30)
            Create(Si49, Score25, SeedConstants.Item29, AssignmentSeed.At19, 18m, "OK start"),
            Create(Si50, Score25, SeedConstants.Item30, AssignmentSeed.At19, 18m, "OK start 2"),
            // Score26 (E7 R1, Team19 → At20)
            Create(Si51, Score26, SeedConstants.Item29, AssignmentSeed.At20, 20m, "Good start"),
            Create(Si52, Score26, SeedConstants.Item30, AssignmentSeed.At20, 20m, "Good start 2"),
            // Score27 (E7 R2, Team18 → At19, Ct17: Item33+Item34)
            Create(Si53, Score27, SeedConstants.Item33, AssignmentSeed.At19, 28m, "Semi OK"),
            Create(Si54, Score27, SeedConstants.Item34, AssignmentSeed.At19, 27m, "Semi good"),
            // Score28 (E7 R3, Team18 → At19, Ct18: Item35+Item36)
            Create(Si55, Score28, SeedConstants.Item35, AssignmentSeed.At19, 44m, "Final good"),
            Create(Si56, Score28, SeedConstants.Item36, AssignmentSeed.At19, 44m, "Final great"),
            // Score29 (E9 R1, Team22 → At24, Ct28: Item55+Item56)
            Create(Si57, Score29, SeedConstants.Item55, AssignmentSeed.At24, 22m, "E9 winter OK"),
            Create(Si58, Score29, SeedConstants.Item56, AssignmentSeed.At24, 22m, "E9 winter good"),
            // Score30 (E9 R1, Team23 → At25 - failed)
            Create(Si59, Score30, SeedConstants.Item55, AssignmentSeed.At25, 0m, "No submission"),
            Create(Si60, Score30, SeedConstants.Item56, AssignmentSeed.At25, 0m, "No submission")
        );
    }

    private static Scores Create(Guid id, Guid submissionId, Guid assignTrackId, decimal totalScore, bool isRetake, bool isMock, Guid? retakeFromScoreId = null) => new()
    {
        Id = id, SubmissionId = submissionId, AssignTrackId = assignTrackId,
        TotalScore = totalScore, IsRetake = isRetake, IsMock = isMock, RetakeFromScoreId = retakeFromScoreId,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };

    private static ScoreItems Create(Guid id, Guid scoreId, Guid criteriaItemId, Guid assignTrackId, decimal score, string? comment) => new()
    {
        Id = id, ScoreId = scoreId, CriteriaItemId = criteriaItemId, AssignTrackId = assignTrackId,
        Score = score, Comment = comment,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };
}
