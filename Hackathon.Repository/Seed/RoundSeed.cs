using System;
using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class RoundSeed
{
    // 12 Rounds GUIDs
    public static readonly Guid RoundEvent1R1 = Guid.Parse("21000000-0000-0000-0000-000000000001");
    public static readonly Guid RoundEvent1R2 = Guid.Parse("21000000-0000-0000-0000-000000000002");
    public static readonly Guid RoundEvent2R1 = Guid.Parse("21000000-0000-0000-0000-000000000003");
    public static readonly Guid RoundEvent2R2 = Guid.Parse("21000000-0000-0000-0000-000000000004");
    public static readonly Guid RoundEvent3R1 = Guid.Parse("21000000-0000-0000-0000-000000000005");
    public static readonly Guid RoundEvent4R1 = Guid.Parse("21000000-0000-0000-0000-000000000006");
    public static readonly Guid RoundEvent4R2 = Guid.Parse("21000000-0000-0000-0000-000000000007");
    public static readonly Guid RoundEvent6R1 = Guid.Parse("21000000-0000-0000-0000-000000000008");
    public static readonly Guid RoundEvent7R1 = Guid.Parse("21000000-0000-0000-0000-000000000009");
    public static readonly Guid RoundEvent7R2 = Guid.Parse("21000000-0000-0000-0000-000000000010");
    public static readonly Guid RoundEvent9R1 = Guid.Parse("21000000-0000-0000-0000-000000000011");
    public static readonly Guid RoundEvent10R1 = Guid.Parse("21000000-0000-0000-0000-000000000012");

    public static void SeedRounds(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rounds>().HasData(
            CreateRound(RoundEvent1R1, SeedConstants.Event1Draft, "Vòng 1 - Khởi động", "Ý tưởng khởi đầu", 1, 10),
            CreateRound(RoundEvent1R2, SeedConstants.Event1Draft, "Vòng 2 - Tăng tốc", "Bản thử nghiệm", 2, 5),
            CreateRound(RoundEvent2R1, SeedConstants.Event2Published, "Vòng 1 - Sơ loại", "Đánh giá sơ bộ", 1, 12),
            CreateRound(RoundEvent2R2, SeedConstants.Event2Published, "Vòng 2 - Chung kết", "Sản phẩm hoàn thiện", 2, 6),
            CreateRound(RoundEvent3R1, SeedConstants.Event3Closed, "Vòng 1 - Duy nhất", "Vòng đấu duy nhất", 1, 8),
            CreateRound(RoundEvent4R1, SeedConstants.Event4Published, "Vòng 1 - Ý tưởng", "Nộp ý tưởng sáng tạo", 1, 15),
            CreateRound(RoundEvent4R2, SeedConstants.Event4Published, "Vòng 2 - Trình bày", "Pitching với giám khảo", 2, 8),
            CreateRound(RoundEvent6R1, SeedConstants.Event6Closed, "Vòng 1 - Lý thuyết", "Trắc nghiệm kiến thức", 1, 10),
            CreateRound(RoundEvent7R1, SeedConstants.Event7Published, "Vòng 1 - Demo", "Video demo ngắn", 1, 10),
            CreateRound(RoundEvent7R2, SeedConstants.Event7Published, "Vòng 2 - Hack ngày đêm", "Lập trình liên tục 24h", 2, 5),
            CreateRound(RoundEvent9R1, SeedConstants.Event9Closed, "Vòng 1 - Phỏng vấn", "Phỏng vấn trực tiếp", 1, 10),
            CreateRound(RoundEvent10R1, SeedConstants.Event10Published, "Vòng 1 - Xét duyệt hồ sơ", "Đánh giá CV và portfolio", 1, 20)
        );
    }

    private static Rounds CreateRound(Guid id, Guid eventId, string name, string description, int roundNo, int limitTeam)
    {
        return new Rounds
        {
            Id = id,
            EventId = eventId,
            Name = name,
            Description = description,
            RoundNo = roundNo,
            StartTime = SeedConstants.CreatedAt.AddDays(11),
            EndTime = SeedConstants.CreatedAt.AddDays(15),
            StartSubmission = SeedConstants.CreatedAt.AddDays(11),
            EndSubmission = SeedConstants.CreatedAt.AddDays(14),
            LimitTeam = limitTeam,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }
}
