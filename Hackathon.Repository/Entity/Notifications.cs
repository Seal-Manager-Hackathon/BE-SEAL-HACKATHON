using Hackathon.Repository.Abtraction;
using Hackathon.Repository.Enum;

namespace Hackathon.Repository.Entity;

public class Notifications : BaseEntity<Guid>, IAuditableEntity
{
    public Guid? UserId { get; set; }
    public Guid? TeamId { get; set; }
    public string? Title { get; set; }
    public NotificationStatusEnum? Status { get; set; }
    public string? Description { get; set; }
    public NotificationTargetTypeEnum TargetType { get; set; } = NotificationTargetTypeEnum.Personal;

    public Users? User { get; set; }
    public Teams? Team { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}