using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Repository.Seed;

public static class RoleSeed
{
    public static void SeedRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Roles>().HasData(
            new Roles
            {
                Id = SeedConstants.AdminRoleId,
                Name = RoleEnum.Admin,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new Roles
            {
                Id = SeedConstants.StaffRoleId,
                Name = RoleEnum.Staff,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new Roles
            {
                Id = SeedConstants.StudentRoleId,
                Name = RoleEnum.Student,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            },
            new Roles
            {
                Id = SeedConstants.LecturerRoleId,
                Name = RoleEnum.Lecturer,
                IsDisable = false,
                CreatedAt = SeedConstants.CreatedAt,
                UpdatedAt = SeedConstants.CreatedAt
            }
        );
    }
}
