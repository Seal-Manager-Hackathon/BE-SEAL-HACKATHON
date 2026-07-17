using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

// Report IDs: 39000000-xxxx
public static class ReportSeed
{
    public static readonly Guid Rpt1 = Guid.Parse("39000000-0000-0000-0000-000000000001");
    public static readonly Guid Rpt2 = Guid.Parse("39000000-0000-0000-0000-000000000002");
    public static readonly Guid Rpt3 = Guid.Parse("39000000-0000-0000-0000-000000000003");
    public static readonly Guid Rpt4 = Guid.Parse("39000000-0000-0000-0000-000000000004");
    public static readonly Guid Rpt5 = Guid.Parse("39000000-0000-0000-0000-000000000005");
    public static readonly Guid Rpt6 = Guid.Parse("39000000-0000-0000-0000-000000000006");
    public static readonly Guid Rpt7 = Guid.Parse("39000000-0000-0000-0000-000000000007");
    public static readonly Guid Rpt8 = Guid.Parse("39000000-0000-0000-0000-000000000008");
    public static readonly Guid Rpt9 = Guid.Parse("39000000-0000-0000-0000-000000000009");
    public static readonly Guid Rpt10 = Guid.Parse("39000000-0000-0000-0000-000000000010");
    public static readonly Guid Rpt11 = Guid.Parse("39000000-0000-0000-0000-000000000011");
    public static readonly Guid Rpt12 = Guid.Parse("39000000-0000-0000-0000-000000000012");
    public static readonly Guid Rpt13 = Guid.Parse("39000000-0000-0000-0000-000000000013");
    public static readonly Guid Rpt14 = Guid.Parse("39000000-0000-0000-0000-000000000014");
    public static readonly Guid Rpt15 = Guid.Parse("39000000-0000-0000-0000-000000000015");
    public static readonly Guid Rpt16 = Guid.Parse("39000000-0000-0000-0000-000000000016");
    public static readonly Guid Rpt17 = Guid.Parse("39000000-0000-0000-0000-000000000017");
    public static readonly Guid Rpt18 = Guid.Parse("39000000-0000-0000-0000-000000000018");
    public static readonly Guid Rpt19 = Guid.Parse("39000000-0000-0000-0000-000000000019");
    public static readonly Guid Rpt20 = Guid.Parse("39000000-0000-0000-0000-000000000020");
    public static readonly Guid Rpt21 = Guid.Parse("39000000-0000-0000-0000-000000000021");
    public static readonly Guid Rpt22 = Guid.Parse("39000000-0000-0000-0000-000000000022");
    public static readonly Guid Rpt23 = Guid.Parse("39000000-0000-0000-0000-000000000023");
    public static readonly Guid Rpt24 = Guid.Parse("39000000-0000-0000-0000-000000000024");
    public static readonly Guid Rpt25 = Guid.Parse("39000000-0000-0000-0000-000000000025");
    public static readonly Guid Rpt26 = Guid.Parse("39000000-0000-0000-0000-000000000026");
    public static readonly Guid Rpt27 = Guid.Parse("39000000-0000-0000-0000-000000000027");
    public static readonly Guid Rpt28 = Guid.Parse("39000000-0000-0000-0000-000000000028");
    public static readonly Guid Rpt29 = Guid.Parse("39000000-0000-0000-0000-000000000029");
    public static readonly Guid Rpt30 = Guid.Parse("39000000-0000-0000-0000-000000000030");

