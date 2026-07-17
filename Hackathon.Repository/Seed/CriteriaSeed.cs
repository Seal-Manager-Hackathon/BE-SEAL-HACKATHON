using System;
using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class CriteriaSeed
{
    // Criteria Templates
    public static readonly Guid Tpl1Active = Guid.Parse("22000000-0000-0000-0000-000000000001");
    public static readonly Guid Tpl2Active = Guid.Parse("22000000-0000-0000-0000-000000000002");
    public static readonly Guid Tpl3Active = Guid.Parse("22000000-0000-0000-0000-000000000003");
    public static readonly Guid Tpl4Active = Guid.Parse("22000000-0000-0000-0000-000000000004");
    public static readonly Guid Tpl5Active = Guid.Parse("22000000-0000-0000-0000-000000000005");
    public static readonly Guid Tpl6Active = Guid.Parse("22000000-0000-0000-0000-000000000006");
    public static readonly Guid Tpl7Active = Guid.Parse("22000000-0000-0000-0000-000000000007");
    public static readonly Guid Tpl8Disabled = Guid.Parse("22000000-0000-0000-0000-000000000008");
    public static readonly Guid Tpl9Disabled = Guid.Parse("22000000-0000-0000-0000-000000000009");
    public static readonly Guid Tpl10Disabled = Guid.Parse("22000000-0000-0000-0000-000000000010");

    // Criteria Items
    public static readonly Guid Item1 = Guid.Parse("23000000-0000-0000-0000-000000000001");
    public static readonly Guid Item2 = Guid.Parse("23000000-0000-0000-0000-000000000002");
    public static readonly Guid Item3 = Guid.Parse("23000000-0000-0000-0000-000000000003");
    public static readonly Guid Item4 = Guid.Parse("23000000-0000-0000-0000-000000000004");
    public static readonly Guid Item5 = Guid.Parse("23000000-0000-0000-0000-000000000005");
    public static readonly Guid Item6 = Guid.Parse("23000000-0000-0000-0000-000000000006");
    public static readonly Guid Item7 = Guid.Parse("23000000-0000-0000-0000-000000000007");
    public static readonly Guid Item8 = Guid.Parse("23000000-0000-0000-0000-000000000008");
    public static readonly Guid Item9 = Guid.Parse("23000000-0000-0000-0000-000000000009");
    public static readonly Guid Item10 = Guid.Parse("23000000-0000-0000-0000-000000000010");
    public static readonly Guid Item11 = Guid.Parse("23000000-0000-0000-0000-000000000011");
    public static readonly Guid Item12 = Guid.Parse("23000000-0000-0000-0000-000000000012");
    public static readonly Guid Item13 = Guid.Parse("23000000-0000-0000-0000-000000000013");
    public static readonly Guid Item14 = Guid.Parse("23000000-0000-0000-0000-000000000014");
    public static readonly Guid Item15 = Guid.Parse("23000000-0000-0000-0000-000000000015");

    public static void SeedCriteria(this ModelBuilder modelBuilder)
    {
        // 10 Criteria Templates
        modelBuilder.Entity<CriteriaTemplates>().HasData(
            new CriteriaTemplates { Id = Tpl1Active, RoundId = RoundSeed.RoundEvent1R1, Title = "Đánh giá Ý tưởng chính thức", Description = "Bảng đánh giá chính thức", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaTemplates { Id = Tpl2Active, RoundId = RoundSeed.RoundEvent1R2, Title = "Đánh giá Thử nghiệm chính thức", Description = "Bảng đánh giá chính thức", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaTemplates { Id = Tpl3Active, RoundId = RoundSeed.RoundEvent2R1, Title = "Đánh giá Vòng sơ loại", Description = "Bảng đánh giá chính thức", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaTemplates { Id = Tpl4Active, RoundId = RoundSeed.RoundEvent2R2, Title = "Đánh giá Vòng chung kết", Description = "Bảng đánh giá chính thức", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaTemplates { Id = Tpl5Active, RoundId = RoundSeed.RoundEvent3R1, Title = "Đánh giá Vòng đấu duy nhất", Description = "Bảng đánh giá chính thức", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaTemplates { Id = Tpl6Active, RoundId = RoundSeed.RoundEvent4R1, Title = "Đánh giá Ý tưởng hè", Description = "Bảng đánh giá chính thức", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaTemplates { Id = Tpl7Active, RoundId = RoundSeed.RoundEvent4R2, Title = "Đánh giá Thuyết trình hè", Description = "Bảng đánh giá chính thức", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaTemplates { Id = Tpl8Disabled, RoundId = RoundSeed.RoundEvent1R1, Title = "Bảng dự phòng A", Description = "Bảng dự phòng", IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaTemplates { Id = Tpl9Disabled, RoundId = RoundSeed.RoundEvent1R2, Title = "Bảng dự phòng B", Description = "Bảng dự phòng", IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaTemplates { Id = Tpl10Disabled, RoundId = RoundSeed.RoundEvent2R1, Title = "Bảng dự phòng C", Description = "Bảng dự phòng", IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );

        // 15 Criteria Items
        modelBuilder.Entity<CriteriaItems>().HasData(
            new CriteriaItems { Id = Item1, CriteriaTemplateId = Tpl1Active, Name = "Ý tưởng sáng tạo", Description = "Tính mới lạ của ý tưởng", Score = 30m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaItems { Id = Item2, CriteriaTemplateId = Tpl1Active, Name = "Tính khả thi", Description = "Khả năng phát triển thực tế", Score = 30m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaItems { Id = Item3, CriteriaTemplateId = Tpl1Active, Name = "Giá trị xã hội", Description = "Lợi ích mang lại", Score = 20m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaItems { Id = Item4, CriteriaTemplateId = Tpl1Active, Name = "Tài liệu thiết kế", Description = "Đầy đủ tài liệu", Score = 20m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaItems { Id = Item5, CriteriaTemplateId = Tpl2Active, Name = "Chất lượng code", Description = "Kiến trúc và style", Score = 40m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaItems { Id = Item6, CriteriaTemplateId = Tpl2Active, Name = "Demo sản phẩm", Description = "Hoạt động ổn định", Score = 40m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaItems { Id = Item7, CriteriaTemplateId = Tpl2Active, Name = "Trả lời câu hỏi", Description = "Phản biện với giám khảo", Score = 20m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaItems { Id = Item8, CriteriaTemplateId = Tpl3Active, Name = "Nghiên cứu thị trường", Description = "Khảo sát thực tế", Score = 50m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaItems { Id = Item9, CriteriaTemplateId = Tpl3Active, Name = "Lựa chọn công nghệ", Description = "Phù hợp bài toán", Score = 50m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaItems { Id = Item10, CriteriaTemplateId = Tpl4Active, Name = "Pitching", Description = "Kỹ năng thuyết trình", Score = 40m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaItems { Id = Item11, CriteriaTemplateId = Tpl4Active, Name = "Business model", Description = "Kế hoạch tài chính", Score = 60m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaItems { Id = Item12, CriteriaTemplateId = Tpl5Active, Name = "Tiêu chí duy nhất A", Description = "Đánh giá chung", Score = 50m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaItems { Id = Item13, CriteriaTemplateId = Tpl5Active, Name = "Tiêu chí duy nhất B", Description = "Đánh giá chung", Score = 50m, IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaItems { Id = Item14, CriteriaTemplateId = Tpl8Disabled, Name = "Tính sáng tạo nháp", Description = "Nháp", Score = 50m, IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new CriteriaItems { Id = Item15, CriteriaTemplateId = Tpl8Disabled, Name = "Khả thi nháp", Description = "Nháp", Score = 50m, IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );
    }
}
