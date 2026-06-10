using Hackathon.Repository.Abtraction;

namespace Hackathon.Repository.Entity;

public class EmailVerifications : BaseEntity<Guid>, IAuditableEntity
{
    public Guid UserId { get; set; }
    public required string TokenHash { get; set; }
    public DateTimeOffset ExpiredAt { get; set; }
    public string? Status { get; set; }

    public Users User { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}