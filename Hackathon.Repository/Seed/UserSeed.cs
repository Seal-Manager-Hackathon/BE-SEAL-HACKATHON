using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class UserSeed
{
    public static void SeedUsers(this ModelBuilder modelBuilder)
    {
        var hash = SeedHelper.HashDefaultPassword();
        modelBuilder.Entity<Users>().HasData(
            // Admins (2)
            Create(SeedConstants.UserAdminActive,   "admin.active@test.local",  "Admin", "SysAdmin",   "ADM001", RoleEnum.Admin,    UserStatusEnum.Active,   true,  false, null, null),
            Create(SeedConstants.UserAdminBanned,    "admin.banned@test.local",  "Admin", "Banned",     "ADM002", RoleEnum.Admin,    UserStatusEnum.Banned,   true,  false, "Security violation", SeedConstants.CreatedAt),
            // Staff (3)
            Create(SeedConstants.UserStaffActive,    "staff.active@test.local",  "Staff", "Active",     "STF001", RoleEnum.Staff,    UserStatusEnum.Active,   true,  false, null, null),
            Create(SeedConstants.UserStaffInactive,  "staff.inactive@test.local","Staff", "Inactive",   "STF002", RoleEnum.Staff,    UserStatusEnum.Inactive, false, false, null, null),
            Create(SeedConstants.UserStaffBanned,    "staff.banned@test.local",  "Staff", "Banned",     "STF003", RoleEnum.Staff,    UserStatusEnum.Banned,   true,  false, "Policy violation", SeedConstants.CreatedAt),
            // Judges (3)
            Create(SeedConstants.UserJudgeActive,    "judge.active@test.local",  "Judge", "Active",     "JDG001", RoleEnum.Lecturer, UserStatusEnum.Active,   true,  false, null, null),
            Create(SeedConstants.UserJudgeInactive,  "judge.inactive@test.local","Judge", "Inactive",   "JDG002", RoleEnum.Lecturer, UserStatusEnum.Inactive, true,  false, null, null),
            Create(SeedConstants.UserJudgeBanned,    "judge.banned@test.local",  "Judge", "Banned",     "JDG003", RoleEnum.Lecturer, UserStatusEnum.Banned,   true,  false, "Conflict of interest", SeedConstants.CreatedAt),
            // Mentors (3)
            Create(SeedConstants.UserMentorActive,   "mentor.active@test.local", "Mentor","Active",     "MNT001", RoleEnum.Lecturer, UserStatusEnum.Active,   true,  false, null, null),
            Create(SeedConstants.UserMentorInactive, "mentor.inactive@test.local","Mentor","Inactive", "MNT002", RoleEnum.Lecturer, UserStatusEnum.Inactive, true,  false, null, null),
            Create(SeedConstants.UserMentorBanned,   "mentor.banned@test.local", "Mentor","Banned",     "MNT003", RoleEnum.Lecturer, UserStatusEnum.Banned,   true,  false, "Spamming users", SeedConstants.CreatedAt),
            // Student Leaders (7)
            Create(SeedConstants.UserStudentLeader1, "leader1@test.local",  "Nguyen",   "Van An",     "STU001", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            Create(SeedConstants.UserStudentLeader2, "leader2@test.local",  "Tran",     "Thi Bich",   "STU002", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            Create(SeedConstants.UserStudentLeader3, "leader3@test.local",  "Le",       "Hoang Minh", "STU003", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            Create(SeedConstants.UserStudentLeader4, "leader4@test.local",  "Pham",     "Minh Duc",   "STU004", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            Create(SeedConstants.UserStudentLeader5, "leader5@test.local",  "Hoang",    "Ngoc Son",   "STU005", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            Create(SeedConstants.UserStudentLeader6, "leader6@test.local",  "Vo",       "Tuan Kiet",  "STU006", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            Create(SeedConstants.UserStudentLeader7, "leader7@test.local",  "Dang",     "Thuy Linh",  "STU007", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            // Student Members (10)
            Create(SeedConstants.UserStudentMember1, "member1@test.local",  "Bui",      "Cong Thanh", "STU008", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            Create(SeedConstants.UserStudentMember2, "member2@test.local",  "Do",       "Hoang Yen",  "STU009", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            Create(SeedConstants.UserStudentMember3, "member3@test.local",  "Ngo",      "Quang Trung","STU010", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            Create(SeedConstants.UserStudentMember4, "member4@test.local",  "Duong",    "Van Kien",   "STU011", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            Create(SeedConstants.UserStudentMember5, "member5@test.local",  "Ly",       "Thi Lan",    "STU012", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            Create(SeedConstants.UserStudentMember6, "member6@test.local",  "Mai",      "Thanh Long", "STU013", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            Create(SeedConstants.UserStudentMember7, "member7@test.local",  "Luong",    "Thi Mai",    "STU014", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            Create(SeedConstants.UserStudentMember8, "member8@test.local",  "Chu",      "Van Manh",   "STU015", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            Create(SeedConstants.UserStudentMember9, "member9@test.local",  "Cao",      "Thi Ngoc",   "STU016", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            Create(SeedConstants.UserStudentMember10,"member10@test.local", "Phan",     "Van Nhan",   "STU017", RoleEnum.Student, UserStatusEnum.Active, true, false, null, null),
            // Special status students (2)
            Create(SeedConstants.UserStudentInactive,"inactive.student@test.local",  "Ta", "Thi Oanh",  "STU018", RoleEnum.Student, UserStatusEnum.Inactive, false, false, null, null),
            Create(SeedConstants.UserStudentBanned,  "banned.student@test.local",    "Quach","Van Phat","STU019", RoleEnum.Student, UserStatusEnum.Banned,   true,  false, "Cheating in exam", SeedConstants.CreatedAt)
        );
    }

    private static Users Create(Guid id, string email, string firstName, string lastName, string studentId,
        RoleEnum role, UserStatusEnum status, bool isVerified, bool isDisable, string? banReason, DateTimeOffset? bannedAt)
    {
        return new Users
        {
            Id = id, Email = email,
            HashPassword = SeedHelper.HashDefaultPassword(),
            FirstName = firstName, LastName = lastName,
            PhoneNumber = "0900000000",
            AvatarUrl = $"https://robohash.org/{email}",
            Bio = "User biography", Address = "FPT Campus Ho Chi Minh City",
            DateOfBirth = new DateTimeOffset(2002, 5, 20, 0, 0, 0, TimeSpan.Zero),
            StudentId = studentId, College = "FPT University",
            ImgUrl = $"https://robohash.org/{email}",
            LinkUrl = $"https://github.com/{firstName.ToLower()}",
            Role = role,
            VerifyEmailAt = isVerified ? SeedConstants.CreatedAt : null,
            Status = status, BanReason = banReason, BannedAt = bannedAt,
            IsVerified = isVerified, IsDisable = isDisable,
            CreatedAt = SeedConstants.CreatedAt, UpdatedAt = SeedConstants.CreatedAt
        };
    }
}
