using Hackathon.Repository.Abtraction;

namespace Hackathon.Repository.Entity;

public class EventRoles : BaseEntity<Guid>, IAuditableEntity
{
    public required string Name { get; set; }

    public ICollection<AssignEvents> AssignEvents { get; set; } = new List<AssignEvents>();

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}