    public static void SeedReports(this ModelBuilder modelBuilder)
    {
        var c = SeedConstants.CreatedAt;

        modelBuilder.Entity<Reports>().HasData(
            // Phúc khảo (appeal) reports — for regrade flow
            Create(Rpt1, SeedConstants.UserStudentLeader1, "Phúc khảo điểm vòng 1", "Em nghĩ bài em làm tốt hơn điểm được nhận", ReportStatusEnum.Pending, "Phúc khảo"),
            Create(Rpt2, SeedConstants.UserStudentLeader2, "Phúc khảo điểm vòng 2", "Điểm thấp hơn so với kỳ vọng", ReportStatusEnum.Pending, "Phúc khảo"),
            Create(Rpt3, SeedConstants.UserStudentLeader3, "Phúc khảo bài nộp", "Nhờ xem lại bài", ReportStatusEnum.Resolved, "Phúc khảo"),
            Create(Rpt4, SeedConstants.UserStudentLeader4, "Phúc khảo lần cuối", "Mong được xem xét", ReportStatusEnum.Reject, "Phúc khảo"),
            // Technical issue reports
            Create(Rpt5, SeedConstants.UserStudentMember1, "Lỗi hệ thống nộp bài", "Không nộp được bài đúng hạn", ReportStatusEnum.Pending, "Kỹ thuật"),
            Create(Rpt6, SeedConstants.UserStudentMember2, "Lỗi đăng nhập", "Không thể đăng nhập vào hệ thống", ReportStatusEnum.Resolved, "Kỹ thuật"),
            Create(Rpt7, SeedConstants.UserStudentMember3, "Lỗi hiển thị điểm", "Điểm không hiển thị đúng", ReportStatusEnum.Pending, "Kỹ thuật"),
            // Policy / rule violation reports
            Create(Rpt8, SeedConstants.UserStudentLeader5, "Báo cáo vi phạm", "Team khác copy ý tưởng", ReportStatusEnum.Pending, "Vi phạm"),
            Create(Rpt9, SeedConstants.UserStudentLeader6, "Sao chép code", "Phát hiện team dùng code giống nhau", ReportStatusEnum.Resolved, "Vi phạm"),
            Create(Rpt10, SeedConstants.UserStudentMember4, "Spam trong hệ thống", "Một số thành viên spam tin nhắn", ReportStatusEnum.Canceled, "Vi phạm"),
            // Other / general reports
            Create(Rpt11, SeedConstants.UserStudentLeader7, "Góp ý event", "Nên kéo dài thời gian nộp bài", ReportStatusEnum.Resolved, "Góp ý"),
            Create(Rpt12, SeedConstants.UserStudentMember5, "Thắc mắc về luật", "Không hiểu rõ về vòng loại", ReportStatusEnum.Pending, "Thắc mắc"),
            Create(Rpt13, SeedConstants.UserStudentLeader8, "Yêu cầu hỗ trợ", "Cần mentor hướng dẫn thêm", ReportStatusEnum.Pending, "Hỗ trợ"),
            Create(Rpt14, SeedConstants.UserStudentLeader9, "Báo mất tài khoản", "Bị mất quyền truy cập", ReportStatusEnum.Resolved, "Bảo mật"),
            Create(Rpt15, SeedConstants.UserStudentMember6, "Lỗi giao diện", "Giao diện mobile bị vỡ", ReportStatusEnum.Pending, "Kỹ thuật"),
            // More edge case reports
            Create(Rpt16, SeedConstants.UserStaff1, "Báo cáo nội bộ", "Kiểm tra hoạt động staff", ReportStatusEnum.Pending, "Nội bộ"),
            Create(Rpt17, SeedConstants.UserJudge1, "Lỗi chấm điểm", "Không thể nhập điểm được", ReportStatusEnum.Resolved, "Kỹ thuật"),
            Create(Rpt18, SeedConstants.UserStaffBanned, "Khiếu nại", "Yêu cầu mở lại tài khoản", ReportStatusEnum.Pending, "Khiếu nại"),
            Create(Rpt19, SeedConstants.UserStudentMember7, "Phúc khảo vòng chung kết", "Xin chấm lại bài vòng chung kết", ReportStatusEnum.Pending, "Phúc khảo"),
            Create(Rpt20, SeedConstants.UserStudentMember8, "Báo cáo sai sót", "Thông tin đội bị sai", ReportStatusEnum.Reject, "Khác"),
            // More reports to hit 30
            Create(Rpt21, SeedConstants.UserStudentMember9, "Phúc khảo E3", "Xin review lại điểm E3", ReportStatusEnum.Pending, "Phúc khảo"),
            Create(Rpt22, SeedConstants.UserStudentMember10, "Phúc khảo E6", "Điểm E6 không chính xác", ReportStatusEnum.Resolved, "Phúc khảo"),
            Create(Rpt23, SeedConstants.UserStudentMember11, "Yêu cầu rời đội", "Muốn rời khỏi đội hiện tại", ReportStatusEnum.Pending, "Hỗ trợ"),
            Create(Rpt24, SeedConstants.UserStudentMember12, "Báo mất dữ liệu", "Bài nộp bị mất", ReportStatusEnum.Canceled, "Kỹ thuật"),
            Create(Rpt25, SeedConstants.UserStudentMember13, "Phúc khảo lần 2", "Phúc khảo lần 2 cho bài vòng 1", ReportStatusEnum.Pending, "Phúc khảo"),
            Create(Rpt26, SeedConstants.UserStudentMember14, "Góp ý tổ chức", "Công tác tổ chức cần cải thiện", ReportStatusEnum.Resolved, "Góp ý"),
            Create(Rpt27, SeedConstants.UserStudentMember15, "Báo cáo bug", "Bug ở trang nộp bài", ReportStatusEnum.Pending, "Kỹ thuật"),
            Create(Rpt28, SeedConstants.UserStudentMember16, "Phúc khảo E9", "Điểm E9 thấp bất thường", ReportStatusEnum.Pending, "Phúc khảo"),
            Create(Rpt29, SeedConstants.UserStudentMember17, "Yêu cầu hỗ trợ kỹ thuật", "Cần giúp đỡ về môi trường dev", ReportStatusEnum.Pending, "Hỗ trợ"),
            Create(Rpt30, SeedConstants.UserStudentMember18, "Phản ánh mentor", "Mentor không hỗ trợ nhiệt tình", ReportStatusEnum.Pending, "Phản ánh")
        );
    }

    private static Reports Create(Guid id, Guid userId, string title, string description, ReportStatusEnum status, string typeReport) => new()
    {
        Id = id, UserId = userId, Title = title, Description = description,
        Status = status, TypeReport = typeReport,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };
}
