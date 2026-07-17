using System;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class UserSeed
{
    public static void SeedUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>().HasData(
            CreateUser(SeedConstants.UserAdminActive, "admin.active@test.local", "Admin", "Active", "ADM001", RoleEnum.Admin, UserStatusEnum.Active, true, false),
            CreateUser(SeedConstants.UserAdminBanned, "admin.banned@test.local", "Admin", "Banned", "ADM002", RoleEnum.Admin, UserStatusEnum.Banned, true, false, "Security violation", SeedConstants.CreatedAt),
            CreateUser(SeedConstants.UserStaffActive, "staff.active@test.local", "Staff", "Active", "STF001", RoleEnum.Staff, UserStatusEnum.Active, true, false),
            CreateUser(SeedConstants.UserStaffInactive, "staff.inactive@test.local", "Staff", "Inactive", "STF002", RoleEnum.Staff, UserStatusEnum.Inactive, false, false),
            CreateUser(SeedConstants.UserJudgeActive, "judge.active@test.local", "Judge", "Active", "JDG001", RoleEnum.Lecturer, UserStatusEnum.Active, true, false),
            CreateUser(SeedConstants.UserJudgeInactive, "judge.inactive@test.local", "Judge", "Inactive", "JDG002", RoleEnum.Lecturer, UserStatusEnum.Inactive, true, false),
            CreateUser(SeedConstants.UserMentorActive, "mentor.active@test.local", "Mentor", "Active", "MNT001", RoleEnum.Lecturer, UserStatusEnum.Active, true, false),
            CreateUser(SeedConstants.UserMentorBanned, "mentor.banned@test.local", "Mentor", "Banned", "MNT002", RoleEnum.Lecturer, UserStatusEnum.Banned, true, false, "Spamming", SeedConstants.CreatedAt),
            CreateUser(SeedConstants.UserStudentLeaderActive1, "leader1@test.local", "Student", "Leader1", "STU001", RoleEnum.Student, UserStatusEnum.Active, true, false),
            CreateUser(SeedConstants.UserStudentMemberActive1, "member1@test.local", "Student", "Member1", "STU002", RoleEnum.Student, UserStatusEnum.Active, true, false),
            CreateUser(SeedConstants.UserStudentMemberInactive1, "member1.in@test.local", "Student", "MemberInactive1", "STU003", RoleEnum.Student, UserStatusEnum.Inactive, false, false),
            CreateUser(SeedConstants.UserStudentLeaderActive2, "leader2@test.local", "Student", "Leader2", "STU004", RoleEnum.Student, UserStatusEnum.Active, true, false),
            CreateUser(SeedConstants.UserStudentMemberActive2, "member2@test.local", "Student", "Member2", "STU005", RoleEnum.Student, UserStatusEnum.Active, true, false),
            CreateUser(SeedConstants.UserStudentLeaderActive3, "leader3@test.local", "Student", "Leader3", "STU006", RoleEnum.Student, UserStatusEnum.Active, true, false),
            CreateUser(SeedConstants.UserStudentMemberBanned3, "member3.ban@test.local", "Student", "MemberBanned3", "STU007", RoleEnum.Student, UserStatusEnum.Banned, true, false, "Cheating", SeedConstants.CreatedAt)
        );
    }

    private static Users CreateUser(Guid id, string email, string firstName, string lastName, string studentId, RoleEnum role, UserStatusEnum status, bool isVerified, bool isDisable, string? banReason = null, DateTimeOffset? bannedAt = null)
    {
        return new Users
        {
            Id = id,
            Email = email,
            HashPassword = "$2a$11$ELUlXu.C3Yh0miS3.dAZaO17ER/stLENq.EWnMYmPBiwZ14X8g1i6", // "String1@" BCrypt hash
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = "0900000000",
            AvatarUrl = $"https://api.dicebear.com/7.x/bottts/svg?seed={email}",
            Bio = "Test user biography",
            Address = "FPT Campus Ho Chi Minh City",
            DateOfBirth = new DateTimeOffset(2002, 5, 20, 0, 0, 0, TimeSpan.Zero),
            StudentId = studentId,
            College = "FPT University",
            ImgUrl = $"https://api.dicebear.com/7.x/bottts/svg?seed={email}",
            LinkUrl = $"https://github.com/{firstName.ToLower()}",
            Role = role,
            VerifyEmailAt = isVerified ? SeedConstants.CreatedAt : null,
            Status = status,
            BanReason = banReason,
            BannedAt = bannedAt,
            IsVerified = isVerified,
            IsDisable = isDisable,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }
}
