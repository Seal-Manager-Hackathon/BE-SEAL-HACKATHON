using System;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class AuthSeed
{
    public static void SeedAuthData(this ModelBuilder modelBuilder)
    {
        // 12 Refresh Tokens
        modelBuilder.Entity<RefreshTokens>().HasData(
            CreateRefreshToken(Guid.Parse("70000000-0000-0000-0000-000000000001"), SeedConstants.UserAdminActive, false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRefreshToken(Guid.Parse("70000000-0000-0000-0000-000000000002"), SeedConstants.UserStaffActive, false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRefreshToken(Guid.Parse("70000000-0000-0000-0000-000000000003"), SeedConstants.UserJudgeActive, false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRefreshToken(Guid.Parse("70000000-0000-0000-0000-000000000004"), SeedConstants.UserMentorActive, false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRefreshToken(Guid.Parse("70000000-0000-0000-0000-000000000005"), SeedConstants.UserStudentLeaderActive1, false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRefreshToken(Guid.Parse("70000000-0000-0000-0000-000000000006"), SeedConstants.UserStudentMemberActive1, false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRefreshToken(Guid.Parse("70000000-0000-0000-0000-000000000007"), SeedConstants.UserStudentLeaderActive2, false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRefreshToken(Guid.Parse("70000000-0000-0000-0000-000000000008"), SeedConstants.UserStudentMemberActive2, false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRefreshToken(Guid.Parse("70000000-0000-0000-0000-000000000009"), SeedConstants.UserStudentLeaderActive3, false, SeedConstants.CreatedAt.AddDays(7)),
            CreateRefreshToken(Guid.Parse("70000000-0000-0000-0000-000000000010"), SeedConstants.UserAdminActive, true, SeedConstants.CreatedAt.AddDays(7)), // disabled
            CreateRefreshToken(Guid.Parse("70000000-0000-0000-0000-000000000011"), SeedConstants.UserStaffActive, false, SeedConstants.CreatedAt.AddDays(-1)), // expired
            CreateRefreshToken(Guid.Parse("70000000-0000-0000-0000-000000000012"), SeedConstants.UserStudentMemberInactive1, false, SeedConstants.CreatedAt.AddDays(7))
        );

        // 12 Reset Passwords
        modelBuilder.Entity<ResetPasswords>().HasData(
            CreateResetPassword(Guid.Parse("71000000-0000-0000-0000-000000000001"), SeedConstants.UserAdminActive, false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateResetPassword(Guid.Parse("71000000-0000-0000-0000-000000000002"), SeedConstants.UserStaffActive, true, false, SeedConstants.CreatedAt.AddHours(2)), // used
            CreateResetPassword(Guid.Parse("71000000-0000-0000-0000-000000000003"), SeedConstants.UserJudgeActive, false, false, SeedConstants.CreatedAt.AddHours(-1)), // expired
            CreateResetPassword(Guid.Parse("71000000-0000-0000-0000-000000000004"), SeedConstants.UserMentorActive, false, true, SeedConstants.CreatedAt.AddHours(2)), // disabled
            CreateResetPassword(Guid.Parse("71000000-0000-0000-0000-000000000005"), SeedConstants.UserStudentLeaderActive1, false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateResetPassword(Guid.Parse("71000000-0000-0000-0000-000000000006"), SeedConstants.UserStudentMemberActive1, false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateResetPassword(Guid.Parse("71000000-0000-0000-0000-000000000007"), SeedConstants.UserStudentLeaderActive2, false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateResetPassword(Guid.Parse("71000000-0000-0000-0000-000000000008"), SeedConstants.UserStudentMemberActive2, false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateResetPassword(Guid.Parse("71000000-0000-0000-0000-000000000009"), SeedConstants.UserStudentLeaderActive3, false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateResetPassword(Guid.Parse("71000000-0000-0000-0000-000000000010"), SeedConstants.UserStudentMemberInactive1, false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateResetPassword(Guid.Parse("71000000-0000-0000-0000-000000000011"), SeedConstants.UserJudgeInactive, false, false, SeedConstants.CreatedAt.AddHours(2)),
            CreateResetPassword(Guid.Parse("71000000-0000-0000-0000-000000000012"), SeedConstants.UserStaffInactive, false, false, SeedConstants.CreatedAt.AddHours(2))
        );

        // 12 Email Verifications
        modelBuilder.Entity<EmailVerifications>().HasData(
            CreateEmailVerification(Guid.Parse("72000000-0000-0000-0000-000000000001"), SeedConstants.UserAdminActive, EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEmailVerification(Guid.Parse("72000000-0000-0000-0000-000000000002"), SeedConstants.UserStaffActive, EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEmailVerification(Guid.Parse("72000000-0000-0000-0000-000000000003"), SeedConstants.UserJudgeActive, EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEmailVerification(Guid.Parse("72000000-0000-0000-0000-000000000004"), SeedConstants.UserMentorActive, EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEmailVerification(Guid.Parse("72000000-0000-0000-0000-000000000005"), SeedConstants.UserStudentLeaderActive1, EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEmailVerification(Guid.Parse("72000000-0000-0000-0000-000000000006"), SeedConstants.UserStudentMemberActive1, EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEmailVerification(Guid.Parse("72000000-0000-0000-0000-000000000007"), SeedConstants.UserStudentLeaderActive2, EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEmailVerification(Guid.Parse("72000000-0000-0000-0000-000000000008"), SeedConstants.UserStudentMemberActive2, EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEmailVerification(Guid.Parse("72000000-0000-0000-0000-000000000009"), SeedConstants.UserStudentLeaderActive3, EmailVerificationStatusEnum.Verified, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEmailVerification(Guid.Parse("72000000-0000-0000-0000-000000000010"), SeedConstants.UserStudentMemberInactive1, EmailVerificationStatusEnum.Pending, false, SeedConstants.CreatedAt.AddDays(1)),
            CreateEmailVerification(Guid.Parse("72000000-0000-0000-0000-000000000011"), SeedConstants.UserStaffInactive, EmailVerificationStatusEnum.Pending, false, SeedConstants.CreatedAt.AddDays(-1)), // expired
            CreateEmailVerification(Guid.Parse("72000000-0000-0000-0000-000000000012"), SeedConstants.UserStudentMemberBanned3, EmailVerificationStatusEnum.Verified, true, SeedConstants.CreatedAt.AddDays(1)) // disabled
        );
    }

    private static RefreshTokens CreateRefreshToken(Guid id, Guid userId, bool isDisable, DateTimeOffset expiredAt)
    {
        return new RefreshTokens
        {
            Id = id,
            UserId = userId,
            RefreshTokenHash = $"hash-{id}",
            IpAddress = "127.0.0.1",
            UserAgent = "Mozilla/5.0 TestBrowser",
            DeviceLabel = "Test Device",
            ExpiredAt = expiredAt,
            IsDisable = isDisable,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }

    private static ResetPasswords CreateResetPassword(Guid id, Guid userId, bool isUsed, bool isDisable, DateTimeOffset expiresAt)
    {
        return new ResetPasswords
        {
            Id = id,
            UserId = userId,
            TokenHash = $"hash-{id}",
            IsUsed = isUsed,
            ExpiresAt = expiresAt,
            IsDisable = isDisable,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }

    private static EmailVerifications CreateEmailVerification(Guid id, Guid userId, EmailVerificationStatusEnum status, bool isDisable, DateTimeOffset expiredAt)
    {
        return new EmailVerifications
        {
            Id = id,
            UserId = userId,
            TokenHash = $"hash-{id}",
            ExpiredAt = expiredAt,
            Status = status,
            IsDisable = isDisable,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }
}
