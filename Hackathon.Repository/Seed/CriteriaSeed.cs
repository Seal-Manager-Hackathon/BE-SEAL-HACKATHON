using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class CriteriaSeed
{
    public static void SeedCriteria(this ModelBuilder modelBuilder)
    {
        var c = SeedConstants.CreatedAt;

        // 30 CriteriaTemplates
        modelBuilder.Entity<CriteriaTemplates>().HasData(
            // E2R1
            Create(SeedConstants.Ct1, SeedConstants.RoundE2R1, "Đánh giá ý tưởng", "Tính sáng tạo và khả thi của ý tưởng"),
            Create(SeedConstants.Ct2, SeedConstants.RoundE2R1, "Đánh giá kỹ thuật", "Chất lượng kỹ thuật và công nghệ"),
            // E2R2
            Create(SeedConstants.Ct3, SeedConstants.RoundE2R2, "Đánh giá sản phẩm", "Chất lượng sản phẩm hoàn thiện"),
            // E2R3
            Create(SeedConstants.Ct4, SeedConstants.RoundE2R3, "Đánh giá tổng quan", "Đánh giá tổng quan dự án"),
            // E3R1
            Create(SeedConstants.Ct5, SeedConstants.RoundE3R1, "Chấm điểm sơ loại", "Tiêu chí chấm vòng sơ loại"),
            Create(SeedConstants.Ct6, SeedConstants.RoundE3R1, "Chấm điểm bổ sung", "Tiêu chí bổ sung"),
            // E3R2
            Create(SeedConstants.Ct7, SeedConstants.RoundE3R2, "Chấm bán kết", "Tiêu chí vòng bán kết"),
            // E3R3
            Create(SeedConstants.Ct8, SeedConstants.RoundE3R3, "Chấm chung kết", "Tiêu chí vòng chung kết"),
            // E4R1
            Create(SeedConstants.Ct9, SeedConstants.RoundE4R1, "Đánh giá ý tưởng", "Sáng tạo và tác động"),
            Create(SeedConstants.Ct10, SeedConstants.RoundE4R1, "Đánh giá kỹ thuật R1", "Kỹ thuật vòng 1"),
            // E4R2
            Create(SeedConstants.Ct11, SeedConstants.RoundE4R2, "Đánh giá phát triển", "Quá trình phát triển"),
            // E4R3
            Create(SeedConstants.Ct12, SeedConstants.RoundE4R3, "Chấm chung kết", "Chung kết"),
            // E6R1
            Create(SeedConstants.Ct13, SeedConstants.RoundE6R1, "Sơ loại", "Sơ loại"),
            Create(SeedConstants.Ct14, SeedConstants.RoundE6R2, "Chung kết", "Chung kết"),
            // E7R1
            Create(SeedConstants.Ct15, SeedConstants.RoundE7R1, "Khởi động", "Vòng khởi động"),
            Create(SeedConstants.Ct16, SeedConstants.RoundE7R1, "Bổ sung khởi động", "Tiêu chí bổ sung"),
            // E7R2
            Create(SeedConstants.Ct17, SeedConstants.RoundE7R2, "Bán kết", "Vòng bán kết"),
            // E7R3
            Create(SeedConstants.Ct18, SeedConstants.RoundE7R3, "Chung kết E7", "Chung kết"),
            // E10R1
            Create(SeedConstants.Ct19, SeedConstants.RoundE10R1, "Khởi động E10", "Vòng khởi động E10"),
            Create(SeedConstants.Ct20, SeedConstants.RoundE10R1, "Bổ sung E10", "Bổ sung"),
            // E10R2
            Create(SeedConstants.Ct21, SeedConstants.RoundE10R2, "Chung kết E10", "Chung kết E10"),
            // Disabled templates
            Create(SeedConstants.Ct22, SeedConstants.RoundE2R1, "Template cũ E2R1", "Đã disable", true),
            Create(SeedConstants.Ct23, SeedConstants.RoundE4R1, "Template cũ E4R1", "Đã disable", true),
            Create(SeedConstants.Ct24, SeedConstants.RoundE7R1, "Template cũ E7R1", "Đã disable", true),
            Create(SeedConstants.Ct25, SeedConstants.RoundE10R1, "Template cũ E10R1", "Đã disable", true),
            Create(SeedConstants.Ct26, SeedConstants.RoundE3R1, "Template cũ E3R1", "Đã disable", true),
            Create(SeedConstants.Ct27, SeedConstants.RoundE6R1, "Template cũ E6R1", "Đã disable", true),
            Create(SeedConstants.Ct28, SeedConstants.RoundE9R1, "Template E9R1", "Vòng 1 E9 - không có score nào dùng"),
            Create(SeedConstants.Ct29, SeedConstants.RoundE5R1, "Template E5R1", "Vòng 1 E5 - draft"),
            Create(SeedConstants.Ct30, SeedConstants.RoundE8R1, "Template E8R1", "Vòng 1 E8 - draft")
        );

        // 60 CriteriaItems (2 per template)
        modelBuilder.Entity<CriteriaItems>().HasData(
            // Ct1 (E2R1 - Ý tưởng)
            Create(SeedConstants.Item1, SeedConstants.Ct1, "Sáng tạo", "Tính sáng tạo của ý tưởng", 25m),
            Create(SeedConstants.Item2, SeedConstants.Ct1, "Khả thi", "Tính khả thi của ý tưởng", 25m),
            // Ct2 (E2R1 - Kỹ thuật)
            Create(SeedConstants.Item3, SeedConstants.Ct2, "Công nghệ", "Công nghệ sử dụng", 20m),
            Create(SeedConstants.Item4, SeedConstants.Ct2, "Kiến trúc", "Kiến trúc hệ thống", 20m),
            // Ct3 (E2R2 - Sản phẩm)
            Create(SeedConstants.Item5, SeedConstants.Ct3, "UI/UX", "Giao diện người dùng", 30m),
            Create(SeedConstants.Item6, SeedConstants.Ct3, "Chức năng", "Đầy đủ chức năng", 30m),
            // Ct4 (E2R3 - Tổng quan)
            Create(SeedConstants.Item7, SeedConstants.Ct4, "Hoàn thiện", "Mức độ hoàn thiện", 40m),
            Create(SeedConstants.Item8, SeedConstants.Ct4, "Thuyết trình", "Kỹ năng thuyết trình", 40m),
            // Ct5 (E3R1 - Sơ loại)
            Create(SeedConstants.Item9, SeedConstants.Ct5, "Chất lượng", "Chất lượng bài nộp", 25m),
            Create(SeedConstants.Item10, SeedConstants.Ct5, "Đúng hạn", "Nộp đúng hạn", 25m),
            // Ct6 (E3R1 - Bổ sung)
            Create(SeedConstants.Item11, SeedConstants.Ct6, "Sáng tạo BS", "Sáng tạo bổ sung", 15m),
            Create(SeedConstants.Item12, SeedConstants.Ct6, "Tác động", "Tác động xã hội", 15m),
            // Ct7 (E3R2)
            Create(SeedConstants.Item13, SeedConstants.Ct7, "Phát triển", "Tiến độ phát triển", 30m),
            Create(SeedConstants.Item14, SeedConstants.Ct7, "Hợp tác", "Tinh thần đồng đội", 30m),
            // Ct8 (E3R3)
            Create(SeedConstants.Item15, SeedConstants.Ct8, "Kết quả", "Kết quả cuối cùng", 50m),
            Create(SeedConstants.Item16, SeedConstants.Ct8, "Ấn tượng", "Ấn tượng tổng thể", 50m),
            // Ct9 (E4R1)
            Create(SeedConstants.Item17, SeedConstants.Ct9, "Ý tưởng", "Ý tưởng kinh doanh", 25m),
            Create(SeedConstants.Item18, SeedConstants.Ct9, "Tác động XH", "Tác động xã hội", 25m),
            // Ct10 (E4R1)
            Create(SeedConstants.Item19, SeedConstants.Ct10, "Kỹ thuật", "Kỹ thuật", 25m),
            Create(SeedConstants.Item20, SeedConstants.Ct10, "Dữ liệu", "Xử lý dữ liệu", 25m),
            // Ct11 (E4R2)
            Create(SeedConstants.Item21, SeedConstants.Ct11, "Tiến độ", "Tiến độ", 35m),
            Create(SeedConstants.Item22, SeedConstants.Ct11, "Chất lượng code", "Code quality", 35m),
            // Ct12 (E4R3)
            Create(SeedConstants.Item23, SeedConstants.Ct12, "Sản phẩm", "Sản phẩm cuối", 45m),
            Create(SeedConstants.Item24, SeedConstants.Ct12, "Demo", "Phần demo", 45m),
            // Ct13 (E6R1)
            Create(SeedConstants.Item25, SeedConstants.Ct13, "Cơ bản", "Đánh giá cơ bản", 20m),
            Create(SeedConstants.Item26, SeedConstants.Ct13, "Nâng cao", "Đánh giá nâng cao", 20m),
            // Ct14 (E6R2)
            Create(SeedConstants.Item27, SeedConstants.Ct14, "Chung kết 1", "Tiêu chí 1", 40m),
            Create(SeedConstants.Item28, SeedConstants.Ct14, "Chung kết 2", "Tiêu chí 2", 40m),
            // Ct15 (E7R1)
            Create(SeedConstants.Item29, SeedConstants.Ct15, "Khởi động 1", "Tiêu chí 1", 20m),
            Create(SeedConstants.Item30, SeedConstants.Ct15, "Khởi động 2", "Tiêu chí 2", 20m),
            // Ct16 (E7R1)
            Create(SeedConstants.Item31, SeedConstants.Ct16, "BS Khởi động 1", "Bổ sung 1", 15m),
            Create(SeedConstants.Item32, SeedConstants.Ct16, "BS Khởi động 2", "Bổ sung 2", 15m),
            // Ct17 (E7R2)
            Create(SeedConstants.Item33, SeedConstants.Ct17, "Bán kết 1", "Tiêu chí 1", 30m),
            Create(SeedConstants.Item34, SeedConstants.Ct17, "Bán kết 2", "Tiêu chí 2", 30m),
            // Ct18 (E7R3)
            Create(SeedConstants.Item35, SeedConstants.Ct18, "Chung kết 1", "Tiêu chí 1", 45m),
            Create(SeedConstants.Item36, SeedConstants.Ct18, "Chung kết 2", "Tiêu chí 2", 45m),
            // Ct19 (E10R1)
            Create(SeedConstants.Item37, SeedConstants.Ct19, "E10 R1-1", "Tiêu chí 1", 25m),
            Create(SeedConstants.Item38, SeedConstants.Ct19, "E10 R1-2", "Tiêu chí 2", 25m),
            // Ct20 (E10R1)
            Create(SeedConstants.Item39, SeedConstants.Ct20, "E10 R1 BS-1", "Bổ sung 1", 15m),
            Create(SeedConstants.Item40, SeedConstants.Ct20, "E10 R1 BS-2", "Bổ sung 2", 15m),
            // Ct21 (E10R2)
            Create(SeedConstants.Item41, SeedConstants.Ct21, "E10 CK-1", "Tiêu chí 1", 40m),
            Create(SeedConstants.Item42, SeedConstants.Ct21, "E10 CK-2", "Tiêu chí 2", 40m),
            // Disabled - Ct22
            Create(SeedConstants.Item43, SeedConstants.Ct22, "Cũ 1", "Item cũ", 10m, true),
            Create(SeedConstants.Item44, SeedConstants.Ct22, "Cũ 2", "Item cũ", 10m, true),
            // Disabled - Ct23
            Create(SeedConstants.Item45, SeedConstants.Ct23, "Cũ E4 1", "Item cũ", 10m, true),
            Create(SeedConstants.Item46, SeedConstants.Ct23, "Cũ E4 2", "Item cũ", 10m, true),
            // Disabled - Ct24
            Create(SeedConstants.Item47, SeedConstants.Ct24, "Cũ E7 1", "Item cũ", 10m, true),
            Create(SeedConstants.Item48, SeedConstants.Ct24, "Cũ E7 2", "Item cũ", 10m, true),
            // Disabled - Ct25
            Create(SeedConstants.Item49, SeedConstants.Ct25, "Cũ E10 1", "Item cũ", 10m, true),
            Create(SeedConstants.Item50, SeedConstants.Ct25, "Cũ E10 2", "Item cũ", 10m, true),
            // Disabled - Ct26
            Create(SeedConstants.Item51, SeedConstants.Ct26, "Cũ E3 1", "Item cũ", 10m, true),
            Create(SeedConstants.Item52, SeedConstants.Ct26, "Cũ E3 2", "Item cũ", 10m, true),
            // Disabled - Ct27
            Create(SeedConstants.Item53, SeedConstants.Ct27, "Cũ E6 1", "Item cũ", 10m, true),
            Create(SeedConstants.Item54, SeedConstants.Ct27, "Cũ E6 2", "Item cũ", 10m, true),
            // Ct28 (E9R1 - no scores)
            Create(SeedConstants.Item55, SeedConstants.Ct28, "E9R1-1", "Item 1", 20m),
            Create(SeedConstants.Item56, SeedConstants.Ct28, "E9R1-2", "Item 2", 20m),
            // Ct29 (E5R1 - draft)
            Create(SeedConstants.Item57, SeedConstants.Ct29, "E5R1-1", "Item 1", 20m),
            Create(SeedConstants.Item58, SeedConstants.Ct29, "E5R1-2", "Item 2", 20m),
            // Ct30 (E8R1 - draft)
            Create(SeedConstants.Item59, SeedConstants.Ct30, "E8R1-1", "Item 1", 20m),
            Create(SeedConstants.Item60, SeedConstants.Ct30, "E8R1-2", "Item 2", 20m)
        );
    }

    private static CriteriaTemplates Create(Guid id, Guid roundId, string title, string desc, bool isDisable = false) => new()
    {
        Id = id, RoundId = roundId, Title = title, Description = desc,
        IsDisable = isDisable, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };

    private static CriteriaItems Create(Guid id, Guid templateId, string name, string desc, decimal score, bool isDisable = false) => new()
    {
        Id = id, CriteriaTemplateId = templateId, Name = name, Description = desc, Score = score,
        IsDisable = isDisable, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };
}
