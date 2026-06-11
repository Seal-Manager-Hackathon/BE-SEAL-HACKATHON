using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class UserSeed
{
    public static void SeedUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>().HasData(
            CreateUser(SeedConstants.AdminUserId, "admin@seed.local", "Admin", "Seed", "System Administrator"),
            CreateUser(SeedConstants.StaffUserId, "staff@seed.local", "Staff", "Seed", "Event Staff"),
            CreateUser(SeedConstants.MentorUserId, "mentor@seed.local", "Mentor", "Lecturer", "Seed Mentor"),
            CreateUser(SeedConstants.JudgeUserId, "judge@seed.local", "Judge", "Lecturer", "Seed Judge"),
            CreateUser(SeedConstants.StudentLeaderUserId, "leader@seed.local", "Student", "Leader", "SEAL001"),
            CreateUser(SeedConstants.StudentMemberUserId, "member@seed.local", "Student", "Member", "SEAL002"),
            CreateUser(SeedConstants.GreenLeaderUserId, "green.leader@seed.local", "Green", "Leader", "SEAL003")
        );
    }

    private static Users CreateUser(Guid id, string email, string firstName, string lastName, string studentId)
    {
        return new Users
        {
            Id = id,
            Email = email,
            HashPassword = "seed-password-hash-not-for-login",
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = "0900000000",
            AvatarUrl = "https://seed.local/avatar.png",
            Bio = "Seed user",
            Address = "Seed address",
            DateOfBirth = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero),
            StudentId = studentId,
            College = "Seed University",
            ImgUrl = "https://seed.local/profile.png",
            LinkUrl = "https://seed.local/users",
            VerifyEmailAt = SeedConstants.CreatedAt,
            Status = "Active",
            IsVerified = true,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }
}
