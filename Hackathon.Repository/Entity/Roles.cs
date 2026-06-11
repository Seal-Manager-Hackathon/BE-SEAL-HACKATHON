using Hackathon.Repository.Abtraction;
using Hackathon.Repository.Enum;

namespace Hackathon.Repository.Entity;

public class Roles : BaseEntity<Guid>, IAuditableEntity
{
    public RoleEnum Name { get; set; }

    public ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}