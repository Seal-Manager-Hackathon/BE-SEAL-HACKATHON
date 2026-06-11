using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class AuthSeed
{
    public static void SeedAuthData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RefreshTokens>().HasData(new RefreshTokens
        {
            Id = Guid.Parse("12000000-0000-0000-0000-000000000001"),
            UserId = SeedConstants.AdminUserId,
            RefreshTokenHash = "seed-refresh-token-hash",
            IpAddress = "127.0.0.1",
            UserAgent = "Seed Agent",
            DeviceLabel = "Seed Device",
            ExpiredAt = SeedConstants.CreatedAt.AddDays(7),
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        });

        modelBuilder.Entity<ResetPasswords>().HasData(new ResetPasswords
        {
            Id = Guid.Parse("13000000-0000-0000-0000-000000000001"),
            UserId = SeedConstants.StudentMemberUserId,
            TokenHash = "seed-reset-password-token-hash",
            IsUsed = false,
            ExpiresAt = SeedConstants.CreatedAt.AddHours(1),
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        });

        modelBuilder.Entity<EmailVerifications>().HasData(new EmailVerifications
        {
            Id = Guid.Parse("14000000-0000-0000-0000-000000000001"),
            UserId = SeedConstants.StudentLeaderUserId,
            TokenHash = "seed-email-verification-token-hash",
            ExpiredAt = SeedConstants.CreatedAt.AddDays(1),
            Status = "Verified",
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        });
    }
}
