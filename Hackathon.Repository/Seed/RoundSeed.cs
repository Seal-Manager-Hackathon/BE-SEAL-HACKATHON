using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class RoundSeed
{
    // 24 Rounds (at least 2 per event)
    public static readonly Guid R1E1  = Guid.Parse("21000000-0000-0000-0000-000000000001");
    public static readonly Guid R2E1  = Guid.Parse("21000000-0000-0000-0000-000000000002");
    public static readonly Guid R1E2  = Guid.Parse("21000000-0000-0000-0000-000000000003");
    public static readonly Guid R2E2  = Guid.Parse("21000000-0000-0000-0000-000000000004");
    public static readonly Guid R1E3  = Guid.Parse("21000000-0000-0000-0000-000000000005");
    public static readonly Guid R2E3  = Guid.Parse("21000000-0000-0000-0000-000000000006");
    public static readonly Guid R1E4  = Guid.Parse("21000000-0000-0000-0000-000000000007");
    public static readonly Guid R2E4  = Guid.Parse("21000000-0000-0000-0000-000000000008");
    public static readonly Guid R1E5  = Guid.Parse("21000000-0000-0000-0000-000000000009");
    public static readonly Guid R2E5  = Guid.Parse("21000000-0000-0000-0000-000000000010");
    public static readonly Guid R1E6  = Guid.Parse("21000000-0000-0000-0000-000000000011");
    public static readonly Guid R2E6  = Guid.Parse("21000000-0000-0000-0000-000000000012");
    public static readonly Guid R1E7  = Guid.Parse("21000000-0000-0000-0000-000000000013");
    public static readonly Guid R2E7  = Guid.Parse("21000000-0000-0000-0000-000000000014");
    public static readonly Guid R1E8  = Guid.Parse("21000000-0000-0000-0000-000000000015");
    public static readonly Guid R2E8  = Guid.Parse("21000000-0000-0000-0000-000000000016");
    public static readonly Guid R1E9  = Guid.Parse("21000000-0000-0000-0000-000000000017");
    public static readonly Guid R2E9  = Guid.Parse("21000000-0000-0000-0000-000000000018");
    public static readonly Guid R1E10 = Guid.Parse("21000000-0000-0000-0000-000000000019");
    public static readonly Guid R2E10 = Guid.Parse("21000000-0000-0000-0000-000000000020");
    public static readonly Guid R3E4  = Guid.Parse("21000000-0000-0000-0000-000000000021");
    public static readonly Guid R1E4B = Guid.Parse("21000000-0000-0000-0000-000000000022");
    public static readonly Guid R1E7B = Guid.Parse("21000000-0000-0000-0000-000000000023");
    public static readonly Guid R2E4B = Guid.Parse("21000000-0000-0000-0000-000000000024");

    public static void SeedRounds(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rounds>().HasData(
            // Event 1 (Draft) - 2 rounds
            Create(R1E1,  SeedConstants.Event1Draft,      "Vòng 1 - Khởi động",  "Ý tưởng khởi đầu", 1, 10),
            Create(R2E1,  SeedConstants.Event1Draft,      "Vòng 2 - Tăng tốc",   "Bản thử nghiệm",   2, 5),
            // Event 2 (Published) - 2 rounds
            Create(R1E2,  SeedConstants.Event2Published,   "Vòng 1 - Sơ loại",    "Đánh giá sơ bộ",   1, 12),
            Create(R2E2,  SeedConstants.Event2Published,   "Vòng 2 - Chung kết",  "Sản phẩm hoàn thiện",2, 6),
            // Event 3 (Closed) - 2 rounds
            Create(R1E3,  SeedConstants.Event3Closed,      "Vòng 1 - Duy nhất",   "Vòng đấu duy nhất",1, 8),
            Create(R2E3,  SeedConstants.Event3Closed,      "Vòng 2 - Tổng kết",   "Chung kết",        2, 4),
            // Event 4 (Published) - 3 rounds (extra)
            Create(R1E4,  SeedConstants.Event4Published,   "Vòng 1 - Ý tưởng",    "Nộp ý tưởng",      1, 15),
            Create(R2E4,  SeedConstants.Event4Published,   "Vòng 2 - Trình bày",  "Pitching",         2, 8),
            Create(R3E4,  SeedConstants.Event4Published,   "Vòng 3 - Chung kết",  "Sản phẩm cuối",    3, 4),
            // Event 5 (Draft) - 2 rounds
            Create(R1E5,  SeedConstants.Event5Draft,       "Vòng 1 - Thiết kế",   "Thiết kế giải pháp",1, 12),
            Create(R2E5,  SeedConstants.Event5Draft,       "Vòng 2 - Phát triển", "Xây dựng MVP",     2, 6),
            // Event 6 (Closed) - 2 rounds
            Create(R1E6,  SeedConstants.Event6Closed,      "Vòng 1 - Lý thuyết",  "Trắc nghiệm",      1, 10),
            Create(R2E6,  SeedConstants.Event6Closed,      "Vòng 2 - Thực hành",  "Lập trình",        2, 5),
            // Event 7 (Published) - 2 rounds
            Create(R1E7,  SeedConstants.Event7Published,   "Vòng 1 - Demo",       "Video demo",       1, 10),
            Create(R2E7,  SeedConstants.Event7Published,   "Vòng 2 - Hackathon",  "Lập trình 24h",    2, 5),
            // Event 8 (Draft) - 2 rounds
            Create(R1E8,  SeedConstants.Event8Draft,       "Vòng 1 - Phân tích",  "Phân tích yêu cầu",1, 10),
            Create(R2E8,  SeedConstants.Event8Draft,       "Vòng 2 - Xây dựng",   "Xây dựng",         2, 5),
            // Event 9 (Closed) - 2 rounds
            Create(R1E9,  SeedConstants.Event9Closed,      "Vòng 1 - Phỏng vấn",  "Phỏng vấn trực tiếp",1, 10),
            Create(R2E9,  SeedConstants.Event9Closed,      "Vòng 2 - Đánh giá",   "Đánh giá cuối",    2, 5),
            // Event 10 (Published) - 2 rounds
            Create(R1E10, SeedConstants.Event10Published,  "Vòng 1 - Xét duyệt",  "Xét duyệt hồ sơ",  1, 20),
            Create(R2E10, SeedConstants.Event10Published,  "Vòng 2 - Phỏng vấn",  "Phỏng vấn",        2, 10),
            // Extra disabled rounds (for edge cases)
            Create(R1E4B, SeedConstants.Event4Published,   "Nháp - Vòng bổ sung",  "Vòng bị vô hiệu",  4, 5, true),
            Create(R2E4B, SeedConstants.Event7Published,   "Nháp - Vòng dự phòng", "Vòng dự phòng",     3, 5, true)
        );
    }

    private static Rounds Create(Guid id, Guid eventId, string name, string description, int roundNo, int limitTeam, bool isDisable = false)
    {
        return new Rounds
        {
            Id = id, EventId = eventId, Name = name, Description = description,
            RoundNo = roundNo,
            StartTime = SeedConstants.CreatedAt.AddDays(11),
            EndTime = SeedConstants.CreatedAt.AddDays(15),
            StartSubmission = SeedConstants.CreatedAt.AddDays(11),
            EndSubmission = SeedConstants.CreatedAt.AddDays(14),
            LimitTeam = limitTeam, IsDisable = isDisable,
            CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
        };
    }
}
