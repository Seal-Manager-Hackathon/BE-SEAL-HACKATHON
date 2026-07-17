using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class AuthSeed
{
    public static void SeedAuthData(this ModelBuilder modelBuilder)
    {
        // 15 Refresh Tokens
        modelBuilder.Entity<RefreshTokens>().HasData(
            CreateRt(Guid.Parse("70000000-0000-0000-0000-000000000001"), SeedConstants.UserAdminActive,      false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRt(Guid.Parse("70000000-0000-0000-0000-000000000002"), SeedConstants.UserStaffActive,     false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRt(Guid.Parse("70000000-0000-0000-0000-000000000003"), SeedConstants.UserJudgeActive,     false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRt(Guid.Parse("70000000-0000-0000-0000-000000000004"), SeedConstants.UserMentorActive,    false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRt(Guid.Parse("70000000-0000-0000-0000-000000000005"), SeedConstants.UserStudentLeader1,  false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRt(Guid.Parse("70000000-0000-0000-0000-000000000006"), SeedConstants.UserStudentLeader2,  false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRt(Guid.Parse("70000000-0000-0000-0000-000000000007"), SeedConstants.UserStudentLeader3,  false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRt(Guid.Parse("70000000-0000-0000-0000-000000000008"), SeedConstants.UserStudentMember1,  false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRt(Guid.Parse("70000000-0000-0000-0000-000000000009"), SeedConstants.UserStudentMember2,  false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRt(Guid.Parse("70000000-0000-0000-0000-000000000010"), SeedConstants.UserAdminActive,     true,  SeedConstants.CreatedAt.AddDays(7)),  // disabled
            CreateRt(Guid.Parse("70000000-0000-0000-0000-000000000011"), SeedConstants.UserStaffActive,     false, SeedConstants.CreatedAt.AddDays(-1)), // expired
            CreateRt(Guid.Parse("70000000-0000-0000-0000-000000000012"), SeedConstants.UserStudentInactive, false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRt(Guid.Parse("70000000-0000-0000-0000-000000000013"), SeedConstants.UserJudgeBanned,     false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRt(Guid.Parse("70000000-0000-0000-0000-000000000014"), SeedConstants.UserMentorBanned,    false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRt(Guid.Parse("70000000-0000-0000-0000-000000000015"), SeedConstants.UserStudentBanned,   false, SeedConstants.CreatedAt.AddDays(7))
        );

        // 15 Reset Passwords
        modelBuilder.Entity<ResetPasswords>().HasData(
            CreateRp(Guid.Parse("71000000-0000-0000-0000-000000000001"), SeedConstants.UserAdminActive,      false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateRp(Guid.Parse("71000000-0000-0000-0000-000000000002"), SeedConstants.UserStaffActive,     true,  false, SeedConstants.CreatedAt.AddHours(2)),   // used
            CreateRp(Guid.Parse("71000000-0000-0000-0000-000000000003"), SeedConstants.UserJudgeActive,     false, false, SeedConstants.CreatedAt.AddHours(-1)),  // expired
            CreateRp(Guid.Parse("71000000-0000-0000-0000-000000000004"), SeedConstants.UserMentorActive,    false, true,  SeedConstants.CreatedAt.AddHours(2)),   // disabled
            CreateRp(Guid.Parse("71000000-0000-0000-0000-000000000005"), SeedConstants.UserStudentLeader1,  false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateRp(Guid.Parse("71000000-0000-0000-0000-000000000006"), SeedConstants.UserStudentLeader2,  false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateRp(Guid.Parse("71000000-0000-0000-0000-000000000007"), SeedConstants.UserStudentLeader3,  false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateRp(Guid.Parse("71000000-0000-0000-0000-000000000008"), SeedConstants.UserStudentMember1,  false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateRp(Guid.Parse("71000000-0000-0000-0000-000000000009"), SeedConstants.UserStudentMember2,  false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateRp(Guid.Parse("71000000-0000-0000-0000-000000000010"), SeedConstants.UserStudentMember3,  false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateRp(Guid.Parse("71000000-0000-0000-0000-000000000011"), SeedConstants.UserStudentInactive, false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateRp(Guid.Parse("71000000-0000-0000-0000-000000000012"), SeedConstants.UserJudgeInactive,   false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateRp(Guid.Parse("71000000-0000-0000-0000-000000000013"), SeedConstants.UserStaffInactive,   false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateRp(Guid.Parse("71000000-0000-0000-0000-000000000014"), SeedConstants.UserStudentBanned,   false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateRp(Guid.Parse("71000000-0000-0000-0000-000000000015"), SeedConstants.UserMentorInactive,  false, false, SeedConstants.CreatedAt.AddHours(2))
        );

        // 15 Email Verifications
        modelBuilder.Entity<EmailVerifications>().HasData(
            CreateEv(Guid.Parse("72000000-0000-0000-0000-000000000001"), SeedConstants.UserAdminActive,      EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEv(Guid.Parse("72000000-0000-0000-0000-000000000002"), SeedConstants.UserStaffActive,     EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEv(Guid.Parse("72000000-0000-0000-0000-000000000003"), SeedConstants.UserJudgeActive,     EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEv(Guid.Parse("72000000-0000-0000-0000-000000000004"), SeedConstants.UserMentorActive,    EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEv(Guid.Parse("72000000-0000-0000-0000-000000000005"), SeedConstants.UserStudentLeader1,  EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEv(Guid.Parse("72000000-0000-0000-0000-000000000006"), SeedConstants.UserStudentLeader2,  EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEv(Guid.Parse("72000000-0000-0000-0000-000000000007"), SeedConstants.UserStudentLeader3,  EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEv(Guid.Parse("72000000-0000-0000-0000-000000000008"), SeedConstants.UserStudentLeader4,  EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEv(Guid.Parse("72000000-0000-0000-0000-000000000009"), SeedConstants.UserStudentLeader5,  EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEv(Guid.Parse("72000000-0000-0000-0000-000000000010"), SeedConstants.UserStudentMember1,  EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEv(Guid.Parse("72000000-0000-0000-0000-000000000011"), SeedConstants.UserStudentInactive, EmailVerificationStatusEnum.Pending,  false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEv(Guid.Parse("72000000-0000-0000-0000-000000000012"), SeedConstants.UserStaffInactive,   EmailVerificationStatusEnum.Pending,  false, SeedConstants.CreatedAt.AddDays(-1)), // expired
            CreateEv(Guid.Parse("72000000-0000-0000-0000-000000000013"), SeedConstants.UserStudentBanned,   EmailVerificationStatusEnum.Verified, true,  SeedConstants.CreatedAt.AddDays(1)),  // disabled
            CreateEv(Guid.Parse("72000000-0000-0000-0000-000000000014"), SeedConstants.UserStudentMember2,  EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEv(Guid.Parse("72000000-0000-0000-0000-000000000015"), SeedConstants.UserStudentLeader6,  EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1))
        );
    }

    private static RefreshTokens CreateRt(Guid id, Guid userId, bool isDisable, DateTimeOffset expiredAt)
        => new() { Id = id, UserId = userId, RefreshTokenHash = $"hash-{id}", IpAddress = "127.0.0.1", UserAgent = "Mozilla/5.0 SeedBrowser", DeviceLabel = "Seed Device", ExpiredAt = expiredAt, IsDisable = isDisable, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };

    private static ResetPasswords CreateRp(Guid id, Guid userId, bool isUsed, bool isDisable, DateTimeOffset expiresAt)
        => new() { Id = id, UserId = userId, TokenHash = $"hash-{id}", IsUsed = isUsed, ExpiresAt = expiresAt, IsDisable = isDisable, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };

    private static EmailVerifications CreateEv(Guid id, Guid userId, EmailVerificationStatusEnum status, bool isDisable, DateTimeOffset expiredAt)
        => new() { Id = id, UserId = userId, TokenHash = $"hash-{id}", ExpiredAt = expiredAt, Status = status, IsDisable = isDisable, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt };
}
