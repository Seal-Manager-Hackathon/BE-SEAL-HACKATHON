using Hackathon.Repository.Abtraction;
using Hackathon.Repository.Enum;

namespace Hackathon.Repository.Entity;

public class Events : BaseEntity<Guid>, IAuditableEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
    public DateTimeOffset? RegisterLimitTime { get; set; }
    public int? LimitTeam { get; set; }
    public int? MinMember { get; set; }
    public int? MaxMember { get; set; }
    public EventStatusEnum? Status { get; set; }
    public int? NumberRound { get; set; }
    public string? Season { get; set; }

    public ICollection<Rounds> Rounds { get; set; } = new List<Rounds>();
    public ICollection<Tracks> Tracks { get; set; } = new List<Tracks>();
    public ICollection<Awards> Awards { get; set; } = new List<Awards>();
    public LeaderBoards? LeaderBoard { get; set; }
    public ICollection<AssignEvents> AssignEvents { get; set; } = new List<AssignEvents>();

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}