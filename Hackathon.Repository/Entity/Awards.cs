using Hackathon.Repository.Abtraction;

namespace Hackathon.Repository.Entity;

public class Awards : BaseEntity<Guid>, IAuditableEntity
{
    public Guid EventId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? LevelAward { get; set; }
    public int? NumberOfAward { get; set; }
    public decimal? Prize { get; set; }

    public Events Event { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}