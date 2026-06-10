using Hackathon.Repository.Abtraction;

namespace Hackathon.Repository.Entity;

public class Topics : BaseEntity<Guid>, IAuditableEntity
{
    public Guid TrackId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }

    public Tracks Track { get; set; } = null!;
    public ICollection<RegisterTeams> RegisterTeams { get; set; } = new List<RegisterTeams>();

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}