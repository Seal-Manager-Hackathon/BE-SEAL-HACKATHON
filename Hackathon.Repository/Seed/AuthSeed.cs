using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

// Auth IDs: RefreshToken=42000000, ResetPassword=43000000, EmailVerification=44000000
public static class AuthSeed
{
    // Refresh tokens
    public static readonly Guid Rt1 = Guid.Parse("42000000-0000-0000-0000-000000000001");
    public static readonly Guid Rt2 = Guid.Parse("42000000-0000-0000-0000-000000000002");
    public static readonly Guid Rt3 = Guid.Parse("42000000-0000-0000-0000-000000000003");
    public static readonly Guid Rt4 = Guid.Parse("42000000-0000-0000-0000-000000000004");
    public static readonly Guid Rt5 = Guid.Parse("42000000-0000-0000-0000-000000000005");
    public static readonly Guid Rt6 = Guid.Parse("42000000-0000-0000-0000-000000000006");
    public static readonly Guid Rt7 = Guid.Parse("42000000-0000-0000-0000-000000000007");
    public static readonly Guid Rt8 = Guid.Parse("42000000-0000-0000-0000-000000000008");
    public static readonly Guid Rt9 = Guid.Parse("42000000-0000-0000-0000-000000000009");
    public static readonly Guid Rt10 = Guid.Parse("42000000-0000-0000-0000-000000000010");
    public static readonly Guid Rt11 = Guid.Parse("42000000-0000-0000-0000-000000000011");
    public static readonly Guid Rt12 = Guid.Parse("42000000-0000-0000-0000-000000000012");
    public static readonly Guid Rt13 = Guid.Parse("42000000-0000-0000-0000-000000000013");
    public static readonly Guid Rt14 = Guid.Parse("42000000-0000-0000-0000-000000000014");
    public static readonly Guid Rt15 = Guid.Parse("42000000-0000-0000-0000-000000000015");

    // Reset password tokens
    public static readonly Guid Rp1 = Guid.Parse("43000000-0000-0000-0000-000000000001");
    public static readonly Guid Rp2 = Guid.Parse("43000000-0000-0000-0000-000000000002");
    public static readonly Guid Rp3 = Guid.Parse("43000000-0000-0000-0000-000000000003");
    public static readonly Guid Rp4 = Guid.Parse("43000000-0000-0000-0000-000000000004");
    public static readonly Guid Rp5 = Guid.Parse("43000000-0000-0000-0000-000000000005");
    public static readonly Guid Rp6 = Guid.Parse("43000000-0000-0000-0000-000000000006");
    public static readonly Guid Rp7 = Guid.Parse("43000000-0000-0000-0000-000000000007");
    public static readonly Guid Rp8 = Guid.Parse("43000000-0000-0000-0000-000000000008");
    public static readonly Guid Rp9 = Guid.Parse("43000000-0000-0000-0000-000000000009");
    public static readonly Guid Rp10 = Guid.Parse("43000000-0000-0000-0000-000000000010");
    public static readonly Guid Rp11 = Guid.Parse("43000000-0000-0000-0000-000000000011");
    public static readonly Guid Rp12 = Guid.Parse("43000000-0000-0000-0000-000000000012");
    public static readonly Guid Rp13 = Guid.Parse("43000000-0000-0000-0000-000000000013");
    public static readonly Guid Rp14 = Guid.Parse("43000000-0000-0000-0000-000000000014");
    public static readonly Guid Rp15 = Guid.Parse("43000000-0000-0000-0000-000000000015");

    // Email verifications
    public static readonly Guid Ev1 = Guid.Parse("44000000-0000-0000-0000-000000000001");
    public static readonly Guid Ev2 = Guid.Parse("44000000-0000-0000-0000-000000000002");
    public static readonly Guid Ev3 = Guid.Parse("44000000-0000-0000-0000-000000000003");
    public static readonly Guid Ev4 = Guid.Parse("44000000-0000-0000-0000-000000000004");
    public static readonly Guid Ev5 = Guid.Parse("44000000-0000-0000-0000-000000000005");
    public static readonly Guid Ev6 = Guid.Parse("44000000-0000-0000-0000-000000000006");
    public static readonly Guid Ev7 = Guid.Parse("44000000-0000-0000-0000-000000000007");
    public static readonly Guid Ev8 = Guid.Parse("44000000-0000-0000-0000-000000000008");
    public static readonly Guid Ev9 = Guid.Parse("44000000-0000-0000-0000-000000000009");
    public static readonly Guid Ev10 = Guid.Parse("44000000-0000-0000-0000-000000000010");
    public static readonly Guid Ev11 = Guid.Parse("44000000-0000-0000-0000-000000000011");
    public static readonly Guid Ev12 = Guid.Parse("44000000-0000-0000-0000-000000000012");
    public static readonly Guid Ev13 = Guid.Parse("44000000-0000-0000-0000-000000000013");
    public static readonly Guid Ev14 = Guid.Parse("44000000-0000-0000-0000-000000000014");
    public static readonly Guid Ev15 = Guid.Parse("44000000-0000-0000-0000-000000000015");

