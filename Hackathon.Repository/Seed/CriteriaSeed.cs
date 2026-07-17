using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class CriteriaSeed
{
    // 20 Criteria Templates
    public static readonly Guid Tpl1  = Guid.Parse("22000000-0000-0000-0000-000000000001");
    public static readonly Guid Tpl2  = Guid.Parse("22000000-0000-0000-0000-000000000002");
    public static readonly Guid Tpl3  = Guid.Parse("22000000-0000-0000-0000-000000000003");
    public static readonly Guid Tpl4  = Guid.Parse("22000000-0000-0000-0000-000000000004");
    public static readonly Guid Tpl5  = Guid.Parse("22000000-0000-0000-0000-000000000005");
    public static readonly Guid Tpl6  = Guid.Parse("22000000-0000-0000-0000-000000000006");
    public static readonly Guid Tpl7  = Guid.Parse("22000000-0000-0000-0000-000000000007");
    public static readonly Guid Tpl8  = Guid.Parse("22000000-0000-0000-0000-000000000008");
    public static readonly Guid Tpl9  = Guid.Parse("22000000-0000-0000-0000-000000000009");
    public static readonly Guid Tpl10 = Guid.Parse("22000000-0000-0000-0000-000000000010");
    public static readonly Guid Tpl11 = Guid.Parse("22000000-0000-0000-0000-000000000011");
    public static readonly Guid Tpl12 = Guid.Parse("22000000-0000-0000-0000-000000000012");
    public static readonly Guid Tpl13 = Guid.Parse("22000000-0000-0000-0000-000000000013");
    public static readonly Guid Tpl14 = Guid.Parse("22000000-0000-0000-0000-000000000014");
    public static readonly Guid Tpl15 = Guid.Parse("22000000-0000-0000-0000-000000000015");
    public static readonly Guid Tpl16 = Guid.Parse("22000000-0000-0000-0000-000000000016");
    public static readonly Guid Tpl17 = Guid.Parse("22000000-0000-0000-0000-000000000017");
    public static readonly Guid Tpl18 = Guid.Parse("22000000-0000-0000-0000-000000000018");
    public static readonly Guid Tpl19 = Guid.Parse("22000000-0000-0000-0000-000000000019");
    public static readonly Guid Tpl20 = Guid.Parse("22000000-0000-0000-0000-000000000020");

    // 40 Criteria Items (2 per template)
    public static readonly Guid Item1  = Guid.Parse("23000000-0000-0000-0000-000000000001");
    public static readonly Guid Item2  = Guid.Parse("23000000-0000-0000-0000-000000000002");
    public static readonly Guid Item3  = Guid.Parse("23000000-0000-0000-0000-000000000003");
    public static readonly Guid Item4  = Guid.Parse("23000000-0000-0000-0000-000000000004");
    public static readonly Guid Item5  = Guid.Parse("23000000-0000-0000-0000-000000000005");
    public static readonly Guid Item6  = Guid.Parse("23000000-0000-0000-0000-000000000006");
    public static readonly Guid Item7  = Guid.Parse("23000000-0000-0000-0000-000000000007");
    public static readonly Guid Item8  = Guid.Parse("23000000-0000-0000-0000-000000000008");
    public static readonly Guid Item9  = Guid.Parse("23000000-0000-0000-0000-000000000009");
    public static readonly Guid Item10 = Guid.Parse("23000000-0000-0000-0000-000000000010");
    public static readonly Guid Item11 = Guid.Parse("23000000-0000-0000-0000-000000000011");
    public static readonly Guid Item12 = Guid.Parse("23000000-0000-0000-0000-000000000012");
    public static readonly Guid Item13 = Guid.Parse("23000000-0000-0000-0000-000000000013");
    public static readonly Guid Item14 = Guid.Parse("23000000-0000-0000-0000-000000000014");
    public static readonly Guid Item15 = Guid.Parse("23000000-0000-0000-0000-000000000015");
    public static readonly Guid Item16 = Guid.Parse("23000000-0000-0000-0000-000000000016");
    public static readonly Guid Item17 = Guid.Parse("23000000-0000-0000-0000-000000000017");
    public static readonly Guid Item18 = Guid.Parse("23000000-0000-0000-0000-000000000018");
    public static readonly Guid Item19 = Guid.Parse("23000000-0000-0000-0000-000000000019");
    public static readonly Guid Item20 = Guid.Parse("23000000-0000-0000-0000-000000000020");
    public static readonly Guid Item21 = Guid.Parse("23000000-0000-0000-0000-000000000021");
    public static readonly Guid Item22 = Guid.Parse("23000000-0000-0000-0000-000000000022");
    public static readonly Guid Item23 = Guid.Parse("23000000-0000-0000-0000-000000000023");
    public static readonly Guid Item24 = Guid.Parse("23000000-0000-0000-0000-000000000024");
    public static readonly Guid Item25 = Guid.Parse("23000000-0000-0000-0000-000000000025");
    public static readonly Guid Item26 = Guid.Parse("23000000-0000-0000-0000-000000000026");
    public static readonly Guid Item27 = Guid.Parse("23000000-0000-0000-0000-000000000027");
    public static readonly Guid Item28 = Guid.Parse("23000000-0000-0000-0000-000000000028");
    public static readonly Guid Item29 = Guid.Parse("23000000-0000-0000-0000-000000000029");
    public static readonly Guid Item30 = Guid.Parse("23000000-0000-0000-0000-000000000030");
    public static readonly Guid Item31 = Guid.Parse("23000000-0000-0000-0000-000000000031");
    public static readonly Guid Item32 = Guid.Parse("23000000-0000-0000-0000-000000000032");
    public static readonly Guid Item33 = Guid.Parse("23000000-0000-0000-0000-000000000033");
    public static readonly Guid Item34 = Guid.Parse("23000000-0000-0000-0000-000000000034");
    public static readonly Guid Item35 = Guid.Parse("23000000-0000-0000-0000-000000000035");
    public static readonly Guid Item36 = Guid.Parse("23000000-0000-0000-0000-000000000036");
    public static readonly Guid Item37 = Guid.Parse("23000000-0000-0000-0000-000000000037");
    public static readonly Guid Item38 = Guid.Parse("23000000-0000-0000-0000-000000000038");
    public static readonly Guid Item39 = Guid.Parse("23000000-0000-0000-0000-000000000039");
    public static readonly Guid Item40 = Guid.Parse("23000000-0000-0000-0000-000000000040");

    public static void SeedCriteria(this ModelBuilder modelBuilder)
    {
        // Templates (active ones for main rounds, disabled for edge cases)
        modelBuilder.Entity<CriteriaTemplates>().HasData(
            Create(Tpl1,  RoundSeed.R1E2,  "Đánh giá sơ loại E2R1",     "Bảng đánh giá chính thức", false),
            Create(Tpl2,  RoundSeed.R2E2,  "Đánh giá chung kết E2R2",   "Bảng đánh giá chính thức", false),
            Create(Tpl3,  RoundSeed.R1E3,  "Đánh giá vòng duy nhất",    "Bảng đánh giá chính thức", false),
            Create(Tpl4,  RoundSeed.R1E4,  "Đánh giá ý tưởng E4R1",     "Bảng đánh giá chính thức", false),
            Create(Tpl5,  RoundSeed.R2E4,  "Đánh giá pitching E4R2",    "Bảng đánh giá chính thức", false),
            Create(Tpl6,  RoundSeed.R3E4,  "Đánh giá chung kết E4R3",   "Bảng đánh giá chính thức", false),
            Create(Tpl7,  RoundSeed.R1E6,  "Đánh giá lý thuyết E6R1",   "Bảng đánh giá chính thức", false),
            Create(Tpl8,  RoundSeed.R1E7,  "Đánh giá demo E7R1",        "Bảng đánh giá chính thức", false),
            Create(Tpl9,  RoundSeed.R2E7,  "Đánh giá hackathon E7R2",   "Bảng đánh giá chính thức", false),
            Create(Tpl10, RoundSeed.R1E10, "Đánh giá xét duyệt E10R1",  "Bảng đánh giá chính thức", false),
            Create(Tpl11, RoundSeed.R2E10, "Đánh giá phỏng vấn E10R2",  "Bảng đánh giá chính thức", false),
            Create(Tpl12, RoundSeed.R1E9,  "Đánh giá phỏng vấn E9R1",   "Bảng đánh giá chính thức", false),
            Create(Tpl13, RoundSeed.R1E1,  "Đánh giá khởi động E1R1",   "Bảng đánh giá draft",      false),
            Create(Tpl14, RoundSeed.R1E5,  "Đánh giá thiết kế E5R1",    "Bảng đánh giá draft",      false),
            Create(Tpl15, RoundSeed.R1E8,  "Đánh giá phân tích E8R1",   "Bảng đánh giá draft",      false),
            // Disabled templates
            Create(Tpl16, RoundSeed.R1E2,  "Bảng dự phòng A",            "Dự phòng",                 true),
            Create(Tpl17, RoundSeed.R2E2,  "Bảng dự phòng B",            "Dự phòng",                 true),
            Create(Tpl18, RoundSeed.R1E4,  "Bảng dự phòng C",            "Dự phòng",                 true),
            Create(Tpl19, RoundSeed.R1E7,  "Bảng dự phòng D",            "Dự phòng",                 true),
            Create(Tpl20, RoundSeed.R1E10, "Bảng dự phòng E",            "Dự phòng",                 true)
        );

        // 40 Criteria Items (2 per template, with varying max scores)
        modelBuilder.Entity<CriteriaItems>().HasData(
            Create(Item1, Tpl1,  "Ý tưởng sáng tạo",   "Tính mới lạ", 50m, false),
            Create(Item2, Tpl1,  "Tính khả thi",        "Khả năng phát triển", 50m, false),
            Create(Item3, Tpl2,  "Chất lượng code",     "Kiến trúc và style", 60m, false),
            Create(Item4, Tpl2,  "Demo sản phẩm",       "Hoạt động ổn định", 40m, false),
            Create(Item5, Tpl3,  "Nghiên cứu thị trường","Khảo sát thực tế", 50m, false),
            Create(Item6, Tpl3,  "Lựa chọn công nghệ",  "Phù hợp bài toán", 50m, false),
            Create(Item7, Tpl4,  "Sáng tạo ý tưởng",    "Tính mới", 40m, false),
            Create(Item8, Tpl4,  "Tác động xã hội",     "Lợi ích cộng đồng", 60m, false),
            Create(Item9, Tpl5,  "Kỹ năng thuyết trình","Pitching", 50m, false),
            Create(Item10,Tpl5,  "Business model",      "Kế hoạch tài chính", 50m, false),
            Create(Item11,Tpl6,  "Sản phẩm hoàn chỉnh", "Mức độ hoàn thiện", 60m, false),
            Create(Item12,Tpl6,  "Khả năng mở rộng",    "Scalability", 40m, false),
            Create(Item13,Tpl7,  "Kiến thức nền tảng",  "Lý thuyết cơ bản", 50m, false),
            Create(Item14,Tpl7,  "Giải quyết vấn đề",   "Problem solving", 50m, false),
            Create(Item15,Tpl8,  "Chất lượng video",    "Nội dung demo", 50m, false),
            Create(Item16,Tpl8,  "Tính thuyết phục",    "Sức hút", 50m, false),
            Create(Item17,Tpl9,  "Kết quả 24h",         "Sản phẩm sau 24h", 70m, false),
            Create(Item18,Tpl9,  "Tinh thần đồng đội",  "Teamwork", 30m, false),
            Create(Item19,Tpl10, "Hồ sơ năng lực",      "CV & Portfolio", 50m, false),
            Create(Item20,Tpl10, "Kinh nghiệm dự án",   "Project experience", 50m, false),
            Create(Item21,Tpl11, "Kỹ năng giao tiếp",   "Communication", 40m, false),
            Create(Item22,Tpl11, "Kiến thức chuyên môn","Technical deep dive", 60m, false),
            Create(Item23,Tpl12, "Phản biện",           "Critical thinking", 50m, false),
            Create(Item24,Tpl12, "Thái độ",             "Attitude", 50m, false),
            Create(Item25,Tpl13, "Ý tưởng khởi đầu",    "Initial idea", 50m, false),
            Create(Item26,Tpl13, "Kế hoạch thực hiện",  "Execution plan", 50m, false),
            Create(Item27,Tpl14, "Thiết kế giải pháp",  "Solution design", 50m, false),
            Create(Item28,Tpl14, "Công nghệ sử dụng",   "Tech stack", 50m, false),
            Create(Item29,Tpl15, "Phân tích yêu cầu",   "Requirements", 50m, false),
            Create(Item30,Tpl15, "Kiến trúc hệ thống",  "System architecture", 50m, false),
            // Disabled criteria items
            Create(Item31,Tpl16, "Dự phòng A1",         "Dự phòng", 50m, true),
            Create(Item32,Tpl16, "Dự phòng A2",         "Dự phòng", 50m, true),
            Create(Item33,Tpl17, "Dự phòng B1",         "Dự phòng", 50m, true),
            Create(Item34,Tpl17, "Dự phòng B2",         "Dự phòng", 50m, true),
            Create(Item35,Tpl18, "Dự phòng C1",         "Dự phòng", 50m, true),
            Create(Item36,Tpl18, "Dự phòng C2",         "Dự phòng", 50m, true),
            Create(Item37,Tpl19, "Dự phòng D1",         "Dự phòng", 50m, true),
            Create(Item38,Tpl19, "Dự phòng D2",         "Dự phòng", 50m, true),
            Create(Item39,Tpl20, "Dự phòng E1",         "Dự phòng", 50m, true),
            Create(Item40,Tpl20, "Dự phòng E2",         "Dự phòng", 50m, true)
        );
    }

    private static CriteriaTemplates Create(Guid id, Guid roundId, string title, string desc, bool isDisable)
        => new() { Id = id, RoundId = roundId, Title = title, Description = desc, IsDisable = isDisable, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };

    private static CriteriaItems Create(Guid id, Guid tplId, string name, string desc, decimal score, bool isDisable)
        => new() { Id = id, CriteriaTemplateId = tplId, Name = name, Description = desc, Score = score, IsDisable = isDisable, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };
}
