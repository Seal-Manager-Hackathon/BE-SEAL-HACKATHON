using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class ReportSeed
{
    public static void SeedReports(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reports>().HasData(
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000001"), UserId = SeedConstants.UserStudentLeaderActive1, Title = "Báo cáo nộp muộn", Description = "Lỗi mạng khi nộp bài", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "LateSubmission", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000002"), UserId = SeedConstants.UserStudentLeaderActive2, Title = "Lỗi file nộp", Description = "File nộp bị hỏng định dạng", Status = ReportStatusEnum.Resolved, Reason = "Đã hỗ trợ nộp lại", TypeReport = "CorruptedFile", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000003"), UserId = SeedConstants.UserJudgeActive, Title = "Nghi vấn đạo văn", Description = "Nội dung giống dự án khác 80%", Status = ReportStatusEnum.Resolved, Reason = "Bị trừ 50% số điểm", TypeReport = "Plagiarism", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000004"), UserId = SeedConstants.UserMentorActive, Title = "Đề xuất phúc khảo", Description = "Mong muốn chấm lại bài", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "RegradeRequest", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000005"), UserId = SeedConstants.UserStudentLeaderActive3, Title = "Báo cáo lỗi UI", Description = "Trang nộp bài bị crash", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "SystemBug", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000006"), UserId = SeedConstants.UserJudgeInactive, Title = "Vi phạm quy chế", Description = "Đội thi dùng code có sẵn không xin phép", Status = ReportStatusEnum.Reject, Reason = "Không đủ bằng chứng", TypeReport = "RuleViolation", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000007"), UserId = SeedConstants.UserStudentMemberActive1, Title = "Đổi ý tưởng", Description = "Xin phép đổi đề tài", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "TopicChange", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000008"), UserId = SeedConstants.UserStudentMemberActive2, Title = "Rút lui khỏi giải", Description = "Lý do sức khỏe", Status = ReportStatusEnum.Resolved, Reason = "Đã xác nhận", TypeReport = "Withdrawal", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000009"), UserId = SeedConstants.UserJudgeActive, Title = "Code không chạy", Description = "Thiếu readme và hướng dẫn cài đặt", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "InvalidSubmission", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000010"), UserId = SeedConstants.UserMentorActive, Title = "Độ trễ phản hồi", Description = "Đội thi phản hồi chậm", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "LowEngagement", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000011"), UserId = SeedConstants.UserStudentLeaderActive1, Title = "Báo cáo nháp", Description = "Nháp", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "DraftReport", IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000012"), UserId = SeedConstants.UserStudentMemberBanned3, Title = "Kháng cáo khóa tài khoản", Description = "Mong muốn được mở lại tài khoản", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "BanAppeal", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );
    }
}