    // Dummy token hash for seed data (computed from "dummy-token-value")
    private const string TokenHashPlaceholder = "$2a$11$DummyTokenHashForSeedDataOnly1234567890ABCDEF";

    public static void SeedAuthData(this ModelBuilder modelBuilder)
    {
        var c = SeedConstants.CreatedAt;

        // ── 15 RefreshTokens ──────────────────────────────────────────
        modelBuilder.Entity<RefreshTokens>().HasData(
            // Active tokens for various users
            CreateRt(Rt1, SeedConstants.UserAdmin1, "192.168.1.1", "Mozilla/5.0", "Chrome Win", c.AddDays(30), null),
            CreateRt(Rt2, SeedConstants.UserStaff1, "192.168.1.2", "Mozilla/5.0", "Chrome Mac", c.AddDays(15), null),
            CreateRt(Rt3, SeedConstants.UserJudge1, "10.0.0.1", "PostmanRuntime", "API Client", c.AddDays(7), null),
            CreateRt(Rt4, SeedConstants.UserStudentLeader1, "172.16.0.1", "Mozilla/5.0", "Firefox Win", c.AddDays(20), null),
            CreateRt(Rt5, SeedConstants.UserStudentMember1, "172.16.0.2", "Mozilla/5.0", "Edge Win", c.AddDays(5), null),
            // Revoked tokens
            CreateRt(Rt6, SeedConstants.UserAdmin1, "192.168.1.10", "Mozilla/5.0", "Chrome Win", c.AddDays(-5), c.AddDays(-10)),
            CreateRt(Rt7, SeedConstants.UserStaff2, "192.168.1.20", "Mozilla/5.0", "Safari Mac", c.AddDays(-3), c.AddDays(-8)),
            CreateRt(Rt8, SeedConstants.UserStudentLeader2, "172.16.0.10", "Mozilla/5.0", "Firefox Win", c.AddDays(-2), c.AddDays(-5)),
            // Expired tokens
            CreateRt(Rt9, SeedConstants.UserJudge2, "10.0.0.2", "Mozilla/5.0", "Chrome Win", c.AddDays(-30), null),
            CreateRt(Rt10, SeedConstants.UserMentor1, "10.0.0.3", "Mozilla/5.0", "Safari Mac", c.AddDays(-60), null),
            // More tokens
            CreateRt(Rt11, SeedConstants.UserJudge3, "10.0.0.5", "Mozilla/5.0", "Chrome Win", c.AddDays(10), null),
            CreateRt(Rt12, SeedConstants.UserStudentLeader3, "172.16.0.5", "Mozilla/5.0", "Edge Win", c.AddDays(14), null),
            CreateRt(Rt13, SeedConstants.UserMentor2, "10.0.0.6", "Mozilla/5.0", "Chrome Mac", c.AddDays(7), null),
            CreateRt(Rt14, SeedConstants.UserStudentLeader4, "172.16.0.6", "Mozilla/5.0", "Firefox Win", c.AddDays(3), null),
            CreateRt(Rt15, SeedConstants.UserStaff3, "192.168.1.30", "Mozilla/5.0", "Chrome Win", c.AddDays(1), null)
        );

        // ── 15 ResetPasswords ─────────────────────────────────────────
        modelBuilder.Entity<ResetPasswords>().HasData(
            // Used tokens
            CreateRp(Rp1, SeedConstants.UserAdmin1, true, c.AddDays(10)),
            CreateRp(Rp2, SeedConstants.UserStaff1, true, c.AddDays(5)),
            CreateRp(Rp3, SeedConstants.UserStudentLeader1, true, c.AddDays(3)),
            CreateRp(Rp4, SeedConstants.UserStudentMember1, true, c.AddDays(1)),
            CreateRp(Rp5, SeedConstants.UserJudge1, true, c.AddDays(7)),
            // Unused valid tokens
            CreateRp(Rp6, SeedConstants.UserStudentLeader2, false, c.AddDays(1)),
            CreateRp(Rp7, SeedConstants.UserStudentMember2, false, c.AddDays(2)),
            CreateRp(Rp8, SeedConstants.UserStaff2, false, c.AddDays(3)),
            CreateRp(Rp9, SeedConstants.UserMentor1, false, c.AddDays(1)),
            CreateRp(Rp10, SeedConstants.UserJudge2, false, c.AddDays(1)),
            // Expired tokens
            CreateRp(Rp11, SeedConstants.UserStudentLeader3, false, c.AddDays(-10)),
            CreateRp(Rp12, SeedConstants.UserStudentMember3, false, c.AddDays(-5)),
            CreateRp(Rp13, SeedConstants.UserStaff3, false, c.AddDays(-3)),
            CreateRp(Rp14, SeedConstants.UserJudge3, false, c.AddDays(-7)),
            CreateRp(Rp15, SeedConstants.UserMentor2, false, c.AddDays(-1))
        );

        // ── 15 EmailVerifications ─────────────────────────────────────
        modelBuilder.Entity<EmailVerifications>().HasData(
            // Verified
            CreateEv(Ev1, SeedConstants.UserAdmin1, c.AddDays(10), EmailVerificationStatusEnum.Verified),
            CreateEv(Ev2, SeedConstants.UserStaff1, c.AddDays(10), EmailVerificationStatusEnum.Verified),
            CreateEv(Ev3, SeedConstants.UserJudge1, c.AddDays(10), EmailVerificationStatusEnum.Verified),
            CreateEv(Ev4, SeedConstants.UserStudentLeader1, c.AddDays(10), EmailVerificationStatusEnum.Verified),
            CreateEv(Ev5, SeedConstants.UserStudentMember1, c.AddDays(10), EmailVerificationStatusEnum.Verified),
            CreateEv(Ev6, SeedConstants.UserMentor1, c.AddDays(10), EmailVerificationStatusEnum.Verified),
            // Pending
            CreateEv(Ev7, SeedConstants.UserStudentLeader2, c.AddDays(3), EmailVerificationStatusEnum.Pending),
            CreateEv(Ev8, SeedConstants.UserStudentMember2, c.AddDays(5), EmailVerificationStatusEnum.Pending),
            CreateEv(Ev9, SeedConstants.UserStudentLeader3, c.AddDays(2), EmailVerificationStatusEnum.Pending),
            CreateEv(Ev10, SeedConstants.UserStaff2, c.AddDays(1), EmailVerificationStatusEnum.Pending),
            // Expired
            CreateEv(Ev11, SeedConstants.UserStudentMember3, c.AddDays(-10), EmailVerificationStatusEnum.Expired),
            CreateEv(Ev12, SeedConstants.UserStudentLeader4, c.AddDays(-5), EmailVerificationStatusEnum.Expired),
            CreateEv(Ev13, SeedConstants.UserStudentMember4, c.AddDays(-3), EmailVerificationStatusEnum.Expired),
            CreateEv(Ev14, SeedConstants.UserJudge2, c.AddDays(-7), EmailVerificationStatusEnum.Expired),
            CreateEv(Ev15, SeedConstants.UserMentor2, c.AddDays(-1), EmailVerificationStatusEnum.Expired)
        );
    }

    private static RefreshTokens CreateRt(Guid id, Guid userId, string ip, string ua, string device, DateTimeOffset expired, DateTimeOffset? revoked) => new()
    {
        Id = id, UserId = userId, RefreshTokenHash = TokenHashPlaceholder,
        IpAddress = ip, UserAgent = ua, DeviceLabel = device,
        ExpiredAt = expired, RevokedAt = revoked,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };

    private static ResetPasswords CreateRp(Guid id, Guid userId, bool isUsed, DateTimeOffset expiresAt) => new()
    {
        Id = id, UserId = userId, TokenHash = TokenHashPlaceholder,
        IsUsed = isUsed, ExpiresAt = expiresAt,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };

    private static EmailVerifications CreateEv(Guid id, Guid userId, DateTimeOffset expiredAt, EmailVerificationStatusEnum status) => new()
    {
        Id = id, UserId = userId, TokenHash = TokenHashPlaceholder,
        ExpiredAt = expiredAt, Status = status,
        IsDisable = false, CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
    };
}
