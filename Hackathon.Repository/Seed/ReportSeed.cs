using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class ReportSeed
{
    public static void SeedReports(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reports>().HasData(
            // 20 Reports covering all statuses and types
            // Pending reports
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000001"), UserId = SeedConstants.UserStudentLeader1, Title = "Nộp bài muộn do lỗi mạng", Description = "Không thể nộp bài đúng hạn do mạng trường mất kết nối", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "LateSubmission", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000002"), UserId = SeedConstants.UserStudentLeader2, Title = "Yêu cầu phúc khảo", Description = "Điểm số không phản ánh đúng chất lượng bài làm", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "RegradeRequest", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // Resolved reports
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000003"), UserId = SeedConstants.UserStudentLeader3, Title = "Lỗi file nộp", Description = "File nộp bị hỏng không mở được", Status = ReportStatusEnum.Resolved, Reason = "Đã hỗ trợ nộp lại file mới", TypeReport = "CorruptedFile", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000004"), UserId = SeedConstants.UserJudgeActive, Title = "Nghi vấn đạo văn", Description = "Phát hiện code giống với dự án khác", Status = ReportStatusEnum.Resolved, Reason = "Đã trừ 50% điểm số", TypeReport = "Plagiarism", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000005"), UserId = SeedConstants.UserStudentMember1, Title = "Phúc khảo điểm số", Description = "Mong muốn chấm lại bài vòng 1", Status = ReportStatusEnum.Resolved, Reason = "Đã đồng ý phúc khảo", TypeReport = "RegradeRequest", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // Rejected reports
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000006"), UserId = SeedConstants.UserStudentLeader4, Title = "Vi phạm quy chế", Description = "Đội thi dùng code mua từ bên ngoài", Status = ReportStatusEnum.Reject, Reason = "Không đủ bằng chứng xác thực", TypeReport = "RuleViolation", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000007"), UserId = SeedConstants.UserMentorActive, Title = "Đề xuất thay đổi đề tài", Description = "Đề tài hiện tại quá khó với sinh viên", Status = ReportStatusEnum.Reject, Reason = "Đã quá hạn thay đổi đề tài", TypeReport = "TopicChange", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // Canceled reports
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000008"), UserId = SeedConstants.UserStudentLeader5, Title = "Báo cáo trùng lặp", Description = "Đã gửi nhầm, đây là báo cáo trùng", Status = ReportStatusEnum.Canceled, Reason = "Người dùng tự hủy", TypeReport = "Duplicate", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000009"), UserId = SeedConstants.UserStudentLeader6, Title = "Rút lui khỏi giải", Description = "Lý do sức khỏe không thể tiếp tục", Status = ReportStatusEnum.Canceled, Reason = "Đã xác nhận rút lui", TypeReport = "Withdrawal", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // More pending reports (various types)
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000010"), UserId = SeedConstants.UserStudentLeader7, Title = "Báo cáo lỗi hệ thống", Description = "Trang nộp bài bị lỗi không upload được", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "SystemBug", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000011"), UserId = SeedConstants.UserStudentMember2, Title = "Khiếu nại điểm", Description = "Điểm vòng 1 thấp hơn dự kiến", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "ScoreComplaint", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000012"), UserId = SeedConstants.UserStudentMember3, Title = "Đổi ý tưởng", Description = "Muốn đổi chủ đề dự án", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "TopicChange", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000013"), UserId = SeedConstants.UserStudentMember4, Title = "Báo cáo gian lận", Description = "Phát hiện đội bạn dùng điện thoại trong giờ thi", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "Cheating", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000014"), UserId = SeedConstants.UserStudentMember5, Title = "Đề xuất cải tiến", Description = "Nên bổ sung tính năng chat trực tiếp", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "Suggestion", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // More resolved
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000015"), UserId = SeedConstants.UserStudentMember6, Title = "Xin gia hạn", Description = "Cần thêm 2 ngày để hoàn thiện", Status = ReportStatusEnum.Resolved, Reason = "Đã gia hạn thêm 2 ngày", TypeReport = "Extension", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000016"), UserId = SeedConstants.UserStudentMember7, Title = "Lỗi tài liệu", Description = "Tài liệu hướng dẫn không rõ ràng", Status = ReportStatusEnum.Resolved, Reason = "Đã cập nhật tài liệu", TypeReport = "Documentation", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // More rejected
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000017"), UserId = SeedConstants.UserStudentMember8, Title = "Khiếu nại kết quả", Description = "Kết quả chung cuộc không công bằng", Status = ReportStatusEnum.Reject, Reason = "Kết quả đã được hội đồng xác nhận", TypeReport = "ResultComplaint", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000018"), UserId = SeedConstants.UserStudentMember9, Title = "Báo cáo spam", Description = "Nội dung không liên quan", Status = ReportStatusEnum.Reject, Reason = "Không đúng định dạng báo cáo", TypeReport = "Spam", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // Disabled report
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000019"), UserId = SeedConstants.UserStudentLeader1, Title = "Báo cáo nháp", Description = "Bản nháp chưa hoàn chỉnh", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "Draft", IsDisable = true, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt },
            // Banned student report
            new Reports { Id = Guid.Parse("91000000-0000-0000-0000-000000000020"), UserId = SeedConstants.UserStudentBanned, Title = "Kháng cáo khóa tài khoản", Description = "Mong muốn được mở lại tài khoản để tham gia kỳ sau", Status = ReportStatusEnum.Pending, Reason = null, TypeReport = "BanAppeal", IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt }
        );
    }
}
