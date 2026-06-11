using Hackathon.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class UserRoleSeed
{
    public static void SeedUserRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRoles>().HasData(
            CreateUserRole(Guid.Parse("11000000-0000-0000-0000-000000000001"), SeedConstants.AdminUserId, SeedConstants.AdminRoleId),
            CreateUserRole(Guid.Parse("11000000-0000-0000-0000-000000000002"), SeedConstants.StaffUserId, SeedConstants.StaffRoleId),
            CreateUserRole(Guid.Parse("11000000-0000-0000-0000-000000000003"), SeedConstants.MentorUserId, SeedConstants.LecturerRoleId),
            CreateUserRole(Guid.Parse("11000000-0000-0000-0000-000000000004"), SeedConstants.JudgeUserId, SeedConstants.LecturerRoleId),
            CreateUserRole(Guid.Parse("11000000-0000-0000-0000-000000000005"), SeedConstants.StudentLeaderUserId, SeedConstants.StudentRoleId),
            CreateUserRole(Guid.Parse("11000000-0000-0000-0000-000000000006"), SeedConstants.StudentMemberUserId, SeedConstants.StudentRoleId),
            CreateUserRole(Guid.Parse("11000000-0000-0000-0000-000000000007"), SeedConstants.GreenLeaderUserId, SeedConstants.StudentRoleId)
        );
    }

    private static UserRoles CreateUserRole(Guid id, Guid userId, Guid roleId)
    {
        return new UserRoles
        {
            Id = id,
            UserId = userId,
            RoleId = roleId,
            IsDisable = false,
            CreatedAt = SeedConstants.CreatedAt,
            UpdatedAt = SeedConstants.CreatedAt
        };
    }
}
