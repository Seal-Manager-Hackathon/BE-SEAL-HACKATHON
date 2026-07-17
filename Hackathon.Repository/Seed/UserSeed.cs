using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class UserSeed
{
    public static void SeedUsers(this ModelBuilder modelBuilder)
    {
        var h = SeedHelper.HashDefaultPassword();
        modelBuilder.Entity<Users>().HasData(
            // Admins (2)
            Create(SeedConstants.UserAdmin1, "admin1@hackathon.com", "Admin", "One", RoleEnum.Admin, UserStatusEnum.Active, "AD100001"),
            Create(SeedConstants.UserAdmin2, "admin2@hackathon.com", "Admin", "Two", RoleEnum.Admin, UserStatusEnum.Active, "AD100002"),
            // Staff (5)
            Create(SeedConstants.UserStaff1, "staff1@hackathon.com", "Staff", "One", RoleEnum.Staff, UserStatusEnum.Active, "ST100001"),
            Create(SeedConstants.UserStaff2, "staff2@hackathon.com", "Staff", "Two", RoleEnum.Staff, UserStatusEnum.Active, "ST100002"),
            Create(SeedConstants.UserStaff3, "staff3@hackathon.com", "Staff", "Three", RoleEnum.Staff, UserStatusEnum.Active, "ST100003"),
            Create(SeedConstants.UserStaffInactive, "staff.inactive@hackathon.com", "Staff", "Inactive", RoleEnum.Staff, UserStatusEnum.Inactive, "ST100004"),
            Create(SeedConstants.UserStaffBanned, "staff.banned@hackathon.com", "Staff", "Banned", RoleEnum.Staff, UserStatusEnum.Banned, "ST100005", "Violated policy"),
            // Judges (6)
            Create(SeedConstants.UserJudge1, "judge1@hackathon.com", "Judge", "One", RoleEnum.Lecturer, UserStatusEnum.Active, "LG100001"),
            Create(SeedConstants.UserJudge2, "judge2@hackathon.com", "Judge", "Two", RoleEnum.Lecturer, UserStatusEnum.Active, "LG100002"),
            Create(SeedConstants.UserJudge3, "judge3@hackathon.com", "Judge", "Three", RoleEnum.Lecturer, UserStatusEnum.Active, "LG100003"),
            Create(SeedConstants.UserJudge4, "judge4@hackathon.com", "Judge", "Four", RoleEnum.Lecturer, UserStatusEnum.Active, "LG100004"),
            Create(SeedConstants.UserJudgeInactive, "judge.inactive@hackathon.com", "Judge", "Inactive", RoleEnum.Lecturer, UserStatusEnum.Inactive, "LG100005"),
            Create(SeedConstants.UserJudgeBanned, "judge.banned@hackathon.com", "Judge", "Banned", RoleEnum.Lecturer, UserStatusEnum.Banned, "LG100006", "Misconduct"),
            // Mentors (6)
            Create(SeedConstants.UserMentor1, "mentor1@hackathon.com", "Mentor", "One", RoleEnum.Lecturer, UserStatusEnum.Active, "MN100001"),
            Create(SeedConstants.UserMentor2, "mentor2@hackathon.com", "Mentor", "Two", RoleEnum.Lecturer, UserStatusEnum.Active, "MN100002"),
            Create(SeedConstants.UserMentor3, "mentor3@hackathon.com", "Mentor", "Three", RoleEnum.Lecturer, UserStatusEnum.Active, "MN100003"),
            Create(SeedConstants.UserMentor4, "mentor4@hackathon.com", "Mentor", "Four", RoleEnum.Lecturer, UserStatusEnum.Active, "MN100004"),
            Create(SeedConstants.UserMentorInactive, "mentor.inactive@hackathon.com", "Mentor", "Inactive", RoleEnum.Lecturer, UserStatusEnum.Inactive, "MN100005"),
            Create(SeedConstants.UserMentorBanned, "mentor.banned@hackathon.com", "Mentor", "Banned", RoleEnum.Lecturer, UserStatusEnum.Banned, "MN100006", "Abusive behavior"),
            // Student Leaders (10)
            Create(SeedConstants.UserStudentLeader1, "leader1@hackathon.com", "Leader", "Alpha", RoleEnum.Student, UserStatusEnum.Active, "STU20001"),
            Create(SeedConstants.UserStudentLeader2, "leader2@hackathon.com", "Leader", "Bravo", RoleEnum.Student, UserStatusEnum.Active, "STU20002"),
            Create(SeedConstants.UserStudentLeader3, "leader3@hackathon.com", "Leader", "Charlie", RoleEnum.Student, UserStatusEnum.Active, "STU20003"),
            Create(SeedConstants.UserStudentLeader4, "leader4@hackathon.com", "Leader", "Delta", RoleEnum.Student, UserStatusEnum.Active, "STU20004"),
            Create(SeedConstants.UserStudentLeader5, "leader5@hackathon.com", "Leader", "Echo", RoleEnum.Student, UserStatusEnum.Active, "STU20005"),
            Create(SeedConstants.UserStudentLeader6, "leader6@hackathon.com", "Leader", "Foxtrot", RoleEnum.Student, UserStatusEnum.Active, "STU20006"),
            Create(SeedConstants.UserStudentLeader7, "leader7@hackathon.com", "Leader", "Golf", RoleEnum.Student, UserStatusEnum.Active, "STU20007"),
            Create(SeedConstants.UserStudentLeader8, "leader8@hackathon.com", "Leader", "Hotel", RoleEnum.Student, UserStatusEnum.Active, "STU20008"),
            Create(SeedConstants.UserStudentLeader9, "leader9@hackathon.com", "Leader", "India", RoleEnum.Student, UserStatusEnum.Active, "STU20009"),
            Create(SeedConstants.UserStudentLeader10, "leader10@hackathon.com", "Leader", "Juliet", RoleEnum.Student, UserStatusEnum.Active, "STU20010"),
            // Student Members (20)
            Create(SeedConstants.UserStudentMember1, "member1@hackathon.com", "Member", "Kilo", RoleEnum.Student, UserStatusEnum.Active, "STU20011"),
            Create(SeedConstants.UserStudentMember2, "member2@hackathon.com", "Member", "Lima", RoleEnum.Student, UserStatusEnum.Active, "STU20012"),
            Create(SeedConstants.UserStudentMember3, "member3@hackathon.com", "Member", "Mike", RoleEnum.Student, UserStatusEnum.Active, "STU20013"),
            Create(SeedConstants.UserStudentMember4, "member4@hackathon.com", "Member", "November", RoleEnum.Student, UserStatusEnum.Active, "STU20014"),
            Create(SeedConstants.UserStudentMember5, "member5@hackathon.com", "Member", "Oscar", RoleEnum.Student, UserStatusEnum.Active, "STU20015"),
            Create(SeedConstants.UserStudentMember6, "member6@hackathon.com", "Member", "Papa", RoleEnum.Student, UserStatusEnum.Active, "STU20016"),
            Create(SeedConstants.UserStudentMember7, "member7@hackathon.com", "Member", "Quebec", RoleEnum.Student, UserStatusEnum.Active, "STU20017"),
            Create(SeedConstants.UserStudentMember8, "member8@hackathon.com", "Member", "Romeo", RoleEnum.Student, UserStatusEnum.Active, "STU20018"),
            Create(SeedConstants.UserStudentMember9, "member9@hackathon.com", "Member", "Sierra", RoleEnum.Student, UserStatusEnum.Active, "STU20019"),
            Create(SeedConstants.UserStudentMember10, "member10@hackathon.com", "Member", "Tango", RoleEnum.Student, UserStatusEnum.Active, "STU20020"),
            Create(SeedConstants.UserStudentMember11, "member11@hackathon.com", "Member", "Uniform", RoleEnum.Student, UserStatusEnum.Active, "STU20021"),
            Create(SeedConstants.UserStudentMember12, "member12@hackathon.com", "Member", "Victor", RoleEnum.Student, UserStatusEnum.Active, "STU20022"),
            Create(SeedConstants.UserStudentMember13, "member13@hackathon.com", "Member", "Whiskey", RoleEnum.Student, UserStatusEnum.Active, "STU20023"),
            Create(SeedConstants.UserStudentMember14, "member14@hackathon.com", "Member", "Xray", RoleEnum.Student, UserStatusEnum.Active, "STU20024"),
            Create(SeedConstants.UserStudentMember15, "member15@hackathon.com", "Member", "Yankee", RoleEnum.Student, UserStatusEnum.Active, "STU20025"),
            Create(SeedConstants.UserStudentMember16, "member16@hackathon.com", "Member", "Zulu", RoleEnum.Student, UserStatusEnum.Active, "STU20026"),
            Create(SeedConstants.UserStudentMember17, "member17@hackathon.com", "Member", "Alpha2", RoleEnum.Student, UserStatusEnum.Active, "STU20027"),
            Create(SeedConstants.UserStudentMember18, "member18@hackathon.com", "Member", "Bravo2", RoleEnum.Student, UserStatusEnum.Active, "STU20028"),
            Create(SeedConstants.UserStudentMember19, "member19@hackathon.com", "Member", "Charlie2", RoleEnum.Student, UserStatusEnum.Active, "STU20029"),
            Create(SeedConstants.UserStudentMember20, "member20@hackathon.com", "Member", "Delta2", RoleEnum.Student, UserStatusEnum.Active, "STU20030"),
            // Banned student
            Create(SeedConstants.UserStudentBanned, "banned.student@hackathon.com", "Banned", "Student", RoleEnum.Student, UserStatusEnum.Banned, "STU99999", "Cheating detected")
        );
    }

    private static Users Create(Guid id, string email, string firstName, string lastName, RoleEnum role, UserStatusEnum status, string studentId, string? banReason = null)
        => new()
        {
            Id = id,
            Email = email,
            HashPassword = SeedHelper.HashDefaultPassword(),
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = "090xxxxxxx",
            AvatarUrl = $"https://robohash.org/{email}",
            Bio = $"Bio for {firstName} {lastName}",
            Address = "FPT University, HCMC",
            DateOfBirth = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero),
            StudentId = studentId,
            College = "FPT University",
            Role = role,
            Status = status,
            IsVerified = status == UserStatusEnum.Active,
            VerifyEmailAt = status == UserStatusEnum.Active ? SeedConstants.CreatedAt : null,
            BanReason = banReason,
            BannedAt = banReason != null ? SeedConstants.CreatedAt : null,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
}
