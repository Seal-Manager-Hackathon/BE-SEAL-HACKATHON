using Hackathon.Repository.Abtraction;

namespace Hackathon.Repository.Entity;

public class UserRoles : BaseEntity<Guid>, IAuditableEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public Users User { get; set; } = null!;
    public Roles Role { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}