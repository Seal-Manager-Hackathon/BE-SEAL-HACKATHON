using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class RoundSeed
{
    public static void SeedRounds(this ModelBuilder modelBuilder)
    {
        var c = SeedConstants.CreatedAt;
        modelBuilder.Entity<Rounds>().HasData(
            // E1 (Draft) - 2 rounds
            Create(SeedConstants.RoundE1R1, SeedConstants.Event1Draft, "Vòng 1 - Ý tưởng", 1, c.AddDays(10), c.AddDays(17), c.AddDays(11), c.AddDays(16), 10),
            Create(SeedConstants.RoundE1R2, SeedConstants.Event1Draft, "Vòng 2 - Chung kết", 2, c.AddDays(18), c.AddDays(30), c.AddDays(19), c.AddDays(28), 5),
            // E2 (Published) - 3 active rounds
            Create(SeedConstants.RoundE2R1, SeedConstants.Event2Published, "Vòng 1 - Khởi động", 1, c.AddDays(10), c.AddDays(17), c.AddDays(11), c.AddDays(16), 12),
            Create(SeedConstants.RoundE2R2, SeedConstants.Event2Published, "Vòng 2 - Bán kết", 2, c.AddDays(18), c.AddDays(24), c.AddDays(19), c.AddDays(23), 8),
            Create(SeedConstants.RoundE2R3, SeedConstants.Event2Published, "Vòng 3 - Chung kết", 3, c.AddDays(25), c.AddDays(30), c.AddDays(26), c.AddDays(29), 4),
            // E3 (Closed) - 3 active + 1 disabled
            Create(SeedConstants.RoundE3R1, SeedConstants.Event3Closed, "Vòng 1 - Sơ loại", 1, c.AddDays(-30), c.AddDays(-23), c.AddDays(-29), c.AddDays(-24), 8),
            Create(SeedConstants.RoundE3R2, SeedConstants.Event3Closed, "Vòng 2 - Bán kết", 2, c.AddDays(-22), c.AddDays(-16), c.AddDays(-21), c.AddDays(-17), 4),
            Create(SeedConstants.RoundE3R3, SeedConstants.Event3Closed, "Vòng 3 - Chung kết", 3, c.AddDays(-15), c.AddDays(-10), c.AddDays(-14), c.AddDays(-11), 2),
            Create(SeedConstants.RoundE3R1B, SeedConstants.Event3Closed, "Vòng 1 - Sơ loại (Disabled)", 1, c.AddDays(-30), c.AddDays(-23), c.AddDays(-29), c.AddDays(-24), 8, true),
            // E4 (Published) - 3 active + 1 disabled
            Create(SeedConstants.RoundE4R1, SeedConstants.Event4Published, "Vòng 1 - Ý tưởng", 1, c.AddDays(10), c.AddDays(17), c.AddDays(11), c.AddDays(16), 15),
            Create(SeedConstants.RoundE4R2, SeedConstants.Event4Published, "Vòng 2 - Phát triển", 2, c.AddDays(18), c.AddDays(24), c.AddDays(19), c.AddDays(23), 10),
            Create(SeedConstants.RoundE4R3, SeedConstants.Event4Published, "Vòng 3 - Chung kết", 3, c.AddDays(25), c.AddDays(30), c.AddDays(26), c.AddDays(29), 5),
            Create(SeedConstants.RoundE4R1B, SeedConstants.Event4Published, "Vòng 1 - Ý tưởng (Disabled)", 1, c.AddDays(10), c.AddDays(17), c.AddDays(11), c.AddDays(16), 15, true),
            // E5 (Draft) - 2 rounds
            Create(SeedConstants.RoundE5R1, SeedConstants.Event5Draft, "Vòng 1 - Nộp đề xuất", 1, c.AddDays(40), c.AddDays(50), c.AddDays(41), c.AddDays(49), 12),
            Create(SeedConstants.RoundE5R2, SeedConstants.Event5Draft, "Vòng 2 - Demo", 2, c.AddDays(51), c.AddDays(60), c.AddDays(52), c.AddDays(59), 6),
            // E6 (Closed) - 2 active + 1 disabled
            Create(SeedConstants.RoundE6R1, SeedConstants.Event6Closed, "Vòng 1 - Sơ loại", 1, c.AddDays(-60), c.AddDays(-53), c.AddDays(-59), c.AddDays(-54), 10),
            Create(SeedConstants.RoundE6R2, SeedConstants.Event6Closed, "Vòng 2 - Chung kết", 2, c.AddDays(-52), c.AddDays(-40), c.AddDays(-51), c.AddDays(-45), 5),
            Create(SeedConstants.RoundE6R1B, SeedConstants.Event6Closed, "Vòng 1 - Sơ loại (Disabled)", 1, c.AddDays(-60), c.AddDays(-53), c.AddDays(-59), c.AddDays(-54), 10, true),
            // E7 (Published) - 3 active + 2 disabled
            Create(SeedConstants.RoundE7R1, SeedConstants.Event7Published, "Vòng 1 - Khởi động", 1, c.AddDays(10), c.AddDays(17), c.AddDays(11), c.AddDays(16), 10),
            Create(SeedConstants.RoundE7R2, SeedConstants.Event7Published, "Vòng 2 - Bán kết", 2, c.AddDays(18), c.AddDays(24), c.AddDays(19), c.AddDays(23), 6),
            Create(SeedConstants.RoundE7R3, SeedConstants.Event7Published, "Vòng 3 - Chung kết", 3, c.AddDays(25), c.AddDays(30), c.AddDays(26), c.AddDays(29), 3),
            Create(SeedConstants.RoundE7R1B, SeedConstants.Event7Published, "Vòng 1 - Khởi động (Disabled)", 1, c.AddDays(10), c.AddDays(17), c.AddDays(11), c.AddDays(16), 10, true),
            Create(SeedConstants.RoundE7R2B, SeedConstants.Event7Published, "Vòng 2 - Bán kết (Disabled)", 2, c.AddDays(18), c.AddDays(24), c.AddDays(19), c.AddDays(23), 6, true),
            // E8 (Draft) - 2 rounds
            Create(SeedConstants.RoundE8R1, SeedConstants.Event8Draft, "Vòng 1 - Nộp đề xuất", 1, c.AddDays(70), c.AddDays(80), c.AddDays(71), c.AddDays(79), 10),
            Create(SeedConstants.RoundE8R2, SeedConstants.Event8Draft, "Vòng 2 - Demo", 2, c.AddDays(81), c.AddDays(90), c.AddDays(82), c.AddDays(89), 5),
            // E9 (Closed) - 2 rounds
            Create(SeedConstants.RoundE9R1, SeedConstants.Event9Closed, "Vòng 1 - Sơ loại", 1, c.AddDays(-90), c.AddDays(-83), c.AddDays(-89), c.AddDays(-84), 10),
            Create(SeedConstants.RoundE9R2, SeedConstants.Event9Closed, "Vòng 2 - Chung kết", 2, c.AddDays(-82), c.AddDays(-70), c.AddDays(-81), c.AddDays(-75), 5),
            // E10 (Published) - 2 active + 1 disabled
            Create(SeedConstants.RoundE10R1, SeedConstants.Event10Published, "Vòng 1 - Khởi động", 1, c.AddDays(10), c.AddDays(17), c.AddDays(11), c.AddDays(16), 10),
            Create(SeedConstants.RoundE10R2, SeedConstants.Event10Published, "Vòng 2 - Chung kết", 2, c.AddDays(18), c.AddDays(30), c.AddDays(19), c.AddDays(28), 5),
            Create(SeedConstants.RoundE10R1B, SeedConstants.Event10Published, "Vòng 1 - Khởi động (Disabled)", 1, c.AddDays(10), c.AddDays(17), c.AddDays(11), c.AddDays(16), 10, true)
        );
    }

    private static Rounds Create(Guid id, Guid eventId, string name, int roundNo, DateTimeOffset start, DateTimeOffset end, DateTimeOffset startSub, DateTimeOffset endSub, int? limitTeam, bool isDisable = false) => new()
    {
        Id = id, EventId = eventId, Name = name, Description = name, RoundNo = roundNo,
        StartTime = start, EndTime = end, StartSubmission = startSub, EndSubmission = endSub,
        LimitTeam = limitTeam, IsDisable = isDisable, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };
}
